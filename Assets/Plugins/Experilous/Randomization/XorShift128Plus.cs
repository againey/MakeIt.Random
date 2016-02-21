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
	/// random data at a time.  It uses the <see cref="SplitMix64"/> random engine for many of its seeding
	/// functions, as recommended by Sebastiano Vigna.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	/// <seealso cref="SplitMix64"/>
	public class XorShift128Plus : BaseRandomEngine, IRandomEngine
	{
		[SerializeField] private ulong _state0;
		[SerializeField] private ulong _state1;

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

		public static XorShift128Plus Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(seeder);
			return instance;
		}

		public override void Seed()
		{
			Seed(SplitMix64.Create());
		}

		public override void Seed(int seed)
		{
			Seed(SplitMix64.Create(seed));
		}

		public override void Seed(params int[] seed)
		{
			Seed(SplitMix64.Create(seed));
		}

		public override void Seed(string seed)
		{
			Seed(SplitMix64.Create(seed));
		}

		public override void Seed(IRandomEngine seeder)
		{
			_state0 = seeder.Next64();
			_state1 = seeder.Next64();
		}

		public override void MergeSeed()
		{
			MergeSeed(SplitMix64.Create());
		}

		public override void MergeSeed(int seed)
		{
			MergeSeed(SplitMix64.Create(seed));
		}

		public override void MergeSeed(params int[] seed)
		{
			MergeSeed(SplitMix64.Create(seed));
		}

		public override void MergeSeed(string seed)
		{
			MergeSeed(SplitMix64.Create(seed));
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			_state0 ^= seeder.Next64();
			_state1 ^= seeder.Next64();
		}

		public override uint Next32()
		{
			return (uint)Next64();
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

		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper64(this);
		}
	}
}
