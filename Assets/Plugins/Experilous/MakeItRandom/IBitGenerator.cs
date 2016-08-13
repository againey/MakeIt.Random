/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
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
	}
}
