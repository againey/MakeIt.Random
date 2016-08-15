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
			random.String(buffer, 0, length, characters);
			return new string(buffer);
		}

		public static string String(this IRandom random, int length, char[] characters, char separator, float separatorFrequency)
		{
			char[] buffer = new char[length];
			random.String(buffer, 0, length, characters, separator, separatorFrequency);
			return new string(buffer);
		}

		private static void String(this IRandom random, char[] buffer, int start, int length, char[] characters)
		{
			for (int i = start; i < start + length; ++i)
			{
				buffer[i] = characters.RandomElement(random);
			}
		}

		private static void String(this IRandom random, char[] buffer, int start, int length, char[] characters, char separator, float separatorFrequency, bool allowSeparatorState = false, bool forceSeparatorState = false)
		{
			for (int i = start; i < start + length; ++i)
			{
				if (allowSeparatorState)
				{
					if (forceSeparatorState || random.Probability(separatorFrequency))
					{
						buffer[i] = separator;
						allowSeparatorState = false;
						forceSeparatorState = false;
					}
					else
					{
						buffer[i] = characters.RandomElement(random);
					}
				}
				else
				{
					buffer[i] = characters.RandomElement(random);
					allowSeparatorState = true;
					forceSeparatorState = random.Probability(separatorFrequency);
				}
			}
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

		public static string AlphaNumericString(this IRandom random, int length, char separator, float separatorFrequency)
		{
			return random.String(length, _alphaNumericCharacters, separator, separatorFrequency);
		}

		public static string AlphaNumericString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphaNumericCharacters(casing));
		}

		public static string AlphaNumericString(this IRandom random, int length, Casing casing, char separator, float separatorFrequency)
		{
			return random.String(length, GetAlphaNumericCharacters(casing), separator, separatorFrequency);
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

		public static string AlphabeticString(this IRandom random, int length, char separator, float separatorFrequency)
		{
			return random.String(length, _alphabeticCharacters, separator, separatorFrequency);
		}

		public static string AlphabeticString(this IRandom random, int length, Casing casing)
		{
			return random.String(length, GetAlphabeticCharacters(casing));
		}

		public static string AlphabeticString(this IRandom random, int length, Casing casing, char separator, float separatorFrequency)
		{
			return random.String(length, GetAlphabeticCharacters(casing), separator, separatorFrequency);
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
			char[] buffer = new char[length];
			buffer[0] = _alphabeticCharacters.RandomElement(random);
			random.String(buffer, 1, length - 1, _alphaNumericCharacters);
			return new string(buffer);
		}

		public static string Identifier(this IRandom random, int length, float underscoreFrequency)
		{
			if (length <= 0) return "";
			char[] buffer = new char[length];
			if (random.Probability(underscoreFrequency))
			{
				buffer[0] = '_';
			}
			else
			{
				buffer[0] = _alphabeticCharacters.RandomElement(random);
			}
			random.String(buffer, 1, length - 1, _alphaNumericCharacters, '_', underscoreFrequency, true, false);
			return new string(buffer);
		}

		public static string Identifier(this IRandom random, int length, Casing casing)
		{
			if (length <= 0) return "";
			char[] buffer = new char[length];
			buffer[0] = GetAlphabeticCharacters(casing).RandomElement(random);
			random.String(buffer, 1, length - 1, GetAlphaNumericCharacters(casing));
			return new string(buffer);
		}

		public static string Identifier(this IRandom random, int length, Casing casing, float underscoreFrequency)
		{
			if (length <= 0) return "";
			char[] buffer = new char[length];
			if (random.Probability(underscoreFrequency))
			{
				buffer[0] = '_';
			}
			else
			{
				buffer[0] = GetAlphabeticCharacters(casing).RandomElement(random);
			}
			random.String(buffer, 1, length - 1, GetAlphaNumericCharacters(casing), '_', underscoreFrequency, true, false);
			return new string(buffer);
		}

		#endregion
	}
}
