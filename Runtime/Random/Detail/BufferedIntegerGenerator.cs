/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

namespace MakeIt.Random.Detail
{
	public class BufferedAnyRangeGeneratorBase
	{
		IRandom _random;

		ulong _rangeMax;

		int _bitCountPerGroup;
		int _excessBitsPer64Bits;
		ulong _bitMask;

		ulong _bits = 0UL;

		public BufferedAnyRangeGeneratorBase(IRandom random, ulong rangeMax, ulong bitMask)
		{
			_random = random;

			_rangeMax = rangeMax;
			_bitMask = bitMask;
			_bitCountPerGroup = DeBruijnLookup.GetBitCountForBitMask(bitMask);
			_excessBitsPer64Bits = 64 % _bitCountPerGroup;
		}

#if MAKEITRANDOM_OPTIMIZED_FOR_32BIT
		protected uint Next32()
		{
			ulong n;
			do
			{
				if (_bits > 1UL)
				{
					n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
				}
				else
				{
					_bits = _random.Next64() >> _excessBitsPer64Bits;
					n = _bits & _bitMask;
					_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup - _excessBitsPer64Bits));
				}
			} while (n > _rangeMax);
			return (uint)n;
		}
#else
		protected ulong Next32()
		{
			ulong n;
			do
			{
				if (_bits > 1UL)
				{
					n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
				}
				else
				{
					_bits = _random.Next64() >> _excessBitsPer64Bits;
					n = _bits & _bitMask;
					_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup - _excessBitsPer64Bits));
				}
			} while (n > _rangeMax);
			return n;
		}
#endif

		protected ulong Next64()
		{
			ulong n;
			do
			{
				if (_bits > 1UL)
				{
					n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
				}
				else
				{
					_bits = _random.Next64() >> _excessBitsPer64Bits;
					n = _bits & _bitMask;
					_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup - _excessBitsPer64Bits));
				}
			} while (n > _rangeMax);
			return n;
		}
	}

	public class BufferedPow2RangeGeneratorBase
	{
		IRandom _random;

		int _bitCountPerGroup;
		int _excessBitsPer64Bits;
		ulong _bitMask;

		ulong _bits;

		public BufferedPow2RangeGeneratorBase(IRandom random, int bitCount, ulong bitMask)
		{
			_random = random;

			_bitMask = bitMask;
			_bitCountPerGroup = bitCount;
			_excessBitsPer64Bits = 64 % _bitCountPerGroup;
		}

#if MAKEITRANDOM_OPTIMIZED_FOR_32BIT
		protected uint Next32()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				return (uint)n;
			}
			else
			{
				_bits = _random.Next64() >> _excessBitsPer64Bits;
				ulong n = _bits & _bitMask;
				_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup - _excessBitsPer64Bits));
				return (uint)n;
			}
		}
#else
		protected ulong Next32()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				return n;
			}
			else
			{
				_bits = _random.Next64() >> _excessBitsPer64Bits;
				ulong n = _bits & _bitMask;
				_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup - _excessBitsPer64Bits));
				return n;
			}
		}
#endif

		protected ulong Next64()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				return n;
			}
			else
			{
				_bits = _random.Next64() >> _excessBitsPer64Bits;
				ulong n = _bits & _bitMask;
				_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup - _excessBitsPer64Bits));
				return n;
			}
		}
	}

	public class BufferedPowPow2RangeGeneratorBase
	{
		IRandom _random;

		int _bitCountPerGroup;
		ulong _bitMask;

		ulong _bits;

		public BufferedPowPow2RangeGeneratorBase(IRandom random, int bitCount, ulong bitMask)
		{
			_random = random;

			_bitMask = bitMask;
			_bitCountPerGroup = bitCount;
		}

#if MAKEITRANDOM_OPTIMIZED_FOR_32BIT
		protected uint Next32()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				return (uint)n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & _bitMask;
				_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup));
				return (uint)n;
			}
		}
#else
		protected ulong Next32()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				return n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & _bitMask;
				_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup));
				return n;
			}
		}
#endif

		protected ulong Next64()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & _bitMask;
				_bits = _bits >> _bitCountPerGroup;
				return n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & _bitMask;
				_bits = (_bits >> _bitCountPerGroup) | (1UL << (64 - _bitCountPerGroup));
				return n;
			}
		}
	}

	public class BufferedBitGenerator
	{
		IRandom _random;

		ulong _bits;

		public BufferedBitGenerator(IRandom random)
		{
			_random = random;
		}

#if MAKEITRANDOM_OPTIMIZED_FOR_32BIT
		protected uint Next32()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & 1UL;
				_bits = _bits >> 1;
				return (uint)n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & 1UL;
				_bits = (_bits >> 1) & 0x8000000000000000UL;
				return (uint)n;
			}
		}
#else
		protected ulong Next32()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & 1UL;
				_bits = _bits >> 1;
				return n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & 1UL;
				_bits = (_bits >> 1) & 0x8000000000000000UL;
				return n;
			}
		}
#endif

		protected ulong Next64()
		{
			if (_bits > 1UL)
			{
				ulong n = _bits & 1UL;
				_bits = _bits >> 1;
				return n;
			}
			else
			{
				_bits = _random.Next64();
				ulong n = _bits & 1UL;
				_bits = (_bits >> 1) & 0x8000000000000000UL;
				return n;
			}
		}
	}
}
