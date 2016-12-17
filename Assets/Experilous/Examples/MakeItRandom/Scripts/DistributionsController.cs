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

		private Texture2D _texture;
		private Color32[] _pixels;

		private int[] _samples;
		private IRandom _random;

		private float _startTime;

		protected void Start()
		{
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
			float offset = 0f;//textureWidth / 2f;
			for (int i = 0; i < sampleCount; ++i)
			{
				var sample = _random.ExponentialDistribution(0.5f);
				var index = Mathf.Clamp(Mathf.FloorToInt(sample * scale + offset), -1, _samples.Length);
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
					byte intensity = y * maxSampleCount <= _samples[x] * textureHeight ? (byte)255 : (byte)0;
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
