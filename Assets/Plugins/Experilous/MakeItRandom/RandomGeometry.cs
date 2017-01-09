/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

//#define PRINT_ALL
//#define PRINT_SOME

using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random vectors of 2, 3, and 4 dimensions and random quaternions with various spatial attributes and constraints.
	/// </summary>
	public static class RandomGeometry
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
		/// <param name="vec">The out parameter which will hold random 2-dimensional vector with a magnitude equal to 1 upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably faster than <see cref="UnitVector2(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="UnitVector2(IRandom)"/>
		public static void UnitVector2(this IRandom random, out Vector2 vec)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var angle = HalfOpenRange(0f, Mathf.PI * 2f, engine);
			vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			// Inline of RandomVector.PointWithinCircle()
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			int iu = (int)upper;
			int iv = (int)lower;
			if (iu == 0 && iv == 0) goto Start;
			int uScaled = iu >> 16;
			int vScaled = iv >> 16;
			int uvSqrScaled = uScaled * uScaled + vScaled * vScaled;
			// First do a check against the 32-bit radius before doing a full 64-bit calculation
			if (uvSqrScaled >= 0x40000000) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.

			long u = iu;
			long v = iv;
			long uSqr = u * u;
			long vSqr = v * v;
			long uvSqr = uSqr + vSqr;
			// If 32-bit version is greater than a certain threshold, then the full 64-bit version might reach or go
			// over the length of 1 even if the 32-bit version doesn't due to bit truncation.
			if (uvSqrScaled >= 0x3FFF0000 && uvSqr >= 0x4000000000000000L) goto Start; // x^2 + y^2 > r^2, so generated point is not inside the circle.
#else
			// Inline of RandomVector.PointWithinCircle()
			Start:
			ulong bits = random.Next64();
			uint upper = (uint)(bits >> 32);
			uint lower = (uint)bits;
			long u = (int)upper;
			long v = (int)lower;
			long uSqr = u * u;
			long vSqr = v * v;
			long uvSqr = uSqr + vSqr;
			if (uvSqr >= 0x4000000000000000L || uvSqr == 0L) goto Start; // x^2 + y^2 > r^2, so generated point is not inside the circle, or is too close to the center for the division below.
#endif

			// Formula is from http://mathworld.wolfram.com/CirclePointPicking.html
			// x = (u^2 - v^2) / (u^2 + v^2)
			// y = 2uv / (u^2 + v^2)

			// For the sake of minimizing precision loss, we need to carefully manage the number of fixed
			// point fractional digits in the numerators and denominator.  So we'll shift uvSqr so that it
			// has at least 32 significant bits, and the two numerators so that they have 62 significant
			// bits.  The resulting division will never overflow a 32-bit signed integer.  And for the sake
			// of performance, we'll manually inline the calls to GetBitsForRangeMax().

			uint uvSqrMask = (uint)(uvSqr >> 32);
			uvSqrMask |= uvSqrMask >> 1;
			uvSqrMask |= uvSqrMask >> 2;
			uvSqrMask |= uvSqrMask >> 4;
			uvSqrMask |= uvSqrMask >> 8;
			uvSqrMask |= uvSqrMask >> 16;
			int bitCountD = Detail.DeBruijnLookup.bitCountTable32[(uvSqrMask * Detail.DeBruijnLookup.multiplier32) >> Detail.DeBruijnLookup.shift32];
			int shiftD = 0;
			if (bitCountD > 0)
			{
				shiftD = bitCountD;
				// The inner addition is to round up the truncated bits, instead of rounding down.
				// This ensures that the final vector never has a magnitude greater than one.
				uvSqr = (uvSqr + ((1L << shiftD) - 1L)) >> shiftD;
			}

			long uvSqrDiff = uSqr - vSqr;
			ulong uvSqrDiffMask = (ulong)System.Math.Abs(uvSqrDiff);
			uvSqrDiffMask |= uvSqrDiffMask >> 1;
			uvSqrDiffMask |= uvSqrDiffMask >> 2;
			uvSqrDiffMask |= uvSqrDiffMask >> 4;
			uvSqrDiffMask |= uvSqrDiffMask >> 8;
			uvSqrDiffMask |= uvSqrDiffMask >> 16;
			uvSqrDiffMask |= uvSqrDiffMask >> 32;
			int bitCountNX = Detail.DeBruijnLookup.bitCountTable64[(uvSqrDiffMask * Detail.DeBruijnLookup.multiplier64) >> Detail.DeBruijnLookup.shift64];
			int shiftNX = 62 - bitCountNX;
			uvSqrDiff = uvSqrDiff << shiftNX;

			long uv = u * v;
			ulong uvMask = (ulong)System.Math.Abs(uv);
			uvMask |= uvMask >> 1;
			uvMask |= uvMask >> 2;
			uvMask |= uvMask >> 4;
			uvMask |= uvMask >> 8;
			uvMask |= uvMask >> 16;
			uvMask |= uvMask >> 32;
			int bitCountNY = Detail.DeBruijnLookup.bitCountTable64[(uvMask * Detail.DeBruijnLookup.multiplier64) >> Detail.DeBruijnLookup.shift64];
			int shiftNY = 62 - bitCountNY;
			uv = uv << shiftNY;

			int x = (int)(uvSqrDiff / uvSqr);
			int y = (int)(uv / uvSqr); // This does not yet have the multiplication by 2; we'll do that when adjusting floating point exponent.

			// Inline of Detail.FloatingPoint.FixedToFloat()
			Detail.FloatingPoint.BitwiseFloat conv;
			conv.bits = 0U;

			if (x == 0L)
			{
				vec.x = 0f;
			}
			else
			{
				conv.number = x;
				conv.bits += ((uint)(-shiftNX - shiftD) << 23);
				vec.x = conv.number;
			}

			if (y == 0L)
			{
				vec.y = 0f;
			}
			else
			{
				conv.number = y;
				conv.bits += ((uint)(-shiftNY - shiftD + 1) << 23);
				vec.y = conv.number;
			}
#endif
		}

#if UNITY_EDITOR
		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void TestUnitVector2()
		{
			var r = XorShift128Plus.Create();

			float negError0 = 0f;
			float posError0 = 0f;
			float minError0 = 0f;
			float maxError0 = 0f;

			int iterations = 10000000;

			for (int i = 0; i < iterations; ++i)
			{
				Vector2 v;
				r.UnitVector2(out v);
				float e = v.magnitude - 1f;
				if (e > 0f)
				{
					posError0 += e;
					maxError0 = Mathf.Max(maxError0, e);
				}
				else if (e < 0f)
				{
					negError0 += e;
					minError0 = Mathf.Min(minError0, e);
				}
			}

			Debug.LogFormat("MakeIt.UnitVector2: Neg: {0:F16}, Pos: {1:F16}, Min: {2:F16}, Max: {3:F16}", negError0 / iterations, posError0 / iterations, minError0, maxError0);
		}
