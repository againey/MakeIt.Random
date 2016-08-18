/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

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
		public static Vector2 UnitVector2(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var angle = random.HalfOpenRange(0f, Mathf.PI * 2f);
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
#else
			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			return new Vector2((uSqr - vSqr) / uvSqr, 2f * u * v / uvSqr);
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
			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			float t = 2f * Mathf.Sqrt(1f - uvSqr);
			return new Vector3(u * t, v * t, 1f - 2f * uvSqr);
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

#if UNITY_EDITOR
		//[UnityEditor.Callbacks.DidReloadScripts]
		private static void CalculatePointWithinCircleTables()
		{
			var sb = new System.Text.StringBuilder();

			int bucketBits = 7;
			int buckets = 1 << bucketBits;
			int diameterBits = 23;
			int radiusBits = diameterBits - 1;
			long diameter = (1 << diameterBits);
			long radius = (1 << radiusBits);
			long radiusSquared = radius * radius;
			long bucketSpan = 1L << (diameterBits - bucketBits);

			sb.AppendLine("<b>Lookup Table Values for PointWithinCircle()</b>");
			sb.AppendFormat("bucketBits = {0}\n", bucketBits);
			sb.AppendFormat("buckets = {0}\n", buckets);
			sb.AppendFormat("diameterBits = {0}\n", diameterBits);
			sb.AppendFormat("radiusBits = {0}\n", radiusBits);
			sb.AppendFormat("diameter = 0x{0:X8}\n", (uint)diameter);
			sb.AppendFormat("radius = 0x{0:X8}\n", (uint)radius);
			sb.AppendFormat("bucketSpan = 0x{0:X8}\n", (uint)bucketSpan);
			sb.AppendFormat("radiusSquared = 0x{0:X16}\n", radiusSquared);
			sb.AppendLine();

			long y = -radius + bucketSpan;

			uint[] minThresholds = new uint[buckets + 1];
			uint[] maxThresholds = new uint[buckets + 1];

			minThresholds[0] = (uint)radius;
			maxThresholds[0] = (uint)radius;

			int i = 1;

			while (i < buckets)
			{
				long ySquared = y * y;
				long xSquared = radiusSquared - ySquared;
				long x = 1L << ((Detail.DeBruijnLookup.GetBitCountForRangeMax(xSquared) + 1) / 2);
				while (ySquared + x * x > radiusSquared)
				{
					--x;
				}
				uint uy = (uint)(radius + y);
				uint uxMin = (uint)(radius - x);
				uint uxMax = (uint)(radius + x);
				uint uxRange = uxMax - uxMin + 1U;
				uint uxMask = Detail.DeBruijnLookup.GetBitMaskForRangeSize(uxRange);
				float fy = Detail.FloatingPoint.BitsToFloatC1O2(uy);
				float fxMin = Detail.FloatingPoint.BitsToFloatC1O2(uxMin);
				float fxMax = Detail.FloatingPoint.BitsToFloatC1O2(uxMax);
				float fxOffset = Detail.FloatingPoint.BitsToFloatC1O2(uxRange) + 1f;
				//sb.AppendFormat("0x{0:X8}, 0x{1:X8}, 0x{2:X8} ({3:F8}, {4:F8}, {5:F8})\n", uy, uxMin, uxMax, fy, fxMin, fxMax);
				minThresholds[i] = uxMin;
				maxThresholds[i] = uxMax;
				y += bucketSpan;
				++i;
			}

			minThresholds[buckets] = (uint)radius;
			maxThresholds[buckets] = (uint)radius;

			sb.Append("private static uint[,] _pointWithinCircle_InnerThresholds = new uint[,]\n{\n");
			int j = 0;
			for (i = 0; i < buckets / 2; ++i, ++j)
			{
				sb.AppendFormat("{0}{{ 0x{1:X8}, 0x{2:X8} }},{3}", j % 16 == 0 ? "\t" : "", minThresholds[i], maxThresholds[i], j % 16 == 15 ? "\n" : " ");
			}
			for (i = i + 1; i <= buckets; ++i, ++j)
			{
				sb.AppendFormat("{0}{{ 0x{1:X8}, 0x{2:X8} }},{3}", j % 16 == 0 ? "\t" : "", minThresholds[i], maxThresholds[i], j % 16 == 15 ? "\n" : " ");
			}
			sb.Append("};\n\n");

			sb.Append("private static uint[,] _pointWithinCircle_OuterThresholds = new uint[,]\n{\n");
			j = 0;
			for (i = 1; i <= buckets / 2; ++i, ++j)
			{
				sb.AppendFormat("{0}{{ 0x{1:X8}, 0x{2:X8} }},{3}", j % 16 == 0 ? "\t" : "", minThresholds[i], maxThresholds[i], j % 16 == 15 ? "\n" : " ");
			}
			for (i = i - 1; i < buckets; ++i, ++j)
			{
				sb.AppendFormat("{0}{{ 0x{1:X8}, 0x{2:X8} }},{3}", j % 16 == 0 ? "\t" : "", minThresholds[i], maxThresholds[i], j % 16 == 15 ? "\n" : " ");
			}
			sb.Append("};\n");

			Debug.Log(sb.ToString());
		}
#endif

#if false
		private static uint[,] _pointWithinCircle_InnerThresholds = new uint[,]
		{
			{ 0x00400000, 0x00400000 }, { 0x00210422, 0x005EFBDE }, { 0x0015AB01, 0x006A54FF }, { 0x000E0A3F, 0x0071F5C1 }, { 0x0008930B, 0x00776CF5 }, { 0x0004ABA1, 0x007B545F }, { 0x00020843, 0x007DF7BD }, { 0x00008082, 0x007F7F7E }, { 0x00008082, 0x007F7F7E }, { 0x00020843, 0x007DF7BD }, { 0x0004ABA1, 0x007B545F }, { 0x0008930B, 0x00776CF5 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x0015AB01, 0x006A54FF }, { 0x00210422, 0x005EFBDE }, { 0x00400000, 0x00400000 },
		};

		private static uint[,] _pointWithinCircle_OuterThresholds = new uint[,]
		{
			{ 0x00210422, 0x005EFBDE }, { 0x0015AB01, 0x006A54FF }, { 0x000E0A3F, 0x0071F5C1 }, { 0x0008930B, 0x00776CF5 }, { 0x0004ABA1, 0x007B545F }, { 0x00020843, 0x007DF7BD }, { 0x00008082, 0x007F7F7E }, { 0x00000000, 0x00800000 }, { 0x00000000, 0x00800000 }, { 0x00008082, 0x007F7F7E }, { 0x00020843, 0x007DF7BD }, { 0x0004ABA1, 0x007B545F }, { 0x0008930B, 0x00776CF5 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x0015AB01, 0x006A54FF }, { 0x00210422, 0x005EFBDE },
		};

		private const int _pointWithinCircle_BucketShift = 19;
		private const uint _pointWithinCircle_BucketMask = 0x0FU;
#elif false
		private static uint[,] _pointWithinCircle_InnerThresholds = new uint[,]
		{
			{ 0x00400000, 0x00400000 }, { 0x0029BA9C, 0x00564564 }, { 0x00210422, 0x005EFBDE }, { 0x001AB0C4, 0x00654F3C }, { 0x0015AB01, 0x006A54FF }, { 0x00118632, 0x006E79CE }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000B15C1, 0x0074EA3F }, { 0x0008930B, 0x00776CF5 }, { 0x00067335, 0x00798CCB }, { 0x0004ABA1, 0x007B545F }, { 0x0003348D, 0x007CCB73 }, { 0x00020843, 0x007DF7BD }, { 0x00012294, 0x007EDD6C }, { 0x00008082, 0x007F7F7E }, { 0x00002009, 0x007FDFF7 },
			{ 0x00002009, 0x007FDFF7 }, { 0x00008082, 0x007F7F7E }, { 0x00012294, 0x007EDD6C }, { 0x00020843, 0x007DF7BD }, { 0x0003348D, 0x007CCB73 }, { 0x0004ABA1, 0x007B545F }, { 0x00067335, 0x00798CCB }, { 0x0008930B, 0x00776CF5 }, { 0x000B15C1, 0x0074EA3F }, { 0x000E0A3F, 0x0071F5C1 }, { 0x00118632, 0x006E79CE }, { 0x0015AB01, 0x006A54FF }, { 0x001AB0C4, 0x00654F3C }, { 0x00210422, 0x005EFBDE }, { 0x0029BA9C, 0x00564564 }, { 0x00400000, 0x00400000 },
		};

		private static uint[,] _pointWithinCircle_OuterThresholds = new uint[,]
		{
			{ 0x0029BA9C, 0x00564564 }, { 0x00210422, 0x005EFBDE }, { 0x001AB0C4, 0x00654F3C }, { 0x0015AB01, 0x006A54FF }, { 0x00118632, 0x006E79CE }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000B15C1, 0x0074EA3F }, { 0x0008930B, 0x00776CF5 }, { 0x00067335, 0x00798CCB }, { 0x0004ABA1, 0x007B545F }, { 0x0003348D, 0x007CCB73 }, { 0x00020843, 0x007DF7BD }, { 0x00012294, 0x007EDD6C }, { 0x00008082, 0x007F7F7E }, { 0x00002009, 0x007FDFF7 }, { 0x00000000, 0x00800000 },
			{ 0x00000000, 0x00800000 }, { 0x00002009, 0x007FDFF7 }, { 0x00008082, 0x007F7F7E }, { 0x00012294, 0x007EDD6C }, { 0x00020843, 0x007DF7BD }, { 0x0003348D, 0x007CCB73 }, { 0x0004ABA1, 0x007B545F }, { 0x00067335, 0x00798CCB }, { 0x0008930B, 0x00776CF5 }, { 0x000B15C1, 0x0074EA3F }, { 0x000E0A3F, 0x0071F5C1 }, { 0x00118632, 0x006E79CE }, { 0x0015AB01, 0x006A54FF }, { 0x001AB0C4, 0x00654F3C }, { 0x00210422, 0x005EFBDE }, { 0x0029BA9C, 0x00564564 },
		};

		private const int _pointWithinCircle_BucketShift = 18;
		private const uint _pointWithinCircle_BucketMask = 0x1FU;
