/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomRange2D
	{
		#region Open

		public static Vector2 PointWithinOpenSquare()
		{
			return PointWithinOpenSquare(DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinOpenSquare(IRandomEngine engine)
		{
			return new Vector2(RandomUnit.OpenFloat(engine), RandomUnit.OpenFloat(engine));
		}

		public static Vector2 PointWithinOpenSquare(float sideLength)
		{
			return PointWithinOpenSquare(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinOpenSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(RandomRange.Open(sideLength, engine), RandomRange.Open(sideLength, engine));
		}

		public static Vector2 PointWithinOpenRectangle(Vector2 size)
		{
			return PointWithinOpenRectangle(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinOpenRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(RandomRange.Open(size.x, engine), RandomRange.Open(size.y, engine));
		}

		public static Vector2 PointWithinOpenParallelogram(Vector2 axis0, Vector2 axis1)
		{
			return PointWithinOpenParallelogram(axis0, axis1, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinOpenParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return RandomUnit.OpenFloat(engine) * axis0 + RandomUnit.OpenFloat(engine) * axis1;
		}

		#endregion

		#region HalfOpen

		public static Vector2 PointWithinHalfOpenSquare()
		{
			return PointWithinHalfOpenSquare(DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfOpenSquare(IRandomEngine engine)
		{
			return new Vector2(RandomUnit.HalfOpenFloat(engine), RandomUnit.HalfOpenFloat(engine));
		}

		public static Vector2 PointWithinHalfOpenSquare(float sideLength)
		{
			return PointWithinHalfOpenSquare(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfOpenSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(RandomRange.HalfOpen(sideLength, engine), RandomRange.HalfOpen(sideLength, engine));
		}

		public static Vector2 PointWithinHalfOpenRectangle(Vector2 size)
		{
			return PointWithinHalfOpenRectangle(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfOpenRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(RandomRange.HalfOpen(size.x, engine), RandomRange.HalfOpen(size.y, engine));
		}

		public static Vector2 PointWithinHalfOpenParallelogram(Vector2 axis0, Vector2 axis1)
		{
			return PointWithinHalfOpenParallelogram(axis0, axis1, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfOpenParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return RandomUnit.HalfOpenFloat(engine) * axis0 + RandomUnit.HalfOpenFloat(engine) * axis1;
		}

		#endregion

		#region HalfClosed

		public static Vector2 PointWithinHalfClosedSquare()
		{
			return PointWithinHalfClosedSquare(DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfClosedSquare(IRandomEngine engine)
		{
			return new Vector2(RandomUnit.HalfClosedFloat(engine), RandomUnit.HalfClosedFloat(engine));
		}

		public static Vector2 PointWithinHalfClosedSquare(float sideLength)
		{
			return PointWithinHalfClosedSquare(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfClosedSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(RandomRange.HalfClosed(sideLength, engine), RandomRange.HalfClosed(sideLength, engine));
		}

		public static Vector2 PointWithinHalfClosedRectangle(Vector2 size)
		{
			return PointWithinHalfClosedRectangle(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfClosedRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(RandomRange.HalfClosed(size.x, engine), RandomRange.HalfClosed(size.y, engine));
		}

		public static Vector2 PointWithinHalfClosedParallelogram(Vector2 axis0, Vector2 axis1)
		{
			return PointWithinHalfClosedParallelogram(axis0, axis1, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinHalfClosedParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return RandomUnit.HalfClosedFloat(engine) * axis0 + RandomUnit.HalfClosedFloat(engine) * axis1;
		}

		#endregion

		#region Closed

		public static Vector2 PointWithinClosedSquare()
		{
			return PointWithinClosedSquare(DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinClosedSquare(IRandomEngine engine)
		{
			return new Vector2(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static Vector2 PointWithinClosedSquare(float sideLength)
		{
			return PointWithinClosedSquare(sideLength, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinClosedSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(RandomRange.Closed(sideLength, engine), RandomRange.Closed(sideLength, engine));
		}

		public static Vector2 PointWithinClosedRectangle(Vector2 size)
		{
			return PointWithinClosedRectangle(size, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinClosedRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(RandomRange.Closed(size.x, engine), RandomRange.Closed(size.y, engine));
		}

		public static Vector2 PointWithinClosedParallelogram(Vector2 axis0, Vector2 axis1)
		{
			return PointWithinClosedParallelogram(axis0, axis1, DefaultRandomEngine.sharedInstance);
		}

		public static Vector2 PointWithinClosedParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return RandomUnit.ClosedFloat(engine) * axis0 + RandomUnit.ClosedFloat(engine) * axis1;
		}

		#endregion
	}
}
