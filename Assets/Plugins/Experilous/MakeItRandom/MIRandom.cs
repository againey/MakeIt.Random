/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

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
		/// Therefore it is recommended that access to this instance is guarded by mutex locks or other appropriate
		/// thread synchronization methods, or that separate instances are constructed such that each one is only ever
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
		public static XorShift128Plus CreateStandard()
		{
			return XorShift128Plus.Create();
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		public static XorShift128Plus CreateStandard(int seed)
		{
			return XorShift128Plus.Create(seed);
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		public static XorShift128Plus CreateStandard(params int[] seed)
		{
			return XorShift128Plus.Create(seed);
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		public static XorShift128Plus CreateStandard(string seed)
		{
			return XorShift128Plus.Create(seed);
		}

		/// <summary>
		/// Creates an instance of the standard random engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the standard random engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static XorShift128Plus CreateStandard(IBitGenerator bitGenerator)
		{
			return XorShift128Plus.Create(bitGenerator);
		}
	}
}
