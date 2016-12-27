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
		public Text samplesPerSecondLabel;

		public int targetSamplesPerSecond = 1000000;
		public float maxFrameTime = 0.010f;
		public float speedupRate = 2f;
		public float startupDelayTime = 1f;
		public int textureWidth = 1024;
		public float aspectRatio = 2f;
		public int minSampleScale = 1024;

		[Header("Uniform")]
		public float uniformLower = -3f;
		public float uniformUpper = +3f;
		public float uniformViewCenter = 0f;
		public float uniformViewRange = 8f;

		[Header("Normal")]
		public float normalMean = 0f;
		public float normalStandardDeviation = 1f;
		public bool normalConstrained = false;
		public float normalMin = -3f;
		public float normalMax = +3f;
		public float normalViewCenter = 0f;
		public float normalViewRange = 8f;

		[Header("Exponential")]
		public float exponentialEventRage = 1f;
		public bool exponentialConstrained = false;
		public float exponentialMax = 6f;
		public float exponentialViewCenter = 4f;
		public float exponentialViewRange = 8f;

		[Header("Triangular")]
		public float triangularLower = -3f;
		public float triangularMode = +1f;
		public float triangularUpper = +3f;
		public float triangularViewCenter = 0f;
		public float triangularViewRange = 8f;

		[Header("Trapezoidal")]
		public float trapezoidalLower = -3f;
		public float trapezoidalLowerMode = 0f;
		public float trapezoidalUpperMode = +2f;
		public float trapezoidalUpper = +3f;
		public float trapezoidalViewCenter = 0f;
		public float trapezoidalViewRange = 8f;

		[Header("Linear")]
		public Vector2 linearPosition0 = new Vector2(-3f, 3f);
		public Vector2 linearPosition1 = new Vector2(+3f, 5f);
		public float linearViewCenter = 0f;
		public float linearViewRange = 8f;

		[Header("Hermite")]
		public Vector2 hermitePosition0 = new Vector2(-3f, 3f);
		public float hermiteSlope0 = 0.5f;
		public Vector2 hermitePosition1 = new Vector2(+3f, 5f);
		public float hermiteSlope1 = 1.5f;
		public float hermiteViewCenter = 0f;
		public float hermiteViewRange = 8f;

		[Header("Piecewise Uniform")]
		public float[] piecewiseUniformPositions = new float[7] { -3f, -2f, -1.5f, -1f, 0.5f, 2f, 3f };
		public float[] piecewiseUniformHeights = new float[6] { 1f, 3f, 2f, 0f, 0.5f, 1f };
		public float piecewiseUniformViewCenter = 0f;
		public float piecewiseUniformViewRange = 8f;

		[Header("Piecewise Linear")]
		public Vector2[] piecewiseLinearPositions = new Vector2[7] { new Vector2(-3f, 1f), new Vector2(-2f, 1f), new Vector2(-1.5f, 3f), new Vector2(-1f, 3.5f), new Vector2(0.5f, 0f), new Vector2(1f, 0f), new Vector2(3f, 2f) };
		public float piecewiseLinearViewCenter = 0f;
		public float piecewiseLinearViewRange = 8f;

		[Header("Piecewise Hermite")]
		public AnimationCurve piecewiseHermiteCurve = AnimationCurve.EaseInOut(-3f, 0f, +3f, 1f);
		public float piecewiseHermiteViewCenter = 0f;
		public float piecewiseHermiteViewRange = 8f;

		private Texture2D _texture;
		private Color[] _pixels;

		private int[] _samples;
		private IRandom _random;

		private float _startTime;
		private float _throttleFactor = 1f;

		private long _totalSampleCount;
		private long _totalTimeMicroseconds;

		private ISampleGenerator<float> _sampleGenerator;
		private float _scale;
		private float _offset;

		protected void Awake()
		{
			_random = MIRandom.CreateStandard();

			OnNormalToggleChanged(true);
		}

		private void Initialize(ISampleGenerator<float> sampleGenerator, float center, float range)
		{
			_startTime = Time.time + startupDelayTime;
			_totalSampleCount = 0L;
			_totalTimeMicroseconds = 0L;

			_sampleGenerator = sampleGenerator;
			_scale = textureWidth / range;
			_offset = textureWidth * (1f / 2f - center / range);

			if (_samples == null || _samples.Length != textureWidth)
			{
				_samples = new int[textureWidth];
			}
			else
			{
				for (int i = 0; i < _samples.Length; ++i)
				{
					_samples[i] = 0;
				}
			}

			if (_texture == null || _texture.width != textureWidth)
			{
				_texture = new Texture2D(textureWidth, 2, TextureFormat.RFloat, false);
				_texture.filterMode = FilterMode.Point;
				_texture.wrapMode = TextureWrapMode.Clamp;

				_pixels = new Color[textureWidth * 2];

				image.texture = _texture;
			}
			else
			{
				for (int i = 0; i < _pixels.Length; ++i)
				{
					_pixels[i] = new Color(0f, 0f, 0f, 1f);
				}
			}

			_texture.SetPixels(_pixels);
			_texture.Apply(false, false);

			image.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
		}

		protected void Update()
		{
			if (Time.fixedTime < _startTime) return;

			int sampleCount = Mathf.CeilToInt(Mathf.Min(targetSamplesPerSecond * Time.deltaTime * _throttleFactor, Mathf.Exp(speedupRate * (Time.time - _startTime)) * Time.deltaTime));

			var stopwatch = new System.Diagnostics.Stopwatch();

			stopwatch.Start();

			for (int i = 0; i < sampleCount; ++i)
			{
				var sample = _sampleGenerator.Next();
				if (float.IsNaN(sample)) continue;
				var index = Mathf.RoundToInt(Mathf.Clamp(sample * _scale + _offset, -1f, _samples.Length));
				if (index >=  0 && index < _samples.Length)
				{
					_samples[index] += 1;
				}
			}

			stopwatch.Stop();

			float elapsedTime = (float)((double)stopwatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency);

			if (elapsedTime > maxFrameTime)
			{
				_throttleFactor = Mathf.Max(0.000001f, _throttleFactor * maxFrameTime * 0.99f / Mathf.Max(0.001f, elapsedTime));
			}
			else if (_throttleFactor < 1f)
			{
				_throttleFactor = Mathf.Min(_throttleFactor * 1.01f, 1f);
			}

			int maxSampleCount = minSampleScale;
			for (int i = 0; i < _samples.Length; ++i)
			{
				maxSampleCount = Mathf.Max(maxSampleCount, _samples[i]);
			}

			for (int x = 0; x < _texture.width; ++x)
			{
				_pixels[x] = _pixels[x + _texture.width] = new Color(_samples[x] / (float)maxSampleCount, 0f, 0f, 1f);
			}

			_texture.SetPixels(_pixels);
			_texture.Apply(false, false);

			_totalSampleCount += sampleCount;
			_totalTimeMicroseconds += Mathf.FloorToInt(elapsedTime * 1000000f);
			double totalSamplesPerSecond = _totalSampleCount / (_totalTimeMicroseconds / 10000000d);
			samplesPerSecondLabel.text = string.Format("samples/s:  {0:N0}", totalSamplesPerSecond);
		}

		public void OnUniformToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakeUniformSampleGenerator(uniformLower, uniformUpper), uniformViewCenter, uniformViewRange);
			}
		}

		public void OnNormalToggleChanged(bool isOn)
		{
			if (isOn)
			{
				if (normalConstrained)
				{
					Initialize(_random.MakeNormalSampleGenerator(normalMean, normalStandardDeviation, normalMin, normalMax), normalViewCenter, normalViewRange);
				}
				else
				{
					Initialize(_random.MakeNormalSampleGenerator(normalMean, normalStandardDeviation), normalViewCenter, normalViewRange);
				}
			}
		}

		public void OnExponentialToggleChanged(bool isOn)
		{
			if (isOn)
			{
				if (exponentialConstrained)
				{
					Initialize(_random.MakeExponentialSampleGenerator(exponentialEventRage, exponentialMax), exponentialViewCenter, exponentialViewRange);
				}
				else
				{
					Initialize(_random.MakeExponentialSampleGenerator(exponentialEventRage), exponentialViewCenter, exponentialViewRange);
				}
			}
		}

		public void OnTriangularToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakeTriangularSampleGenerator(triangularLower, triangularMode, triangularUpper), triangularViewCenter, triangularViewRange);
			}
		}

		public void OnTrapezoidalToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakeTrapezoidalSampleGenerator(trapezoidalLower, trapezoidalLowerMode, trapezoidalUpperMode, trapezoidalUpper), trapezoidalViewCenter, trapezoidalViewRange);
			}
		}

		public void OnLinearToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakeLinearSampleGenerator(linearPosition0, linearPosition1), linearViewCenter, linearViewRange);
			}
		}

		public void OnHermiteToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakeHermiteSplineSampleGenerator(hermitePosition0, hermiteSlope0, hermitePosition1, hermiteSlope1), hermiteViewCenter, hermiteViewRange);
			}
		}

		public void OnPiecewiseUniformToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakePiecewiseUniformSampleGenerator(piecewiseUniformPositions, piecewiseUniformHeights), piecewiseUniformViewCenter, piecewiseUniformViewRange);
			}
		}

		public void OnPiecewiseLinearToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakePiecewiseLinearSampleGenerator(piecewiseLinearPositions), piecewiseLinearViewCenter, piecewiseLinearViewRange);
			}
		}

		public void OnPiecewiseHermiteToggleChanged(bool isOn)
		{
			if (isOn)
			{
				Initialize(_random.MakePiecewiseHermiteSampleGenerator(piecewiseHermiteCurve), piecewiseHermiteViewCenter, piecewiseHermiteViewRange);
			}
		}
	}
}
