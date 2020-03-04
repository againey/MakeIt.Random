/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.MakeItRandom.Detail
{
	/// <summary>
	/// A static class of functions for various probability distributions.
	/// </summary>
	public static class Distributions
	{
		#region Ziggurat Algorithm

		#region Ziggurat Table Types

		public struct FloatZigguratSegment
		{
			public uint n;
			public float s;

			public FloatZigguratSegment(uint n, float s)
			{
				this.n = n;
				this.s = s;
			}
		}

		public struct DoubleZigguratSegment
		{
			public ulong n;
			public double s;

			public DoubleZigguratSegment(ulong n, double s)
			{
				this.n = n;
				this.s = s;
			}
		}

		public class OneSidedFloatZigguratTable
		{
			public FloatZigguratSegment[] segments;
			public float[] segmentUpperBounds;
			public uint mask;
			public int shift;

			public OneSidedFloatZigguratTable(FloatZigguratSegment[] segments, float[] segmentUpperBounds, uint mask, int shift)
			{
				this.segments = segments;
				this.segmentUpperBounds = segmentUpperBounds;
				this.mask = mask;
				this.shift = shift;
			}
		}

		public class OneSidedDoubleZigguratTable
		{
			public DoubleZigguratSegment[] segments;
			public double[] segmentUpperBounds;
			public ulong mask;
			public int shift;

			public OneSidedDoubleZigguratTable(DoubleZigguratSegment[] segments, double[] segmentUpperBounds, ulong mask, int shift)
			{
				this.segments = segments;
				this.segmentUpperBounds = segmentUpperBounds;
				this.mask = mask;
				this.shift = shift;
			}
		}

		public class TwoSidedSymmetricFloatZigguratTable
		{
			public FloatZigguratSegment[] segments;
			public float[] segmentUpperBounds;
			public int threshold;
			public int mask;
			public int shift;

			public TwoSidedSymmetricFloatZigguratTable(FloatZigguratSegment[] segments, float[] segmentUpperBounds, int threshold, int mask, int shift)
			{
				this.segments = segments;
				this.segmentUpperBounds = segmentUpperBounds;
				this.threshold = threshold;
				this.mask = mask;
				this.shift = shift;
			}
		}

		public class TwoSidedSymmetricDoubleZigguratTable
		{
			public DoubleZigguratSegment[] segments;
			public double[] segmentUpperBounds;
			public long threshold;
			public long mask;
			public int shift;

			public TwoSidedSymmetricDoubleZigguratTable(DoubleZigguratSegment[] segments, double[] segmentUpperBounds, long threshold, long mask, int shift)
			{
				this.segments = segments;
				this.segmentUpperBounds = segmentUpperBounds;
				this.threshold = threshold;
				this.mask = mask;
				this.shift = shift;
			}
		}

		#endregion

		#region Sampling

		public static float SampleZiggurat(IRandom random, OneSidedFloatZigguratTable ziggurat, Func<float, float> f, Func<IRandom, float, float> sampleTailFallback)
		{
			do
			{
				// Select a random segment, and a random n/x within the segment.
				uint n = random.Next32();
				int i = (int)(n & ziggurat.mask);
				n = n >> ziggurat.shift;
				var segment = ziggurat.segments[i];
				float x = n * segment.s;

				// Dominant quick check within fully-contained segment rectangle.
				if (n < segment.n) return x;

				// Rare case within tail.
				if (i == 0) return sampleTailFallback(random, (1 << (32 - ziggurat.shift)) * ziggurat.segments[1].s);

				// Slow check within the partially-contained segment rectangle.
				if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < f(x)) return x;
			} while (true);
		}

		public static double SampleZiggurat(IRandom random, OneSidedDoubleZigguratTable ziggurat, Func<double, double> f, Func<IRandom, double, double> sampleTailFallback)
		{
			do
			{
				// Select a random segment, and a random n/x within the segment.
				ulong n = random.Next64();
				int i = (int)(n & ziggurat.mask);
				n = n >> ziggurat.shift;
				var segment = ziggurat.segments[i];
				double x = n * segment.s;

				// Dominant quick check within fully-contained segment rectangle.
				if (n < segment.n) return x;

				// Rare case within tail.
				if (i == 0) return sampleTailFallback(random, (1L << (64 - ziggurat.shift)) * ziggurat.segments[1].s);

				// Slow check within the partially-contained segment rectangle.
				if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < f(x)) return x;
			} while (true);
		}

		public static float SampleZiggurat(IRandom random, TwoSidedSymmetricFloatZigguratTable ziggurat, Func<float, float> f, Func<IRandom, float, float> sampleTailFallback)
		{
			do
			{
				// Select a random segment, and a random n/x within the segment.
				int n;
				do
				{
					n = (int)random.Next32();
				} while (n < ziggurat.threshold);
				int i = n & ziggurat.mask;
				n = n >> ziggurat.shift;
				var segment = ziggurat.segments[i];
				float x = n * segment.s;

				// Dominant quick check within fully-contained segment rectangle.
				if ((uint)Math.Abs(n) < segment.n) return x;

				// Rare case within tail.
				if (i == 0) return sampleTailFallback(random, (1 << (31 - ziggurat.shift)) * ziggurat.segments[1].s * (n > 0 ? +1f : -1f));

				// Slow check within the partially-contained segment rectangle.
				if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < f(x)) return x;
			} while (true);
		}

		public static double SampleZiggurat(IRandom random, TwoSidedSymmetricDoubleZigguratTable ziggurat, Func<double, double> f, Func<IRandom, double, double> sampleTailFallback)
		{
			do
			{
				// Select a random segment, and a random n/x within the segment.
				long n;
				do
				{
					n = (long)random.Next64();
				} while (n < ziggurat.threshold);
				int i = (int)(n & ziggurat.mask);
				n = n >> ziggurat.shift;
				var segment = ziggurat.segments[i];
				double x = n * segment.s;


				// Dominant quick check within fully-contained segment rectangle.
				if ((ulong)Math.Abs(n) < segment.n) return x;

				// Rare case within tail.
				if (i == 0) return sampleTailFallback(random, (1L << (63 - ziggurat.shift)) * ziggurat.segments[1].s * (n > 0L ? +1d : -1d));

				// Slow check within the partially-contained segment rectangle.
				if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < f(x)) return x;
			} while (true);
		}

		#endregion

		#region Table Generation

		public static OneSidedFloatZigguratTable GenerateOneSidedFloatZigguratTable(int tableSizeMagnitidue, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double acceptableError)
		{
			int segmentCount = 1 << tableSizeMagnitidue;

			var segments = new FloatZigguratSegment[segmentCount];
			var segmentUpperBounds = new float[segmentCount];

			var x = GenerateZigguratTableXValues(segmentCount, f, fInv, fCDF, totalArea, totalArea, acceptableError);

			double intToFloatScale = 1 << (32 - tableSizeMagnitidue);

			double y0 = f(x[0]);
			double a0 = x[0] * y0;
			double v = a0 + totalArea - fCDF(x[0]);
			uint n0 = (uint)Math.Floor(a0 / v * intToFloatScale);
			segments[0] = new FloatZigguratSegment(n0, (float)(v / y0 / intToFloatScale));
			segmentUpperBounds[0] = (float)f(x[0]);

			for (int i = 1; i < segmentCount; ++i)
			{
				uint n = (uint)Math.Floor(x[i] / x[i - 1] * intToFloatScale);
				segments[i] = new FloatZigguratSegment(n, (float)(x[i - 1] / intToFloatScale));
				segmentUpperBounds[i] = (float)f(x[i]);
			}

			return new OneSidedFloatZigguratTable(segments, segmentUpperBounds, ~((~0U) << tableSizeMagnitidue), tableSizeMagnitidue);
		}

		public static OneSidedDoubleZigguratTable GenerateOneSidedDoubleZigguratTable(int tableSizeMagnitidue, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double acceptableError)
		{
			int segmentCount = 1 << tableSizeMagnitidue;

			var segments = new DoubleZigguratSegment[segmentCount];
			var segmentUpperBounds = new double[segmentCount];

			var x = GenerateZigguratTableXValues(segmentCount, f, fInv, fCDF, totalArea, totalArea, acceptableError);

			double intToFloatScale = 1L << (64 - tableSizeMagnitidue);

			double y0 = f(x[0]);
			double a0 = x[0] * y0;
			double v = a0 + totalArea - fCDF(x[0]);
			ulong n0 = (ulong)Math.Floor(a0 / v * intToFloatScale);
			segments[0] = new DoubleZigguratSegment(n0, v / y0 / intToFloatScale);
			segmentUpperBounds[0] = (float)f(x[0]);

			for (int i = 1; i < segmentCount; ++i)
			{
				ulong n = (ulong)Math.Floor(x[i] / x[i - 1] * intToFloatScale);
				segments[i] = new DoubleZigguratSegment(n, x[i - 1] / intToFloatScale);
				segmentUpperBounds[i] = f(x[i]);
			}

			return new OneSidedDoubleZigguratTable(segments, segmentUpperBounds, ~((~0UL) << tableSizeMagnitidue), tableSizeMagnitidue);
		}

		public static TwoSidedSymmetricFloatZigguratTable GenerateTwoSidedSymmetricFloatZigguratTable(int tableSizeMagnitidue, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double acceptableError)
		{
			int segmentCount = 1 << tableSizeMagnitidue;

			var segments = new FloatZigguratSegment[segmentCount];
			var segmentUpperBounds = new float[segmentCount];

			var x = GenerateZigguratTableXValues(segmentCount, f, fInv, fCDF, totalArea, totalArea * 0.5d, acceptableError);

			double intToFloatScale = 1 << (31 - tableSizeMagnitidue);

			double y0 = f(x[0]);
			double a0 = x[0] * y0;
			double v = a0 + totalArea - fCDF(x[0]);
			uint n0 = (uint)Math.Floor(a0 / v * intToFloatScale);
			segments[0] = new FloatZigguratSegment(n0, (float)(v / y0 / intToFloatScale));
			segmentUpperBounds[0] = (float)f(x[0]);

			for (int i = 1; i < segmentCount; ++i)
			{
				uint n = (uint)Math.Floor(x[i] / x[i - 1] * intToFloatScale);
				segments[i] = new FloatZigguratSegment(n, (float)(x[i - 1] / intToFloatScale));
				segmentUpperBounds[i] = (float)f(x[i]);
			}

			return new TwoSidedSymmetricFloatZigguratTable(segments, segmentUpperBounds, int.MinValue + segmentCount, (int)(~((~0U) << tableSizeMagnitidue)), tableSizeMagnitidue);
		}

		public static TwoSidedSymmetricDoubleZigguratTable GenerateTwoSidedSymmetricDoubleZigguratTable(int tableSizeMagnitidue, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double acceptableError)
		{
			int segmentCount = 1 << tableSizeMagnitidue;

			var segments = new DoubleZigguratSegment[segmentCount];
			var segmentUpperBounds = new double[segmentCount];

			var x = GenerateZigguratTableXValues(segmentCount, f, fInv, fCDF, totalArea, totalArea * 0.5d, acceptableError);

			double intToFloatScale = 1L << (63 - tableSizeMagnitidue);

			double y0 = f(x[0]);
			double a0 = x[0] * y0;
			double v = a0 + totalArea - fCDF(x[0]);
			ulong n0 = (ulong)Math.Floor(a0 / v * intToFloatScale);
			segments[0] = new DoubleZigguratSegment(n0, v / y0 / intToFloatScale);
			segmentUpperBounds[0] = (float)f(x[0]);

			for (int i = 1; i < segmentCount; ++i)
			{
				ulong n = (ulong)Math.Floor(x[i] / x[i - 1] * intToFloatScale);
				segments[i] = new DoubleZigguratSegment(n, x[i - 1] / intToFloatScale);
				segmentUpperBounds[i] = f(x[i]);
			}

			return new TwoSidedSymmetricDoubleZigguratTable(segments, segmentUpperBounds, long.MinValue + segmentCount, (long)(~((~0UL) << tableSizeMagnitidue)), tableSizeMagnitidue);
		}

		private static double[] GenerateZigguratTableXValues(int segmentCount, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double activeArea, double acceptableError)
		{
#if UNITY_EDITOR
			var loopGuard = new InfiniteLoopGuard();
#endif
			var x = new double[segmentCount];
			double a = activeArea / segmentCount;
			double rMin = fInv(f(0d) / segmentCount);
			double rMax = rMin;
#if UNITY_EDITOR
			loopGuard.Reset(100);
#endif
			do
			{
				rMax = fInv(f(rMax) * 0.5d);
#if UNITY_EDITOR
				loopGuard.Iterate();
#endif
			} while (rMax * f(rMax) + totalArea - fCDF(rMax) > a);
			double tableError;
#if UNITY_EDITOR
			loopGuard.Reset(1000);
#endif
			do
			{
				double rAvg = (rMin + rMax) * 0.5d;
				tableError = CalculateZigguratTableError(rAvg, segmentCount, f, fInv, fCDF, totalArea, x);
				if (double.IsNaN(tableError) || tableError > 0d)
				{
					rMin = rAvg;
				}
				else
				{
					rMax = rAvg;
				}
#if UNITY_EDITOR
				loopGuard.Iterate();
#endif
			} while (double.IsNaN(tableError) || Math.Abs(tableError) > acceptableError);

			return x;
		}

		private static double CalculateZigguratTableError(double r, int segmentCount, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double[] x)
		{
			x[0] = r;
			x[segmentCount - 1] = 0d;
			double v = r * f(r) + totalArea - fCDF(r);
			double xPrev = r;
			for (int i = 1; i < segmentCount - 1; ++i)
			{
				x[i] = xPrev = fInv(v / xPrev + f(xPrev));

				// Not-a-number or negative means that the bottom segment was far too big and blew out the top of the domain, and so r should be increased.
				if (double.IsNaN(xPrev) || xPrev < 0d) return double.NaN;
			}

			// Return the area of the bottom segment minus the area of the top segment.
			// Positive means that the bottom segment was larger than the top segment, needs to be smaller, and so r should be increased.
			// Negative means that the bottom segment was smaller than the top segment, needs to be larger, and so r should be decreased.
			return v - xPrev * (f(0d) - f(xPrev));
		}

		#endregion

		#region Lookup Table Generation

		private static void GenerateZigguratLookupTable(string distributionName, string tableTypeName, string segmentTypeName, string numericTypeName, int segmentCount, Func<int, string> segmentToString, Func<int, string> upperBoundToString, Func<string> finaleToString)
		{
			using (var file = System.IO.File.Open(string.Format("Experilous/MakeItRandom/Generated/{0}{1}.txt", distributionName, tableTypeName), System.IO.FileMode.Create, System.IO.FileAccess.Write))
			{
				using (var writer = new System.IO.StreamWriter(file))
				{
					writer.WriteLine("\t\t\t#region Lookup Table");
					writer.WriteLine();
					writer.WriteLine("\t\t\tpublic static readonly {0} zigguratTable = new {0}(", tableTypeName);

					writer.WriteLine("\t\t\t\tnew {0}[]", segmentTypeName);
					writer.WriteLine("\t\t\t\t{");
					int itemsInRow = 0;
					for (int i = 0; i < segmentCount; ++i)
					{
						if (itemsInRow == 0) writer.Write("\t\t\t\t\t");
						writer.Write("new {0}({1})", segmentTypeName, segmentToString(i));
						if (itemsInRow < 3)
						{
							writer.Write(", ");
							++itemsInRow;
						}
						else
						{
							writer.WriteLine(",");
							itemsInRow = 0;
						}
					}
					writer.WriteLine("\t\t\t\t},");

					writer.WriteLine("\t\t\t\tnew {0}[]", numericTypeName);
					writer.WriteLine("\t\t\t\t{");
					itemsInRow = 0;
					for (int i = 0; i < segmentCount; ++i)
					{
						if (itemsInRow == 0) writer.Write("\t\t\t\t\t");
						writer.Write(upperBoundToString(i));
						if (itemsInRow < 3)
						{
							writer.Write(", ");
							++itemsInRow;
						}
						else
						{
							writer.WriteLine(",");
							itemsInRow = 0;
						}
					}
					writer.WriteLine("\t\t\t\t},");
					writer.WriteLine("\t\t\t\t{0});", finaleToString());
					writer.WriteLine();
					writer.WriteLine("\t\t\t#endregion");
				}
			}
		}

		private static void GenerateZigguratLookupTable(OneSidedFloatZigguratTable table, string distributionName)
		{
			GenerateZigguratLookupTable(distributionName, "OneSidedFloatZigguratTable", "FloatZigguratSegment", "float", table.segments.Length,
				(int i) => string.Format("{0:D}U, {1:R}f", table.segments[i].n, table.segments[i].s),
				(int i) => string.Format("{0:R}f", table.segmentUpperBounds[i]),
				() => string.Format("{0:D}U, {1:D}", table.mask, table.shift));
		}

		private static void GenerateZigguratLookupTable(OneSidedDoubleZigguratTable table, string distributionName)
		{
			GenerateZigguratLookupTable(distributionName, "OneSidedDoubleZigguratTable", "DoubleZigguratSegment", "double", table.segments.Length,
				(int i) => string.Format("{0:D}UL, {1:R}d", table.segments[i].n, table.segments[i].s),
				(int i) => string.Format("{0:R}d", table.segmentUpperBounds[i]),
				() => string.Format("{0:D}UL, {1:D}", table.mask, table.shift));
		}

		private static void GenerateZigguratLookupTable(TwoSidedSymmetricFloatZigguratTable table, string distributionName)
		{
			GenerateZigguratLookupTable(distributionName, "TwoSidedSymmetricFloatZigguratTable", "FloatZigguratSegment", "float", table.segments.Length,
				(int i) => string.Format("{0:D}U, {1:R}f", table.segments[i].n, table.segments[i].s),
				(int i) => string.Format("{0:R}f", table.segmentUpperBounds[i]),
				() => string.Format("{0:D}, {1:D}, {2:D}", table.threshold, table.mask, table.shift));
		}

		private static void GenerateZigguratLookupTable(TwoSidedSymmetricDoubleZigguratTable table, string distributionName)
		{
			GenerateZigguratLookupTable(distributionName, "TwoSidedSymmetricDoubleZigguratTable", "DoubleZigguratSegment", "double", table.segments.Length,
				(int i) => string.Format("{0:D}UL, {1:R}d", table.segments[i].n, table.segments[i].s),
				(int i) => string.Format("{0:R}d", table.segmentUpperBounds[i]),
				() => string.Format("{0:D}L, {1:D}L, {2:D}", table.threshold, table.mask, table.shift));
		}

		#endregion

		#endregion

		public static class NormalFloat
		{
			public static float F(float x)
			{
				return Mathf.Exp(-x * x * 0.5f);
			}

			public static float SampleFallback(IRandom random, float xMin)
			{
				// https://en.wikipedia.org/wiki/Ziggurat_algorithm#Fallback_algorithms_for_the_tail
				float x, y;
				do
				{
					x = -Mathf.Log(random.PreciseFloatOO()) / xMin;
					y = -Mathf.Log(random.PreciseFloatOO());
				} while (y * 2f <= x * x);
				return xMin + x;
			}

			public static float Sample(IRandom random, TwoSidedSymmetricFloatZigguratTable ziggurat)
			{
				do
				{
					// Select a random segment, and a random n/x within the segment.
					int n;
					do
					{
						n = (int)random.Next32();
					} while (n < ziggurat.threshold);
					int i = n & ziggurat.mask;
					n = n >> ziggurat.shift;
					var segment = ziggurat.segments[i];
					float x = n * segment.s;

					// Dominant quick check within fully-contained segment rectangle.
					if ((uint)Math.Abs(n) < segment.n) return x;

					// Rare case within tail.
					if (i == 0) return SampleFallback(random, (1 << (31 - ziggurat.shift)) * ziggurat.segments[1].s * (n > 0 ? +1f : -1f));

					// Slow check within the partially-contained segment rectangle.
					if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < F(x)) return x;
				} while (true);
			}

