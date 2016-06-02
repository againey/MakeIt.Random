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
	/// <para>As its name implies, it maintains 64 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// <para>This is an earlier version of the SplitMix64 class, available for backwards-compatible
	/// consistency of earlier random sequences.  It differs from the up-to-date version in how it uses
	/// seeds of various types to initialize its state.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	/// <seealso cref="SplitMix64"/>
	public sealed class SplitMix64B : BaseRandomEngine
	{
		[SerializeField] private ulong _state;

		public static SplitMix64B Create()
		{
			var instance = CreateInstance<SplitMix64B>();
			instance.Seed();
			return instance;
		}

		public static SplitMix64B Create(int seed)
		{
			var instance = CreateInstance<SplitMix64B>();
			instance.Seed(seed);
			return instance;
		}

		public static SplitMix64B Create(params int[] seed)
		{
			var instance = CreateInstance<SplitMix64B>();
			instance.Seed(seed);
			return instance;
		}

		public static SplitMix64B Create(string seed)
		{
			var instance = CreateInstance<SplitMix64B>();
			instance.Seed(seed);
			return instance;
		}

		public static SplitMix64B Create(RandomStateGenerator stateGenerator)
		{
			var instance = CreateInstance<SplitMix64B>();
			instance.Seed(stateGenerator);
			return instance;
		}

		public static SplitMix64B Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<SplitMix64B>();
			instance.Seed(seeder);
			return instance;
		}

		public static SplitMix64B CreateWithState(ulong state)
		{
			var instance = CreateInstance<SplitMix64B>();
			return instance.CopyState(state);
		}

		public SplitMix64B Clone()
		{
			var instance = CreateInstance<SplitMix64B>();
			return instance.CopyState(this);
		}

		public SplitMix64B CopyState(SplitMix64B source)
		{
			_state = source._state;
			return this;
		}

		public SplitMix64B CopyState(ulong state)
		{
			_state = state;
			return this;
		}

		private static ulong Hash(byte[] seed)
		{
			ulong h = 14695981039346656037UL;
			for (int i = 0; i < seed.Length; ++i)
			{
				h = (h ^ seed[i]) * 1099511628211UL;
			}
			return h;
		}

		public override void Seed()
		{
			_state = Hash(System.BitConverter.GetBytes(System.Environment.TickCount));
		}

		public override void Seed(int seed)
		{
			_state = Hash(System.BitConverter.GetBytes(seed));
		}

		public override void Seed(params int[] seed)
		{
			var byteData = new byte[seed.Length * 4];
			System.Buffer.BlockCopy(seed, 0, byteData, 0, byteData.Length);
			_state = Hash(byteData);
		}

		public override void Seed(string seed)
		{
			_state = Hash(new System.Text.UTF8Encoding().GetBytes(seed));
		}

		public override void Seed(RandomStateGenerator stateGenerator)
		{
			_state = stateGenerator.Next64();
		}

		public override void Seed(IRandomEngine seeder)
		{
			_state = seeder.Next64();
		}

		public override void MergeSeed()
		{
			_state ^= Hash(System.BitConverter.GetBytes(System.Environment.TickCount));
		}

		public override void MergeSeed(int seed)
		{
			_state ^= Hash(System.BitConverter.GetBytes(seed));
		}

		public override void MergeSeed(params int[] seed)
		{
			var byteData = new byte[seed.Length * 4];
			System.Buffer.BlockCopy(seed, 0, byteData, 0, byteData.Length);
			_state ^= Hash(byteData);
		}

		public override void MergeSeed(string seed)
		{
			_state ^= Hash(new System.Text.UTF8Encoding().GetBytes(seed));
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
