/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using Experilous.MakeItRandom;

namespace Experilous.Examples.MakeItRandom
{
	public class DistributionsController : MonoBehaviour
	{
		public RawImage image;

		public int maxSamplesPerUpdate = 10000;
		public float speedupRate = 2f;
		public float delayTime = 1f;
		public int textureWidth = 1024;
		public int textureHeight = 1024;
		public float range = 12f;
		public float mean = 0f;
		public float standardDeviation = 1f;

		public AnimationCurve curve;

		private Texture2D _texture;
		private Color32[] _pixels;

		private int[] _samples;
		private IRandom _random;

		private float _startTime;

		private static float Hermite(Vector2 p0, Vector2 p1, float m0, float m1, float x)
		{
			/*
			float dx = (p1.x - p0.x);
			float t = (x - p0.x) / dx;
			float t2 = t * t;
			float t3 = t2 * t;
			float h00 = 2f * t3 - 3f * t2 + 1f;
			float h10 = t3 - 2f * t2 + t;
			float h01 = -2f * t3 + 3f * t2;
			float h11 = t3 - t2;
			return h00 * p0.y + h10 * dx * m0 + h01 * p1.y + h11 * dx * m1;
			*/

			float xDelta = (p1.x - p0.x);
			float yDelta = (p1.y - p0.y);
			float t = (x - p0.x) / xDelta;
			float a = -2f * yDelta + (m0 + m1) * xDelta;
			float b = 3f * yDelta - (2f * m0 + m1) * xDelta;
			float c = m0 * xDelta;
			float d = p0.y;

			return ((a * t + b) * t + c) * t + d;
		}

		protected void Start()
		{
			/*for (int i = 0; i < curve.length; ++i)
			{
				var keyframe = curve[i];

				if (i > 0)
				{
					var prevKeyframe = curve[i - 1];

					Vector2 p0 = new Vector2(prevKeyframe.time, prevKeyframe.value);
					Vector2 p1 = new Vector2(keyframe.time, keyframe.value);
					float m0 = prevKeyframe.outTangent;
					float m1 = keyframe.inTangent;
					float t = Mathf.Lerp(prevKeyframe.time, keyframe.time, 0.25f);
					Debug.LogFormat("Compare at time t = {0:F6}; Unity:  {1:F6}  Hermite:  {2:F6}", t, curve.Evaluate(t), Hermite(p0, p1, m0, m1, t));
					t = Mathf.Lerp(prevKeyframe.time, keyframe.time, 0.5f);
					Debug.LogFormat("Compare at time t = {0:F6}; Unity:  {1:F6}  Hermite:  {2:F6}", t, curve.Evaluate(t), Hermite(p0, p1, m0, m1, t));
					t = Mathf.Lerp(prevKeyframe.time, keyframe.time, 0.75f);
					Debug.LogFormat("Compare at time t = {0:F6}; Unity:  {1:F6}  Hermite:  {2:F6}", t, curve.Evaluate(t), Hermite(p0, p1, m0, m1, t));
				}

				Debug.LogFormat("Keyframe[{0}] at ({1:F4}, {2:F4}), / {3:F4} > {4:F4} /", i, keyframe.time, keyframe.value, keyframe.inTangent, keyframe.outTangent);
			}*/

			_startTime = Time.time + delayTime;
			_random = MIRandom.CreateStandard();

			_samples = new int[textureWidth];
			_texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
			_texture.filterMode = FilterMode.Point;
			_pixels = new Color32[textureWidth * textureHeight];
			for (int i = 0; i < _pixels.Length; ++i)
			{
				_pixels[i] = new Color32(0, 0, 0, 0);
			}

			_texture.SetPixels32(_pixels);
			_texture.Apply(false, false);

			image.texture = _texture;
			image.GetComponent<AspectRatioFitter>().aspectRatio = (float)textureWidth / textureHeight;
		}

		protected void FixedUpdate()
		{
			if (Time.fixedTime < _startTime) return;

			int sampleCount = Mathf.CeilToInt(Mathf.Min(Mathf.Exp(speedupRate * (Time.fixedTime - _startTime)), maxSamplesPerUpdate));

			float scale = textureWidth / range;
			float offset = textureWidth / 2f;
			for (int i = 0; i < sampleCount; ++i)
			{
				//var sample = _random.NormalSample(0f, 1f);
				//var sample = _random.ExponentialSample(1f) - 2f;
				//var sample = _random.TriangularSample(-2f, 1f, 2f);
				//var sample = _random.TrapezoidalSample(-2f, 0.5f, 1.5f, 2f);
				//var sample = _random.LinearSample(-2f, 2f, +2f, 2f);
				//var sample = _random.PiecewiseLinearSample(new float[] { -3f, -1f, 0.5f, 2.5f, 3f }, new float[] { 1f, 1f, 1.5f, 0f, 1f });
				//var sample = _random.PiecewiseConstantSample(new float[] { -3f, -1f, 0.5f, 2.5f, 3f }, new float[] { 1f, 0f, 1.5f, 0.5f });
				//var sample = _random.HermiteSample(-2f, 2f, 0.5f, +2f, 1f, 2f);
				var sample = _random.PiecewiseCurveSample(curve);
				if (float.IsNaN(sample)) continue;
				var index = Mathf.Clamp(Mathf.RoundToInt(sample * scale + offset), -1, _samples.Length);
				if (index >=  0 && index < _samples.Length)
				{
					_samples[index] += 1;
				}
			}

			int maxSampleCount = 0;
			for (int i = 0; i < _samples.Length; ++i)
			{
				maxSampleCount = Mathf.Max(maxSampleCount, _samples[i]);
			}

			for (int y = 0; y < textureHeight; ++y)
			{
				int rowStart = y * textureWidth;
				for (int x = 0; x < textureWidth; ++x)
				{
					byte intensity = y * maxSampleCount < _samples[x] * textureHeight ? (byte)255 : (byte)0;
					_pixels[rowStart + x] = new Color32(intensity, intensity, intensity, 255);
				}
			}
		}

		protected void Update()
		{
			_texture.SetPixels32(_pixels);
			_texture.Apply(false, false);
		}
	}
}
