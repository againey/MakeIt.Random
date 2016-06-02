/******************************************************************************\
* Copyright Andy Gainey                                                        *
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
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	public sealed class SplitMix64 : BaseRandomEngine
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

		public static SplitMix64 Create(RandomStateGenerator stateGenerator)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed(stateGenerator);
			return instance;
		}

		public static SplitMix64 Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed(seeder);
			return instance;
		}

		public static SplitMix64 CreateWithState(ulong state)
		{
			var instance = CreateInstance<SplitMix64>();
			return instance.CopyState(state);
		}

		public SplitMix64 Clone()
		{
			var instance = CreateInstance<SplitMix64>();
			return instance.CopyState(this);
		}

		public SplitMix64 CopyState(SplitMix64 source)
		{
			_state = source._state;
			return this;
		}

		public SplitMix64 CopyState(ulong state)
		{
			_state = state;
			return this;
		}

		public override void Seed(RandomStateGenerator stateGenerator)
		{
			_state = stateGenerator.Next64();
		}

		public override void Seed(IRandomEngine seeder)
		{
			_state = seeder.Next64();
		}

		public override void MergeSeed(RandomStateGenerator stateGenerator)
		{
			_state ^= stateGenerator.Next64();
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			_state ^= seeder.Next64();
		}

		public override void Step()
		{
			_state += 0x9E3779B97F4A7C15UL;
		}

		public override uint Next32()
		{
			_state += 0x9E3779B97F4A7C15UL;
			var z = _state;
			z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
			z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
			return (uint)(z ^ (z >> 31));
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
