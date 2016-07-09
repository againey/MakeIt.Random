/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

// Uncomment this if you want perfect uniformity at the expense of performance
// in some cases, in particular the ClosedFloat() and ClosedDouble() functions.
#define FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY

using System.Runtime.InteropServices;

namespace Experilous.Randomization
{
	public static class RandomUnit
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseFloat
		{
			[FieldOffset(0)]
			public uint bits;
			[FieldOffset(0)]
			public float number;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseDouble
		{
			[FieldOffset(0)]
			public ulong bits;
			[FieldOffset(0)]
			public double number;
		}

		#region Open

		public static float OpenFloat(IRandomEngine engine)
		{
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & (engine.NextLessThan(0x007FFFFFU) + 1U);
			return value.number - 1f;
		}

		public static double OpenDouble(IRandomEngine engine)
		{
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & (engine.NextLessThan(0x000FFFFFFFFFFFFFUL) + 1UL);
			return value.number - 1.0;
		}

		#endregion

		#region HalfOpen

		public static float HalfOpenFloat(IRandomEngine engine)
		{
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & engine.Next32();
			return value.number - 1f;
		}

		public static double HalfOpenDouble(IRandomEngine engine)
		{
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & engine.Next64();
			return value.number - 1.0;
		}

		#endregion

		#region HalfClosed

		public static float HalfClosedFloat(IRandomEngine engine)
		{
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & engine.Next32();
			return 2f - value.number;
		}

		public static double HalfClosedDouble(IRandomEngine engine)
		{
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & engine.Next64();
			return 2.0 - value.number;
		}

		#endregion

		#region Closed

		public static float ClosedFloat(IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFloatFast(engine);
#else
			return ClosedFloatPerfect(engine);
#endif
		}

		public static float ClosedFloatFast(IRandomEngine engine)
		{
			BitwiseFloat value;
			value.number = 0f;
			uint random = engine.Next32() & 0x00FFFFFFU; // Generate within a range that is nearly double what we need.
			random = (random + 1U) >> 1; // Offset and divide in half such that the min and max values have half the probability of the other values.
			value.bits = 0x3F800000U + random;
			return value.number - 1f;
		}

		public static float ClosedFloatPerfect(IRandomEngine engine)
		{
			BitwiseFloat value;
			value.number = 0f;
			uint random = engine.NextLessThanOrEqual(0x00800000U);
			value.bits = 0x3F800000U + random;
			return value.number - 1f;
		}

		public static double ClosedDouble(IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedDoubleFast(engine);
#else
			return ClosedDoublePerfect(engine);
#endif
		}

		public static double ClosedDoubleFast(IRandomEngine engine)
		{
			BitwiseDouble value;
			value.number = 0.0;
			ulong random = engine.Next64() & 0x001FFFFFFFFFFFFFUL; // Generate within a range that is nearly double what we need.
			random = (random + 1UL) >> 1; // Offset and divide in half such that the min and max values have half the probability of the other values.
			value.bits = 0x3FF0000000000000UL + random;
			return value.number - 1.0;
		}

		public static double ClosedDoublePerfect(IRandomEngine engine)
		{
			BitwiseDouble value;
			value.number = 0.0;
			ulong random = engine.NextLessThanOrEqual(0x0010000000000000UL);
			value.bits = 0x3FF0000000000000UL + random;
			return value.number - 1.0;
		}

		#endregion
	}
}
