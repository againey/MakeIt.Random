/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomRadial3D
	{
		public static Vector3 UnitVector()
		{
			return UnitVector(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 UnitVector(IRandomEngine engine)
		{
			var longitude = RandomRange.HalfOpen(0f, Mathf.PI * 2f, engine);
			var latitude = Mathf.Acos(RandomRange.HalfOpen(-1f, +1f, engine));
			var cosineLatitude = Mathf.Cos(latitude);
			return new Vector3(Mathf.Cos(longitude) * cosineLatitude, Mathf.Sin(latitude), Mathf.Sin(longitude) * cosineLatitude);
		}

		public static Vector3 ScaledVector(float radius)
		{
			return UnitVector(DefaultRandomEngine.sharedInstance) * radius;
		}

		public static Vector3 ScaledVector(float radius, IRandomEngine engine)
		{
			return UnitVector(engine) * radius;
		}

		#region Open

		public static Vector3 PointWithinOpenSphere()
		{
			return PointWithinOpenSphere(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinOpenSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.OpenFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinOpenSphere(float radius)
		{
			return PointWithinOpenSphere(radius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinOpenSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.OpenFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinOpenSphericalShell(float innerRadius, float outerRadius)
		{
			return PointWithinOpenSphericalShell(innerRadius, outerRadius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinOpenSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Pow(RandomUnit.OpenFloat(engine) * unitRange + unitMin, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region HalfOpen

		public static Vector3 PointWithinHalfOpenSphere()
		{
			return PointWithinHalfOpenSphere(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfOpenSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfOpenFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfOpenSphere(float radius)
		{
			return PointWithinHalfOpenSphere(radius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfOpenSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfOpenFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfOpenSphericalShell(float innerRadius, float outerRadius)
		{
			return PointWithinHalfOpenSphericalShell(innerRadius, outerRadius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfOpenSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Pow(RandomUnit.HalfOpenFloat(engine) * unitRange + unitMin, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region HalfClosed

		public static Vector3 PointWithinHalfClosedSphere()
		{
			return PointWithinHalfClosedSphere(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfClosedSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfClosedFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfClosedSphere(float radius)
		{
			return PointWithinHalfClosedSphere(radius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfClosedSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.HalfClosedFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinHalfClosedSphericalShell(float innerRadius, float outerRadius)
		{
			return PointWithinHalfClosedSphericalShell(innerRadius, outerRadius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfClosedSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Pow(RandomUnit.HalfClosedFloat(engine) * unitRange + unitMin, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region Closed

		public static Vector3 PointWithinClosedSphere()
		{
			return PointWithinClosedSphere(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinClosedSphere(IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine), 1f / 3f);
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinClosedSphere(float radius)
		{
			return PointWithinClosedSphere(radius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinClosedSphere(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine), 1f / 3f) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector3 PointWithinClosedSphericalShell(float innerRadius, float outerRadius)
		{
			return PointWithinClosedSphericalShell(innerRadius, outerRadius, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinClosedSphericalShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Pow(RandomUnit.ClosedFloat(engine) * unitRange + unitMin, 1f / 3f) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion
	}
}
