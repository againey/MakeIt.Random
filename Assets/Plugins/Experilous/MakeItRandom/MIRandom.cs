/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

using System;
using System.Reflection;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// <c>MIRandom</c> is a convenience class for creating instances of the standard random engine.
	/// It also provides static access to a shared instance that is automatically created on load.
	/// </summary>
	public static class MIRandom
	{
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
		[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		[UnityEditor.Callbacks.DidReloadScripts]
		private static void InitializeStaticFields()
		{
			FindStandardCreator();
			CreateShared();
		}
#endif

		private static Type _standardType = null;
		private static MethodInfo _standardCreator = null;
		private static object[] _standardCreatorParameters = null;

		private static void FindStandardCreator()
		{
#if MAKEITRANDOM_OPTIMIZED_FOR_64BIT
			_standardType = typeof(XorShift128Plus);
#else
			var xorShiftAddType = Assembly.GetExecutingAssembly().GetType("Experilous.MakeItRandom.XorShiftAdd", false, false);
			_standardType = (xorShiftAddType != null) ? xorShiftAddType : typeof(XorShift128Plus);
#endif
			_standardCreator = _standardType.GetMethod("CreateUninitialized", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
			if (_standardCreator == null) throw new InvalidOperationException(string.Format("Failed to find default creator function for the default random engine type {0}.", _standardType.Name));
			_standardCreatorParameters = new object[0];
		}

		private static IRandom _shared = null;

		/// <summary>
		/// A globally accessible shared instance of the standard random engine, automatically created on load.
		/// </summary>
		/// <remarks>
		/// Although this shared instance can easily be accessed and used from any thread, not just the main thread,
		/// the implementation of <see cref="IRandom"/> should not be presumed to be thread-safe, and most likely is not.
		/// Therefore it is recommended that access to this instance be guarded by mutex locks or other appropriate
		/// thread synchronization methods, or that separate instances be constructed such that each one is only ever
		/// access from a single thread.
		/// </remarks>
		public static IRandom shared
		{
			get
			{
#if !UNITY_5_2 && !UNITY_5_3_OR_NEWER
				if (_shared == null)
				{
					_shared = CreateStandard();
				}
#endif
				return _shared;
			}
			set
			{
				if (value == null)
				{
					throw new System.NullReferenceException();
				}
				_shared = value;
			}
		}

		private static void CreateShared()
		{
			_shared = CreateStandard();
		}

		/// <summary>
		/// Creates an instance of the standard random engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		public static IRandom CreateStandard()
		{
#if !UNITY_5_2 && !UNITY_5_3_OR_NEWER
			if (_standardCreator == null)
			{
				FindStandardCreator();
			}
#endif
			var random = (IRandom)_standardCreator.Invoke(null, _standardCreatorParameters);
			random.Seed();
			return random;
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		public static IRandom CreateStandard(int seed)
		{
#if !UNITY_5_2 && !UNITY_5_3_OR_NEWER
			if (_standardCreator == null)
			{
				FindStandardCreator();
			}
#endif
			var random = (IRandom)_standardCreator.Invoke(null, _standardCreatorParameters);
			random.Seed(seed);
			return random;
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		public static IRandom CreateStandard(params int[] seed)
		{
#if !UNITY_5_2 && !UNITY_5_3_OR_NEWER
			if (_standardCreator == null)
			{
				FindStandardCreator();
			}
#endif
			var random = (IRandom)_standardCreator.Invoke(null, _standardCreatorParameters);
			random.Seed(seed);
			return random;
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		public static IRandom CreateStandard(string seed)
		{
#if !UNITY_5_2 && !UNITY_5_3_OR_NEWER
			if (_standardCreator == null)
			{
				FindStandardCreator();
			}
#endif
			var random = (IRandom)_standardCreator.Invoke(null, _standardCreatorParameters);
			random.Seed(seed);
			return random;
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static IRandom CreateStandard(IBitGenerator bitGenerator)
		{
#if !UNITY_5_2 && !UNITY_5_3_OR_NEWER
			if (_standardCreator == null)
			{
				FindStandardCreator();
			}
#endif
			var random = (IRandom)_standardCreator.Invoke(null, _standardCreatorParameters);
			random.Seed(bitGenerator);
			return random;
		}
	}
}
