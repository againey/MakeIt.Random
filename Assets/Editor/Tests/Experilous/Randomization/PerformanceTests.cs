/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Randomization.Tests
{
	class PerformanceTests
	{
		private static string seed = "random seed";
		private const int iterations = 10000000;
		private volatile uint result32;
		private volatile uint result64;

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
	}
}
#endif
