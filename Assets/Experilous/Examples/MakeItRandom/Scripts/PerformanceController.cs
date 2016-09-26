/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Experilous.MakeItRandom;
using System.Runtime;

namespace Experilous.Examples.MakeItRandom
{
	public class PerformanceController : MonoBehaviour
	{
		public PerformanceResultItem performanceResultItemPrefab;

		public Toggle xorShift128PlusToggle;
		public Toggle xorShift1024StarToggle;
		public Toggle xoroShiro128PlusToggle;
		public Toggle splitMix64Toggle;
		public Toggle xorShiftAddToggle;
		public Toggle unityRandomToggle;
		public Toggle systemRandomToggle;

		public Toggle useNativeCallsToggle;

		public Toggle uint31bitToggle;
		public Toggle uint32bitToggle;
		public Toggle uint64bitToggle;
		public Toggle uintLessThan6Toggle;
		public Toggle floatOpenToggle;
		public Toggle floatHalfOpenToggle;
		public Toggle floatHalfClosedToggle;
		public Toggle floatClosedToggle;
		public Toggle doubleOpenToggle;
		public Toggle doubleHalfOpenToggle;
		public Toggle doubleHalfClosedToggle;
		public Toggle doubleClosedToggle;
		public Toggle unitVector2Toggle;
		public Toggle unitVector3Toggle;
		public Toggle unitVector4Toggle;
		public Toggle quaternionToggle;
		public Toggle vector2WithinCircleToggle;
		public Toggle vector3WithinSphereToggle;

		public Slider warmupDurationSlider;
		public Text warmupDurationText;

		public Slider measurementDurationSlider;
		public Text measurementDurationText;

		public Button measurePerformanceButton;
		public Text measurePerformanceButtonText;

		public Slider measurementProgressSlider;

		public VerticalLayoutGroup performanceResultItemsLayoutGroup;
		public ScrollRect performanceResultItemsScrollRect;

		private EventWaitHandle _concurrentWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
		private Exception _measurementException;

		private Coroutine _measurementCoroutine;
		private object _cancelLock = new object();
		private bool _cancelPending;

		private string _currentGeneratorName;
		private string _currentOperationName;
		private double _currentPerformanceMeasurement;

#pragma warning disable 0414
		private int _generatedInt;
		private uint _generatedUInt;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
		private uint _generatedUInt2;
#endif
		private ulong _generatedULong;
		private float _generatedFloat;
		private double _generatedDouble;
#pragma warning restore 0414

		protected void Start()
		{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
			performanceResultItemsScrollRect.scrollSensitivity = 25;
#endif

			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			OnWarmupDurationSliderValueChanged();
			OnMeasurementDurationSliderValueChanged();
			EnableDisableMeasurePerformanceButton();
		}

		public void OnGeneratorToggleValueChanged(Toggle toggle)
		{
			EnableDisableMeasurePerformanceButton();
		}

		public void OnOperationToggleValueChanged(Toggle toggle)
		{
			EnableDisableMeasurePerformanceButton();
		}

		public void OnWarmupDurationSliderValueChanged()
		{
			warmupDurationText.text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:F2} s", warmupDuration);
		}

