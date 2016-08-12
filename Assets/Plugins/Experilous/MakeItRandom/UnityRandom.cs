/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous.MakeIt.Random
{
	/// <summary>
	/// Adapts the standard Unity random class to the <see cref="IRandom"/> interface.
	/// </summary>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	/// <seealso cref="UnityEngine.Random"/>
	public sealed class UnityRandom : RandomBase
	{
		public static UnityRandom Create()
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed();
			return instance;
		}

		public static UnityRandom Create(int seed)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(seed);
			return instance;
		}

		public static UnityRandom Create(params int[] seed)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(seed);
			return instance;
		}

		public static UnityRandom Create(string seed)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(seed);
			return instance;
		}

		public static UnityRandom Create(IBitGenerator bitGenerator)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(bitGenerator);
			return instance;
		}

		public static UnityRandom CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.RestoreState(stateArray);
			return instance;
		}

		public UnityRandom Clone()
		{
			var instance = CreateInstance<UnityRandom>();
			return instance;
		}

		public void CopyStateFrom(UnityRandom source)
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

		public override void Seed(IBitGenerator bitGenerator)
		{
			UnityEngine.Random.seed = (int)bitGenerator.Next32();
		}

		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			UnityEngine.Random.seed ^= (int)bitGenerator.Next32();
		}

		public override void Step()
		{
#pragma warning disable 0219
			float throwaway = UnityEngine.Random.value;
#pragma warning restore 0219
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

		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper64(this);
		}
	}
}
