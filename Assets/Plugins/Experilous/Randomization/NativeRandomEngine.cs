/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// Adapts the standard random engine class from the .NET libary to the <see cref="IRandomEngine"/> interface.
	/// </summary>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	/// <seealso cref="System.Random"/>
	public class NativeRandomEngine : BaseRandomEngine, IRandomEngine
	{
		[SerializeField] private System.Random _random;
		[SerializeField] private byte[] _buffer = new byte[4];

		public static NativeRandomEngine Create()
		{
			var instance = CreateInstance<NativeRandomEngine>();
			instance.Seed();
			return instance;
		}

		public static NativeRandomEngine Create(int seed)
		{
			var instance = CreateInstance<NativeRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static NativeRandomEngine Create(params int[] seed)
		{
			var instance = CreateInstance<NativeRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static NativeRandomEngine Create(string seed)
		{
			var instance = CreateInstance<NativeRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static NativeRandomEngine Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<NativeRandomEngine>();
			instance.Seed(seeder);
			return instance;
		}

		public override void Seed()
		{
			_random = new System.Random();
		}

		public override void Seed(int seed)
		{
			_random = new System.Random(seed);
		}

		public override void Seed(params int[] seed)
		{
			_random = new System.Random((int)RandomSeedUtility.Seed32(seed));
		}

		public override void Seed(string seed)
		{
			_random = new System.Random((int)RandomSeedUtility.Seed32(seed));
		}

		public override void Seed(IRandomEngine seeder)
		{
			_random = new System.Random((int)seeder.Next32());
		}

		public override void MergeSeed()
		{
			_random = new System.Random((int)(Next32() ^ RandomSeedUtility.Seed32()));
		}

		public override void MergeSeed(int seed)
		{
			_random = new System.Random((int)(Next32() ^ RandomSeedUtility.Seed32(seed)));
		}

		public override void MergeSeed(params int[] seed)
		{
			_random = new System.Random((int)(Next32() ^ RandomSeedUtility.Seed32(seed)));
		}

		public override void MergeSeed(string seed)
		{
			_random = new System.Random((int)(Next32() ^ RandomSeedUtility.Seed32(seed)));
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			_random = new System.Random((int)(Next32() ^ seeder.Next32()));
		}

		public override uint Next32()
		{
			_random.NextBytes(_buffer);
			return System.BitConverter.ToUInt32(_buffer, 0);
		}

		public override ulong Next64()
		{
			return ((ulong)Next32() << 32) | Next32();
		}

		public override System.Random AsSystemRandom()
		{
			return _random;
		}
	}
}
