/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public struct RandomVector
	{
		private IRandomEngine _random;

		public RandomVector(IRandomEngine random)
		{
			_random = random;
		}

		#region Unit Vector

		public Vector2 UnitVector2()
		{
			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			return new Vector2((uSqr - vSqr) / uvSqr, 2f * u * v / uvSqr);
		}

		public Vector3 UnitVector3()
		{
			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr >= 1f) goto Start;

			float t = 2f * Mathf.Sqrt(1f - uvSqr);
			return new Vector3(u * t, v * t, 1f - 2f * uvSqr);
		}

		public Vector4 UnitVector4()
		{
			Start1:
			float u1 = _random.Unit().OpenFloat() * 2f - 1f;
			float v1 = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr1 = u1 * u1;
			float vSqr1 = v1 * v1;
			float uvSqr1 = uSqr1 + vSqr1;
			if (uvSqr1 >= 1f) goto Start1;

			Start2:
			float u2 = _random.Unit().OpenFloat() * 2f - 1f;
			float v2 = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr2 = u2 * u2;
			float vSqr2 = v2 * v2;
			float uvSqr2 = uSqr2 + vSqr2;
			if (uvSqr2 >= 1f) goto Start2;

			float t = Mathf.Sqrt((1f - uvSqr1) / uvSqr2);
			return new Vector4(u1, v1, u2 * t, v2 * t);
		}

		#endregion

		#region Scaled Vector

		public Vector2 ScaledVector2(float radius)
		{
			return UnitVector2() * radius;
		}

		public Vector3 ScaledVector3(float radius)
		{
			return UnitVector3() * radius;
		}

		public Vector4 ScaledVector4(float radius)
		{
			return UnitVector4() * radius;
		}

		#endregion

		#region Radial

		public Vector2 PointWithinCircle()
		{
			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > 1f) goto Start;

			return new Vector2(u, v);
		}

		public Vector2 PointWithinCircle(float radius)
		{
			float rSqr = radius * radius;

			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr > rSqr) goto Start;

			return new Vector2(u, v);
		}

		public Vector2 PointWithinCircularShell(float innerRadius, float outerRadius)
		{
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float uvSqr = uSqr + vSqr;
			if (uvSqr < irSqr || uvSqr > orSqr) goto Start;

			return new Vector2(u, v);
		}

		public Vector3 PointWithinSphere()
		{
			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float w = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > 1f) goto Start;

			return new Vector3(u, v, w);
		}

		public Vector3 PointWithinSphere(float radius)
		{
			float rSqr = radius * radius;

			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float w = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr > rSqr) goto Start;

			return new Vector3(u, v, w);
		}

		public Vector3 PointWithinSphericalShell(float innerRadius, float outerRadius)
		{
			float irSqr = innerRadius * innerRadius;
			float orSqr = outerRadius * outerRadius;

			Start:
			float u = _random.Unit().OpenFloat() * 2f - 1f;
			float v = _random.Unit().OpenFloat() * 2f - 1f;
			float w = _random.Unit().OpenFloat() * 2f - 1f;
			float uSqr = u * u;
			float vSqr = v * v;
			float wSqr = w * w;
			float uvwSqr = uSqr + vSqr + wSqr;
			if (uvwSqr < irSqr || uvwSqr > orSqr) goto Start;

			return new Vector3(u, v, w);
		}

		#endregion

		#region Axial

		public Vector2 PointWithinSquare()
		{
			return new Vector2(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat());
		}

		public Vector2 PointWithinSquare(float sideLength)
		{
			return new Vector2(_random.Range().Closed(sideLength), _random.Range().Closed(sideLength));
		}

		public Vector2 PointWithinRectangle(Vector2 size)
		{
			return new Vector2(_random.Range().Closed(size.x), _random.Range().Closed(size.y));
		}

		public Vector2 PointWithinParallelogram(Vector2 axis0, Vector2 axis1)
		{
			return _random.Unit().ClosedFloat() * axis0 + _random.Unit().ClosedFloat() * axis1;
		}

		public Vector2 PointWithinTriangle(Vector2 axis0, Vector2 axis1)
		{
			float u = Mathf.Sqrt(_random.Unit().ClosedFloat());
			float v = _random.Range().Closed(u);
			return (1f - u) * axis0 + v * axis1;
		}

		public Vector3 PointWithinCube()
		{
			return new Vector3(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat());
		}

		public Vector3 PointWithinCube(float sideLength)
		{
			return new Vector3(_random.Range().Closed(sideLength), _random.Range().Closed(sideLength), _random.Range().Closed(sideLength));
		}

		public Vector3 PointWithinCuboid(Vector3 size)
		{
			return new Vector3(_random.Range().Closed(size.x), _random.Range().Closed(size.y), _random.Range().Closed(size.z));
		}

		public Vector3 PointWithinRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return _random.Unit().ClosedFloat() * axis0 + _random.Unit().ClosedFloat() * axis1 + _random.Unit().ClosedFloat() * axis2;
		}

		#endregion
	}

	public static class RandomVectorExtensions
	{
		public static RandomVector Vector(this IRandomEngine random)
		{
			return new RandomVector(random);
		}
	}
}
