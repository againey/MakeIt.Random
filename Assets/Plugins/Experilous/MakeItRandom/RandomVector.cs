/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if (UNITY_64 || MAKEITRANDOM_64) && !MAKEITRANDOM_32
#define OPTIMIZE_FOR_64
#else
#define OPTIMIZE_FOR_32
#endif

using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random vectors of 2, 3, and 4 dimensions within various spatial distributions.
	/// </summary>
	public static class RandomVector
	{
		#region Unit Vector

		/// <summary>
		/// Generates a random 2-dimensional unit vector, selected from a uniform distribution of all points on the perimeter of a unit circle.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 2-dimensional unit vector.</returns>
		/// <remarks><note type="note">This function variant can be noticeably slower than <see cref="UnitVector2(IRandom, out Vector2)"/> in some environments.</note></remarks>
		/// <seealso cref="UnitVector2(IRandom, out Vector2)"/>
		public static Vector2 UnitVector2(this IRandom random)
		{
			Vector2 v;
			random.UnitVector2(out v);
			return v;
		}

		/// <summary>
		/// Generates a random 2-dimensional unit vector, selected from a uniform distribution of all points on the perimeter of a unit circle.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="v">The out parameter which will hold random 2-dimensional vector with a magnitude equal to 1 upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably faster than <see cref="UnitVector2(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="UnitVector2(IRandom)"/>
		public static void UnitVector2(this IRandom random, out Vector2 v)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Sqrt(random.ClosedFloatUnit());
			v = random.UnitVector2() * distance;
#else
#if OPTIMIZE_FOR_32
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (upper >= 0xFFFF8000U && random.RangeCO(0x0000400000000002UL) < 0x0000000000080000UL)
			{
				v = ((upper & 0x00004000U) == 0UL) ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				return;
			}
			upper = (lower >> 23) | (upper << 9);
			int ix = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int ixScaled = ix >> 8;
			int iyScaled = iy >> 8;
			int scaledRadiusSquared = ixScaled * ixScaled + iyScaled * iyScaled;
			if (scaledRadiusSquared > 0x10000000) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.

			long lx = ix;
			long ly = iy;
			long lxSqr = lx * lx;
			long lySqr = ly * ly;
			long lxySqr = lxSqr + lySqr;
			if (scaledRadiusSquared > 0x0FFF8000 && lxySqr > 0x0000100000000000L) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.

			// Formula is from http://mathworld.wolfram.com/CirclePointPicking.html
			lxySqr = lxySqr >> 4;
			upper = (uint)((int)(((lxSqr - lySqr) << 18) / lxySqr) + 0x00400000);
			lower = (uint)((int)(((lx * ly) << 19) / lxySqr) + 0x00400000);

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & upper;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & lower;
			v.y = value.number * 2f - 3f;
#else
			Start:
			ulong bits = random.Next64();
			if (bits >= 0xFFFF800000000000UL && random.RangeCO(0x0000400000000002UL) < 0x0000000000080000UL)
			{
				v = ((bits & 0x0000400000000000UL) == 0UL) ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				return;
			}
			uint upper = (uint)(bits >> 23);
			uint lower = (uint)bits;
			long ix = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long ixSqr = ix * ix;
			long iySqr = iy * iy;
			long ixySqr = ixSqr + iySqr;
			if (ixySqr > 0x0000100000000000L) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.
			
			// Formula is from http://mathworld.wolfram.com/CirclePointPicking.html
			ixySqr = ixySqr >> 4;
			upper = (uint)(((ixSqr - iySqr) << 18) / ixySqr + 0x00400000L);
			lower = (uint)(((ix * iy) << 19) / ixySqr + 0x00400000L);

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & upper;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & lower;
			v.y = value.number * 2f - 3f;
#endif
#endif
		}

		/// <summary>
		/// Generates a random 3-dimensional unit vector, selected from a uniform distribution of all points on the surface of a unit sphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 3-dimensional unit vector.</returns>
		public static Vector3 UnitVector3(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var longitude = random.HalfOpenRange(0f, Mathf.PI * 2f);
			var z = random.ClosedRange(-1f, +1f);
			var invertedZ = Mathf.Sqrt(1f - z * z);
			return new Vector3(invertedZ * Mathf.Cos(longitude), invertedZ * Mathf.Sin(longitude), z);
#else
#if truef
			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			float t = 2f * Mathf.Sqrt(1f - uvSqr);
			Vector3 vec;
			vec.x = u * t;
			vec.y = v * t;
			vec.z = 1f - 2f * uvSqr;

			float makeItDelta = vec.magnitude - 1f;
			_sumPosMakeItDelta += makeItDelta > 0f ? makeItDelta : 0d;
			_sumNegMakeItDelta += makeItDelta < 0f ? makeItDelta : 0d;
			_worstPosMakeItDelta = Mathf.Max(_worstPosMakeItDelta, makeItDelta);
			_worstNegMakeItDelta = Mathf.Min(_worstNegMakeItDelta, makeItDelta);
			float unityDelta = Random.onUnitSphere.magnitude - 1f;
			_sumPosUnityDelta += unityDelta > 0f ? unityDelta : 0d;
			_sumNegUnityDelta += unityDelta < 0f ? unityDelta : 0d;
			_worstPosUnityDelta = Mathf.Max(_worstPosUnityDelta, unityDelta);
			_worstNegUnityDelta = Mathf.Min(_worstNegUnityDelta, unityDelta);
			++_countUnit;
			if (_countUnit % 1024 == 0)
			Debug.LogFormat("{0:F8}, {1:F8}, {2:F8}, {3:F8}, {4:F8}, {5:F8}, {6:F8}, {7:F8}, {8:F8}, {9:F8}", vec.magnitude, makeItDelta, _worstPosMakeItDelta, _worstNegMakeItDelta, _worstPosUnityDelta, _worstNegUnityDelta, _sumPosMakeItDelta, _sumNegMakeItDelta, _sumPosUnityDelta, _sumNegUnityDelta);

			return vec;
#else
			Vector3 v;
			random.UnitVector3(out v);
			return v;
#endif
#endif
		}

