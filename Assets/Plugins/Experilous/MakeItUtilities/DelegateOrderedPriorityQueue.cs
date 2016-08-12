/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;

namespace Experilous.Containers
{
	public class DelegateOrderedPriorityQueue<T> : PriorityQueue<T> where T : IEquatable<T>
	{
		public delegate bool AreOrderedDelegate(T lhs, T rhs);

		private AreOrderedDelegate _areOrdered;

		/// <summary>
		/// Initializes the priority queue with the specified ordering delegate and zero initial capacity.
		/// </summary>
		/// <param name="areOrdered">The delegate that will indicate if two items are ordered properly.</param>
		public DelegateOrderedPriorityQueue(AreOrderedDelegate areOrdered)
		{
			if (areOrdered == null) throw new ArgumentNullException("areOrdered");
			_areOrdered = areOrdered;
		}

		/// <summary>
		/// Initializes the priority queue with the specified ordering delegate and a specified initial capacity.
		/// </summary>
		/// <param name="areOrdered">The delegate that will indicate if two items are ordered properly.</param>
		public DelegateOrderedPriorityQueue(AreOrderedDelegate areOrdered, int initialCapacity)
			: base(initialCapacity)
		{
			if (areOrdered == null) throw new ArgumentNullException("areOrdered");
			_areOrdered = areOrdered;
		}

		/// <summary>
		/// Removes all elements from the queue, and sets a new <see cref="AreOrderedDelegate"/> functor which will be applied to any elements pushed later on.
		/// </summary>
		/// <param name="areOrdered">The delegate that will indicate if two items are ordered properly.</param>
		/// <remarks>This does not deallocate any memory used by the internal data structure.
		/// It therefore enables reuse of the priority queue instance to cut down on the cost
		/// of allocation and garbage collection.</remarks>
		public void Reset(AreOrderedDelegate areOrdered)
		{
			if (areOrdered == null) throw new ArgumentNullException("areOrdered");

			Clear();
			_areOrdered = areOrdered;
		}

		protected override bool AreOrdered(T lhs, T rhs)
		{
			return _areOrdered(lhs, rhs);
		}
	}
}
