/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// Wraps an implementation of <see cref="IRandom"/> in a derivation of <see cref="ScriptableObject"/>.
	/// </summary>
	/// <seealso cref="ScriptableObject"/>
	/// <seealso cref="IRandom"/>
	public class ScriptableObjectRandomWrapper : ScriptableObject, IRandom, ISerializationCallbackReceiver
	{
		private IRandom _random;
		[SerializeField] private byte[] _state;

		/// <summary>
		/// Provides access to the wrapped <see cref="IRandom"/> instance.
		/// </summary>
		/// <remarks>This property cannot be assigned null.</remarks>
		public IRandom random
		{
			get
			{
				return _random;
			}
			set
			{
				if (value == null) throw new NullReferenceException();
				_random = value;
			}
		}

		private ScriptableObjectRandomWrapper() { }

		/// <summary>
		/// Creates a wrapper instance around the provided <see cref="IRandom"/> instance.
		/// </summary>
		/// <param name="random">The random engine to be wrapped as a <see cref="ScriptableObject"/>.</param>
		/// <returns>The wrapper around the provided random engine.</returns>
		public static ScriptableObjectRandomWrapper CreateInstance(IRandom random)
		{
			ScriptableObjectRandomWrapper scriptableObject = CreateInstance<ScriptableObjectRandomWrapper>();
			scriptableObject.random = random;
			return scriptableObject;
		}

		/// <inheritdoc />
		public byte[] SaveState()
		{
			return _random.SaveState();
		}

		/// <inheritdoc />
		public void RestoreState(byte[] stateArray)
		{
			_random.RestoreState(stateArray);
		}

		/// <summary>
		/// Implementation of ISerializationCallbackReceiver to save the wrapped random engine's state to an internal byte buffer which can be serialized by Unity.
		/// </summary>
		public void OnBeforeSerialize()
		{
			_state = _random.SaveState();
		}

		/// <summary>
		/// Implementation of ISerializationCallbackReceiver to restore the wrapped random engine's state from an internal byte buffer which can be deserialized by Unity.
		/// </summary>
		public void OnAfterDeserialize()
		{
			_random.RestoreState(_state);
		}

		/// <inheritdoc />
		public void Seed()
		{
			_random.Seed();
		}

		/// <inheritdoc />
		public void Seed(string seed)
		{
			_random.Seed(seed);
		}

		/// <inheritdoc />
		public void Seed(IBitGenerator bitGenerator)
		{
			_random.Seed(bitGenerator);
		}

		/// <inheritdoc />
		public void Seed(params int[] seed)
		{
			_random.Seed(seed);
		}

		/// <inheritdoc />
		public void Seed(int seed)
		{
			_random.Seed(seed);
		}

		/// <inheritdoc />
		public void MergeSeed()
		{
			_random.MergeSeed();
		}

		/// <inheritdoc />
		public void MergeSeed(IBitGenerator bitGenerator)
		{
			_random.MergeSeed(bitGenerator);
		}

		/// <inheritdoc />
		public void MergeSeed(string seed)
		{
			_random.MergeSeed(seed);
		}

		/// <inheritdoc />
		public void MergeSeed(params int[] seed)
		{
			_random.MergeSeed(seed);
		}

		/// <inheritdoc />
		public void MergeSeed(int seed)
		{
			_random.MergeSeed(seed);
		}

		/// <inheritdoc />
		public int stepBitCount { get { return _random.stepBitCount; } }

		/// <inheritdoc />
		public void Step()
		{
			_random.Step();
		}

		/// <inheritdoc />
		public uint Next32()
		{
			return _random.Next32();
		}

		/// <inheritdoc />
		public ulong Next64()
		{
			return _random.Next64();
		}

		/// <inheritdoc />
		public void Next64(out uint lower, out uint upper)
		{
			_random.Next64(out lower, out upper);
		}

		/// <inheritdoc />
		public int skipAheadMagnitude
		{
			get
			{
				return _random.skipAheadMagnitude;
			}
		}

		/// <inheritdoc />
		public int skipBackMagnitude
		{
			get
			{
				return _random.skipBackMagnitude;
			}
		}

		/// <inheritdoc />
		public void SkipAhead()
		{
			_random.SkipAhead();
		}

		/// <inheritdoc />
		public void SkipBack()
		{
			_random.SkipBack();
		}

		/// <inheritdoc />
		public System.Random AsSystemRandom()
		{
			return _random.AsSystemRandom();
		}
	}
}