#if UNITY_EDITOR
		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void Test()
		{
			var sb = new System.Text.StringBuilder();
			sb.Append("private static uint[] _fastSqrtUpper = new uint[] { 0, ");
			for (int i = 0; i < 64; ++i)
			{
				sb.AppendFormat("{0}, ", (uint)System.Math.Floor(System.Math.Sqrt(System.Math.Pow(2d, i))));
			}
			sb.Append("};\n");
			sb.Append("private static uint[] _fastSqrtMid = new uint[] { ");
			for (int i = 0; i < 64; ++i)
			{
				sb.AppendFormat("{0}, ", (uint)System.Math.Floor(System.Math.Sqrt(1d + i / 64d) * (1L << 31)));
			}
			sb.Append("};\n");
			sb.Append("private static uint[] _fastSqrtMidRecip = new uint[] { ");
			for (int i = 0; i < 64; ++i)
			{
				sb.AppendFormat("{0}, ", (uint)System.Math.Floor(1d / (1d + i / 64d) * (1L << 31)));
			}
			sb.Append("};\n");
			sb.Append("private static uint[] _fastSqrtLower = new uint[] { ");
			for (int i = 0; i < 64; ++i)
			{
				sb.AppendFormat("{0}, ", (uint)System.Math.Floor(System.Math.Sqrt(1d + i / 4096d) * (1L << 31)));
			}
			sb.Append("};\n");
			Debug.Log(sb.ToString( ));

			var r = XorShift128Plus.Create(4647564);
			Vector3 v;
			r.UnitVector3(out v);
		}
#endif

private static uint[] _fastSqrtUpper = new uint[] { 0, 1, 1, 2, 2, 4, 5, 8, 11, 16, 22, 32, 45, 64, 90, 128, 181, 256, 362, 512, 724, 1024, 1448, 2048, 2896, 4096, 5792, 8192, 11585, 16384, 23170, 32768, 46340, 65536, 92681, 131072, 185363, 262144, 370727, 524288, 741455, 1048576, 1482910, 2097152, 2965820, 4194304, 5931641, 8388608, 11863283, 16777216, 23726566, 33554432, 47453132, 67108864, 94906265, 134217728, 189812531, 268435456, 379625062, 536870912, 759250124, 1073741824, 1518500249, 2147483648, 3037000499, };
private static uint[] _fastSqrtMid = new uint[] { 2147483648, 2164195835, 2180779953, 2197238903, 2213575477, 2229792364, 2245892157, 2261877356, 2277750374, 2293513541, 2309169105, 2324719241, 2340166051, 2355511566, 2370757755, 2385906521, 2400959708, 2415919104, 2430786438, 2445563392, 2460251592, 2474852620, 2489368009, 2503799249, 2518147786, 2532415027, 2546602337, 2560711045, 2574742443, 2588697789, 2602578306, 2616385184, 2630119584, 2643782635, 2657375437, 2670899063, 2684354560, 2697742945, 2711065213, 2724322335, 2737515256, 2750644901, 2763712171, 2776717947, 2789663090, 2802548438, 2815374814, 2828143019, 2840853838, 2853508038, 2866106369, 2878649564, 2891138341, 2903573402, 2915955434, 2928285110, 2940563089, 2952790016, 2964966521, 2977093224, 2989170731, 3001199635, 3013180520, 3025113955, };
private static uint[] _fastSqrtMidRecip = new uint[] { 2147483648, 2114445438, 2082408385, 2051327663, 2021161080, 1991868890, 1963413621, 1935759908, 1908874353, 1882725390, 1857283155, 1832519379, 1808407282, 1784921473, 1762037865, 1739733588, 1717986918, 1696777203, 1676084798, 1655891005, 1636178017, 1616928864, 1598127365, 1579758085, 1561806289, 1544257904, 1527099483, 1510318170, 1493901668, 1477838209, 1462116526, 1446725826, 1431655765, 1416896427, 1402438300, 1388272257, 1374389534, 1360781717, 1347440720, 1334358771, 1321528398, 1308942414, 1296593900, 1284476200, 1272582902, 1260907830, 1249445031, 1238188770, 1227133513, 1216273924, 1205604855, 1195121334, 1184818564, 1174691910, 1164736893, 1154949188, 1145324612, 1135859119, 1126548798, 1117389865, 1108378657, 1099511627, 1090785345, 1082196484, };
private static uint[] _fastSqrtLower = new uint[] { 2147483648, 2147745776, 2148007872, 2148269936, 2148531968, 2148793968, 2149055936, 2149317872, 2149579776, 2149841649, 2150103489, 2150365298, 2150627075, 2150888820, 2151150533, 2151412214, 2151673863, 2151935481, 2152197067, 2152458621, 2152720143, 2152981634, 2153243092, 2153504519, 2153765914, 2154027278, 2154288610, 2154549910, 2154811178, 2155072415, 2155333620, 2155594793, 2155855935, 2156117045, 2156378124, 2156639171, 2156900186, 2157161170, 2157422122, 2157683043, 2157943932, 2158204789, 2158465615, 2158726410, 2158987173, 2159247904, 2159508604, 2159769273, 2160029910, 2160290516, 2160551090, 2160811633, 2161072144, 2161332624, 2161593073, 2161853490, 2162113876, 2162374230, 2162634553, 2162894845, 2163155106, 2163415335, 2163675533, 2163935699, };

		public static void UnitVector3(this IRandom random, out Vector3 v)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Pow(random.ClosedFloatUnit(), 1f / 3f);
			return random.UnitVector3() * distance;
