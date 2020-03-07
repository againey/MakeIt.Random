/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

namespace MakeIt.Random
{
	/// <summary>
	/// Interface for a basic engine for generating sequences of raw pseudo-random bits.
	/// </summary>
	public interface IRandom : IBitGenerator
	{
		/// <summary>
		/// Saves the pseudo-random engine's internal state as a byte array, which can be restored later.
		/// </summary>
		/// <returns>The internal state as a byte array.</returns>
		byte[] SaveState();

		/// <summary>
		/// Restores the pseudo-random engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		void RestoreState(byte[] stateArray);

		/// <summary>
		/// Reseed the pseudo-random engine with a transient value (such as system time).
		/// </summary>
		void Seed();

		/// <summary>
		/// Reseed the pseudo-random engine with the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		void Seed(int seed);

		/// <summary>
		/// Reseed the pseudo-random engine with the supplied array of integer values.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		void Seed(params int[] seed);

		/// <summary>
		/// Reseed the pseudo-random engine with the supplied string.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		void Seed(string seed);

		/// <summary>
		/// Reseed the psuedo-random engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		void Seed(IBitGenerator bitGenerator);

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and a transient value (such as system time).
		/// </summary>
		void MergeSeed();

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used, in conjuction with the current state, to indirectly determine the new state of the random engine.</param>
		void MergeSeed(int seed);

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and the supplied array of integer values.
		/// </summary>
		/// <param name="seed">An array of integer values used, in conjuction with the current state, to indirectly determine the new state of the random engine.</param>
		void MergeSeed(params int[] seed);

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and the supplied string.
		/// </summary>
		/// <param name="seed">An string value used, in conjuction with the current state, to indirectly determine the new state of the random engine.</param>
		void MergeSeed(string seed);

		/// <summary>
		/// Reseed the psuedo-random engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		void MergeSeed(IBitGenerator bitGenerator);

		/// <summary>
		/// The number of bits that the psuedo-random engine naturally produces in a single step.
		/// </summary>
		int stepBitCount { get; }

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		void Step();

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead"/>() is guaranteed to skip forward at least <code>2^skipAheadMagnitude</code> steps each time it is called.</para>
		/// <para>This property is guaranteed to be non-negative.  A value of 0 indicates that <see cref="SkipAhead"/>() is not guaranteed to do anything
		/// other than advance the state one single iteration.</para>
		/// </remarks>
		int skipAheadMagnitude { get; }

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipBack"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipBack"/>() is guaranteed to skip backward at least <code>2^skipBackMagnitude</code> steps each time it is called.</para>
		/// <para>This property may be negative.  A value of 0 indicates that <see cref="SkipBack"/>() is not guaranteed to do anything other than reverse
		/// the state one single iteration.  A negative value indicates that <see cref="SkipBack"/>() is not supported and will throw an exception.</para>
		/// </remarks>
		int skipBackMagnitude { get; }

		/// <summary>
		/// Advances the state forward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be advanced by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		void SkipAhead();

		/// <summary>
		/// Reverses the state backward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be reversed by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		void SkipBack();

		/// <summary>
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper"/>
		System.Random AsSystemRandom();
	}
}
