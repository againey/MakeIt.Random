/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random quaternions various rotational qualities.
	/// </summary>
	public static class RandomQuaternion
	{
		/// <summary>
		/// Generates a random unit quaternion, selected from a uniform distribution of all possible 3-dimensional rotations or orientations.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random unit quaternion.</returns>
		public static Quaternion Rotation(this IRandom random)
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
			return new Quaternion(u1, v1, u2 * t, v2 * t);
		}
	}
}