#else
#if OPTIMIZE_FOR_32
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (upper >= 0xFFFF8000U && random.RangeCO(0x0000400000000002UL) < 0x0000000000080000UL)
			{
				v = ((upper & 0x00004000U) == 0UL) ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				return;
			}
			upper = (lower >> 23) | (upper << 9);
			int ix = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int ixScaled = ix >> 8;
			int iyScaled = iy >> 8;
			int scaledRadiusSquared = ixScaled * ixScaled + iyScaled * iyScaled;
			if (scaledRadiusSquared > 0x10000000 || scaledRadiusSquared > 0x0FFF8000 && (long)ix * ix + (long)iy * iy > 0x0000100000000000L)
			{
				goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.
			}

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & upper;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & lower;
			v.y = value.number * 2f - 3f;
#elif true
			// Method by Marsaglia
			//   http://projecteuclid.org/download/pdf_1/euclid.aoms/1177692644

			Start:
			ulong bits = random.Next64();
			uint upper = (uint)(bits >> 26);
			uint lower = (uint)bits;
			// 1/2^6 * 1/2^6 * 2^12/(2^52+1) = 1/(2^52+1)
			if (upper >= 0xFC000000U && lower >= 0xFC000000U && random.RangeCO(0x0010000000000001UL) < 0x0000000000001000UL)
			{
				v.x = 0f;
				v.y = 0f;
				v.z = -1f;
				return;
			}
			long ix = (upper & 0x03FFFFFFU) - (1L << 25); // x*2^25
			long iy = (lower & 0x03FFFFFFU) - (1L << 25); // y*2^25
			//ix = 0x40000000L;
			//iy = 0x18000000L;
			ulong ixSqr = (ulong)(ix * ix); // x*x*2^50
			ulong iySqr = (ulong)(iy * iy); // y*y*2^50
			ulong ixySqr = ixSqr + iySqr; // (x*x + y*y) * 2^50
			if (ixySqr >= (1UL << 50)) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.

			//float t = 2f * Mathf.Sqrt(1f - uvSqr);
			//return new Vector3(u * t, v * t, 1f - 2f * uvSqr);
			//t = 2 * sqrt(1 - sqr)
			//x = u * t;
			//y = v * t;
			//z = 1 - 2 * sqr = 2(1/2 - sqr)

#if true //Integer Square Root Method
#if truef //Loopy Version
			ulong ixySqrInv = (1UL << 50) - ixySqr; // 2^50 - x*x*2^50 + y*y*2^50
			//ixySqrInv = ixySqrInv << 0;
			ulong ixyInv = 0;
			ulong rem = ((ixySqrInv & 0xC000000000000000UL) >> 62);
			for (int i = 0; i < 32; ++i)
			{
				ulong cmp = ixyInv << 1;
				if ((ixyInv != 0 || rem != 0) && cmp < rem)
				{
					ixyInv |= 1;
					cmp |= 1;
					rem = rem - cmp;
				}
				ixySqrInv = ixySqrInv << 2;
				rem = (rem << 2) | ((ixySqrInv & 0xC000000000000000UL) >> 62);
				ixyInv = ixyInv << 1;
			}
			// ixyInv = 2 * sqrt(2^62 - x*x*2^62 - y*y*2^62) = 2 * sqrt(2^62 * (1 - x*x - y*y)) = 2 * 2^31 * sqrt(1 - x*x - y*y)
			//ixyInv = (ixyInv + (1UL << 5)) >> 6;

			long llx = ix * (long)ixyInv; // x*2^25 * 2 * 2^25 * sqrt(1 - x*x - y*y) = 2^51 * x * sqrt(1 - x*x - y*y)
			long lly = iy * (long)ixyInv; // y*2^25 * 2 * 2^25 * sqrt(1 - x*x - y*y) = 2^51 * y * sqrt(1 - x*x - y*y)
			long llz = (1L << 50) - ((long)ixySqr << 1); // 2 * (0.5 - x*x - y*y) * 2^50 = 2^51 * (0.5 - x*x - y*y) = 2^50 - (x*x + y*y) * 2^51
#elif truef //Other Loopy Version
			ulong ixySqrInv = (1UL << 50) - ixySqr; // 2^50 - x*x*2^50 + y*y*2^50
			//ixySqrInv = ixySqrInv << 12;
			ulong one = 1UL << 62;
			ulong ixyInv = 0UL;
			while (one > ixySqrInv)
			{
				one = one >> 2;
			}
			while (one != 0UL)
			{
				ixyInv = ixyInv >> 1;
				ulong sqrtPlusOne = ixyInv + one;
				if (ixySqrInv >= sqrtPlusOne)
				{
					ixySqrInv -= sqrtPlusOne;
					ixyInv += one << 1;
				}
				one = one >> 2;
			}

			long llx = ix * (long)ixyInv; // x*2^25 * 2 * 2^25 * sqrt(1 - x*x - y*y) = 2^51 * x * sqrt(1 - x*x - y*y)
			long lly = iy * (long)ixyInv; // y*2^25 * 2 * 2^25 * sqrt(1 - x*x - y*y) = 2^51 * y * sqrt(1 - x*x - y*y)
			long llz = (1L << 50) - ((long)ixySqr << 1); // 2 * (0.5 - x*x - y*y) * 2^50 = 2^51 * (0.5 - x*x - y*y) = 2^50 - (x*x + y*y) * 2^51