#if UNITY_EDITOR
			//[UnityEditor.Callbacks.DidReloadScripts] // Uncomment this attribute in order to generate and print tables in the Unity console pane.
			private static void GenerateZigguratLookupTable()
			{
				var table = GenerateTwoSidedSymmetricFloatZigguratTable(8,
					NormalDouble.F,
					NormalDouble.Inv,
					NormalDouble.CDF,
					NormalDouble.totalArea,
					0.0000000001d);

				Distributions.GenerateZigguratLookupTable(table, "normal");
			}
#endif

			#region Lookup Table

			public static readonly TwoSidedSymmetricFloatZigguratTable zigguratTable = new TwoSidedSymmetricFloatZigguratTable(
				new FloatZigguratSegment[]
				{
					new FloatZigguratSegment(7838056U, 4.66207155E-07f), new FloatZigguratSegment(7918285U, 4.35609564E-07f), new FloatZigguratSegment(8074798U, 4.11186335E-07f), new FloatZigguratSegment(8146897U, 3.9580425E-07f),
					new FloatZigguratSegment(8189112U, 3.8439947E-07f), new FloatZigguratSegment(8217090U, 3.752578E-07f), new FloatZigguratSegment(8237109U, 3.67585073E-07f), new FloatZigguratSegment(8252203U, 3.609465E-07f),
					new FloatZigguratSegment(8264025U, 3.55077276E-07f), new FloatZigguratSegment(8273555U, 3.49803884E-07f), new FloatZigguratSegment(8281414U, 3.45006185E-07f), new FloatZigguratSegment(8288016U, 3.40597552E-07f),
					new FloatZigguratSegment(8293645U, 3.36513267E-07f), new FloatZigguratSegment(8298506U, 3.327038E-07f), new FloatZigguratSegment(8302749U, 3.29130245E-07f), new FloatZigguratSegment(8306487U, 3.25761533E-07f),
					new FloatZigguratSegment(8309806U, 3.22572475E-07f), new FloatZigguratSegment(8312775U, 3.195423E-07f), new FloatZigguratSegment(8315447U, 3.16653654E-07f), new FloatZigguratSegment(8317864U, 3.13891974E-07f),
					new FloatZigguratSegment(8320062U, 3.11244833E-07f), new FloatZigguratSegment(8322069U, 3.08701544E-07f), new FloatZigguratSegment(8323910U, 3.06252929E-07f), new FloatZigguratSegment(8325604U, 3.038909E-07f),
					new FloatZigguratSegment(8327168U, 3.016085E-07f), new FloatZigguratSegment(8328616U, 2.9939946E-07f), new FloatZigguratSegment(8329961U, 2.972583E-07f), new FloatZigguratSegment(8331213U, 2.95180115E-07f),
					new FloatZigguratSegment(8332382U, 2.93160525E-07f), new FloatZigguratSegment(8333475U, 2.911956E-07f), new FloatZigguratSegment(8334499U, 2.89281758E-07f), new FloatZigguratSegment(8335460U, 2.87415816E-07f),
					new FloatZigguratSegment(8336365U, 2.85594865E-07f), new FloatZigguratSegment(8337216U, 2.83816235E-07f), new FloatZigguratSegment(8338019U, 2.82077451E-07f), new FloatZigguratSegment(8338778U, 2.80376355E-07f),
					new FloatZigguratSegment(8339495U, 2.78710871E-07f), new FloatZigguratSegment(8340175U, 2.77079124E-07f), new FloatZigguratSegment(8340818U, 2.7547938E-07f), new FloatZigguratSegment(8341429U, 2.7391E-07f),
					new FloatZigguratSegment(8342010U, 2.723695E-07f), new FloatZigguratSegment(8342561U, 2.70856532E-07f), new FloatZigguratSegment(8343086U, 2.69369764E-07f), new FloatZigguratSegment(8343586U, 2.67908E-07f),
					new FloatZigguratSegment(8344062U, 2.66470153E-07f), new FloatZigguratSegment(8344516U, 2.65055121E-07f), new FloatZigguratSegment(8344949U, 2.63661946E-07f), new FloatZigguratSegment(8345362U, 2.62289717E-07f),
					new FloatZigguratSegment(8345757U, 2.60937554E-07f), new FloatZigguratSegment(8346134U, 2.59604661E-07f), new FloatZigguratSegment(8346495U, 2.582902E-07f), new FloatZigguratSegment(8346839U, 2.5699353E-07f),
					new FloatZigguratSegment(8347169U, 2.557139E-07f), new FloatZigguratSegment(8347484U, 2.54450725E-07f), new FloatZigguratSegment(8347786U, 2.53203325E-07f), new FloatZigguratSegment(8348075U, 2.51971159E-07f),
					new FloatZigguratSegment(8348351U, 2.50753658E-07f), new FloatZigguratSegment(8348616U, 2.495503E-07f), new FloatZigguratSegment(8348869U, 2.483606E-07f), new FloatZigguratSegment(8349112U, 2.471841E-07f),
					new FloatZigguratSegment(8349344U, 2.46020278E-07f), new FloatZigguratSegment(8349566U, 2.44868772E-07f), new FloatZigguratSegment(8349779U, 2.43729119E-07f), new FloatZigguratSegment(8349983U, 2.42600976E-07f),
					new FloatZigguratSegment(8350178U, 2.41483946E-07f), new FloatZigguratSegment(8350364U, 2.40377659E-07f), new FloatZigguratSegment(8350542U, 2.39281775E-07f), new FloatZigguratSegment(8350712U, 2.38195966E-07f),
					new FloatZigguratSegment(8350875U, 2.37119934E-07f), new FloatZigguratSegment(8351030U, 2.36053353E-07f), new FloatZigguratSegment(8351179U, 2.34995937E-07f), new FloatZigguratSegment(8351320U, 2.33947418E-07f),
					new FloatZigguratSegment(8351455U, 2.3290751E-07f), new FloatZigguratSegment(8351583U, 2.31875973E-07f), new FloatZigguratSegment(8351705U, 2.3085255E-07f), new FloatZigguratSegment(8351821U, 2.29837E-07f),
					new FloatZigguratSegment(8351930U, 2.2882908E-07f), new FloatZigguratSegment(8352035U, 2.27828579E-07f), new FloatZigguratSegment(8352133U, 2.26835283E-07f), new FloatZigguratSegment(8352226U, 2.25848993E-07f),
					new FloatZigguratSegment(8352314U, 2.24869481E-07f), new FloatZigguratSegment(8352396U, 2.23896578E-07f), new FloatZigguratSegment(8352474U, 2.22930069E-07f), new FloatZigguratSegment(8352546U, 2.21969813E-07f),
					new FloatZigguratSegment(8352614U, 2.210156E-07f), new FloatZigguratSegment(8352676U, 2.2006725E-07f), new FloatZigguratSegment(8352734U, 2.1912463E-07f), new FloatZigguratSegment(8352788U, 2.18187566E-07f),
					new FloatZigguratSegment(8352837U, 2.172559E-07f), new FloatZigguratSegment(8352882U, 2.16329482E-07f), new FloatZigguratSegment(8352922U, 2.15408164E-07f), new FloatZigguratSegment(8352958U, 2.144918E-07f),
					new FloatZigguratSegment(8352989U, 2.13580265E-07f), new FloatZigguratSegment(8353017U, 2.126734E-07f), new FloatZigguratSegment(8353040U, 2.11771081E-07f), new FloatZigguratSegment(8353060U, 2.10873182E-07f),
					new FloatZigguratSegment(8353075U, 2.09979589E-07f), new FloatZigguratSegment(8353087U, 2.0909016E-07f), new FloatZigguratSegment(8353094U, 2.082048E-07f), new FloatZigguratSegment(8353098U, 2.07323353E-07f),
					new FloatZigguratSegment(8353098U, 2.06445748E-07f), new FloatZigguratSegment(8353094U, 2.05571837E-07f), new FloatZigguratSegment(8353086U, 2.04701536E-07f), new FloatZigguratSegment(8353075U, 2.03834745E-07f),
					new FloatZigguratSegment(8353060U, 2.02971336E-07f), new FloatZigguratSegment(8353041U, 2.0211121E-07f), new FloatZigguratSegment(8353018U, 2.01254281E-07f), new FloatZigguratSegment(8352992U, 2.0040045E-07f),
					new FloatZigguratSegment(8352962U, 1.995496E-07f), new FloatZigguratSegment(8352929U, 1.9870167E-07f), new FloatZigguratSegment(8352891U, 1.97856536E-07f), new FloatZigguratSegment(8352851U, 1.97014117E-07f),
					new FloatZigguratSegment(8352806U, 1.96174341E-07f), new FloatZigguratSegment(8352758U, 1.953371E-07f), new FloatZigguratSegment(8352706U, 1.94502292E-07f), new FloatZigguratSegment(8352651U, 1.93669877E-07f),
					new FloatZigguratSegment(8352592U, 1.92839735E-07f), new FloatZigguratSegment(8352529U, 1.920118E-07f), new FloatZigguratSegment(8352463U, 1.91185975E-07f), new FloatZigguratSegment(8352393U, 1.903622E-07f),
					new FloatZigguratSegment(8352319U, 1.89540373E-07f), new FloatZigguratSegment(8352241U, 1.88720435E-07f), new FloatZigguratSegment(8352160U, 1.879023E-07f), new FloatZigguratSegment(8352075U, 1.87085888E-07f),
					new FloatZigguratSegment(8351986U, 1.86271137E-07f), new FloatZigguratSegment(8351894U, 1.85457949E-07f), new FloatZigguratSegment(8351797U, 1.84646268E-07f), new FloatZigguratSegment(8351696U, 1.83836008E-07f),
					new FloatZigguratSegment(8351592U, 1.830271E-07f), new FloatZigguratSegment(8351484U, 1.8221948E-07f), new FloatZigguratSegment(8351371U, 1.81413057E-07f), new FloatZigguratSegment(8351254U, 1.80607785E-07f),
					new FloatZigguratSegment(8351134U, 1.79803564E-07f), new FloatZigguratSegment(8351009U, 1.79000352E-07f), new FloatZigguratSegment(8350880U, 1.7819805E-07f), new FloatZigguratSegment(8350746U, 1.773966E-07f),
					new FloatZigguratSegment(8350608U, 1.76595933E-07f), new FloatZigguratSegment(8350466U, 1.75795989E-07f), new FloatZigguratSegment(8350319U, 1.74996671E-07f), new FloatZigguratSegment(8350168U, 1.74197936E-07f),
					new FloatZigguratSegment(8350012U, 1.733997E-07f), new FloatZigguratSegment(8349851U, 1.726019E-07f), new FloatZigguratSegment(8349686U, 1.71804459E-07f), new FloatZigguratSegment(8349515U, 1.71007315E-07f),
					new FloatZigguratSegment(8349340U, 1.70210384E-07f), new FloatZigguratSegment(8349159U, 1.69413624E-07f), new FloatZigguratSegment(8348973U, 1.68616936E-07f), new FloatZigguratSegment(8348782U, 1.67820261E-07f),
					new FloatZigguratSegment(8348586U, 1.67023515E-07f), new FloatZigguratSegment(8348383U, 1.66226656E-07f), new FloatZigguratSegment(8348176U, 1.65429583E-07f), new FloatZigguratSegment(8347962U, 1.64632226E-07f),
					new FloatZigguratSegment(8347743U, 1.63834542E-07f), new FloatZigguratSegment(8347517U, 1.63036418E-07f), new FloatZigguratSegment(8347285U, 1.62237811E-07f), new FloatZigguratSegment(8347047U, 1.61438621E-07f),
					new FloatZigguratSegment(8346802U, 1.606388E-07f), new FloatZigguratSegment(8346551U, 1.59838251E-07f), new FloatZigguratSegment(8346293U, 1.590369E-07f), new FloatZigguratSegment(8346028U, 1.5823467E-07f),
					new FloatZigguratSegment(8345755U, 1.57431487E-07f), new FloatZigguratSegment(8345475U, 1.56627266E-07f), new FloatZigguratSegment(8345188U, 1.55821922E-07f), new FloatZigguratSegment(8344892U, 1.55015385E-07f),
					new FloatZigguratSegment(8344589U, 1.54207555E-07f), new FloatZigguratSegment(8344277U, 1.53398361E-07f), new FloatZigguratSegment(8343956U, 1.525877E-07f), new FloatZigguratSegment(8343627U, 1.51775509E-07f),
					new FloatZigguratSegment(8343289U, 1.50961682E-07f), new FloatZigguratSegment(8342941U, 1.50146121E-07f), new FloatZigguratSegment(8342583U, 1.49328741E-07f), new FloatZigguratSegment(8342216U, 1.48509443E-07f),
					new FloatZigguratSegment(8341838U, 1.47688141E-07f), new FloatZigguratSegment(8341449U, 1.46864721E-07f), new FloatZigguratSegment(8341050U, 1.460391E-07f), new FloatZigguratSegment(8340638U, 1.45211146E-07f),
					new FloatZigguratSegment(8340215U, 1.44380778E-07f), new FloatZigguratSegment(8339780U, 1.43547879E-07f), new FloatZigguratSegment(8339332U, 1.42712324E-07f), new FloatZigguratSegment(8338871U, 1.41874025E-07f),
					new FloatZigguratSegment(8338396U, 1.41032842E-07f), new FloatZigguratSegment(8337906U, 1.40188646E-07f), new FloatZigguratSegment(8337402U, 1.39341338E-07f), new FloatZigguratSegment(8336883U, 1.3849079E-07f),
					new FloatZigguratSegment(8336347U, 1.37636832E-07f), new FloatZigguratSegment(8335795U, 1.36779377E-07f), new FloatZigguratSegment(8335225U, 1.35918242E-07f), new FloatZigguratSegment(8334638U, 1.35053313E-07f),
					new FloatZigguratSegment(8334031U, 1.34184418E-07f), new FloatZigguratSegment(8333405U, 1.33311417E-07f), new FloatZigguratSegment(8332758U, 1.32434138E-07f), new FloatZigguratSegment(8332090U, 1.31552426E-07f),
					new FloatZigguratSegment(8331399U, 1.306661E-07f), new FloatZigguratSegment(8330684U, 1.29774975E-07f), new FloatZigguratSegment(8329945U, 1.28878881E-07f), new FloatZigguratSegment(8329180U, 1.27977614E-07f),
					new FloatZigguratSegment(8328388U, 1.2707099E-07f), new FloatZigguratSegment(8327567U, 1.26158781E-07f), new FloatZigguratSegment(8326716U, 1.2524076E-07f), new FloatZigguratSegment(8325833U, 1.24316728E-07f),
					new FloatZigguratSegment(8324918U, 1.23386442E-07f), new FloatZigguratSegment(8323967U, 1.22449634E-07f), new FloatZigguratSegment(8322979U, 1.21506062E-07f), new FloatZigguratSegment(8321952U, 1.20555455E-07f),
					new FloatZigguratSegment(8320883U, 1.19597516E-07f), new FloatZigguratSegment(8319771U, 1.18631959E-07f), new FloatZigguratSegment(8318612U, 1.17658466E-07f), new FloatZigguratSegment(8317404U, 1.16676709E-07f),
					new FloatZigguratSegment(8316143U, 1.15686341E-07f), new FloatZigguratSegment(8314826U, 1.14686983E-07f), new FloatZigguratSegment(8313450U, 1.13678269E-07f), new FloatZigguratSegment(8312011U, 1.12659777E-07f),
					new FloatZigguratSegment(8310504U, 1.11631081E-07f), new FloatZigguratSegment(8308924U, 1.10591714E-07f), new FloatZigguratSegment(8307266U, 1.09541205E-07f), new FloatZigguratSegment(8305525U, 1.08479021E-07f),
					new FloatZigguratSegment(8303694U, 1.07404617E-07f), new FloatZigguratSegment(8301765U, 1.06317415E-07f), new FloatZigguratSegment(8299732U, 1.0521677E-07f), new FloatZigguratSegment(8297586U, 1.04102028E-07f),
					new FloatZigguratSegment(8295317U, 1.02972457E-07f), new FloatZigguratSegment(8292914U, 1.0182729E-07f), new FloatZigguratSegment(8290365U, 1.00665687E-07f), new FloatZigguratSegment(8287658U, 9.94867548E-08f),
					new FloatZigguratSegment(8284776U, 9.828952E-08f), new FloatZigguratSegment(8281703U, 9.707292E-08f), new FloatZigguratSegment(8278418U, 9.583582E-08f), new FloatZigguratSegment(8274901U, 9.457696E-08f),
					new FloatZigguratSegment(8271124U, 9.3294986E-08f), new FloatZigguratSegment(8267059U, 9.1988376E-08f), new FloatZigguratSegment(8262672U, 9.06555E-08f), new FloatZigguratSegment(8257923U, 8.929452E-08f),
					new FloatZigguratSegment(8252764U, 8.790341E-08f), new FloatZigguratSegment(8247143U, 8.647992E-08f), new FloatZigguratSegment(8240993U, 8.502153E-08f), new FloatZigguratSegment(8234236U, 8.35254E-08f),
					new FloatZigguratSegment(8226779U, 8.19883255E-08f), new FloatZigguratSegment(8218507U, 8.040665E-08f), new FloatZigguratSegment(8209278U, 7.877619E-08f), new FloatZigguratSegment(8198919U, 7.709214E-08f),
					new FloatZigguratSegment(8187208U, 7.53488862E-08f), new FloatZigguratSegment(8173862U, 7.353985E-08f), new FloatZigguratSegment(8158514U, 7.16572544E-08f), new FloatZigguratSegment(8140680U, 6.969175E-08f),
					new FloatZigguratSegment(8119703U, 6.76319942E-08f), new FloatZigguratSegment(8094675U, 6.54639862E-08f), new FloatZigguratSegment(8064303U, 6.317016E-08f), new FloatZigguratSegment(8026676U, 6.072799E-08f),
					new FloatZigguratSegment(7978859U, 5.810785E-08f), new FloatZigguratSegment(7916088U, 5.526952E-08f), new FloatZigguratSegment(7830108U, 5.215626E-08f), new FloatZigguratSegment(7705263U, 4.868378E-08f),
					new FloatZigguratSegment(7507891U, 4.471795E-08f), new FloatZigguratSegment(7150247U, 4.0023032E-08f), new FloatZigguratSegment(6309364U, 3.411467E-08f), new FloatZigguratSegment(0U, 2.56588333E-08f),
				},
				new float[]
				{
					0.00126026315f, 0.00260904827f, 0.004037947f, 0.005522377f,
					0.00705084857f, 0.00861655548f, 0.0102149434f, 0.0118427295f,
					0.0134974224f, 0.01517706f, 0.016880054f, 0.0186050925f,
					0.0203510672f, 0.0221170336f, 0.0239021741f, 0.0257057734f,
					0.0275272056f, 0.0293659084f, 0.031221386f, 0.033093188f,
					0.03498091f, 0.0368841849f, 0.0388026759f, 0.04073608f,
					0.04268411f, 0.04464652f, 0.0466230623f, 0.0486135222f,
					0.05061769f, 0.0526353866f, 0.05466643f, 0.0567106567f,
					0.0587679222f, 0.0608380772f, 0.0629209951f, 0.0650165454f,
					0.06712462f, 0.0692451149f, 0.07137792f, 0.07352294f,
					0.0756801f, 0.0778493062f, 0.080030486f, 0.0822235644f,
					0.0844284743f, 0.08664516f, 0.08887356f, 0.09111361f,
					0.09336528f, 0.09562851f, 0.0979032442f, 0.10018947f,
					0.102487125f, 0.104796194f, 0.107116632f, 0.109448425f,
					0.111791536f, 0.114145942f, 0.116511635f, 0.118888579f,
					0.121276774f, 0.123676196f, 0.126086831f, 0.128508687f,
					0.130941749f, 0.133386f, 0.135841444f, 0.138308078f,
					0.140785918f, 0.143274948f, 0.145775169f, 0.148286611f,
					0.150809258f, 0.153343126f, 0.15588823f, 0.158444583f,
					0.161012188f, 0.163591072f, 0.166181251f, 0.168782741f,
					0.17139557f, 0.174019739f, 0.176655293f, 0.179302245f,
					0.181960627f, 0.184630454f, 0.187311783f, 0.190004617f,
					0.192709f, 0.195424974f, 0.198152557f, 0.2008918f,
					0.203642711f, 0.206405371f, 0.2091798f, 0.211966053f,
					0.214764148f, 0.217574149f, 0.2203961f, 0.223230049f,
					0.226076037f, 0.228934139f, 0.231804386f, 0.234686837f,
					0.237581551f, 0.240488574f, 0.24340798f, 0.246339828f,
					0.249284178f, 0.2522411f, 0.255210638f, 0.258192867f,
					0.261187881f, 0.26419574f, 0.2672165f, 0.270250231f,
					0.273297f, 0.276356965f, 0.279430121f, 0.282516569f,
					0.2856164f, 0.2887297f, 0.291856557f, 0.294997066f,
					0.298151284f, 0.301319361f, 0.304501355f, 0.3076974f,
					0.310907543f, 0.314131916f, 0.317370623f, 0.320623755f,
					0.323891461f, 0.327173829f, 0.330470949f, 0.333783f,
					0.337110043f, 0.340452224f, 0.3438097f, 0.347182542f,
					0.350570917f, 0.353974968f, 0.357394785f, 0.360830575f,
					0.364282429f, 0.367750555f, 0.371235043f, 0.374736071f,
					0.3782538f, 0.381788373f, 0.38534f, 0.388908833f,
					0.392495036f, 0.3960988f, 0.399720281f, 0.4033597f,
					0.407017261f, 0.4106931f, 0.414387524f, 0.418100625f,
					0.421832681f, 0.4255839f, 0.429354519f, 0.433144748f,
					0.436954826f, 0.440785021f, 0.44463554f, 0.448506683f,
					0.4523987f, 0.456311822f, 0.460246384f, 0.464202672f,
					0.468180925f, 0.472181529f, 0.476204723f, 0.480250835f,
					0.484320253f, 0.488413274f, 0.492530257f, 0.496671557f,
					0.500837564f, 0.505028665f, 0.5092452f, 0.5134877f,
					0.5177565f, 0.52205205f, 0.5263748f, 0.5307253f,
					0.5351039f, 0.5395112f, 0.5439477f, 0.548413932f,
					0.552910447f, 0.5574379f, 0.561996758f, 0.566587746f,
					0.5712115f, 0.575868666f, 0.580559969f, 0.58528614f,
					0.590047956f, 0.594846249f, 0.599681735f, 0.604555368f,
					0.609468043f, 0.6144207f, 0.6194143f, 0.62445f,
					0.629528761f, 0.6346518f, 0.6398203f, 0.645035446f,
					0.6502987f, 0.655611455f, 0.660975158f, 0.6663913f,
					0.6718617f, 0.677388f, 0.682972133f, 0.6886161f,
					0.69432193f, 0.7000919f, 0.7059285f, 0.711834252f,
					0.717811942f, 0.7238645f, 0.729995251f, 0.7362076f,
					0.7425053f, 0.7488924f, 0.7553735f, 0.761953354f,
					0.7686373f, 0.7754313f, 0.782341838f, 0.78937614f,
					0.796542346f, 0.803849459f, 0.811307847f, 0.8189292f,
					0.826726854f, 0.83471626f, 0.842915654f, 0.851346254f,
					0.860033631f, 0.86900866f, 0.878309667f, 0.887984633f,
					0.8980959f, 0.908726454f, 0.9199915f, 0.932060063f,
					0.945198953f, 0.9598791f, 0.9771017f, 1f,
				},
				-2147483392, 255, 8);

			#endregion
		}

		public static class NormalDouble
		{
			public const double totalArea = 2.506628274631d;

			public static double F(double x)
			{
				return Math.Exp(-x * x * 0.5d);
			}

			public static double Inv(double y)
			{
				return Math.Sqrt(Math.Log(1d / (y * y)));
			}

			public static double CDF(double x)
			{
				// https://en.wikipedia.org/wiki/Normal_distribution#Numerical_approximations_for_the_normal_CDF
				double t = 1d / (1d + 0.2316419d * x);
				return totalArea - F(x) * ((((1.330274429d * t - 1.821255978d) * t + 1.781477937d) * t - 0.356563782d) * t + 0.319381530d) * t;
			}

			public static double SampleFallback(IRandom random, double xMin)
			{
				// https://en.wikipedia.org/wiki/Ziggurat_algorithm#Fallback_algorithms_for_the_tail
				double x, y;
				do
				{
					x = -Math.Log(random.PreciseDoubleOO()) / xMin;
					y = -Math.Log(random.PreciseDoubleOO());
				} while (y * 2d <= x * x);
				return xMin + x;
			}

			public static double Sample(IRandom random, TwoSidedSymmetricDoubleZigguratTable ziggurat)
			{
				do
				{
					// Select a random segment, and a random n/x within the segment.
					long n;
					do
					{
						n = (long)random.Next64();
					} while (n < ziggurat.threshold);
					int i = (int)(n & ziggurat.mask);
					n = n >> ziggurat.shift;
					var segment = ziggurat.segments[i];
					double x = n * segment.s;


					// Dominant quick check within fully-contained segment rectangle.
					if ((ulong)Math.Abs(n) < segment.n) return x;

					// Rare case within tail.
					if (i == 0) return SampleFallback(random, (1L << (63 - ziggurat.shift)) * ziggurat.segments[1].s * (n > 0L ? +1d : -1d));

					// Slow check within the partially-contained segment rectangle.
					if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < F(x)) return x;
				} while (true);
			}

