/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Containers
{
	/// <summary>
	/// A container supporting abstract push and pop actions for inserting
	/// and removing elements in an implementation-defined order, and allowing
	/// the front item to be accessed without popping it.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the container.</typeparam>
	public interface IPeekableQueue<T> : IQueue<T>
	{
		T front { get; }
		T Peek();
		void RemoveFront();
	}
}
