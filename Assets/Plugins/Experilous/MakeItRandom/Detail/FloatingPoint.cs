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
	/// A static class of constants and methods for efficiently doing various floating point bit manipulation.
	/// </summary>
	public static class FloatingPoint
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct BitwiseFloat
		{
			[FieldOffset(0)]
			public uint bits;
			[FieldOffset(0)]
			public float number;
		}

#if OPTIMIZE_FOR_32
		[StructLayout(LayoutKind.Explicit)]
		public struct BitwiseDouble
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
		public struct BitwiseDouble
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
		public const uint floatOne = 0x3F800000U;

		public const int doubleExponentBitCount = 11;
		public const int doubleMantissaBitCount = 52;
		public const ulong doubleSignMask = 1UL << (doubleExponentBitCount + doubleMantissaBitCount);
		public const ulong doubleExponentMask = ((1UL << doubleExponentBitCount) - 1UL) << doubleMantissaBitCount;
		public const ulong doubleSignExponentMask = doubleSignMask | doubleExponentMask;
		public const ulong doubleMantissaMask = (1UL << doubleMantissaBitCount) - 1UL;
		public const ulong doubleOne = 0x3FF0000000000000UL;

		public const uint doubleSignMaskUpper = (uint)(doubleSignMask >> 32);
		public const uint doubleExponentMaskUpper = (uint)(doubleExponentMask >> 32);
		public const uint doubleSignExponentMaskUpper = (uint)(doubleSignExponentMask >> 32);
		public const uint doubleMantissaMaskUpper = (uint)(doubleMantissaMask >> 32);
		public const uint doubleOneUpper = (uint)(doubleOne >> 32);
/*
		public static float BitsToFloatC1O2(uint bits)
		{
			BitwiseFloat value;
			value.number = 0f;
			value.bits = floatOne | floatMantissaMask & bits;
			return value.number;
		}

		public static double BitsToDoubleC1O2(ulong bits)
		{
#if OPTIMIZE_FOR_32
			BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = (uint)bits;
			value.upperBits = doubleOneUpper | doubleMantissaMaskUpper & (uint)(bits >> 32);
			return value.number;
#else
			BitwiseDouble value;
			value.number = 0d;
			value.bits = doubleOne | doubleMantissaMask & bits;
			return value.number;
#endif
		}

		public static double BitsToDoubleC1O2(uint lower, uint upper)
		{
#if OPTIMIZE_FOR_32
			BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = doubleOneUpper | doubleMantissaMaskUpper & upper;
			return value.number;
#else
			BitwiseDouble value;
			value.number = 0d;
			value.bits = doubleOne | doubleMantissaMask & ((ulong)upper << 32) | lower;
			return value.number;
#endif
		}
*/
	}
}