#if UNITY_EDITOR
			//[UnityEditor.Callbacks.DidReloadScripts] // Uncomment this attribute in order to generate and print tables in the Unity console pane.
			private static void GenerateZigguratLookupTable()
			{
				var table = GenerateTwoSidedSymmetricDoubleZigguratTable(8,
					NormalDouble.F,
					NormalDouble.Inv,
					NormalDouble.CDF,
					NormalDouble.totalArea,
					0.0000000001d);

				Distributions.GenerateZigguratLookupTable(table, "normal");
			}
#endif

			#region Lookup Table

			public static readonly TwoSidedSymmetricDoubleZigguratTable zigguratTable = new TwoSidedSymmetricDoubleZigguratTable(
				new DoubleZigguratSegment[]
				{
					new DoubleZigguratSegment(33664194707075580UL, 1.0854731187964883E-16d), new DoubleZigguratSegment(34008778872348400UL, 1.0142325429635547E-16d), new DoubleZigguratSegment(34680993337014104UL, 9.5736780388841845E-17d), new DoubleZigguratSegment(34990657568770336UL, 9.2155356756013127E-17d),
					new DoubleZigguratSegment(35171969675147616UL, 8.94999777450312E-17d), new DoubleZigguratSegment(35292134271790764UL, 8.7371512890583414E-17d), new DoubleZigguratSegment(35378117278016196UL, 8.5585071376125462E-17d), new DoubleZigguratSegment(35442945514234080UL, 8.4039405778611841E-17d),
					new DoubleZigguratSegment(35493718323093744UL, 8.2672870773124692E-17d), new DoubleZigguratSegment(35534649467085456UL, 8.1445061477858823E-17d), new DoubleZigguratSegment(35568405008977104UL, 8.0328013974976749E-17d), new DoubleZigguratSegment(35596757795375192UL, 7.9301546846675833E-17d),
					new DoubleZigguratSegment(35620934667556988UL, 7.8350602558666633E-17d), new DoubleZigguratSegment(35641812848711008UL, 7.7463638140262342E-17d), new DoubleZigguratSegment(35660036954743912UL, 7.6631603650886906E-17d), new DoubleZigguratSegment(35676091784898296UL, 7.5847267857806921E-17d),
					new DoubleZigguratSegment(35690349282072844UL, 7.5104758238378093E-17d), new DoubleZigguratSegment(35703099787115232UL, 7.4399238277715669E-17d), new DoubleZigguratSegment(35714573408379836UL, 7.3726675551129153E-17d), new DoubleZigguratSegment(35724954981311136UL, 7.308367150700726E-17d),
					new DoubleZigguratSegment(35734394760846728UL, 7.246733420165257E-17d), new DoubleZigguratSegment(35743016206439688UL, 7.1875181573923323E-17d), new DoubleZigguratSegment(35750921744806292UL, 7.1305066846536944E-17d), new DoubleZigguratSegment(35758197099729808UL, 7.0755120230545752E-17d),
					new DoubleZigguratSegment(35764914589387784UL, 7.022370282547083E-17d), new DoubleZigguratSegment(35771135668386020UL, 6.9709369768342458E-17d), new DoubleZigguratSegment(35776912909584068UL, 6.9210840484864176E-17d), new DoubleZigguratSegment(35782291565124904UL, 6.8726974456648237E-17d),
					new DoubleZigguratSegment(35787310807698084UL, 6.8256751317626843E-17d), new DoubleZigguratSegment(35792004726196092UL, 6.7799254380931024E-17d), new DoubleZigguratSegment(35796403130849804UL, 6.7353656908321467E-17d), new DoubleZigguratSegment(35800532209211864UL, 6.6919210590299986E-17d),
					new DoubleZigguratSegment(35804415064373916UL, 6.6495235821835874E-17d), new DoubleZigguratSegment(35808072159456316UL, 6.6081113446981766E-17d), new DoubleZigguratSegment(35811521686945528UL, 6.5676277713109855E-17d), new DoubleZigguratSegment(35814779877351944UL, 6.528021022747218E-17d),
					new DoubleZigguratSegment(35817861258552220UL, 6.4892434749169039E-17d), new DoubleZigguratSegment(35820778874804868UL, 6.4512512681230418E-17d), new DoubleZigguratSegment(35823544472597684UL, 6.4140039152460463E-17d), new DoubleZigguratSegment(35826168659065284UL, 6.3774639598510711E-17d),
					new DoubleZigguratSegment(35828661037604592UL, 6.34159667674924E-17d), new DoubleZigguratSegment(35831030324442284UL, 6.3063698088186088E-17d), new DoubleZigguratSegment(35833284449216072UL, 6.2717533349223272E-17d), new DoubleZigguratSegment(35835430642080296UL, 6.2377192646010181E-17d),
					new DoubleZigguratSegment(35837475509404112UL, 6.2042414559032007E-17d), new DoubleZigguratSegment(35839425099774752UL, 6.1712954532822292E-17d), new DoubleZigguratSegment(35841284961729536UL, 6.1388583429547154E-17d), new DoubleZigguratSegment(35843060194405860UL, 6.1069086235024589E-17d),
					new DoubleZigguratSegment(35844755492105972UL, 6.0754260898225572E-17d), new DoubleZigguratSegment(35846375183616132UL, 6.0443917288003085E-17d), new DoubleZigguratSegment(35847923266988940UL, 6.01378762530641E-17d), new DoubleZigguratSegment(35849403440390440UL, 5.9835968773111986E-17d),
					new DoubleZigguratSegment(35850819129523364UL, 5.9538035190706432E-17d), new DoubleZigguratSegment(35852173512063472UL, 5.924392451476283E-17d), new DoubleZigguratSegment(35853469539482884UL, 5.8953493787785243E-17d), new DoubleZigguratSegment(35854709956581572UL, 5.86666075099288E-17d),
					new DoubleZigguratSegment(35855897319003892UL, 5.8383137113846216E-17d), new DoubleZigguratSegment(35857034008979116UL, 5.8102960485012374E-17d), new DoubleZigguratSegment(35858122249492996UL, 5.7825961522857639E-17d), new DoubleZigguratSegment(35859164117070260UL, 5.7552029738591955E-17d),
					new DoubleZigguratSegment(35860161553324524UL, 5.7281059886079485E-17d), new DoubleZigguratSegment(35861116375412264UL, 5.7012951622538921E-17d), new DoubleZigguratSegment(35862030285510420UL, 5.6747609196206521E-17d), new DoubleZigguratSegment(35862904879422248UL, 5.6484941158415288E-17d),
					new DoubleZigguratSegment(35863741654403748UL, 5.6224860097820187E-17d), new DoubleZigguratSegment(35864542016291452UL, 5.5967282394742606E-17d), new DoubleZigguratSegment(35865307286003252UL, 5.571212799382039E-17d), new DoubleZigguratSegment(35866038705475488UL, 5.545932019333822E-17d),
					new DoubleZigguratSegment(35866737443092224UL, 5.5208785449779225E-17d), new DoubleZigguratSegment(35867404598656440UL, 5.4960453196285553E-17d), new DoubleZigguratSegment(35868041207947340UL, 5.4714255673845981E-17d), new DoubleZigguratSegment(35868648246903020UL, 5.4470127774144235E-17d),
					new DoubleZigguratSegment(35869226635463676UL, 5.4228006893104534E-17d), new DoubleZigguratSegment(35869777241106620UL, 5.3987832794262582E-17d), new DoubleZigguratSegment(35870300882101204UL, 5.3749547481171936E-17d), new DoubleZigguratSegment(35870798330508716UL, 5.35130950781288E-17d),
					new DoubleZigguratSegment(35871270314949872UL, 5.3278421718563805E-17d), new DoubleZigguratSegment(35871717523160160UL, 5.3045475440507924E-17d), new DoubleZigguratSegment(35872140604351216UL, 5.28142060885925E-17d), new DoubleZigguratSegment(35872540171394800UL, 5.2584565222090544E-17d),
					new DoubleZigguratSegment(35872916802844080UL, 5.235650602854949E-17d), new DoubleZigguratSegment(35873271044805700UL, 5.212998324260366E-17d), new DoubleZigguratSegment(35873603412674756UL, 5.1904953069589761E-17d), new DoubleZigguratSegment(35873914392743628UL, 5.1681373113620042E-17d),
					new DoubleZigguratSegment(35874204443694672UL, 5.1459202309796161E-17d), new DoubleZigguratSegment(35874473997985716UL, 5.1238400860272662E-17d), new DoubleZigguratSegment(35874723463136704UL, 5.1018930173902257E-17d), new DoubleZigguratSegment(35874953222924820UL, 5.0800752809216491E-17d),
					new DoubleZigguratSegment(35875163638494956UL, 5.0583832420514567E-17d), new DoubleZigguratSegment(35875355049391712UL, 5.0368133706850852E-17d), new DoubleZigguratSegment(35875527774518516UL, 5.0153622363727465E-17d), new DoubleZigguratSegment(35875682113029072UL, 4.9940265037313126E-17d),
					new DoubleZigguratSegment(35875818345155812UL, 4.972802928102276E-17d), new DoubleZigguratSegment(35875936732979608UL, 4.9516883514304547E-17d), new DoubleZigguratSegment(35876037521144716UL, 4.9306796983492352E-17d), new DoubleZigguratSegment(35876120937522532UL, 4.909773972459166E-17d),
					new DoubleZigguratSegment(35876187193827380UL, 4.8889682527876581E-17d), new DoubleZigguratSegment(35876236486187384UL, 4.8682596904184085E-17d), new DoubleZigguratSegment(35876268995673216UL, 4.8476455052799463E-17d), new DoubleZigguratSegment(35876284888787052UL, 4.8271229830834529E-17d),
					new DoubleZigguratSegment(35876284317914272UL, 4.8066894724006371E-17d), new DoubleZigguratSegment(35876267421739792UL, 4.7863423818731043E-17d), new DoubleZigguratSegment(35876234325631068UL, 4.7660791775451929E-17d), new DoubleZigguratSegment(35876185141989488UL, 4.7458973803127895E-17d),
					new DoubleZigguratSegment(35876119970571700UL, 4.7257945634811152E-17d), new DoubleZigguratSegment(35876038898782456UL, 4.7057683504249107E-17d), new DoubleZigguratSegment(35875942001940124UL, 4.6858164123448746E-17d), new DoubleZigguratSegment(35875829343516196UL, 4.6659364661145692E-17d),
					new DoubleZigguratSegment(35875700975349836UL, 4.6461262722123733E-17d), new DoubleZigguratSegment(35875556937838412UL, 4.6263836327333759E-17d), new DoubleZigguratSegment(35875397260104932UL, 4.60670638947641E-17d), new DoubleZigguratSegment(35875221960143116UL, 4.5870924221016983E-17d),
					new DoubleZigguratSegment(35875031044940916UL, 4.5675396463548382E-17d), new DoubleZigguratSegment(35874824510582828UL, 4.5480460123530982E-17d), new DoubleZigguratSegment(35874602342331888UL, 4.5286095029301979E-17d), new DoubleZigguratSegment(35874364514691508UL, 4.5092281320359729E-17d),
					new DoubleZigguratSegment(35874110991447596UL, 4.4898999431874913E-17d), new DoubleZigguratSegment(35873841725691400UL, 4.4706230079683639E-17d), new DoubleZigguratSegment(35873556659823120UL, 4.4513954245731648E-17d), new DoubleZigguratSegment(35873255725536628UL, 4.4322153163940065E-17d),
					new DoubleZigguratSegment(35872938843785276UL, 4.4130808306464661E-17d), new DoubleZigguratSegment(35872605924728916UL, 4.3939901370321723E-17d), new DoubleZigguratSegment(35872256867662032UL, 4.3749414264354834E-17d), new DoubleZigguratSegment(35871891560923020UL, 4.3559329096517885E-17d),
					new DoubleZigguratSegment(35871509881784372UL, 4.3369628161450639E-17d), new DoubleZigguratSegment(35871111696323500UL, 4.3180293928324075E-17d), new DoubleZigguratSegment(35870696859274160UL, 4.2991309028933341E-17d), new DoubleZigguratSegment(35870265213857844UL, 4.2802656246017204E-17d),
					new DoubleZigguratSegment(35869816591594968UL, 4.2614318501783156E-17d), new DoubleZigguratSegment(35869350812095264UL, 4.2426278846618258E-17d), new DoubleZigguratSegment(35868867682826892UL, 4.2238520447966022E-17d), new DoubleZigguratSegment(35868366998863560UL, 4.2051026579350316E-17d),
					new DoubleZigguratSegment(35867848542609188UL, 4.1863780609527425E-17d), new DoubleZigguratSegment(35867312083499068UL, 4.1676765991747995E-17d), new DoubleZigguratSegment(35866757377676896UL, 4.1489966253110574E-17d), new DoubleZigguratSegment(35866184167646664UL, 4.1303364983988877E-17d),
					new DoubleZigguratSegment(35865592181898328UL, 4.1116945827514946E-17d), new DoubleZigguratSegment(35864981134506320UL, 4.0930692469100447E-17d), new DoubleZigguratSegment(35864350724699460UL, 4.07445886259785E-17d), new DoubleZigguratSegment(35863700636401124UL, 4.0558618036748247E-17d),
					new DoubleZigguratSegment(35863030537738160UL, 4.037276445090438E-17d), new DoubleZigguratSegment(35862340080516980UL, 4.0187011618333752E-17d), new DoubleZigguratSegment(35861628899665208UL, 4.0001343278760819E-17d), new DoubleZigguratSegment(35860896612636980UL, 3.9815743151123617E-17d),
					new DoubleZigguratSegment(35860142818779980UL, 3.9630194922861487E-17d), new DoubleZigguratSegment(35859367098662056UL, 3.9444682239095424E-17d), new DoubleZigguratSegment(35858569013355140UL, 3.9259188691681492E-17d), new DoubleZigguratSegment(35857748103673908UL, 3.9073697808117217E-17d),
					new DoubleZigguratSegment(35856903889366572UL, 3.88881930402802E-17d), new DoubleZigguratSegment(35856035868254904UL, 3.8702657752977574E-17d), new DoubleZigguratSegment(35855143515320252UL, 3.8517075212284249E-17d), new DoubleZigguratSegment(35854226281732248UL, 3.83314285736468E-17d),
					new DoubleZigguratSegment(35853283593816532UL, 3.8145700869729168E-17d), new DoubleZigguratSegment(35852314851957512UL, 3.79598749979752E-17d), new DoubleZigguratSegment(35851319429431836UL, 3.7773933707861888E-17d), new DoubleZigguratSegment(35850296671168024UL, 3.7587859587815938E-17d),
					new DoubleZigguratSegment(35849245892427168UL, 3.7401635051764994E-17d), new DoubleZigguratSegment(35848166377399248UL, 3.7215242325293222E-17d), new DoubleZigguratSegment(35847057377709304UL, 3.70286634313694E-17d), new DoubleZigguratSegment(35845918110826860UL, 3.6841880175613884E-17d),
					new DoubleZigguratSegment(35844747758371804UL, 3.6654874131068793E-17d), new DoubleZigguratSegment(35843545464309232UL, 3.6467626622433658E-17d), new DoubleZigguratSegment(35842310333024872UL, 3.6280118709726541E-17d), new DoubleZigguratSegment(35841041427272436UL, 3.6092331171327947E-17d),
					new DoubleZigguratSegment(35839737765982992UL, 3.5904244486362238E-17d), new DoubleZigguratSegment(35838398321925948UL, 3.5715838816368176E-17d), new DoubleZigguratSegment(35837022019210084UL, 3.5527093986206982E-17d), new DoubleZigguratSegment(35835607730612088UL, 3.5337989464152724E-17d),
					new DoubleZigguratSegment(35834154274718872UL, 3.5148504341105944E-17d), new DoubleZigguratSegment(35832660412868724UL, 3.4958617308867188E-17d), new DoubleZigguratSegment(35831124845874880UL, 3.4768306637402508E-17d), new DoubleZigguratSegment(35829546210513544UL, 3.4577550151027855E-17d),
					new DoubleZigguratSegment(35827923075756752UL, 3.4386325203433822E-17d), new DoubleZigguratSegment(35826253938728312UL, 3.4194608651466136E-17d), new DoubleZigguratSegment(35824537220359376UL, 3.400237682757051E-17d), new DoubleZigguratSegment(35822771260717184UL, 3.38096055108035E-17d),
					new DoubleZigguratSegment(35820954313978616UL, 3.3616269896302648E-17d), new DoubleZigguratSegment(35819084543016604UL, 3.3422344563100776E-17d), new DoubleZigguratSegment(35817160013564636UL, 3.3227803440159466E-17d), new DoubleZigguratSegment(35815178687920572UL, 3.3032619770486245E-17d),
					new DoubleZigguratSegment(35813138418147292UL, 3.283676607318827E-17d), new DoubleZigguratSegment(35811036938722564UL, 3.26402141033026E-17d), new DoubleZigguratSegment(35808871858585836UL, 3.2442934809228737E-17d), new DoubleZigguratSegment(35806640652523676UL, 3.2244898287573534E-17d),
					new DoubleZigguratSegment(35804340651828860UL, 3.2046073735201342E-17d), new DoubleZigguratSegment(35801969034161036UL, 3.1846429398262723E-17d), new DoubleZigguratSegment(35799522812528312UL, 3.1645932517954048E-17d), new DoubleZigguratSegment(35796998823299900UL, 3.1444549272736306E-17d),
					new DoubleZigguratSegment(35794393713148992UL, 3.1242244716715365E-17d), new DoubleZigguratSegment(35791703924813096UL, 3.1038982713856265E-17d), new DoubleZigguratSegment(35788925681545224UL, 3.083472586767153E-17d), new DoubleZigguratSegment(35786054970113428UL, 3.0629435445986742E-17d),
					new DoubleZigguratSegment(35783087522188376UL, 3.0423071300345669E-17d), new DoubleZigguratSegment(35780018793937868UL, 3.0215591779571182E-17d), new DoubleZigguratSegment(35776843943623812UL, 3.0006953636946593E-17d), new DoubleZigguratSegment(35773557806969784UL, 2.9797111930423926E-17d),
					new DoubleZigguratSegment(35770154870036240UL, 2.9586019915200083E-17d), new DoubleZigguratSegment(35766629239304296UL, 2.9373628927927925E-17d), new DoubleZigguratSegment(35762974608627224UL, 2.9159888261745578E-17d), new DoubleZigguratSegment(35759184222660468UL, 2.8944745031212309E-17d),
					new DoubleZigguratSegment(35755250836324664UL, 2.8728144026131585E-17d), new DoubleZigguratSegment(35751166669790432UL, 2.8510027553119066E-17d), new DoubleZigguratSegment(35746923358397352UL, 2.8290335263633149E-17d), new DoubleZigguratSegment(35742511896829180UL, 2.8069003967025613E-17d),
					new DoubleZigguratSegment(35737922576762168UL, 2.7845967426985929E-17d), new DoubleZigguratSegment(35733144917078656UL, 2.7621156139541869E-17d), new DoubleZigguratSegment(35728167585590724UL, 2.7394497090535609E-17d), new DoubleZigguratSegment(35722978311044436UL, 2.7165913490213404E-17d),
					new DoubleZigguratSegment(35717563783966876UL, 2.6935324482241311E-17d), new DoubleZigguratSegment(35711909544670368UL, 2.6702644824080875E-17d), new DoubleZigguratSegment(35705999856430460UL, 2.6467784535217825E-17d), new DoubleZigguratSegment(35699817561496592UL, 2.6230648509221188E-17d),
					new DoubleZigguratSegment(35693343917162064UL, 2.5991136085005665E-17d), new DoubleZigguratSegment(35686558408595640UL, 2.574914057195866E-17d), new DoubleZigguratSegment(35679438534499520UL, 2.5504548722752941E-17d), new DoubleZigguratSegment(35671959560877180UL, 2.5257240146670295E-17d),
					new DoubleZigguratSegment(35664094237236188UL, 2.5007086655076293E-17d), new DoubleZigguratSegment(35655812468366472UL, 2.4753951529270857E-17d), new DoubleZigguratSegment(35647080933365484UL, 2.4497688699240826E-17d), new DoubleZigguratSegment(35637862641748124UL, 2.42381418197943E-17d),
					new DoubleZigguratSegment(35628116414179804UL, 2.3975143228078815E-17d), new DoubleZigguratSegment(35617796272468200UL, 2.3708512763471374E-17d), new DoubleZigguratSegment(35606850719764496UL, 2.3438056427142347E-17d), new DoubleZigguratSegment(35595221887214084UL, 2.3163564854063853E-17d),
					new DoubleZigguratSegment(35582844517235560UL, 2.2884811564629531E-17d), new DoubleZigguratSegment(35569644745748208UL, 2.2601550956081931E-17d), new DoubleZigguratSegment(35555538635402056UL, 2.2313515985216139E-17d), new DoubleZigguratSegment(35540430398342264UL, 2.2020415482826678E-17d),
					new DoubleZigguratSegment(35524210229071720UL, 2.1721931026396633E-17d), new DoubleZigguratSegment(35506751643877428UL, 2.1417713279656403E-17d), new DoubleZigguratSegment(35487908190645160UL, 2.1107377684585354E-17d), new DoubleZigguratSegment(35467509348197948UL, 2.0790499361429334E-17d),
					new DoubleZigguratSegment(35445355372412284UL, 2.0466607032898442E-17d), new DoubleZigguratSegment(35421210759614272UL, 2.0135175736418704E-17d), new DoubleZigguratSegment(35394795874520784UL, 1.9795618018169048E-17d), new DoubleZigguratSegment(35365776112393596UL, 1.9447273207436863E-17d),
					new DoubleZigguratSegment(35333747705118680UL, 1.908939423896812E-17d), new DoubleZigguratSegment(35298218893900992UL, 1.8721131308609017E-17d), new DoubleZigguratSegment(35258584604257224UL, 1.8341511389484261E-17d), new DoubleZigguratSegment(35214091850277996UL, 1.7949412264741622E-17d),
					new DoubleZigguratSegment(35163791656304948UL, 1.7543529189065707E-17d), new DoubleZigguratSegment(35106470948133716UL, 1.712233147823125E-17d), new DoubleZigguratSegment(35040553965378924UL, 1.6684005083168497E-17d), new DoubleZigguratSegment(34963956025378688UL, 1.6226375256651104E-17d),
					new DoubleZigguratSegment(34873860465221124UL, 1.5746800278294615E-17d), new DoubleZigguratSegment(34766367254131880UL, 1.5242021968979491E-17d), new DoubleZigguratSegment(34635918196021084UL, 1.4707949676759017E-17d), new DoubleZigguratSegment(34474313779230784UL, 1.4139338084679377E-17d),
					new DoubleZigguratSegment(34268938726180456UL, 1.3529288183152378E-17d), new DoubleZigguratSegment(33999340538109528UL, 1.2868438197180147E-17d), new DoubleZigguratSegment(33630057975745736UL, 1.2143575380250794E-17d), new DoubleZigguratSegment(33093853899417256UL, 1.1335075768855529E-17d),
					new DoubleZigguratSegment(32246150418226184UL, 1.0411708757188931E-17d), new DoubleZigguratSegment(30710080583058260UL, 9.3185883091894422E-18d), new DoubleZigguratSegment(27098515317920860UL, 7.9429406911622032E-18d), new DoubleZigguratSegment(0UL, 5.97416283079066E-18d),
				},
				new double[]
				{
					0.0012602631037794076d, 0.0026090481839409815d, 0.0040379470002657189d, 0.0055223769562859539d,
					0.0070508485338551625d, 0.0086165553374865853d, 0.010214943584137251d, 0.011842729631753177d,
					0.013497422046532948d, 0.015177059457249942d, 0.01688005403419755d, 0.018605091913229687d,
					0.020351066643595386d, 0.022117032914488306d, 0.023902173322087931d, 0.025705773847723452d,
					0.027527205344009837d, 0.02936590927893078d, 0.03122138656925684d, 0.033093188701737858d,
					0.034980910579235004d, 0.036884184688332884d, 0.038802676293856184d, 0.040736079441651259d,
					0.042684113604935771d, 0.044646520848480864d, 0.046623063413460605d, 0.048613521647041753d,
					0.050617692216774535d, 0.052635386562021366d, 0.054666429544031385d, 0.056710658263552288d,
					0.058767921020584771d, 0.060838076395404707d, 0.062920992433580986d, 0.065016545920612942d,
					0.06712462173415161d, 0.069245112263675815d, 0.071377916889054788d, 0.073522941510715728d,
					0.07568009812519913d, 0.077849304440773273d, 0.0800304835285196d, 0.082223563504925129d,
					0.0844284772425457d, 0.086645162105747311d, 0.0888735597089166d, 0.0911136156948517d,
					0.093365279531327575d, 0.095628504324064639d, 0.097903246644540087d, 0.10018946637125786d,
					0.10248712654325007d, 0.1047961932247166d, 0.10711663537982939d, 0.10944842475683067d,
					0.1117915357806457d, 0.11414594545331015d, 0.11651163326158441d, 0.1188885810911868d,
					0.12127677314713731d, 0.1236761958797481d, 0.12608683791584466d, 0.12850868999483861d,
					0.13094174490930813d, 0.13338599744977464d, 0.13584144435339196d, 0.13830808425628768d,
					0.1407859176493225d, 0.14327494683705133d, 0.14577517589968933d, 0.14828661065790358d,
					0.1508092586402647d, 0.15334312905320882d, 0.15588823275337071d, 0.15844458222216212d,
					0.16101219154247903d, 0.16359107637743125d, 0.16618125395099712d, 0.16878274303051302d,
					0.17139556391091637d, 0.17401973840066678d, 0.17665528980927606d, 0.17930224293638461d,
					0.18196062406232591d, 0.18463046094012789d, 0.18731178278890181d, 0.19000462028857643d,
					0.19270900557593706d, 0.19542497224193547d, 0.19815255533023685d, 0.20089179133697707d,
					0.20364271821170368d, 0.20640537535947912d, 0.20917980364412631d, 0.21196604539260014d,
					0.21476414440047067d, 0.21757414593850696d, 0.22039609676035149d, 0.22323004511127939d,
					0.22607604073803739d, 0.22893413489975975d, 0.23180438037996215d, 0.234686831499614d,
					0.23758154413129448d, 0.24048857571443658d, 0.2434079852716689d, 0.24633983342626395d,
					0.24928418242070521d, 0.25224109613638718d, 0.25521064011446359d, 0.25819288157786274d,
					0.26118788945448895d, 0.26419573440163319d, 0.26721648883161536d, 0.27025022693868683d,
					0.27329702472721934d, 0.27635696004121318d, 0.27943011259515582d, 0.28251656400626773d,
					0.28561639782817211d, 0.28872969958603067d, 0.29185655681318706d, 0.29499705908936413d,
					0.2981512980804642d, 0.30131936758002414d, 0.30450136355237972d, 0.30769738417759696d,
					0.31090752989823395d, 0.31413190346799535d, 0.31737061000235139d, 0.32062375703119322d,
					0.32389145455360052d, 0.3271738150948057d, 0.33047095376543795d, 0.33378298832314d,
					0.33711003923665456d, 0.340452229752479d, 0.34380968596420125d, 0.34718253688462491d,
					0.35057091452080869d, 0.3539749539521444d, 0.35739479341161062d, 0.36083057437034172d,
					0.36428244162566636d, 0.36775054339277291d, 0.37123503140017139d, 0.37473606098913287d,
					0.37825379121729397d, 0.38178838496662981d, 0.38534000905600846d, 0.38890883435855428d,
					0.39249503592406065d, 0.3960987931067082d, 0.39972028969836149d, 0.40335971406773158d,
					0.40701725930571442d, 0.41069312337723179d, 0.41438750927992329d, 0.41810062521006269d,
					0.4218326847360947d, 0.42558390698021531d, 0.42935451680844844d, 0.43314474502970085d,
					0.43695482860431323d, 0.44078501086265803d, 0.44463554173437719d, 0.44850667798889243d,
					0.45239868348786821d, 0.45631182945035631d, 0.46024639473140438d, 0.46420266611497124d,
					0.4681809386220519d, 0.47218151583498907d, 0.47620471023901778d, 0.48025084358217812d,
					0.48432024725481471d, 0.48841326268998525d, 0.49253024178620258d, 0.49667154735405783d,
					0.50083755358839455d, 0.5050286465678524d, 0.50924522478374934d, 0.51348769970044261d,
					0.5177564963495026d, 0.52205205396023491d, 0.52637482662932111d, 0.53072528403260044d,
					0.53510391218229825d, 0.53951121423331849d, 0.54394771134256592d, 0.54841394358565088d,
					0.55291047093576107d, 0.5574378743099635d, 0.56199675668874394d, 0.56658774431518844d,
					0.57121148798089449d, 0.57586866440645668d, 0.58055997772523316d, 0.5852861610800647d,
					0.59004797834371825d, 0.5948462259750642d, 0.59968173502441113d, 0.60455537330302844d,
					0.60946804773371765d, 0.61442070690139527d, 0.61941434382505389d, 0.62444999897523468d,
					0.62952876356433585d, 0.63465178314077086d, 0.63982026152226645d, 0.64503546510857d,
					0.65029872761964114d, 0.65561145531220477d, 0.6609751327355311d, 0.66639132909673138d,
					0.67186170531700928d, 0.677388021873564d, 0.68297214753765556d, 0.68861606913830564d,
					0.69432190250392845d, 0.70009190476181493d, 0.705928488208979d, 0.71183423600894935d,
					0.71781192001958771d, 0.72386452111949351d, 0.72999525247832509d, 0.73620758631385352d,
					0.74250528480162237d, 0.74889243595964516d, 0.75537349553135413d, 0.76195333614981209d,
					0.76863730540551067d, 0.77543129488775442d, 0.78234182286676468d, 0.78937613408958662d,
					0.79654232126471991d, 0.80384947433796661d, 0.81130786581244374d, 0.81892918344439058d,
					0.82672682613658155d, 0.8347162855364465d, 0.84291564603138935d, 0.85134625175895529d,
					0.86003361489043029d, 0.8690086821390387d, 0.87830965033534147d, 0.88798465572506224d,
					0.898095917332063d, 0.908726435976224d, 0.91999150148558251d, 0.93206007296811533d,
					0.94519895106839869d, 0.95987909012352313d, 0.97710170042559519d, 1d,
				},
				-9223372036854775552L, 255L, 8);

			#endregion
		}

		public static class ExponentialFloat
		{
			public static float F(float x)
			{
				return Mathf.Exp(-x);
			}

			public static float SampleFallback(IRandom random, float xMin)
			{
				return Sample(random, zigguratTable) + xMin;
			}

			public static float Sample(IRandom random, OneSidedFloatZigguratTable ziggurat)
			{
				do
				{
					// Select a random segment, and a random n/x within the segment.
					uint n = random.Next32();
					int i = (int)(n & ziggurat.mask);
					n = n >> ziggurat.shift;
					var segment = ziggurat.segments[i];
					float x = n * segment.s;

					// Dominant quick check within fully-contained segment rectangle.
					if (n < segment.n) return x;

					// Rare case within tail.
					if (i == 0) return SampleFallback(random, (1 << (32 - ziggurat.shift)) * ziggurat.segments[1].s);

					// Slow check within the partially-contained segment rectangle.
					if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < F(x)) return x;
				} while (true);
			}

