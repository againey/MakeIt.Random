/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// An implementation of the <see cref="IRandomEngine"/> interface using the 64-bit SplitMix generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG implements the SplitMix64 algorithm provided by Sebastiano Vigna, based on the
	/// following <a href="http://xorshift.di.unimi.it/splitmix64.c">implementation in C</a>.</para>
	/// 
	/// <para>As its name implies, it maintains 64 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.  It can serve as a convenient helper for seeding another PRNG with a larger
	/// state when starting from a small seed.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	/// <seealso cref="XorShift128Plus"/>
	public class SplitMix64 : BaseRandomEngine, IRandomEngine
	{
		[SerializeField] private ulong _state;

		public static SplitMix64 Create()
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed();
			return instance;
		}

		public static SplitMix64 Create(int seed)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed(seed);
			return instance;
		}

		public static SplitMix64 Create(params int[] seed)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed(seed);
			return instance;
		}

		public static SplitMix64 Create(string seed)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed(seed);
			return instance;
		}

		public static SplitMix64 Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed(seeder);
			return instance;
		}

		public override void Seed()
		{
			_state = RandomSeedUtility.Seed64();
		}

		public override void Seed(int seed)
		{
			_state = RandomSeedUtility.Seed64(seed);
		}

		public override void Seed(params int[] seed)
		{
			_state = RandomSeedUtility.Seed64(seed);
		}

		public override void Seed(string seed)
		{
			_state = RandomSeedUtility.Seed64(seed);
		}

		public override void Seed(IRandomEngine seeder)
		{
			_state = seeder.Next64();
		}

		public override void MergeSeed()
		{
			_state ^= RandomSeedUtility.Seed64();
		}

		public override void MergeSeed(int seed)
		{
			_state ^= RandomSeedUtility.Seed64(seed);
		}

		public override void MergeSeed(params int[] seed)
		{
			_state ^= RandomSeedUtility.Seed64(seed);
		}

		public override void MergeSeed(string seed)
		{
			_state ^= RandomSeedUtility.Seed64(seed);
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			_state ^= seeder.Next64();
		}

		public override uint Next32()
		{
			return (uint)Next64();
		}

		public override ulong Next64()
		{
			_state += 0x9E3779B97F4A7C15UL;
			var z = _state;
			z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
			z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
			return z ^ (z >> 31);
		}

		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper64(this);
		}
	}
}