#else //Lookup Table
			ulong ixySqrInv = (1UL << 50) - ixySqr; // 2^50 - x*x*2^50 + y*y*2^50
			ixySqrInv = ixySqrInv << 12;
			ulong mask = ixySqrInv | (ixySqrInv >> 1);
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = Detail.DeBruijnLookup.bitCountTable64[mask * Detail.DeBruijnLookup.multiplier64 >> Detail.DeBruijnLookup.shift64];
			int b = (int)((bitCount >= 7 ? (ixySqrInv >> (bitCount - 7)) : (ixySqrInv << (7 - bitCount))) & 0x3FU);
			//long c = (long)((bitCount >= 13 ? (ixySqrInv >> (bitCount - 13)) : (ixySqrInv << (13 - bitCount))) & 0x3FUL);
			ulong sqrtA = _fastSqrtUpper[bitCount];
			ulong sqrtB = _fastSqrtMid[b];
			//long sqrtC = 0L;
			ulong ixyInv = (sqrtA * sqrtB) >> 31;
			//ixyInv = ((long)ixySqrInv / ixyInv + ixyInv) >> 1;
			//ixyInv = ((long)ixySqrInv / ixyInv + ixyInv) >> 1;
			ixyInv = (ixySqrInv / ixyInv + ixyInv) >> 1;
			ixyInv = (ixySqrInv / ixyInv + ixyInv);
			//long bRecip = _fastSqrtMidRecip[b];
			//long cOverB = (c * bRecip) >> 31;
			//long sqrtCOverBPlusOne = _fastSqrtLower[cOverB];
			//long ixyInv = (((sqrtA * sqrtB) >> 31) * sqrtCOverBPlusOne) >> 18;
			//Debug.LogFormat("{0}, 0x{1:X2}, 0x{2:X2}", bitCount, b, c);
			//Debug.LogFormat("0x{0:X16}, 0x{1:X16}, 0x{2:X16}, 0x{3:X16}, 0x{4:X2}, 0x{5:X16}, 0x{6:X16}", ixySqrInv, sqrtA, sqrtB, bRecip, cOverB, sqrtCOverBPlusOne, ixyInv);
			//Debug.LogFormat("{0:F16}, {1:F16}, {2:F16}, {3:F16}, {4:F16}, {5:F16}, {6:F16}", ixySqrInv / (double)(1UL << 50), sqrtA / (double)(1UL << 25), sqrtB / (double)(1UL << 31), bRecip / (double)(1UL << 31), cOverB / (double)(1UL << 31), sqrtCOverBPlusOne / (double)(1UL << 31), ixyInv / (double)(1UL << 38));
			//ixyInv = (long)(System.Math.Sqrt(ixySqrInv / (double)(1UL << 50)) * (1L << 38));

			long llx = ix * (long)ixyInv; // x*2^25 * 2 * 2^31 * sqrt(1 - x*x - y*y) = 2^57 * x * sqrt(1 - x*x - y*y)
			long lly = iy * (long)ixyInv; // y*2^25 * 2 * 2^31 * sqrt(1 - x*x - y*y) = 2^57 * y * sqrt(1 - x*x - y*y)
			long llz = (1L << 50) - ((long)ixySqr << 1); // 2 * (0.5 - x*x - y*y) * 2^50 = 2^51 * (0.5 - x*x - y*y) = 2^50 - (x*x + y*y) * 2^51
#endif

			//Debug.LogFormat("0x{0:X16}, 0x{1:X16}, 0x{2:X16}, 0x{3:X16}", ixyInv, llx, lly, llz);
#if truef // meticulous conversion
			ulong ulx = (ulong)System.Math.Abs(llx);
			ulong uly = (ulong)System.Math.Abs(lly);
			ulong ulz = (ulong)System.Math.Abs(llz);

			//Debug.LogFormat("0x{0:X16}, 0x{1:X16}, 0x{2:X16}", ulx, uly, ulz);

			int bx = Detail.DeBruijnLookup.GetBitCountForRangeSize(ulx);
			int by = Detail.DeBruijnLookup.GetBitCountForRangeSize(uly);
			int bz = Detail.DeBruijnLookup.GetBitCountForRangeSize(ulz);

			//Debug.LogFormat("{0}, {1}, {2}", bx, by, bz);

			uint ux = (uint)((ulx + (1UL << (bx - 24))) >> (bx - 24));
			uint uy = (uint)((uly + (1UL << (bx - 24))) >> (by - 24));
			uint uz = (uint)((ulz + (1UL << (bx - 24))) >> (bz - 24));

			//Debug.LogFormat("0x{0:X8}, 0x{1:X8}, 0x{2:X8}", ux, uy, uz);

			uint ex = 0x3F800000U - ((51U - (uint)bx) << 23);
			uint ey = 0x3F800000U - ((51U - (uint)by) << 23);
			uint ez = 0x3F800000U - ((51U - (uint)bz) << 23);

			//Debug.LogFormat("0x{0:X8}, 0x{1:X8}, 0x{2:X8}", ex, ey, ez);

			ux = (ux & 0x007FFFFF) | ex | (uint)((ulong)llx >> 32) & 0x80000000U;
			uy = (uy & 0x007FFFFF) | ey | (uint)((ulong)lly >> 32) & 0x80000000U;
			uz = (uz & 0x007FFFFF) | ez | (uint)((ulong)llz >> 32) & 0x80000000U;

			//Debug.LogFormat("0x{0:X8}, 0x{1:X8}, 0x{2:X8}", ux, uy, uz);

			Detail.FloatingPoint.BitwiseFloat t;
			t.number = 0f;
			t.bits = ux; v.x = t.number;
			t.bits = uy; v.y = t.number;
			t.bits = uz; v.z = t.number;
			//Debug.LogFormat("< {0:F8}, {1:F8}, {2:F3} >, {3:F8}", v.x, v.y, v.z, v.magnitude);


			//uint ux = (uint)(llx + (1L << 62));

			//uint ux = (uint)(((ix * (long)ixyInv + (1L << 39)) >> 40) + 0x00400000L);
			//uint uy = (uint)(((iy * (long)ixyInv + (1L << 39)) >> 40) + 0x00400000L);
			//uint uz = (uint)((((1L << 61) - (long)ixySqr + (1L << 38)) >> 39) + 0x00400000L);

			//Detail.FloatingPoint.BitwiseFloat value;
			//value.number = 0f;
			//value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & ux;
			//v.x = value.number * 2f - 3f;
			//value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & uy;
			//v.y = value.number * 2f - 3f;
			//value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & uz;
			//v.z = value.number * 2f - 3f;
