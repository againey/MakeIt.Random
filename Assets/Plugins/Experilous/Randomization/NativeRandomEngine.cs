/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// Adapts the standard random engine class from the .NET libary to the <see cref="IRandomEngine"/> interface.
	/// </summary>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	/// <seealso cref="System.Random"/>
	public sealed class NativeRandomEngine : BaseRandomEngine
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

		public NativeRandomEngine Clone()
		{
			var instance = CreateInstance<NativeRandomEngine>();
			return instance.CopyState(this);
		}

		public NativeRandomEngine CopyState(NativeRandomEngine source)
		{
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream())
			{
				binaryFormatter.Serialize(stream, source._random);
				_random = (System.Random)binaryFormatter.Deserialize(stream);
			}
			return this;
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
			Seed(new RandomStateGenerator(seed));
		}

		public override void Seed(string seed)
		{
			Seed(new RandomStateGenerator(seed));
		}

		public override void Seed(RandomStateGenerator stateGenerator)
		{
			_random = new System.Random((int)stateGenerator.Next32());
		}

		public override void Seed(IRandomEngine seeder)
		{
			_random = new System.Random((int)seeder.Next32());
		}

		public override void MergeSeed()
		{
			MergeSeed(new RandomStateGenerator());
		}

		public override void MergeSeed(int seed)
		{
			MergeSeed(Create(seed));
		}

		public override void MergeSeed(params int[] seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		public override void MergeSeed(string seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		public override void MergeSeed(RandomStateGenerator stateGenerator)
		{
			_random = new System.Random((int)(Next32() ^ stateGenerator.Next32()));
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			_random = new System.Random((int)(Next32() ^ seeder.Next32()));
		}

		public override void Step()
		{
			_random.NextBytes(_buffer);
		}

		public override uint Next32()
		{
			_random.NextBytes(_buffer);
			return BitConverter.ToUInt32(_buffer, 0);
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
