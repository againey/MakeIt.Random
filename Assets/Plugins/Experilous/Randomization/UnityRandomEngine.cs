/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// Adapts the standard Unity random class to the <see cref="IRandomEngine"/> interface.
	/// </summary>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	/// <seealso cref="UnityEngine.Random"/>
	public sealed class UnityRandomEngine : BaseRandomEngine
	{
		[SerializeField] private byte[] _buffer = new byte[4];

		public static UnityRandomEngine Create()
		{
			var instance = CreateInstance<UnityRandomEngine>();
			instance.Seed();
			return instance;
		}

		public static UnityRandomEngine Create(int seed)
		{
			var instance = CreateInstance<UnityRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static UnityRandomEngine Create(params int[] seed)
		{
			var instance = CreateInstance<UnityRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static UnityRandomEngine Create(string seed)
		{
			var instance = CreateInstance<UnityRandomEngine>();
			instance.Seed(seed);
			return instance;
		}

		public static UnityRandomEngine Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<UnityRandomEngine>();
			instance.Seed(seeder);
			return instance;
		}

		public static UnityRandomEngine CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<UnityRandomEngine>();
			instance.RestoreState(stateArray);
			return instance;
		}

		public UnityRandomEngine Clone()
		{
			var instance = CreateInstance<UnityRandomEngine>();
			return instance;
		}

		public void CopyStateFrom(UnityRandomEngine source)
		{
			// Since Unity uses a shared global instance, this function is a no-op.
			// Maintained for consistency with other random engine classes.
		}

		public override byte[] SaveState()
		{
			throw new NotSupportedException("The Unity Random class does not expose any method to save its state.");
		}

		public override void RestoreState(byte[] stateArray)
		{
			throw new NotSupportedException("The Unity Random class does not expose any method to restore its state.");
		}

		public override void Seed(RandomStateGenerator stateGenerator)
		{
			UnityEngine.Random.seed = (int)stateGenerator.Next32();
		}

		public override void Seed(IRandomEngine seeder)
		{
			UnityEngine.Random.seed = (int)seeder.Next32();
		}

		public override void MergeSeed(RandomStateGenerator stateGenerator)
		{
			UnityEngine.Random.seed ^= (int)stateGenerator.Next32();
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			UnityEngine.Random.seed ^= (int)seeder.Next32();
		}

		public override void Step()
		{
			float throwaway = UnityEngine.Random.value;
		}

		private uint Next16()
		{
			return (uint)UnityEngine.Random.Range(0, 65536);
		}

		private uint Next24()
		{
			return (uint)UnityEngine.Random.Range(0, 16777216);
		}

		public override uint Next32()
		{
			return (Next16() << 16) | Next16();
		}

		public override ulong Next64()
		{
			return ((ulong)Next16() << 48) | ((ulong)Next24() << 24) | Next24();
		}

		public override uint NextLessThan(uint upperBound)
		{
			if (upperBound < int.MaxValue)
			{
				return (uint)UnityEngine.Random.Range(0, (int)upperBound);
			}
			else
			{
				return base.NextLessThan(upperBound);
			}
		}

		public override uint NextLessThanOrEqual(uint upperBound)
		{
			if (upperBound > 0 && upperBound - 1 < int.MaxValue)
			{
				return (uint)UnityEngine.Random.Range(0, (int)upperBound + 1);
			}
			else
			{
				return base.NextLessThan(upperBound);
			}
		}

		public override ulong NextLessThan(ulong upperBound)
		{
			if (upperBound < int.MaxValue)
			{
				return (ulong)UnityEngine.Random.Range(0, (int)upperBound);
			}
			else
			{
				return base.NextLessThan(upperBound);
			}
		}

		public override ulong NextLessThanOrEqual(ulong upperBound)
		{
			if (upperBound > 0 && upperBound - 1 < int.MaxValue)
			{
				return (ulong)UnityEngine.Random.Range(0, (int)upperBound + 1);
			}
			else
			{
				return base.NextLessThan(upperBound);
			}
		}

		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper64(this);
		}
	}
}