#endif

		/// <summary>
		/// Generates a random 3-dimensional unit vector, selected from a uniform distribution of all points on the surface of a unit sphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 3-dimensional unit vector.</returns>
		/// <remarks><note type="note">This function variant can be noticeably slower than <see cref="UnitVector3(IRandom, out Vector3)"/> in some environments.</note></remarks>
		/// <seealso cref="UnitVector3(IRandom, out Vector3)"/>
		public static Vector3 UnitVector3(this IRandom random)
		{
			Vector3 v;
			random.UnitVector3(out v);
			return v;
		}

		/// <summary>
		/// Generates a random 3-dimensional unit vector, selected from a uniform distribution of all points on the surface of a unit sphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="vec">The out parameter which will hold random 3-dimensional vector with a magnitude equal to 1 upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably faster than <see cref="UnitVector3(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="UnitVector3(IRandom)"/>
		public static void UnitVector3(this IRandom random, out Vector3 vec)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var longitude = random.RangeCO(0f, Mathf.PI * 2f);
			var z = random.RangeCC(-1f, +1f);
			var invertedZ = Mathf.Sqrt(1f - z * z);
			vec = new Vector3(invertedZ * Mathf.Cos(longitude), invertedZ * Mathf.Sin(longitude), z);
#else
			// The overall method used is that derived by Marsaglia, and described in his paper found at
			//   http://projecteuclid.org/download/pdf_1/euclid.aoms/1177692644
			// We first need to find a 2D point inside a unit circle.  Then there's a square root that
			// needs to be calculated, followed by the rest of Marsaglia's formula.  It's all done in
			// fixed point form up until the end to maintain speed and bit-level reliability.  Final
			// conversion to float is designed to get maximum possible precision for numbers near zero.

			// Find a point inside a circle, modified inline of RandomVector.PointWithinCircle()
			Start:

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
#else
			ulong bits = random.Next64();
			uint upper = (uint)(bits >> 32);
			uint lower = (uint)bits;
#endif

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

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			int u = (int)(upper & 0x03FFFFFFU) - 0x02000000; // x*2^25
			int v = (int)(lower & 0x03FFFFFFU) - 0x02000000; // y*2^25
			int uScaled = u >> 10; //x*2^15
			int vScaled = v >> 10; //y*2^15
			int uvSqrScaled = uScaled * uScaled + vScaled * vScaled; //(x^2 + y^2)*2^30
			// First do a check against the 32-bit radius before doing a full 64-bit calculation
			if (uvSqrScaled > 0x40000000) goto Start; // x^2 + y^2 >= r^2, so generated point is not inside the circle.

			ulong uSqr = (ulong)((long)u * u); // x^2 * 2^50
			ulong vSqr = (ulong)((long)v * v); // y^2 * 2^50
			ulong uvSqr = uSqr + vSqr; // (x^2 + y^2) * 2^50
			if (uvSqr >= 0x0004000000000000UL) goto Start; // x^2 + y^2 >= r^2, so generated point is not inside the circle.
#else
			long u = (upper & 0x03FFFFFFU) - 0x02000000L; // x*2^25
			long v = (lower & 0x03FFFFFFU) - 0x02000000L; // y*2^25
			ulong uSqr = (ulong)(u * u); // x^2 * 2^50
			ulong vSqr = (ulong)(v * v); // y^2 * 2^50
			ulong uvSqr = uSqr + vSqr; // (x^2 + y^2) * 2^50
			if (uvSqr >= 0x0004000000000000UL) goto Start; // x^2 + y^2 >= r^2, so generated point is not inside the circle.
#endif

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

			if (x == 0L)
			{
				vec.x = 0f;
			}
			else
			{
				conv.number = x;
				conv.bits -= 0x1C000000U; // exponent -= 56
				vec.x = conv.number;
			}

			if (y == 0L)
			{
				vec.y = 0f;
			}
			else
			{
				conv.number = y;
				conv.bits -= 0x1C000000U; // exponent -= 56
				vec.y = conv.number;
			}

			if (z == 0L)
			{
				vec.z = 0f;
			}
			else
			{
				conv.number = z;
				conv.bits -= 0x19000000U; // exponent -= 50
				vec.z = conv.number;
			}
#endif
		}

#if UNITY_EDITOR
		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void TestUnitVector3()
		{
			var r = XorShift128Plus.Create();

			float negError0 = 0f;
			float posError0 = 0f;
			float minError0 = 0f;
			float maxError0 = 0f;

			float negError1 = 0f;
			float posError1 = 0f;
			float minError1 = 0f;
			float maxError1 = 0f;

			int iterations = 1000000;

			for (int i = 0; i < iterations; ++i)
			{
				Vector3 v;
				r.UnitVector3(out v);
				float e = v.magnitude - 1f;
				if (e > 0f)
				{
					posError0 += e;
					maxError0 = Mathf.Max(maxError0, e);
				}
				else if (e < 0f)
				{
					negError0 += e;
					minError0 = Mathf.Min(minError0, e);
				}

				v = Random.onUnitSphere;
				e = v.magnitude - 1f;
				if (e > 0f)
				{
					posError1 += e;
					maxError1 = Mathf.Max(maxError1, e);
				}
				else if (e < 0f)
				{
					negError1 += e;
					minError1 = Mathf.Min(minError1, e);
				}
			}

			Debug.LogFormat("MakeIt.UnitVector3: Neg: {0:F16}, Pos: {1:F16}, Min: {2:F16}, Max: {3:F16}", negError0 / iterations, posError0 / iterations, minError0, maxError0);
			Debug.LogFormat("Unity.onUnitSphere: Neg: {0:F16}, Pos: {1:F16}, Min: {2:F16}, Max: {3:F16}", negError1 / iterations, posError1 / iterations, minError1, maxError1);
		}
