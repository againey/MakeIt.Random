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

		public int samplesPerUpdate = 1000;
		public int textureWidth = 1024;
		public int textureHeight = 1024;
		public float range = 12f;

		private Texture2D _texture;
		private Color32[] _pixels;

		private int[] _samples;
		private IRandom _random;

		protected void Start()
		{
			_random = MIRandom.CreateStandard();

			_samples = new int[textureWidth];
			_texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
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

		protected void Update()
		{
			float scale = textureWidth / range;
			float offset = textureWidth / 2f;
			for (int i = 0; i < samplesPerUpdate; ++i)
			{
				var sample = _random.NormalDistribution(0f, 1f);
				var index = Mathf.Clamp(Mathf.RoundToInt(sample * scale + offset), 0, _samples.Length - 1);
				_samples[index] += 1;
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

			_texture.SetPixels32(_pixels);
			_texture.Apply(false, false);
		}
	}
}
