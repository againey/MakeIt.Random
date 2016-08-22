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

		public static void UnitVector3(this IRandom random, out Vector3 vec)
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
#else
			// The overall method used is that derived by Marsaglia, and described in his paper found at
			//   http://projecteuclid.org/download/pdf_1/euclid.aoms/1177692644
			// We first need to find a 2D point inside a unit circle.  Then there's a square root that
			// needs to be calculated, followed by the rest of Marsaglia's formula.  It's all done in
			// fixed point form up until the end to maintain speed and bit-level reliability.  Final
			// conversion to float is designed to get maximum possible precision for numbers near zero.

			// Find a point inside a circle, modified inline of RandomVector.PointWithinCircle()
			Start:
			ulong bits = random.Next64();
			uint upper = (uint)(bits >> 32);
			uint lower = (uint)bits;
			// Using 26 bits of each of the 2D components, that gives us 2^52 possible positions in 2D space.
			// This is a half-closed square, so positions with u = +1 or v = +1 never occur, but that's okay
			// because we reject points that are exactly on the edge of the circle anyway.  But technically,
			// any point exactly on the circumference should map to (0, 0, -1) using Marsaglia's formula, and
			// no other (u, v) pair inside the circle will.  So we increase the number of possible states by
			// one, and in that rare 1 out of 2^52 + 1 times, we return exactly (0, 0, -1).
			// 1/2^6 * 1/2^6 * 2^12/(2^52+1) = 1/(2^52+1)
			// We divide the first 12-bit check into two 6-bit checks so that the 32-bit version of this
			// function is more efficient.
			if (upper >= 0xFC000000U && lower >= 0xFC000000U && random.RangeCO(0x0010000000000001UL) < 0x0000000000001000UL)
			{
				vec.x = 0f;
				vec.y = 0f;
				vec.z = -1f;
				return;
			}
			long u = (upper & 0x03FFFFFFU) - 0x02000000L; // x*2^25
			long v = (lower & 0x03FFFFFFU) - 0x02000000L; // y*2^25
			ulong uSqr = (ulong)(u * u); // x^2 * 2^50
			ulong vSqr = (ulong)(v * v); // y^2 * 2^50
			ulong uvSqr = uSqr + vSqr; // (x^2 + y^2) * 2^50
			if (uvSqr >= 0x0004000000000000UL) goto Start; // x^2 + y^2 >= r^2, so generated point is not inside the circle.

			ulong uvSqrInv = (0x0004000000000000UL - uvSqr) << 12; // (1 - (x^2 + y^2)) * 2^62

			// Calculate the square root of uvSqrInv.  This starts with an approximation found at
			//   http://stackoverflow.com/a/1100591
			// It is followed by two uses of the divide-and-average method to improve the initial approximation.

			// Begin with an inline of Detail.DeBruijnLookup.GetBitMaskForRangeMax()
			ulong mask = uvSqrInv | (uvSqrInv >> 1);
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = Detail.DeBruijnLookup.bitCountTable64[mask * Detail.DeBruijnLookup.multiplier64 >> Detail.DeBruijnLookup.shift64];

			// Lookup sqrt(a) (the portion of the square root determined by the magnitude of the number)
			ulong sqrtA = Detail.FloatingPoint.fastSqrtUpper[bitCount]; // a * 2^31
			// Lookup sqrt(b) (the square root of a number between 1 and 2, using 6 bits worth of data)
			ulong sqrtB = Detail.FloatingPoint.fastSqrtLower[(bitCount >= 7 ? (uvSqrInv >> (bitCount - 7)) : (uvSqrInv << (7 - bitCount))) & 0x3FU]; // b * 2^31
			// Square root is a*b
			ulong uvInv = (sqrtA * sqrtB) >> 31; // a * b * 2^31 = sqrt(1 - (x^2 + y^2)) * 2^31

			// Improve the square root approximation using the divide-and-average method twice
			uvInv = (uvSqrInv / uvInv + uvInv) >> 1; // sqrt(1 - (x^2 + y^2)) * 2^31, better approximation
			uvInv = (uvSqrInv / uvInv + uvInv); // 2 * sqrt(1 - (x^2 + y^2)) * 2^31, even better approximation, multiplied by 2

			// Determine the final components using Marsaglia's formulas:  t = 2 * sqrt(1 - (u^2 + v^2)); x = u*t; y = v*t; z = 1 - 2 * (u^2 + v^2))
			long x = u * (long)uvInv; // x * 2^25 * 2 * sqrt(1 - (x^2 + y^2)) * 2^31 = 2 * x * sqrt(1 - (x^2 + y^2)) * 2^56
			long y = v * (long)uvInv; // y * 2^25 * 2 * sqrt(1 - (x^2 + y^2)) * 2^31 = 2 * y * sqrt(1 - (x^2 + y^2)) * 2^56
			long z = 0x0004000000000000L - ((long)uvSqr << 1); // 2 * (1/2 - (x^2 + y^2)) * 2^50

			// Inline of Detail.FloatingPoint.FixedToFloat()
			Detail.FloatingPoint.BitwiseFloat conv;
			conv.bits = 0U;
			conv.number = x;
			conv.bits -= 0x1C000000U; // exponent -= 56
			vec.x = conv.number;
			conv.number = y;
			conv.bits -= 0x1C000000U; // exponent -= 56
			vec.y = conv.number;
			conv.number = z;
			conv.bits -= 0x19000000U; // exponent -= 50
			vec.z = conv.number;
#endif
#endif
		}

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
#endif
#endif
		}

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
