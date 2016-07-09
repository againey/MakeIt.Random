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
	public sealed class SystemRandomEngine : BaseRandomEngine
	{
		[SerializeField] private System.Random _random;

		public static SystemRandomEngine Create()
		{
			var instance = CreateInstance<SystemRandomEngine>();
			instance.Seed();
			return instance;
		}

		public static SystemRandomEngine Create(int seed)
		{
			var instance = CreateInstance<SystemRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static SystemRandomEngine Create(params int[] seed)
		{
			var instance = CreateInstance<SystemRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static SystemRandomEngine Create(string seed)
		{
			var instance = CreateInstance<SystemRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static SystemRandomEngine Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<SystemRandomEngine>();
			instance.Seed(seeder);
			return instance;
		}

		public static SystemRandomEngine CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<SystemRandomEngine>();
			instance.RestoreState(stateArray);
			return instance;
		}

		public SystemRandomEngine Clone()
		{
			var instance = CreateInstance<SystemRandomEngine>();
			instance.CopyStateFrom(this);
			return instance;
		}

		public void CopyStateFrom(SystemRandomEngine source)
		{
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream())
			{
				binaryFormatter.Serialize(stream, source._random);
				_random = (System.Random)binaryFormatter.Deserialize(stream);
			}
		}

		public override byte[] SaveState()
		{
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream())
			{
				binaryFormatter.Serialize(stream, _random);
				return stream.ToArray();
			}
		}

		public override void RestoreState(byte[] stateArray)
		{
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				_random = (System.Random)binaryFormatter.Deserialize(stream);
			}
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
			_random.Next();
		}

		public override uint Next32()
		{
			return
				(uint)_random.Next() << 1 & 0xFFFF0000U |
				(uint)_random.Next() & 0x0000FFFFU;
		}

		public override ulong Next64()
		{
			return
				(ulong)_random.Next() << 33 & 0xFFFFFF0000000000UL |
				(ulong)_random.Next() << 12 & 0x000000FFFFFF0000UL |
				(ulong)_random.Next() & 0x000000000000FFFFUL;
		}

		public override System.Random AsSystemRandom()
		{
			return _random;
		}
	}
}