#elif false
		private static uint[,] _pointWithinCircle_InnerThresholds = new uint[,]
		{
			{ 0x00400000, 0x00400000 }, { 0x00302021, 0x004FDFDF }, { 0x0029BA9C, 0x00564564 }, { 0x0024F1CB, 0x005B0E35 }, { 0x00210422, 0x005EFBDE }, { 0x001DA61D, 0x006259E3 }, { 0x001AB0C4, 0x00654F3C }, { 0x00180CCF, 0x0067F331 }, { 0x0015AB01, 0x006A54FF }, { 0x001380B9, 0x006C7F47 }, { 0x00118632, 0x006E79CE }, { 0x000FB590, 0x00704A70 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000C80A0, 0x00737F60 }, { 0x000B15C1, 0x0074EA3F }, { 0x0009C73B, 0x007638C5 },
			{ 0x0008930B, 0x00776CF5 }, { 0x00077782, 0x0078887E }, { 0x00067335, 0x00798CCB }, { 0x000584ED, 0x007A7B13 }, { 0x0004ABA1, 0x007B545F }, { 0x0003E66C, 0x007C1994 }, { 0x0003348D, 0x007CCB73 }, { 0x0002955A, 0x007D6AA6 }, { 0x00020843, 0x007DF7BD }, { 0x00018CCF, 0x007E7331 }, { 0x00012294, 0x007EDD6C }, { 0x0000C93D, 0x007F36C3 }, { 0x00008082, 0x007F7F7E }, { 0x00004829, 0x007FB7D7 }, { 0x00002009, 0x007FDFF7 }, { 0x00000801, 0x007FF7FF },
			{ 0x00000801, 0x007FF7FF }, { 0x00002009, 0x007FDFF7 }, { 0x00004829, 0x007FB7D7 }, { 0x00008082, 0x007F7F7E }, { 0x0000C93D, 0x007F36C3 }, { 0x00012294, 0x007EDD6C }, { 0x00018CCF, 0x007E7331 }, { 0x00020843, 0x007DF7BD }, { 0x0002955A, 0x007D6AA6 }, { 0x0003348D, 0x007CCB73 }, { 0x0003E66C, 0x007C1994 }, { 0x0004ABA1, 0x007B545F }, { 0x000584ED, 0x007A7B13 }, { 0x00067335, 0x00798CCB }, { 0x00077782, 0x0078887E }, { 0x0008930B, 0x00776CF5 },
			{ 0x0009C73B, 0x007638C5 }, { 0x000B15C1, 0x0074EA3F }, { 0x000C80A0, 0x00737F60 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000FB590, 0x00704A70 }, { 0x00118632, 0x006E79CE }, { 0x001380B9, 0x006C7F47 }, { 0x0015AB01, 0x006A54FF }, { 0x00180CCF, 0x0067F331 }, { 0x001AB0C4, 0x00654F3C }, { 0x001DA61D, 0x006259E3 }, { 0x00210422, 0x005EFBDE }, { 0x0024F1CB, 0x005B0E35 }, { 0x0029BA9C, 0x00564564 }, { 0x00302021, 0x004FDFDF }, { 0x00400000, 0x00400000 },
		};

		private static uint[,] _pointWithinCircle_OuterThresholds = new uint[,]
		{
			{ 0x00302021, 0x004FDFDF }, { 0x0029BA9C, 0x00564564 }, { 0x0024F1CB, 0x005B0E35 }, { 0x00210422, 0x005EFBDE }, { 0x001DA61D, 0x006259E3 }, { 0x001AB0C4, 0x00654F3C }, { 0x00180CCF, 0x0067F331 }, { 0x0015AB01, 0x006A54FF }, { 0x001380B9, 0x006C7F47 }, { 0x00118632, 0x006E79CE }, { 0x000FB590, 0x00704A70 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000C80A0, 0x00737F60 }, { 0x000B15C1, 0x0074EA3F }, { 0x0009C73B, 0x007638C5 }, { 0x0008930B, 0x00776CF5 },
			{ 0x00077782, 0x0078887E }, { 0x00067335, 0x00798CCB }, { 0x000584ED, 0x007A7B13 }, { 0x0004ABA1, 0x007B545F }, { 0x0003E66C, 0x007C1994 }, { 0x0003348D, 0x007CCB73 }, { 0x0002955A, 0x007D6AA6 }, { 0x00020843, 0x007DF7BD }, { 0x00018CCF, 0x007E7331 }, { 0x00012294, 0x007EDD6C }, { 0x0000C93D, 0x007F36C3 }, { 0x00008082, 0x007F7F7E }, { 0x00004829, 0x007FB7D7 }, { 0x00002009, 0x007FDFF7 }, { 0x00000801, 0x007FF7FF }, { 0x00000000, 0x00800000 },
			{ 0x00000000, 0x00800000 }, { 0x00000801, 0x007FF7FF }, { 0x00002009, 0x007FDFF7 }, { 0x00004829, 0x007FB7D7 }, { 0x00008082, 0x007F7F7E }, { 0x0000C93D, 0x007F36C3 }, { 0x00012294, 0x007EDD6C }, { 0x00018CCF, 0x007E7331 }, { 0x00020843, 0x007DF7BD }, { 0x0002955A, 0x007D6AA6 }, { 0x0003348D, 0x007CCB73 }, { 0x0003E66C, 0x007C1994 }, { 0x0004ABA1, 0x007B545F }, { 0x000584ED, 0x007A7B13 }, { 0x00067335, 0x00798CCB }, { 0x00077782, 0x0078887E },
			{ 0x0008930B, 0x00776CF5 }, { 0x0009C73B, 0x007638C5 }, { 0x000B15C1, 0x0074EA3F }, { 0x000C80A0, 0x00737F60 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000FB590, 0x00704A70 }, { 0x00118632, 0x006E79CE }, { 0x001380B9, 0x006C7F47 }, { 0x0015AB01, 0x006A54FF }, { 0x00180CCF, 0x0067F331 }, { 0x001AB0C4, 0x00654F3C }, { 0x001DA61D, 0x006259E3 }, { 0x00210422, 0x005EFBDE }, { 0x0024F1CB, 0x005B0E35 }, { 0x0029BA9C, 0x00564564 }, { 0x00302021, 0x004FDFDF },
		};

		private const int _pointWithinCircle_BucketShift = 17;
		private const uint _pointWithinCircle_BucketMask = 0x3FU;
