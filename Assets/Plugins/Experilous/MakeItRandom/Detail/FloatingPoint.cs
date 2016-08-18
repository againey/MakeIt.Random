/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if (UNITY_64 || MAKEITRANDOM_64) && !MAKEITRANDOM_32
#define OPTIMIZE_FOR_64
#else
#define OPTIMIZE_FOR_32
#endif

using System.Runtime.InteropServices;

namespace Experilous.MakeItRandom.Detail
{
	/// <summary>
	/// A static class of lookup tables and methods for quickly looking up common attributes associated with integer values.
	/// </summary>
	public static class FloatingPoint
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseFloat
		{
			[FieldOffset(0)]
			public uint bits;
			[FieldOffset(0)]
			public float number;
		}

#if OPTIMIZE_FOR_32
		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseDouble
		{
			[FieldOffset(0)]
			public uint lowerBits;
			[FieldOffset(4)]
			public uint upperBits;
			[FieldOffset(0)]
			public double number;
		}
#else
		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseDouble
		{
			[FieldOffset(0)]
			public ulong bits;
			[FieldOffset(0)]
			public double number;
		}
#endif

		public const int floatExponentBitCount = 8;
		public const int floatMantissaBitCount = 23;
		public const uint floatSignMask = 1U << (floatExponentBitCount + floatMantissaBitCount);
		public const uint floatExponentMask = ((1U << floatExponentBitCount) - 1U) << floatMantissaBitCount;
		public const uint floatSignExponentMask = floatSignMask | floatExponentMask;
		public const uint floatMantissaMask = (1U << floatMantissaBitCount) - 1U;
		public const uint floatSignExponent1 = 0x3F800000U;

		public static float BitsToFloatC1O2(uint bits)
		{
			BitwiseFloat value;
			value.number = 0f;
			value.bits = floatSignExponent1 | floatMantissaMask & bits;
			return value.number;
		}
	}
}