#if UNITY_EDITOR
			//[UnityEditor.Callbacks.DidReloadScripts] // Uncomment this attribute in order to generate and print tables in the Unity console pane.
			private static void GenerateZigguratLookupTable()
			{
				var table = GenerateOneSidedFloatZigguratTable(8,
					ExponentialDouble.F,
					ExponentialDouble.Inv,
					ExponentialDouble.CDF,
					ExponentialDouble.totalArea,
					0.0000000001d);

				Distributions.GenerateZigguratLookupTable(table, "exponential");
			}
#endif

			#region Lookup Table

			public static readonly OneSidedFloatZigguratTable zigguratTable = new OneSidedFloatZigguratTable(
				new FloatZigguratSegment[]
				{
					new FloatZigguratSegment(14848161U, 5.183886E-07f), new FloatZigguratSegment(15129198U, 4.58783944E-07f), new FloatZigguratSegment(15658929U, 4.13717856E-07f), new FloatZigguratSegment(15911694U, 3.86141437E-07f),
					new FloatZigguratSegment(16061744U, 3.66220746E-07f), new FloatZigguratSegment(16161893U, 3.50603131E-07f), new FloatZigguratSegment(16233847U, 3.37744353E-07f), new FloatZigguratSegment(16288240U, 3.26805747E-07f),
					new FloatZigguratSegment(16330913U, 3.17280922E-07f), new FloatZigguratSegment(16365357U, 3.088407E-07f), new FloatZigguratSegment(16393787U, 3.01259064E-07f), new FloatZigguratSegment(16417682U, 2.94374047E-07f),
					new FloatZigguratSegment(16438068U, 2.8806565E-07f), new FloatZigguratSegment(16455680U, 2.82242468E-07f), new FloatZigguratSegment(16471057U, 2.76833276E-07f), new FloatZigguratSegment(16484608U, 2.71781516E-07f),
					new FloatZigguratSegment(16496645U, 2.67041429E-07f), new FloatZigguratSegment(16507411U, 2.625756E-07f), new FloatZigguratSegment(16517102U, 2.58352969E-07f), new FloatZigguratSegment(16525871U, 2.54347469E-07f),
					new FloatZigguratSegment(16533847U, 2.50537028E-07f), new FloatZigguratSegment(16541132U, 2.46902744E-07f), new FloatZigguratSegment(16547814U, 2.43428417E-07f), new FloatZigguratSegment(16553965U, 2.40099951E-07f),
					new FloatZigguratSegment(16559645U, 2.36904981E-07f), new FloatZigguratSegment(16564906U, 2.3383275E-07f), new FloatZigguratSegment(16569794U, 2.30873681E-07f), new FloatZigguratSegment(16574345U, 2.28019317E-07f),
					new FloatZigguratSegment(16578593U, 2.252621E-07f), new FloatZigguratSegment(16582567U, 2.22595261E-07f), new FloatZigguratSegment(16586292U, 2.20012723E-07f), new FloatZigguratSegment(16589790U, 2.17509E-07f),
					new FloatZigguratSegment(16593081U, 2.15079112E-07f), new FloatZigguratSegment(16596181U, 2.12718561E-07f), new FloatZigguratSegment(16599107U, 2.10423224E-07f), new FloatZigguratSegment(16601871U, 2.08189348E-07f),
					new FloatZigguratSegment(16604487U, 2.060135E-07f), new FloatZigguratSegment(16606964U, 2.038925E-07f), new FloatZigguratSegment(16609314U, 2.0182344E-07f), new FloatZigguratSegment(16611545U, 1.99803651E-07f),
					new FloatZigguratSegment(16613665U, 1.97830644E-07f), new FloatZigguratSegment(16615681U, 1.95902118E-07f), new FloatZigguratSegment(16617601U, 1.94015939E-07f), new FloatZigguratSegment(16619430U, 1.9217012E-07f),
					new FloatZigguratSegment(16621174U, 1.90362812E-07f), new FloatZigguratSegment(16622837U, 1.88592281E-07f), new FloatZigguratSegment(16624426U, 1.86856923E-07f), new FloatZigguratSegment(16625943U, 1.85155216E-07f),
					new FloatZigguratSegment(16627394U, 1.83485753E-07f), new FloatZigguratSegment(16628780U, 1.81847213E-07f), new FloatZigguratSegment(16630107U, 1.80238331E-07f), new FloatZigguratSegment(16631377U, 1.78657942E-07f),
					new FloatZigguratSegment(16632593U, 1.77104937E-07f), new FloatZigguratSegment(16633757U, 1.75578265E-07f), new FloatZigguratSegment(16634873U, 1.74076931E-07f), new FloatZigguratSegment(16635942U, 1.72600011E-07f),
					new FloatZigguratSegment(16636967U, 1.71146638E-07f), new FloatZigguratSegment(16637949U, 1.69715932E-07f), new FloatZigguratSegment(16638891U, 1.68307139E-07f), new FloatZigguratSegment(16639795U, 1.66919492E-07f),
					new FloatZigguratSegment(16640661U, 1.65552265E-07f), new FloatZigguratSegment(16641491U, 1.64204792E-07f), new FloatZigguratSegment(16642288U, 1.628764E-07f), new FloatZigguratSegment(16643052U, 1.615665E-07f),
					new FloatZigguratSegment(16643784U, 1.60274482E-07f), new FloatZigguratSegment(16644486U, 1.589998E-07f), new FloatZigguratSegment(16645158U, 1.57741908E-07f), new FloatZigguratSegment(16645803U, 1.56500278E-07f),
					new FloatZigguratSegment(16646421U, 1.55274449E-07f), new FloatZigguratSegment(16647012U, 1.54063926E-07f), new FloatZigguratSegment(16647578U, 1.52868282E-07f), new FloatZigguratSegment(16648119U, 1.51687061E-07f),
					new FloatZigguratSegment(16648637U, 1.50519867E-07f), new FloatZigguratSegment(16649132U, 1.493663E-07f), new FloatZigguratSegment(16649604U, 1.48225979E-07f), new FloatZigguratSegment(16650055U, 1.47098547E-07f),
					new FloatZigguratSegment(16650485U, 1.45983634E-07f), new FloatZigguratSegment(16650894U, 1.44880914E-07f), new FloatZigguratSegment(16651284U, 1.43790061E-07f), new FloatZigguratSegment(16651654U, 1.42710746E-07f),
					new FloatZigguratSegment(16652005U, 1.416427E-07f), new FloatZigguratSegment(16652338U, 1.405856E-07f), new FloatZigguratSegment(16652654U, 1.395392E-07f), new FloatZigguratSegment(16652951U, 1.38503182E-07f),
					new FloatZigguratSegment(16653232U, 1.37477329E-07f), new FloatZigguratSegment(16653495U, 1.36461367E-07f), new FloatZigguratSegment(16653742U, 1.35455068E-07f), new FloatZigguratSegment(16653974U, 1.34458176E-07f),
					new FloatZigguratSegment(16654189U, 1.33470465E-07f), new FloatZigguratSegment(16654389U, 1.32491735E-07f), new FloatZigguratSegment(16654574U, 1.31521759E-07f), new FloatZigguratSegment(16654744U, 1.30560338E-07f),
					new FloatZigguratSegment(16654899U, 1.29607258E-07f), new FloatZigguratSegment(16655040U, 1.28662336E-07f), new FloatZigguratSegment(16655166U, 1.27725386E-07f), new FloatZigguratSegment(16655279U, 1.26796223E-07f),
					new FloatZigguratSegment(16655377U, 1.25874678E-07f), new FloatZigguratSegment(16655462U, 1.2496055E-07f), new FloatZigguratSegment(16655534U, 1.24053713E-07f), new FloatZigguratSegment(16655592U, 1.23153967E-07f),
					new FloatZigguratSegment(16655637U, 1.22261184E-07f), new FloatZigguratSegment(16655669U, 1.21375209E-07f), new FloatZigguratSegment(16655688U, 1.20495869E-07f), new FloatZigguratSegment(16655694U, 1.19623053E-07f),
					new FloatZigguratSegment(16655687U, 1.18756589E-07f), new FloatZigguratSegment(16655668U, 1.17896363E-07f), new FloatZigguratSegment(16655636U, 1.17042227E-07f), new FloatZigguratSegment(16655592U, 1.16194052E-07f),
					new FloatZigguratSegment(16655535U, 1.15351725E-07f), new FloatZigguratSegment(16655465U, 1.1451511E-07f), new FloatZigguratSegment(16655384U, 1.13684088E-07f), new FloatZigguratSegment(16655290U, 1.12858544E-07f),
					new FloatZigguratSegment(16655183U, 1.12038364E-07f), new FloatZigguratSegment(16655065U, 1.11223429E-07f), new FloatZigguratSegment(16654934U, 1.10413644E-07f), new FloatZigguratSegment(16654791U, 1.09608884E-07f),
					new FloatZigguratSegment(16654635U, 1.08809061E-07f), new FloatZigguratSegment(16654467U, 1.08014063E-07f), new FloatZigguratSegment(16654287U, 1.072238E-07f), new FloatZigguratSegment(16654095U, 1.06438158E-07f),
					new FloatZigguratSegment(16653890U, 1.05657051E-07f), new FloatZigguratSegment(16653672U, 1.04880385E-07f), new FloatZigguratSegment(16653442U, 1.04108075E-07f), new FloatZigguratSegment(16653199U, 1.03340021E-07f),
					new FloatZigguratSegment(16652944U, 1.02576138E-07f), new FloatZigguratSegment(16652676U, 1.0181634E-07f), new FloatZigguratSegment(16652395U, 1.01060543E-07f), new FloatZigguratSegment(16652101U, 1.00308661E-07f),
					new FloatZigguratSegment(16651793U, 9.95606158E-08f), new FloatZigguratSegment(16651473U, 9.881633E-08f), new FloatZigguratSegment(16651139U, 9.80757164E-08f), new FloatZigguratSegment(16650792U, 9.73387E-08f),
					new FloatZigguratSegment(16650431U, 9.660521E-08f), new FloatZigguratSegment(16650056U, 9.587517E-08f), new FloatZigguratSegment(16649667U, 9.514851E-08f), new FloatZigguratSegment(16649264U, 9.442515E-08f),
					new FloatZigguratSegment(16648847U, 9.370501E-08f), new FloatZigguratSegment(16648415U, 9.29880457E-08f), new FloatZigguratSegment(16647969U, 9.227416E-08f), new FloatZigguratSegment(16647507U, 9.156331E-08f),
					new FloatZigguratSegment(16647030U, 9.08554156E-08f), new FloatZigguratSegment(16646538U, 9.015041E-08f), new FloatZigguratSegment(16646029U, 8.944822E-08f), new FloatZigguratSegment(16645505U, 8.87488E-08f),
					new FloatZigguratSegment(16644964U, 8.80520759E-08f), new FloatZigguratSegment(16644407U, 8.735798E-08f), new FloatZigguratSegment(16643833U, 8.66664536E-08f), new FloatZigguratSegment(16643242U, 8.597744E-08f),
					new FloatZigguratSegment(16642632U, 8.52908641E-08f), new FloatZigguratSegment(16642005U, 8.460668E-08f), new FloatZigguratSegment(16641360U, 8.39248244E-08f), new FloatZigguratSegment(16640695U, 8.324523E-08f),
					new FloatZigguratSegment(16640012U, 8.25678441E-08f), new FloatZigguratSegment(16639309U, 8.189261E-08f), new FloatZigguratSegment(16638585U, 8.121946E-08f), new FloatZigguratSegment(16637841U, 8.054834E-08f),
					new FloatZigguratSegment(16637076U, 7.98792E-08f), new FloatZigguratSegment(16636290U, 7.921197E-08f), new FloatZigguratSegment(16635481U, 7.85466057E-08f), new FloatZigguratSegment(16634649U, 7.7883044E-08f),
					new FloatZigguratSegment(16633795U, 7.722122E-08f), new FloatZigguratSegment(16632916U, 7.656109E-08f), new FloatZigguratSegment(16632012U, 7.59026E-08f), new FloatZigguratSegment(16631083U, 7.524567E-08f),
					new FloatZigguratSegment(16630128U, 7.45902753E-08f), new FloatZigguratSegment(16629147U, 7.39363344E-08f), new FloatZigguratSegment(16628137U, 7.32838E-08f), new FloatZigguratSegment(16627100U, 7.263262E-08f),
					new FloatZigguratSegment(16626033U, 7.198273E-08f), new FloatZigguratSegment(16624936U, 7.13340853E-08f), new FloatZigguratSegment(16623807U, 7.068662E-08f), new FloatZigguratSegment(16622647U, 7.00402651E-08f),
					new FloatZigguratSegment(16621453U, 6.93949858E-08f), new FloatZigguratSegment(16620225U, 6.875071E-08f), new FloatZigguratSegment(16618961U, 6.810738E-08f), new FloatZigguratSegment(16617660U, 6.74649456E-08f),
					new FloatZigguratSegment(16616322U, 6.682334E-08f), new FloatZigguratSegment(16614944U, 6.61825E-08f), new FloatZigguratSegment(16613526U, 6.554237E-08f), new FloatZigguratSegment(16612065U, 6.49029E-08f),
					new FloatZigguratSegment(16610560U, 6.426401E-08f), new FloatZigguratSegment(16609009U, 6.362565E-08f), new FloatZigguratSegment(16607412U, 6.29877448E-08f), new FloatZigguratSegment(16605765U, 6.235024E-08f),
					new FloatZigguratSegment(16604066U, 6.171307E-08f), new FloatZigguratSegment(16602315U, 6.107616E-08f), new FloatZigguratSegment(16600508U, 6.043945E-08f), new FloatZigguratSegment(16598643U, 5.98028649E-08f),
					new FloatZigguratSegment(16596718U, 5.91663358E-08f), new FloatZigguratSegment(16594730U, 5.85297961E-08f), new FloatZigguratSegment(16592677U, 5.789317E-08f), new FloatZigguratSegment(16590554U, 5.72563827E-08f),
					new FloatZigguratSegment(16588360U, 5.66193563E-08f), new FloatZigguratSegment(16586091U, 5.59820137E-08f), new FloatZigguratSegment(16583743U, 5.534427E-08f), new FloatZigguratSegment(16581312U, 5.470605E-08f),
					new FloatZigguratSegment(16578795U, 5.40672573E-08f), new FloatZigguratSegment(16576186U, 5.34278151E-08f), new FloatZigguratSegment(16573482U, 5.278763E-08f), new FloatZigguratSegment(16570677U, 5.2146607E-08f),
					new FloatZigguratSegment(16567767U, 5.150465E-08f), new FloatZigguratSegment(16564744U, 5.0861658E-08f), new FloatZigguratSegment(16561603U, 5.021753E-08f), new FloatZigguratSegment(16558338U, 4.957216E-08f),
					new FloatZigguratSegment(16554941U, 4.89254361E-08f), new FloatZigguratSegment(16551404U, 4.82772435E-08f), new FloatZigguratSegment(16547720U, 4.762746E-08f), new FloatZigguratSegment(16543878U, 4.69759627E-08f),
					new FloatZigguratSegment(16539869U, 4.63226222E-08f), new FloatZigguratSegment(16535683U, 4.56672957E-08f), new FloatZigguratSegment(16531308U, 4.50098518E-08f), new FloatZigguratSegment(16526731U, 4.435013E-08f),
					new FloatZigguratSegment(16521937U, 4.368798E-08f), new FloatZigguratSegment(16516913U, 4.30232348E-08f), new FloatZigguratSegment(16511642U, 4.235572E-08f), new FloatZigguratSegment(16506104U, 4.168525E-08f),
					new FloatZigguratSegment(16500280U, 4.101164E-08f), new FloatZigguratSegment(16494148U, 4.0334676E-08f), new FloatZigguratSegment(16487683U, 3.96541431E-08f), new FloatZigguratSegment(16480857U, 3.89698123E-08f),
					new FloatZigguratSegment(16473641U, 3.828144E-08f), new FloatZigguratSegment(16465999U, 3.75887552E-08f), new FloatZigguratSegment(16457894U, 3.689149E-08f), new FloatZigguratSegment(16449284U, 3.618933E-08f),
					new FloatZigguratSegment(16440118U, 3.5481964E-08f), new FloatZigguratSegment(16430344U, 3.476904E-08f), new FloatZigguratSegment(16419898U, 3.40501849E-08f), new FloatZigguratSegment(16408709U, 3.332499E-08f),
					new FloatZigguratSegment(16396697U, 3.25930181E-08f), new FloatZigguratSegment(16383767U, 3.18537872E-08f), new FloatZigguratSegment(16369810U, 3.11067723E-08f), new FloatZigguratSegment(16354700U, 3.03513978E-08f),
					new FloatZigguratSegment(16338288U, 2.95870333E-08f), new FloatZigguratSegment(16320400U, 2.88129733E-08f), new FloatZigguratSegment(16300827U, 2.80284453E-08f), new FloatZigguratSegment(16279320U, 2.72325771E-08f),
					new FloatZigguratSegment(16255579U, 2.64244E-08f), new FloatZigguratSegment(16229238U, 2.56028141E-08f), new FloatZigguratSegment(16199845U, 2.47665746E-08f), new FloatZigguratSegment(16166839U, 2.3914259E-08f),
					new FloatZigguratSegment(16129512U, 2.30442279E-08f), new FloatZigguratSegment(16086957U, 2.21545786E-08f), new FloatZigguratSegment(16037997U, 2.1243082E-08f), new FloatZigguratSegment(15981072U, 2.03070929E-08f),
					new FloatZigguratSegment(15914072U, 1.93434424E-08f), new FloatZigguratSegment(15834075U, 1.8348274E-08f), new FloatZigguratSegment(15736910U, 1.73168164E-08f), new FloatZigguratSegment(15616422U, 1.62430513E-08f),
					new FloatZigguratSegment(15463134U, 1.51192161E-08f), new FloatZigguratSegment(15261681U, 1.39349989E-08f), new FloatZigguratSegment(14985448U, 1.267621E-08f), new FloatZigguratSegment(14584127U, 1.132242E-08f),
					new FloatZigguratSegment(13950393U, 9.842373E-09f), new FloatZigguratSegment(12810156U, 8.184014E-09f), new FloatZigguratSegment(10218206U, 6.248862E-09f), new FloatZigguratSegment(0U, 3.80588538E-09f),
				},
				new float[]
				{
					0.000454134366f, 0.0009672693f, 0.00153629982f, 0.00214596768f,
					0.00278879888f, 0.00346026476f, 0.004157295f, 0.00487765577f,
					0.00561964232f, 0.006381906f, 0.00716335326f, 0.007963077f,
					0.008780315f, 0.009614414f, 0.01046481f, 0.0113310134f,
					0.0122125922f, 0.0131091652f, 0.0140203917f, 0.0149459681f,
					0.0158856213f, 0.0168391075f, 0.0178062f, 0.0187867f,
					0.0197804235f, 0.0207872037f, 0.0218068883f, 0.0228393357f,
					0.0238844212f, 0.0249420255f, 0.0260120463f, 0.0270943847f,
					0.02818895f, 0.02929566f, 0.0304144435f, 0.031545233f,
					0.0326879621f, 0.0338425823f, 0.0350090377f, 0.0361872837f,
					0.037377283f, 0.0385789946f, 0.0397923924f, 0.0410174429f,
					0.0422541238f, 0.0435024127f, 0.0447623f, 0.0460337624f,
					0.0473167934f, 0.0486113839f, 0.049917534f, 0.0512352362f,
					0.0525644943f, 0.053905312f, 0.05525769f, 0.05662164f,
					0.0579971746f, 0.059384305f, 0.0607830472f, 0.0621934161f,
					0.0636154339f, 0.06504912f, 0.0664944947f, 0.06795159f,
					0.0694204345f, 0.07090106f, 0.0723934844f, 0.07389775f,
					0.07541389f, 0.0769419447f, 0.07848195f, 0.08003395f,
					0.0815979838f, 0.0831740946f, 0.08476233f, 0.08636274f,
					0.0879753754f, 0.08960028f, 0.0912375152f, 0.09288713f,
					0.09454919f, 0.09622374f, 0.09791085f, 0.09961058f,
					0.101323f, 0.103048161f, 0.104786143f, 0.106537007f,
					0.108300827f, 0.110077679f, 0.111867629f, 0.113670766f,
					0.115487166f, 0.1173169f, 0.119160056f, 0.121016718f,
					0.122886978f, 0.124770917f, 0.126668632f, 0.1285802f,
					0.130505741f, 0.13244532f, 0.134399071f, 0.136367068f,
					0.138349429f, 0.140346244f, 0.142357647f, 0.144383729f,
					0.146424592f, 0.148480371f, 0.150551185f, 0.152637139f,
					0.154738367f, 0.156854987f, 0.158987135f, 0.161134943f,
					0.163298532f, 0.165478036f, 0.167673618f, 0.1698854f,
					0.172113538f, 0.174358174f, 0.176619455f, 0.178897545f,
					0.1811926f, 0.18350479f, 0.185834259f, 0.1881812f,
					0.190545768f, 0.19292815f, 0.195328519f, 0.197747067f,
					0.200183973f, 0.202639446f, 0.205113649f, 0.207606822f,
					0.210119158f, 0.212650865f, 0.215202153f, 0.217773244f,
					0.220364377f, 0.222975761f, 0.225607663f, 0.2282603f,
					0.23093392f, 0.23362878f, 0.236345157f, 0.23908329f,
					0.241843462f, 0.244625971f, 0.24743107f, 0.250259072f,
					0.2531103f, 0.255985022f, 0.258883536f, 0.26180625f,
					0.264753431f, 0.2677254f, 0.2707226f, 0.2737453f,
					0.276793927f, 0.279868841f, 0.282970428f, 0.286099076f,
					0.289255232f, 0.292439282f, 0.2956517f, 0.298892915f,
					0.3021634f, 0.3054636f, 0.308794081f, 0.312155247f,
					0.315547675f, 0.3189719f, 0.3224285f, 0.325917959f,
					0.329440951f, 0.332998067f, 0.3365899f, 0.340217143f,
					0.343880445f, 0.3475805f, 0.351318f, 0.355093747f,
					0.358908474f, 0.362763f, 0.3666581f, 0.370594651f,
					0.374573559f, 0.378595769f, 0.382662177f, 0.386773825f,
					0.390931726f, 0.395136982f, 0.3993907f, 0.403694f,
					0.408048183f, 0.412454456f, 0.4169142f, 0.42142874f,
					0.425999552f, 0.430628151f, 0.435316116f, 0.4400651f,
					0.444876879f, 0.449753255f, 0.454696149f, 0.459707618f,
					0.464789748f, 0.469944835f, 0.4751752f, 0.480483353f,
					0.485872f, 0.491343856f, 0.496902f, 0.5025495f,
					0.508289754f, 0.5141264f, 0.520063162f, 0.5261042f,
					0.532253861f, 0.5385169f, 0.5448982f, 0.5514034f,
					0.5580383f, 0.5648092f, 0.571723044f, 0.5787874f,
					0.586010337f, 0.5934009f, 0.600968957f, 0.608725369f,
					0.6166822f, 0.6248527f, 0.633251965f, 0.6418967f,
					0.650805831f, 0.660000861f, 0.6695063f, 0.679350555f,
					0.6895665f, 0.70019263f, 0.711274743f, 0.722867668f,
					0.7350381f, 0.7478686f, 0.7614634f, 0.775956869f,
					0.7915276f, 0.8084217f, 0.8269933f, 0.8477855f,
					0.87170434f, 0.900469959f, 0.9381437f, 1f,
				},
				255U, 8);

			#endregion
		}

		public static class ExponentialDouble
		{
			public const double totalArea = 1d;

			public static double F(double x)
			{
				return Math.Exp(-x);
			}

			public static double Inv(double y)
			{
				return -Math.Log(y);
			}

			public static double CDF(double x)
			{
				return 1f - Math.Exp(-x);
			}

			public static double SampleFallback(IRandom random, double xMin)
			{
				return Sample(random, zigguratTable) + xMin;
			}

			public static double Sample(IRandom random, OneSidedDoubleZigguratTable ziggurat)
			{
				do
				{
					// Select a random segment, and a random n/x within the segment.
					ulong n = random.Next64();
					int i = (int)(n & ziggurat.mask);
					n = n >> ziggurat.shift;
					var segment = ziggurat.segments[i];
					double x = n * segment.s;

					// Dominant quick check within fully-contained segment rectangle.
					if (n < segment.n) return x;

					// Rare case within tail.
					if (i == 0) return SampleFallback(random, (1L << (64 - ziggurat.shift)) * ziggurat.segments[1].s);

					// Slow check within the partially-contained segment rectangle.
					if (random.RangeCO(ziggurat.segmentUpperBounds[i - 1], ziggurat.segmentUpperBounds[i]) < F(x)) return x;
				} while (true);
			}

#if UNITY_EDITOR
			//[UnityEditor.Callbacks.DidReloadScripts] // Uncomment this attribute in order to generate and print tables in the Unity console pane.
			private static void GenerateZigguratLookupTable()
			{
				var table = GenerateOneSidedDoubleZigguratTable(8,
					ExponentialDouble.F,
					ExponentialDouble.Inv,
					ExponentialDouble.CDF,
					ExponentialDouble.totalArea,
					0.0000000001d);

				Distributions.GenerateZigguratLookupTable(table, "exponential");
			}
#endif

			#region Lookup Table

			public static readonly OneSidedDoubleZigguratTable zigguratTable = new OneSidedDoubleZigguratTable(
				new DoubleZigguratSegment[]
				{
					new DoubleZigguratSegment(63772366859522280UL, 1.20696750791125E-16d), new DoubleZigguratSegment(64979414100238672UL, 1.0681896298331217E-16d), new DoubleZigguratSegment(67254589512039624UL, 9.6326191876421157E-17d), new DoubleZigguratSegment(68340206366486832UL, 8.9905562077145374E-17d),
					new DoubleZigguratSegment(68984669232829704UL, 8.526741348889737E-17d), new DoubleZigguratSegment(69414802082054456UL, 8.1631150670592829E-17d), new DoubleZigguratSegment(69723845488995376UL, 7.8637237937017711E-17d), new DoubleZigguratSegment(69957458710799600UL, 7.609039270331503E-17d),
					new DoubleZigguratSegment(70140739975257512UL, 7.3872720521710013E-17d), new DoubleZigguratSegment(70288673577583824UL, 7.19075810198551E-17d), new DoubleZigguratSegment(70410779876389352UL, 7.0142343184507428E-17d), new DoubleZigguratSegment(70513409253243560UL, 6.8539300429306E-17d),
					new DoubleZigguratSegment(70600966658820592UL, 6.7070512214976535E-17d), new DoubleZigguratSegment(70676607522012184UL, 6.5714697526358633E-17d), new DoubleZigguratSegment(70742653925986240UL, 6.4455272862060018E-17d), new DoubleZigguratSegment(70800854274376992UL, 6.3279063402889638E-17d),
					new DoubleZigguratSegment(70852551217625272UL, 6.2175427953490451E-17d), new DoubleZigguratSegment(70898793638242776UL, 6.1135647843497291E-17d), new DoubleZigguratSegment(70940413345692672UL, 6.0152489661463532E-17d), new DoubleZigguratSegment(70978078840928856UL, 5.9219885666882523E-17d),
					new DoubleZigguratSegment(71012333790235880UL, 5.8332695809998062E-17d), new DoubleZigguratSegment(71043625065941864UL, 5.7486527562431951E-17d), new DoubleZigguratSegment(71072323521182632UL, 5.6677597483183857E-17d), new DoubleZigguratSegment(71098739610610056UL, 5.5902623429363161E-17d),
					new DoubleZigguratSegment(71123135293969672UL, 5.5158739614067925E-17d), new DoubleZigguratSegment(71145733218227816UL, 5.4443428934794447E-17d), new DoubleZigguratSegment(71166723879712304UL, 5.3754468521966395E-17d), new DoubleZigguratSegment(71186271267988856UL, 5.3089885523930752E-17d),
					new DoubleZigguratSegment(71204517355341680UL, 5.2447920902046322E-17d), new DoubleZigguratSegment(71221585699139440UL, 5.1826999554768008E-17d), new DoubleZigguratSegment(71237584355744536UL, 5.1225705487422824E-17d), new DoubleZigguratSegment(71252608255239720UL, 5.0642761038094523E-17d),
					new DoubleZigguratSegment(71266741150279312UL, 5.0077009389346928E-17d), new DoubleZigguratSegment(71280057225887664UL, 4.9527399760977617E-17d), new DoubleZigguratSegment(71292622437321400UL, 4.8992974805038552E-17d), new DoubleZigguratSegment(71304495628308168UL, 4.8472859821247125E-17d),
					new DoubleZigguratSegment(71315729470752208UL, 4.7966253486016329E-17d), new DoubleZigguratSegment(71326371258417424UL, 4.74724198470148E-17d), new DoubleZigguratSegment(71336463580487072UL, 4.6990681381359775E-17d), new DoubleZigguratSegment(71346044895765176UL, 4.6520412952161853E-17d),
					new DoubleZigguratSegment(71355150024270752UL, 4.6061036527357203E-17d), new DoubleZigguratSegment(71363810569815488UL, 4.5612016548221393E-17d), new DoubleZigguratSegment(71372055284652128UL, 4.5172855853905525E-17d), new DoubleZigguratSegment(71379910385285088UL, 4.4743092083723381E-17d),
					new DoubleZigguratSegment(71387399826935872UL, 4.4322294491482472E-17d), new DoubleZigguratSegment(71394545542866120UL, 4.3910061116462461E-17d), new DoubleZigguratSegment(71401367653717376UL, 4.3506016264145878E-17d), new DoubleZigguratSegment(71407884651176136UL, 4.3109808256848286E-17d),
					new DoubleZigguratSegment(71414113559577480UL, 4.2721107420253575E-17d), new DoubleZigguratSegment(71420070078489120UL, 4.2339604276754553E-17d), new DoubleZigguratSegment(71425768708846112UL, 4.1965007920603921E-17d), new DoubleZigguratSegment(71431222864816272UL, 4.1597044553336888E-17d),
					new DoubleZigguratSegment(71436444973250568UL, 4.123545616084716E-17d), new DoubleZigguratSegment(71441446562302288UL, 4.0879999315974251E-17d), new DoubleZigguratSegment(71446238340570696UL, 4.0530444092567017E-17d), new DoubleZigguratSegment(71450830267934128UL, 4.0186573078786464E-17d),
					new DoubleZigguratSegment(71455231619075776UL, 3.9848180478950536E-17d), new DoubleZigguratSegment(71459451040569208UL, 3.9515071294545575E-17d), new DoubleZigguratSegment(71463496602274232UL, 3.9187060576167681E-17d), new DoubleZigguratSegment(71467375843695584UL, 3.8863972739140352E-17d),
					new DoubleZigguratSegment(71471095815871664UL, 3.8545640936406042E-17d), new DoubleZigguratSegment(71474663119289416UL, 3.8231906483028066E-17d), new DoubleZigguratSegment(71478083938258224UL, 3.79226183272825E-17d), new DoubleZigguratSegment(71481364072123376UL, 3.7617632563880344E-17d),
					new DoubleZigguratSegment(71484508963652568UL, 3.7316811985350846E-17d), new DoubleZigguratSegment(71487523724889744UL, 3.7020025668046269E-17d), new DoubleZigguratSegment(71490413160735560UL, 3.6727148589605975E-17d), new DoubleZigguratSegment(71493181790483760UL, 3.6438061275049469E-17d),
					new DoubleZigguratSegment(71495833867516728UL, 3.6152649468960936E-17d), new DoubleZigguratSegment(71498373397340208UL, 3.5870803831486411E-17d), new DoubleZigguratSegment(71500804154117744UL, 3.5592419656093572E-17d), new DoubleZigguratSegment(71503129695847080UL, 3.5317396607247272E-17d),
					new DoubleZigguratSegment(71505353378306168UL, 3.5045638476334119E-17d), new DoubleZigguratSegment(71507478367882224UL, 3.4777052954330013E-17d), new DoubleZigguratSegment(71509507653385736UL, 3.4511551419847518E-17d), new DoubleZigguratSegment(71511444056940432UL, 3.4249048741327744E-17d),
					new DoubleZigguratSegment(71513290244031144UL, 3.3989463092255551E-17d), new DoubleZigguratSegment(71515048732782992UL, 3.3732715778379211E-17d), new DoubleZigguratSegment(71516721902538448UL, 3.347873107600745E-17d), new DoubleZigguratSegment(71518312001791240UL, 3.3227436080539247E-17d),
					new DoubleZigguratSegment(71519821155531784UL, 3.2978760564455773E-17d), new DoubleZigguratSegment(71521251372051992UL, 3.2732636844070869E-17d), new DoubleZigguratSegment(71522604549254016UL, 3.2488999654396488E-17d), new DoubleZigguratSegment(71523882480502568UL, 3.2247786031534141E-17d),
					new DoubleZigguratSegment(71525086860056864UL, 3.2008935202052505E-17d), new DoubleZigguratSegment(71526219288115176UL, 3.17723884788559E-17d), new DoubleZigguratSegment(71527281275501592UL, 3.1538089163088858E-17d), new DoubleZigguratSegment(71528274248022352UL, 3.1305982451658517E-17d),
					new DoubleZigguratSegment(71529199550516096UL, 3.1076015349990128E-17d), new DoubleZigguratSegment(71530058450620744UL, 3.0848136589661031E-17d), new DoubleZigguratSegment(71530852142277376UL, 3.0622296550586263E-17d), new DoubleZigguratSegment(71531581748990056UL, 3.0398447187454056E-17d),
					new DoubleZigguratSegment(71532248326858184UL, 3.0176541960132511E-17d), new DoubleZigguratSegment(71532852867397528UL, 2.9956535767789653E-17d), new DoubleZigguratSegment(71533396300163880UL, 2.9738384886488344E-17d), new DoubleZigguratSegment(71533879495192424UL, 2.9522046910035111E-17d),
					new DoubleZigguratSegment(71534303265264960UL, 2.9307480693877972E-17d), new DoubleZigguratSegment(71534668368015544UL, 2.9094646301863269E-17d), new DoubleZigguratSegment(71534975507884952UL, 2.8883504955674859E-17d), new DoubleZigguratSegment(71535225337932888UL, 2.8674018986791709E-17d),
					new DoubleZigguratSegment(71535418461516336UL, 2.8466151790811229E-17d), new DoubleZigguratSegment(71535555433841896UL, 2.8259867783996306E-17d), new DoubleZigguratSegment(71535636763398888UL, 2.8055132361913684E-17d), new DoubleZigguratSegment(71535662913279912UL, 2.78519118600403E-17d),
					new DoubleZigguratSegment(71535634302394504UL, 2.7650173516222479E-17d), new DoubleZigguratSegment(71535551306581368UL, 2.7449885434880427E-17d), new DoubleZigguratSegment(71535414259624120UL, 2.7251016552857628E-17d), new DoubleZigguratSegment(71535223454174752UL, 2.70535366068212E-17d),
					new DoubleZigguratSegment(71534979142589192UL, 2.6857416102125263E-17d), new DoubleZigguratSegment(71534681537678208UL, 2.6662626283055072E-17d), new DoubleZigguratSegment(71534330813377344UL, 2.6469139104374668E-17d), new DoubleZigguratSegment(71533927105338544UL, 2.6276927204105794E-17d),
					new DoubleZigguratSegment(71533470511446256UL, 2.6085963877470075E-17d), new DoubleZigguratSegment(71532961092260304UL, 2.5896223051930759E-17d), new DoubleZigguratSegment(71532398871387568UL, 2.5707679263274017E-17d), new DoubleZigguratSegment(71531783835784296UL, 2.5520307632673431E-17d),
					new DoubleZigguratSegment(71531115935990608UL, 2.5334083844684606E-17d), new DoubleZigguratSegment(71530395086298368UL, 2.5148984126119895E-17d), new DoubleZigguratSegment(71529621164853544UL, 2.4964985225756105E-17d), new DoubleZigguratSegment(71528794013693928UL, 2.4782064394830715E-17d),
					new DoubleZigguratSegment(71527913438722624UL, 2.4600199368284624E-17d), new DoubleZigguratSegment(71526979209617888UL, 2.4419368346711734E-17d), new DoubleZigguratSegment(71525991059679344UL, 2.423954997897786E-17d), new DoubleZigguratSegment(71524948685610536UL, 2.4060723345473449E-17d),
					new DoubleZigguratSegment(71523851747237608UL, 2.3882867941966423E-17d), new DoubleZigguratSegment(71522699867163496UL, 2.370596366402328E-17d), new DoubleZigguratSegment(71521492630357192UL, 2.352999079196811E-17d), new DoubleZigguratSegment(71520229583676768UL, 2.33549299763508E-17d),
					new DoubleZigguratSegment(71518910235325544UL, 2.3180762223896976E-17d), new DoubleZigguratSegment(71517534054239544UL, 2.3007468883913711E-17d), new DoubleZigguratSegment(71516100469405040UL, 2.2835031635126063E-17d), new DoubleZigguratSegment(71514608869104128UL, 2.2663432472920768E-17d),
					new DoubleZigguratSegment(71513058600086448UL, 2.2492653696974424E-17d), new DoubleZigguratSegment(71511448966664608UL, 2.2322677899244458E-17d), new DoubleZigguratSegment(71509779229730824UL, 2.2153487952302097E-17d), new DoubleZigguratSegment(71508048605691832UL, 2.198506699798742E-17d),
					new DoubleZigguratSegment(71506256265319024UL, 2.181739843636726E-17d), new DoubleZigguratSegment(71504401332510368UL, 2.1650465914977544E-17d), new DoubleZigguratSegment(71502482882960120UL, 2.1484253318332238E-17d), new DoubleZigguratSegment(71500499942732576UL, 2.1318744757681668E-17d),
					new DoubleZigguratSegment(71498451486735056UL, 2.1153924561003576E-17d), new DoubleZigguratSegment(71496336437085424UL, 2.098977726321071E-17d), new DoubleZigguratSegment(71494153661368752UL, 2.0826287596559232E-17d), new DoubleZigguratSegment(71491901970777576UL, 2.0663440481242576E-17d),
					new DoubleZigguratSegment(71489580118129312UL, 2.0501221016155804E-17d), new DoubleZigguratSegment(71487186795754240UL, 2.0339614469815731E-17d), new DoubleZigguratSegment(71484720633246856UL, 2.0178606271422415E-17d), new DoubleZigguratSegment(71482180195072584UL, 2.0018182002047778E-17d),
					new DoubleZigguratSegment(71479563978021464UL, 1.985832738593732E-17d), new DoubleZigguratSegment(71476870408499648UL, 1.969902828191098E-17d), new DoubleZigguratSegment(71474097839648632UL, 1.9540270674849322E-17d), new DoubleZigguratSegment(71471244548281720UL, 1.9382040667251208E-17d),
					new DoubleZigguratSegment(71468308731626032UL, 1.922432447084916E-17d), new DoubleZigguratSegment(71465288503857344UL, 1.9067108398268543E-17d), new DoubleZigguratSegment(71462181892414368UL, 1.8910378854716563E-17d), new DoubleZigguratSegment(71458986834077944UL, 1.8754122329686949E-17d),
					new DoubleZigguratSegment(71455701170798680UL, 1.8598325388666054E-17d), new DoubleZigguratSegment(71452322645256392UL, 1.8442974664825692E-17d), new DoubleZigguratSegment(71448848896132328UL, 1.8288056850687888E-17d), new DoubleZigguratSegment(71445277453073952UL, 1.8133558689746263E-17d),
					new DoubleZigguratSegment(71441605731330320UL, 1.797946696802835E-17d), new DoubleZigguratSegment(71437831026034192UL, 1.7825768505582683E-17d), new DoubleZigguratSegment(71433950506104784UL, 1.7672450147873911E-17d), new DoubleZigguratSegment(71429961207743080UL, 1.7519498757068557E-17d),
					new DoubleZigguratSegment(71425860027488696UL, 1.7366901203193372E-17d), new DoubleZigguratSegment(71421643714805160UL, 1.7214644355147341E-17d), new DoubleZigguratSegment(71417308864156744UL, 1.7062715071547652E-17d), new DoubleZigguratSegment(71412851906537296UL, 1.6911100191388819E-17d),
					new DoubleZigguratSegment(71408269100407136UL, 1.6759786524493173E-17d), new DoubleZigguratSegment(71403556521991136UL, 1.66087608417296E-17d), new DoubleZigguratSegment(71398710054885048UL, 1.645800986497626E-17d), new DoubleZigguratSegment(71393725378913792UL, 1.6307520256801348E-17d),
					new DoubleZigguratSegment(71388597958178944UL, 1.6157278609834474E-17d), new DoubleZigguratSegment(71383323028226568UL, 1.6007271435799465E-17d), new DoubleZigguratSegment(71377895582260496UL, 1.5857485154177305E-17d), new DoubleZigguratSegment(71372310356317792UL, 1.5707906080465929E-17d),
					new DoubleZigguratSegment(71366561813315232UL, 1.5558520414001098E-17d), new DoubleZigguratSegment(71360644125866232UL, 1.5409314225300011E-17d), new DoubleZigguratSegment(71354551157757176UL, 1.5260273442886417E-17d), new DoubleZigguratSegment(71348276443960256UL, 1.5111383839552822E-17d),
					new DoubleZigguratSegment(71341813169047312UL, 1.4962631018011879E-17d), new DoubleZigguratSegment(71335154143854104UL, 1.4814000395885147E-17d), new DoubleZigguratSegment(71328291780228152UL, 1.4665477189973224E-17d), new DoubleZigguratSegment(71321218063675016UL, 1.4517046399746417E-17d),
					new DoubleZigguratSegment(71313924523696616UL, 1.4368692789990012E-17d), new DoubleZigguratSegment(71306402201592224UL, 1.4220400872532339E-17d), new DoubleZigguratSegment(71298641615465872UL, 1.4072154886977439E-17d), new DoubleZigguratSegment(71290632722153896UL, 1.3923938780357068E-17d),
					new DoubleZigguratSegment(71282364875752632UL, 1.3775736185608778E-17d), new DoubleZigguratSegment(71273826782387200UL, 1.3627530398778089E-17d), new DoubleZigguratSegment(71265006450818544UL, 1.3479304354832931E-17d), new DoubleZigguratSegment(71255891138435608UL, 1.3331040601967594E-17d),
					new DoubleZigguratSegment(71246467292122376UL, 1.3182721274261178E-17d), new DoubleZigguratSegment(71236720483423656UL, 1.3034328062541884E-17d), new DoubleZigguratSegment(71226635337358696UL, 1.28858421832931E-17d), new DoubleZigguratSegment(71216195454144664UL, 1.2737244345420058E-17d),
					new DoubleZigguratSegment(71205383322992928UL, 1.2588514714676393E-17d), new DoubleZigguratSegment(71194180227025976UL, 1.243963287552815E-17d), new DoubleZigguratSegment(71182566138229392UL, 1.2290577790208097E-17d), new DoubleZigguratSegment(71170519601199848UL, 1.2141327754685309E-17d),
					new DoubleZigguratSegment(71158017604269208UL, 1.1991860351243424E-17d), new DoubleZigguratSegment(71145035436377184UL, 1.1842152397324965E-17d), new DoubleZigguratSegment(71131546527819136UL, 1.1692179890258388E-17d), new DoubleZigguratSegment(71117522272709312UL, 1.1541917947437776E-17d),
					new DoubleZigguratSegment(71102931830663096UL, 1.1391340741471876E-17d), new DoubleZigguratSegment(71087741904803656UL, 1.1240421429758035E-17d), new DoubleZigguratSegment(71071916492728992UL, 1.1089132077866427E-17d), new DoubleZigguratSegment(71055416606517360UL, 1.0937443576039054E-17d),
					new DoubleZigguratSegment(71038199957185192UL, 1.0785325548014646E-17d), new DoubleZigguratSegment(71020220598218552UL, 1.0632746251282332E-17d), new DoubleZigguratSegment(71001428521848824UL, 1.0479672467741293E-17d), new DoubleZigguratSegment(70981769200598696UL, 1.0326069383597154E-17d),
					new DoubleZigguratSegment(70961183065243192UL, 1.0171900457154626E-17d), new DoubleZigguratSegment(70939604908654200UL, 1.0017127272965171E-17d), new DoubleZigguratSegment(70916963202955104UL, 9.8617093805521682E-18d), new DoubleZigguratSegment(70893179314914896UL, 9.7056041156570079E-18d),
					new DoubleZigguratSegment(70868166601440784UL, 9.5487664016187281E-18d), new DoubleZigguratSegment(70841829363236496UL, 9.3911485281061336E-18d), new DoubleZigguratSegment(70814061629987016UL, 9.2326999039508186E-18d), new DoubleZigguratSegment(70784745744555392UL, 9.0733667802648861E-18d),
					new DoubleZigguratSegment(70753750706306280UL, 8.9130919393463231E-18d), new DoubleZigguratSegment(70720930224362304UL, 8.7518143440503925E-18d), new DoubleZigguratSegment(70686120419778504UL, 8.5894687413013E-18d), new DoubleZigguratSegment(70649137100503544UL, 8.4259852121896653E-18d),
					new DoubleZigguratSegment(70609772513534400UL, 8.2612886595890151E-18d), new DoubleZigguratSegment(70567791453432432UL, 8.095298222352302E-18d), new DoubleZigguratSegment(70522926573379376UL, 7.92792660281732E-18d), new DoubleZigguratSegment(70474872701480272UL, 7.7590792914254253E-18d),
					new DoubleZigguratSegment(70423279907230520UL, 7.5886536685651286E-18d), new DoubleZigguratSegment(70367744985522208UL, 7.4165379590539734E-18d), new DoubleZigguratSegment(70307800920475736UL, 7.242610008647501E-18d), new DoubleZigguratSegment(70242903747443136UL, 7.066735844172189E-18d),
					new DoubleZigguratSegment(70172416032100376UL, 6.8887679687100935E-18d), new DoubleZigguratSegment(70095585905830032UL, 6.7085433298604241E-18d), new DoubleZigguratSegment(70011520199026600UL, 6.5258808812531432E-18d), new DoubleZigguratSegment(69919149640719184UL, 6.340578633444393E-18d),
					new DoubleZigguratSegment(69817183253405528UL, 6.1524100575325489E-18d), new DoubleZigguratSegment(69704047821513848UL, 5.96111965951503E-18d), new DoubleZigguratSegment(69577806414364616UL, 5.7664174798549849E-18d), new DoubleZigguratSegment(69436047005750224UL, 5.5679721821766175E-18d),
					new DoubleZigguratSegment(69275727577351408UL, 5.36540226370626E-18d), new DoubleZigguratSegment(69092956533460488UL, 5.1582647259604188E-18d), new DoubleZigguratSegment(68882674629876648UL, 4.9460402509591506E-18d), new DoubleZigguratSegment(68638182865525768UL, 4.7281134745321224E-18d),
					new DoubleZigguratSegment(68350421943724144UL, 4.5037462269844095E-18d), new DoubleZigguratSegment(68006836686689368UL, 4.272040428935364E-18d), new DoubleZigguratSegment(67589518048921080UL, 4.0318853224078037E-18d), new DoubleZigguratSegment(67072025684496680UL, 3.7818801669484457E-18d),
					new DoubleZigguratSegment(66413657710639856UL, 3.5202169470124157E-18d), new DoubleZigguratSegment(65548422586560504UL, 3.2444947199183369E-18d), new DoubleZigguratSegment(64362011183706384UL, 2.951410102162591E-18d), new DoubleZigguratSegment(62638351640543032UL, 2.6362063921133255E-18d),
					new DoubleZigguratSegment(59916484182323304UL, 2.2916061130118481E-18d), new DoubleZigguratSegment(55019203100610928UL, 1.9054893971358314E-18d), new DoubleZigguratSegment(43886864057643872UL, 1.4549265701529616E-18d), new DoubleZigguratSegment(0UL, 8.8612679136286624E-19d),
				},
				new double[]
				{
					0.00045413435380838583d, 0.00096726928225608492d, 0.0015362997801877318d, 0.0021459677435587436d,
					0.0027887987933646174d, 0.0034602647775755354d, 0.00415729512051815d, 0.0048776559831702785d,
					0.0056196422067748492d, 0.0063819059368280728d, 0.0071633531830815438d, 0.007963077437399475d,
					0.008780314985125548d, 0.00961441364175124d, 0.010464810180209824d, 0.011331013596943648d,
					0.01221259242529206d, 0.01310916493021775d, 0.014020391402069251d, 0.014945968010501512d,
					0.015885621838705087d, 0.01683910682469196d, 0.017806200409482005d, 0.018786700743183851d,
					0.0197804243364133d, 0.020787204070895984d, 0.021806887502514336d, 0.022839335404527462d,
					0.023884420509610454d, 0.024942026417692716d, 0.026012046643002395d, 0.027094383778729809d,
					0.028188948761657083d, 0.029295660222218869d, 0.030414443907949711d, 0.031545232170276945d,
					0.032687963506241695d, 0.0338425821480539d, 0.035009037694472986d, 0.036187284778901582d,
					0.037377282769822696d, 0.038578995499829953d, 0.039792391020019545d, 0.04101744137694914d,
					0.042254122409738012d, 0.043502413565196d, 0.044762297729135668d, 0.046033761072250677d,
					0.047316792909138726d, 0.048611385569216854d, 0.049917534278422444d, 0.051235237050719563d,
					0.0525644945885407d, 0.053905310191389319d, 0.055257689671912989d, 0.056621641278830029d,
					0.057997175626157471d, 0.059384305628245183d, 0.060783046440171115d, 0.062193415403097467d,
					0.063615431994227228d, 0.065049117781035462d, 0.0664944963794817d, 0.067951593415937109d,
					0.069420436492586224d, 0.070901055156084636d, 0.072393480869275217d, 0.073897746985783219d,
					0.075413888727327183d, 0.076941943163597912d, 0.07848194919457073d, 0.0800339475351294d,
					0.0815979807018903d, 0.0831740930021269d, 0.084762330524702528d, 0.0863627411329294d,
					0.087975374459278929d, 0.089600281901875981d, 0.091237516622715828d, 0.0928871335475499d,
					0.094549189367391d, 0.0962237425415948d, 0.097910853302479131d, 0.099610583661447025d,
					0.10132299741658451d, 0.10304816016170756d, 0.10478613929683697d, 0.10653700404008339d,
					0.10830082544092837d, 0.11007767639489066d, 0.1118676316595702d, 0.1136707678720647d,
					0.11548716356775823d, 0.11731689920048237d, 0.11916005716405433d, 0.12101672181519911d,
					0.12288697949786473d, 0.12477091856894357d, 0.1266686294254139d, 0.12858020453291971d,
					0.13050573845580818d, 0.13244532788864832d, 0.13439907168925541d, 0.13636707091324907d,
					0.13834942885017629d, 0.14034625106123183d, 0.14235764541861229d, 0.14438372214654291d,
					0.14642459386401843d, 0.14848037562930291d, 0.15055118498623585d, 0.15263714201239584d,
					0.15473836936917526d, 0.15685499235382366d, 0.15898713895352098d, 0.16113493990154418d,
					0.16329852873559628d, 0.16547804185836973d, 0.16767361860042004d, 0.16988540128543045d,
					0.17211353529795254d, 0.17435816915371238d, 0.17661945457257688d, 0.17889754655427997d,
					0.18119260345701418d, 0.18350478707899806d, 0.18583426274313683d, 0.18818119938489949d,
					0.19054576964354239d, 0.19292814995681637d, 0.19532852065930245d, 0.19774706608452836d,
					0.20018397467102708d, 0.20263943907250709d, 0.20511365627231393d, 0.20760682770237221d,
					0.21011915936680808d, 0.21265086197046337d, 0.2152021510525246d, 0.21777324712550275d,
					0.22036437581981341d, 0.22297576803422109d, 0.22560766009242714d, 0.22826029390609703d,
					0.23093391714463996d, 0.23362878341207297d, 0.23634515243132118d, 0.23908329023632727d,
					0.24184346937236645d, 0.24462596910498685d, 0.24743107563802222d, 0.25025908234115091d,
					0.25311028998750618d, 0.25598500700187404d, 0.25888354972005062d, 0.26180624265996677d,
					0.2647534188052289d, 0.26772541990176768d, 0.27072259676833227d, 0.27374530962161758d,
					0.27679392841686712d, 0.27986883320485051d, 0.28297041450617866d, 0.28609907370398729d,
					0.28925522345609278d, 0.29243928812780412d, 0.29565170424666087d, 0.298892920980461d,
					0.30216340064004349d, 0.30546361920840193d, 0.30879406689782413d, 0.31215524873688633d,
					0.31554768518926862d, 0.31897191280651971d, 0.322428484917064d, 0.32591797235393272d,
					0.3294409642239034d, 0.33299806872095522d, 0.33658991398719129d, 0.34021714902464906d,
					0.34388044466171425d, 0.34758049457817869d, 0.35131801639334165d, 0.35509375282194855d,
					0.35890847290319938d, 0.36276297330854124d, 0.36665807973449621d, 0.37059464838737088d,
					0.37457356756735349d, 0.37859575936024165d, 0.38266218144586261d, 0.38677382903316421d,
					0.39093173693297839d, 0.39513698178060652d, 0.39939068442166209d, 0.40369401247605469d,
					0.40804818309662805d, 0.412454465940805d, 0.41691418637567085d, 0.42142872893928357d,
					0.42599954108367416d, 0.43062813722804411d, 0.43531610315413866d, 0.44006510077974276d,
					0.44487687335079279d, 0.44975325109782172d, 0.45469615740847008d, 0.45970761557474382d,
					0.46478975618174562d, 0.46994482521395248d, 0.47517519296600047d, 0.4804833638576631d,
					0.48587198726763225d, 0.49134386951826819d, 0.4969019871642204d, 0.50254950176239732d,
					0.50828977633001138d, 0.51412639373237223d, 0.52006317728404428d, 0.5261042138975448d,
					0.53225388017500486d, 0.53851687191277653d, 0.54489823758021771d, 0.55140341644618618d,
					0.55803828216579487d, 0.56480919281315733d, 0.57172304856301015d, 0.5787873584983233d,
					0.5860103183698947d, 0.59340090158134917d, 0.600968966251662d, 0.60872538196267267d,
					0.61668218079466508d, 0.62485273857929213d, 0.63325199408589494d, 0.64189671629439837d,
					0.65080583327696861d, 0.66000084093627787d, 0.66950631658364246d, 0.6793505721104135d,
					0.68956649595606312d, 0.700192654914412d, 0.71127476062850714d, 0.722867659407808d,
					0.73503809223523664d, 0.74786862177705538d, 0.761463388627856d, 0.77595685180163154d,
					0.7915276367141405d, 0.80842165123997678d, 0.82699329632824714d, 0.84778550026617883d,
					0.8717043319607144d, 0.90046992940234216d, 0.93814368012746407d, 1d,
				},
				255UL, 8);

			#endregion
		}
	}
}