#elif true
		private static uint[,] _pointWithinCircle_InnerThresholds = new uint[,]
		{
			{ 0x00400000, 0x00400000 }, { 0x0034BB07, 0x004B44F9 }, { 0x00302021, 0x004FDFDF }, { 0x002CA295, 0x00535D6B }, { 0x0029BA9C, 0x00564564 }, { 0x00273369, 0x0058CC97 }, { 0x0024F1CB, 0x005B0E35 }, { 0x0022E591, 0x005D1A6F }, { 0x00210422, 0x005EFBDE }, { 0x001F461C, 0x0060B9E4 }, { 0x001DA61D, 0x006259E3 }, { 0x001C200F, 0x0063DFF1 }, { 0x001AB0C4, 0x00654F3C }, { 0x001955B4, 0x0066AA4C }, { 0x00180CCF, 0x0067F331 }, { 0x0016D463, 0x00692B9D },
			{ 0x0015AB01, 0x006A54FF }, { 0x00148F76, 0x006B708A }, { 0x001380B9, 0x006C7F47 }, { 0x00127DE4, 0x006D821C }, { 0x00118632, 0x006E79CE }, { 0x001098F4, 0x006F670C }, { 0x000FB590, 0x00704A70 }, { 0x000EDB7C, 0x00712484 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000D416C, 0x0072BE94 }, { 0x000C80A0, 0x00737F60 }, { 0x000BC782, 0x0074387E }, { 0x000B15C1, 0x0074EA3F }, { 0x000A6B15, 0x007594EB }, { 0x0009C73B, 0x007638C5 }, { 0x000929F5, 0x0076D60B },
			{ 0x0008930B, 0x00776CF5 }, { 0x0008024A, 0x0077FDB6 }, { 0x00077782, 0x0078887E }, { 0x0006F289, 0x00790D77 }, { 0x00067335, 0x00798CCB }, { 0x0005F962, 0x007A069E }, { 0x000584ED, 0x007A7B13 }, { 0x000515B6, 0x007AEA4A }, { 0x0004ABA1, 0x007B545F }, { 0x00044690, 0x007BB970 }, { 0x0003E66C, 0x007C1994 }, { 0x00038B1D, 0x007C74E3 }, { 0x0003348D, 0x007CCB73 }, { 0x0002E2A7, 0x007D1D59 }, { 0x0002955A, 0x007D6AA6 }, { 0x00024C93, 0x007DB36D },
			{ 0x00020843, 0x007DF7BD }, { 0x0001C85C, 0x007E37A4 }, { 0x00018CCF, 0x007E7331 }, { 0x00015590, 0x007EAA70 }, { 0x00012294, 0x007EDD6C }, { 0x0000F3D1, 0x007F0C2F }, { 0x0000C93D, 0x007F36C3 }, { 0x0000A2D0, 0x007F5D30 }, { 0x00008082, 0x007F7F7E }, { 0x0000624C, 0x007F9DB4 }, { 0x00004829, 0x007FB7D7 }, { 0x00003214, 0x007FCDEC }, { 0x00002009, 0x007FDFF7 }, { 0x00001203, 0x007FEDFD }, { 0x00000801, 0x007FF7FF }, { 0x00000201, 0x007FFDFF },
			{ 0x00000201, 0x007FFDFF }, { 0x00000801, 0x007FF7FF }, { 0x00001203, 0x007FEDFD }, { 0x00002009, 0x007FDFF7 }, { 0x00003214, 0x007FCDEC }, { 0x00004829, 0x007FB7D7 }, { 0x0000624C, 0x007F9DB4 }, { 0x00008082, 0x007F7F7E }, { 0x0000A2D0, 0x007F5D30 }, { 0x0000C93D, 0x007F36C3 }, { 0x0000F3D1, 0x007F0C2F }, { 0x00012294, 0x007EDD6C }, { 0x00015590, 0x007EAA70 }, { 0x00018CCF, 0x007E7331 }, { 0x0001C85C, 0x007E37A4 }, { 0x00020843, 0x007DF7BD },
			{ 0x00024C93, 0x007DB36D }, { 0x0002955A, 0x007D6AA6 }, { 0x0002E2A7, 0x007D1D59 }, { 0x0003348D, 0x007CCB73 }, { 0x00038B1D, 0x007C74E3 }, { 0x0003E66C, 0x007C1994 }, { 0x00044690, 0x007BB970 }, { 0x0004ABA1, 0x007B545F }, { 0x000515B6, 0x007AEA4A }, { 0x000584ED, 0x007A7B13 }, { 0x0005F962, 0x007A069E }, { 0x00067335, 0x00798CCB }, { 0x0006F289, 0x00790D77 }, { 0x00077782, 0x0078887E }, { 0x0008024A, 0x0077FDB6 }, { 0x0008930B, 0x00776CF5 },
			{ 0x000929F5, 0x0076D60B }, { 0x0009C73B, 0x007638C5 }, { 0x000A6B15, 0x007594EB }, { 0x000B15C1, 0x0074EA3F }, { 0x000BC782, 0x0074387E }, { 0x000C80A0, 0x00737F60 }, { 0x000D416C, 0x0072BE94 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000EDB7C, 0x00712484 }, { 0x000FB590, 0x00704A70 }, { 0x001098F4, 0x006F670C }, { 0x00118632, 0x006E79CE }, { 0x00127DE4, 0x006D821C }, { 0x001380B9, 0x006C7F47 }, { 0x00148F76, 0x006B708A }, { 0x0015AB01, 0x006A54FF },
			{ 0x0016D463, 0x00692B9D }, { 0x00180CCF, 0x0067F331 }, { 0x001955B4, 0x0066AA4C }, { 0x001AB0C4, 0x00654F3C }, { 0x001C200F, 0x0063DFF1 }, { 0x001DA61D, 0x006259E3 }, { 0x001F461C, 0x0060B9E4 }, { 0x00210422, 0x005EFBDE }, { 0x0022E591, 0x005D1A6F }, { 0x0024F1CB, 0x005B0E35 }, { 0x00273369, 0x0058CC97 }, { 0x0029BA9C, 0x00564564 }, { 0x002CA295, 0x00535D6B }, { 0x00302021, 0x004FDFDF }, { 0x0034BB07, 0x004B44F9 }, { 0x00400000, 0x00400000 },
		};

		private static uint[,] _pointWithinCircle_OuterThresholds = new uint[,]
		{
			{ 0x0034BB07, 0x004B44F9 }, { 0x00302021, 0x004FDFDF }, { 0x002CA295, 0x00535D6B }, { 0x0029BA9C, 0x00564564 }, { 0x00273369, 0x0058CC97 }, { 0x0024F1CB, 0x005B0E35 }, { 0x0022E591, 0x005D1A6F }, { 0x00210422, 0x005EFBDE }, { 0x001F461C, 0x0060B9E4 }, { 0x001DA61D, 0x006259E3 }, { 0x001C200F, 0x0063DFF1 }, { 0x001AB0C4, 0x00654F3C }, { 0x001955B4, 0x0066AA4C }, { 0x00180CCF, 0x0067F331 }, { 0x0016D463, 0x00692B9D }, { 0x0015AB01, 0x006A54FF },
			{ 0x00148F76, 0x006B708A }, { 0x001380B9, 0x006C7F47 }, { 0x00127DE4, 0x006D821C }, { 0x00118632, 0x006E79CE }, { 0x001098F4, 0x006F670C }, { 0x000FB590, 0x00704A70 }, { 0x000EDB7C, 0x00712484 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000D416C, 0x0072BE94 }, { 0x000C80A0, 0x00737F60 }, { 0x000BC782, 0x0074387E }, { 0x000B15C1, 0x0074EA3F }, { 0x000A6B15, 0x007594EB }, { 0x0009C73B, 0x007638C5 }, { 0x000929F5, 0x0076D60B }, { 0x0008930B, 0x00776CF5 },
			{ 0x0008024A, 0x0077FDB6 }, { 0x00077782, 0x0078887E }, { 0x0006F289, 0x00790D77 }, { 0x00067335, 0x00798CCB }, { 0x0005F962, 0x007A069E }, { 0x000584ED, 0x007A7B13 }, { 0x000515B6, 0x007AEA4A }, { 0x0004ABA1, 0x007B545F }, { 0x00044690, 0x007BB970 }, { 0x0003E66C, 0x007C1994 }, { 0x00038B1D, 0x007C74E3 }, { 0x0003348D, 0x007CCB73 }, { 0x0002E2A7, 0x007D1D59 }, { 0x0002955A, 0x007D6AA6 }, { 0x00024C93, 0x007DB36D }, { 0x00020843, 0x007DF7BD },
			{ 0x0001C85C, 0x007E37A4 }, { 0x00018CCF, 0x007E7331 }, { 0x00015590, 0x007EAA70 }, { 0x00012294, 0x007EDD6C }, { 0x0000F3D1, 0x007F0C2F }, { 0x0000C93D, 0x007F36C3 }, { 0x0000A2D0, 0x007F5D30 }, { 0x00008082, 0x007F7F7E }, { 0x0000624C, 0x007F9DB4 }, { 0x00004829, 0x007FB7D7 }, { 0x00003214, 0x007FCDEC }, { 0x00002009, 0x007FDFF7 }, { 0x00001203, 0x007FEDFD }, { 0x00000801, 0x007FF7FF }, { 0x00000201, 0x007FFDFF }, { 0x00000000, 0x00800000 },
			{ 0x00000000, 0x00800000 }, { 0x00000201, 0x007FFDFF }, { 0x00000801, 0x007FF7FF }, { 0x00001203, 0x007FEDFD }, { 0x00002009, 0x007FDFF7 }, { 0x00003214, 0x007FCDEC }, { 0x00004829, 0x007FB7D7 }, { 0x0000624C, 0x007F9DB4 }, { 0x00008082, 0x007F7F7E }, { 0x0000A2D0, 0x007F5D30 }, { 0x0000C93D, 0x007F36C3 }, { 0x0000F3D1, 0x007F0C2F }, { 0x00012294, 0x007EDD6C }, { 0x00015590, 0x007EAA70 }, { 0x00018CCF, 0x007E7331 }, { 0x0001C85C, 0x007E37A4 },
			{ 0x00020843, 0x007DF7BD }, { 0x00024C93, 0x007DB36D }, { 0x0002955A, 0x007D6AA6 }, { 0x0002E2A7, 0x007D1D59 }, { 0x0003348D, 0x007CCB73 }, { 0x00038B1D, 0x007C74E3 }, { 0x0003E66C, 0x007C1994 }, { 0x00044690, 0x007BB970 }, { 0x0004ABA1, 0x007B545F }, { 0x000515B6, 0x007AEA4A }, { 0x000584ED, 0x007A7B13 }, { 0x0005F962, 0x007A069E }, { 0x00067335, 0x00798CCB }, { 0x0006F289, 0x00790D77 }, { 0x00077782, 0x0078887E }, { 0x0008024A, 0x0077FDB6 },
			{ 0x0008930B, 0x00776CF5 }, { 0x000929F5, 0x0076D60B }, { 0x0009C73B, 0x007638C5 }, { 0x000A6B15, 0x007594EB }, { 0x000B15C1, 0x0074EA3F }, { 0x000BC782, 0x0074387E }, { 0x000C80A0, 0x00737F60 }, { 0x000D416C, 0x0072BE94 }, { 0x000E0A3F, 0x0071F5C1 }, { 0x000EDB7C, 0x00712484 }, { 0x000FB590, 0x00704A70 }, { 0x001098F4, 0x006F670C }, { 0x00118632, 0x006E79CE }, { 0x00127DE4, 0x006D821C }, { 0x001380B9, 0x006C7F47 }, { 0x00148F76, 0x006B708A },
			{ 0x0015AB01, 0x006A54FF }, { 0x0016D463, 0x00692B9D }, { 0x00180CCF, 0x0067F331 }, { 0x001955B4, 0x0066AA4C }, { 0x001AB0C4, 0x00654F3C }, { 0x001C200F, 0x0063DFF1 }, { 0x001DA61D, 0x006259E3 }, { 0x001F461C, 0x0060B9E4 }, { 0x00210422, 0x005EFBDE }, { 0x0022E591, 0x005D1A6F }, { 0x0024F1CB, 0x005B0E35 }, { 0x00273369, 0x0058CC97 }, { 0x0029BA9C, 0x00564564 }, { 0x002CA295, 0x00535D6B }, { 0x00302021, 0x004FDFDF }, { 0x0034BB07, 0x004B44F9 },
		};

		private const int _pointWithinCircle_BucketShift = 16;
		private const uint _pointWithinCircle_BucketMask = 0x7FU;
