/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// <c>MIRandom</c> is a convenience class for creating instances of the standard random engine.
	/// It also provides static access to a shared instance that is automatically created on load.
	/// </summary>
	public static class MIRandom
	{
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

#if UNITY_5_2 || UNITY_5_3_OR_NEWER
		[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
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
#if MAKEITRANDOM_OPTIMIZED_FOR_64BIT
			return XorShift128Plus.Create();
#else
			return XorShiftAdd.Create();
#endif
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		public static IRandom CreateStandard(int seed)
		{
#if MAKEITRANDOM_OPTIMIZED_FOR_64BIT || MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			return XorShift128Plus.Create(seed);
#else
			return XorShiftAdd.Create(seed);
#endif
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		public static IRandom CreateStandard(params int[] seed)
		{
#if MAKEITRANDOM_OPTIMIZED_FOR_64BIT || MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			return XorShift128Plus.Create(seed);
#else
			return XorShiftAdd.Create(seed);
#endif
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		public static IRandom CreateStandard(string seed)
		{
#if MAKEITRANDOM_OPTIMIZED_FOR_64BIT || MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			return XorShift128Plus.Create(seed);
#else
			return XorShiftAdd.Create(seed);
#endif
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
#if MAKEITRANDOM_OPTIMIZED_FOR_64BIT || MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			return XorShift128Plus.Create(bitGenerator);
#else
			return XorShiftAdd.Create(bitGenerator);
#endif
		}
	}
}
