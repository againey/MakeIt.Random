/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomVector
	{
		#region Unit Vector

		public static Vector2 UnitVector2(this IRandomEngine random)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var angle = random.HalfOpenRange(0f, Mathf.PI * 2f);
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
#else
			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			return new Vector2((uSqr - vSqr) / uvSqr, 2f * u * v / uvSqr);
#endif
		}

		public static Vector3 UnitVector3(this IRandomEngine random)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var longitude = random.HalfOpenRange(0f, Mathf.PI * 2f);
			var z = random.ClosedRange(-1f, +1f);
			var invertedZ = Mathf.Sqrt(1f - z * z);
			return new Vector3(invertedZ * Mathf.Cos(longitude), invertedZ * Mathf.Sin(longitude), z);
#else
			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			float t = 2f * Mathf.Sqrt(1f - uvSqr);
			return new Vector3(u * t, v * t, 1f - 2f * uvSqr);
#endif
		}

		public static Vector4 UnitVector4(this IRandomEngine random)
		{
			Start1:
			float u1 = random.OpenFloatUnit() * 2f - 1f;
			float v1 = random.OpenFloatUnit() * 2f - 1f;
			float uSqr1 = u1 * u1;
			float vSqr1 = v1 * v1;
			float uvSqr1 = uSqr1 + vSqr1;
			if (uvSqr1 >= 1f) goto Start1;

			Start2:
			float u2 = random.OpenFloatUnit() * 2f - 1f;
			float v2 = random.OpenFloatUnit() * 2f - 1f;
			float uSqr2 = u2 * u2;
			float vSqr2 = v2 * v2;
			float uvSqr2 = uSqr2 + vSqr2;
			if (uvSqr2 >= 1f) goto Start2;

			float t = Mathf.Sqrt((1f - uvSqr1) / uvSqr2);
			return new Vector4(u1, v1, u2 * t, v2 * t);
		}

		#endregion

		#region Scaled Vector

		public static Vector2 ScaledVector2(this IRandomEngine random, float radius)
		{
			return UnitVector2(random) * radius;
		}

		public static Vector3 ScaledVector3(this IRandomEngine random, float radius)
		{
			return UnitVector3(random) * radius;
		}

		public static Vector4 ScaledVector4(this IRandomEngine random, float radius)
		{
			return UnitVector4(random) * radius;
		}

		#endregion

		#region Radial

		public static Vector2 PointWithinCircle(this IRandomEngine random)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var distance = Mathf.Sqrt(random.ClosedFloatUnit());
			return random.UnitVector2() * distance;
#else
			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > 1f) goto Start;

			return new Vector2(u, v);
#endif
		}

		public static Vector2 PointWithinCircle(this IRandomEngine random, float radius)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var distance = Mathf.Sqrt(random.ClosedFloatUnit()) * radius;
			return random.UnitVector2() * distance;
#else
			float rSqr = radius * radius;

			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > rSqr) goto Start;

			return new Vector2(u, v);
#endif
		}

		public static Vector2 PointWithinCircularShell(this IRandomEngine random, float innerRadius, float outerRadius)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var unitMin = innerRadius / outerRadius;
			var unitMinSquared = unitMin * unitMin;
			var unitRange = 1f - unitMinSquared;
			var distance = Mathf.Sqrt(random.ClosedFloatUnit() * unitRange + unitMinSquared) * outerRadius;
			return random.UnitVector2() * distance;
#else
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr < irSqr || uvSqr > orSqr) goto Start;

			return new Vector2(u, v);
#endif
		}

		public static Vector3 PointWithinSphere(this IRandomEngine random)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var distance = Mathf.Pow(random.ClosedFloatUnit(), 1f / 3f);
			return random.UnitVector3() * distance;
#else
			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float w = random.OpenFloatUnit() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > 1f) goto Start;

			return new Vector3(u, v, w);
#endif
		}

		public static Vector3 PointWithinSphere(this IRandomEngine random, float radius)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var distance = Mathf.Pow(random.ClosedFloatUnit(), 1f / 3f) * radius;
			return random.UnitVector3() * distance;
#else
			float rSqr = radius * radius;

			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float w = random.OpenFloatUnit() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > rSqr) goto Start;

			return new Vector3(u, v, w);
#endif
		}

		public static Vector3 PointWithinSphericalShell(this IRandomEngine random, float innerRadius, float outerRadius)
		{
#if RANDOMIZATION_COMPAT_V1_0
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(random.ClosedFloatUnit() * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return random.UnitVector3() * distance;
#else
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = random.OpenFloatUnit() * 2f - 1f;
			float v = random.OpenFloatUnit() * 2f - 1f;
			float w = random.OpenFloatUnit() * 2f - 1f;
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

		public static Vector2 PointWithinSquare(this IRandomEngine random)
		{
			return new Vector2(random.ClosedFloatUnit(), random.ClosedFloatUnit());
		}

		public static Vector2 PointWithinSquare(this IRandomEngine random, float sideLength)
		{
			return new Vector2(random.ClosedRange(sideLength), random.ClosedRange(sideLength));
		}

		public static Vector2 PointWithinRectangle(this IRandomEngine random, Vector2 size)
		{
			return new Vector2(random.ClosedRange(size.x), random.ClosedRange(size.y));
		}

		public static Vector2 PointWithinParallelogram(this IRandomEngine random, Vector2 axis0, Vector2 axis1)
		{
			return random.ClosedFloatUnit() * axis0 + random.ClosedFloatUnit() * axis1;
		}

		public static Vector2 PointWithinTriangle(this IRandomEngine random, Vector2 axis0, Vector2 axis1)
		{
			float u = Mathf.Sqrt(random.ClosedFloatUnit());
			float v = random.ClosedRange(u);
			return (1f - u) * axis0 + v * axis1;
		}

		public static Vector3 PointWithinCube(this IRandomEngine random)
		{
			return new Vector3(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit());
		}

		public static Vector3 PointWithinCube(this IRandomEngine random, float sideLength)
		{
			return new Vector3(random.ClosedRange(sideLength), random.ClosedRange(sideLength), random.ClosedRange(sideLength));
		}

		public static Vector3 PointWithinBox(this IRandomEngine random, Vector3 size)
		{
			return new Vector3(random.ClosedRange(size.x), random.ClosedRange(size.y), random.ClosedRange(size.z));
		}

		public static Vector3 PointWithinBox(this IRandomEngine random, Bounds box)
		{
			return random.PointWithinBox(box.size) + box.min;
		}

		public static Vector3 PointWithinRhomboid(this IRandomEngine random, Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return random.ClosedFloatUnit() * axis0 + random.ClosedFloatUnit() * axis1 + random.ClosedFloatUnit() * axis2;
		}

		#endregion
	}
}