#else
		private static uint[,] _pointWithinCircle_InnerThresholds = new uint[,]
		{
			{ 0x00400000, 0x00400000 }, { 0x00380402, 0x0047FBFE }, { 0x0034BB07, 0x004B44F9 }, { 0x0032399C, 0x004DC664 }, { 0x00302021, 0x004FDFDF }, { 0x002E497A, 0x0051B686 }, { 0x002CA295, 0x00535D6B }, { 0x002B2019, 0x0054DFE7 }, { 0x0029BA9C, 0x00564564 }, { 0x00286CF8, 0x00579308 }, { 0x00273369, 0x0058CC97 }, { 0x00260B17, 0x0059F4E9 }, { 0x0024F1CB, 0x005B0E35 }, { 0x0023E5C4, 0x005C1A3C }, { 0x0022E591, 0x005D1A6F }, { 0x0021F005, 0x005E0FFB },
			{ 0x00210422, 0x005EFBDE }, { 0x00202112, 0x005FDEEE }, { 0x001F461C, 0x0060B9E4 }, { 0x001E72A4, 0x00618D5C }, { 0x001DA61D, 0x006259E3 }, { 0x001CE00F, 0x00631FF1 }, { 0x001C200F, 0x0063DFF1 }, { 0x001B65BD, 0x00649A43 }, { 0x001AB0C4, 0x00654F3C }, { 0x001A00D8, 0x0065FF28 }, { 0x001955B4, 0x0066AA4C }, { 0x0018AF19, 0x006750E7 }, { 0x00180CCF, 0x0067F331 }, { 0x00176EA2, 0x0068915E }, { 0x0016D463, 0x00692B9D }, { 0x00163DE5, 0x0069C21B },
			{ 0x0015AB01, 0x006A54FF }, { 0x00151B92, 0x006AE46E }, { 0x00148F76, 0x006B708A }, { 0x0014068D, 0x006BF973 }, { 0x001380B9, 0x006C7F47 }, { 0x0012FDDE, 0x006D0222 }, { 0x00127DE4, 0x006D821C }, { 0x001200B3, 0x006DFF4D }, { 0x00118632, 0x006E79CE }, { 0x00110E4F, 0x006EF1B1 }, { 0x001098F4, 0x006F670C }, { 0x00102610, 0x006FD9F0 }, { 0x000FB590, 0x00704A70 }, { 0x000F4764, 0x0070B89C }, { 0x000EDB7C, 0x00712484 }, { 0x000E71CA, 0x00718E36 },
			{ 0x000E0A3F, 0x0071F5C1 }, { 0x000DA4CF, 0x00725B31 }, { 0x000D416C, 0x0072BE94 }, { 0x000CE00B, 0x00731FF5 }, { 0x000C80A0, 0x00737F60 }, { 0x000C2320, 0x0073DCE0 }, { 0x000BC782, 0x0074387E }, { 0x000B6DBB, 0x00749245 }, { 0x000B15C1, 0x0074EA3F }, { 0x000ABF8D, 0x00754073 }, { 0x000A6B15, 0x007594EB }, { 0x000A1852, 0x0075E7AE }, { 0x0009C73B, 0x007638C5 }, { 0x000977C9, 0x00768837 }, { 0x000929F5, 0x0076D60B }, { 0x0008DDB7, 0x00772249 },
			{ 0x0008930B, 0x00776CF5 }, { 0x000849E8, 0x0077B618 }, { 0x0008024A, 0x0077FDB6 }, { 0x0007BC2A, 0x007843D6 }, { 0x00077782, 0x0078887E }, { 0x0007344E, 0x0078CBB2 }, { 0x0006F289, 0x00790D77 }, { 0x0006B22D, 0x00794DD3 }, { 0x00067335, 0x00798CCB }, { 0x0006359E, 0x0079CA62 }, { 0x0005F962, 0x007A069E }, { 0x0005BE7E, 0x007A4182 }, { 0x000584ED, 0x007A7B13 }, { 0x00054CAC, 0x007AB354 }, { 0x000515B6, 0x007AEA4A }, { 0x0004E009, 0x007B1FF7 },
			{ 0x0004ABA1, 0x007B545F }, { 0x00047879, 0x007B8787 }, { 0x00044690, 0x007BB970 }, { 0x000415E2, 0x007BEA1E }, { 0x0003E66C, 0x007C1994 }, { 0x0003B82B, 0x007C47D5 }, { 0x00038B1D, 0x007C74E3 }, { 0x00035F3E, 0x007CA0C2 }, { 0x0003348D, 0x007CCB73 }, { 0x00030B06, 0x007CF4FA }, { 0x0002E2A7, 0x007D1D59 }, { 0x0002BB6E, 0x007D4492 }, { 0x0002955A, 0x007D6AA6 }, { 0x00027066, 0x007D8F9A }, { 0x00024C93, 0x007DB36D }, { 0x000229DD, 0x007DD623 },
			{ 0x00020843, 0x007DF7BD }, { 0x0001E7C3, 0x007E183D }, { 0x0001C85C, 0x007E37A4 }, { 0x0001AA0B, 0x007E55F5 }, { 0x00018CCF, 0x007E7331 }, { 0x000170A6, 0x007E8F5A }, { 0x00015590, 0x007EAA70 }, { 0x00013B8A, 0x007EC476 }, { 0x00012294, 0x007EDD6C }, { 0x00010AAC, 0x007EF554 }, { 0x0000F3D1, 0x007F0C2F }, { 0x0000DE02, 0x007F21FE }, { 0x0000C93D, 0x007F36C3 }, { 0x0000B582, 0x007F4A7E }, { 0x0000A2D0, 0x007F5D30 }, { 0x00009125, 0x007F6EDB },
			{ 0x00008082, 0x007F7F7E }, { 0x000070E4, 0x007F8F1C }, { 0x0000624C, 0x007F9DB4 }, { 0x000054B9, 0x007FAB47 }, { 0x00004829, 0x007FB7D7 }, { 0x00003C9D, 0x007FC363 }, { 0x00003214, 0x007FCDEC }, { 0x0000288D, 0x007FD773 }, { 0x00002009, 0x007FDFF7 }, { 0x00001885, 0x007FE77B }, { 0x00001203, 0x007FEDFD }, { 0x00000C82, 0x007FF37E }, { 0x00000801, 0x007FF7FF }, { 0x00000481, 0x007FFB7F }, { 0x00000201, 0x007FFDFF }, { 0x00000081, 0x007FFF7F },
			{ 0x00000081, 0x007FFF7F }, { 0x00000201, 0x007FFDFF }, { 0x00000481, 0x007FFB7F }, { 0x00000801, 0x007FF7FF }, { 0x00000C82, 0x007FF37E }, { 0x00001203, 0x007FEDFD }, { 0x00001885, 0x007FE77B }, { 0x00002009, 0x007FDFF7 }, { 0x0000288D, 0x007FD773 }, { 0x00003214, 0x007FCDEC }, { 0x00003C9D, 0x007FC363 }, { 0x00004829, 0x007FB7D7 }, { 0x000054B9, 0x007FAB47 }, { 0x0000624C, 0x007F9DB4 }, { 0x000070E4, 0x007F8F1C }, { 0x00008082, 0x007F7F7E },
			{ 0x00009125, 0x007F6EDB }, { 0x0000A2D0, 0x007F5D30 }, { 0x0000B582, 0x007F4A7E }, { 0x0000C93D, 0x007F36C3 }, { 0x0000DE02, 0x007F21FE }, { 0x0000F3D1, 0x007F0C2F }, { 0x00010AAC, 0x007EF554 }, { 0x00012294, 0x007EDD6C }, { 0x00013B8A, 0x007EC476 }, { 0x00015590, 0x007EAA70 }, { 0x000170A6, 0x007E8F5A }, { 0x00018CCF, 0x007E7331 }, { 0x0001AA0B, 0x007E55F5 }, { 0x0001C85C, 0x007E37A4 }, { 0x0001E7C3, 0x007E183D }, { 0x00020843, 0x007DF7BD },
			{ 0x000229DD, 0x007DD623 }, { 0x00024C93, 0x007DB36D }, { 0x00027066, 0x007D8F9A }, { 0x0002955A, 0x007D6AA6 }, { 0x0002BB6E, 0x007D4492 }, { 0x0002E2A7, 0x007D1D59 }, { 0x00030B06, 0x007CF4FA }, { 0x0003348D, 0x007CCB73 }, { 0x00035F3E, 0x007CA0C2 }, { 0x00038B1D, 0x007C74E3 }, { 0x0003B82B, 0x007C47D5 }, { 0x0003E66C, 0x007C1994 }, { 0x000415E2, 0x007BEA1E }, { 0x00044690, 0x007BB970 }, { 0x00047879, 0x007B8787 }, { 0x0004ABA1, 0x007B545F },
			{ 0x0004E009, 0x007B1FF7 }, { 0x000515B6, 0x007AEA4A }, { 0x00054CAC, 0x007AB354 }, { 0x000584ED, 0x007A7B13 }, { 0x0005BE7E, 0x007A4182 }, { 0x0005F962, 0x007A069E }, { 0x0006359E, 0x0079CA62 }, { 0x00067335, 0x00798CCB }, { 0x0006B22D, 0x00794DD3 }, { 0x0006F289, 0x00790D77 }, { 0x0007344E, 0x0078CBB2 }, { 0x00077782, 0x0078887E }, { 0x0007BC2A, 0x007843D6 }, { 0x0008024A, 0x0077FDB6 }, { 0x000849E8, 0x0077B618 }, { 0x0008930B, 0x00776CF5 },
			{ 0x0008DDB7, 0x00772249 }, { 0x000929F5, 0x0076D60B }, { 0x000977C9, 0x00768837 }, { 0x0009C73B, 0x007638C5 }, { 0x000A1852, 0x0075E7AE }, { 0x000A6B15, 0x007594EB }, { 0x000ABF8D, 0x00754073 }, { 0x000B15C1, 0x0074EA3F }, { 0x000B6DBB, 0x00749245 }, { 0x000BC782, 0x0074387E }, { 0x000C2320, 0x0073DCE0 }, { 0x000C80A0, 0x00737F60 }, { 0x000CE00B, 0x00731FF5 }, { 0x000D416C, 0x0072BE94 }, { 0x000DA4CF, 0x00725B31 }, { 0x000E0A3F, 0x0071F5C1 },
			{ 0x000E71CA, 0x00718E36 }, { 0x000EDB7C, 0x00712484 }, { 0x000F4764, 0x0070B89C }, { 0x000FB590, 0x00704A70 }, { 0x00102610, 0x006FD9F0 }, { 0x001098F4, 0x006F670C }, { 0x00110E4F, 0x006EF1B1 }, { 0x00118632, 0x006E79CE }, { 0x001200B3, 0x006DFF4D }, { 0x00127DE4, 0x006D821C }, { 0x0012FDDE, 0x006D0222 }, { 0x001380B9, 0x006C7F47 }, { 0x0014068D, 0x006BF973 }, { 0x00148F76, 0x006B708A }, { 0x00151B92, 0x006AE46E }, { 0x0015AB01, 0x006A54FF },
			{ 0x00163DE5, 0x0069C21B }, { 0x0016D463, 0x00692B9D }, { 0x00176EA2, 0x0068915E }, { 0x00180CCF, 0x0067F331 }, { 0x0018AF19, 0x006750E7 }, { 0x001955B4, 0x0066AA4C }, { 0x001A00D8, 0x0065FF28 }, { 0x001AB0C4, 0x00654F3C }, { 0x001B65BD, 0x00649A43 }, { 0x001C200F, 0x0063DFF1 }, { 0x001CE00F, 0x00631FF1 }, { 0x001DA61D, 0x006259E3 }, { 0x001E72A4, 0x00618D5C }, { 0x001F461C, 0x0060B9E4 }, { 0x00202112, 0x005FDEEE }, { 0x00210422, 0x005EFBDE },
			{ 0x0021F005, 0x005E0FFB }, { 0x0022E591, 0x005D1A6F }, { 0x0023E5C4, 0x005C1A3C }, { 0x0024F1CB, 0x005B0E35 }, { 0x00260B17, 0x0059F4E9 }, { 0x00273369, 0x0058CC97 }, { 0x00286CF8, 0x00579308 }, { 0x0029BA9C, 0x00564564 }, { 0x002B2019, 0x0054DFE7 }, { 0x002CA295, 0x00535D6B }, { 0x002E497A, 0x0051B686 }, { 0x00302021, 0x004FDFDF }, { 0x0032399C, 0x004DC664 }, { 0x0034BB07, 0x004B44F9 }, { 0x00380402, 0x0047FBFE }, { 0x00400000, 0x00400000 },
		};

		private static uint[,] _pointWithinCircle_OuterThresholds = new uint[,]
		{
			{ 0x00380402, 0x0047FBFE }, { 0x0034BB07, 0x004B44F9 }, { 0x0032399C, 0x004DC664 }, { 0x00302021, 0x004FDFDF }, { 0x002E497A, 0x0051B686 }, { 0x002CA295, 0x00535D6B }, { 0x002B2019, 0x0054DFE7 }, { 0x0029BA9C, 0x00564564 }, { 0x00286CF8, 0x00579308 }, { 0x00273369, 0x0058CC97 }, { 0x00260B17, 0x0059F4E9 }, { 0x0024F1CB, 0x005B0E35 }, { 0x0023E5C4, 0x005C1A3C }, { 0x0022E591, 0x005D1A6F }, { 0x0021F005, 0x005E0FFB }, { 0x00210422, 0x005EFBDE },
			{ 0x00202112, 0x005FDEEE }, { 0x001F461C, 0x0060B9E4 }, { 0x001E72A4, 0x00618D5C }, { 0x001DA61D, 0x006259E3 }, { 0x001CE00F, 0x00631FF1 }, { 0x001C200F, 0x0063DFF1 }, { 0x001B65BD, 0x00649A43 }, { 0x001AB0C4, 0x00654F3C }, { 0x001A00D8, 0x0065FF28 }, { 0x001955B4, 0x0066AA4C }, { 0x0018AF19, 0x006750E7 }, { 0x00180CCF, 0x0067F331 }, { 0x00176EA2, 0x0068915E }, { 0x0016D463, 0x00692B9D }, { 0x00163DE5, 0x0069C21B }, { 0x0015AB01, 0x006A54FF },
			{ 0x00151B92, 0x006AE46E }, { 0x00148F76, 0x006B708A }, { 0x0014068D, 0x006BF973 }, { 0x001380B9, 0x006C7F47 }, { 0x0012FDDE, 0x006D0222 }, { 0x00127DE4, 0x006D821C }, { 0x001200B3, 0x006DFF4D }, { 0x00118632, 0x006E79CE }, { 0x00110E4F, 0x006EF1B1 }, { 0x001098F4, 0x006F670C }, { 0x00102610, 0x006FD9F0 }, { 0x000FB590, 0x00704A70 }, { 0x000F4764, 0x0070B89C }, { 0x000EDB7C, 0x00712484 }, { 0x000E71CA, 0x00718E36 }, { 0x000E0A3F, 0x0071F5C1 },
			{ 0x000DA4CF, 0x00725B31 }, { 0x000D416C, 0x0072BE94 }, { 0x000CE00B, 0x00731FF5 }, { 0x000C80A0, 0x00737F60 }, { 0x000C2320, 0x0073DCE0 }, { 0x000BC782, 0x0074387E }, { 0x000B6DBB, 0x00749245 }, { 0x000B15C1, 0x0074EA3F }, { 0x000ABF8D, 0x00754073 }, { 0x000A6B15, 0x007594EB }, { 0x000A1852, 0x0075E7AE }, { 0x0009C73B, 0x007638C5 }, { 0x000977C9, 0x00768837 }, { 0x000929F5, 0x0076D60B }, { 0x0008DDB7, 0x00772249 }, { 0x0008930B, 0x00776CF5 },
			{ 0x000849E8, 0x0077B618 }, { 0x0008024A, 0x0077FDB6 }, { 0x0007BC2A, 0x007843D6 }, { 0x00077782, 0x0078887E }, { 0x0007344E, 0x0078CBB2 }, { 0x0006F289, 0x00790D77 }, { 0x0006B22D, 0x00794DD3 }, { 0x00067335, 0x00798CCB }, { 0x0006359E, 0x0079CA62 }, { 0x0005F962, 0x007A069E }, { 0x0005BE7E, 0x007A4182 }, { 0x000584ED, 0x007A7B13 }, { 0x00054CAC, 0x007AB354 }, { 0x000515B6, 0x007AEA4A }, { 0x0004E009, 0x007B1FF7 }, { 0x0004ABA1, 0x007B545F },
			{ 0x00047879, 0x007B8787 }, { 0x00044690, 0x007BB970 }, { 0x000415E2, 0x007BEA1E }, { 0x0003E66C, 0x007C1994 }, { 0x0003B82B, 0x007C47D5 }, { 0x00038B1D, 0x007C74E3 }, { 0x00035F3E, 0x007CA0C2 }, { 0x0003348D, 0x007CCB73 }, { 0x00030B06, 0x007CF4FA }, { 0x0002E2A7, 0x007D1D59 }, { 0x0002BB6E, 0x007D4492 }, { 0x0002955A, 0x007D6AA6 }, { 0x00027066, 0x007D8F9A }, { 0x00024C93, 0x007DB36D }, { 0x000229DD, 0x007DD623 }, { 0x00020843, 0x007DF7BD },
			{ 0x0001E7C3, 0x007E183D }, { 0x0001C85C, 0x007E37A4 }, { 0x0001AA0B, 0x007E55F5 }, { 0x00018CCF, 0x007E7331 }, { 0x000170A6, 0x007E8F5A }, { 0x00015590, 0x007EAA70 }, { 0x00013B8A, 0x007EC476 }, { 0x00012294, 0x007EDD6C }, { 0x00010AAC, 0x007EF554 }, { 0x0000F3D1, 0x007F0C2F }, { 0x0000DE02, 0x007F21FE }, { 0x0000C93D, 0x007F36C3 }, { 0x0000B582, 0x007F4A7E }, { 0x0000A2D0, 0x007F5D30 }, { 0x00009125, 0x007F6EDB }, { 0x00008082, 0x007F7F7E },
			{ 0x000070E4, 0x007F8F1C }, { 0x0000624C, 0x007F9DB4 }, { 0x000054B9, 0x007FAB47 }, { 0x00004829, 0x007FB7D7 }, { 0x00003C9D, 0x007FC363 }, { 0x00003214, 0x007FCDEC }, { 0x0000288D, 0x007FD773 }, { 0x00002009, 0x007FDFF7 }, { 0x00001885, 0x007FE77B }, { 0x00001203, 0x007FEDFD }, { 0x00000C82, 0x007FF37E }, { 0x00000801, 0x007FF7FF }, { 0x00000481, 0x007FFB7F }, { 0x00000201, 0x007FFDFF }, { 0x00000081, 0x007FFF7F }, { 0x00000000, 0x00800000 },
			{ 0x00000000, 0x00800000 }, { 0x00000081, 0x007FFF7F }, { 0x00000201, 0x007FFDFF }, { 0x00000481, 0x007FFB7F }, { 0x00000801, 0x007FF7FF }, { 0x00000C82, 0x007FF37E }, { 0x00001203, 0x007FEDFD }, { 0x00001885, 0x007FE77B }, { 0x00002009, 0x007FDFF7 }, { 0x0000288D, 0x007FD773 }, { 0x00003214, 0x007FCDEC }, { 0x00003C9D, 0x007FC363 }, { 0x00004829, 0x007FB7D7 }, { 0x000054B9, 0x007FAB47 }, { 0x0000624C, 0x007F9DB4 }, { 0x000070E4, 0x007F8F1C },
			{ 0x00008082, 0x007F7F7E }, { 0x00009125, 0x007F6EDB }, { 0x0000A2D0, 0x007F5D30 }, { 0x0000B582, 0x007F4A7E }, { 0x0000C93D, 0x007F36C3 }, { 0x0000DE02, 0x007F21FE }, { 0x0000F3D1, 0x007F0C2F }, { 0x00010AAC, 0x007EF554 }, { 0x00012294, 0x007EDD6C }, { 0x00013B8A, 0x007EC476 }, { 0x00015590, 0x007EAA70 }, { 0x000170A6, 0x007E8F5A }, { 0x00018CCF, 0x007E7331 }, { 0x0001AA0B, 0x007E55F5 }, { 0x0001C85C, 0x007E37A4 }, { 0x0001E7C3, 0x007E183D },
			{ 0x00020843, 0x007DF7BD }, { 0x000229DD, 0x007DD623 }, { 0x00024C93, 0x007DB36D }, { 0x00027066, 0x007D8F9A }, { 0x0002955A, 0x007D6AA6 }, { 0x0002BB6E, 0x007D4492 }, { 0x0002E2A7, 0x007D1D59 }, { 0x00030B06, 0x007CF4FA }, { 0x0003348D, 0x007CCB73 }, { 0x00035F3E, 0x007CA0C2 }, { 0x00038B1D, 0x007C74E3 }, { 0x0003B82B, 0x007C47D5 }, { 0x0003E66C, 0x007C1994 }, { 0x000415E2, 0x007BEA1E }, { 0x00044690, 0x007BB970 }, { 0x00047879, 0x007B8787 },
			{ 0x0004ABA1, 0x007B545F }, { 0x0004E009, 0x007B1FF7 }, { 0x000515B6, 0x007AEA4A }, { 0x00054CAC, 0x007AB354 }, { 0x000584ED, 0x007A7B13 }, { 0x0005BE7E, 0x007A4182 }, { 0x0005F962, 0x007A069E }, { 0x0006359E, 0x0079CA62 }, { 0x00067335, 0x00798CCB }, { 0x0006B22D, 0x00794DD3 }, { 0x0006F289, 0x00790D77 }, { 0x0007344E, 0x0078CBB2 }, { 0x00077782, 0x0078887E }, { 0x0007BC2A, 0x007843D6 }, { 0x0008024A, 0x0077FDB6 }, { 0x000849E8, 0x0077B618 },
			{ 0x0008930B, 0x00776CF5 }, { 0x0008DDB7, 0x00772249 }, { 0x000929F5, 0x0076D60B }, { 0x000977C9, 0x00768837 }, { 0x0009C73B, 0x007638C5 }, { 0x000A1852, 0x0075E7AE }, { 0x000A6B15, 0x007594EB }, { 0x000ABF8D, 0x00754073 }, { 0x000B15C1, 0x0074EA3F }, { 0x000B6DBB, 0x00749245 }, { 0x000BC782, 0x0074387E }, { 0x000C2320, 0x0073DCE0 }, { 0x000C80A0, 0x00737F60 }, { 0x000CE00B, 0x00731FF5 }, { 0x000D416C, 0x0072BE94 }, { 0x000DA4CF, 0x00725B31 },
			{ 0x000E0A3F, 0x0071F5C1 }, { 0x000E71CA, 0x00718E36 }, { 0x000EDB7C, 0x00712484 }, { 0x000F4764, 0x0070B89C }, { 0x000FB590, 0x00704A70 }, { 0x00102610, 0x006FD9F0 }, { 0x001098F4, 0x006F670C }, { 0x00110E4F, 0x006EF1B1 }, { 0x00118632, 0x006E79CE }, { 0x001200B3, 0x006DFF4D }, { 0x00127DE4, 0x006D821C }, { 0x0012FDDE, 0x006D0222 }, { 0x001380B9, 0x006C7F47 }, { 0x0014068D, 0x006BF973 }, { 0x00148F76, 0x006B708A }, { 0x00151B92, 0x006AE46E },
			{ 0x0015AB01, 0x006A54FF }, { 0x00163DE5, 0x0069C21B }, { 0x0016D463, 0x00692B9D }, { 0x00176EA2, 0x0068915E }, { 0x00180CCF, 0x0067F331 }, { 0x0018AF19, 0x006750E7 }, { 0x001955B4, 0x0066AA4C }, { 0x001A00D8, 0x0065FF28 }, { 0x001AB0C4, 0x00654F3C }, { 0x001B65BD, 0x00649A43 }, { 0x001C200F, 0x0063DFF1 }, { 0x001CE00F, 0x00631FF1 }, { 0x001DA61D, 0x006259E3 }, { 0x001E72A4, 0x00618D5C }, { 0x001F461C, 0x0060B9E4 }, { 0x00202112, 0x005FDEEE },
			{ 0x00210422, 0x005EFBDE }, { 0x0021F005, 0x005E0FFB }, { 0x0022E591, 0x005D1A6F }, { 0x0023E5C4, 0x005C1A3C }, { 0x0024F1CB, 0x005B0E35 }, { 0x00260B17, 0x0059F4E9 }, { 0x00273369, 0x0058CC97 }, { 0x00286CF8, 0x00579308 }, { 0x0029BA9C, 0x00564564 }, { 0x002B2019, 0x0054DFE7 }, { 0x002CA295, 0x00535D6B }, { 0x002E497A, 0x0051B686 }, { 0x00302021, 0x004FDFDF }, { 0x0032399C, 0x004DC664 }, { 0x0034BB07, 0x004B44F9 }, { 0x00380402, 0x0047FBFE },
		};

		private const int _pointWithinCircle_BucketShift = 15;
		private const uint _pointWithinCircle_BucketMask = 0xFFU;
