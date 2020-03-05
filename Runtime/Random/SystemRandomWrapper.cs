/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

namespace MakeIt.Random
{
	/// <summary>
	/// Wraps an implementation of <see cref="IRandom"/> in a derivation of <see cref="System.Random"/>.
	/// </summary>
	/// <seealso cref="System.Random"/>
	/// <seealso cref="IRandom"/>
	public class SystemRandomWrapper : System.Random
	{
		private IRandom _random;

		/// <summary>
		/// Creates a wrapper instance around the provided <paramref name="random"/> engine.
		/// </summary>
		/// <param name="random">The random engine instance to be wrapped.</param>
		public SystemRandomWrapper(IRandom random)
		{
			_random = random;
		}
		
		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <returns>A random double in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCO(IRandom)"/>
		protected override double Sample()
		{
			return _random.DoubleCO();
		}

		/// <summary>
		/// Returns a random integer greater than or equal to zero and strictly less than <see cref="int.MaxValue"/>.
		/// </summary>
		/// <returns>A random integer in the range [0, <see cref="int.MaxValue"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCO(IRandom, int)"/>
		public override int Next()
		{
			return _random.RangeCO(int.MaxValue);
		}

		/// <summary>
		/// Returns a random integer greater than or equal to <paramref name="minValue"/> and strictly less than <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="maxValue">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random integer in the range [<paramref name="minValue"/>, <paramref name="maxValue"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCO(IRandom, int, int)"/>
		public override int Next(int minValue, int maxValue)
		{
			return _random.RangeCO(minValue, maxValue);
		}

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		public override void NextBytes(byte[] buffer)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			int maxUnrolled = (buffer.Length + 7) >> 3;
			int i = 0;
			while (i < maxUnrolled)
			{
				uint lower, upper;
				_random.Next64(out lower, out upper);
				buffer[i] = (byte)lower;
				buffer[i + 1] = (byte)(lower >> 8);
				buffer[i + 2] = (byte)(lower >> 16);
				buffer[i + 3] = (byte)(lower >> 24);
				buffer[i + 4] = (byte)upper;
				buffer[i + 5] = (byte)(upper >> 8);
				buffer[i + 6] = (byte)(upper >> 16);
				buffer[i + 7] = (byte)(upper >> 24);
				i += 8;
			}
			if (i < buffer.Length)
			{
				uint lower, upper;
				_random.Next64(out lower, out upper);
				switch (buffer.Length - i)
				{
					case 7: buffer[i + 6] = (byte)(upper >> 16); goto case 6;
					case 6: buffer[i + 5] = (byte)(upper >>  8); goto case 5;
					case 5: buffer[i + 4] = (byte)upper;         goto case 4;
					case 4: buffer[i + 3] = (byte)(lower >> 24); goto case 3;
					case 3: buffer[i + 2] = (byte)(lower >> 16); goto case 2;
					case 2: buffer[i + 1] = (byte)(lower >>  8); goto case 1;
					case 1: buffer[i    ] = (byte)lower; break;
				}
			}
#else
			int maxUnrolled = (buffer.Length + 7) >> 3;
			int i = 0;
			while (i < maxUnrolled)
			{
				ulong next = _random.Next64();
				buffer[i] = (byte)next;
				buffer[i + 1] = (byte)(next >> 8);
				buffer[i + 2] = (byte)(next >> 16);
				buffer[i + 3] = (byte)(next >> 24);
				buffer[i + 4] = (byte)(next >> 32);
				buffer[i + 5] = (byte)(next >> 40);
				buffer[i + 6] = (byte)(next >> 48);
				buffer[i + 7] = (byte)(next >> 56);
				i += 8;
			}
			if (i < buffer.Length)
			{
				ulong next = _random.Next64();
				switch (buffer.Length - i)
				{
					case 7: buffer[i + 6] = (byte)(next >> 48); goto case 6;
					case 6: buffer[i + 5] = (byte)(next >> 40); goto case 5;
					case 5: buffer[i + 4] = (byte)(next >> 32); goto case 4;
					case 4: buffer[i + 3] = (byte)(next >> 24); goto case 3;
					case 3: buffer[i + 2] = (byte)(next >> 16); goto case 2;
					case 2: buffer[i + 1] = (byte)(next >>  8); goto case 1;
					case 1: buffer[i    ] = (byte)next; break;
				}
			}
#endif
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <returns>A random double in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCO(IRandom)"/>
		public override double NextDouble()
		{
			return _random.DoubleCO();
		}
	}
}
