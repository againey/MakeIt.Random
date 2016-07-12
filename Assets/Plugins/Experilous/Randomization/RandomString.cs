/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class RandomString
	{
		#region UsingCharacters

		public static string UsingCharacters(int length, char[] characters, IRandomEngine engine)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = characters.RandomElement(engine);
			}
			return new string(buffer);
		}

		public static string UsingCharacters(int length, char[] characters, char otherCharacter, float otherFrequency, IRandomEngine engine)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = Chance.Probability(otherFrequency, engine) ? otherCharacter : characters.RandomElement(engine);
			}
			return new string(buffer);
		}

		public static string UsingCharacters(int length, char[] characters, char otherCharacter, int otherCountPerChunk, int averageChunkLength, IRandomEngine engine)
		{
			char[] buffer = new char[length];
			for (int i = 0; i < length; ++i)
			{
				buffer[i] = Chance.Probability(otherCountPerChunk, averageChunkLength, engine) ? otherCharacter : characters.RandomElement(engine);
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

		public static string Binary(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _binaryCharacters, engine);
		}

		#endregion

		#region Octal

		private static char[] _octalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7',
		};

		public static string Octal(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _octalCharacters, engine);
		}

		#endregion

		#region Decimal

		private static char[] _decimalCharacters =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		};

		public static string Decimal(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _decimalCharacters, engine);
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

		public static string Hexadecimal(int length, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetHexadecimalCharacters(casing), engine);
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

		public static string AlphaNumeric(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphaNumericCharacters, engine);
		}

		public static string AlphaNumericWithSpaces(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphaNumericCharacters, ' ', 1, 62, engine);
		}

		public static string AlphaNumericWithSpaces(int length, float spaceFrequency, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphaNumericCharacters, ' ', spaceFrequency, engine);
		}

		public static string AlphaNumericWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphaNumericCharacters, ' ', spaceCountPerChunk, averageChunkLength, engine);
		}

		public static string AlphaNumeric(int length, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing), engine);
		}

		public static string AlphaNumericWithSpaces(int length, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing), ' ', 1, 62, engine);
		}

		public static string AlphaNumericWithSpaces(int length, float spaceFrequency, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing), ' ', spaceFrequency, engine);
		}

		public static string AlphaNumericWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphaNumericCharacters(casing), ' ', spaceCountPerChunk, averageChunkLength, engine);
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

		public static string Alphabetic(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphabeticCharacters, engine);
		}

		public static string AlphabeticWithSpaces(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphabeticCharacters, ' ', 1, 52, engine);
		}

		public static string AlphabeticWithSpaces(int length, float spaceFrequency, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphabeticCharacters, ' ', spaceFrequency, engine);
		}

		public static string AlphabeticWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength, IRandomEngine engine)
		{
			return UsingCharacters(length, _alphabeticCharacters, ' ', spaceCountPerChunk, averageChunkLength, engine);
		}

		public static string Alphabetic(int length, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing), engine);
		}

		public static string AlphabeticWithSpaces(int length, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing), ' ', 1, 52, engine);
		}

		public static string AlphabeticWithSpaces(int length, float spaceFrequency, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing), ' ', spaceFrequency, engine);
		}

		public static string AlphabeticWithSpaces(int length, int spaceCountPerChunk, int averageChunkLength, Casing casing, IRandomEngine engine)
		{
			return UsingCharacters(length, GetAlphabeticCharacters(casing), ' ', spaceCountPerChunk, averageChunkLength, engine);
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

		public static string Identifier(int length, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return _alphabeticCharacters.RandomElement(engine) + UsingCharacters(length - 1, _alphaNumericCharacters, engine);
		}

		public static string IdentifierWithUnderscores(int length, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, _alphabeticCharacters, '_', 1, 52, engine) + UsingCharacters(length, _alphaNumericCharacters, '_', 1, 62, engine);
		}

		public static string IdentifierWithUnderscores(int length, float underscoreFrequency, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, _alphabeticCharacters, '_', underscoreFrequency, engine) + UsingCharacters(length, _alphaNumericCharacters, '_', underscoreFrequency, engine);
		}

		public static string IdentifierWithUnderscores(int length, int underscoreCountPerChunk, int averageChunkLength, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, _alphabeticCharacters, '_', underscoreCountPerChunk, averageChunkLength, engine) + UsingCharacters(length, _alphaNumericCharacters, '_', underscoreCountPerChunk, averageChunkLength, engine);
		}

		public static string Identifier(int length, Casing casing, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return GetIdentifierFirstCharacters(casing).RandomElement(engine) + UsingCharacters(length - 1, GetIdentifierCharacters(casing), engine);
		}

		public static string IdentifierWithUnderscores(int length, Casing casing, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, GetAlphabeticCharacters(casing), '_', 1, 52, engine) + UsingCharacters(length, GetAlphaNumericCharacters(casing), '_', 1, 62, engine);
		}

		public static string IdentifierWithUnderscores(int length, float underscoreFrequency, Casing casing, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, GetAlphabeticCharacters(casing), '_', underscoreFrequency, engine) + UsingCharacters(length, GetAlphaNumericCharacters(casing), '_', underscoreFrequency, engine);
		}

		public static string IdentifierWithUnderscores(int length, int underscoreCountPerChunk, int averageChunkLength, Casing casing, IRandomEngine engine)
		{
			if (length <= 0) return "";
			return UsingCharacters(1, GetAlphabeticCharacters(casing), '_', underscoreCountPerChunk, averageChunkLength, engine) + UsingCharacters(length, GetAlphaNumericCharacters(casing), '_', underscoreCountPerChunk, averageChunkLength, engine);
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

		public static string Base64(int length, IRandomEngine engine)
		{
			return UsingCharacters(length, _base64PlusSlashCharacters, engine);
		}

		public static string Base64(int length, Base64CharacterPairs characterPairs, IRandomEngine engine)
		{
			switch (characterPairs)
			{
				case Base64CharacterPairs.PlusSlash: return UsingCharacters(length, _base64PlusSlashCharacters, engine);
				case Base64CharacterPairs.HyphenUnderscore: return UsingCharacters(length, _base64HyphenUnderscoreCharacters, engine);
				case Base64CharacterPairs.PeriodUnderscore: return UsingCharacters(length, _base64PeriodUnderscoreCharacters, engine);
				case Base64CharacterPairs.PeriodHyphen: return UsingCharacters(length, _base64PeriodHyphenCharacters, engine);
				case Base64CharacterPairs.UnderscoreColon: return UsingCharacters(length, _base64UnderscoreColonCharacters, engine);
				case Base64CharacterPairs.UnderscoreHyphen: return UsingCharacters(length, _base64UnderscoreHyphenCharacters, engine);
				case Base64CharacterPairs.BangHyphen: return UsingCharacters(length, _base64BangHyphenCharacters, engine);
				case Base64CharacterPairs.TildeHyphen: return UsingCharacters(length, _base64TildeHyphenCharacters, engine);
				default: throw new System.NotImplementedException();
			}
		}

		#endregion
	}
}
