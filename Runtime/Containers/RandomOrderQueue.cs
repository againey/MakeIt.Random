/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using System.Collections.Generic;
using MakeIt.Random;

namespace MakeIt.Containers
{
	/// <summary>
	/// A container with a push/pop queue interface which pops elements at random, with
	/// no influence from how recently any particular element was pushed into the queue.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the container.</typeparam>
	public class RandomOrderQueue<T> : IQueue<T>
	{
		private List<T> _items;
		private IRandom _random;

		/// <summary>
		/// Constructs an empty random order queue, with a reference to a random engine to be used when popping elements.
		/// </summary>
		/// <param name="random">The random engine which will be used for producing a random pop order.</param>
		public RandomOrderQueue(IRandom random)
		{
			if (random == null) throw new ArgumentNullException("random");

			_items = new List<T>();
			_random = random;
		}

		/// <summary>
		/// Indicates if there are zero items in the queue.
		/// </summary>
		public bool isEmpty
		{
			get
			{
				return _items.Count == 0;
			}
		}

		/// <summary>
		/// Returns the number of items in the queue.
		/// </summary>
		public int Count
		{
			get
			{
				return _items.Count;
			}
		}

		/// <summary>
		/// Pushes an item into the queue.
		/// </summary>
		/// <param name="item">The item to put in the queue.</param>
		public void Push(T item)
		{
			_items.Add(item);
		}

		/// <summary>
		/// Removes a random item from the queue.
		/// </summary>
		/// <returns>The item that was just removed from the queue.</returns>
		public T Pop()
		{
			var randomIndex = _random.Index(_items.Count);
			var frontIndex = _items.Count - 1;
			var item = _items[randomIndex];
			_items[randomIndex] = _items[frontIndex];
			_items.RemoveAt(frontIndex);
			return item;
		}

		/// <summary>
		/// Removes all items from the queue.
		/// </summary>
		public void Clear()
		{
			_items.Clear();
		}
	}
}

