/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeIt.Random
{
	public static class RandomEngine
	{
		private static IRandomEngine _shared = null;

		public static IRandomEngine shared
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

		public static XorShift128Plus CreateStandard()
		{
			return XorShift128Plus.Create();
		}

		public static XorShift128Plus CreateStandard(int seed)
		{
			return XorShift128Plus.Create(seed);
		}

		public static XorShift128Plus CreateStandard(params int[] seed)
		{
			return XorShift128Plus.Create(seed);
		}

		public static XorShift128Plus CreateStandard(string seed)
		{
			return XorShift128Plus.Create(seed);
		}

		public static XorShift128Plus CreateStandard(RandomStateGenerator stateGenerator)
		{
			return XorShift128Plus.Create(stateGenerator);
		}

		public static XorShift128Plus CreateStandard(IRandomEngine seeder)
		{
			return XorShift128Plus.Create(seeder);
		}
	}
}
