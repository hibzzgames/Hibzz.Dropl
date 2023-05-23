using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = System.Object;

namespace Hibzz.Dropl
{
    /// <summary>
    /// This class is used to define filters that filter different operation 
    /// from an executer
    /// </summary>
    public class Filter
    {
        public Object Target                    = null;
        public Type   OperationType             = null; 
        public Func<Operation, bool> CustomRule = null;

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
