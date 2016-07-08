/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomRadial3D
	{
		public static Vector3 UnitVector(IRandomEngine engine)
		{
			var longitude = RandomRange.HalfOpen(0f, Mathf.PI * 2f, engine);
			var z = RandomRange.Closed(-1f, +1f, engine);
			var invertedZ = Mathf.Sqrt(1f - z * z);
			return new Vector3(invertedZ * Mathf.Cos(longitude), invertedZ * Mathf.Sin(longitude), z);
		}

		public static Vector3 ScaledVector(float radius, IRandomEngine engine)
		{
			return UnitVector(engine) * radius;
		}

		#region Open

		public static Vector3 PointWithinOpenSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.OpenFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinOpenSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.OpenFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinOpenSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(RandomUnit.OpenFloat(engine) * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region HalfOpen

		public static Vector3 PointWithinHalfOpenSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfOpenFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfOpenSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfOpenFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfOpenSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(RandomUnit.HalfOpenFloat(engine) * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region HalfClosed

		public static Vector3 PointWithinHalfClosedSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfClosedFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfClosedSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfClosedFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfClosedSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(RandomUnit.HalfClosedFloat(engine) * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region Closed

		public static Vector3 PointWithinClosedSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinClosedSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinClosedSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitMinPow3 = unitMin * unitMin * unitMin;
			var unitRange = 1f - unitMinPow3;
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine) * unitRange + unitMinPow3, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion
	}
}
