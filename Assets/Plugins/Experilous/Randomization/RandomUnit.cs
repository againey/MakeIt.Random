/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class RandomUnit
	{
		#region Open

		public static float OpenFloat(IRandomEngine engine)
		{
			return (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)(engine.NextLessThan(0x7FFFFFu) + 1U) << 29))) - 1.0f;
		}

		public static double OpenDouble(IRandomEngine engine)
		{
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & (engine.NextLessThan(0x000FFFFFFFFFFFFFUL) + 1UL))) - 1.0;
		}

		#endregion

		#region HalfOpen

		public static float HalfOpenFloat(IRandomEngine engine)
		{
			return (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)engine.Next32() << 29))) - 1.0f;
		}

		public static double HalfOpenDouble(IRandomEngine engine)
		{
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & engine.Next64())) - 1.0;
		}

		#endregion

		#region HalfClosed

		public static float HalfClosedFloat(IRandomEngine engine)
		{
			return 2.0f - (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)engine.Next32() << 29)));
		}

		public static double HalfClosedDouble(IRandomEngine engine)
		{
			return 2.0 - System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & engine.Next64()));
		}

		#endregion

		#region Closed

		public static float ClosedFloat(IRandomEngine engine)
		{
			var random = engine.NextLessThanOrEqual(0x00800000U);
			return (random != 0x00800000U) ? (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)random << 29))) - 1.0f : 1.0f;
		}

		public static double ClosedDouble(IRandomEngine engine)
		{
			var random = engine.NextLessThanOrEqual(0x0010000000000000UL);
			return (random != 0x0010000000000000UL) ? System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & random)) - 1.0 : 1.0;
		}

		#endregion
	}
}
