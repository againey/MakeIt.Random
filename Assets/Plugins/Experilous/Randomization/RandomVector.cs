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
			var angle = RandomRange.HalfOpen(0f, Mathf.PI * 2f, engine);
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		}

		public static Vector3 UnitVector3(IRandomEngine engine)
		{
			var longitude = RandomRange.HalfOpen(0f, Mathf.PI * 2f, engine);
			var z = RandomRange.Closed(-1f, +1f, engine);
			var invertedZ = Mathf.Sqrt(1f - z * z);
			return new Vector3(invertedZ * Mathf.Cos(longitude), invertedZ * Mathf.Sin(longitude), z);
		}

		public static Vector4 UnitVector4(IRandomEngine engine)
		{
			var theta0 = RandomRange.HalfOpen(0f, Mathf.PI * 2f, engine);
			var theta1 = Mathf.Acos(RandomRange.Closed(-1f, +1f, engine));
			var theta2 = 0.5f * (RandomRange.HalfOpen(0f, Mathf.PI, engine) + Mathf.Asin(RandomUnit.ClosedFloat(engine)) + Mathf.PI * 0.5f);
			var sinTheta1 = Mathf.Sin(theta1);
			var sinTheta2 = Mathf.Sin(theta2);
			return new Vector4(
				Mathf.Sin(theta0) * sinTheta1 * sinTheta2,
				Mathf.Cos(theta0) * sinTheta1 * sinTheta2,
				Mathf.Cos(theta1) * sinTheta2,
				Mathf.Cos(theta2));
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
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloat(engine));
			return ScaledVector2(distance, engine);
		}

		public static Vector2 PointWithinCircle(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloat(engine)) * radius;
			return ScaledVector2(distance, engine);
		}

		public static Vector2 PointWithinCircularShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinSquared = unitMin * unitMin;
			var unitRange = 1f - unitMinSquared;
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloat(engine) * unitRange + unitMinSquared) * outerRadius;
			return ScaledVector2(distance, engine);
		}

		public static Vector3 PointWithinSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine), 1f / 3f);
			return ScaledVector3(distance, engine);
		}

		public static Vector3 PointWithinSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine), 1f / 3f) * radius;
			return ScaledVector3(distance, engine);
		}

		public static Vector3 PointWithinSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine) * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return ScaledVector3(distance, engine);
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