#endif

		/// <summary>
		/// Generates a random 2-dimensional vector selected from a uniform distribution of all points within a unit circle.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random 2-dimensional vector with a magnitude less than or equal to 1.</returns>
		public static Vector2 PointWithinCircle(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Sqrt(random.ClosedFloatUnit());
			return random.UnitVector2() * distance;
#else
#if false
			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > 1f) goto Start;

			return new Vector2(u, v);
#elif false
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (lower >= Detail.FloatingPoint.floatSignExponentMask & upper >= Detail.FloatingPoint.floatSignExponentMask) // First part of test for generating the special case values <+1, 0> and <0, +1>.
			{
			}
			float u = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
			float v = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > 1f) goto Start;

			return new Vector2(u, v);
#elif true
			Start:
			ulong bits = random.Next64();
			if (bits >= 0xFFFFC00000000000UL) // First part of test for generating the special case values <+1, 0> and <0, +1>.
			{
			}
			uint upper = (uint)(bits >> 23);
			uint lower = (uint)bits;
			long ix = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			if (ix * ix + iy * iy > 0x0000100000000000L) goto Start; // x^2 + y^2 <= r^2, so generated point is within the circle.
			Vector2 v;
			v.x = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
			v.y = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
			return v;
			//return new Vector2(Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f, Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f);