#else
			Detail.FloatingPoint.BitwiseFloat conv;
			conv.bits = 0U;
			conv.number = llx;
			conv.bits -= 0x1C000000U; // exponent -= 56
			//conv.bits -= (56U << 23);
			v.x = conv.number;
			conv.number = lly;
			conv.bits -= 0x1C000000U; // exponent -= 56
			//conv.bits -= (56U << 23);
			v.y = conv.number;
			conv.number = llz;
			conv.bits -= 0x19000000U; // exponent -= 50
			//conv.bits -= (50U << 23);
			v.z = conv.number;
			//Debug.LogFormat("< {0:F8}, {1:F8}, {2:F3} >, {3:F8}", v.x, v.y, v.z, v.magnitude);
#endif

#elif true //Float Square Root Method
			ulong ixySqrInvT4 = (1UL << 52) - (ixySqr << 2); // (1 - x*x - y*y) * 2^50 * 4 = (1 - x*x - y*y) * 2^52
			long llz = (1L << 50) - ((long)ixySqr << 1); // 2 * (0.5 - x*x - y*y) * 2^50 = 2^51 * (0.5 - x*x - y*y) = 2^50 - (x*x + y*y) * 2^51

			Detail.FloatingPoint.BitwiseFloat conv;
			conv.bits = 0U;
			conv.number = ixySqrInvT4;
			conv.bits -= 0x19000000U; // exponent -= 50
			float fxyInvT2 = Mathf.Sqrt(conv.number);
			conv.number = ix;
			conv.bits -= 0x0C800000U; // exponent -= 25
			v.x = conv.number * fxyInvT2;
			conv.number = iy;
			conv.bits -= 0x0C800000U; // exponent -= 25
			v.y = conv.number * fxyInvT2;
			conv.number = llz;
			conv.bits -= 0x19000000U; // exponent -= 50
			v.z = conv.number;
#endif


#if truef //Measure Error
			float makeItDelta = v.magnitude - 1f;
			_sumPosMakeItDelta += makeItDelta > 0f ? makeItDelta : 0d;
			_sumNegMakeItDelta += makeItDelta < 0f ? makeItDelta : 0d;
			_worstPosMakeItDelta = Mathf.Max(_worstPosMakeItDelta, makeItDelta);
			_worstNegMakeItDelta = Mathf.Min(_worstNegMakeItDelta, makeItDelta);
			float unityDelta = Random.onUnitSphere.magnitude - 1f;
			_sumPosUnityDelta += unityDelta > 0f ? unityDelta : 0d;
			_sumNegUnityDelta += unityDelta < 0f ? unityDelta : 0d;
			_worstPosUnityDelta = Mathf.Max(_worstPosUnityDelta, unityDelta);
			_worstNegUnityDelta = Mathf.Min(_worstNegUnityDelta, unityDelta);
			++_countUnit;
			if (_countUnit % 1024 == 0)
			Debug.LogFormat("{0:F8}, {1:F8}, {2:F8}, {3:F8}, {4:F8}, {5:F8}, {6:F8}, {7:F8}, {8:F8}, {9:F8}", v.magnitude, makeItDelta, _worstPosMakeItDelta, _worstNegMakeItDelta, _worstPosUnityDelta, _worstNegUnityDelta, _sumPosMakeItDelta, _sumNegMakeItDelta, _sumPosUnityDelta, _sumNegUnityDelta);