#endif

		/// <summary>
		/// Generates a random 4-dimensional unit vector, selected from a uniform distribution of all points on the surface of a unit hypersphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 4-dimensional unit vector.</returns>
		/// <remarks><note type="note">This function variant can be noticeably slower than <see cref="UnitVector4(IRandom, out Vector4)"/> in some environments.</note></remarks>
		/// <seealso cref="UnitVector4(IRandom, out Vector4)"/>
		public static Vector4 UnitVector4(this IRandom random)
		{
			Vector4 vec;
			random.UnitVector4(out vec);
			return vec;
		}

		/// <summary>
		/// Generates a random 4-dimensional unit vector, selected from a uniform distribution of all points on the surface of a unit hypersphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="vec">The out parameter which will hold random 4-dimensional vector with a magnitude equal to 1 upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably faster than <see cref="UnitVector4(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="UnitVector4(IRandom)"/>
		public static void UnitVector4(this IRandom random, out Vector4 vec)
		{
			// General formula from:  http://mathworld.wolfram.com/HyperspherePointPicking.html

			// First modified inline of RandomVector.PointWithinCircle()
			Start1:

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			int iu = (int)upper;
			int iv = (int)lower;
			if (iu == 0 && iv == 0) goto Start1;
			int uScaled = iu >> 16;
			int vScaled = iv >> 16;
			int uvSqrScaled = uScaled * uScaled + vScaled * vScaled;
			// First do a check against the 32-bit radius before doing a full 64-bit calculation
			if (uvSqrScaled >= 0x40000000) goto Start1; // x^2 + y^2 > r^2, so generated point is outside the circle.

			long u1 = iu;
			long v1 = iv;
			long uSqr = u1 * u1;
			long vSqr = v1 * v1;
			long uvSqr1 = uSqr + vSqr;
			// If 32-bit version is greater than a certain threshold, then the full 64-bit version might reach or go
			// over the length of 1 even if the 32-bit version doesn't due to bit truncation.
			if (uvSqrScaled >= 0x3FFF0000 && uvSqr1 >= 0x4000000000000000L) goto Start1; // x^2 + y^2 > r^2, so generated point is not inside the circle.
#else
			ulong bits = random.Next64();
			uint upper = (uint)(bits >> 32);
			uint lower = (uint)bits;
			long u1 = (int)upper;
			long v1 = (int)lower;
			long uSqr = u1 * u1;
			long vSqr = v1 * v1;
			long uvSqr1 = uSqr + vSqr;
			if (uvSqr1 >= 0x4000000000000000L) goto Start1; // x^2 + y^2 > r^2, so generated point is not inside the circle.
#endif

			// Second modified inline of RandomVector.PointWithinCircle()
			Start2:

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			random.Next64(out lower, out upper);
			iu = (int)upper;
			iv = (int)lower;
			if (iu == 0 && iv == 0) goto Start2;
			uScaled = iu >> 16;
			vScaled = iv >> 16;
			uvSqrScaled = uScaled * uScaled + vScaled * vScaled;
			// First do a check against the 32-bit radius before doing a full 64-bit calculation
			if (uvSqrScaled >= 0x40000000) goto Start2; // x^2 + y^2 > r^2, so generated point is outside the circle.

			long u2 = iu;
			long v2 = iv;
			uSqr = u2 * u2;
			vSqr = v2 * v2;
			long uvSqr2 = uSqr + vSqr;
			// If 32-bit version is greater than a certain threshold, then the full 64-bit version might reach or go
			// over the length of 1 even if the 32-bit version doesn't due to bit truncation.
			if (uvSqrScaled >= 0x3FFF0000 && uvSqr2 >= 0x4000000000000000L) goto Start2; // x^2 + y^2 > r^2, so generated point is not inside the circle.
#else
			bits = random.Next64();
			upper = (uint)(bits >> 32);
			lower = (uint)bits;
			long u2 = (int)upper;
			long v2 = (int)lower;
			uSqr = u2 * u2;
			vSqr = v2 * v2;
			long uvSqr2 = uSqr + vSqr;
			if (uvSqr2 >= 0x4000000000000000L) goto Start2; // x^2 + y^2 > r^2, so generated point is not inside the circle.
#endif

			ulong numerator;
			ulong denominator;

			if (uvSqr2 > 0xFFFFFFFFL)
			{
				numerator = 0x4000000000000000UL - (ulong)uvSqr1;
				denominator = (ulong)uvSqr2;
			}
			else if (uvSqr1 > 0xFFFFFFFFL)
			{
				numerator = 0x4000000000000000UL - (ulong)uvSqr2;
				denominator = (ulong)uvSqr1;
			}
			else
			{
				goto Start1;
			}

			uint denominatorMask = (uint)(denominator >> 32);
			denominatorMask |= denominatorMask >> 1;
			denominatorMask |= denominatorMask >> 2;
			denominatorMask |= denominatorMask >> 4;
			denominatorMask |= denominatorMask >> 8;
			denominatorMask |= denominatorMask >> 16;
			int shiftD = Detail.DeBruijnLookup.bitCountTable32[(denominatorMask * Detail.DeBruijnLookup.multiplier32) >> Detail.DeBruijnLookup.shift32];

			ulong numeratorMask = numerator;
			numeratorMask |= numeratorMask >> 1;
			numeratorMask |= numeratorMask >> 2;
			numeratorMask |= numeratorMask >> 4;
			numeratorMask |= numeratorMask >> 8;
			numeratorMask |= numeratorMask >> 16;
			numeratorMask |= numeratorMask >> 32;
			int shiftN = 62 - Detail.DeBruijnLookup.bitCountTable64[(numeratorMask * Detail.DeBruijnLookup.multiplier64) >> Detail.DeBruijnLookup.shift64];

			// Because of the square root below, we can't have these two shifts amount differ by
			// an odd amount.
			if (((shiftN + shiftD) & 1) == 1)
			{
				shiftD -= 1;
			}

			if (shiftD > 0)
			{
				// The inner addition is to round up the truncated bits, instead of rounding down.
				// This ensures that the final vector never has a magnitude greater than one.
				denominator = (denominator + ((1UL << shiftD) - 1UL)) >> shiftD;
			}

			numerator = numerator << shiftN;

			ulong t;
			ulong tSqr = (numerator / denominator) << 32;
			if (tSqr > 0UL)
			{
				// Calculate the square root of tSqr.  This starts with an approximation found at
				//   http://stackoverflow.com/a/1100591
				// It is followed by two uses of the divide-and-average method to improve the initial approximation.

				// Begin with an inline of Detail.DeBruijnLookup.GetBitMaskForRangeMax()
				ulong mask = tSqr | (tSqr >> 1);
				mask |= mask >> 2;
				mask |= mask >> 4;
				mask |= mask >> 8;
				mask |= mask >> 16;
				mask |= mask >> 32;
				int bitCount = Detail.DeBruijnLookup.bitCountTable64[mask * Detail.DeBruijnLookup.multiplier64 >> Detail.DeBruijnLookup.shift64];

				// Lookup sqrt(a) (the portion of the square root determined by the magnitude of the number)
				ulong sqrtA = Detail.FloatingPoint.fastSqrtUpper[bitCount]; // a * 2^31
				// Lookup sqrt(b) (the square root of a number between 1 and 2, using 6 bits worth of data)
				ulong sqrtB = Detail.FloatingPoint.fastSqrtLower[(bitCount >= 7 ? (tSqr >> (bitCount - 7)) : (tSqr << (7 - bitCount))) & 0x3FU]; // b * 2^31
				// Square root is a*b
				t = (sqrtA * sqrtB) >> 31; // a * b * 2^31 = sqrt((1 - uvSqr1) / uvSqr2) * 2^31

				// Improve the square root approximation using the divide-and-average method twice
				t = (tSqr / t + t) >> 1; // sqrt((1 - uvSqr1) / uvSqr2) * 2^31, better approximation
				t = (tSqr / t + t) >> 1; // sqrt((1 - uvSqr1) / uvSqr2) * 2^31, even better approximation
			}
			else
			{
				t = 0UL;
			}

			long x, y, z, w;

			if (uvSqr2 > 0xFFFFFFFFL)
			{
				x = u1;
				y = v1;
				z = u2 * (long)t;
				w = v2 * (long)t;
			}
			else
			{
				x = u1 * (long)t;
				y = v1 * (long)t;
				z = u2;
				w = v2;
			}

			// The shift by 1 is due to the square root halving the effects of the shifts.
			uint exponent = ((uint)(-47 - ((shiftN + shiftD) >> 1)) << 23);

			// Inline of Detail.FloatingPoint.FixedToFloat()
			Detail.FloatingPoint.BitwiseFloat conv;
			conv.bits = 0U;

			if (x == 0L)
			{
				vec.x = 0f;
			}
			else
			{
				conv.number = x;
				if (uvSqr2 > 0xFFFFFFFFL)
				{
					conv.bits -= 0x0F800000U; // exponent -= 31
				}
				else
				{
					conv.bits += exponent;
				}
				vec.x = conv.number;
			}

			if (y == 0L)
			{
				vec.y = 0f;
			}
			else
			{
				conv.number = y;
				if (uvSqr2 > 0xFFFFFFFFL)
				{
					conv.bits -= 0x0F800000U; // exponent -= 31
				}
				else
				{
					conv.bits += exponent;
				}
				vec.y = conv.number;
			}

			if (z == 0L)
			{
				vec.z = 0f;
			}
			else
			{
				conv.number = z;
				if (uvSqr2 > 0xFFFFFFFFL)
				{
					conv.bits += exponent;
				}
				else
				{
					conv.bits -= 0x0F800000U; // exponent -= 31
				}
				vec.z = conv.number;
			}

			if (w == 0L)
			{
				vec.w = 0f;
			}
			else
			{
				conv.number = w;
				if (uvSqr2 > 0xFFFFFFFFL)
				{
					conv.bits += exponent;
				}
				else
				{
					conv.bits -= 0x0F800000U; // exponent -= 31
				}
				vec.w = conv.number;
			}
		}

