/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using Experilous.Randomization;

namespace Experilous.Examples.Randomization
{
	public class SpatialController : MonoBehaviour
	{
		public Transform pointObjectPrefab;

		public int pointObjectCount = 128;

		public Color baseColor = new Color(0.6f, 0.3f, 0.1f);
		public float hueRange = 0.1f;
		public float saturationRange = 0.4f;
		public float valueRange = 0.4f;

		public float narrowShellThickness = 0.2f;
		public float wideShellThickness = 0.5f;

		private XorShift128Plus _random;

		private Transform[] _pointObjects;

		protected void Awake()
		{
			_random = XorShift128Plus.Create();
			_pointObjects = new Transform[pointObjectCount];

			for (int i = 0; i < _pointObjects.Length; ++i)
			{
				_pointObjects[i] = Instantiate(pointObjectPrefab);
				_pointObjects[i].SetParent(transform, false);
			}
		}

		public void PositionAtUnitVectors2D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.UnitVector2();
			}
		}

		public void PositionWithinCircle2D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinCircle();
			}
		}

		public void PositionWithinThinShell2D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinCircularShell(1f - narrowShellThickness, 1f);
			}
		}

		public void PositionWithinThickShell2D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinCircularShell(1f - wideShellThickness, 1f);
			}
		}

		public void PositionAtUnitVectors3D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.UnitVector3();
			}
		}

		public void PositionWithinSphere3D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinSphere();
			}
		}

		public void PositionWithinThinShell3D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinSphericalShell(1f - narrowShellThickness, 1f);
			}
		}

		public void PositionWithinThickShell3D()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinSphericalShell(1f - wideShellThickness, 1f);
			}
		}

		public void PositionWithinSquare()
		{
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinSquare(2f) - Vector2.one;
			}
		}

		public void PositionWithinRectangle()
		{
			Vector2 size = new Vector2(_random.ClosedRange(1f, 2f), _random.ClosedRange(1f, 2f));
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinRectangle(size) - size * 0.5f;
			}
		}

		public void PositionWithinParallelogram()
		{
			Vector2 axis0 = _random.UnitVector2();
			Vector2 axis1 = _random.UnitVector2();
			float dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			while (dot < 0.4f || dot > 0.8f)
			{
				axis1 = _random.UnitVector2();
				dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			}
			axis0 *= _random.ClosedRange(1f, 2f);
			axis1 *= _random.ClosedRange(1f, 2f);
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinParallelogram(axis0, axis1) - (axis0 + axis1) * 0.5f;
			}
		}

		public void PositionWithinTriangle()
		{
			Vector2 axis0 = _random.UnitVector2();
			Vector2 axis1 = _random.UnitVector2();
			float dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			while (dot < 0.4f || dot > 0.8f)
			{
				axis1 = _random.UnitVector2();
				dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			}
			axis0 *= _random.ClosedRange(1f, 2f);
			axis1 *= _random.ClosedRange(1f, 2f);
			Vector2 offset = (axis0 + axis1) / 3f;
			foreach (var pointObject in _pointObjects)
			{
				pointObject.position = _random.PointWithinTriangle(axis0, axis1) - offset;
			}
		}
	}
}
