/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	/// <summary>
	/// Wraps an implementation of <see cref="IRandomEngine"/> in a derivation of <see cref="System.Random"/>.
	/// </summary>
	/// <remarks>
	/// This wrapper is preferable for random engines that optimally produce random numbers 32 bits at a time.
	/// </remarks>
	/// <seealso cref="System.Random"/>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="SystemRandomWrapper64"/>
	public class SystemRandomWrapper32 : System.Random
	{
		private IRandomEngine _randomEngine;
		private uint[] _sourceBuffer;

		public SystemRandomWrapper32(IRandomEngine randomEngine)
		{
			_randomEngine = randomEngine;
			_sourceBuffer = new uint[1];
		}

		protected override double Sample()
		{
			return RandomUnit.HalfOpenDouble(_randomEngine);
		}

		public override int Next()
		{
			return RandomRange.HalfOpen(int.MaxValue, _randomEngine);
		}

		public override int Next(int minValue, int maxValue)
		{
			return RandomRange.HalfOpen(minValue, maxValue, _randomEngine);
		}

		public override void NextBytes(byte[] buffer)
		{
			if (buffer.Length <= 4)
			{
				_sourceBuffer[0] = _randomEngine.Next32();
				System.Buffer.BlockCopy(_sourceBuffer, 0, buffer, 0, buffer.Length);
			}
			else
			{
				var sourceBuffer = new uint[(buffer.Length + 3) / 4];
				for (int i = 0; i < sourceBuffer.Length; ++i)
				{
					sourceBuffer[i] = _randomEngine.Next32();
				}
				System.Buffer.BlockCopy(sourceBuffer, 0, buffer, 0, buffer.Length);
			}
		}

		public override double NextDouble()
		{
			return Sample();
		}
	}

	/// <summary>
	/// Wraps an implementation of <see cref="IRandomEngine"/> in a derivation of <see cref="System.Random"/>.
	/// </summary>
	/// <remarks>
	/// This wrapper is preferable for random engines that optimally produce random numbers 64 bits at a time.
	/// </remarks>
	/// <seealso cref="System.Random"/>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="SystemRandomWrapper32"/>
	public class SystemRandomWrapper64 : System.Random
	{
		private IRandomEngine _randomEngine;
		private ulong[] _sourceBuffer;

		public SystemRandomWrapper64(IRandomEngine randomEngine)
		{
			_randomEngine = randomEngine;
			_sourceBuffer = new ulong[1];
		}

		protected override double Sample()
		{
			return RandomUnit.HalfOpenDouble(_randomEngine);
		}

		public override int Next()
		{
			return RandomRange.HalfOpen(int.MaxValue, _randomEngine);
		}

		public override int Next(int minValue, int maxValue)
		{
			return RandomRange.HalfOpen(minValue, maxValue, _randomEngine);
		}

		public override void NextBytes(byte[] buffer)
		{
			if (buffer.Length <= 8)
			{
				_sourceBuffer[0] = _randomEngine.Next64();
				System.Buffer.BlockCopy(_sourceBuffer, 0, buffer, 0, buffer.Length);
			}
			else
			{
				var sourceBuffer = new ulong[(buffer.Length + 7) / 8];
				for (int i = 0; i < sourceBuffer.Length; ++i)
				{
					sourceBuffer[i] = _randomEngine.Next64();
				}
				System.Buffer.BlockCopy(sourceBuffer, 0, buffer, 0, buffer.Length);
			}
		}

		public override double NextDouble()
		{
			return Sample();
		}
	}
}