#endif
#else //Cook method
			Start:
			ulong bits0 = random.Next64();
			ulong bits1 = random.Next64();
			if (bits0 >= 0xFFFF800000000000UL && random.RangeCO(0x0000400000000002UL) < 0x0000000000080000UL)
			{
				v = ((bits0 & 0x0000400000000000UL) == 0UL) ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				return;
			}
			uint upper0 = (uint)(bits0 >> 23);
			uint lower0 = (uint)bits0;
			uint upper1 = (uint)(bits1 >> 23);
			uint lower1 = (uint)bits1;
			long t0 = (int)(upper0 & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long t1 = (int)(lower0 & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long t2 = (int)(upper1 & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long t3 = (int)(lower1 & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			ulong t0Sqr = (ulong)(t0 * t0);
			ulong t1Sqr = (ulong)(t1 * t1);
			ulong t2Sqr = (ulong)(t2 * t2);
			ulong t3Sqr = (ulong)(t3 * t3);
			ulong t03Sqr = t0Sqr + t3Sqr;
			ulong t12Sqr = t1Sqr + t2Sqr;
			ulong tSqr = t03Sqr + t12Sqr;
			if (tSqr > 0x0000100000000000UL) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.

			long lxn = (t1 * t3 + t0 * t2) << 1;
			long lyn = (t2 * t3 - t0 * t1) << 1;
			long lzn = (long)t03Sqr - (long)t12Sqr;

			long mul = (long)(0x4000000000000000UL / tSqr);
			uint ux = (uint)(((lxn * mul) >> 40) + 0x00400000L);
			uint uy = (uint)(((lyn * mul) >> 40) + 0x00400000L);
			uint uz = (uint)(((lzn * mul) >> 40) + 0x00400000L);

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne + ux;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne + uy;
			v.y = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne + uz;
			v.z = value.number * 2f - 3f;

			/*value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & upper0;
			float ft0 = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & lower0;
			float ft1 = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & upper1;
			float ft2 = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & lower1;
			float ft3 = value.number * 2f - 3f;

			float fxn = (ft1 * ft3 + ft0 * ft2) * 2f;
			float fyn = (ft2 * ft3 - ft0 * ft1) * 2f;
			float fzn = (ft0 * ft0 + ft3 * ft3) - (ft1 * ft1 + ft2 * ft2);
			float fdv = (ft0 * ft0 + ft3 * ft3 + ft1 * ft1 + ft2 * ft2);

			Vector3 v2 = new Vector3(fxn / fdv, fyn / fdv, fzn / fdv);
			v = v2;

			float makeItDelta = v.magnitude - 1f;
			_sumPosMakeItDelta += makeItDelta > 0f ? makeItDelta : 0d;
			_sumNegMakeItDelta += makeItDelta < 0f ? makeItDelta : 0d;
			_worstPosMakeItDelta = Mathf.Max(_worstPosMakeItDelta, makeItDelta);
			_worstNegMakeItDelta = Mathf.Min(_worstNegMakeItDelta, makeItDelta);
			float unityDelta = Random.onUnitSphere.magnitude - 1f;
			_sumPosUnityDelta += unityDelta > 0f ? unityDelta : 0d;
			_sumNegUnityDelta += unityDelta < 0f ? unityDelta : 0d;
			_worstPosUnityDelta = Mathf.Max(_worstPosUnityDelta, unityDelta);
			_worstNegUnityDelta = Mathf.Min(_worstNegUnityDelta, unityDelta);
			++_countUnit;
			if (_countUnit % 1024 == 0)
			Debug.LogFormat("{0:F8}, {1:F8}, {2:F8}, {3:F8}, {4:F8}, {5:F8}, {6:F8}, {7:F8}, {8:F8}, {9:F8}", v.magnitude, makeItDelta, _worstPosMakeItDelta, _worstNegMakeItDelta, _worstPosUnityDelta, _worstNegUnityDelta, _sumPosMakeItDelta, _sumNegMakeItDelta, _sumPosUnityDelta, _sumNegUnityDelta);*/
#endif
#endif
		}

		private static float ToFloatNegOnePosOne(uint bits)
		{
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & bits;
			return value.number * 2f - 3f;
		}

		static float _worstPosMakeItDelta = 0f;
		static float _worstNegMakeItDelta = 0f;
		static float _worstPosUnityDelta = 0f;
		static float _worstNegUnityDelta = 0f;
		static double _sumPosMakeItDelta = 0d;
		static double _sumNegMakeItDelta = 0d;
		static double _sumPosUnityDelta = 0d;
		static double _sumNegUnityDelta = 0d;
		static int _countUnit = 0;

		/// <summary>
		/// Generates a random 4-dimensional unit vector, selected from a uniform distribution of all points on the surface of a unit hypersphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 4-dimensional unit vector.</returns>
		public static Vector4 UnitVector4(this IRandom random)
		{
			Start1:
			float u1 = random.FloatOO() * 2f - 1f;
			float v1 = random.FloatOO() * 2f - 1f;
			float uSqr1 = u1 * u1;
			float vSqr1 = v1 * v1;
			float uvSqr1 = uSqr1 + vSqr1;
			if (uvSqr1 >= 1f) goto Start1;

			Start2:
			float u2 = random.FloatOO() * 2f - 1f;
			float v2 = random.FloatOO() * 2f - 1f;
			float uSqr2 = u2 * u2;
			float vSqr2 = v2 * v2;
			float uvSqr2 = uSqr2 + vSqr2;
			if (uvSqr2 >= 1f) goto Start2;

			float t = Mathf.Sqrt((1f - uvSqr1) / uvSqr2);
			return new Vector4(u1, v1, u2 * t, v2 * t);
		}

		#endregion

		#region Scaled Vector

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points on the perimeter of a circle with the specified <paramref name="radius"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="radius">The radius of the circle from whose perimeter the random vector will be selected.  The vector's magnitude will be equal to this value.</param>
		/// <returns>A random 2-dimensional vector with a magnitude specified by <paramref name="radius"/>.</returns>
		public static Vector2 ScaledVector2(this IRandom random, float radius)
		{
			return UnitVector2(random) * radius;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points on the surface of a sphere with the specified <paramref name="radius"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="radius">The radius of the sphere from whose surface the random vector will be selected.  The vector's magnitude will be equal to this value.</param>
		/// <returns>A random 3-dimensional vector with a magnitude specified by <paramref name="radius"/>.</returns>
		public static Vector3 ScaledVector3(this IRandom random, float radius)
		{
			return UnitVector3(random) * radius;
		}

		/// <summary>
		/// Generates a random 4-dimensional vector selected from a uniform distribution of all points on the surface of a hypersphere with the specified <paramref name="radius"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="radius">The radius of the hypersphere from whose surface the random vector will be selected.  The vector's magnitude will be equal to this value.</param>
		/// <returns>A random 4-dimensional vector with a magnitude specified by <paramref name="radius"/>.</returns>
		public static Vector4 ScaledVector4(this IRandom random, float radius)
		{
			return UnitVector4(random) * radius;
		}

		#endregion

		#region Radial

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a unit circle.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 2-dimensional vector with a magnitude less than or equal to 1.</returns>
		/// <remarks><note type="note">This function variant can be noticeably slower than <see cref="PointWithinCircle(IRandom, out Vector2)"/> in some environments.</note></remarks>
		/// <seealso cref="PointWithinCircle(IRandom, out Vector2)"/>
		public static Vector2 PointWithinCircle(this IRandom random)
		{
			Vector2 v;
			random.PointWithinCircle(out v);
			return v;
		}

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a unit circle.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="v">The out parameter which will hold random 2-dimensional vector with a magnitude less than or equal to 1 upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably faster than <see cref="PointWithinCircle(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="PointWithinCircle(IRandom)"/>
		public static void PointWithinCircle(this IRandom random, out Vector2 v)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Sqrt(random.ClosedFloatUnit());
			v = random.UnitVector2() * distance;
#else
#if OPTIMIZE_FOR_32
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (upper >= 0xFFFF8000U && random.RangeCO(0x0000400000000002UL) < 0x0000000000080000UL)
			{
				v = ((upper & 0x00004000U) == 0UL) ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				return;
			}
			upper = (lower >> 23) | (upper << 9);
			int ix = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int ixScaled = ix >> 8;
			int iyScaled = iy >> 8;
			int scaledRadiusSquared = ixScaled * ixScaled + iyScaled * iyScaled;
			if (scaledRadiusSquared > 0x10000000 || scaledRadiusSquared > 0x0FFF8000 && (long)ix * ix + (long)iy * iy > 0x0000100000000000L)
			{
				goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.
			}

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & upper;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & lower;
			v.y = value.number * 2f - 3f;
#else
			Start:
			ulong bits = random.Next64();
			if (bits >= 0xFFFF800000000000UL && random.RangeCO(0x0000400000000002UL) < 0x0000000000080000UL)
			{
				v = ((bits & 0x0000400000000000UL) == 0UL) ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				return;
			}
			uint upper = (uint)(bits >> 23);
			uint lower = (uint)bits;
			long ix = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			if (ix * ix + iy * iy > 0x0000100000000000L) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & upper;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & lower;
			v.y = value.number * 2f - 3f;

			/*_maxMine = Mathf.Max(_maxMine, v.magnitude);
			_sumMine += 1d - v.magnitude;
			float vUm = Random.insideUnitCircle.magnitude;
			_maxUnity = Mathf.Max(_maxUnity, vUm);
			_sumUnity += 1d - vUm;
			++_count;
			if (_count % 1024 == 0)
			Debug.LogFormat("{0:F8}, {1:F16}, {2:F8}, {3:F16}", _maxMine, _sumMine / _count, _maxUnity, _sumUnity / _count);*/
#endif
#endif
		}

		static float _maxMine = 0f;
		static double _sumMine = 0d;
		static float _maxUnity = 0f;
		static double _sumUnity = 0d;
		static int _count;

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a circle with the specified <paramref name="radius"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="radius">The radius of the circle from whose area the random vector will be selected.  The vector's magnitude will be less than or equal to this value.</param>
		/// <returns>A random 2-dimensional vector with a magnitude less than or equal to <paramref name="radius"/>.</returns>
		public static Vector2 PointWithinCircle(this IRandom random, float radius)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Sqrt(random.ClosedFloatUnit()) * radius;
			return random.UnitVector2() * distance;
#else
			float rSqr = radius * radius;

			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > rSqr) goto Start;

			return new Vector2(u, v);
#endif
		}

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within the area of a larger circle with the specified <paramref name="outerRadius"/> minus a smaller circle with the specified <paramref name="innerRadius"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="innerRadius">The radius of the smaller circle from whose area the random vector will not be selected.  The vector's magnitude will be greater than or equal to this value.</param>
		/// <param name="outerRadius">The radius of the larger circle from whose area the random vector will be selected.  The vector's magnitude will be less than or equal to this value.</param>
		/// <returns>A random 2-dimensional vector with a magnitude greater than or equal to <paramref name="innerRadius"/> and less than or equal to <paramref name="outerRadius"/>.</returns>
		public static Vector2 PointWithinCircularShell(this IRandom random, float innerRadius, float outerRadius)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var unitMin = innerRadius / outerRadius;
			var unitMinSquared = unitMin * unitMin;
			var unitRange = 1f - unitMinSquared;
			var distance = Mathf.Sqrt(random.ClosedFloatUnit() * unitRange + unitMinSquared) * outerRadius;
			return random.UnitVector2() * distance;
#else
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr < irSqr || uvSqr > orSqr) goto Start;

			return new Vector2(u, v);
