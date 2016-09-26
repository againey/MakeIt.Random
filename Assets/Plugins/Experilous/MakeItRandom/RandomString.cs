/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random strings with various character sets and patterns.
	/// </summary>
	public static class RandomString
	{
		#region UsingCharacters

		/// <summary>
		/// Generates a random string using the provided character set.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="characters">The allowable characters to select from when generating the string.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the provided character set.</returns>
		/// <seealso cref="String(IRandom, int, char[], char, float, bool, bool, bool)"/>
		/// <seealso cref="Characters(IRandom, char[], int, int, char[])"/>
		public static string String(this IRandom random, int length, char[] characters)
		{
			char[] buffer = new char[length];
			random.Characters(buffer, 0, length, characters);
			return new string(buffer);
		}

		/// <summary>
		/// Generates a random string using the provided character set, plus a separator character which can occur randomly with a specified probability.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="characters">The allowable characters to select from when generating the string.</param>
		/// <param name="separator">The separator character to be randomly inserted into the string according to the probability specified.</param>
		/// <param name="separatorProbability">The probability of any specific index in the string being a separator character.  Must be in the range [0, 1].</param>
		/// <param name="allowSeparatorAtEnd">Whether or not the last character to be generated is allowed to be a separator character.</param>
		/// <param name="allowSeparatorAtBegin">Whether or not the first character to be generated is allowed to be a separator character.</param>
		/// <param name="forceSeparatorAtBegin">Whether or not the first character to be generated must be a separator character.  Ignored if <paramref name="allowSeparatorAtBegin"/> is false.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the provided character set.</returns>
		/// <remarks><para>It is guaranteed that two separator characters will never be generated one immediately after the other, regardless of the separator probability.</para>
		/// <para>A separator probability of 0 will guarantee that there are no separators at all, while a probability of 1 will guarantee
		/// that every second character is a separtor character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-separator characters, rather than
		/// being strictly limited to one non-separator character at a time.</para></remarks>
		/// <seealso cref="String(IRandom, int, char[])"/>
		/// <seealso cref="Characters(IRandom, char[], int, int, char[], char, float, bool, bool, bool)"/>
		public static string String(this IRandom random, int length, char[] characters, char separator, float separatorProbability, bool allowSeparatorAtEnd = false, bool allowSeparatorAtBegin = false, bool forceSeparatorAtBegin = false)
		{
			char[] buffer = new char[length];
			random.Characters(buffer, 0, length, characters, separator, separatorProbability);
			return new string(buffer);
		}

		/// <summary>
		/// Fills a portion of a provided character buffer with random characters using the provided character set.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="buffer">The character buffer to which random charactors will be written.</param>
		/// <param name="start">The start index of the character buffer where random character generation will begin.  Must be less than or equal to <paramref name="buffer"/>.Length - <paramref name="length"/>.</param>
		/// <param name="length">The number of random characters to be generated.  Must be less than or equal to <c><paramref name="buffer"/>.Length - <paramref name="start"/></c>.</param>
		/// <param name="characters">The allowable characters to select from when generating the string.</param>
		/// <seealso cref="Characters(IRandom, char[], int, int, char[], char, float, bool, bool, bool)"/>
		/// <seealso cref="String(IRandom, int, char[])"/>
		public static void Characters(this IRandom random, char[] buffer, int start, int length, char[] characters)
		{
			if (length <= 0) return;

			int end = start + length;
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(characters.Length);
			for (int i = start; i < end; ++i)
			{
				buffer[i] = characters[generator.Next()];
			}
		}

		/// <summary>
		/// Fills a portion of a provided character buffer with random characters using the provided character set.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="buffer">The character buffer to which random charactors will be written.</param>
		/// <param name="start">The start index of the character buffer where random character generation will begin.  Must be less than or equal to <paramref name="buffer"/>.Length - <paramref name="length"/>.</param>
		/// <param name="length">The number of random characters to be generated.  Must be less than or equal to <c><paramref name="buffer"/>.Length - <paramref name="start"/></c>.</param>
		/// <param name="characters">The allowable characters to select from when generating the string.</param>
		/// <param name="separator">The separator character to be randomly inserted into the string according to the probability specified.</param>
		/// <param name="separatorProbability">The probability of any specific index in the string being a separator character.  Must be in the range [0, 1].</param>
		/// <param name="allowSeparatorAtEnd">Whether or not the last character to be generated is allowed to be a separator character.</param>
		/// <param name="allowSeparatorAtBegin">Whether or not the first character to be generated is allowed to be a separator character.</param>
		/// <param name="forceSeparatorAtBegin">Whether or not the first character to be generated must be a separator character.  Ignored if <paramref name="allowSeparatorAtBegin"/> is false.</param>
		/// <remarks><para>It is guaranteed that two separator characters will never be generated one immediately after the other within the
		/// character sequence, regardless of the separator probability.</para>
		/// <para>A separator probability of 0 will guarantee that there are no separators at all, while a probability of 1 will guarantee
		/// that every second character is a separtor character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-separator characters, rather than
		/// being strictly limited to one non-separator character at a time.</para></remarks>
		/// <seealso cref="Characters(IRandom, char[], int, int, char[])"/>
		/// <seealso cref="String(IRandom, int, char[], char, float, bool, bool, bool)"/>
		public static void Characters(this IRandom random, char[] buffer, int start, int length, char[] characters, char separator, float separatorProbability, bool allowSeparatorAtEnd = false, bool allowSeparatorAtBegin = false, bool forceSeparatorAtBegin = false)
		{
			if (length <= 0) return;

			bool allowSeparatorState = allowSeparatorAtBegin;
			bool forceSeparatorState = forceSeparatorAtBegin;

			int end = start + length;
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(characters.Length);
			for (int i = start; i < end; ++i)
			{
				if (allowSeparatorState && (allowSeparatorAtEnd || i + 1 < end))
				{
					if (forceSeparatorState || random.Probability(separatorProbability))
					{
						buffer[i] = separator;
						allowSeparatorState = false;
						forceSeparatorState = false;
					}
					else
					{
						buffer[i] = characters[generator.Next()];
					}
				}
				else
				{
					buffer[i] = characters[generator.Next()];
					allowSeparatorState = true;
					forceSeparatorState = random.Probability(separatorProbability);
				}
			}
		}

		#endregion

		/// <summary>
		/// Indicates the type of letter casing to use when generating random strings, for character sets where casing is relevant.
		/// </summary>
		public enum Casing
		{
			/// <summary>
			/// Restrict all generated letters to lower case.
			/// </summary>
			Lower,
			/// <summary>
			/// Restrict all generated letters to upper case.
			/// </summary>
			Upper,
			/// <summary>
			/// Allow any letter case for any letter generated.
			/// </summary>
			Any,
		}

		#region Binary

		/// <summary>
		/// Generates a random string representing a sequence of binary digits.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string, with each character being a uniformly random selection binary digits { 0, 1 }.</returns>
		public static string BinaryString(this IRandom random, int length)
		{
			if (length <= 0) return "";

			char[] buffer = new char[length];
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(2);

			for (int i = 0; i < length; ++i)
			{
				buffer[i] = (char)('0' + generator.Next());
			}

			return new string(buffer);
		}

		#endregion

		#region Octal

		/// <summary>
		/// Generates a random string representing a sequence of octal digits.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string, with each character being a uniformly random selection octal digits { 0, 1, 2, 3, 4, 5, 6, 7 }.</returns>
		public static string OctalString(this IRandom random, int length)
		{
			if (length <= 0) return "";

			char[] buffer = new char[length];
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(8);

			for (int i = 0; i < length; ++i)
			{
				buffer[i] = (char)('0' + generator.Next());
			}

			return new string(buffer);
		}

		#endregion

		#region Decimal

		/// <summary>
		/// Generates a random string representing a sequence of decimal digits.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string, with each character being a uniformly random selection decimal digits { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.</returns>
		public static string DecimalString(this IRandom random, int length)
		{
			if (length <= 0) return "";

			char[] buffer = new char[length];
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(10);

			for (int i = 0; i < length; ++i)
			{
				buffer[i] = (char)('0' + generator.Next());
			}

			return new string(buffer);
		}

		#endregion

		#region Hexadecimal

		private static char[] _lowerHexadecimalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
		};

		private static char[] _upperHexadecimalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',
		};

		private static char[] GetHexadecimalCharacters(Casing casing)
		{
			switch (casing)
			{
				case Casing.Lower: return _lowerHexadecimalCharacters;
				case Casing.Upper: return _upperHexadecimalCharacters;
				case Casing.Any: throw new System.NotSupportedException("Hexadecimal strings cannot mix letter casing and must use only lowercase or only upper case letters.");
				default: throw new System.NotImplementedException();
			}
		}

		/// <summary>
		/// Generates a random string representing a sequence of decimal digits.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string, with each character being a uniformly random selection decimal digits { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A, B, C, D, E, F }.</returns>
		/// <remarks>Upper case letters are used for the digits A through F.  If you want to control
		/// the letter casing used, see <see cref="HexadecimalString(IRandom, int, Casing)"/>.</remarks>
		/// <seealso cref="Casing"/>
		/// <seealso cref="HexadecimalString(IRandom, int, Casing)"/>
		public static string HexadecimalString(this IRandom random, int length)
		{
			if (length <= 0) return "";

			char[] buffer = new char[length];
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(16);

			for (int i = 0; i < length; ++i)
			{
				buffer[i] = _upperHexadecimalCharacters[generator.Next()];
			}

			return new string(buffer);
		}

		/// <summary>
		/// Generates a random string representing a sequence of decimal digits.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="casing">The letter casing used for digits A through F.  Must be either upper or lower casing.</param>
		/// <returns>A random string, with each character being a uniformly random selection decimal digits { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A, B, C, D, E, F }.</returns>
		/// <seealso cref="Casing"/>
		/// <seealso cref="HexadecimalString(IRandom, int)"/>
		public static string HexadecimalString(this IRandom random, int length, Casing casing)
		{
			if (length <= 0) return "";

			char[] characters = GetHexadecimalCharacters(casing);
			char[] buffer = new char[length];
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(characters.Length);

			for (int i = 0; i < length; ++i)
			{
				buffer[i] = characters[generator.Next()];
			}

			return new string(buffer);
		}

		#endregion

		#region Base64

		/// <summary>
		/// Indicates the additional two characters to use when generating a Base64 string, in addition to the standard 62 characters
		/// consisting of the 10 decimal digits and both the 26 lower case and 26 upper case letters of the English alphabet.
		/// </summary>
		public enum Base64CharacterPairs
		{
			/// <summary>
			/// Use + and / as the final two Base64 characters.
			/// </summary>
			PlusSlash,
			/// <summary>
			/// Use - and _ as the final two Base64 characters.
			/// </summary>
			HyphenUnderscore,
			/// <summary>
			/// Use . and _ as the final two Base64 characters.
			/// </summary>
			PeriodUnderscore,
			/// <summary>
			/// Use . and - as the final two Base64 characters.
			/// </summary>
			PeriodHyphen,
			/// <summary>
			/// Use _ and : as the final two Base64 characters.
			/// </summary>
			UnderscoreColon,
			/// <summary>
			/// Use _ and - as the final two Base64 characters.
			/// </summary>
			UnderscoreHyphen,
			/// <summary>
			/// Use ! and - as the final two Base64 characters.
			/// </summary>
			BangHyphen,
			/// <summary>
			/// Use ~ and - as the final two Base64 characters.
			/// </summary>
			TildeHyphen,
		}

		private static char[] _base64PlusSlashCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/',
		};

		private static char[] _base64HyphenUnderscoreCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_',
		};

		private static char[] _base64PeriodUnderscoreCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '_',
		};

		private static char[] _base64PeriodHyphenCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '-',
		};

		private static char[] _base64UnderscoreColonCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/',
		};

		private static char[] _base64UnderscoreHyphenCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '_', '-',
		};

		private static char[] _base64BangHyphenCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '!', '-',
		};

		private static char[] _base64TildeHyphenCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '~', '-',
		};

		/// <summary>
		/// Generates a random string representing a sequence of Base64 digits.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the default Base64 character set.</returns>
		/// <seealso cref="Base64String(IRandom, int, Base64CharacterPairs)"/>
		public static string Base64String(this IRandom random, int length)
		{
			return random.Base64String(length, _base64PlusSlashCharacters);
		}

		/// <summary>
		/// Generates a random string representing a sequence of Base64 digits.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="characterPairs">The specific Base64 character set used to generate the random string.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the Base64 character set specified.</returns>
		/// <seealso cref="Base64String(IRandom, int)"/>
		/// <seealso cref="Base64CharacterPairs"/>
		public static string Base64String(this IRandom random, int length, Base64CharacterPairs characterPairs)
		{
			switch (characterPairs)
			{
				case Base64CharacterPairs.PlusSlash: return random.Base64String(length, _base64PlusSlashCharacters);
				case Base64CharacterPairs.HyphenUnderscore: return random.Base64String(length, _base64HyphenUnderscoreCharacters);
				case Base64CharacterPairs.PeriodUnderscore: return random.Base64String(length, _base64PeriodUnderscoreCharacters);
				case Base64CharacterPairs.PeriodHyphen: return random.Base64String(length, _base64PeriodHyphenCharacters);
				case Base64CharacterPairs.UnderscoreColon: return random.Base64String(length, _base64UnderscoreColonCharacters);
				case Base64CharacterPairs.UnderscoreHyphen: return random.Base64String(length, _base64UnderscoreHyphenCharacters);
				case Base64CharacterPairs.BangHyphen: return random.Base64String(length, _base64BangHyphenCharacters);
				case Base64CharacterPairs.TildeHyphen: return random.Base64String(length, _base64TildeHyphenCharacters);
				default: throw new System.NotImplementedException();
			}
		}

		private static string Base64String(this IRandom random, int length, char[] characters)
		{
			if (length <= 0) return "";

			char[] buffer = new char[length];
			IRangeGenerator<int> generator = random.MakeRangeCOGenerator(64);

			for (int i = 0; i < length; ++i)
			{
				buffer[i] = characters[generator.Next()];
			}

			return new string(buffer);
		}

		#endregion

		#region Alpha-Numeric

		private static char[] _alphaNumericCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		};

		private static char[] _lowerAlphaNumericCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		};

		private static char[] _upperAlphaNumericCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
		};

		private static char[] GetAlphaNumericCharacters(Casing casing)
		{
			switch (casing)
			{
				case Casing.Lower: return _lowerAlphaNumericCharacters;
				case Casing.Upper: return _upperAlphaNumericCharacters;
				case Casing.Any: return _alphaNumericCharacters;
				default: throw new System.NotImplementedException();
			}
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet, both upper and lower case.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of decimal digits and English letters, both upper and lower case.</returns>
		/// <seealso cref="AlphaNumericString(IRandom, int, char, float)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, Casing)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, Casing, char, float)"/>
		public static string AlphaNumericString(this IRandom random, int length)
		{
			return random.String(length, _alphaNumericCharacters);
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet, both upper and lower case, plus a separator character which can occur randomly with a specified probability.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="separator">The separator character to be randomly inserted into the string according to the probability specified.</param>
		/// <param name="separatorProbability">The probability of any specific index in the string being a separator character.  Must be in the range [0, 1].</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of decimal digits and English letters, both upper and lower case.</returns>
		/// <remarks><para>The generated string is guaranteed to not start with the separator character, and it is further guaranteed that
		/// two separator characters will never be generated one immediately after the other, regardless of the separator probability.</para>
		/// <para>A separator probability of 0 will guarantee that there are no separators at all, while a probability of 1 will guarantee
		/// that every second character is a separtor character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-separator characters, rather than
		/// being strictly limited to one non-separator character at a time.</para></remarks>
		/// <seealso cref="AlphaNumericString(IRandom, int)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, Casing)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, Casing, char, float)"/>
		public static string AlphaNumericString(this IRandom random, int length, char separator, float separatorProbability)
		{
			return random.String(length, _alphaNumericCharacters, separator, separatorProbability);
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="casing">The letter casing used for English letters.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of decimal digits and English letters.</returns>
		/// <seealso cref="AlphaNumericString(IRandom, int)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, char, float)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, Casing, char, float)"/>
		public static string AlphaNumericString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphaNumericCharacters(casing));
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet, plus a separator character which can occur randomly with a specified probability.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="casing">The letter casing used for English letters.</param>
		/// <param name="separator">The separator character to be randomly inserted into the string according to the probability specified.</param>
		/// <param name="separatorProbability">The probability of any specific index in the string being a separator character.  Must be in the range [0, 1].</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of decimal digits and English letters.</returns>
		/// <remarks><para>The generated string is guaranteed to not start with the separator character, and it is further guaranteed that
		/// two separator characters will never be generated one immediately after the other, regardless of the separator probability.</para>
		/// <para>A separator probability of 0 will guarantee that there are no separators at all, while a probability of 1 will guarantee
		/// that every second character is a separtor character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-separator characters, rather than
		/// being strictly limited to one non-separator character at a time.</para></remarks>
		/// <seealso cref="AlphaNumericString(IRandom, int)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, char, float)"/>
		/// <seealso cref="AlphaNumericString(IRandom, int, Casing)"/>
		public static string AlphaNumericString(this IRandom random, int length, Casing casing, char separator, float separatorProbability)
		{
			return random.String(length, GetAlphaNumericCharacters(casing), separator, separatorProbability);
		}

		#endregion

		#region Alphabetic

		private static char[] _alphabeticCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		};

		private static char[] _lowerAlphabeticCharacters =
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		};

		private static char[] _upperAlphabeticCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
		};

		private static char[] GetAlphabeticCharacters(Casing casing)
		{
			switch (casing)
			{
				case Casing.Lower: return _lowerAlphabeticCharacters;
				case Casing.Upper: return _upperAlphabeticCharacters;
				case Casing.Any: return _alphabeticCharacters;
				default: throw new System.NotImplementedException();
			}
		}

		/// <summary>
		/// Generates a random string using only letters from the English alphabet, both upper and lower case.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of English letters, both upper and lower case.</returns>
		/// <seealso cref="AlphabeticString(IRandom, int, char, float)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, Casing)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, Casing, char, float)"/>
		public static string AlphabeticString(this IRandom random, int length)
		{
			return random.String(length, _alphabeticCharacters);
		}

		/// <summary>
		/// Generates a random string using only letters from the English alphabet, both upper and lower case, plus a separator character which can occur randomly with a specified probability.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="separator">The separator character to be randomly inserted into the string according to the probability specified.</param>
		/// <param name="separatorProbability">The probability of any specific index in the string being a separator character.  Must be in the range [0, 1].</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of English letters, both upper and lower case.</returns>
		/// <remarks><para>The generated string is guaranteed to not start with the separator character, and it is further guaranteed that
		/// two separator characters will never be generated one immediately after the other, regardless of the separator probability.</para>
		/// <para>A separator probability of 0 will guarantee that there are no separators at all, while a probability of 1 will guarantee
		/// that every second character is a separtor character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-separator characters, rather than
		/// being strictly limited to one non-separator character at a time.</para></remarks>
		/// <seealso cref="AlphabeticString(IRandom, int)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, Casing)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, Casing, char, float)"/>
		public static string AlphabeticString(this IRandom random, int length, char separator, float separatorProbability)
		{
			return random.String(length, _alphabeticCharacters, separator, separatorProbability);
		}

		/// <summary>
		/// Generates a random string using only letters from the English alphabet.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="casing">The letter casing used for English letters.</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of English letters.</returns>
		/// <seealso cref="AlphabeticString(IRandom, int)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, char, float)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, Casing, char, float)"/>
		public static string AlphabeticString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphabeticCharacters(casing));
		}

		/// <summary>
		/// Generates a random string using only and letters from the English alphabet, plus a separator character which can occur randomly with a specified probability.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="casing">The letter casing used for English letters.</param>
		/// <param name="separator">The separator character to be randomly inserted into the string according to the probability specified.</param>
		/// <param name="separatorProbability">The probability of any specific index in the string being a separator character.  Must be in the range [0, 1].</param>
		/// <returns>A random string, with each character being a uniformly random selection from the set of English letters.</returns>
		/// <remarks><para>The generated string is guaranteed to not start with the separator character, and it is further guaranteed that
		/// two separator characters will never be generated one immediately after the other, regardless of the separator probability.</para>
		/// <para>A separator probability of 0 will guarantee that there are no separators at all, while a probability of 1 will guarantee
		/// that every second character is a separtor character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-separator characters, rather than
		/// being strictly limited to one non-separator character at a time.</para></remarks>
		/// <seealso cref="AlphabeticString(IRandom, int)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, char, float)"/>
		/// <seealso cref="AlphabeticString(IRandom, int, Casing)"/>
		public static string AlphabeticString(this IRandom random, int length, Casing casing, char separator, float separatorProbability)
		{
			return random.String(length, GetAlphabeticCharacters(casing), separator, separatorProbability);
		}

		#endregion

		#region Identifier

		private static char[] GetIdentifierFirstCharacters(Casing casing)
		{
			switch (casing)
			{
				case Casing.Lower: return _lowerAlphabeticCharacters;
				case Casing.Upper: return _upperAlphabeticCharacters;
				case Casing.Any: return _alphabeticCharacters;
				default: throw new System.NotImplementedException();
			}
		}

		private static char[] GetIdentifierCharacters(Casing casing)
		{
			switch (casing)
			{
				case Casing.Lower: return _lowerAlphaNumericCharacters;
				case Casing.Upper: return _upperAlphaNumericCharacters;
				case Casing.Any: return _alphaNumericCharacters;
				default: throw new System.NotImplementedException();
			}
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet, both upper and lower case, with the restriction that the first character cannot be a decimal digit.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <returns>A random string which conforms to typical rules of valid identifier names in many programming languages.</returns>
		/// <seealso cref="Identifier(IRandom, int, float)"/>
		/// <seealso cref="Identifier(IRandom, int, Casing)"/>
		/// <seealso cref="Identifier(IRandom, int, Casing, float)"/>
		public static string Identifier(this IRandom random, int length)
		{
			if (length <= 0) return "";
			char[] buffer = new char[length];
			buffer[0] = _alphabeticCharacters.RandomElement(random);
			random.Characters(buffer, 1, length - 1, _alphaNumericCharacters);
			return new string(buffer);
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet, both upper and lower case, plus the underscore character which can occur randomly with a specified probability, with the restriction that the first character cannot be a decimal digit.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="underscoreProbability">The probability of any specific index in the string being a underscore character.  Must be in the range [0, 1].</param>
		/// <returns>A random string which conforms to typical rules of valid identifier names in many programming languages.</returns>
		/// <remarks><para>It is guaranteed that except at the very beginning of the string, two underscore characters will never be generated
		/// one immediately after the other, regardless of the underscore probability.</para>
		/// <para>An underscore probability of 0 will guarantee that there are no underscore at all, while a probability of 1 will guarantee
		/// that every second character is a underscore character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-underscore characters, rather than
		/// being strictly limited to one non-underscore character at a time.</para></remarks>
		/// <seealso cref="Identifier(IRandom, int)"/>
		/// <seealso cref="Identifier(IRandom, int, Casing)"/>
		/// <seealso cref="Identifier(IRandom, int, Casing, float)"/>
		public static string Identifier(this IRandom random, int length, float underscoreProbability)
		{
			if (length <= 0) return "";
			char[] buffer = new char[length];
			if (random.Probability(underscoreProbability))
			{
				buffer[0] = '_';
			}
			else
			{
				buffer[0] = _alphabeticCharacters.RandomElement(random);
			}
			random.Characters(buffer, 1, length - 1, _alphaNumericCharacters, '_', underscoreProbability, true, true, false);
			return new string(buffer);
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet, with the restriction that the first character cannot be a decimal digit.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="casing">The letter casing used for English letters.</param>
		/// <returns>A random string which conforms to typical rules of valid identifier names in many programming languages.</returns>
		/// <seealso cref="Identifier(IRandom, int)"/>
		/// <seealso cref="Identifier(IRandom, int, float)"/>
		/// <seealso cref="Identifier(IRandom, int, Casing, float)"/>
		public static string Identifier(this IRandom random, int length, Casing casing)
		{
			if (length <= 0) return "";
			char[] buffer = new char[length];
			buffer[0] = GetAlphabeticCharacters(casing).RandomElement(random);
			random.Characters(buffer, 1, length - 1, GetAlphaNumericCharacters(casing));
			return new string(buffer);
		}

		/// <summary>
		/// Generates a random string using only decimal digits and letters from the English alphabet, plus the underscore character which can occur randomly with a specified probability, with the restriction that the first character cannot be a decimal digit.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of the string to be generated.</param>
		/// <param name="casing">The letter casing used for English letters.</param>
		/// <param name="underscoreProbability">The probability of any specific index in the string being a underscore character.  Must be in the range [0, 1].</param>
		/// <returns>A random string which conforms to typical rules of valid identifier names in many programming languages.</returns>
		/// <remarks><para>It is guaranteed that except at the very beginning of the string, two underscore characters will never be generated
		/// one immediately after the other, regardless of the underscore probability.</para>
		/// <para>An underscore probability of 0 will guarantee that there are no underscore at all, while a probability of 1 will guarantee
		/// that every second character is a underscore character.  Probabilities less than 1 but greater that 0.5 will be biased toward the
		/// same every-other character outcome, but will have a chance of generating longer sequences of non-underscore characters, rather than
		/// being strictly limited to one non-underscore character at a time.</para></remarks>
		/// <seealso cref="Identifier(IRandom, int)"/>
		/// <seealso cref="Identifier(IRandom, int, float)"/>
		/// <seealso cref="Identifier(IRandom, int, Casing)"/>
		public static string Identifier(this IRandom random, int length, Casing casing, float underscoreProbability)
		{
			if (length <= 0) return "";
			char[] buffer = new char[length];
			if (random.Probability(underscoreProbability))
			{
				buffer[0] = '_';
			}
			else
			{
				buffer[0] = GetAlphabeticCharacters(casing).RandomElement(random);
			}
			random.Characters(buffer, 1, length - 1, GetAlphaNumericCharacters(casing), '_', underscoreProbability, true, true, false);
			return new string(buffer);
		}

		#endregion
	}
}
