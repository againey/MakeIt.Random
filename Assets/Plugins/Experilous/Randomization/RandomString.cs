/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public struct RandomString
	{
		private IRandomEngine _random;

		public RandomString(IRandomEngine random)
		{
			_random = random;
		}

		#region UsingCharacters

		public string UsingCharacters(int length, char[] characters)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = characters.RandomElement(_random);
			}
			return new string(buffer);
		}

		public string UsingCharacters(int length, char[] characters, char otherCharacter, float otherFrequency)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = _random.Chance().Probability(otherFrequency) ? otherCharacter : characters.RandomElement(_random);
			}
			return new string(buffer);
		}

		public string UsingCharacters(int length, char[] characters, char otherCharacter, int otherCountPerChunk, int averageChunkLength)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = _random.Chance().Probability(otherCountPerChunk, averageChunkLength) ? otherCharacter : characters.RandomElement(_random);
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

		public string Binary(int length)
		{
			return UsingCharacters(length, _binaryCharacters);
		}

		#endregion

		#region Octal

		private static char[] _octalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7',
		};

		public string Octal(int length)
		{
			return UsingCharacters(length, _octalCharacters);
		}

		#endregion

		#region Decimal

		private static char[] _decimalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		};

		public string Decimal(int length)
		{
			return UsingCharacters(length, _decimalCharacters);
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

		public string Hexadecimal(int length, Casing casing)
		{
			return UsingCharacters(length, GetHexadecimalCharacters(casing));
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

		public string Base64(int length)
		{
			return UsingCharacters(length, _base64PlusSlashCharacters);
		}

		public string Base64(int length, Base64CharacterPairs characterPairs)
		{
			switch (characterPairs)
			{
				case Base64CharacterPairs.PlusSlash: return UsingCharacters(length, _base64PlusSlashCharacters);
				case Base64CharacterPairs.HyphenUnderscore: return UsingCharacters(length, _base64HyphenUnderscoreCharacters);
				case Base64CharacterPairs.PeriodUnderscore: return UsingCharacters(length, _base64PeriodUnderscoreCharacters);
				case Base64CharacterPairs.PeriodHyphen: return UsingCharacters(length, _base64PeriodHyphenCharacters);
				case Base64CharacterPairs.UnderscoreColon: return UsingCharacters(length, _base64UnderscoreColonCharacters);
				case Base64CharacterPairs.UnderscoreHyphen: return UsingCharacters(length, _base64UnderscoreHyphenCharacters);
				case Base64CharacterPairs.BangHyphen: return UsingCharacters(length, _base64BangHyphenCharacters);
				case Base64CharacterPairs.TildeHyphen: return UsingCharacters(length, _base64TildeHyphenCharacters);
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

		public string AlphaNumeric(int length)
		{
			return UsingCharacters(length, _alphaNumericCharacters);
		}

		public string AlphaNumericWithSpaces(int length)
		{
			return UsingCharacters(length, _alphaNumericCharacters, ' ', 1, 62);
		}

		public string AlphaNumericWithSpaces(int length, float spaceFrequency)
		{
			return UsingCharacters(length, _alphaNumericCharacters, ' ', spaceFrequency);
		}

		public string AlphaNumericWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength)
		{
			return UsingCharacters(length, _alphaNumericCharacters, ' ', spaceCountPerChunk, averageChunkLength);
		}

		public string AlphaNumeric(int length, Casing casing)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing));
		}

		public string AlphaNumericWithSpaces(int length, Casing casing)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing), ' ', 1, 62);
		}

		public string AlphaNumericWithSpaces(int length, float spaceFrequency, Casing casing)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing), ' ', spaceFrequency);
		}

		public string AlphaNumericWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength, Casing casing)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing), ' ', spaceCountPerChunk, averageChunkLength);
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

		public string Alphabetic(int length)
		{
			return UsingCharacters(length, _alphabeticCharacters);
		}

		public string AlphabeticWithSpaces(int length)
		{
			return UsingCharacters(length, _alphabeticCharacters, ' ', 1, 52);
		}

		public string AlphabeticWithSpaces(int length, float spaceFrequency)
		{
			return UsingCharacters(length, _alphabeticCharacters, ' ', spaceFrequency);
		}

		public string AlphabeticWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength)
		{
			return UsingCharacters(length, _alphabeticCharacters, ' ', spaceCountPerChunk, averageChunkLength);
		}

		public string Alphabetic(int length, Casing casing)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing));
		}

		public string AlphabeticWithSpaces(int length, Casing casing)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing), ' ', 1, 52);
		}

		public string AlphabeticWithSpaces(int length, float spaceFrequency, Casing casing)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing), ' ', spaceFrequency);
		}

		public string AlphabeticWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength, Casing casing)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing), ' ', spaceCountPerChunk, averageChunkLength);
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

		public string Identifier(int length)
		{
			if (length <= 0) return "";
			return _alphabeticCharacters.RandomElement(_random) + UsingCharacters(length - 1, _alphaNumericCharacters);
		}

		public string IdentifierWithUnderscores(int length)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, _alphabeticCharacters, '_', 1, 52) + UsingCharacters(length, _alphaNumericCharacters, '_', 1, 62);
		}

		public string IdentifierWithUnderscores(int length, float underscoreFrequency)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, _alphabeticCharacters, '_', underscoreFrequency) + UsingCharacters(length, _alphaNumericCharacters, '_', underscoreFrequency);
		}

		public string IdentifierWithUnderscores(int length, int underscoreCountPerChunk, int averageChunkLength)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, _alphabeticCharacters, '_', underscoreCountPerChunk, averageChunkLength) + UsingCharacters(length, _alphaNumericCharacters, '_', underscoreCountPerChunk, averageChunkLength);
		}

		public string Identifier(int length, Casing casing)
		{
			if (length <= 0) return "";
			return GetIdentifierFirstCharacters(casing).RandomElement(_random) + UsingCharacters(length - 1, GetIdentifierCharacters(casing));
		}

		public string IdentifierWithUnderscores(int length, Casing casing)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, GetAlphabeticCharacters(casing), '_', 1, 52) + UsingCharacters(length, GetAlphaNumericCharacters(casing), '_', 1, 62);
		}

		public string IdentifierWithUnderscores(int length, float underscoreFrequency, Casing casing)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, GetAlphabeticCharacters(casing), '_', underscoreFrequency) + UsingCharacters(length, GetAlphaNumericCharacters(casing), '_', underscoreFrequency);
		}

		public string IdentifierWithUnderscores(int length, int underscoreCountPerChunk, int averageChunkLength, Casing casing)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, GetAlphabeticCharacters(casing), '_', underscoreCountPerChunk, averageChunkLength) + UsingCharacters(length, GetAlphaNumericCharacters(casing), '_', underscoreCountPerChunk, averageChunkLength);
		}

		#endregion
	}

	public static class RandomStringExtensions
	{
		public static RandomString String(this IRandomEngine random)
		{
			return new RandomString(random);
		}
	}
}