#elif true
			Vector2 v;
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (lower >= Detail.FloatingPoint.floatSignExponentMask & upper >= Detail.FloatingPoint.floatSignExponentMask) // First part of test for generating the special case values <+1, 0> and <0, +1>.
			{
			}

			uint bucket = (lower >> _pointWithinCircle_BucketShift) & _pointWithinCircle_BucketMask;
			uint ux = upper & Detail.FloatingPoint.floatMantissaMask;
			if (ux >= _pointWithinCircle_InnerThresholds[bucket, 0] & ux <= _pointWithinCircle_InnerThresholds[bucket, 1]) // Generated point is definitely within a rectangle that is entirely inside the circle.
			{
				v.x = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
				v.y = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
				return v;
			}
			else if (ux < _pointWithinCircle_OuterThresholds[bucket, 0] | ux > _pointWithinCircle_OuterThresholds[bucket, 1]) // Generated point is definitely within one of two rectangles that are entirely outside the circle.
			{
				goto Start;
			}
			else // Generated point is near the boundary of the circle, so we'll have to do a more precise check.
			{
				long ix = (int)ux - 0x00400000;
				long iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
				if (ix * ix + iy * iy <= 0x0000100000000000L) // x^2 + y^2 <= r^2, so generated point is within the circle.
				{
					v.x = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
					v.y = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
					return v;
				}
				else
				{
					goto Start;
				}
			}
