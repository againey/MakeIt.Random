/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using System.IO;
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
{
	class ArchitectureIdentityTests
	{
		private const string seed = "random seed";

		private const int next32Count = 1024;
		private const int next64Count = 1024;
		private const int intRangeCount = 1024;
		private const int floatRangeCount = 1024;
		private const int geometryRangeCount = 1024;
		private const string dataFolderPath = "Experilous/MakeItRandom/TestData";

#if MAKEITRANDOM_OPTIMIZED_FOR_32BIT
		private const string dataFilePathTemplate = "{0}_{1}_32opt.dat";
		private const string dataFilePathTemplate_CrossPlatform = "{0}_{1}_64opt.dat";
#else
		private const string dataFilePathTemplate = "{0}_{1}_64opt.dat";
		private const string dataFilePathTemplate_CrossPlatform = "{0}_{1}_32opt.dat";
#endif

		private static void CalculateTestValues(IRandom random, string operationName, System.Action<IRandom, BinaryWriter> writeTestValue, int testValueCount)
		{
			string dataFilePath = string.Format(dataFilePathTemplate, random.GetType().Name, operationName);
			using (var file = File.Open(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), dataFolderPath), dataFilePath), FileMode.Create, FileAccess.Write))
			{
				using (var writer = new BinaryWriter(file))
				{
					var state = random.SaveState();
					writer.Write(state.Length);
					writer.Write(state);
					writer.Write(testValueCount);
					for (int i = 0; i < testValueCount; ++i)
					{
						writeTestValue(random, writer);
					}
				}
			}
		}

		//[UnityEditor.MenuItem("Assets/Make It Random/Calculate Arch Identity Test Values")]
		public static void CalculateAllTestValues()
		{
			Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory().Replace('\\', '/').Trim('/'), dataFolderPath));

			CalculateTestValues(XorShift128Plus.Create(seed), "Next32", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next32()); }, next32Count);
			CalculateTestValues(XorShift1024Star.Create(seed), "Next32", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next32()); }, next32Count);
			CalculateTestValues(XoroShiro128Plus.Create(seed), "Next32", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next32()); }, next32Count);
			CalculateTestValues(XorShiftAdd.Create(seed), "Next32", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next32()); }, next32Count);
			CalculateTestValues(SplitMix64.Create(seed), "Next32", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next32()); }, next32Count);

			CalculateTestValues(XorShift128Plus.Create(seed), "Next64", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next64()); }, next64Count);
			CalculateTestValues(XorShift1024Star.Create(seed), "Next64", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next64()); }, next64Count);
			CalculateTestValues(XoroShiro128Plus.Create(seed), "Next64", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next64()); }, next64Count);
			CalculateTestValues(XorShiftAdd.Create(seed), "Next64", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next64()); }, next64Count);
			CalculateTestValues(SplitMix64.Create(seed), "Next64", (IRandom random, BinaryWriter writer) => { writer.Write(random.Next64()); }, next64Count);

			CalculateTestValues(XorShift128Plus.Create(seed), "IntRange", (IRandom random, BinaryWriter writer) => { writer.Write(random.RangeCO(71, 1500)); }, intRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "IntRange_Pow2", (IRandom random, BinaryWriter writer) => { writer.Write(random.RangeCO(71, 1095)); }, intRangeCount);

			CalculateTestValues(XorShift128Plus.Create(seed), "FloatOO", (IRandom random, BinaryWriter writer) => { writer.Write(random.FloatOO()); }, floatRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "FloatCO", (IRandom random, BinaryWriter writer) => { writer.Write(random.FloatCO()); }, floatRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "FloatOC", (IRandom random, BinaryWriter writer) => { writer.Write(random.FloatOC()); }, floatRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "FloatCC", (IRandom random, BinaryWriter writer) => { writer.Write(random.FloatCC()); }, floatRangeCount);

			CalculateTestValues(XorShift128Plus.Create(seed), "DoubleOO", (IRandom random, BinaryWriter writer) => { writer.Write(random.DoubleOO()); }, floatRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "DoubleCO", (IRandom random, BinaryWriter writer) => { writer.Write(random.DoubleCO()); }, floatRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "DoubleOC", (IRandom random, BinaryWriter writer) => { writer.Write(random.DoubleOC()); }, floatRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "DoubleCC", (IRandom random, BinaryWriter writer) => { writer.Write(random.DoubleCC()); }, floatRangeCount);

			CalculateTestValues(XorShift128Plus.Create(seed), "UnitVector2", (IRandom random, BinaryWriter writer) => { writer.Write(random.UnitVector2()); }, geometryRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "UnitVector3", (IRandom random, BinaryWriter writer) => { writer.Write(random.UnitVector3()); }, geometryRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "UnitVector4", (IRandom random, BinaryWriter writer) => { writer.Write(random.UnitVector4()); }, geometryRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "Rotation", (IRandom random, BinaryWriter writer) => { writer.Write(random.Rotation()); }, geometryRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "PointWithinCircle", (IRandom random, BinaryWriter writer) => { writer.Write(random.PointWithinCircle()); }, geometryRangeCount);
			CalculateTestValues(XorShift128Plus.Create(seed), "PointWithinSphere", (IRandom random, BinaryWriter writer) => { writer.Write(random.PointWithinSphere()); }, geometryRangeCount);
		}

		private void CompareTestValues<TRandom>(System.Func<byte[], TRandom> randomFactory, string operationName, bool crossPlatform, System.Action<IRandom, BinaryReader> comparer) where TRandom : IRandom
		{
			string dataFilePath = string.Format(crossPlatform ? dataFilePathTemplate_CrossPlatform : dataFilePathTemplate, typeof(TRandom).Name, operationName);
			try
			{
				using (var file = File.Open(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), dataFolderPath), dataFilePath), FileMode.Open, FileAccess.Read))
				{
					using (var reader = new BinaryReader(file))
					{
						int stateLength = reader.ReadInt32();
						byte[] state = reader.ReadBytes(stateLength);
						var random = randomFactory(state);
						int testValueCount = reader.ReadInt32();
						for (int i = 0; i < testValueCount; ++i)
						{
							comparer(random, reader);
						}
					}
				}
			}
			catch (DirectoryNotFoundException)
			{
				Assert.Inconclusive(
					crossPlatform == false
						? "No test data to compare against.  First execute CalculateTestValues."
						: "No test data to compare against.  First change the build settings to a different platform and execute CalculateTestValues().");
			}
			catch (FileNotFoundException)
			{
				Assert.Inconclusive(
					crossPlatform == false
						? "No test data to compare against.  First execute CalculateTestValues."
						: "No test data to compare against.  First change the build settings to a different platform and execute CalculateTestValues().");
			}
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_Next32()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "Next32", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_Next32_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "Next32", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_Next64()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "Next64", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_Next64_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "Next64", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift1024Star_Next32()
		{
			CompareTestValues<XorShift1024Star>(XorShift1024Star.CreateWithState, "Next32", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift1024Star_Next32_CrossPlatform()
		{
			CompareTestValues<XorShift1024Star>(XorShift1024Star.CreateWithState, "Next32", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift1024Star_Next64()
		{
			CompareTestValues<XorShift1024Star>(XorShift1024Star.CreateWithState, "Next64", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift1024Star_Next64_CrossPlatform()
		{
			CompareTestValues<XorShift1024Star>(XorShift1024Star.CreateWithState, "Next64", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXoroShiro128Plus_Next32()
		{
			CompareTestValues<XoroShiro128Plus>(XoroShiro128Plus.CreateWithState, "Next32", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXoroShiro128Plus_Next32_CrossPlatform()
		{
			CompareTestValues<XoroShiro128Plus>(XoroShiro128Plus.CreateWithState, "Next32", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXoroShiro128Plus_Next64()
		{
			CompareTestValues<XoroShiro128Plus>(XoroShiro128Plus.CreateWithState, "Next64", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXoroShiro128Plus_Next64_CrossPlatform()
		{
			CompareTestValues<XoroShiro128Plus>(XoroShiro128Plus.CreateWithState, "Next64", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShiftAdd_Next32()
		{
			CompareTestValues<XorShiftAdd>(XorShiftAdd.CreateWithState, "Next32", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShiftAdd_Next32_CrossPlatform()
		{
			CompareTestValues<XorShiftAdd>(XorShiftAdd.CreateWithState, "Next32", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShiftAdd_Next64()
		{
			CompareTestValues<XorShiftAdd>(XorShiftAdd.CreateWithState, "Next64", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShiftAdd_Next64_CrossPlatform()
		{
			CompareTestValues<XorShiftAdd>(XorShiftAdd.CreateWithState, "Next64", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareSplitMix64_Next32()
		{
			CompareTestValues<SplitMix64>(SplitMix64.CreateWithState, "Next32", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareSplitMix64_Next32_CrossPlatform()
		{
			CompareTestValues<SplitMix64>(SplitMix64.CreateWithState, "Next32", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt32(), random.Next32()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareSplitMix64_Next64()
		{
			CompareTestValues<SplitMix64>(SplitMix64.CreateWithState, "Next64", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareSplitMix64_Next64_CrossPlatform()
		{
			CompareTestValues<SplitMix64>(SplitMix64.CreateWithState, "Next64", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadUInt64(), random.Next64()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_IntRange()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "IntRange", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadInt32(), random.RangeCO(71, 1500)); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_IntRange_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "IntRange", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadInt32(), random.RangeCO(71, 1500)); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_IntRange_Pow2()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "IntRange_Pow2", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadInt32(), random.RangeCO(71, 1095)); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_IntRange_Pow2_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "IntRange_Pow2", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadInt32(), random.RangeCO(71, 1095)); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatOO()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatOO", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatOO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatOO_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatOO", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatOO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatCO()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatCO", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatCO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatCO_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatCO", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatCO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatOC()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatOC", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatOC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatOC_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatOC", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatOC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatCC()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatCC", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatCC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_FloatCC_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "FloatCC", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadSingle(), random.FloatCC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleOO()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleOO", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleOO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleOO_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleOO", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleOO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleCO()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleCO", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleCO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleCO_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleCO", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleCO()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleOC()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleOC", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleOC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleOC_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleOC", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleOC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleCC()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleCC", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleCC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_DoubleCC_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "DoubleCC", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadDouble(), random.DoubleCC()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_UnitVector2()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "UnitVector2", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector2(), random.UnitVector2()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_UnitVector2_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "UnitVector2", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector2(), random.UnitVector2()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_UnitVector3()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "UnitVector3", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector3(), random.UnitVector3()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_UnitVector3_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "UnitVector3", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector3(), random.UnitVector3()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_UnitVector4()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "UnitVector4", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector4(), random.UnitVector4()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_UnitVector4_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "UnitVector4", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector4(), random.UnitVector4()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_Rotation()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "Rotation", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadQuaternion(), random.Rotation()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_Rotation_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "Rotation", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadQuaternion(), random.Rotation()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_PointWithinCircle()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "PointWithinCircle", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector2(), random.PointWithinCircle()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_PointWithinCircle_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "PointWithinCircle", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector2(), random.PointWithinCircle()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_PointWithinSphere()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "PointWithinSphere", false, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector3(), random.PointWithinSphere()); });
		}

		[TestCase(Category = "Determinism")]
		public void CompareXorShift128Plus_PointWithinSphere_CrossPlatform()
		{
			CompareTestValues<XorShift128Plus>(XorShift128Plus.CreateWithState, "PointWithinSphere", true, (IRandom random, BinaryReader reader) => { Assert.AreEqual(reader.ReadVector3(), random.PointWithinSphere()); });
		}
	}

	static class ArchitectureIdentityExtensions
	{
		public static void Write(this BinaryWriter writer, Vector2 vector)
		{
			writer.Write(vector.x);
			writer.Write(vector.y);
		}

		public static void Write(this BinaryWriter writer, Vector3 vector)
		{
			writer.Write(vector.x);
			writer.Write(vector.y);
			writer.Write(vector.z);
		}

		public static void Write(this BinaryWriter writer, Vector4 vector)
		{
			writer.Write(vector.x);
			writer.Write(vector.y);
			writer.Write(vector.z);
			writer.Write(vector.w);
		}

		public static void Write(this BinaryWriter writer, Quaternion quaternion)
		{
			writer.Write(quaternion.x);
			writer.Write(quaternion.y);
			writer.Write(quaternion.z);
			writer.Write(quaternion.w);
		}

		public static Vector2 ReadVector2(this BinaryReader reader)
		{
			return new Vector2(reader.ReadSingle(), reader.ReadSingle());
		}

		public static Vector3 ReadVector3(this BinaryReader reader)
		{
			return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}

		public static Vector4 ReadVector4(this BinaryReader reader)
		{
			return new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}

		public static Quaternion ReadQuaternion(this BinaryReader reader)
		{
			return new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
	}
}
#endif
