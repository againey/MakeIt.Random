/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomRange3D
	{
		#region Open

		public static Vector3 PointWithinOpenCube()
		{
			return PointWithinOpenCube(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinOpenCube(IRandomEngine engine)
		{
			return new Vector3(RandomUnit.OpenFloat(engine), RandomUnit.OpenFloat(engine), RandomUnit.OpenFloat(engine));
		}

		public static Vector3 PointWithinOpenCube(float sideLength)
		{
			return PointWithinOpenCube(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinOpenCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(RandomRange.Open(sideLength, engine), RandomRange.Open(sideLength, engine), RandomRange.Open(sideLength, engine));
		}

		public static Vector3 PointWithinOpenCuboid(Vector3 size)
		{
			return PointWithinOpenCuboid(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinOpenCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(RandomRange.Open(size.x, engine), RandomRange.Open(size.y, engine), RandomRange.Open(size.z, engine));
		}

		public static Vector3 PointWithinOpenRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return PointWithinOpenRhomboid(axis0, axis1, axis2, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinOpenRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return RandomUnit.OpenFloat(engine) * axis0 + RandomUnit.OpenFloat(engine) * axis1 + RandomUnit.OpenFloat(engine) * axis2;
		}

		#endregion

		#region HalfOpen

		public static Vector3 PointWithinHalfOpenCube()
		{
			return PointWithinHalfOpenCube(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfOpenCube(IRandomEngine engine)
		{
			return new Vector3(RandomUnit.HalfOpenFloat(engine), RandomUnit.HalfOpenFloat(engine), RandomUnit.HalfOpenFloat(engine));
		}

		public static Vector3 PointWithinHalfOpenCube(float sideLength)
		{
			return PointWithinHalfOpenCube(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfOpenCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(RandomRange.HalfOpen(sideLength, engine), RandomRange.HalfOpen(sideLength, engine), RandomRange.HalfOpen(sideLength, engine));
		}

		public static Vector3 PointWithinHalfOpenCuboid(Vector3 size)
		{
			return PointWithinHalfOpenCuboid(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfOpenCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(RandomRange.HalfOpen(size.x, engine), RandomRange.HalfOpen(size.y, engine), RandomRange.HalfOpen(size.z, engine));
		}

		public static Vector3 PointWithinHalfOpenRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return PointWithinHalfOpenRhomboid(axis0, axis1, axis2, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfOpenRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return RandomUnit.HalfOpenFloat(engine) * axis0 + RandomUnit.HalfOpenFloat(engine) * axis1 + RandomUnit.HalfOpenFloat(engine) * axis2;
		}

		#endregion

		#region HalfClosed

		public static Vector3 PointWithinHalfClosedCube()
		{
			return PointWithinHalfClosedCube(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfClosedCube(IRandomEngine engine)
		{
			return new Vector3(RandomUnit.HalfClosedFloat(engine), RandomUnit.HalfClosedFloat(engine), RandomUnit.HalfClosedFloat(engine));
		}

		public static Vector3 PointWithinHalfClosedCube(float sideLength)
		{
			return PointWithinHalfClosedCube(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfClosedCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(RandomRange.HalfClosed(sideLength, engine), RandomRange.HalfClosed(sideLength, engine), RandomRange.HalfClosed(sideLength, engine));
		}

		public static Vector3 PointWithinHalfClosedCuboid(Vector3 size)
		{
			return PointWithinHalfClosedCuboid(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfClosedCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(RandomRange.HalfClosed(size.x, engine), RandomRange.HalfClosed(size.y, engine), RandomRange.HalfClosed(size.z, engine));
		}

		public static Vector3 PointWithinHalfClosedRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return PointWithinHalfClosedRhomboid(axis0, axis1, axis2, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinHalfClosedRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return RandomUnit.HalfClosedFloat(engine) * axis0 + RandomUnit.HalfClosedFloat(engine) * axis1 + RandomUnit.HalfClosedFloat(engine) * axis2;
		}

		#endregion

		#region Closed

		public static Vector3 PointWithinClosedCube()
		{
			return PointWithinClosedCube(DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinClosedCube(IRandomEngine engine)
		{
			return new Vector3(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static Vector3 PointWithinClosedCube(float sideLength)
		{
			return PointWithinClosedCube(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinClosedCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(RandomRange.Closed(sideLength, engine), RandomRange.Closed(sideLength, engine), RandomRange.Closed(sideLength, engine));
		}

		public static Vector3 PointWithinClosedCuboid(Vector3 size)
		{
			return PointWithinClosedCuboid(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinClosedCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(RandomRange.Closed(size.x, engine), RandomRange.Closed(size.y, engine), RandomRange.Closed(size.z, engine));
		}

		public static Vector3 PointWithinClosedRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return PointWithinClosedRhomboid(axis0, axis1, axis2, DefaultRandomEngine.sharedInstance);
		}

		public static Vector3 PointWithinClosedRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return RandomUnit.ClosedFloat(engine) * axis0 + RandomUnit.ClosedFloat(engine) * axis1 + RandomUnit.ClosedFloat(engine) * axis2;
		}

		#endregion
	}
}
