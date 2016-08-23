namespace Experilous.MakeItRandom.Detail
{
	public class BufferedAnyRangeGeneratorBase32
	{
		IRandom _random;

		uint _rangeMax;

		int _bitCountPerGroup;
		int _bitGroupCountMinus1Per32Bits;
		uint _bitMask;

		int _bitGroupCount = 0;
		uint _bits = 0U;

		public BufferedAnyRangeGeneratorBase32(IRandom random, uint rangeMax, uint bitMask)
		{
			_random = random;

			_rangeMax = rangeMax;
			_bitMask = bitMask;
			_bitCountPerGroup = DeBruijnLookup.GetBitCountForBitMask(bitMask);
			_bitGroupCountMinus1Per32Bits = 32 / _bitCountPerGroup - 1;
		}

		protected uint Next32()
		{
			uint n;
			do
			{
				if (_bitGroupCount > 0)
				{
					n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					--_bitGroupCount;
				}
				else
				{
					_bits = _random.Next32();
					n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					_bitGroupCount = _bitGroupCountMinus1Per32Bits;
				}
			} while (n > _rangeMax);
			return n;
		}
	}

	public class BufferedAnyRangeGeneratorBase64
	{
		IRandom _random;

		ulong _rangeMax;

		int _bitCountPerGroup;
		int _bitGroupCountMinus1Per64Bits;
		ulong _bitMask;

		int _bitGroupCount = 0;
		ulong _bits = 0UL;

		public BufferedAnyRangeGeneratorBase64(IRandom random, ulong rangeMax, ulong bitMask)
		{
			_random = random;

			_rangeMax = rangeMax;
			_bitMask = bitMask;
			_bitCountPerGroup = DeBruijnLookup.GetBitCountForBitMask(bitMask);
			_bitGroupCountMinus1Per64Bits = 64 / _bitCountPerGroup - 1;
		}

		protected ulong Next64()
		{
			ulong n;
			do
			{
				if (_bitGroupCount > 0)
				{
					n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					--_bitGroupCount;
				}
				else
				{
					_bits = _random.Next64();
					n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					_bitGroupCount = _bitGroupCountMinus1Per64Bits;
				}
			} while (n > _rangeMax);
			return n;
		}
	}

	public class BufferedPow2RangeGeneratorBase32
	{
		IRandom _random;

		int _bitCountPerGroup;
		int _bitGroupCountMinus1Per32Bits;
		uint _bitMask;

		int _bitGroupCount;
		uint _bits;

		public BufferedPow2RangeGeneratorBase32(IRandom random, int bitCount, uint bitMask)
		{
			_random = random;

			_bitMask = bitMask;
			_bitCountPerGroup = bitCount;
			_bitGroupCountMinus1Per32Bits = 32 / _bitCountPerGroup - 1;
		}

		protected uint Next32()
		{
			if (_bitGroupCount > 0)
			{
				uint n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				--_bitGroupCount;
				return n;
			}
			else
			{
				_bits = _random.Next32();
				uint n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				_bitGroupCount = _bitGroupCountMinus1Per32Bits;
				return n;
			}
		}
	}

	public class BufferedPow2RangeGeneratorBase64
	{
		IRandom _random;

		int _bitCountPerGroup;
		int _bitGroupCountMinus1Per64Bits;
		ulong _bitMask;

		int _bitGroupCount;
		ulong _bits;

		public BufferedPow2RangeGeneratorBase64(IRandom random, int bitCount, ulong bitMask)
		{
			_random = random;

			_bitMask = bitMask;
			_bitCountPerGroup = bitCount;
			_bitGroupCountMinus1Per64Bits = 64 / _bitCountPerGroup - 1;
		}

		protected ulong Next64()
		{
			if (_bitGroupCount > 0)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				--_bitGroupCount;
				return n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				_bitGroupCount = _bitGroupCountMinus1Per64Bits;
				return n;
			}
		}
	}

	public class BufferedPowPow2RangeGeneratorBase32
	{
		IRandom _random;

		int _bitCountPerGroup;
		int _bitGroupCountMinus1Per32Bits;
		uint _bitMask;

		int _bitGroupCount;
		uint _bits;

		public BufferedPowPow2RangeGeneratorBase32(IRandom random, int bitCount, uint bitMask)
		{
			_random = random;

			_bitMask = bitMask;
			_bitCountPerGroup = bitCount;
			_bitGroupCountMinus1Per32Bits = 32 / _bitCountPerGroup - 1;
		}

		protected uint Next32()
		{
			if (_bitGroupCount > 0)
			{
				uint n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				--_bitGroupCount;
				return n;
			}
			else
			{
				_bits = _random.Next32();
				uint n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				_bitGroupCount = _bitGroupCountMinus1Per32Bits;
				return n;
			}
		}
	}

	public class BufferedPowPow2RangeGeneratorBase64
	{
		IRandom _random;

		int _bitCountPerGroup;
		int _bitGroupCountMinus1Per64Bits;
		ulong _bitMask;

		int _bitGroupCount;
		ulong _bits;

		public BufferedPowPow2RangeGeneratorBase64(IRandom random, int bitCount, ulong bitMask)
		{
			_random = random;

			_bitMask = bitMask;
			_bitCountPerGroup = bitCount;
			_bitGroupCountMinus1Per64Bits = 64 / _bitCountPerGroup - 1;
		}

		protected ulong Next64()
		{
			if (_bitGroupCount > 0)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				--_bitGroupCount;
				return n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				_bitGroupCount = _bitGroupCountMinus1Per64Bits;
				return n;
			}
		}
	}
}
