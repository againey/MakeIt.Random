/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// A utility class for providing more advanced random number generation on top of a basic random engine.
	/// </summary>
	/// <remarks>
	/// <para>This class can be used both as a static utility class, accepting a random engine parameter for each
	/// of its functions, or as an instantiated class which stores a reference to a random engine supplied at
	/// construction, obviating the need to pass the random engine to the class methods.</para>
	/// </remarks>
	public sealed class RandomUtility
	{
		private IRandomEngine _engine;

		public RandomUtility(IRandomEngine engine)
		{
			if (engine == null) throw new System.ArgumentNullException("engine");
			_engine = engine;
		}

		public IRandomEngine engine { get { return _engine; } }

		#region HalfOpenRange

		#region int

		public static int HalfOpenRange(int lowerInclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public int HalfOpenRange(int lowerInclusive, int upperExclusive)
		{
			return HalfOpenRange(lowerInclusive, upperExclusive, _engine);
		}

		public static int HalfOpenRange(int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)upperExclusive);
		}

		public int HalfOpenRange(int upperExclusive)
		{
			return HalfOpenRange(upperExclusive, _engine);
		}

		#endregion

		#region uint

		public static uint HalfOpenRange(uint lowerInclusive, uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public uint HalfOpenRange(uint lowerInclusive, uint upperExclusive)
		{
			return HalfOpenRange(lowerInclusive, upperExclusive, _engine);
		}

		public static uint HalfOpenRange(uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive);
		}

		public uint HalfOpenRange(uint upperExclusive)
		{
			return HalfOpenRange(upperExclusive, _engine);
		}

		#endregion

		#region float

		public static float HalfOpenRange(float lowerInclusive, float upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerInclusive) * HalfOpenFloatUnit(engine) + lowerInclusive;
		}

		public float HalfOpenRange(float lowerInclusive, float upperExclusive)
		{
			return HalfOpenRange(lowerInclusive, upperExclusive, _engine);
		}

		public static float HalfOpenRange(float upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * HalfOpenFloatUnit(engine);
		}

		public float HalfOpenRange(float upperExclusive)
		{
			return HalfOpenRange(upperExclusive, _engine);
		}

		#endregion

		#region double

		public static double HalfOpenRange(double lowerInclusive, double upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerInclusive) * HalfOpenDoubleUnit(engine) + lowerInclusive;
		}

		public double HalfOpenRange(double lowerInclusive, double upperExclusive)
		{
			return HalfOpenRange(lowerInclusive, upperExclusive, _engine);
		}

		public static double HalfOpenRange(double upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * HalfOpenDoubleUnit(engine);
		}

		public double HalfOpenRange(double upperExclusive)
		{
			return HalfOpenRange(upperExclusive, _engine);
		}

		#endregion

		#endregion

		#region HalfOpenUnit

		public static float HalfOpenFloatUnit(IRandomEngine engine)
		{
			return (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)engine.Next32() << 29))) - 1.0f;
		}

		public float HalfOpenFloatUnit()
		{
			return HalfOpenFloatUnit(_engine);
		}

		public static double HalfOpenDoubleUnit(IRandomEngine engine)
		{
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & engine.Next64())) - 1.0;
		}

		public double HalfOpenDoubleUnit()
		{
			return HalfOpenDoubleUnit(_engine);
		}

		#endregion

		#region ClosedRange

		#region int

		public static int ClosedRange(int lowerInclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThanOrEqual((uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public int ClosedRange(int lowerInclusive, int upperInclusive)
		{
			return ClosedRange(lowerInclusive, upperInclusive, _engine);
		}

		public static int ClosedRange(int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThanOrEqual((uint)upperInclusive);
		}

		public int ClosedRange(int upperInclusive)
		{
			return ClosedRange(upperInclusive, _engine);
		}

		#endregion

		#region uint

		public static uint ClosedRange(uint lowerInclusive, uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public uint ClosedRange(uint lowerInclusive, uint upperInclusive)
		{
			return ClosedRange(lowerInclusive, upperInclusive, _engine);
		}

		public static uint ClosedRange(uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive);
		}

		public uint ClosedRange(uint upperExclusive)
		{
			return ClosedRange(upperExclusive, _engine);
		}

		#endregion

		#region float

		public static float ClosedRange(float lowerInclusive, float upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * ClosedFloatUnit(engine) + lowerInclusive;
		}

		public float ClosedRange(float lowerInclusive, float upperInclusive)
		{
			return ClosedRange(lowerInclusive, upperInclusive, _engine);
		}

		public static float ClosedRange(float upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * ClosedFloatUnit(engine);
		}

		public float ClosedRange(float upperInclusive)
		{
			return ClosedRange(upperInclusive, _engine);
		}

		#endregion

		#region double

		public static double ClosedRange(double lowerInclusive, double upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * ClosedDoubleUnit(engine) + lowerInclusive;
		}

		public double ClosedRange(double lowerInclusive, double upperInclusive)
		{
			return ClosedRange(lowerInclusive, upperInclusive, _engine);
		}

		public static double ClosedRange(double upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * ClosedDoubleUnit(engine);
		}

		public double ClosedRange(double upperInclusive)
		{
			return ClosedRange(upperInclusive, _engine);
		}

		#endregion

		#endregion

		#region ClosedUnit

		public static float ClosedFloatUnit(IRandomEngine engine)
		{
			var random = engine.NextLessThanOrEqual(0x00800000U);
			return (random != 0x00800000U) ? (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)random << 29))) - 1.0f : 1.0f;
		}

		public float ClosedFloatUnit()
		{
			return ClosedFloatUnit(_engine);
		}

		public static double ClosedDoubleUnit(IRandomEngine engine)
		{
			var random = engine.NextLessThanOrEqual(0x0010000000000000UL);
			return (random != 0x0010000000000000UL) ? System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & random)) - 1.0 : 1.0;
		}

		public double ClosedDoubleUnit()
		{
			return ClosedDoubleUnit(_engine);
		}

		#endregion

		#region Circular/Spherical

		public static Vector2 UnitVector2(IRandomEngine engine)
		{
			var angle = HalfOpenRange(0f, Mathf.PI * 2f, engine);
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		}

		public Vector2 UnitVector2()
		{
			return UnitVector2(_engine);
		}

		public static Vector2 CircleVector2(IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(ClosedFloatUnit(engine));
			return UnitVector2(engine) * distance;
		}

		public Vector2 CircleVector2()
		{
			return CircleVector2(_engine);
		}

		public static Vector2 CircleVector2(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Sqrt(ClosedFloatUnit(engine)) * radius;
			return UnitVector2(engine) * distance;
		}

		public Vector2 CircleVector2(float radius)
		{
			return CircleVector2(radius, _engine);
		}

		public static Vector2 RingVector2(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Sqrt(ClosedFloatUnit(engine) * unitRange + unitMin) * outerRadius;
			return UnitVector2(engine) * distance;
		}

		public Vector2 RingVector2(float innerRadius, float outerRadius)
		{
			return RingVector2(innerRadius, outerRadius, _engine);
		}

		public static Vector3 UnitVector3(IRandomEngine engine)
		{
			var longitude = HalfOpenRange(0f, Mathf.PI * 2f, engine);
			var latitude = Mathf.Acos(HalfOpenRange(-1f, +1f, engine));
			var cosineLatitude = Mathf.Cos(latitude);
			return new Vector3(Mathf.Cos(longitude) * cosineLatitude, Mathf.Sin(latitude), Mathf.Sin(longitude) * cosineLatitude);
		}

		public Vector3 UnitVector3()
		{
			return UnitVector3(_engine);
		}

		public static Vector3 SphereVector3(IRandomEngine engine)
		{
			var distance = Mathf.Pow(ClosedFloatUnit(engine), 1f / 3f);
			return UnitVector3(engine) * distance;
		}

		public Vector3 SphereVector3()
		{
			return SphereVector3(_engine);
		}

		public static Vector3 SphereVector3(float radius, IRandomEngine engine)
		{
			var distance = Mathf.Pow(ClosedFloatUnit(engine), 1f / 3f) * radius;
			return UnitVector3(engine) * distance;
		}

		public Vector3 SphereVector3(float radius)
		{
			return SphereVector3(radius, _engine);
		}

		public static Vector3 ShellVector3(float innerRadius, float outerRadius, IRandomEngine engine)
		{
			var unitMin = innerRadius / outerRadius;
			var unitRange = 1f - unitMin;
			var distance = Mathf.Pow(ClosedFloatUnit(engine) * unitRange + unitMin, 1f / 3f) * outerRadius;
			return UnitVector3(engine) * distance;
		}

		public Vector3 ShellVector3(float innerRadius, float outerRadius)
		{
			return ShellVector3(innerRadius, outerRadius, _engine);
		}

		#endregion

		#region Rectangular/Cubic

		public static Vector2 HalfOpenUnitSquare(IRandomEngine engine)
		{
			return new Vector2(HalfOpenFloatUnit(engine), HalfOpenFloatUnit(engine));
		}

		public Vector2 HalfOpenUnitSquare()
		{
			return HalfOpenUnitSquare(_engine);
		}

		public static Vector2 ClosedUnitSquare(IRandomEngine engine)
		{
			return new Vector2(ClosedFloatUnit(engine), ClosedFloatUnit(engine));
		}

		public Vector2 ClosedUnitSquare()
		{
			return ClosedUnitSquare(_engine);
		}

		public static Vector2 HalfOpenSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(HalfOpenRange(sideLength, engine), HalfOpenRange(sideLength, engine));
		}

		public Vector2 HalfOpenSquare(float sideLength)
		{
			return HalfOpenSquare(sideLength, _engine);
		}

		public static Vector2 ClosedSquare(float sideLength, IRandomEngine engine)
		{
			return new Vector2(ClosedRange(sideLength, engine), ClosedRange(sideLength, engine));
		}

		public Vector2 ClosedSquare(float sideLength)
		{
			return ClosedSquare(sideLength, _engine);
		}

		public static Vector2 HalfOpenRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(HalfOpenRange(size.x, engine), HalfOpenRange(size.y, engine));
		}

		public Vector2 HalfOpenRectangle(Vector2 size)
		{
			return HalfOpenRectangle(size, _engine);
		}

		public static Vector2 ClosedRectangle(Vector2 size, IRandomEngine engine)
		{
			return new Vector2(ClosedRange(size.x, engine), ClosedRange(size.y, engine));
		}

		public Vector2 ClosedRectangle(Vector2 size)
		{
			return ClosedRectangle(size, _engine);
		}

		public static Vector2 HalfOpenParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return HalfOpenFloatUnit(engine) * axis0 + HalfOpenFloatUnit(engine) * axis1;
		}

		public Vector2 HalfOpenParallelogram(Vector2 axis0, Vector2 axis1)
		{
			return HalfOpenParallelogram(axis0, axis1, _engine);
		}

		public static Vector2 ClosedParallelogram(Vector2 axis0, Vector2 axis1, IRandomEngine engine)
		{
			return ClosedFloatUnit(engine) * axis0 + ClosedFloatUnit(engine) * axis1;
		}

		public Vector2 ClosedParallelogram(Vector2 axis0, Vector2 axis1)
		{
			return ClosedParallelogram(axis0, axis1, _engine);
		}

		public static Vector3 HalfOpenUnitCube(IRandomEngine engine)
		{
			return new Vector3(HalfOpenFloatUnit(engine), HalfOpenFloatUnit(engine), HalfOpenFloatUnit(engine));
		}

		public Vector3 HalfOpenUnitCube()
		{
			return HalfOpenUnitCube(_engine);
		}

		public static Vector3 ClosedUnitCube(IRandomEngine engine)
		{
			return new Vector3(ClosedFloatUnit(engine), ClosedFloatUnit(engine), ClosedFloatUnit(engine));
		}

		public Vector3 ClosedUnitCube()
		{
			return ClosedUnitCube(_engine);
		}

		public static Vector3 HalfOpenCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(HalfOpenRange(sideLength, engine), HalfOpenRange(sideLength, engine), HalfOpenRange(sideLength, engine));
		}

		public Vector3 HalfOpenCube(float sideLength)
		{
			return HalfOpenCube(sideLength, _engine);
		}

		public static Vector3 ClosedCube(float sideLength, IRandomEngine engine)
		{
			return new Vector3(ClosedRange(sideLength, engine), ClosedRange(sideLength, engine), ClosedRange(sideLength, engine));
		}

		public Vector3 ClosedCube(float sideLength)
		{
			return ClosedCube(sideLength, _engine);
		}

		public static Vector3 HalfOpenRectangularCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(HalfOpenRange(size.x, engine), HalfOpenRange(size.y, engine), HalfOpenRange(size.z, engine));
		}

		public Vector3 HalfOpenRectangularCuboid(Vector3 size)
		{
			return HalfOpenRectangularCuboid(size, _engine);
		}

		public static Vector3 ClosedRectangularCuboid(Vector3 size, IRandomEngine engine)
		{
			return new Vector3(ClosedRange(size.x, engine), ClosedRange(size.y, engine), ClosedRange(size.z, engine));
		}

		public Vector3 ClosedRectangularCuboid(Vector3 size)
		{
			return ClosedRectangularCuboid(size, _engine);
		}

		public static Vector3 HalfOpenRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return HalfOpenFloatUnit(engine) * axis0 + HalfOpenFloatUnit(engine) * axis1 + HalfOpenFloatUnit(engine) * axis2;
		}

		public Vector3 HalfOpenRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return HalfOpenRhomboid(axis0, axis1, axis2, _engine);
		}

		public static Vector3 ClosedRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2, IRandomEngine engine)
		{
			return ClosedFloatUnit(engine) * axis0 + ClosedFloatUnit(engine) * axis1 + ClosedFloatUnit(engine) * axis2;
		}

		public Vector3 ClosedRhomboid(Vector3 axis0, Vector3 axis1, Vector3 axis2)
		{
			return ClosedRhomboid(axis0, axis1, axis2, _engine);
		}

		#endregion

		#region Chance

		public static bool Chance(int ratioNumerator, int ratioDenominator, IRandomEngine engine)
		{
			return HalfOpenRange(ratioDenominator, engine) < ratioNumerator;
		}

		public bool Chance(int ratioNumerator, int ratioDenominator)
		{
			return Chance(ratioNumerator, ratioDenominator, _engine);
		}

		public static bool Chance(uint ratioNumerator, uint ratioDenominator, IRandomEngine engine)
		{
			return HalfOpenRange(ratioDenominator, engine) < ratioNumerator;
		}

		public bool Chance(uint ratioNumerator, uint ratioDenominator)
		{
			return Chance(ratioNumerator, ratioDenominator, _engine);
		}

		public static bool Chance(float probability, IRandomEngine engine)
		{
			return HalfOpenFloatUnit(engine) < probability;
		}

		public bool Chance(float probability)
		{
			return Chance(probability, _engine);
		}

		public static bool Chance(double probability, IRandomEngine engine)
		{
			return HalfOpenDoubleUnit(engine) < probability;
		}

		public bool Chance(double probability)
		{
			return Chance(probability, _engine);
		}

		#endregion

		#region Miscellaneous

		private static char[] _hexadecimalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',
		};

		public static string HexadecimalString(int length, IRandomEngine engine)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = _hexadecimalCharacters[engine.Next32(4)];
			}
			return new string(buffer);
		}

		public string HexadecimalString(int length)
		{
			return HexadecimalString(length, _engine);
		}

		#endregion
	}
}
