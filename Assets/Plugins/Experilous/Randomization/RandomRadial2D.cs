/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomRadial2D
	{
		public static Vector2 UnitVector(IRandomEngine engine)
		{
			var angle = RandomRange.HalfOpen(0f, Mathf.PI * 2f, engine);
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		}

		public static Vector2 ScaledVector(float radius, IRandomEngine engine)
		{
			return UnitVector(engine) * radius;
		}

		#region Open

		public static Vector2 PointWithinOpenCircle(IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.OpenFloat(engine));
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinOpenCircle(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.OpenFloat(engine)) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinOpenCircularShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Sqrt(RandomUnit.OpenFloat(engine) * unitRange + unitMin) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region HalfOpen

		public static Vector2 PointWithinHalfOpenCircle(IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.HalfOpenFloat(engine));
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinHalfOpenCircle(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.HalfOpenFloat(engine)) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinHalfOpenCircularShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Sqrt(RandomUnit.HalfOpenFloat(engine) * unitRange + unitMin) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region HalfClosed

		public static Vector2 PointWithinHalfClosedCircle(IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.HalfClosedFloat(engine));
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinHalfClosedCircle(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.HalfClosedFloat(engine)) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinHalfClosedCircularShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Sqrt(RandomUnit.HalfClosedFloat(engine) * unitRange + unitMin) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion

		#region Closed

		public static Vector2 PointWithinClosedCircle(IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloat(engine));
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinClosedCircle(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloat(engine)) * radius;
			return ScaledVector(distance, engine);
		}

		public static Vector2 PointWithinClosedCircularShell(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Sqrt(RandomUnit.ClosedFloat(engine) * unitRange + unitMin) * outerRadius;
			return ScaledVector(distance, engine);
		}

		#endregion
	}
}
