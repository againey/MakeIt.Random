/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeIt.Random
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
		/// <returns>A 32-bit unsigned integer full of generated bits.</returns>
		uint Next32();

		/// <summary>
		/// <summary>
		/// Get the next 64 bits of generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer full of generated bits.</returns>
		ulong Next64();
	}
}
