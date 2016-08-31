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
			Quaternion quat;
			random.Rotation(out quat);
			return quat;
		}

		/// <summary>
		/// Generates a random unit quaternion, selected from a uniform distribution of all possible 3-dimensional rotations or orientations.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random unit quaternion.</returns>
		public static void Rotation(this IRandom random, out Quaternion quat)
		{
			quat = Quaternion.AngleAxis(random.HalfAngleDegCO(), random.UnitVector3());
		}
	}
}
