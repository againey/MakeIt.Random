/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using System.Text;

namespace Experilous.Randomization
{
	/// <summary>
	/// A static utility class to make it easier to seed PRNGs from a variety of common seed formats.
	/// </summary>
	public static class RandomSeedUtility
	{
		#region 32-bit Seeds

		/// <summary>
		/// Generate a 32-bit unsigned integer for use as a seed, based on the transient system tick count.
		/// </summary>
		/// <returns>A 32-bit unsigned integer based on a hash of the system tick count.</returns>
		public static uint Seed32()
		{
			return Seed32(Environment.TickCount);
		}

		/// <summary>
		/// Generate a 32-bit unsigned integer for use as a seed, based on the supplied integer value.
		/// </summary>
		/// <param name="data">The value used to produce the seed.</param>
		/// <returns>A 32-bit unsigned integer based on a hash of the supplied integer value.</returns>
		public static uint Seed32(int data)
		{
			return Seed32(BitConverter.GetBytes(data));
		}

		/// <summary>
		/// Generate a 32-bit unsigned integer for use as a seed, based on the supplied array of integer values.
		/// </summary>
		/// <param name="data">The array of values used to produce the seed.</param>
		/// <returns>A 32-bit unsigned integer based on a hash of the supplied array of integer values.</returns>
		public static uint Seed32(int[] data)
		{
			var byteData = new byte[data.Length * 4];
			Buffer.BlockCopy(data, 0, byteData, 0, byteData.Length);
			return Seed32(byteData);
		}

		/// <summary>
		/// Generate a 32-bit unsigned integer for use as a seed, based on the supplied string.
		/// </summary>
		/// <param name="data">The value used to produce the seed.</param>
		/// <returns>A 32-bit unsigned integer based on a hash of the supplied string.</returns>
		public static uint Seed32(string data)
		{
			return Seed32(new UTF8Encoding().GetBytes(data));
		}

		/// <summary>
		/// Generate a 32-bit unsigned integer for use as a seed, based on the supplied array of bytes.
		/// </summary>
		/// <param name="data">The array of values used to produce the seed.</param>
		/// <returns>A 32-bit unsigned integer based on a hash of the supplied array of bytes.</returns>
		/// <remarks>
		/// <para>Uses the <a href="http://www.isthe.com/chongo/tech/comp/fnv/">FNV-1a</a> hash function,
		/// developed by Glenn Fowler, Phong Vo, and Landon Curt Noll.</para>
		/// </remarks>
		public static uint Seed32(byte[] data)
		{
			uint h = 2166136261U;
			for (int i = 0; i < data.Length; ++i)
			{
				h = (h ^ data[i]) * 16777619U;
			}
			return h;
		}

		#endregion

		#region 64-bit Seeds

		/// <summary>
		/// Generate a 64-bit unsigned integer for use as a seed, based on the transient system tick count.
		/// </summary>
		/// <returns>A 64-bit unsigned integer based on a hash of the system tick count.</returns>
		public static ulong Seed64()
		{
			return Seed64(Environment.TickCount);
		}

		/// <summary>
		/// Generate a 64-bit unsigned integer for use as a seed, based on the supplied integer value.
		/// </summary>
		/// <param name="data">The value used to produce the seed.</param>
		/// <returns>A 64-bit unsigned integer based on a hash of the supplied integer value.</returns>
		public static ulong Seed64(int data)
		{
			return Seed64(BitConverter.GetBytes(data));
		}

		/// <summary>
		/// Generate a 64-bit unsigned integer for use as a seed, based on the supplied array of integer values.
		/// </summary>
		/// <param name="data">The array of values used to produce the seed.</param>
		/// <returns>A 64-bit unsigned integer based on a hash of the supplied array of integer values.</returns>
		public static ulong Seed64(int[] data)
		{
			var byteData = new byte[data.Length * 4];
			Buffer.BlockCopy(data, 0, byteData, 0, byteData.Length);
			return Seed64(byteData);
		}

		/// <summary>
		/// Generate a 64-bit unsigned integer for use as a seed, based on the supplied string.
		/// </summary>
		/// <param name="data">The value used to produce the seed.</param>
		/// <returns>A 64-bit unsigned integer based on a hash of the supplied string.</returns>
		public static ulong Seed64(string data)
		{
			return Seed64(new UTF8Encoding().GetBytes(data));
		}

		/// <summary>
		/// Generate a 64-bit unsigned integer for use as a seed, based on the supplied array of bytes.
		/// </summary>
		/// <param name="data">The array of values used to produce the seed.</param>
		/// <returns>A 64-bit unsigned integer based on a hash of the supplied array of bytes.</returns>
		/// <remarks>
		/// <para>Uses the <a href="http://www.isthe.com/chongo/tech/comp/fnv/">FNV-1a</a> hash function,
		/// developed by Glenn Fowler, Phong Vo, and Landon Curt Noll.</para>
		/// </remarks>
		public static ulong Seed64(byte[] data)
		{
			ulong h = 14695981039346656037UL;
			for (int i = 0; i < data.Length; ++i)
			{
				h = (h ^ data[i]) * 1099511628211UL;
			}
			return h;
		}

		#endregion
	}
}
