using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Dropl
{
	public class Sequence : Operation
	{
		// contains a sequence of operation that needs to be pushed to the sequence's executer
		List<Operation> operationQueue = new List<Operation>();

		// the operation that was pushed out of the queue that is being currently executed
		Operation currentOperation = null;

		// public constructor
		public Sequence() : base(null, useDefaultExpirationRules: false)
		{
			// a sequence will expire if it has nothing left to push to its
			// executer from the operation queue
			HasExpired += () => operationQueue.Count <= 0;
		}

		protected override void OnOperationTick()
		{
			// How does this system work? 
			// --------------------------
			// On it's first tick where the current operation is null, the
			// sequence will push out the first operation in its operation
			// queue into the executer that it's part of... After that process,
			// it'll continuously check if the operation that was pushed into
			// the executer still exists in the executer executing commands...
			// If the current operation is still being executed, the sequence
			// will wait... And as soon as the current operation expires,
			// it'll be removed from the executer and the sequence will push
			// the next operation in its operation queue into the executer.

			// Based on the configuration created in the constructor, this
			// operation will continue until there are no more operations left
			// in the operation queue, where this sequence would have expired

			// if there are no current operations being performed or if the
			// current operation is no longer being executed by the executer,
			// the cached operation has expired and a new operation in the
			// queue needs to be pushed into the executer
			if(currentOperation == null || !BelongsTo.Operations.Contains(currentOperation))
			{
				// get the first element in the queue and pop it... moving
				// forward it'll be considered as the new current operation
				var head_operation = operationQueue[0];
				operationQueue.Remove(head_operation);
				currentOperation = head_operation;

				// the popped out operation gets added to the executer that
				// the sequence belongs to
				BelongsTo.Add(head_operation);
			}
		}

		/// <summary>
		/// Add the given operation to sequence so that it'll get executed 
		/// later when all other operation in the queue has been dispersed
		/// </summary>
		/// <param name="operation">The operation to add</param>
		public void Add(Operation operation)
		{
			operationQueue.Add(operation);
		}

		/// <summary>
		/// Remove the given operation from the sequence if it's found
		/// </summary>
		/// <param name="operation">The operation to remove</param>
		/// <remarks>
		/// This function does not work recursively. If you want to 
		/// recursively remove the operation from all nested sequences, 
		/// please use <see cref="RemoveAll(Operation)"/> instead
		/// </remarks>
		public void Remove(Operation operation)
		{
			operationQueue.Remove(operation);
		}

		/// <summary>
		/// Remove operations that matches the filter
		/// </summary>
		/// <param name="filter">The filter to use to determine which operation(s) to remove</param>
		/// <remarks>
		/// This function doesn't recursively remove operations. Please use <see cref="RemoveAll(Filter)"/> instead to remove content recursively.
		/// </remarks>
		public void Remove(Filter filter)
		{
			operationQueue.RemoveAll((operation) => filter.DoesMatch(operation));
		}

		/// <summary>
		/// Recursively remove all operation of requested object from the nested sequences
		/// </summary>
		/// <param name="operation">The operation to remove</param>
		public void RemoveAll(Operation operation)
		{
			operationQueue.RemoveAll((op) =>
			{
				// check if the operation is a sequence... if it's a sequence
				// remove all matching operations inside recursively
				var sequence_operation = op as Sequence;
				if(op != null)
				{
					sequence_operation.RemoveAll(operation);
				}

				return operation == op;
			});
		}

		/// <summary>
		/// Recursively remove all the operations that match filter from nested sequences
		/// </summary>
		/// <param name="filter">The filter to use to determine which operation(s) to remove</param>
		public void RemoveAll(Filter filter)
		{
			operationQueue.RemoveAll((operation) => 
			{
				// check if any of the operations in the operation queue is a
				// sequence... if it's a sequence remove all elements inside
				// it that match the filter
				var sequence_operation = operation as Sequence;
				if(sequence_operation != null)
				{
					sequence_operation.RemoveAll(filter);
				}

				return filter.DoesMatch(operation);
			});
		}
	}
}
