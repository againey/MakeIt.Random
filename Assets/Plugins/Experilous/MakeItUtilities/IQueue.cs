/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeIt.Utilities
{
	/// <summary>
	/// A container supporting abstract push and pop actions for inserting
	/// and removing elements in an implementation-defined order.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the container.</typeparam>
	public interface IQueue<T>
	{
		bool isEmpty { get; }
		int Count { get; }
		void Push(T item);
		T Pop();
		void Clear();
	}
}
