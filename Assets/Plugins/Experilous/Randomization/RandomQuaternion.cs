/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomQuaternion
	{
		public static Quaternion Rotation(IRandomEngine engine)
		{
			var theta0 = RandomRange.HalfOpen(0f, Mathf.PI * 2f, engine);
			var theta1 = Mathf.Acos(RandomRange.ClosedFast(-1f, +1f, engine));
			var theta2 = 0.5f * (RandomRange.HalfOpen(0f, Mathf.PI, engine) + Mathf.Asin(RandomUnit.ClosedFloatFast(engine)) + Mathf.PI * 0.5f);
			var sinTheta1 = Mathf.Sin(theta1);
			var sinTheta2 = Mathf.Sin(theta2);
			return new Quaternion(
				Mathf.Sin(theta0) * sinTheta1 * sinTheta2,
				Mathf.Cos(theta0) * sinTheta1 * sinTheta2,
				Mathf.Cos(theta1) * sinTheta2,
				Mathf.Cos(theta2));
		}
	}
}
