/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// An implementation of the <see cref="IRandomEngine"/> interface using the 128-bit XorShift+ generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG is based on Marsaglia's XorShift class of generators, and modified by Sebastiano Vigna
	/// in his paper <a href="http://vigna.di.unimi.it/ftp/papers/xorshiftplus.pdf">Further scramblings of
	/// Marsaglia's xorshift generators</a>.</para>
	/// 
	/// <para>As its name implies, it maintains 128 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	public sealed class XorShift128Plus : BaseRandomEngine
	{
		[SerializeField] private ulong _state0 = 0UL;
		[SerializeField] private ulong _state1 = 1UL; //to avoid ever having an invalid all 0-bit state

		public static XorShift128Plus Create()
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed();
			return instance;
		}

		public static XorShift128Plus Create(int seed)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift128Plus Create(params int[] seed)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift128Plus Create(string seed)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift128Plus Create(RandomStateGenerator stateGenerator)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(stateGenerator);
			return instance;
		}

		public static XorShift128Plus Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(seeder);
			return instance;
		}

		public static XorShift128Plus CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.RestoreState(stateArray);
			return instance;
		}

		public static XorShift128Plus CreateWithState(ulong state0, ulong state1)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.RestoreState(state0, state1);
			return instance;
		}

		public XorShift128Plus Clone()
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.CopyStateFrom(this);
			return instance;
		}

		public void CopyStateFrom(XorShift128Plus source)
		{
			_state0 = source._state0;
			_state1 = source._state1;
		}

		public override byte[] SaveState()
		{
			var stateArray = new byte[sizeof(ulong) * 2];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					SaveState(writer, _state0);
					SaveState(writer, _state1);
				}
			}
			return stateArray;
		}

		public void SaveState(out ulong state0, out ulong state1)
		{
			state0 = _state0;
			state1 = _state1;
		}

		public override void RestoreState(byte[] stateArray)
		{
			ulong state0;
			ulong state1;
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var reader = new System.IO.BinaryReader(stream))
				{
					RestoreState(reader, out state0);
					RestoreState(reader, out state1);
				}
			}
			RestoreState(state0, state1);
		}

		public void RestoreState(ulong state0, ulong state1)
		{
			if (state0 == 0 && state1 == 0)
			{
				throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift128+ random number generator.");
			}
			_state0 = state0;
			_state1 = state1;
		}

		public override void Seed(RandomStateGenerator stateGenerator)
		{
			int tryCount = 0;
			int offsetIncrement = stateGenerator.ComputeIdealOffsetIncrement(2);

			do
			{
				ulong state0 = stateGenerator.Next64(offsetIncrement);
				ulong state1 = stateGenerator.Next64(offsetIncrement);
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided state generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void Seed(IRandomEngine seeder)
		{
			int tryCount = 0;

			do
			{
				ulong state0 = seeder.Next64();
				ulong state1 = seeder.Next64();
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided random engine was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void MergeSeed(RandomStateGenerator stateGenerator)
		{
			int tryCount = 0;
			int offsetIncrement = stateGenerator.ComputeIdealOffsetIncrement(2);

			do
			{
				ulong state0 = _state0 ^ stateGenerator.Next64(offsetIncrement);
				ulong state1 = _state1 ^ stateGenerator.Next64(offsetIncrement);
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided state generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			int tryCount = 0;

			do
			{
				ulong state0 = _state0 ^ seeder.Next64();
				ulong state1 = _state1 ^ seeder.Next64();
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided random engine was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void Step()
		{
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
		}

		public override uint Next32()
		{
			var x = _state0;
			var y = _state1;
			var next = (uint)(x + y);
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5);
			return next;
		}

		public override ulong Next64()
		{
			var x = _state0;
			var y = _state1;
			var next = x + y;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5);
			return next;
		}

		public override int skipAheadMagnitude { get { return 64; } }

		public override void SkipAhead()
		{
			ulong x = 0;
			ulong y = 0;

			ulong b = 0x8A5CD789635D2DFFUL;
			for (int i = 0; i < 64; ++i)
			{
				if ((b & 1UL) != 0UL)
				{
					x ^= _state0;
					y ^= _state1;
				}
				b >>= 1;
				Step();
			}

			b = 0x121FD2155C472F96UL;
			for (int i = 0; i < 64; ++i)
			{
				if ((b & 1UL) != 0UL)
				{
					x ^= _state0;
					y ^= _state1;
				}
				b >>= 1;
				Step();
			}

			_state0 = x;
			_state1 = y;
		}

		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper64(this);
		}
	}
}
