/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

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

		public static float OpenFloatFast32(IRandomEngine engine)
		{
			return (float)((engine.Next32() & 0x7FFFFFFFU) + 1U) / 0x80000081U;
		}

		public static float OpenFloatFast64(IRandomEngine engine)
		{
			return (float)((engine.Next64() & 0x7FFFFFFFUL) + 1UL) / 0x80000081UL;
		}

		public static double OpenDouble(IRandomEngine engine)
		{
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & (engine.NextLessThan(0x000FFFFFFFFFFFFFUL) + 1UL);
			return value.number - 1.0;
		}

		public static double OpenDoubleFast32(IRandomEngine engine)
		{
			return (double)((engine.Next32() & 0x7FFFFFFFUL) + 1UL) / 0x80000001UL;
		}

		public static double OpenDoubleFast64(IRandomEngine engine)
		{
			return (double)((engine.Next64() & 0x7FFFFFFFFFFFFFFFUL) + 1UL) / 0x8000000000000401UL;
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
			var random = engine.NextLessThanOrEqual(0x00800000U);
			if (random == 0x00800000U) return 1f;
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & random;
			return value.number - 1f;
		}

		public static float ClosedFloatFast32(IRandomEngine engine)
		{
			return (float)engine.Next32() / uint.MaxValue;
		}

		public static float ClosedFloatFast64(IRandomEngine engine)
		{
			return (float)engine.Next64() / ulong.MaxValue;
		}

		public static double ClosedDouble(IRandomEngine engine)
		{
			var random = engine.NextLessThanOrEqual(0x0010000000000000UL);
			if (random == 0x0010000000000000UL) return 1.0;
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random;
			return value.number - 1.0;
		}

		public static double ClosedDoubleFast32(IRandomEngine engine)
		{
			return (double)engine.Next32() / uint.MaxValue;
		}

		public static double ClosedDoubleFast64(IRandomEngine engine)
		{
			return (double)engine.Next64() / ulong.MaxValue;
		}

		#endregion
	}
}