#if UNITY_EDITOR
		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void TestUnitVector4()
		{
			var r = XorShift128Plus.Create();

			var invSqrt2 = 1f / Mathf.Sqrt(2f);
			var invSqrt3 = 1f / Mathf.Sqrt(3f);
			var invSqrt4 = 1f / Mathf.Sqrt(4f);
			Vector4[] comparisonVectors = new Vector4[]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f),
				new Vector4(invSqrt2, invSqrt2, 0f, 0f),
				new Vector4(invSqrt2, 0f, invSqrt2, 0f),
				new Vector4(invSqrt2, 0f, 0f, invSqrt2),
				new Vector4(0f, invSqrt2, invSqrt2, 0f),
				new Vector4(0f, invSqrt2, 0f, invSqrt2),
				new Vector4(0f, 0f, invSqrt2, invSqrt2),
				new Vector4(invSqrt3, invSqrt3, invSqrt3, 0f),
				new Vector4(invSqrt3, invSqrt3, 0f, invSqrt3),
				new Vector4(invSqrt3, 0f, invSqrt3, invSqrt3),
				new Vector4(0f, invSqrt3, invSqrt3, invSqrt3),
				new Vector4(invSqrt4, invSqrt4, invSqrt4, invSqrt4),
			};

			Vector4[] componentVectors = new Vector4[]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f),
				new Vector4(1f, 1f, 0f, 0f),
				new Vector4(1f, 0f, 1f, 0f),
				new Vector4(1f, 0f, 0f, 1f),
				new Vector4(0f, 1f, 1f, 0f),
				new Vector4(0f, 1f, 0f, 1f),
				new Vector4(0f, 0f, 1f, 1f),
				new Vector4(1f, 1f, 1f, 0f),
				new Vector4(1f, 1f, 0f, 1f),
				new Vector4(1f, 0f, 1f, 1f),
				new Vector4(0f, 1f, 1f, 1f),
			};

			float negError = 0f;
			float posError = 0f;
			float minError = 0f;
			float maxError = 0f;
			float[] distanceSums = new float[comparisonVectors.Length];
			float[] componentSquareSums = new float[componentVectors.Length];

			int iterations = 1000000;

			for (int i = 0; i < iterations; ++i)
			{
				Vector4 v;
				r.UnitVector4(out v);
				float e = v.magnitude - 1f;
				if (e > 0f)
				{
					posError += e;
					maxError = Mathf.Max(maxError, e);
				}
				else if (e < 0f)
				{
					negError += e;
					minError = Mathf.Min(minError, e);
				}

				for (int j = 0; j < comparisonVectors.Length; ++j)
				{
					distanceSums[j] += (comparisonVectors[j] - v).magnitude;
				}

				for (int j = 0; j < componentVectors.Length; ++j)
				{
					componentSquareSums[j] += Vector4.Scale(componentVectors[j], v).sqrMagnitude;
				}
			}

			Debug.LogFormat("MakeIt.UnitVector4:    Neg: {0:F16}, Pos: {1:F16}, Min: {2:F16}, Max: {3:F16}", negError / iterations, posError / iterations, minError, maxError);

			Debug.Log("Distances");
			for (int j = 0; j < comparisonVectors.Length; ++j)
			{
				Debug.LogFormat("Comparison Vector {0}: {1:F16}", comparisonVectors[j], distanceSums[j] / iterations);
			}

			Debug.Log("Component Square Sums");
			for (int j = 0; j < componentVectors.Length; ++j)
			{
				Debug.LogFormat("Component Vector {0}: {1:F16}", componentVectors[j], componentSquareSums[j] / iterations);
			}
		}
#endif

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

		#region Radial Vectors

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
		/// <param name="vec">The out parameter which will hold random 2-dimensional vector with a magnitude less than or equal to 1 upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably faster than <see cref="PointWithinCircle(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="PointWithinCircle(IRandom)"/>
		public static void PointWithinCircle(this IRandom random, out Vector2 vec)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var distance = Mathf.Sqrt(random.FloatCC());
			vec = random.UnitVector2() * distance;
#else
			Start:

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
#else
			ulong bits = random.Next64();
			uint upper = (uint)(bits >> 32);
			uint lower = (uint)bits;
#endif

			// Using 23 bits of each of the 2D components, that gives us 2^46 possible positions in 2D space.
			// This is a half-closed square, so positions with u = +1 or v = +1 never occur.  To compensate,
			// we increase the number of possible states by two, and in those rare 2 out of 2^46 + 2 times,
			// we return exactly (+1, 0) or (0, +1).
			// 1/2^9 * 1/2^9 * (2 * 2^18)/(2^46+2) = 1/2^9 * 1/2^9 * 2^18/(2^45+1) = 2/(2^46+2)
			if (upper >= 0xFF800000U && lower >= 0xFF800000U && random.RangeCO(0x0000200000000001UL) < 0x0000000000040000UL)
			{
				vec = random.Chance() ? new Vector2(1f, 0f) : new Vector2(0f, 1f);
				return;
			}

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			int u = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int v = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			int uScaled = u >> 8;
			int vScaled = v >> 8;
			int uvScaled = uScaled * uScaled + vScaled * vScaled;
			// First do a check against the 32-bit radius before doing a full 64-bit calculation
			if (uvScaled > 0x10000000 || uvScaled > 0x0FFF8000 && (long)u * u + (long)v * v > 0x0000100000000000L)
			{
				goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.
			}
#else
			long u = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long v = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			if (u * u + v * v > 0x0000100000000000L) goto Start; // x^2 + y^2 > r^2, so generated point is outside the circle.
#endif

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & upper;
			vec.x = value.number - 3f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & lower;
			vec.y = value.number - 3f;
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
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var distance = Mathf.Sqrt(random.FloatCC()) * radius;
			return random.UnitVector2() * distance;
#else
			return random.PointWithinCircle() * radius;
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
			var unitMin = innerRadius / outerRadius;
			var unitMinSquared = unitMin * unitMin;
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var unitRange = 1f - unitMinSquared;
			var distance = Mathf.Sqrt(random.FloatCC() * unitRange + unitMinSquared) * outerRadius;
#else
			var distance = Mathf.Sqrt(random.PreciseRangeCC(unitMinSquared, 1f)) * outerRadius;
