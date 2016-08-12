/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.MakeIt.Random
{
	/// <summary>
	/// An implementation of the <see cref="IRandom"/> interface using the 64-bit SplitMix generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG implements the SplitMix64 algorithm provided by Sebastiano Vigna, based on the
	/// following <a href="http://xorshift.di.unimi.it/splitmix64.c">implementation in C</a>.</para>
	/// 
	/// <para>As its name implies, it maintains 64 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	public sealed class SplitMix64 : RandomBase
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

		public static SplitMix64 Create(IBitGenerator bitGenerator)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.Seed(bitGenerator);
			return instance;
		}

		public static SplitMix64 CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.RestoreState(stateArray);
			return instance;
		}

		public static SplitMix64 CreateWithState(ulong state)
		{
			var instance = CreateInstance<SplitMix64>();
			instance.RestoreState(state);
			return instance;
		}

		public SplitMix64 Clone()
		{
			var instance = CreateInstance<SplitMix64>();
			instance.CopyStateFrom(this);
			return instance;
		}

		public void CopyStateFrom(SplitMix64 source)
		{
			_state = source._state;
		}

		public override byte[] SaveState()
		{
			var stateArray = new byte[sizeof(ulong)];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					SaveState(writer, _state);
				}
			}
			return stateArray;
		}

		public void SaveState(out ulong state)
		{
			state = _state;
		}

		public override void RestoreState(byte[] stateArray)
		{
			ulong state;
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var reader = new System.IO.BinaryReader(stream))
				{
					RestoreState(reader, out state);
				}
			}
			RestoreState(state);
		}

		public void RestoreState(ulong state)
		{
			_state = state;
		}

#if RANDOMIZATION_COMPAT_V1_0
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

		public override void Seed(IBitGenerator bitGenerator)
		{
			_state = bitGenerator.Next64();
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

		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			_state ^= bitGenerator.Next64();
		}
#else
		public override void Seed(IBitGenerator bitGenerator)
		{
			_state = bitGenerator.Next64();
		}

		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			_state ^= bitGenerator.Next64();
		}
#endif

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
