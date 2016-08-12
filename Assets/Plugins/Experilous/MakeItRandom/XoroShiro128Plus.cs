/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// An implementation of the <see cref="IRandomEngine"/> interface using the 128-bit XoroShiro+ generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG is based on David Blackman's and Sebastiano Vigna's xoroshiro128+ generator, adapted from
	/// a <a href="http://xorshift.di.unimi.it/xoroshiro128plus.c">C code reference implementation</a>.</para>
	/// 
	/// <para>As its name implies, it maintains 128 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	public sealed class XoroShiro128Plus : BaseRandomEngine
	{
		[SerializeField] private ulong _state0 = 0UL;
		[SerializeField] private ulong _state1 = 1UL; //to avoid ever having an invalid all 0-bit state

		public static XoroShiro128Plus Create()
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.Seed();
			return instance;
		}

		public static XoroShiro128Plus Create(int seed)
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.Seed(seed);
			return instance;
		}

		public static XoroShiro128Plus Create(params int[] seed)
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.Seed(seed);
			return instance;
		}

		public static XoroShiro128Plus Create(string seed)
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.Seed(seed);
			return instance;
		}

		public static XoroShiro128Plus Create(IBitGenerator bitGenerator)
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.Seed(bitGenerator);
			return instance;
		}

		public static XoroShiro128Plus CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.RestoreState(stateArray);
			return instance;
		}

		public static XoroShiro128Plus CreateWithState(ulong state0, ulong state1)
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.RestoreState(state0, state1);
			return instance;
		}

		public XoroShiro128Plus Clone()
		{
			var instance = CreateInstance<XoroShiro128Plus>();
			instance.CopyStateFrom(this);
			return instance;
		}

		public void CopyStateFrom(XoroShiro128Plus source)
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
				throw new System.ArgumentException("All 0 bits is an invalid state for the XoroShiro128+ random number generator.");
			}
			_state0 = state0;
			_state1 = state1;
		}

		public override void Seed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				ulong state0 = bitGenerator.Next64();
				ulong state1 = bitGenerator.Next64();
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				ulong state0 = _state0 ^ bitGenerator.Next64();
				ulong state1 = _state1 ^ bitGenerator.Next64();
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void Step()
		{
			var x = _state0;
			var y = _state1;
			y ^= x;
			_state0 = ((x << 55) | (x >> 9)) ^ y ^ (y << 14);
			_state1 = (y << 36) | (y >> 28);
		}

		public override uint Next32()
		{
			var x = _state0;
			var y = _state1;
			var next = (uint)(x + y);
			y ^= x;
			_state0 = ((x << 55) | (x >> 9)) ^ y ^ (y << 14);
			_state1 = (y << 36) | (y >> 28);
			return next;
		}

		public override ulong Next64()
		{
			var x = _state0;
			var y = _state1;
			var next = x + y;
			y ^= x;
			_state0 = ((x << 55) | (x >> 9)) ^ y ^ (y << 14);
			_state1 = (y << 36) | (y >> 28);
			return next;
		}

		public override int skipAheadMagnitude { get { return 64; } }

		public override void SkipAhead()
		{
			ulong x = 0;
			ulong y = 0;

			ulong b = 0xBEAC0467EBA5FACBUL;
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

			b = 0xD86B048B86AA9922UL;
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
