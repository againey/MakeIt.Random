/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomVector
	{
		#region Unit Vector

		public static Vector2 UnitVector2(IRandomEngine engine)
		{
			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			return new Vector2((uSqr - vSqr) / uvSqr, 2f * u * v / uvSqr);
		}

		public static Vector3 UnitVector3(IRandomEngine engine)
		{
			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			float t = 2f * Mathf.Sqrt(1f - uvSqr);
			return new Vector3(u * t, v * t, 1f - 2f * uvSqr);
		}

		public static Vector4 UnitVector4(IRandomEngine engine)
		{
			Start1:
			float u1 = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v1 = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr1 = u1 * u1;
			float vSqr1 = v1 * v1;
			float uvSqr1 = uSqr1 + vSqr1;
			if (uvSqr1 >= 1f) goto Start1;

			Start2:
			float u2 = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v2 = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr2 = u2 * u2;
			float vSqr2 = v2 * v2;
			float uvSqr2 = uSqr2 + vSqr2;
			if (uvSqr2 >= 1f) goto Start2;

			float t = Mathf.Sqrt((1f - uvSqr1) / uvSqr2);
			return new Vector4(u1, v1, u2 * t, v2 * t);
		}

		#endregion

		#region Scaled Vector

		public static Vector2 ScaledVector2(float radius, IRandomEngine engine)
		{
			return UnitVector2(engine) * radius;
		}

		public static Vector3 ScaledVector3(float radius, IRandomEngine engine)
		{
			return UnitVector3(engine) * radius;
		}

		public static Vector4 ScaledVector4(float radius, IRandomEngine engine)
		{
			return UnitVector4(engine) * radius;
		}

		#endregion

		#region Radial

		public static Vector2 PointWithinCircle(IRandomEngine engine)
		{
			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > 1f) goto Start;

			return new Vector2(u, v);
		}

		public static Vector2 PointWithinCircle(float radius, IRandomEngine engine)
		{
			float rSqr = radius * radius;

			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > rSqr) goto Start;

			return new Vector2(u, v);
		}

		public static Vector2 PointWithinCircularShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr < irSqr || uvSqr > orSqr) goto Start;

			return new Vector2(u, v);
		}

		public static Vector3 PointWithinSphere(IRandomEngine engine)
		{
			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float w = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > 1f) goto Start;

			return new Vector3(u, v, w);
		}

		public static Vector3 PointWithinSphere(float radius, IRandomEngine engine)
		{
			float rSqr = radius * radius;

			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float w = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > rSqr) goto Start;

			return new Vector3(u, v, w);
		}

		public static Vector3 PointWithinSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float v = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float w = RandomUnit.OpenFloat(engine) * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr < irSqr || uvwSqr > orSqr) goto Start;

			return new Vector3(u, v, w);
		}

		#endregion

		#region Axial

		public static Vector2 PointWithinSquare(IRandomEngine engine)
		{
			return new Vector2(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static Vector2 PointWithinSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(RandomRange.Closed(sideLength, engine), RandomRange.Closed(sideLength, engine));
		}

		public static Vector2 PointWithinRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(RandomRange.Closed(size.x, engine), RandomRange.Closed(size.y, engine));
		}

		public static Vector2 PointWithinParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return RandomUnit.ClosedFloat(engine) * axis0 + RandomUnit.ClosedFloat(engine) * axis1;
		}

		public static Vector2 PointWithinTriangle(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			float u = Mathf.Sqrt(RandomUnit.ClosedFloat(engine));
			float v = RandomRange.Closed(u, engine);
			return (1f - u) * axis0 + v * axis1;
		}

		public static Vector3 PointWithinCube(IRandomEngine engine)
		{
			return new Vector3(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static Vector3 PointWithinCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(RandomRange.Closed(sideLength, engine), RandomRange.Closed(sideLength, engine), RandomRange.Closed(sideLength, engine));
		}

		public static Vector3 PointWithinCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(RandomRange.Closed(size.x, engine), RandomRange.Closed(size.y, engine), RandomRange.Closed(size.z, engine));
		}

		public static Vector3 PointWithinRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return RandomUnit.ClosedFloat(engine) * axis0 + RandomUnit.ClosedFloat(engine) * axis1 + RandomUnit.ClosedFloat(engine) * axis2;
		}

		#endregion
	}
}
