using System.Linq;
using UnityEditor;
using System.Reflection;

namespace MakeIt.Random.BuildConfig
{
	public static class BuildConfig
	{
		[InitializeOnLoadMethod]
		static void UpdateDefines()
		{
			var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			bool hasNSubstitute = assemblies.Any((Assembly assembly) => assembly.GetName().Name == "NSubstitute");
#if MAKEIT_TEST_USE_NSUBSTITUTE
			bool usingNSubstitute = true;
#else
			bool usingNSubstitute = false;
#endif

			if (hasNSubstitute != usingNSubstitute)
			{
				var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';').ToList();
				defines.RemoveAll((string define) => define.Length == 0);

				if (usingNSubstitute)
				{
					defines.RemoveAll((string define) => define == "MAKEIT_TEST_USE_NSUBSTITUTE");
				}
				else
				{
					defines.Add("MAKEIT_TEST_USE_NSUBSTITUTE");
				}

				PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", defines));
			}
		}
	}
}
