/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using Experilous.MakeItRandom;

namespace Experilous.Examples.MakeItRandom
{
	public class SpatialController : MonoBehaviour
	{
		public Material pointMaterial;
		public Material pointOrientationMaterial;
		public new ParticleSystem particleSystem;

		public float narrowShellThickness = 0.2f;
		public float wideShellThickness = 0.5f;

		public float rotationSpeed = 1f;

		private XorShift128Plus _random;

		private ParticleSystem.Particle[] _particles;

		private bool _flat;
		private AnimationCurve _flatRotationRate;

		protected void Awake()
		{
			_random = XorShift128Plus.Create();

			_particles = new ParticleSystem.Particle[particleSystem.maxParticles];
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].angularVelocity = 0f;
				_particles[i].lifetime = float.PositiveInfinity;
#if UNITY_5_3_OR_NEWER
				_particles[i].startSize = 0.1f;
#else
				_particles[i].size = 0.1f;
#endif
				_particles[i].startLifetime = 0f;
				_particles[i].velocity = Vector3.zero;
			}

			OnToggleChanged_UnitVectors3D(true);

			_flatRotationRate = new AnimationCurve(
				new Keyframe(0f, 0f, 0f, 0f),
				new Keyframe(0.3f, 0f, 0f, 0f),
				new Keyframe(0.7f, 1f, 0f, 0f),
				new Keyframe(1f, 1f, 0f, 0f));
		}

		protected void Update()
		{
			float rotation = Time.time * rotationSpeed;
			if (_flat)
			{
				float t = Mathf.Repeat(rotation, 0.5f) * 2f;
				rotation = _flatRotationRate.Evaluate(t) * 0.5f + (Mathf.Repeat(rotation, 1f) < 0.5f ? 0f : 0.5f);
			}
			else
			{
				rotation = Mathf.Repeat(rotation, 1f);
			}
			particleSystem.transform.localRotation = Quaternion.AngleAxis(rotation * 360f, Vector3.up);
		}

		private void Set2D(int particleCount = 0, bool showOrientation = false)
		{
			particleSystem.SetParticles(_particles, particleCount > 0 ? particleCount : _particles.Length);

			var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
			renderer.material = showOrientation ? pointOrientationMaterial : pointMaterial;

			_flat = true;
		}

		private void Set3D(int particleCount = 0, bool showOrientation = false)
		{
			particleSystem.SetParticles(_particles, particleCount > 0 ? particleCount : _particles.Length);

			var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
			renderer.material = showOrientation ? pointOrientationMaterial : pointMaterial;

			_flat = false;
		}

		public void OnToggleChanged_UnitVectors2D(bool isOn)
		{
			if (!isOn) return;

			int particleCount = _particles.Length / 10;

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.UnitVector2();
				_particles[i].rotation = 0f;
			}

			Set2D(particleCount);
		}

		public void OnToggleChanged_WithinCircle2D(bool isOn)
		{
			if (!isOn) return;

			int particleCount = _particles.Length / 2;

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinCircle();
				_particles[i].rotation = 0f;
			}

			Set2D(particleCount);
		}

		public void OnToggleChanged_WithinThinShell2D(bool isOn)
		{
			if (!isOn) return;

			int particleCount = _particles.Length / 6;

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinCircularShell(1f - narrowShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set2D(particleCount);
		}

		public void OnToggleChanged_WithinThickShell2D(bool isOn)
		{
			if (!isOn) return;

			int particleCount = _particles.Length / 4;

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinCircularShell(1f - wideShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set2D(particleCount);
		}

		public void OnToggleChanged_UnitVectors3D(bool isOn)
		{
			if (!isOn) return;

			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.UnitVector3();
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void OnToggleChanged_WithinSphere3D(bool isOn)
		{
			if (!isOn) return;

			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSphere();
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void OnToggleChanged_WithinThinShell3D(bool isOn)
		{
			if (!isOn) return;

			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSphericalShell(1f - narrowShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void OnToggleChanged_WithinThickShell3D(bool isOn)
		{
			if (!isOn) return;

			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSphericalShell(1f - wideShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void OnToggleChanged_RandomRotations(bool isOn)
		{
			if (!isOn) return;

			for (int i = 0; i < _particles.Length; ++i)
			{
				float angle;
				Vector3 axis;
				Quaternion rotation = _random.Rotation();
				rotation.ToAngleAxis(out angle, out axis);
				_particles[i].position = axis.normalized;
				_particles[i].rotation = angle;
			}

			Set3D(0, true);
		}

		public void OnToggleChanged_WithinSquare(bool isOn)
		{
			if (!isOn) return;

			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSquare(2f) - Vector2.one;
				_particles[i].rotation = 0f;
			}

			Set2D();
		}

		public void OnToggleChanged_WithinRectangle(bool isOn)
		{
			if (!isOn) return;

			Vector2 size = new Vector2(_random.RangeCC(1f, 2f), _random.RangeCC(1f, 2f));

			float area = size.x * size.y;
			int particleCount = Mathf.Min(Mathf.FloorToInt(area / 4f * _particles.Length), _particles.Length);

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinRectangle(size) - size * 0.5f;
				_particles[i].rotation = 0f;
			}

			Set2D(particleCount);
		}

		public void OnToggleChanged_WithinParallelogram(bool isOn)
		{
			if (!isOn) return;

			Vector2 axis0 = _random.UnitVector2();
			Vector2 axis1 = _random.UnitVector2();
			float dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			while (dot < 0.2f || dot > 0.6f)
			{
				axis1 = _random.UnitVector2();
				dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			}
			axis0 *= _random.RangeCC(1f, 2f);
			axis1 *= _random.RangeCC(1f, 2f);

			float d0 = (axis0 + axis1).magnitude;
			float d1 = (axis0 - axis1).magnitude;
			float dMax = Mathf.Max(d0, d1);

			if (dMax > 2f)
			{
				float scale = 2f / dMax;
				axis0 *= scale;
				axis1 *= scale;
			}

			Vector2 axisC = (axis0 + axis1) * 0.5f;

			float area = Vector3.Cross(axis0, axis1).magnitude;
			int particleCount = Mathf.Min(Mathf.FloorToInt(area / 4f * _particles.Length), _particles.Length);

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinParallelogram(axis0, axis1) - axisC;
				_particles[i].rotation = 0f;
			}

			Set2D(particleCount);
		}

		public void OnToggleChanged_WithinTriangle(bool isOn)
		{
			if (!isOn) return;

			Vector2 axis0 = _random.UnitVector2();
			Vector2 axis1 = _random.UnitVector2();
			float dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			while (dot < 0.2f || dot > 0.6f)
			{
				axis1 = _random.UnitVector2();
				dot = Mathf.Abs(Vector2.Dot(axis0, axis1));
			}
			axis0 *= _random.RangeCC(1f, 2f);
			axis1 *= _random.RangeCC(1f, 2f);

			float d0 = (axis0 + axis1).magnitude;
			float d1 = (axis0 - axis1).magnitude;
			float dMax = Mathf.Max(d0, d1);

			if (dMax > 2f)
			{
				float scale = 2f / dMax;
				axis0 *= scale;
				axis1 *= scale;
			}

			Vector2 axisC = (axis0 + axis1) * 0.33333333f;

			float area = Vector3.Cross(axis0, axis1).magnitude * 0.5f;
			int particleCount = Mathf.Min(Mathf.FloorToInt(area / 4f * _particles.Length), _particles.Length);

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinTriangle(axis0, axis1) - axisC;
				_particles[i].rotation = 0f;
			}

			Set2D(particleCount);
		}

		public void OnToggleChanged_WithinCube(bool isOn)
		{
			if (!isOn) return;

			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinCube(1.5f) - new Vector3(0.75f, 0.75f, 0.75f);
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void OnToggleChanged_WithinBox(bool isOn)
		{
			if (!isOn) return;

			Vector3 size = new Vector3(_random.RangeCC(0.25f, 2f), _random.RangeCC(0.25f, 2f), _random.RangeCC(0.25f, 2f));

			float area = size.x * size.y * size.z;
			int particleCount = Mathf.Min(Mathf.FloorToInt(area / 2.25f * _particles.Length), _particles.Length);

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinBox(size) - size * 0.5f;
				_particles[i].rotation = 0f;
			}

			Set3D(particleCount);
		}

		private static float Determinant(float a, float b, float c, float d)
		{
			return a * d - b * c;
		}

		private static float Determinant(Vector3 a, Vector3 b, Vector3 c)
		{
			return a.x * Determinant(b.y, b.z, c.y, c.z) - a.y * Determinant(b.x, b.z, c.x, c.z) + a.z * Determinant(b.x, b.y, c.x, c.y);
		}

		public void OnToggleChanged_WithinRhomboid(bool isOn)
		{
			if (!isOn) return;

			Vector3 axis0, axis1, axis2, axisC;
			float dot01, dot02, dot12;
			float dot0C, dot1C, dot2C;

			do
			{
				axis0 = _random.UnitVector3();
				axis1 = _random.UnitVector3();
				dot01 = Mathf.Abs(Vector3.Dot(axis0, axis1));
				while (dot01 < 0.2f || dot01 > 0.6f)
				{
					axis1 = _random.UnitVector3();
					dot01 = Mathf.Abs(Vector3.Dot(axis0, axis1));
				}
				axis2 = _random.UnitVector3();
				dot02 = Mathf.Abs(Vector3.Dot(axis0, axis2));
				dot12 = Mathf.Abs(Vector3.Dot(axis1, axis2));
				int tries = 0;
				while ((dot02 < 0.2f || dot02 > 0.6f || dot12 < 0.2f || dot12 > 0.6f) && tries < 100)
				{
					axis2 = _random.UnitVector3();
					dot02 = Mathf.Abs(Vector3.Dot(axis0, axis2));
					dot12 = Mathf.Abs(Vector3.Dot(axis1, axis2));
					++tries;
				}

				if (tries < 100)
				{
					axisC = (axis0 + axis1 + axis2).normalized;
					dot0C = Mathf.Abs(Vector3.Dot(axis0, axisC));
					dot1C = Mathf.Abs(Vector3.Dot(axis1, axisC));
					dot2C = Mathf.Abs(Vector3.Dot(axis2, axisC));
				}
				else
				{
					dot0C = dot1C = dot2C = 0f;
				}
			} while (dot0C < 0.4f || dot0C > 0.8f || dot1C < 0.4f || dot1C > 0.9f || dot2C < 0.4f || dot2C > 0.8f);

			axis0 *= _random.RangeCC(1f, 2f);
			axis1 *= _random.RangeCC(1f, 2f);
			axis2 *= _random.RangeCC(1f, 2f);

			float d0 = (axis0 + axis1 + axis2).magnitude;
			float d1 = (axis0 + axis1 - axis2).magnitude;
			float d2 = (axis0 - axis1 + axis2).magnitude;
			float d3 = (axis0 - axis1 - axis2).magnitude;
			float d4 = (-axis0 + axis1 + axis2).magnitude;
			float d5 = (-axis0 + axis1 - axis2).magnitude;
			float d6 = (-axis0 - axis1 + axis2).magnitude;
			float d7 = (-axis0 - axis1 - axis2).magnitude;
			float dMax = Mathf.Max(d0, d1, d2, d3, d4, d5, d6, d7);

			if (dMax > 2f)
			{
				float scale = 2f / dMax;
				axis0 *= scale;
				axis1 *= scale;
				axis2 *= scale;
			}

			axisC = (axis0 + axis1 + axis2) * 0.5f;

			float area = Mathf.Abs(Determinant(axis0, axis1, axis2));
			int particleCount = Mathf.Min(Mathf.FloorToInt(area / 2.25f * _particles.Length), _particles.Length);

			for (int i = 0; i < particleCount; ++i)
			{
				_particles[i].position = _random.PointWithinRhomboid(axis0, axis1, axis2) - axisC;
				_particles[i].rotation = 0f;
			}

			Set3D(particleCount);
		}
	}
}
