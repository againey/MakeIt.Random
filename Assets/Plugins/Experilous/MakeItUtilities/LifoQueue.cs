/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experilous.MakeIt.Utilities
{
	/// <summary>
	/// A last-in first-out queue-interface container.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the queue.</typeparam>
	[Serializable]
	public class LifoQueue<T> : IPeekableQueue<T>
	{
		[SerializeField] private List<T> _items;

		public LifoQueue()
		{
			_items = new List<T>();
		}

		public T front
		{
			get
			{
				return _items[_items.Count - 1];
			}
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
			var item = front;
			RemoveFront();
			return item;
		}

		public T Peek()
		{
			return front;
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
