/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers according to non-uniform distributions.
	/// </summary>
	public static class RandomNonUniform
	{
		public static float NormalDistribution(this IRandom random, float mean, float standardDeviation)
		{
			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.NormalFloat.zigguratTable,
				Detail.Distributions.NormalFloat.F,
				Detail.Distributions.NormalFloat.SampleFallback);

			return sample * standardDeviation + mean;
		}

		public static float ExponentialDistribution(this IRandom random, float eventRate)
		{
			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.ExponentialFloat.zigguratTable,
				Detail.Distributions.ExponentialFloat.F,
				Detail.Distributions.ExponentialFloat.SampleFallback);

			return sample / eventRate;
		}
	}
}
