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

		public class TwoSidedSymmetricFloatZigguratTable
		{
			public FloatZigguratSegment[] segments;
			public float[] segmentUpperBounds;
			public int threshold;
			public int shift;
			public int mask;

			public TwoSidedSymmetricFloatZigguratTable(FloatZigguratSegment[] segments, float[] segmentUpperBounds, int threshold, int shift, int mask)
			{
				this.segments = segments;
				this.segmentUpperBounds = segmentUpperBounds;
				this.threshold = threshold;
				this.shift = shift;
				this.mask = mask;
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

		public class TwoSidedSymmetricDoubleZigguratTable
		{
			public DoubleZigguratSegment[] segments;
			public double[] segmentUpperBounds;
			public long threshold;
			public long shift;
			public long mask;

			public TwoSidedSymmetricDoubleZigguratTable(DoubleZigguratSegment[] segments, double[] segmentUpperBounds, long threshold, long shift, long mask)
			{
				this.segments = segments;
				this.segmentUpperBounds = segmentUpperBounds;
				this.threshold = threshold;
				this.shift = shift;
				this.mask = mask;
			}
		}

		#region Sampling

		public static float SampleZigguratOneSided(IRandom random, FloatZigguratSegment[] segments, float[] segmentUpperBounds, uint threshold, uint mask, int shift, Func<float, float> f, Func<IRandom, float, float> tailSampleFallback)
		{
			do
			{
				// Select a random segment, and a random n/x within the segment.
				uint n = random.Next32();
				int i = (int)(n & mask);
				n = n >> shift;
				var segment = segments[i];
				float x = n * segment.s;

				// Dominant quick check within fully-contained segment rectangle.
				if (n < segment.n) return x;

				// Rare case within tail.
				if (i == 0) return tailSampleFallback(random, (1 << (32 - shift)) * segments[1].s);

				// Slow check within the partially-contained segment rectangle.
				if (random.RangeCO(segmentUpperBounds[i - 1], segmentUpperBounds[i]) < f(x)) return x;
			} while (true);
		}

		public static double SampleZigguratOneSided(IRandom random, DoubleZigguratSegment[] segments, double[] segmentUpperBounds, ulong threshold, ulong mask, int shift, Func<double, double> f, Func<IRandom, double, double> tailSampleFallback)
		{
			do
			{
				// Select a random segment, and a random n/x within the segment.
				ulong n = random.Next64();
				int i = (int)(n & mask);
				n = n >> shift;
				var segment = segments[i];
				double x = n * segment.s;

				// Dominant quick check within fully-contained segment rectangle.
				if (n < segment.n) return x;

				// Rare case within tail.
				if (i == 0) return tailSampleFallback(random, (1L << (64 - shift)) * segments[1].s);

				// Slow check within the partially-contained segment rectangle.
				if (random.RangeCO(segmentUpperBounds[i - 1], segmentUpperBounds[i]) < f(x)) return x;
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

		public static double SampleZigguratTwoSidedSymmetric(IRandom random, DoubleZigguratSegment[] segments, double[] segmentUpperBounds, long threshold, long mask, int shift, Func<double, double> f, Func<IRandom, double, double> tailSampleFallback)
		{
			do
			{
				// Select a random segment, and a random n/x within the segment.
				long n;
				do
				{
					n = (long)random.Next64();
				} while (n < threshold);
				int i = (int)(n & mask);
				n = n >> shift;
				var segment = segments[i];
				double x = n * segment.s;


				// Dominant quick check within fully-contained segment rectangle.
				if ((ulong)Math.Abs(n) < segment.n) return x;

				// Rare case within tail.
				if (i == 0) return tailSampleFallback(random, (1L << (63 - shift)) * segments[1].s * (n > 0L ? +1d : -1d));

				// Slow check within the partially-contained segment rectangle.
				if (random.RangeCO(segmentUpperBounds[i - 1], segmentUpperBounds[i]) < f(x)) return x;
			} while (true);
		}

		#endregion

		#region Table Construction

		public static TwoSidedSymmetricFloatZigguratTable BuildTwoSidedSymmetricFloatZigguratTable(int tableSizeMagnitidue, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double acceptableError)
		{
			int segmentCount = 1 << tableSizeMagnitidue;

			var segments = new FloatZigguratSegment[segmentCount];
			var segmentUpperBounds = new float[segmentCount];

			var x = new double[segmentCount];
			double a = fCDF(0d) / segmentCount;
			double rMin = fInv(f(0d) / segmentCount);
			double rMax = rMin;
			do
			{
				rMax = fInv(f(rMax) * 0.5d);
			} while (rMax * f(rMax) + totalArea - fCDF(rMax) > a);
			double tableError;
			do
			{
				double rAvg = (rMin + rMax) * 0.5d;
				tableError = CalculateZigguratTableErrorTwoSidedSymmetric(rAvg, segmentCount, f, fInv, fCDF, totalArea, x);
				if (double.IsNaN(tableError) || tableError > 0d)
				{
					rMin = rAvg;
				}
				else
				{
					rMax = rAvg;
				}
			} while (double.IsNaN(tableError) || Math.Abs(tableError) > acceptableError);

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

			return new TwoSidedSymmetricFloatZigguratTable(segments, segmentUpperBounds, int.MinValue + segmentCount, tableSizeMagnitidue, (int)(~((~0U) << tableSizeMagnitidue)));
		}

		public static TwoSidedSymmetricDoubleZigguratTable BuildTwoSidedSymmetricDoubleZiggurat(int tableSizeMagnitidue, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double acceptableError)
		{
			int segmentCount = 1 << tableSizeMagnitidue;

			var segments = new DoubleZigguratSegment[segmentCount];
			var segmentUpperBounds = new double[segmentCount];

			var x = new double[segmentCount];
			double a = fCDF(0d) / segmentCount;
			double rMin = fInv(f(0d) / segmentCount);
			double rMax = rMin;
			do
			{
				rMax = fInv(f(rMax) * 0.5d);
			} while (rMax * f(rMax) + totalArea - fCDF(rMax) > a);
			double tableError;
			do
			{
				double rAvg = (rMin + rMax) * 0.5d;
				tableError = CalculateZigguratTableErrorTwoSidedSymmetric(rAvg, segmentCount, f, fInv, fCDF, totalArea, x);
				if (double.IsNaN(tableError) || tableError > 0d)
				{
					rMin = rAvg;
				}
				else
				{
					rMax = rAvg;
				}
			} while (double.IsNaN(tableError) || Math.Abs(tableError) > acceptableError);

			double intToFloatScale = 1 << (63 - tableSizeMagnitidue);

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

			return new TwoSidedSymmetricDoubleZigguratTable(segments, segmentUpperBounds, int.MinValue + segmentCount, tableSizeMagnitidue, (long)(~((~0UL) << tableSizeMagnitidue)));
		}

		public static double CalculateZigguratTableErrorTwoSidedSymmetric(double r, int segmentCount, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double[] x)
		{
			x[0] = r;
			x[segmentCount - 1] = 0d;
			double v = r * f(r) + totalArea - fCDF(r);
			double xPrev = r;
			for (int i = 1; i < segmentCount - 1; ++i)
			{
				x[i] = xPrev = fInv(v / xPrev + f(xPrev));
			}
			return v - xPrev * (f(0d) - f(xPrev));
		}

		#endregion

		#region Lookup Table Generation

		private static void GenerateZigguratLookupTable(TwoSidedSymmetricFloatZigguratTable table, string distributionName)
		{
			using (var file = System.IO.File.Open("Experilous/MakeItRandom/Generated/" + distributionName + "DistributionFloatZigguratTable.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write))
			{
				using (var writer = new System.IO.StreamWriter(file))
				{
					writer.Write("\t\t\tpublic static readonly TwoSidedSymmetricFloatZigguratTable zigguratTable = new TwoSidedSymmetricFloatZigguratTable(\n", distributionName);

					writer.Write("\t\t\t\tnew FloatZigguratSegment[]\n\t\t\t{\n");
					int itemsInRow = 0;
					foreach (var segment in table.segments)
					{
						if (itemsInRow == 0) writer.Write("\t\t\t\t\t");
						writer.Write("new FloatZigguratSegment({0:D}U, {1:R}f)", segment.n, segment.s);
						if (itemsInRow < 3)
						{
							writer.Write(", ");
							++itemsInRow;
						}
						else
						{
							writer.Write(",\n");
							itemsInRow = 0;
						}

					}
					writer.Write("\t\t\t\t},\n");

					writer.Write("\t\t\t\tnew float[]\n\t\t\t{\n");
					itemsInRow = 0;
					foreach (var segmentUpperBound in table.segmentUpperBounds)
					{
						if (itemsInRow == 0) writer.Write("\t\t\t\t\t");
						writer.Write(segmentUpperBound.ToString("R"));
						if (itemsInRow < 3)
						{
							writer.Write("f, ");
							++itemsInRow;
						}
						else
						{
							writer.Write("f,\n");
							itemsInRow = 0;
						}

					}
					writer.Write("\t\t\t\t},\n");
					writer.Write("\t\t\t\t{0:D}, {1:D}, {2:D});\n\n", table.threshold, table.shift, table.mask);
				}
			}
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

#if UNITY_EDITOR
			//[UnityEditor.Callbacks.DidReloadScripts] // Uncomment this attribute in order to generate and print tables in the Unity console pane.
			private static void GenerateZigguratLookupTable()
			{
				var table = BuildTwoSidedSymmetricFloatZigguratTable(8,
					NormalDouble.F,
					NormalDouble.Inv,
					NormalDouble.CDF,
					NormalDouble.totalArea,
					0.0000000001d);

				Distributions.GenerateZigguratLookupTable(table, "normal");
			}
#endif

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
				-2147483392, 8, 255);
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
		}
	}
}
