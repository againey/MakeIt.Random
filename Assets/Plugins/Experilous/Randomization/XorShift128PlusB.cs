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
	/// <para>As its name implies, it maintains 128 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// <para>This is an earlier version of the XorShift128Plus class, available for backwards-compatible
	/// consistency of earlier random sequences.  It differs from the up-to-date version in how it uses
	/// seeds of various types to initialize its state, as well as with the set of bit shift amounts that
	/// it uses to step through subsequent states.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	/// <seealso cref="XorShift128Plus"/>
	public sealed class XorShift128PlusB : BaseRandomEngine
	{
		[SerializeField] private ulong _state0 = 0UL;
		[SerializeField] private ulong _state1 = 1UL; //to avoid ever having an invalid all 0-bit state

		public static XorShift128PlusB Create()
		{
			var instance = CreateInstance<XorShift128PlusB>();
			instance.Seed();
			return instance;
		}

		public static XorShift128PlusB Create(int seed)
		{
			var instance = CreateInstance<XorShift128PlusB>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift128PlusB Create(params int[] seed)
		{
			var instance = CreateInstance<XorShift128PlusB>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift128PlusB Create(string seed)
		{
			var instance = CreateInstance<XorShift128PlusB>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift128PlusB Create(RandomStateGenerator stateGenerator)
		{
			var instance = CreateInstance<XorShift128PlusB>();
			instance.Seed(stateGenerator);
			return instance;
		}

		public static XorShift128PlusB Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<XorShift128PlusB>();
			instance.Seed(seeder);
			return instance;
		}

		public static XorShift128PlusB CreateWithState(ulong state0, ulong state1)
		{
			var instance = CreateInstance<XorShift128PlusB>();
			return instance.CopyState(state0, state1);
		}

		public XorShift128PlusB Clone()
		{
			var instance = CreateInstance<XorShift128PlusB>();
			return instance.CopyState(this);
		}

		public XorShift128PlusB CopyState(XorShift128PlusB source)
		{
			_state0 = source._state0;
			_state1 = source._state1;
			return this;
		}

		public XorShift128PlusB CopyState(ulong state0, ulong state1)
		{
			if (state0 == 0 && state1 == 0)
			{
				throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift128+ random number generator.");
			}
			_state0 = state0;
			_state1 = state1;
			return this;
		}

		public override void Seed()
		{
			Seed(SplitMix64B.Create());
		}

		public override void Seed(int seed)
		{
			Seed(SplitMix64B.Create(seed));
		}

		public override void Seed(params int[] seed)
		{
			Seed(SplitMix64B.Create(seed));
		}

		public override void Seed(string seed)
		{
			Seed(SplitMix64B.Create(seed));
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

		public override void MergeSeed()
		{
			MergeSeed(SplitMix64B.Create());
		}

		public override void MergeSeed(int seed)
		{
			MergeSeed(SplitMix64B.Create(seed));
		}

		public override void MergeSeed(params int[] seed)
		{
			MergeSeed(SplitMix64B.Create(seed));
		}

		public override void MergeSeed(string seed)
		{
			MergeSeed(SplitMix64B.Create(seed));
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
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
		}

		public override uint Next32()
		{
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
			return (uint)(_state1 + y);
		}

		public override ulong Next64()
		{
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
			return _state1 + y;
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