		public void OnMeasurementDurationSliderValueChanged()
		{
			measurementDurationText.text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:F0} s", measurementDuration);
		}

		private float warmupDuration { get { return warmupDurationSlider.value * 0.05f; } }
		private float measurementDuration { get { return measurementDurationSlider.value * 1f; } }

		private void EnableDisableMeasurePerformanceButton()
		{
			bool anyGeneratorSelected =
				xorShift128PlusToggle.isOn ||
				xorShift1024StarToggle.isOn ||
				xoroShiro128PlusToggle.isOn ||
				splitMix64Toggle.isOn ||
				xorShiftAddToggle.isOn ||
				unityRandomToggle.isOn ||
				systemRandomToggle.isOn;

			bool anyOperationSelected =
				uint31bitToggle.isOn ||
				uint32bitToggle.isOn ||
				uint64bitToggle.isOn ||
				uintLessThan6Toggle.isOn ||
				floatOpenToggle.isOn ||
				floatHalfOpenToggle.isOn ||
				floatHalfClosedToggle.isOn ||
				floatClosedToggle.isOn ||
				doubleOpenToggle.isOn ||
				doubleHalfOpenToggle.isOn ||
				doubleHalfClosedToggle.isOn ||
				doubleClosedToggle.isOn ||
				unitVector2Toggle.isOn ||
				unitVector3Toggle.isOn ||
				unitVector4Toggle.isOn ||
				quaternionToggle.isOn ||
				vector2WithinCircleToggle.isOn ||
				vector3WithinSphereToggle.isOn;

			measurePerformanceButton.interactable = _measurementCoroutine != null && _cancelPending == false || anyGeneratorSelected && anyOperationSelected;
		}

		#region Measurement

		public void MeasurePerformance()
		{
			if (_measurementCoroutine == null)
			{
				_cancelPending = false;
				_measurementCoroutine = StartCoroutine(MeasurePerformanceConcurrently());
			}
			else
			{
				lock (_cancelLock)
				{
					_cancelPending = true;
				}

				measurePerformanceButtonText.text = "Canceling...";
				EnableDisableMeasurePerformanceButton();
			}
		}

		public IEnumerator MeasurePerformanceConcurrently()
		{
			List<Toggle> generatorToggles = new List<Toggle>();

			if (xorShift128PlusToggle.isOn) generatorToggles.Add(xorShift128PlusToggle);
			if (xorShift1024StarToggle.isOn) generatorToggles.Add(xorShift1024StarToggle);
			if (xoroShiro128PlusToggle.isOn) generatorToggles.Add(xoroShiro128PlusToggle);
			if (splitMix64Toggle.isOn) generatorToggles.Add(splitMix64Toggle);
			if (xorShiftAddToggle.isOn) generatorToggles.Add(xorShiftAddToggle);
			if (unityRandomToggle.isOn) generatorToggles.Add(unityRandomToggle);
			if (systemRandomToggle.isOn) generatorToggles.Add(systemRandomToggle);

			List<Toggle> operationToggles = new List<Toggle>();

			if (uint31bitToggle.isOn) operationToggles.Add(uint31bitToggle);
			if (uint32bitToggle.isOn) operationToggles.Add(uint32bitToggle);
			if (uint64bitToggle.isOn) operationToggles.Add(uint64bitToggle);
			if (uintLessThan6Toggle.isOn) operationToggles.Add(uintLessThan6Toggle);
			if (floatOpenToggle.isOn) operationToggles.Add(floatOpenToggle);
			if (floatHalfOpenToggle.isOn) operationToggles.Add(floatHalfOpenToggle);
			if (floatHalfClosedToggle.isOn) operationToggles.Add(floatHalfClosedToggle);
			if (floatClosedToggle.isOn) operationToggles.Add(floatClosedToggle);
			if (doubleOpenToggle.isOn) operationToggles.Add(doubleOpenToggle);
			if (doubleHalfOpenToggle.isOn) operationToggles.Add(doubleHalfOpenToggle);
			if (doubleHalfClosedToggle.isOn) operationToggles.Add(doubleHalfClosedToggle);
			if (doubleClosedToggle.isOn) operationToggles.Add(doubleClosedToggle);
			if (unitVector2Toggle.isOn) operationToggles.Add(unitVector2Toggle);
			if (unitVector3Toggle.isOn) operationToggles.Add(unitVector3Toggle);
			if (unitVector4Toggle.isOn) operationToggles.Add(unitVector4Toggle);
			if (quaternionToggle.isOn) operationToggles.Add(quaternionToggle);
			if (vector2WithinCircleToggle.isOn) operationToggles.Add(vector2WithinCircleToggle);
			if (vector3WithinSphereToggle.isOn) operationToggles.Add(vector3WithinSphereToggle);

			int totalRunCount = generatorToggles.Count * operationToggles.Count;
			float targetRunDuration = warmupDuration + measurementDuration;

			int completedRunCount = 0;

			string originalMeasurePerformanceButtonText = measurePerformanceButtonText.text;
			measurePerformanceButtonText.text = string.Format("Cancel ({0:F0} of {1:F0} complete)", completedRunCount, totalRunCount);
			measurementProgressSlider.value = 0;
			measurementProgressSlider.gameObject.SetActive(true);

			foreach (Toggle operationToggle in operationToggles)
			{
				foreach (Toggle generatorToggle in generatorToggles)
				{
					yield return null;

					WaitHandle waitHandle;
					try
					{
						waitHandle = MeasurePerformance(generatorToggle, operationToggle);
					}
					catch (Exception)
					{
						measurementProgressSlider.gameObject.SetActive(false);
						measurementProgressSlider.value = 0;
						measurePerformanceButtonText.text = originalMeasurePerformanceButtonText;
						EnableDisableMeasurePerformanceButton();
						throw;
					}

					if (waitHandle != null)
					{
						float startTime = Time.time;

						while (!waitHandle.WaitOne(0))
						{
							float currentRunDuration = Time.time - startTime;
							measurementProgressSlider.value = (completedRunCount + currentRunDuration / targetRunDuration) / totalRunCount;
							yield return null;
						}

						if (_measurementException != null)
						{
							Exception exception = _measurementException;
							_measurementException = null;
							_measurementCoroutine = null;
							_cancelPending = false;

							measurementProgressSlider.gameObject.SetActive(false);
							measurementProgressSlider.value = 0;
							measurePerformanceButtonText.text = originalMeasurePerformanceButtonText;
							EnableDisableMeasurePerformanceButton();
							throw exception;
						}
					}

					lock (_cancelLock)
					{
						if (_cancelPending) goto MeasurementsComplete;
					}

					bool isScrollbarAtBottom = performanceResultItemsScrollRect.verticalNormalizedPosition < 0.0001f;

					PerformanceResultItem performanceResultItem = Instantiate(performanceResultItemPrefab);
					performanceResultItem.generator.text = _currentGeneratorName;
					performanceResultItem.operation.text = _currentOperationName;
					performanceResultItem.performance.text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N1} ops/s", _currentPerformanceMeasurement);
					performanceResultItem.transform.SetParent(performanceResultItemsLayoutGroup.transform, false);

					if (isScrollbarAtBottom)
					{
						Canvas.ForceUpdateCanvases();
						performanceResultItemsScrollRect.verticalNormalizedPosition = 0f;
						Canvas.ForceUpdateCanvases();
					}

					++completedRunCount;
					measurePerformanceButtonText.text = string.Format("Cancel ({0:F0} of {1:F0} complete)", completedRunCount, totalRunCount);
					measurementProgressSlider.value = (float)completedRunCount / totalRunCount;
				}
			}

			MeasurementsComplete:

			_measurementCoroutine = null;
			_cancelPending = false;

			measurementProgressSlider.gameObject.SetActive(false);
			measurementProgressSlider.value = 0;
			measurePerformanceButtonText.text = originalMeasurePerformanceButtonText;
			EnableDisableMeasurePerformanceButton();
		}

		private WaitHandle MeasurePerformance(Toggle generatorToggle, Toggle operationToggle)
		{
			_currentGeneratorName = "";
			_currentOperationName = "";
			_currentPerformanceMeasurement = 0.0;

			Action<long> operation = null;

			if (useNativeCallsToggle.isOn)
			{
				if (generatorToggle == unityRandomToggle)
				{
					_currentGeneratorName = "UnityEngine.Random (native)";

					if (operationToggle == uintLessThan6Toggle)
					{
						_currentOperationName = "Integer Less Than 6 (Random.Range(0, 6))";
						operation = MeasurePerformance_UnityRandomUIntLessThan6;
					}
					else if (operationToggle == floatClosedToggle)
					{
						_currentOperationName = "Closed Unit Float (Random.value)";
						operation = MeasurePerformance_UnityRandomFloatClosed;
					}
					else if (operationToggle == unitVector3Toggle)
					{
						_currentOperationName = "Unit Vector3 (Random.onUnitSphere)";
						operation = MeasurePerformance_UnityRandomUnitVector3;
					}
					else if (operationToggle == quaternionToggle)
					{
						_currentOperationName = "Quaternion (Random.rotationUniform)";
						operation = MeasurePerformance_UnityRandomQuaternion;
					}
					else if (operationToggle == vector2WithinCircleToggle)
					{
						_currentOperationName = "Within Circle (Random.insideUnitCircle)";
						operation = MeasurePerformance_UnityRandomVector2WithinCircle;
					}
					else if (operationToggle == vector3WithinSphereToggle)
					{
						_currentOperationName = "Within Sphere (Random.insideUnitSphere)";
						operation = MeasurePerformance_UnityRandomVector3WithinSphere;
					}
				}
				else if (generatorToggle == systemRandomToggle)
				{
					_currentGeneratorName = "System.Random (native)";

					if (operationToggle == uint31bitToggle)
					{
						_currentOperationName = "31-bit Integer (Random.Next())";
						System.Random systemRandom = new System.Random();
						operation = (long iterations) => { MeasurePerformance_SystemRandomUInt31bit(systemRandom, iterations); };
					}
					else if (operationToggle == uintLessThan6Toggle)
					{
						_currentOperationName = "Integer Less Than 6 (Random.Next(6))";
						System.Random systemRandom = new System.Random();
						operation = (long iterations) => { MeasurePerformance_SystemRandomUIntLessThan6(systemRandom, iterations); };
					}
					else if (operationToggle == doubleHalfOpenToggle)
					{
						_currentOperationName = "Half Open Unit Double (Random.NextDouble())";
						System.Random systemRandom = new System.Random();
						operation = (long iterations) => { MeasurePerformance_SystemRandomDoubleHalfOpen(systemRandom, iterations); };
					}
				}
			}

			if (operation == null)
			{
				IRandom random = null;

				if (generatorToggle == xorShift128PlusToggle)
				{
					_currentGeneratorName = "XorShift128+";
					random = XorShift128Plus.Create();
				}
				else if (generatorToggle == xorShift1024StarToggle)
				{
					_currentGeneratorName = "XorShift1024*";
					random = XorShift1024Star.Create();
				}
				else if (generatorToggle == xoroShiro128PlusToggle)
				{
					_currentGeneratorName = "XoroShiro128+";
					random = XoroShiro128Plus.Create();
				}
				else if (generatorToggle == splitMix64Toggle)
				{
					_currentGeneratorName = "SplitMix64";
					random = SplitMix64.Create();
				}
				else if (generatorToggle == xorShiftAddToggle)
				{
					_currentGeneratorName = "XorShift-Add";
					random = XorShiftAdd.Create();
				}
				else if (generatorToggle == unityRandomToggle)
				{
					_currentGeneratorName = "UnityEngine.Random";
					random = UnityRandom.Create();
				}
				else if (generatorToggle == systemRandomToggle)
				{
					_currentGeneratorName = "System.Random";
					random = SystemRandom.Create();
				}
				else
				{
					throw new InvalidOperationException();
				}

				if (operationToggle == uint31bitToggle)
				{
					_currentOperationName = "31-bit Integer";
					operation = (long iterations) => { MeasurePerformance_UInt31bit(random, iterations); };
				}
				else if (operationToggle == uint32bitToggle)
				{
					_currentOperationName = "32-bit Integer";
					operation = (long iterations) => { MeasurePerformance_UInt32bit(random, iterations); };
				}
				else if (operationToggle == uint64bitToggle)
				{
					_currentOperationName = "64-bit Integer";
					operation = (long iterations) => { MeasurePerformance_UInt64bit(random, iterations); };
				}
				else if (operationToggle == uintLessThan6Toggle)
				{
					_currentOperationName = "Integer Less Than 6";
					operation = (long iterations) => { MeasurePerformance_UIntLessThan6(random, iterations); };
				}
				else if (operationToggle == floatOpenToggle)
				{
					_currentOperationName = "Open Unit Float";
					operation = (long iterations) => { MeasurePerformance_FloatOpen(random, iterations); };
				}
				else if (operationToggle == floatHalfOpenToggle)
				{
					_currentOperationName = "HalfOpen Unit Float";
					operation = (long iterations) => { MeasurePerformance_FloatHalfOpen(random, iterations); };
				}
				else if (operationToggle == floatHalfClosedToggle)
				{
					_currentOperationName = "HalfClosed Unit Float";
					operation = (long iterations) => { MeasurePerformance_FloatHalfClosed(random, iterations); };
				}
				else if (operationToggle == floatClosedToggle)
				{
					_currentOperationName = "Closed Unit Float";
					operation = (long iterations) => { MeasurePerformance_FloatClosed(random, iterations); };
				}
				else if (operationToggle == doubleOpenToggle)
				{
					_currentOperationName = "Open Unit Double";
					operation = (long iterations) => { MeasurePerformance_DoubleOpen(random, iterations); };
				}
				else if (operationToggle == doubleHalfOpenToggle)
				{
					_currentOperationName = "HalfOpen Unit Double";
					operation = (long iterations) => { MeasurePerformance_DoubleHalfOpen(random, iterations); };
				}
				else if (operationToggle == doubleHalfClosedToggle)
				{
					_currentOperationName = "HalfClosed Unit Double";
					operation = (long iterations) => { MeasurePerformance_DoubleHalfClosed(random, iterations); };
				}
				else if (operationToggle == doubleClosedToggle)
				{
					_currentOperationName = "Closed Unit Double";
					operation = (long iterations) => { MeasurePerformance_DoubleClosed(random, iterations); };
				}
				else if (operationToggle == unitVector2Toggle)
				{
					_currentOperationName = "Unit Vector2";
					operation = (long iterations) => { MeasurePerformance_UnitVector2(random, iterations); };
				}
				else if (operationToggle == unitVector3Toggle)
				{
					_currentOperationName = "Unit Vector3";
					operation = (long iterations) => { MeasurePerformance_UnitVector3(random, iterations); };
				}
				else if (operationToggle == unitVector4Toggle)
				{
					_currentOperationName = "Unit Vector4";
					operation = (long iterations) => { MeasurePerformance_UnitVector4(random, iterations); };
				}
				else if (operationToggle == quaternionToggle)
				{
					_currentOperationName = "Quaternion";
					operation = (long iterations) => { MeasurePerformance_Quaternion(random, iterations); };
				}
				else if (operationToggle == vector2WithinCircleToggle)
				{
					_currentOperationName = "Within Circle";
					operation = (long iterations) => { MeasurePerformance_Vector2WithinCircle(random, iterations); };
				}
				else if (operationToggle == vector3WithinSphereToggle)
				{
					_currentOperationName = "Within Sphere";
					operation = (long iterations) => { MeasurePerformance_Vector3WithinSphere(random, iterations); };
				}
				else
				{
					throw new InvalidOperationException();
				}
			}

			if (generatorToggle == unityRandomToggle)
			{
				MeasurePerformance(operation);
				return null;
			}
			else
			{
				_concurrentWaitHandle.Reset();
				_measurementException = null;
				Action action = () => { MeasurePerformance(operation); };
				ThreadPool.QueueUserWorkItem(ExecuteWaitableAction, action);
				return _concurrentWaitHandle;
			}
		}

		private void ExecuteWaitableAction(object action)
		{
			try
			{
				((Action)action)();
			}
			catch (Exception e)
			{
				_measurementException = e;
			}
			finally
			{
				_concurrentWaitHandle.Set();
			}
		}

		private void MeasurePerformance(Action<long> operationLoop)
		{
			System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
			IntPtr originalProcessorAffinity = currentProcess.ProcessorAffinity;
			System.Diagnostics.ProcessPriorityClass originalPriorityClass = currentProcess.PriorityClass;
			System.Threading.ThreadPriority originalThreadPriority = Thread.CurrentThread.Priority;
			GCLatencyMode originalGCLatencyMode = GCSettings.LatencyMode;

			try
			{
				currentProcess.ProcessorAffinity = new IntPtr(2);
				currentProcess.PriorityClass = System.Diagnostics.ProcessPriorityClass.AboveNormal;
				Thread.CurrentThread.Priority = System.Threading.ThreadPriority.AboveNormal;

				GCSettings.LatencyMode = GCLatencyMode.LowLatency;

				long ticksPerSecond = System.Diagnostics.Stopwatch.Frequency;
				long warmupDurationTickCount = (long)((double)Math.Max(warmupDuration, 0.01f) * ticksPerSecond);
				long measurementDurationTickCount = (long)((double)Math.Max(measurementDuration, 0.1f) * ticksPerSecond);
				long targetWarmupBatchDurationTickCount = Math.Min(ticksPerSecond, warmupDurationTickCount / 4L);
				long targetMeasurementBatchDurationTickCount = Math.Min(ticksPerSecond, measurementDurationTickCount / 16L);

				long batchIterationCount = 1024L;
				long warmupIterationCount = 0L;
				long measurementIterationCount = 0L;

				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();

				System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

				while (true)
				{
					timer.Start();
					operationLoop(batchIterationCount);
					timer.Stop();

					warmupIterationCount += batchIterationCount;

					if (timer.ElapsedTicks >= warmupDurationTickCount) break;

					lock (_cancelLock)
					{
						if (_cancelPending) return;
					}

					// Double the batch iteration count.
					batchIterationCount = batchIterationCount << 1;
					// Based on past performance, estimate how long the next batch will take given the new batch iteration count.
					long expectedBatchDurationTickCount = batchIterationCount * timer.ElapsedTicks / warmupIterationCount;
					// Clamp the next batch duration to be no larger than the target batch duration.
					long nextBatchDurationTickCount = Math.Min(expectedBatchDurationTickCount, targetWarmupBatchDurationTickCount);
					// Also clamp the next batch duration to be no larger than the remaining time for the warmup phase.
					nextBatchDurationTickCount = Math.Min(nextBatchDurationTickCount, warmupDurationTickCount - timer.ElapsedTicks);
					// Convert the estimated clamped batch duration back into an iteration count for the next batch.
					batchIterationCount = (int)(nextBatchDurationTickCount * warmupIterationCount / timer.ElapsedTicks);
					// Make the batch iteration count a multiple of 16.
					batchIterationCount = (batchIterationCount + 15L) & ~0xFL;
				}

				// Use the warmup phase performance and the target measurement batch duration to estimate the iteration count of the first measurement batch.
				batchIterationCount = (int)(targetMeasurementBatchDurationTickCount * warmupIterationCount / timer.ElapsedTicks);
				// Make the batch iteration count a multiple of 16.
				batchIterationCount = (batchIterationCount + 15L) & ~0xFL;

				timer.Reset();

				while (true)
				{
					timer.Start();
					operationLoop(batchIterationCount);
					timer.Stop();

					measurementIterationCount += batchIterationCount;

					if (timer.ElapsedTicks >= measurementDurationTickCount) break;

					lock (_cancelLock)
					{
						if (_cancelPending) return;
					}

					// Double the batch iteration count.
					batchIterationCount = batchIterationCount << 1;
					// Based on past performance, estimate how long the next batch will take given the new batch iteration count.
					long expectedBatchDurationTickCount = batchIterationCount * timer.ElapsedTicks / measurementIterationCount;
					// Clamp the next batch duration to be no larger than the target batch duration.
					long nextBatchDurationTickCount = Math.Min(expectedBatchDurationTickCount, targetMeasurementBatchDurationTickCount);
					// Also clamp the next batch duration to be no larger than the remaining time for the measurement phase.
					nextBatchDurationTickCount = Math.Min(nextBatchDurationTickCount, measurementDurationTickCount - timer.ElapsedTicks);
					// Convert the estimated clamped batch duration back into an iteration count for the next batch.
					batchIterationCount = (int)(nextBatchDurationTickCount * measurementIterationCount / timer.ElapsedTicks);
					// Make the batch iteration count a multiple of 16.
					batchIterationCount = (batchIterationCount + 15L) & ~0xFL;
				}

				_currentPerformanceMeasurement = (double)measurementIterationCount * ticksPerSecond / timer.ElapsedTicks;
			}
			finally
			{
				GCSettings.LatencyMode = originalGCLatencyMode;

				Thread.CurrentThread.Priority = originalThreadPriority;
				currentProcess.PriorityClass = originalPriorityClass;
				currentProcess.ProcessorAffinity = originalProcessorAffinity;
			}
		}

		#endregion

		#region Native Operations

		private void MeasurePerformance_UnityRandomUIntLessThan6(long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);

				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);

				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);

				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
				_generatedInt = UnityEngine.Random.Range(0, 6);
			}
		}

		private void MeasurePerformance_UnityRandomFloatClosed(long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;

				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;

				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;

				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
				_generatedFloat = UnityEngine.Random.value;
			}
		}

		private void MeasurePerformance_UnityRandomUnitVector3(long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;

				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;

				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;

				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
				_generatedFloat = UnityEngine.Random.onUnitSphere.x;
			}
		}

		private void MeasurePerformance_UnityRandomQuaternion(long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;

				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;

				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;

				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
				_generatedFloat = UnityEngine.Random.rotationUniform.x;
			}
		}

		private void MeasurePerformance_UnityRandomVector2WithinCircle(long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;

				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;

				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;

				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
				_generatedFloat = UnityEngine.Random.insideUnitCircle.x;
			}
		}

		private void MeasurePerformance_UnityRandomVector3WithinSphere(long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;

				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;

				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;

				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
				_generatedFloat = UnityEngine.Random.insideUnitSphere.x;
			}
		}

		private void MeasurePerformance_SystemRandomUInt31bit(System.Random random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();

				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();

				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();

				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();
				_generatedInt = random.Next();
			}
		}

		private void MeasurePerformance_SystemRandomUIntLessThan6(System.Random random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);

				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);

				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);

				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
				_generatedInt = random.Next(6);
			}
		}

		private void MeasurePerformance_SystemRandomDoubleHalfOpen(System.Random random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();

				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();

				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();

				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
				_generatedDouble = random.NextDouble();
			}
		}

		#endregion

		#region Generic Operations

		private void MeasurePerformance_UInt31bit(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;

				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;

				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;

				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
				_generatedUInt = random.Next32() & 0x7FFFFFFFU;
			}
		}

		private void MeasurePerformance_UInt32bit(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();

				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();

				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();

				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
				_generatedUInt = random.Next32();
			}
		}

		private void MeasurePerformance_UInt64bit(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);

				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);

				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);

				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
				random.Next64(out _generatedUInt, out _generatedUInt2);