#endif
#endif
		}

		public static void PointWithinCircle(this IRandom random, out Vector2 vec)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Sqrt(random.ClosedFloatUnit());
			return random.UnitVector2() * distance;
#else
#if false
			Start:
			vec.x = random.FloatOO() * 2f - 1f;
			vec.y = random.FloatOO() * 2f - 1f;
			float uSqr = vec.x * vec.x;
			float vSqr = vec.y * vec.y;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > 1f) goto Start;
#elif false
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (lower >= Detail.FloatingPoint.floatSignExponentMask & upper >= Detail.FloatingPoint.floatSignExponentMask) // First part of test for generating the special case values <+1, 0> and <0, +1>.
			{
			}
			vec.x = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
			vec.y = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
			float uSqr = vec.x * vec.x;
			float vSqr = vec.y * vec.y;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > 1f) goto Start;
#elif true
			Start:
			ulong bits = random.Next64();
			if (bits >= 0xFFFFC00000000000UL) // First part of test for generating the special case values <+1, 0> and <0, +1>.
			{
			}
			uint upper = (uint)(bits >> 23);
			uint lower = (uint)bits;
			long ix = (int)(upper & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			long iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
			if (ix * ix + iy * iy > 0x0000100000000000L) goto Start; // x^2 + y^2 <= r^2, so generated point is within the circle.
			vec.x = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
			vec.y = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
#elif true
			Start:
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (lower >= Detail.FloatingPoint.floatSignExponentMask & upper >= Detail.FloatingPoint.floatSignExponentMask) // First part of test for generating the special case values <+1, 0> and <0, +1>.
			{
			}

			uint bucket = (lower >> _pointWithinCircle_BucketShift) & _pointWithinCircle_BucketMask;
			uint ux = upper & Detail.FloatingPoint.floatMantissaMask;
			if (ux >= _pointWithinCircle_InnerThresholds[bucket, 0] & ux <= _pointWithinCircle_InnerThresholds[bucket, 1]) // Generated point is definitely within a rectangle that is entirely inside the circle.
			{
				vec.x = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
				vec.y = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
				return;
			}
			else if (ux < _pointWithinCircle_OuterThresholds[bucket, 0] | ux > _pointWithinCircle_OuterThresholds[bucket, 1]) // Generated point is definitely within one of two rectangles that are entirely outside the circle.
			{
				goto Start;
			}
			else // Generated point is near the boundary of the circle, so we'll have to do a more precise check.
			{
				long ix = (int)ux - 0x00400000;
				long iy = (int)(lower & Detail.FloatingPoint.floatMantissaMask) - 0x00400000;
				if (ix * ix + iy * iy <= 0x0000100000000000L) // x^2 + y^2 <= r^2, so generated point is within the circle.
				{
					vec.x = Detail.FloatingPoint.BitsToFloatC1O2(upper) * 2f - 3f;
					vec.y = Detail.FloatingPoint.BitsToFloatC1O2(lower) * 2f - 3f;
					return;
				}
				else
				{
					goto Start;
				}
			}
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
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var distance = Mathf.Pow(random.ClosedFloatUnit(), 1f / 3f);
			return random.UnitVector3() * distance;
#else
			Start:
			float u = random.FloatOO() * 2f - 1f;
			float v = random.FloatOO() * 2f - 1f;
			float w = random.FloatOO() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > 1f) goto Start;

			return new Vector3(u, v, w);
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