#endif
			return random.UnitVector2() * distance;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a unit sphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 3-dimensional vector with a magnitude less than or equal to 1.</returns>
		/// <remarks><note type="note">This function variant can be noticeably slower than <see cref="PointWithinSphere(IRandom, out Vector3)"/> in some environments.</note></remarks>
		/// <seealso cref="PointWithinSphere(IRandom, out Vector3)"/>
		public static Vector3 PointWithinSphere(this IRandom random)
		{
			Vector3 v;
			random.PointWithinSphere(out v);
			return v;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a unit sphere.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="vec">The out parameter which will hold random 3-dimensional vector with a magnitude less than or equal to 1 upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably faster than <see cref="PointWithinSphere(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="PointWithinSphere(IRandom)"/>
		public static void PointWithinSphere(this IRandom random, out Vector3 vec)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var distance = Mathf.Pow(random.FloatCC(), 1f / 3f);
			vec = random.UnitVector3() * distance;
#else
			Start:

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint ux, uy, uz, uw;
			random.Next64(out ux, out uy);
			random.Next64(out uz, out uw);
#else
			ulong uxuy = random.Next64();
			ulong uzuw = random.Next64();
			uint ux = (uint)uxuy;
			uint uy = (uint)(uxuy >> 32);
			uint uz = (uint)uzuw;
#endif

			// Using 23 bits of each of the 3D components, that gives us 2^69 possible positions in 3D space.
			// This is a half-closed cube, so positions (+1, 0, 0), (0, +1, 0), and (0, 0, +1) never occur.
			// To compensate, we increase the number of possible states by three, and in those rare 3 out of
			// 2^69 + 3 times, we return one of those special three positions.
			// 1/2^41 * 1/2^9 * 1/2^9 * (3 * 2^59)/(2^69+3) = 3/(2^69+3)
			// That's impossible to directly calculate with 64-bit integers, but thanks to the fact that 2^69+3
			// can be factored as 5 * 418427 * 282149961813509, (and thanks to the following website for letting
			// me calculate that quickly:  http://www.numberempire.com/numberfactorizer.php), we can adjust the
			// random chance rolls as follows:
			// 1/2^41 * 1/2^9 * 1/2^9 * 2^40/282149961813509 * (3 * 2^19)/(5 * 418427) = 3/(2^69+3)
			if (
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
				uw == 0xFFFFFFFFU && uz >= 0xFF800000UL && ux >= 0xFF800000U && uy >= 0xFF800000U
#else
				uzuw >= 0xFFFFFFFFFF800000UL && ux >= 0xFF800000U && uy >= 0xFF800000U
#endif
				&& random.Probability(0x0000010000000000UL, 282149961813509UL) && random.Probability(0x00180000U, 2092135U))
			{
				switch (random.RangeCO(3U))
				{
					case 0U: vec = new Vector3(1f, 0f, 0f); return;
					case 1U: vec = new Vector3(0f, 1f, 0f); return;
					case 2U: vec = new Vector3(0f, 0f, 1f); return;
					default: throw new System.InvalidOperationException();
				}
			}

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
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
#else
			long ix = (int)(ux & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iy = (int)(uy & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iz = (int)(uz & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			if (ix * ix + iy * iy + iz * iz > 0x0000100000000000L) goto Start; // x^2 + y^2 + z^2 > r^2, so generated point is outside the sphere.
#endif

			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & ux;
			vec.x = value.number - 3f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & uy;
			vec.y = value.number - 3f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & uz;
			vec.z = value.number - 3f;
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
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var distance = Mathf.Pow(random.FloatCC(), 1f / 3f) * radius;
			return random.UnitVector3() * distance;
#else
			return random.PointWithinSphere() * radius;
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
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(random.FloatCC() * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
#else
			var distance = Mathf.Pow(random.PreciseRangeCC(unitMinPow3, 1f), 1f / 3f) * outerRadius;
#endif
			return random.UnitVector3() * distance;
		}

		#endregion

		#region Axial Vectors

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
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a parallelogram with corners at <paramref name="root"/>, <paramref name="side0"/>, <paramref name="side1"/>, and an implicit corner opposite from <paramref name="root"/> located at <paramref name="side0"/> + <paramref name="side1"/> - <paramref name="root"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="root">The root point defining the parallelogram from within which the vector will be selected.</param>
		/// <param name="side0">The first side point defining the parallelogram from within which the vector will be selected.</param>
		/// <param name="side1">The second side point defining the parallelogram from within which the vector will be selected.</param>
		/// <returns>A random 2-dimensional vector from within a parallelogram.</returns>
		public static Vector2 PointWithinParallelogram(this IRandom random, Vector2 root, Vector2 side0, Vector2 side1)
		{
			return random.PointWithinParallelogram(side0 - root, side1 - root) + root;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a parallelogram with corners at (0, 0, 0), <paramref name="axis0"/>, <paramref name="axis1"/>, and <paramref name="axis0"/> + <paramref name="axis1"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="axis0">The first axis defining the parallelogram from within which the vector will be selected.</param>
		/// <param name="axis1">The second axis defining the parallelogram from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a parallelogram.</returns>
		public static Vector3 PointWithinParallelogram(this IRandom random, Vector3 axis0, Vector3 axis1)
		{
			return random.FloatCC() * axis0 + random.FloatCC() * axis1;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a parallelogram with corners at <paramref name="root"/>, <paramref name="side0"/>, <paramref name="side1"/>, and an implicit corner opposite from <paramref name="root"/> located at <paramref name="side0"/> + <paramref name="side1"/> - <paramref name="root"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="root">The root point defining the parallelogram from within which the vector will be selected.</param>
		/// <param name="side0">The first side point defining the parallelogram from within which the vector will be selected.</param>
		/// <param name="side1">The second side point defining the parallelogram from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a parallelogram.</returns>
		public static Vector3 PointWithinParallelogram(this IRandom random, Vector3 root, Vector3 side0, Vector3 side1)
		{
			return random.PointWithinParallelogram(side0 - root, side1 - root) + root;
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
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a triangle with corners at <paramref name="point0"/>, <paramref name="point1"/>, and <paramref name="point2"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="point0">The first point defining the triangle from within which the vector will be selected.</param>
		/// <param name="point1">The second point defining the triangle from within which the vector will be selected.</param>
		/// <param name="point2">The third point defining the triangle from within which the vector will be selected.</param>
		/// <returns>A random 2-dimensional vector from within a triangle.</returns>
		public static Vector2 PointWithinTriangle(this IRandom random, Vector2 point0, Vector2 point1, Vector2 point2)
		{
			return random.PointWithinTriangle(point1 - point0, point2 - point0) + point0;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a triangle with corners at (0, 0, 0), <paramref name="axis0"/>, and <paramref name="axis1"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="axis0">The first axis defining the triangle from within which the vector will be selected.</param>
		/// <param name="axis1">The second axis defining the triangle from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a triangle.</returns>
		public static Vector3 PointWithinTriangle(this IRandom random, Vector3 axis0, Vector3 axis1)
		{
			float u = Mathf.Sqrt(random.FloatCC());
			float v = random.RangeCC(u);
			return (1f - u) * axis0 + v * axis1;
		}

		/// <summary>
		/// Generates a random 3-dimensional vector selected from a uniform distribution of all points within a triangle with corners at <paramref name="point0"/>, <paramref name="point1"/>, and <paramref name="point2"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="point0">The first point defining the triangle from within which the vector will be selected.</param>
		/// <param name="point1">The second point defining the triangle from within which the vector will be selected.</param>
		/// <param name="point2">The third point defining the triangle from within which the vector will be selected.</param>
		/// <returns>A random 3-dimensional vector from within a triangle.</returns>
		public static Vector3 PointWithinTriangle(this IRandom random, Vector3 point0, Vector3 point1, Vector3 point2)
		{
			return random.PointWithinTriangle(point1 - point0, point2 - point0) + point0;
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

		#region Quaternion

		/// <summary>
		/// Generates a random quaternion, selected from a uniform distribution of all possible 3-dimensional rotations or orientations.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random rotation quaternion.</returns>
		/// <remarks><note type="note">This function variant can be noticeably slower than <see cref="Rotation(IRandom, out Quaternion)"/> in some environments.</note></remarks>
		/// <seealso cref="Rotation(IRandom, out Quaternion)"/>
		public static Quaternion Rotation(this IRandom random)
		{
			Quaternion quat;
			random.Rotation(out quat);
			return quat;
		}

		private static ulong sinApprox9thOrderA = 3373259731UL; // 1.5707964688206900 as Q1.31
		private static ulong sinApprox9thOrderB = 2774394673UL; // 0.6459640975062460 as Q0.32
		private static ulong sinApprox9thOrderC = 342258823UL; // 0.0796883420605488 as Q0.32
		private static ulong sinApprox9thOrderD = 20058644UL; // 0.0046702668851237 as Q0.32
		private static ulong sinApprox9thOrderE = 2630973174UL; // 0.0001495535101299 as Q0.44

		/// <summary>
		/// Generates a random quaternion, selected from a uniform distribution of all possible 3-dimensional rotations or orientations.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quat">The out parameter which will hold random rotation quaternion upon completion of the function.</param>
		/// <remarks><note type="note">This function variant can be noticeably slower than <see cref="Rotation(IRandom)"/> in some environments.</note></remarks>
		/// <seealso cref="Rotation(IRandom)"/>
		public static void Rotation(this IRandom random, out Quaternion quat)
		{
			// Overall algorithm is to generate a uniformly distributed axis (3D unit vector), generate a
			// random angle to rotate around that axis, and then convert that angle-axis pair to a quaternion
			// the following formula:
			//
			//   qx = ax * sin(theta/2)
			//   qy = ay * sin(theta/2)
			//   qz = az * sin(theta/2)
			//   qw = cos(theta/2)
			//
			// This is all done in fixed point, with sine and cosine approximations approximations based on
			// fifth-order polynomials.

			// Modified inline of UnitVector3

			long x;
			long y;
			long z;

			// Find a point inside a circle, modified inline of RandomVector.PointWithinCircle()
			Axis:

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
#else
			ulong bits = random.Next64();
			uint upper = (uint)(bits >> 32);
			uint lower = (uint)bits;
#endif

			if (upper >= 0xFC000000U && lower >= 0xFC000000U && random.RangeCO(0x0010000000000001UL) < 0x0000000000001000UL)
			{
				x = 0L; // Q32.32
				y = 0L; // Q32.32
				z = -1L << 32; // Q32.32
			}
			else
			{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
				int u = (int)(upper & 0x03FFFFFFU) - 0x02000000; // x*2^25
				int v = (int)(lower & 0x03FFFFFFU) - 0x02000000; // y*2^25
				int uScaled = u >> 10; //x*2^15
				int vScaled = v >> 10; //y*2^15
				int uvSqrScaled = uScaled * uScaled + vScaled * vScaled; //(x^2 + y^2)*2^30
				// First do a check against the 32-bit radius before doing a full 64-bit calculation
				if (uvSqrScaled > 0x40000000) goto Axis; // x^2 + y^2 >= r^2, so generated point is not inside the circle.

				ulong uSqr = (ulong)((long)u * u); // x^2 * 2^50
				ulong vSqr = (ulong)((long)v * v); // y^2 * 2^50
				ulong uvSqr = uSqr + vSqr; // (x^2 + y^2) * 2^50
				if (uvSqr >= 0x0004000000000000UL) goto Axis; // x^2 + y^2 >= r^2, so generated point is not inside the circle.
#else
				long u = (upper & 0x03FFFFFFU) - 0x02000000L; // x*2^25
				long v = (lower & 0x03FFFFFFU) - 0x02000000L; // y*2^25
				ulong uSqr = (ulong)(u * u); // x^2 * 2^50
				ulong vSqr = (ulong)(v * v); // y^2 * 2^50
				ulong uvSqr = uSqr + vSqr; // (x^2 + y^2) * 2^50
				if (uvSqr >= 0x0004000000000000UL) goto Axis; // x^2 + y^2 >= r^2, so generated point is not inside the circle.
#endif

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
				x = (u * (long)uvInv) >> 24; // x * 2^25 * 2 * sqrt(1 - (x^2 + y^2)) * 2^31 = 2 * x * sqrt(1 - (x^2 + y^2)) * 2^56, shifted down to Q32.32
				y = (v * (long)uvInv) >> 24; // y * 2^25 * 2 * sqrt(1 - (x^2 + y^2)) * 2^31 = 2 * y * sqrt(1 - (x^2 + y^2)) * 2^56, shifted down to Q32.32
				z = (0x0002000000000000L - ((long)uvSqr)) >> 17; // 2 * (1/2 - (x^2 + y^2)) * 2^49, shifted down to Q32.32
			}

			ulong angleBits = random.Next64(); // Q1.32, in range [0, 2), as if it were radians in [0, π), scaled by 2/π

			long sine;
			long cosine;
			long w;

			// Ninth order polynomial approximation of sine
			// sine(½πx) = sine(z) = (a - (b - (c - (d - e⋅z²)⋅z²)⋅z²)⋅z²)⋅z, x ∈ [0, ½π)

			ulong thetaS = angleBits >> 32; // Q0.32
			if (thetaS > 0UL)
			{
				ulong thetaSSqr = (thetaS * thetaS) >> 32; // Q0.32
				ulong n = (thetaSSqr * sinApprox9thOrderE) >> 44; // e⋅z², Q0.32 * Q0.44 / 2^44 -> Q0.32
				n = (thetaSSqr * (sinApprox9thOrderD - n)) >> 32; // (d - e⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^32 -> Q0.32
				n = (thetaSSqr * (sinApprox9thOrderC - n)) >> 32; // (c - (d - e⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.32
				n = (thetaSSqr * (sinApprox9thOrderB - n)) >> 33; // (b - (c - (d - e⋅z²)⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.31
				n = (thetaS * (sinApprox9thOrderA - n)) >> 32; // (a - (b - (c - (d - e⋅z²)⋅z²)⋅z²)⋅z²)⋅z, (Q1.31 - Q0.31) * Q0.32 / 2^32 -> Q0.31
				sine = (long)n; // Q0.31

				ulong thetaC = 0x100000000UL - thetaS; // Q0.32
				ulong thetaCSqr = (thetaC * thetaC) >> 32; // Q0.32
				n = (thetaCSqr * sinApprox9thOrderE) >> 44; // e⋅z², Q0.32 * Q0.44 / 2^44 -> Q0.32
				n = (thetaCSqr * (sinApprox9thOrderD - n)) >> 32; // (d - e⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^32 -> Q0.32
				n = (thetaCSqr * (sinApprox9thOrderC - n)) >> 32; // (c - (d - e⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.32
				n = (thetaCSqr * (sinApprox9thOrderB - n)) >> 33; // (b - (c - (d - e⋅z²)⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.31
				n = (thetaC * (sinApprox9thOrderA - n)) >> 32; // (a - (b - (c - (d - e⋅z²)⋅z²)⋅z²)⋅z²)⋅z, (Q1.31 - Q0.31) * Q0.32 / 2^32 -> Q0.31
				cosine = (long)n; // Q0.31
			}
			else
			{
				sine = 0L;
				cosine = 0x80000000L;
			}

			if ((angleBits & 0x80000000UL) == 0UL)
			{
				// If the angle is in the first quadrant of the trig cycle, sine stays as it is and w is simply cosine.
				w = cosine;
			}
			else
			{
				// If the angle is in the second quadrant of the trig cycle, sine and cosine swap, and w is the negative of the swapped cosine.
				w = -sine;
				sine = cosine;
			}

			x *= sine; // Q0.32 * Q0.31 = Q0.63
			y *= sine; // Q0.32 * Q0.31 = Q0.63
			z *= sine; // Q0.32 * Q0.31 = Q0.63

			// Inline of Detail.FloatingPoint.FixedToFloat()
			Detail.FloatingPoint.BitwiseFloat conv;
			conv.bits = 0U;

			if (x == 0L)
			{
				quat.x = 0f;
			}
			else
			{
				conv.number = x;
				conv.bits -= 0x1F800000U; // exponent -= 63
				quat.x = conv.number;
			}

			if (y == 0L)
			{
				quat.y = 0f;
			}
			else
			{
				conv.number = y;
				conv.bits -= 0x1F800000U; // exponent -= 63
				quat.y = conv.number;
			}

			if (z == 0L)
			{
				quat.z = 0f;
			}
			else
			{
				conv.number = z;
				conv.bits -= 0x1F800000U; // exponent -= 63
				quat.z = conv.number;
			}

			if (w == 0L)
			{
				quat.w = 0f;
			}
			else
			{
				conv.number = w;
				conv.bits -= 0x0F800000U; // exponent -= 31
				quat.w = conv.number;
			}
		}

#if UNITY_EDITOR
		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void CalculateFixedPointSineConstants()
		{
			//double a = System.Math.PI / 2d;
			//double a = 1.5707963267948966192313216916398
			//double a = 1.57079646882; // (π^2 * (π + 1) - 660)/384 + 10/π
			double a = (System.Math.PI * System.Math.PI * (System.Math.PI + 1d) - 660d) / 384d + 10d / System.Math.PI;
			double b, c, d, e;
			ulong ia, ib, ic, id, ie;
			int ieShift;
			CalculateFixedPointSineConstants(a, out b, out c, out d, out e, out ia, out ib, out ic, out id, out ie, out ieShift);

			Debug.LogFormat(
				"private const ulong sinApprox9thOrderA = {0}UL; // {1:F16} as Q1.31\r\n" +
				"private const ulong sinApprox9thOrderB = {2}UL; // {3:F16} as Q0.32\r\n" +
				"private const ulong sinApprox9thOrderC = {4}UL; // {5:F16} as Q0.32\r\n" +
				"private const ulong sinApprox9thOrderD = {6}UL; // {7:F16} as Q0.32\r\n" +
				"private const ulong sinApprox9thOrderE = {8}UL; // {9:F16} as Q0.{10}\r\n",
				ia, a, ib, b, ic, c, id, d, ie, e, ieShift);
		}

		private static void CalculateFixedPointSineConstants(double a, out double b, out double c, out double d, out double e, out ulong ia, out ulong ib, out ulong ic, out ulong id, out ulong ie, out int ieShift)
		{
			// f(x) = ax - bx^3 + cx^5 - dx^7 + ex^9
			b = System.Math.PI * System.Math.PI * System.Math.PI / 48d;
			c = -6d * a + 3d * b + (252d - System.Math.PI * System.Math.PI) / 32d;
			d = 4d * a - 3d * b + 2d * c - 9d / 2d;
			e = 1d - a + b - c + d;

			double eShifted = e;
			ieShift = 32;
			while (eShifted < 0.5d && ieShift < 64)
			{
				++ieShift;
				eShifted *= 2d;
			}

			ia = (ulong)System.Math.Round(a * (1L << 31));
			ib = (ulong)System.Math.Round(b * (1L << 32));
			ic = (ulong)System.Math.Round(c * (1L << 32));
			id = (ulong)System.Math.Round(d * (1L << 32));
			ie = (ulong)System.Math.Round(eShifted * (1L << 32));
		}

		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void TestSineCosine()
		{
			ulong increment = 1000UL; //0x100000000UL / (1UL << 30);

			double minSineError = 0d;
			double maxSineError = 0d;
			double sineErrSqrSum = 0d;
			double minCosineError = 0d;
			double maxCosineError = 0d;
			double cosineErrSqrSum = 0d;
			double count = 0d;

			ulong worstNegSine = 0;
			ulong worstPosSine = 0;
			ulong worstNegCosine = 0;
			ulong worstPosCosine = 0;

			bool investigate = false;

			ulong angle = 0UL;
			ulong angleBits = angle < 0x100000000UL ? (angle << 32) : ((angle << 32) | 0x80000000UL);

			while (true)
			{
				long sine;
				long cosine;

				//   a = ½π ≈ 1.5707557812310000, as Q1.31 ≈ 3373172355UL
				//   b = 3a - 17/4 ≈ 0.6456924812270430, as Q0.32 ≈ 2773228090UL
				//   c = 3a - 2b - 7/2 ≈ 0.0791176187610851, as Q0.32 ≈ 339807585UL
				//   d = 1 - a + b - c ≈ 0.0041809187650426, as Q0.39 ≈ 2298484398UL

				double dAngle = angle * 1.5707963267948966192313216916398 / (double)(1UL << 32);
				if (investigate) Debug.LogFormat("Fixed Angle = 0x{0:X16}, Angle Bits = 0x{1:X16}, Float Angle = {2:F16}", angle, angleBits, dAngle);

				ulong thetaS = angleBits >> 32; // Q0.32
				if (thetaS > 0UL)
				{
					ulong thetaSSqr = (thetaS * thetaS) >> 32; // Q0.32
					ulong n = (thetaSSqr * 2298484398UL) >> 39; // d⋅z², Q0.32 * Q0.39 / 2^39 -> Q0.32
					if (investigate) Debug.LogFormat("Fixed Sine n[0] = {0:F16}", n / (double)(1UL << 32));
					n = (thetaSSqr * (339807585UL - n)) >> 32; // (c - d⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^32 -> Q0.32
					if (investigate) Debug.LogFormat("Fixed Sine n[1] = {0:F16}", n / (double)(1UL << 32));
					n = (thetaSSqr * (2773228090UL - n)) >> 33; // (b - (c - d⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.31
					if (investigate) Debug.LogFormat("Fixed Sine n[2] = {0:F16}", n / (double)(1UL << 31));
					n = (thetaS * (3373172355UL - n)) >> 32; // (a - (b - (c - d⋅z²)⋅z²)⋅z²)⋅z, (Q1.31 - Q0.31) * Q0.32 / 2^32 -> Q0.31
					if (investigate) Debug.LogFormat("Fixed Sine n[3] = {0:F16}", n / (double)(1UL << 31));
					sine = (long)n; // Q0.31

					double thetaSd = angle / (double)(1UL << 32);
					double thetaSSqrd = thetaSd * thetaSd;
					double nd = thetaSSqrd * 0.0041809187650426; // d⋅z², Q0.32 * Q0.34 / 2^34 -> Q0.32
					if (investigate) Debug.LogFormat("Float Sine n[0] = {0:F16}", nd);
					nd = thetaSSqrd * (0.0791176187610851 - nd); // (c - d⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^32 -> Q0.32
					if (investigate) Debug.LogFormat("Float Sine n[1] = {0:F16}", nd);
					nd = thetaSSqrd * (0.6456924812270430 - nd); // (b - (c - d⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.31
					if (investigate) Debug.LogFormat("Float Sine n[2] = {0:F16}", nd);
					nd = thetaSd * (1.5707557812310000 - nd); // (a - (b - (c - d⋅z²)⋅z²)⋅z²)⋅z, (Q1.31 - Q0.31) * Q0.32 / 2^32 -> Q0.31
					if (investigate) Debug.LogFormat("Float Sine n[3] = {0:F16}", nd);

					ulong thetaC = 0x100000000UL - thetaS; // Q0.32
					ulong thetaCSqr = (thetaC * thetaC) >> 32; // Q0.32
					n = (thetaCSqr * 2298484398UL) >> 39; // d⋅z², Q0.32 * Q0.34 / 2^39 -> Q0.32
					if (investigate) Debug.LogFormat("Fixed Cosine n[0] = {0:F16}", n / (double)(1UL << 32));
					n = (thetaCSqr * (339807585UL - n)) >> 32; // (c - d⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^32 -> Q0.32
					if (investigate) Debug.LogFormat("Fixed Cosine n[1] = {0:F16}", n / (double)(1UL << 32));
					n = (thetaCSqr * (2773228090UL - n)) >> 33; // (b - (c - d⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.31
					if (investigate) Debug.LogFormat("Fixed Cosine n[2] = {0:F16}", n / (double)(1UL << 31));
					n = (thetaC * (3373172355UL - n)) >> 32; // (a - (b - (c - d⋅z²)⋅z²)⋅z²)⋅z, (Q1.31 - Q0.31) * Q0.32 / 2^32 -> Q0.31
					if (investigate) Debug.LogFormat("Fixed Cosine n[3] = {0:F16}", n / (double)(1UL << 31));
					cosine = (long)n; // Q0.31

					double thetaCd = 1d - thetaSd;
					double thetaCSqrd = thetaCd * thetaCd;
					nd = thetaCSqrd * 0.0041809187650426; // d⋅z², Q0.32 * Q0.34 / 2^34 -> Q0.32
					if (investigate) Debug.LogFormat("Float Cosine n[0] = {0:F16}", nd);
					nd = thetaCSqrd * (0.0791176187610851 - nd); // (c - d⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^32 -> Q0.32
					if (investigate) Debug.LogFormat("Float Cosine n[1] = {0:F16}", nd);
					nd = thetaCSqrd * (0.6456924812270430 - nd); // (b - (c - d⋅z²)⋅z²)⋅z², (Q0.32 - Q0.32) * Q0.32 / 2^33 -> Q0.31
					if (investigate) Debug.LogFormat("Float Cosine n[2] = {0:F16}", nd);
					nd = thetaCd * (1.5707557812310000 - nd); // (a - (b - (c - d⋅z²)⋅z²)⋅z²)⋅z, (Q1.31 - Q0.31) * Q0.32 / 2^32 -> Q0.31
					if (investigate) Debug.LogFormat("Float Cosine n[3] = {0:F16}", nd);
				}
				else
				{
					sine = 0L;
					cosine = 0x80000000L;
				}

				if ((angleBits & 0x80000000UL) == 0UL)
				{
					// If the angle is in the first quadrant of the trig cycle, sine stays as it is and w is simply cosine.
				}
				else
				{
					// If the angle is in the second quadrant of the trig cycle, sine and cosine swap, and w is the negative of the swapped cosine.
					long swapTemp = cosine;
					cosine = -sine;
					sine = swapTemp;
				}

				double dSine = sine / (double)(1UL << 31);
				double dCosine = cosine / (double)(1UL << 31);

				if (investigate) Debug.LogFormat("Approximate Sine = {0:F16}, Actual Sine = {1:F16}", dSine, System.Math.Sin(dAngle));
				if (investigate) Debug.LogFormat("Approximate Cosine = {0:F16}, Actual Cosine = {1:F16}", dCosine, System.Math.Cos(dAngle));

				double sineError = dSine - System.Math.Sin(dAngle);
				double cosineError = dCosine - System.Math.Cos(dAngle);

				if (sineError <= minSineError)
				{
					minSineError = sineError;
					worstNegSine = angle;
				}
				if (sineError >= maxSineError)
				{
					maxSineError = sineError;
					worstPosSine = angle;
				}
				sineErrSqrSum += sineError * sineError;
				if (cosineError <= minCosineError)
				{
					minCosineError = cosineError;
					worstNegCosine = angle;
				}
				if (cosineError >= maxCosineError)
				{
					maxCosineError = cosineError;
					worstPosCosine = angle;
				}
				cosineErrSqrSum += cosineError * cosineError;
				count += 1d;

				if (investigate) break;

				if (angle < 0xFFFFFFFFUL)
				{
					angle += increment;
					if (angle > 0xFFFFFFFFUL)
					{
						angle = 0xFFFFFFFFUL;
					}
					angleBits = angle << 32;
				}
				else if (angle == 0xFFFFFFFFUL)
				{
					angle = 0x100000000UL;
					angleBits = (angle << 32) | 0x80000000UL;
				}
				else if (angle < 0x1FFFFFFFFUL)
				{
					angle += increment;
					if (angle > 0x1FFFFFFFFUL)
					{
						angle = 0x1FFFFFFFFUL;
					}
					angleBits = (angle << 32) | 0x80000000UL;
				}
				else
				{
					break;
				}
			}

			//Sine Error:  Min: -0.0000000006657173, Max: 0.0000084876841161, StdDev: 0.0000000000233876, Worst Neg Angle: 4290793000, Worst Pos Angle: 6113584296
			//Cosine Error:  Min: -0.0000084876772696, Max: 0.0000084876841161, StdDev: 0.0000000000233876, Worst Neg Angle: 6769701296, Worst Pos Angle: 1818617000

			Debug.LogFormat("Sine Error:  Min: {0:F16}, Max: {1:F16}, StdDev: {2:F16}, Worst Neg Angle: {3}, Worst Pos Angle: {4}", minSineError, maxSineError, sineErrSqrSum / count, worstNegSine, worstPosSine);
			Debug.LogFormat("Cosine Error:  Min: {0:F16}, Max: {1:F16}, StdDev: {2:F16}, Worst Neg Angle: {3}, Worst Pos Angle: {4}", minCosineError, maxCosineError, cosineErrSqrSum / count, worstNegCosine, worstPosCosine);
		}

		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void TestRotation()
		{
			var r = XorShift128Plus.Create();

			float negError0 = 0f;
			float posError0 = 0f;
			float minError0 = 0f;
			float maxError0 = 0f;

			float negError1 = 0f;
			float posError1 = 0f;
			float minError1 = 0f;
			float maxError1 = 0f;

			int iterations = 1000000;

			for (int i = 0; i < iterations; ++i)
			{
				Quaternion q;
				r.Rotation(out q);
				Vector4 qv = new Vector4(q.x, q.y, q.z, q.w);
				float e = qv.magnitude - 1f;
				if (e > 0f)
				{
					posError0 += e;
					maxError0 = Mathf.Max(maxError0, e);
				}
				else if (e < 0f)
				{
					negError0 += e;
					minError0 = Mathf.Min(minError0, e);
				}

				q = Random.rotationUniform;
				qv = new Vector4(q.x, q.y, q.z, q.w);
				e = qv.magnitude - 1f;
				if (e > 0f)
				{
					posError1 += e;
					maxError1 = Mathf.Max(maxError1, e);
				}
				else if (e < 0f)
				{
					negError1 += e;
					minError1 = Mathf.Min(minError1, e);
				}
			}

			Debug.LogFormat("MakeIt.Rotation: Neg: {0:F16}, Pos: {1:F16}, Min: {2:F16}, Max: {3:F16}", negError0 / iterations, posError0 / iterations, minError0, maxError0);
			Debug.LogFormat("Unity.rotation:  Neg: {0:F16}, Pos: {1:F16}, Min: {2:F16}, Max: {3:F16}", negError1 / iterations, posError1 / iterations, minError1, maxError1);
		}
#endif

#endregion
	}
}
