using System;

namespace Hibzz.Dropl
{
    /// <summary>
    /// This class is used to define filters that filter different operation 
    /// from an executer
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// The object that an operation is working on
        /// </summary>
        public object Target = null;

        /// <summary>
        /// The type of operation that needs to be filtered
        /// </summary>
        /// <remarks>
        /// Use <c>typeof(OperationSubclass)</c> to set this value
        /// </remarks>
        public Type OperationType = null; 

        /// <summary>
        /// A custom rule can be added to check if an operation passes the test
        /// </summary>
        public Func<Operation, bool> CustomRule = null;

		/// <summary>
		/// Check if the given operation matches the filter
		/// </summary>
		/// <param name="operatation">The operation to check</param>
		/// <returns>
		/// <para> True: all the rules in the filter matches the operation </para>
		/// <para> False: any of the rule in the filter fails </para>
		/// </returns>
		public bool DoesMatch(Operation operatation)
        {
            // if rule is available, check the rule and see if the operation
            // can pass the rule. But if no such rule is defined, then we
            // automatically pass it

            // rule 1: operation's type matches
            if (OperationType is not null && !(operatation.GetType() == OperationType))
            {
                return false;
            }

			// rule 2: target matches
			if (Target is not null && !(operatation.Target == Target))
			{
				return false;
			}

			// rule 3: custom rule defined by the user
			if (CustomRule is not null && !CustomRule(operatation))
            {
                return false;
            }

            return true;
        }
    }
}
