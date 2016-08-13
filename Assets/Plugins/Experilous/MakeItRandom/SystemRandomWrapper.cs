/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// Wraps an implementation of <see cref="IRandom"/> in a derivation of <see cref="System.Random"/>.
	/// </summary>
	/// <remarks>
	/// This wrapper is preferable for random engines that optimally produce random numbers 32 bits at a time.
	/// </remarks>
	/// <seealso cref="System.Random"/>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="SystemRandomWrapper64"/>
	public class SystemRandomWrapper32 : System.Random
	{
		private IRandom _random;
		private uint[] _sourceBuffer;

		/// <summary>
		/// Creates a wrapper instance around the provided <paramref name="random"/> engine.
		/// </summary>
		/// <param name="random">The random engine instance to be wrapped.</param>
		public SystemRandomWrapper32(IRandom random)
		{
			_random = random;
			_sourceBuffer = new uint[1];
		}
		
		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <returns>A random double in the range [0, 1).</returns>
		/// <seealso cref="RandomUnit.HalfOpenDoubleUnit(IRandom)"/>
		protected override double Sample()
		{
			return _random.HalfOpenDoubleUnit();
		}

		/// <summary>
		/// Returns a random integer greater than or equal to zero and strictly less than <see cref="int.MaxValue"/>.
		/// </summary>
		/// <returns>A random integer in the range [0, <see cref="int.MaxValue"/>).</returns>
		/// <seealso cref="RandomRange.HalfOpenRange(IRandom, int)"/>
		public override int Next()
		{
			return _random.HalfOpenRange(int.MaxValue);
		}

		/// <summary>
		/// Returns a random integer greater than or equal to <paramref name="minValue"/> and strictly less than <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="maxValue">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random integer in the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</returns>
		/// <seealso cref="RandomRange.HalfOpenRange(IRandom, int, int)"/>
		public override int Next(int minValue, int maxValue)
		{
			return _random.HalfOpenRange(minValue, maxValue);
		}

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		public override void NextBytes(byte[] buffer)
		{
			if (buffer.Length <= 4)
			{
				_sourceBuffer[0] = _random.Next32();
				System.Buffer.BlockCopy(_sourceBuffer, 0, buffer, 0, buffer.Length);
			}
			else
			{
				var sourceBuffer = new uint[(buffer.Length + 3) / 4];
				for (int i = 0; i < sourceBuffer.Length; ++i)
				{
					sourceBuffer[i] = _random.Next32();
				}
				System.Buffer.BlockCopy(sourceBuffer, 0, buffer, 0, buffer.Length);
			}
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <returns>A random double in the range [0, 1).</returns>
		/// <seealso cref="RandomUnit.HalfOpenDoubleUnit(IRandom)"/>
		public override double NextDouble()
		{
			return _random.HalfOpenDoubleUnit();
		}
	}

	/// <summary>
	/// Wraps an implementation of <see cref="IRandom"/> in a derivation of <see cref="System.Random"/>.
	/// </summary>
	/// <remarks>
	/// This wrapper is preferable for random engines that optimally produce random numbers 64 bits at a time.
	/// </remarks>
	/// <seealso cref="System.Random"/>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="SystemRandomWrapper32"/>
	public class SystemRandomWrapper64 : System.Random
	{
		private IRandom _random;
		private ulong[] _sourceBuffer;

		/// <summary>
		/// Creates a wrapper instance around the provided <paramref name="random"/> engine.
		/// </summary>
		/// <param name="random">The random engine instance to be wrapped.</param>
		public SystemRandomWrapper64(IRandom random)
		{
			_random = random;
			_sourceBuffer = new ulong[1];
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <returns>A random double in the range [0, 1).</returns>
		/// <seealso cref="RandomUnit.HalfOpenDoubleUnit(IRandom)"/>
		protected override double Sample()
		{
			return _random.HalfOpenDoubleUnit();
		}

		/// <summary>
		/// Returns a random integer greater than or equal to zero and strictly less than <see cref="int.MaxValue"/>.
		/// </summary>
		/// <returns>A random integer in the range [0, <see cref="int.MaxValue"/>).</returns>
		/// <seealso cref="RandomRange.HalfOpenRange(IRandom, int)"/>
		public override int Next()
		{
			return _random.HalfOpenRange(int.MaxValue);
		}

		/// <summary>
		/// Returns a random integer greater than or equal to <paramref name="minValue"/> and strictly less than <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="maxValue">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random integer in the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</returns>
		/// <seealso cref="RandomRange.HalfOpenRange(IRandom, int, int)"/>
		public override int Next(int minValue, int maxValue)
		{
			return _random.HalfOpenRange(minValue, maxValue);
		}

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		public override void NextBytes(byte[] buffer)
		{
			if (buffer.Length <= 8)
			{
				_sourceBuffer[0] = _random.Next64();
				System.Buffer.BlockCopy(_sourceBuffer, 0, buffer, 0, buffer.Length);
			}
			else
			{
				var sourceBuffer = new ulong[(buffer.Length + 7) / 8];
				for (int i = 0; i < sourceBuffer.Length; ++i)
				{
					sourceBuffer[i] = _random.Next64();
				}
				System.Buffer.BlockCopy(sourceBuffer, 0, buffer, 0, buffer.Length);
			}
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <returns>A random double in the range [0, 1).</returns>
		/// <seealso cref="RandomUnit.HalfOpenDoubleUnit(IRandom)"/>
		public override double NextDouble()
		{
			return _random.HalfOpenDoubleUnit();
		}
	}
}