#endif
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a unit sphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 3-dimensional vector with a magnitude less than or equal to 1.</returns>
		public static Vector3 PointWithinSphere(this IRandom random)
		{
			Vector3 v;
			random.PointWithinSphere(out v);
			return v;
		}

		public static void PointWithinSphere(this IRandom random, out Vector3 v)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Pow(random.ClosedFloatUnit(), 1f / 3f);
			return random.UnitVector3() * distance;
#else
#if OPTIMIZE_FOR_32
			Start:
			uint ux, uy, uz, uw;
			random.Next64(out ux, out uy);
			random.Next64(out uz, out uw);
			if (false) //TODO: I have more bits to work with, and this isn't exactly like the 2D version anyway.
			{
				v = Vector3.zero;
				return;
			}
			int ix = (int)(ux & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int iy = (int)(uy & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int iz = (int)(uz & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int ixScaled = ix >> 8;
			int iyScaled = iy >> 8;
			int izScaled = iz >> 8;
			int scaledRadiusSquared = ixScaled * ixScaled + iyScaled * iyScaled + izScaled * izScaled;
			if (scaledRadiusSquared > 0x10000000 || scaledRadiusSquared > 0x0FFF8000 && (long)ix * ix + (long)iy * iy  + (long)iz * iz > 0x0000100000000000L)
			{
				goto Start; // x^2 + y^2 + z^2 > r^2, so generated point is outside the sphere.
			}

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & ux;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & uy;
			v.y = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & uz;
			v.y = value.number * 2f - 3f;
#else
			Start:
			ulong bits0 = random.Next64();
			ulong bits1 = random.Next64();
			if (false) //TODO: I have more bits to work with, and this isn't exactly like the 2D version anyway.
			{
				//v = ((bits0 & 0x0000400000000000UL) == 0UL) ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				v = Vector3.zero;
				return;
			}
			uint ux = (uint)bits0;
			uint uy = (uint)(bits0 >> 32);
			uint uz = (uint)bits1;
			long ix = (int)(ux & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iy = (int)(uy & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iz = (int)(uz & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			if (ix * ix + iy * iy + iz * iz > 0x0000100000000000L) goto Start; // x^2 + y^2 + z^2 > r^2, so generated point is outside the sphere.

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & ux;
			v.x = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & uy;
			v.y = value.number * 2f - 3f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & uz;
			v.z = value.number * 2f - 3f;
#endif
#endif
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a sphere with the specified <paramref name="radius"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="radius">The radius of the sphere from whose area the random vector will be selected.  The vector's magnitude will be less than or equal to this value.</param>
		/// <returns>A random 3-dimensional vector with a magnitude less than or equal to <paramref name="radius"/>.</returns>
		public static Vector3 PointWithinSphere(this IRandom random, float radius)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Pow(random.ClosedFloatUnit(), 1f / 3f) * radius;
			return random.UnitVector3() * distance;
#else
			float rSqr = radius * radius;

			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float w = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > rSqr) goto Start;

			return new Vector3(u, v, w);
#endif
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within the area of a larger sphere with the specified <paramref name="outerRadius"/> minus a smaller sphere with the specified <paramref name="innerRadius"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="innerRadius">The radius of the smaller sphere from whose area the random vector will not be selected.  The vector's magnitude will be greater than or equal to this value.</param>
		/// <param name="outerRadius">The radius of the larger sphere from whose area the random vector will be selected.  The vector's magnitude will be less than or equal to this value.</param>
		/// <returns>A random 3-dimensional vector with a magnitude greater than or equal to <paramref name="innerRadius"/> and less than or equal to <paramref name="outerRadius"/>.</returns>
		public static Vector3 PointWithinSphericalShell(this IRandom random, float innerRadius, float outerRadius)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(random.ClosedFloatUnit() * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return random.UnitVector3() * distance;
#else
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float w = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr < irSqr || uvwSqr > orSqr) goto Start;

			return new Vector3(u, v, w);
#endif
		}

		#endregion

		#region Axial

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a unit square from (0, 0) to (1, 1).
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 2-dimensional vector from within a unit square.</returns>
		public static Vector2 PointWithinSquare(this IRandom random)
		{
			return new Vector2(random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a square from (0, 0) to (<paramref name="sideLength"/>, <paramref name="sideLength"/>).
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="sideLength">The length of the square from within which the vector will be selected.</param>
		/// <returns>A random 2-dimensional vector from within a square.</returns>
		public static Vector2 PointWithinSquare(this IRandom random, float sideLength)
		{
			return new Vector2(random.RangeCC(sideLength), random.RangeCC(sideLength));
		}

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a rectangle from (0, 0) to <paramref name="size"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="size">The size of the rectangle from within which the vector will be selected.</param>
		/// <returns>A random 2-dimensional vector from within a rectangle.</returns>
		public static Vector2 PointWithinRectangle(this IRandom random, Vector2 size)
		{
			return new Vector2(random.RangeCC(size.x), random.RangeCC(size.y));
		}

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a parallelogram with corners at (0, 0), <paramref name="axis0"/>, <paramref name="axis1"/>, and <paramref name="axis0"/> + <paramref name="axis1"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="axis0">The first axis defining the parallelogram from within which the vector will be selected.</param>
		/// <param name="axis1">The second axis defining the parallelogram from within which the vector will be selected.</param>
		/// <returns>A random 2-dimensional vector from within a parallelogram.</returns>
		public static Vector2 PointWithinParallelogram(this IRandom random, Vector2 axis0, Vector2 axis1)
		{
			return random.FloatCC() * axis0 + random.FloatCC() * axis1;
		}

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a triangle with corners at (0, 0), <paramref name="axis0"/>, and <paramref name="axis1"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="axis0">The first axis defining the triangle from within which the vector will be selected.</param>
		/// <param name="axis1">The second axis defining the triangle from within which the vector will be selected.</param>
		/// <returns>A random 2-dimensional vector from within a triangle.</returns>
		public static Vector2 PointWithinTriangle(this IRandom random, Vector2 axis0, Vector2 axis1)
		{
			float u = Mathf.Sqrt(random.FloatCC());
			float v = random.RangeCC(u);
			return (1f - u) * axis0 + v * axis1;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a unit cube from (0, 0, 0) to (1, 1, 1).
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 3-dimensional vector from within a unit cube.</returns>
		public static Vector3 PointWithinCube(this IRandom random)
		{
			return new Vector3(random.FloatCC(), random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a cube from (0, 0, 0) to (<paramref name="sideLength"/>, <paramref name="sideLength"/>, <paramref name="sideLength"/>).
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="sideLength">The length of the cube from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a cube.</returns>
		public static Vector3 PointWithinCube(this IRandom random, float sideLength)
		{
			return new Vector3(random.RangeCC(sideLength), random.RangeCC(sideLength), random.RangeCC(sideLength));
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within an axis aligned box from (0, 0, 0) to <paramref name="size"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="size">The size of the box from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a box.</returns>
		public static Vector3 PointWithinBox(this IRandom random, Vector3 size)
		{
			return new Vector3(random.RangeCC(size.x), random.RangeCC(size.y), random.RangeCC(size.z));
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within an axis aligned box described by the <see cref="Bounds"/> specified.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="box">The bounds of the box from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a box.</returns>
		public static Vector3 PointWithinBox(this IRandom random, Bounds box)
		{
			return random.PointWithinBox(box.size) + box.min;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a rhomboid, also know as a parallelepiped, with corners at (0, 0), the sum of any two of the axis parameters, and a far corner at the sum of all three axis parameters.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="axis0">The first axis defining the rhomboid from within which the vector will be selected.</param>
		/// <param name="axis1">The second axis defining the rhomboid from within which the vector will be selected.</param>
		/// <param name="axis2">The third axis defining the rhomboid from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a rhomboid.</returns>
		public static Vector3 PointWithinRhomboid(this IRandom random, Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return random.FloatCC() * axis0 + random.FloatCC() * axis1 + random.FloatCC() * axis2;
		}

		#endregion
	}
}