#else
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();

				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();

				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();

				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
				_generatedULong = random.Next64();
#endif
			}
		}

		private void MeasurePerformance_UIntLessThan6(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			IRangeGenerator<uint> sequence = random.MakeRangeCOGenerator(6U);
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();

				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();

				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();

				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
				_generatedUInt = sequence.Next();
			}
		}

		private void MeasurePerformance_FloatOpen(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();

				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();

				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();

				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
				_generatedFloat = random.FloatOO();
			}
		}

		private void MeasurePerformance_FloatHalfOpen(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();

				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();

				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();

				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
				_generatedFloat = random.FloatCO();
			}
		}

		private void MeasurePerformance_FloatHalfClosed(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();

				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();

				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();

				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
				_generatedFloat = random.FloatOC();
			}
		}

		private void MeasurePerformance_FloatClosed(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();

				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();

				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();

				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
				_generatedFloat = random.FloatCC();
			}
		}

		private void MeasurePerformance_DoubleOpen(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();

				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();

				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();

				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
				_generatedDouble = random.DoubleOO();
			}
		}

		private void MeasurePerformance_DoubleHalfOpen(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();

				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();

				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();

				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
				_generatedDouble = random.DoubleCO();
			}
		}

		private void MeasurePerformance_DoubleHalfClosed(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();

				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();

				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();

				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
				_generatedDouble = random.DoubleOC();
			}
		}

		private void MeasurePerformance_DoubleClosed(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();

				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();

				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();

				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
				_generatedDouble = random.DoubleCC();
			}
		}

		private void MeasurePerformance_UnitVector2(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				Vector2 v;

				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;

				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;

				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;

				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
				random.UnitVector2(out v); _generatedFloat = v.x;
			}
		}

		private void MeasurePerformance_UnitVector3(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				Vector3 v;

				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;

				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;

				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;

				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
				random.UnitVector3(out v); _generatedFloat = v.x;
			}
		}

		private void MeasurePerformance_UnitVector4(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				Vector4 v;

				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;

				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;

				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;

				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
				random.UnitVector4(out v); _generatedFloat = v.x;
			}
		}

		private void MeasurePerformance_Quaternion(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				Quaternion q;

				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;

				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;

				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;

				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
				random.Rotation(out q); _generatedFloat = q.x;
			}
		}

		private void MeasurePerformance_Vector2WithinCircle(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				Vector2 v;

				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;

				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;

				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;

				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
				random.PointWithinCircle(out v); _generatedFloat = v.x;
			}
		}

		private void MeasurePerformance_Vector3WithinSphere(IRandom random, long iterations)
		{
			long unrolledIterations = iterations >> 4;
			for (int i = 0; i < unrolledIterations; ++i)
			{
				Vector3 v;

				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;

				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;

				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;

				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
				random.PointWithinSphere(out v); _generatedFloat = v.x;
			}
		}

#endregion
	}
}
