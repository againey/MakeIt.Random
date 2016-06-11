/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// An implementation of the <see cref="IRandomEngine"/> interface using the 1024-bit XorShift* generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG is based on Sebastiano Vigna's xorshift1024* generator, adapted from a
	/// <a href="http://xoroshiro.di.unimi.it/xorshift1024star.c">C code reference implementation</a>.</para>
	/// 
	/// <para>As its name implies, it maintains 1024 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="BaseRandomEngine"/>
	public sealed class XorShift1024Star : BaseRandomEngine
	{
		[SerializeField] private ulong[] _state = new ulong[] { 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 1UL, };
		[SerializeField] private int _offset;

		public static XorShift1024Star Create()
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.Seed();
			return instance;
		}

		public static XorShift1024Star Create(int seed)
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift1024Star Create(params int[] seed)
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift1024Star Create(string seed)
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.Seed(seed);
			return instance;
		}

		public static XorShift1024Star Create(RandomStateGenerator stateGenerator)
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.Seed(stateGenerator);
			return instance;
		}

		public static XorShift1024Star Create(IRandomEngine seeder)
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.Seed(seeder);
			return instance;
		}

		public static XorShift1024Star CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.RestoreState(stateArray);
			return instance;
		}

		public static XorShift1024Star CreateWithState(ulong[] state, int offset)
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.RestoreState(state, offset);
			return instance;
		}

		public XorShift1024Star Clone()
		{
			var instance = CreateInstance<XorShift1024Star>();
			instance.CopyStateFrom(this);
			return instance;
		}

		public void CopyStateFrom(XorShift1024Star source)
		{
			for (int i = 0; i < 16; ++i)
			{
				_state[i] = source._state[i];
			}
			_offset = source._offset;
		}

		public override byte[] SaveState()
		{
			var stateArray = new byte[sizeof(ulong) * 16 + sizeof(byte)];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					for (int i = 0; i < _state.Length; ++i)
					{
						SaveState(writer, _state[i]);
					}
					SaveState(writer, (byte)_offset);
				}
			}
			return stateArray;
		}

		public void SaveState(out ulong[] state, out int offset)
		{
			state = _state.Clone() as ulong[];
			offset = _offset;
		}

		public override void RestoreState(byte[] stateArray)
		{
			ulong[] state = new ulong[16];
			byte offset;
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var reader = new System.IO.BinaryReader(stream))
				{
					for (int i = 0; i < state.Length; ++i)
					{
						RestoreState(reader, out state[i]);
					}
					RestoreState(reader, out offset);
				}
			}
			RestoreState(state, offset);
		}

		public void RestoreState(ulong[] state, int offset)
		{
			if (state == null) throw new System.ArgumentNullException("state");
			if (state.Length != 16) throw new System.ArgumentException("The provided state array must have a length of exactly 16.", "state");
			if (offset < 0 || offset >= 16) throw new System.ArgumentOutOfRangeException("The provided offset must be within the range [0, 15].", "offset");
			for (int i = 0; i < 16; ++i)
			{
				if (state[i] != 0UL)
				{
					for (i = 0; i < 16; ++i)
					{
						_state[i] = state[i];
					}
					return;
				}
			}
			_offset = offset;
			throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift1024* random number generator.");
		}

		public override void Seed(RandomStateGenerator stateGenerator)
		{
			int tryCount = 0;
			int offsetIncrement = stateGenerator.ComputeIdealOffsetIncrement(16);

			do
			{
				ulong[] state = new ulong[16];
				bool allZeroes = true;
				for (int i = 0; i < 16; ++i)
				{
					state[i] = stateGenerator.Next64(offsetIncrement);
					allZeroes = allZeroes && state[i] == 0;
				}
				if (!allZeroes)
				{
					_state = state;
					_offset = 0;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided state generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void Seed(IRandomEngine seeder)
		{
			int tryCount = 0;

			do
			{
				ulong[] state = new ulong[16];
				bool allZeroes = true;
				for (int i = 0; i < 16; ++i)
				{
					state[i] = seeder.Next64();
					allZeroes = allZeroes && state[i] == 0;
				}
				if (!allZeroes)
				{
					_state = state;
					_offset = 0;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided random engine was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void MergeSeed(RandomStateGenerator stateGenerator)
		{
			int tryCount = 0;
			int offsetIncrement = stateGenerator.ComputeIdealOffsetIncrement(16);

			do
			{
				ulong[] state = new ulong[16];
				bool allZeroes = true;
				for (int i = 0; i < 16; ++i)
				{
					state[i] = _state[i] ^ stateGenerator.Next64(offsetIncrement);
					allZeroes = allZeroes && state[i] == 0;
				}
				if (!allZeroes)
				{
					_state = state;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided state generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void MergeSeed(IRandomEngine seeder)
		{
			int tryCount = 0;

			do
			{
				ulong[] state = new ulong[16];
				bool allZeroes = true;
				for (int i = 0; i < 16; ++i)
				{
					state[i] = _state[i] ^ seeder.Next64();
					allZeroes = allZeroes && state[i] == 0;
				}
				if (!allZeroes)
				{
					_state = state;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided random engine was unable to generate a non-zero state, which is required by this random engine.");
		}

		public override void Step()
		{
			var x = _state[_offset];
			_offset = (_offset + 1) & 0xF;
			var y = _state[_offset];
			y ^= y << 31;
			_state[_offset] = y ^ x ^ (y >> 11) ^ (x >> 30);
		}

		public override uint Next32()
		{
			var x = _state[_offset];
			_offset = (_offset + 1) & 0xF;
			var y = _state[_offset];
			y ^= y << 31;
			_state[_offset] = y ^ x ^ (y >> 11) ^ (x >> 30);
			return (uint)(_state[_offset] * 1181783497276652981UL);
		}

		public override ulong Next64()
		{
			var x = _state[_offset];
			_offset = (_offset + 1) & 0xF;
			var y = _state[_offset];
			y ^= y << 31;
			_state[_offset] = y ^ x ^ (y >> 11) ^ (x >> 30);
			return _state[_offset] * 1181783497276652981UL;
		}

		public override int skipAheadMagnitude { get { return 512; } }

		private static readonly ulong[] _jumpTable = new ulong[]
		{
			0x84242F96ECA9C41DUL, 0xA3C65B8776F96855UL, 0x5B34A39F070B5837UL, 0x4489AFFCE4F31A1EUL,
			0x2FFEEB0A48316F40UL, 0xDC2D9891FE68C022UL, 0x3659132BB12FEA70UL, 0xAAC17D8EFA43CAB8UL,
			0xC4CB815590989B13UL, 0x5EE975283D71C93BUL, 0x691548C86C1BD540UL, 0x7910C41D10A1E6A5UL,
			0x0B5FC64563B3E2A8UL, 0x047F7684E9FC949DUL, 0xB99181F2D8F685CAUL, 0x284600E3F30E38C3UL,
		};

		public override void SkipAhead()
		{
			ulong[] t = new ulong[16];
			for (int i = 0; i < 16; ++i)
			{
				for (int b = 0; b < 64; ++b)
				{
					if ((_jumpTable[i] & (1UL << b)) != 0UL)
					{
						for (int j = 0; j < 16; ++j)
						{
							t[j] ^= _state[(j + _offset) & 0xF];
						}
					}
					Step();
				}
			}

			for (int j = 0; j < 16; ++j)
			{
				_state[(j + _offset) & 0xF] = t[j];
			}
		}

		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper64(this);
		}
	}
}
