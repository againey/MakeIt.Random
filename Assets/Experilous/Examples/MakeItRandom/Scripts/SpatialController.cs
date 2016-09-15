/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using Experilous.MakeItRandom;

namespace Experilous.Examples.MakeItRandom
{
	public class SpatialController : MonoBehaviour
	{
		public Transform pointObjectPrefab;
		public new ParticleSystem particleSystem;

		public Color baseColor = new Color(0.6f, 0.3f, 0.1f);
		public float hueRange = 0.1f;
		public float saturationRange = 0.4f;
		public float valueRange = 0.4f;

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

			PositionAtUnitVectors3D();

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

		private void Set2D()
		{
			particleSystem.SetParticles(_particles, _particles.Length);
			particleSystem.Play();
			particleSystem.Pause();

			_flat = true;
		}

		private void Set3D()
		{
			particleSystem.SetParticles(_particles, _particles.Length);
			particleSystem.Play();
			particleSystem.Pause();

			_flat = false;
		}

		public void PositionAtUnitVectors2D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.UnitVector2();
				_particles[i].rotation = 0f;
			}

			Set2D();
		}

		public void PositionWithinCircle2D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinCircle();
				_particles[i].rotation = 0f;
			}

			Set2D();
		}

		public void PositionWithinThinShell2D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinCircularShell(1f - narrowShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set2D();
		}

		public void PositionWithinThickShell2D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinCircularShell(1f - wideShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set2D();
		}

		public void PositionAtUnitVectors3D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.UnitVector3();
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void PositionWithinSphere3D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSphere();
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void PositionWithinThinShell3D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSphericalShell(1f - narrowShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void PositionWithinThickShell3D()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSphericalShell(1f - wideShellThickness, 1f);
				_particles[i].rotation = 0f;
			}

			Set3D();
		}

		public void PositionWithRandomRotation()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				float angle;
				Vector3 axis;
				Quaternion rotation = _random.Rotation();
				rotation.ToAngleAxis(out angle, out axis);
				_particles[i].position = axis.normalized;
				_particles[i].rotation = angle;
			}

			Set3D();
		}

		public void PositionWithinSquare()
		{
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinSquare(2f) - Vector2.one;
				_particles[i].rotation = 0f;
			}

			Set2D();
		}

		public void PositionWithinRectangle()
		{
			Vector2 size = new Vector2(_random.RangeCC(1f, 2f), _random.RangeCC(1f, 2f));
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinRectangle(size) - size * 0.5f;
				_particles[i].rotation = 0f;
			}

			Set2D();
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
			axis0 *= _random.RangeCC(1f, 2f);
			axis1 *= _random.RangeCC(1f, 2f);
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinParallelogram(axis0, axis1) - (axis0 + axis1) * 0.5f;
				_particles[i].rotation = 0f;
			}

			Set2D();
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
			axis0 *= _random.RangeCC(1f, 2f);
			axis1 *= _random.RangeCC(1f, 2f);
			Vector2 offset = (axis0 + axis1) / 3f;
			for (int i = 0; i < _particles.Length; ++i)
			{
				_particles[i].position = _random.PointWithinTriangle(axis0, axis1) - offset;
				_particles[i].rotation = 0f;
			}

			Set2D();
		}
	}
}
