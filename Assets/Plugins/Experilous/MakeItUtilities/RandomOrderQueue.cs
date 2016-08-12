/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using System.Collections.Generic;
using Experilous.Randomization;

namespace Experilous.Containers
{
	/// <summary>
	/// A last-in first-out container.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the stack.</typeparam>
	public class RandomOrderQueue<T> : IQueue<T>
	{
		private List<T> _items;
		private IRandomEngine _random;

		public RandomOrderQueue(IRandomEngine random)
		{
			if (random == null) throw new ArgumentNullException("randomEngine");

			_items = new List<T>();
			_random = random;
		}

		public bool isEmpty
		{
			get
			{
				return _items.Count == 0;
			}
		}

		public int Count
		{
			get
			{
				return _items.Count;
			}
		}

		public void Push(T item)
		{
			_items.Add(item);
		}

		public T Pop()
		{
			var randomIndex = _random.UniformIndex(_items.Count);
			var frontIndex = _items.Count - 1;
			var item = _items[randomIndex];
			_items[randomIndex] = _items[frontIndex];
			_items.RemoveAt(frontIndex);
			return item;
		}

		public void RemoveFront()
		{
			_items.RemoveAt(_items.Count - 1);
		}

		public void Clear()
		{
			_items.Clear();
		}
	}
}
