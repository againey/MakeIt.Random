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
		public static IRandom shared
		{
			get
			{
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

		[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
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
