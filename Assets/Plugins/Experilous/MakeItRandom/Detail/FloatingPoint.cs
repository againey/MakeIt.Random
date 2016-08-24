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
		public const uint floatTwo = 0x40000000U;

		public const int doubleExponentBitCount = 11;
		public const int doubleMantissaBitCount = 52;
		public const int doubleMantissaBitCountUpper = 20;
		public const ulong doubleSignMask = 1UL << (doubleExponentBitCount + doubleMantissaBitCount);
		public const ulong doubleExponentMask = ((1UL << doubleExponentBitCount) - 1UL) << doubleMantissaBitCount;
		public const ulong doubleSignExponentMask = doubleSignMask | doubleExponentMask;
		public const ulong doubleMantissaMask = (1UL << doubleMantissaBitCount) - 1UL;
		public const ulong doubleOne = 0x3FF0000000000000UL;
		public const ulong doubleTwo = 0x4000000000000000UL;

		public const uint doubleSignMaskUpper = (uint)(doubleSignMask >> 32);
		public const uint doubleExponentMaskUpper = (uint)(doubleExponentMask >> 32);
		public const uint doubleSignExponentMaskUpper = (uint)(doubleSignExponentMask >> 32);
		public const uint doubleMantissaMaskUpper = (uint)(doubleMantissaMask >> 32);
		public const uint doubleOneUpper = (uint)(doubleOne >> 32);

		public static readonly uint[] fastSqrtUpper = new uint[] { 0, 1, 1, 2, 2, 4, 5, 8, 11, 16, 22, 32, 45, 64, 90, 128, 181, 256, 362, 512, 724, 1024, 1448, 2048, 2896, 4096, 5792, 8192, 11585, 16384, 23170, 32768, 46340, 65536, 92681, 131072, 185363, 262144, 370727, 524288, 741455, 1048576, 1482910, 2097152, 2965820, 4194304, 5931641, 8388608, 11863283, 16777216, 23726566, 33554432, 47453132, 67108864, 94906265, 134217728, 189812531, 268435456, 379625062, 536870912, 759250124, 1073741824, 1518500249, 2147483648, 3037000499, };
		public static readonly uint[] fastSqrtLower = new uint[] { 2147483648, 2164195835, 2180779953, 2197238903, 2213575477, 2229792364, 2245892157, 2261877356, 2277750374, 2293513541, 2309169105, 2324719241, 2340166051, 2355511566, 2370757755, 2385906521, 2400959708, 2415919104, 2430786438, 2445563392, 2460251592, 2474852620, 2489368009, 2503799249, 2518147786, 2532415027, 2546602337, 2560711045, 2574742443, 2588697789, 2602578306, 2616385184, 2630119584, 2643782635, 2657375437, 2670899063, 2684354560, 2697742945, 2711065213, 2724322335, 2737515256, 2750644901, 2763712171, 2776717947, 2789663090, 2802548438, 2815374814, 2828143019, 2840853838, 2853508038, 2866106369, 2878649564, 2891138341, 2903573402, 2915955434, 2928285110, 2940563089, 2952790016, 2964966521, 2977093224, 2989170731, 3001199635, 3013180520, 3025113955, };

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

		public static float FixedToFloat(int n, uint fractionalBits)
		{
			if (n == 0) return 0f;
			BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= (fractionalBits << floatMantissaBitCount);
			return value.number;
		}

		public static float FixedToFloat(uint n, uint fractionalBits)
		{
			if (n == 0U) return 0f;
			BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= (fractionalBits << floatMantissaBitCount);
			return value.number;
		}

		public static float FixedToFloat(long n, uint fractionalBits)
		{
			if (n == 0L) return 0f;
			BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= (fractionalBits << floatMantissaBitCount);
			return value.number;
		}

		public static float FixedToFloat(ulong n, uint fractionalBits)
		{
			if (n == 0UL) return 0f;
			BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= (fractionalBits << floatMantissaBitCount);
			return value.number;
		}

		public static double FixedToDouble(int n, uint fractionalBits)
		{
			if (n == 0) return 0d;
			BitwiseDouble value;
#if OPTIMIZE_FOR_32
			value.lowerBits = 0U;
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= (fractionalBits << doubleMantissaBitCountUpper);
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= (fractionalBits << doubleMantissaBitCount);
#endif
			return value.number;
		}

		public static double FixedToDouble(uint n, uint fractionalBits)
		{
			if (n == 0U) return 0d;
			BitwiseDouble value;
#if OPTIMIZE_FOR_32
			value.lowerBits = 0U;
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= (fractionalBits << doubleMantissaBitCountUpper);
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= (fractionalBits << doubleMantissaBitCount);
#endif
			return value.number;
		}

		public static double FixedToDouble(long n, uint fractionalBits)
		{
			if (n == 0L) return 0d;
			BitwiseDouble value;
#if OPTIMIZE_FOR_32
			value.lowerBits = 0U;
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= (fractionalBits << doubleMantissaBitCountUpper);
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= (fractionalBits << doubleMantissaBitCount);
#endif
			return value.number;
		}

		public static double FixedToDouble(ulong n, uint fractionalBits)
		{
			if (n == 0UL) return 0d;
			BitwiseDouble value;
#if OPTIMIZE_FOR_32
			value.lowerBits = 0U;
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= (fractionalBits << doubleMantissaBitCountUpper);
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= (fractionalBits << doubleMantissaBitCount);
#endif
			return value.number;
		}

		#region Lookup Table Generation

#if UNITY_EDITOR
		//[UnityEditor.Callbacks.DidReloadScripts] // Uncomment this attribute in order to generate and print tables in the Unity console pane.
		private static void GenerateFastSquareRootLookupTables()
		{
			var sb = new System.Text.StringBuilder();
			sb.Append("\t\tpublic static readonly uint[] fastSqrtUpper = new uint[] { 0, ");
			for (int i = 0; i < 64; ++i)
			{
				sb.AppendFormat("{0}, ", (uint)System.Math.Floor(System.Math.Sqrt(System.Math.Pow(2d, i))));
			}
			sb.Append("};\n");
			sb.Append("\t\tpublic static readonly uint[] fastSqrtLower = new uint[] { ");
			for (int i = 0; i < 64; ++i)
			{
				sb.AppendFormat("{0}, ", (uint)System.Math.Floor(System.Math.Sqrt(1d + i / 64d) * (1L << 31)));
			}
			sb.Append("};\n");
			UnityEngine.Debug.Log(sb.ToString( ));
		}
#endif

		#endregion
	}
}
