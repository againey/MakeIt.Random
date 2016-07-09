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
			var z = RandomRange.ClosedFast(-1f, +1f, engine);
			var invertedZ = Mathf.Sqrt(1f - z * z);
			return new Vector3(invertedZ * Mathf.Cos(longitude), invertedZ * Mathf.Sin(longitude), z);
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

		#endregion

		#region Radial

		public static Vector2 PointWithinCircle(IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloatFast(engine));
			return ScaledVector2(distance, engine);
		}

		public static Vector2 PointWithinCircle(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloatFast(engine)) * radius;
			return ScaledVector2(distance, engine);
		}

		public static Vector2 PointWithinCircularShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinSquared = unitMin * unitMin;
			var unitRange = 1f - unitMinSquared;
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloatFast(engine) * unitRange + unitMinSquared) * outerRadius;
			return ScaledVector2(distance, engine);
		}

		public static Vector3 PointWithinSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloatFast(engine), 1f / 3f);
			return ScaledVector3(distance, engine);
		}

		public static Vector3 PointWithinSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloatFast(engine), 1f / 3f) * radius;
			return ScaledVector3(distance, engine);
		}

		public static Vector3 PointWithinSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(RandomUnit.ClosedFloatFast(engine) * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return ScaledVector3(distance, engine);
		}

		#endregion

		#region Axial

		public static Vector2 PointWithinSquare(IRandomEngine engine)
		{
			return new Vector2(RandomUnit.ClosedFloatFast(engine), RandomUnit.ClosedFloatFast(engine));
		}

		public static Vector2 PointWithinSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(RandomRange.ClosedFast(sideLength, engine), RandomRange.ClosedFast(sideLength, engine));
		}

		public static Vector2 PointWithinRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(RandomRange.ClosedFast(size.x, engine), RandomRange.ClosedFast(size.y, engine));
		}

		public static Vector2 PointWithinParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return RandomUnit.ClosedFloatFast(engine) * axis0 + RandomUnit.ClosedFloatFast(engine) * axis1;
		}

		public static Vector2 PointWithinTriangle(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			float u = Mathf.Sqrt(RandomUnit.ClosedFloatFast(engine));
			float v = RandomRange.ClosedFast(u, engine);
			return (1f - u) * axis0 + v * axis1;
		}

		public static Vector3 PointWithinCube(IRandomEngine engine)
		{
			return new Vector3(RandomUnit.ClosedFloatFast(engine), RandomUnit.ClosedFloatFast(engine), RandomUnit.ClosedFloatFast(engine));
		}

		public static Vector3 PointWithinCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(RandomRange.ClosedFast(sideLength, engine), RandomRange.ClosedFast(sideLength, engine), RandomRange.ClosedFast(sideLength, engine));
		}

		public static Vector3 PointWithinCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(RandomRange.ClosedFast(size.x, engine), RandomRange.ClosedFast(size.y, engine), RandomRange.ClosedFast(size.z, engine));
		}

		public static Vector3 PointWithinRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return RandomUnit.ClosedFloatFast(engine) * axis0 + RandomUnit.ClosedFloatFast(engine) * axis1 + RandomUnit.ClosedFloatFast(engine) * axis2;
		}

		#endregion
	}
}
