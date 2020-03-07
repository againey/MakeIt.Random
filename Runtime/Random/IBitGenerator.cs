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
	/// Interface for a generator of bits.
	/// </summary>
	/// <remarks>
	/// This interface offers basic access to a sequence of generated bits, either 32 or 64 at a time.
	/// </remarks>
	public interface IBitGenerator
	{
		/// <summary>
		/// Get the next 32 bits of generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of generated data.</returns>
		uint Next32();

		/// <summary>
		/// Get the next 64 bits of generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of generated data.</returns>
		ulong Next64();

		/// <summary>
		/// Get the next 64 bits of generated data as two 32-bit values.
		/// </summary>
		/// <param name="lower">The lower 32 bits of generated data.</param>
		/// <param name="upper">The upper 32 bits of generated data.</param>
		void Next64(out uint lower, out uint upper);
	}
}
