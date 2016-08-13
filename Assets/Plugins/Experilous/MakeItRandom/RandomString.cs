/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	public static class RandomString
	{
		#region UsingCharacters

		public static string String(this IRandom random, int length, char[] characters)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = characters.RandomElement(random);
			}
			return new string(buffer);
		}

		public static string String(this IRandom random, int length, char[] characters, char otherCharacter, float otherFrequency)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = random.Probability(otherFrequency) ? otherCharacter : characters.RandomElement(random);
			}
			return new string(buffer);
		}

		public static string String(this IRandom random, int length, char[] characters, char otherCharacter, int otherCountPerChunk, int averageChunkLength)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = random.Probability(otherCountPerChunk, averageChunkLength) ? otherCharacter : characters.RandomElement(random);
			}
			return new string(buffer);
		}

		#endregion

		public enum Casing
		{
			Lower,
			Upper,
		}

		#region Binary

		private static char[] _binaryCharacters =
		{
			'0', '1',
		};

		public static string BinaryString(this IRandom random, int length)
		{
			return random.String(length, _binaryCharacters);
		}

		#endregion

		#region Octal

		private static char[] _octalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7',
		};

		public static string OctalString(this IRandom random, int length)
		{
			return random.String(length, _octalCharacters);
		}

		#endregion

		#region Decimal

		private static char[] _decimalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		};

		public static string DecimalString(this IRandom random, int length)
		{
			return random.String(length, _decimalCharacters);
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
				default: throw new System.NotImplementedException();
			}
		}

		public static string HexadecimalString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetHexadecimalCharacters(casing));
		}

		#endregion

		#region Base64

		public enum Base64CharacterPairs
		{
			PlusSlash,
			HyphenUnderscore,
			PeriodUnderscore,
			PeriodHyphen,
			UnderscoreColon,
			UnderscoreHyphen,
			BangHyphen,
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

		public static string Base64String(this IRandom random, int length)
		{
			return random.String(length, _base64PlusSlashCharacters);
		}

		public static string Base64String(this IRandom random, int length, Base64CharacterPairs characterPairs)
		{
			switch (characterPairs)
			{
				case Base64CharacterPairs.PlusSlash: return random.String(length, _base64PlusSlashCharacters);
				case Base64CharacterPairs.HyphenUnderscore: return random.String(length, _base64HyphenUnderscoreCharacters);
				case Base64CharacterPairs.PeriodUnderscore: return random.String(length, _base64PeriodUnderscoreCharacters);
				case Base64CharacterPairs.PeriodHyphen: return random.String(length, _base64PeriodHyphenCharacters);
				case Base64CharacterPairs.UnderscoreColon: return random.String(length, _base64UnderscoreColonCharacters);
				case Base64CharacterPairs.UnderscoreHyphen: return random.String(length, _base64UnderscoreHyphenCharacters);
				case Base64CharacterPairs.BangHyphen: return random.String(length, _base64BangHyphenCharacters);
				case Base64CharacterPairs.TildeHyphen: return random.String(length, _base64TildeHyphenCharacters);
				default: throw new System.NotImplementedException();
			}
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
				default: throw new System.NotImplementedException();
			}
		}

		public static string AlphaNumericString(this IRandom random, int length)
		{
			return random.String(length, _alphaNumericCharacters);
		}

		public static string AlphaNumericWithSpacesString(this IRandom random, int length)
		{
			return random.String(length, _alphaNumericCharacters, ' ', 1, 62);
		}

		public static string AlphaNumericWithSpacesString(this IRandom random, int length, float spaceFrequency)
		{
			return random.String(length, _alphaNumericCharacters, ' ', spaceFrequency);
		}

		public static string AlphaNumericWithSpacesString(this IRandom random, int length, int spaceCountPerChunk, int averageChunkLength)
		{
			return random.String(length, _alphaNumericCharacters, ' ', spaceCountPerChunk, averageChunkLength);
		}

		public static string AlphaNumericString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphaNumericCharacters(casing));
		}

		public static string AlphaNumericWithSpacesString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphaNumericCharacters(casing), ' ', 1, 62);
		}

		public static string AlphaNumericWithSpacesString(this IRandom random, int length, float spaceFrequency, Casing casing)
		{
			return random.String(length, GetAlphaNumericCharacters(casing), ' ', spaceFrequency);
		}

		public static string AlphaNumericWithSpacesString(this IRandom random, int length, int spaceCountPerChunk, int averageChunkLength, Casing casing)
		{
			return random.String(length, GetAlphaNumericCharacters(casing), ' ', spaceCountPerChunk, averageChunkLength);
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
				default: throw new System.NotImplementedException();
			}
		}

		public static string AlphabeticString(this IRandom random, int length)
		{
			return random.String(length, _alphabeticCharacters);
		}

		public static string AlphabeticWithSpacesString(this IRandom random, int length)
		{
			return random.String(length, _alphabeticCharacters, ' ', 1, 52);
		}

		public static string AlphabeticWithSpacesString(this IRandom random, int length, float spaceFrequency)
		{
			return random.String(length, _alphabeticCharacters, ' ', spaceFrequency);
		}

		public static string AlphabeticWithSpacesString(this IRandom random, int length, int spaceCountPerChunk, int averageChunkLength)
		{
			return random.String(length, _alphabeticCharacters, ' ', spaceCountPerChunk, averageChunkLength);
		}

		public static string AlphabeticString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphabeticCharacters(casing));
		}

		public static string AlphabeticWithSpacesString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphabeticCharacters(casing), ' ', 1, 52);
		}

		public static string AlphabeticWithSpacesString(this IRandom random, int length, float spaceFrequency, Casing casing)
		{
			return random.String(length, GetAlphabeticCharacters(casing), ' ', spaceFrequency);
		}

		public static string AlphabeticWithSpacesString(this IRandom random, int length, int spaceCountPerChunk, int averageChunkLength, Casing casing)
		{
			return random.String(length, GetAlphabeticCharacters(casing), ' ', spaceCountPerChunk, averageChunkLength);
		}

		#endregion

		#region Identifier

		private static char[] GetIdentifierFirstCharacters(Casing casing)
		{
			switch (casing)
			{
				case Casing.Lower: return _lowerAlphabeticCharacters;
				case Casing.Upper: return _upperAlphabeticCharacters;
				default: throw new System.NotImplementedException();
			}
		}

		private static char[] GetIdentifierCharacters(Casing casing)
		{
			switch (casing)
			{
				case Casing.Lower: return _lowerAlphaNumericCharacters;
				case Casing.Upper: return _upperAlphaNumericCharacters;
				default: throw new System.NotImplementedException();
			}
		}

		public static string Identifier(this IRandom random, int length)
		{
			if (length <= 0) return "";
			return _alphabeticCharacters.RandomElement(random) + random.String(length - 1, _alphaNumericCharacters);
		}

		public static string IdentifierWithUnderscores(this IRandom random, int length)
		{
			if (length <= 0) return "";
			return random.String(1, _alphabeticCharacters, '_', 1, 52) + random.String(length, _alphaNumericCharacters, '_', 1, 62);
		}

		public static string IdentifierWithUnderscores(this IRandom random, int length, float underscoreFrequency)
		{
			if (length <= 0) return "";
			return random.String(1, _alphabeticCharacters, '_', underscoreFrequency) + random.String(length, _alphaNumericCharacters, '_', underscoreFrequency);
		}

		public static string IdentifierWithUnderscores(this IRandom random, int length, int underscoreCountPerChunk, int averageChunkLength)
		{
			if (length <= 0) return "";
			return random.String(1, _alphabeticCharacters, '_', underscoreCountPerChunk, averageChunkLength) + random.String(length, _alphaNumericCharacters, '_', underscoreCountPerChunk, averageChunkLength);
		}

		public static string Identifier(this IRandom random, int length, Casing casing)
		{
			if (length <= 0) return "";
			return GetIdentifierFirstCharacters(casing).RandomElement(random) + random.String(length - 1, GetIdentifierCharacters(casing));
		}

		public static string IdentifierWithUnderscores(this IRandom random, int length, Casing casing)
		{
			if (length <= 0) return "";
			return random.String(1, GetAlphabeticCharacters(casing), '_', 1, 52) + random.String(length, GetAlphaNumericCharacters(casing), '_', 1, 62);
		}

		public static string IdentifierWithUnderscores(this IRandom random, int length, float underscoreFrequency, Casing casing)
		{
			if (length <= 0) return "";
			return random.String(1, GetAlphabeticCharacters(casing), '_', underscoreFrequency) + random.String(length, GetAlphaNumericCharacters(casing), '_', underscoreFrequency);
		}

		public static string IdentifierWithUnderscores(this IRandom random, int length, int underscoreCountPerChunk, int averageChunkLength, Casing casing)
		{
			if (length <= 0) return "";
			return random.String(1, GetAlphabeticCharacters(casing), '_', underscoreCountPerChunk, averageChunkLength) + random.String(length, GetAlphaNumericCharacters(casing), '_', underscoreCountPerChunk, averageChunkLength);
		}

		#endregion
	}
}
