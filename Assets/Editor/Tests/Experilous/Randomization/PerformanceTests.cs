/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using NUnit.Framework;

namespace Experilous.Randomization.Tests
{
	class PerformanceTests
	{
		private static string seed = "random seed";
		private const int iterations = 100000000;
		private volatile uint result32;
		private volatile uint result64;
		private volatile float resultFloat;

		public static uint Generate32(int count, IRandomEngine engine)
		{
			for (int i = 1; i < count; ++i)
			{
				engine.Next32();
			}
			return engine.Next32();
		}

		public static ulong Generate64(int count, IRandomEngine engine)
		{
			for (int i = 1; i < count; ++i)
			{
				engine.Next64();
			}
			return engine.Next64();
		}

		public static float GenerateClosedFloat(int count, IRandomEngine engine)
		{
			float sum = 0f;
			int halfCount = count / 2;
			for (int i = 1; i < halfCount; ++i)
			{
				sum += RandomUnit.ClosedFloat(engine) - RandomUnit.ClosedFloat(engine);
			}
			return sum + RandomUnit.ClosedFloat(engine);
		}

		public static float GenerateClosedFloatFast32(int count, IRandomEngine engine)
		{
			float sum = 0f;
			int halfCount = count / 2;
			for (int i = 1; i < halfCount; ++i)
			{
				sum += RandomUnit.ClosedFloatFast32(engine) - RandomUnit.ClosedFloatFast32(engine);
			}
			return sum + RandomUnit.ClosedFloatFast32(engine);
		}

		public static float GenerateClosedFloatFast64(int count, IRandomEngine engine)
		{
			float sum = 0f;
			int halfCount = count / 2;
			for (int i = 1; i < halfCount; ++i)
			{
				sum += RandomUnit.ClosedFloatFast64(engine) - RandomUnit.ClosedFloatFast64(engine);
			}
			return sum + RandomUnit.ClosedFloatFast64(engine);
		}

		public static double GenerateHalfOpenDouble(int count, IRandomEngine engine)
		{
			double sum = 0.0;
			int halfCount = count / 2;
			for (int i = 1; i < halfCount; ++i)
			{
				sum += RandomUnit.HalfOpenDouble(engine) - RandomUnit.HalfOpenDouble(engine);
			}
			return sum + RandomUnit.HalfOpenDouble(engine);
		}

		[Test]
		public void UnityRandomEngine_32()
		{
			result32 = Generate32(iterations, UnityRandomEngine.Create(seed));
		}

		[Test]
		public void UnityRandomEngine_64()
		{
			ulong result = Generate64(iterations, UnityRandomEngine.Create(seed));
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void UnityRandomEngine_RawBinaryStep()
		{
			for (int i = 1; i < iterations; ++i)
			{
				UnityEngine.Random.Range(0, 2);
			}
			int result = UnityEngine.Random.Range(0, 2);
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void UnityRandomEngine_RawIntegerStep()
		{
			for (int i = 1; i < iterations; ++i)
			{
				UnityEngine.Random.Range(0, int.MaxValue);
			}
			int result = UnityEngine.Random.Range(0, 2);
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void UnityRandomEngine_RawFloatStep()
		{
			float sum = 0f;
			int halfIterations = iterations / 2;
			for (int i = 1; i < halfIterations; ++i)
			{
				sum += UnityEngine.Random.value - UnityEngine.Random.value;
			}
			float result = sum + UnityEngine.Random.value;
			resultFloat = result;
		}

		[Test]
		public void NativeRandomEngine_32()
		{
			result32 = Generate32(iterations, NativeRandomEngine.Create(seed));
		}

		[Test]
		public void NativeRandomEngine_64()
		{
			ulong result = Generate64(iterations, NativeRandomEngine.Create(seed));
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void NativeRandomEngine_RawIntegerStep()
		{
			var random = new System.Random((int)(new RandomStateGenerator(seed).Next32()));
			for (int i = 1; i < iterations; ++i)
			{
				random.Next();
			}
			int result = random.Next();
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void NativeRandomEngine_RawDoubleStep()
		{
			var random = new System.Random((int)(new RandomStateGenerator(seed).Next32()));
			double sum = 0.0;
			int halfIterations = iterations / 2;
			for (int i = 1; i < halfIterations; ++i)
			{
				sum += random.NextDouble() - random.NextDouble();
			}
			double result = sum + random.NextDouble();
			resultFloat = (float)result;
		}

		[Test]
		public void SplitMix64_32()
		{
			result32 = Generate32(iterations, SplitMix64.Create(seed));
		}

		[Test]
		public void SplitMix64_64()
		{
			ulong result = Generate64(iterations, SplitMix64.Create(seed));
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void SplitMix64_ClosedFloat()
		{
			float result = GenerateClosedFloat(iterations, SplitMix64.Create(seed));
			resultFloat = result;
		}

		[Test]
		public void SplitMix64_HalfOpenDouble()
		{
			double result = GenerateHalfOpenDouble(iterations, SplitMix64.Create(seed));
			resultFloat = (float)result;
		}

		[Test]
		public void XorShift128Plus_32()
		{
			result32 = Generate32(iterations, XorShift128Plus.Create(seed));
		}

		[Test]
		public void XorShift128Plus_64()
		{
			ulong result = Generate64(iterations, XorShift128Plus.Create(seed));
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void XorShift128Plus_ClosedFloat()
		{
			float result = GenerateClosedFloat(iterations, XorShift128Plus.Create(seed));
			resultFloat = result;
		}

		[Test]
		public void XorShift128Plus_ClosedFloatFast32()
		{
			float result = GenerateClosedFloatFast32(iterations, XorShift128Plus.Create(seed));
			resultFloat = result;
		}

		[Test]
		public void XorShift128Plus_ClosedFloatFast64()
		{
			float result = GenerateClosedFloatFast64(iterations, XorShift128Plus.Create(seed));
			resultFloat = result;
		}

		[Test]
		public void XorShift128Plus_HalfOpenDouble()
		{
			double result = GenerateHalfOpenDouble(iterations, XorShift128Plus.Create(seed));
			resultFloat = (float)result;
		}

		[Test]
		public void XoroShiro128Plus_32()
		{
			result32 = Generate32(iterations, XoroShiro128Plus.Create(seed));
		}

		[Test]
		public void XoroShiro128Plus_64()
		{
			ulong result = Generate64(iterations, XoroShiro128Plus.Create(seed));
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void XoroShiro128Plus_ClosedFloat()
		{
			float result = GenerateClosedFloat(iterations, XoroShiro128Plus.Create(seed));
			resultFloat = result;
		}

		[Test]
		public void XoroShiro128Plus_HalfOpenDouble()
		{
			double result = GenerateHalfOpenDouble(iterations, XoroShiro128Plus.Create(seed));
			resultFloat = (float)result;
		}

		[Test]
		public void XorShift1024Star_32()
		{
			result32 = Generate32(iterations, XorShift1024Star.Create(seed));
		}

		[Test]
		public void XorShift1024Star_64()
		{
			ulong result = Generate64(iterations, XorShift1024Star.Create(seed));
			result32 = (uint)result;
			result64 = (uint)(result >> 32);
		}

		[Test]
		public void XorShift1024Star_ClosedFloat()
		{
			float result = GenerateClosedFloat(iterations, XorShift1024Star.Create(seed));
			resultFloat = result;
		}

		[Test]
		public void XorShift1024Star_HalfOpenDouble()
		{
			double result = GenerateHalfOpenDouble(iterations, XorShift1024Star.Create(seed));
			resultFloat = (float)result;
		}
	}
}
#endif
