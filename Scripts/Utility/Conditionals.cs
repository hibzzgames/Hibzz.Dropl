using System;
using System.Collections.Generic;

namespace Hibzz.Dropl
{
	// This is a very good utility function and I wish to wrap it in it's own
	// function so that other packages and games can use it individually.
	// However, it means adding a dependency and without a good dependency
	// management system by Unity, it's more hassle from a end user standpoint.
	// Having already experimented with the Singleton package, it seems to be
	// a pain in the butt to work with it without automation.
    public class Conditional
    {
		#region public static function

		// overriding the += operation to add a new condition to the checklist
		public static Conditional operator+(Conditional a, Func<bool> condition)
		{
			a.checks.Add(condition);
			return a;
		}

		// overriding the -= operation to remove a condition from the checklist
		public static Conditional operator-(Conditional a, Func<bool> condition)
		{
			a.checks.Remove(condition);
			return a;
		}

		// adding an implicit conversion to bool so that checks can be
		// automatically performed inside an if statement
		public static implicit operator bool(Conditional a)
		{
			return a.currentRule();
		}

		#endregion

		// the list of rules to work with
		protected List<Func<bool>> checks = new List<Func<bool>>();

		protected Func<bool> currentRule;

		// default constructor
		public Conditional(RuleType rule)
		{
			if(rule == RuleType.CHECK_ALL_ARE_TRUE) 
			{
				currentRule = CheckAllAreTrue;
			}

			else if(rule == RuleType.CHECK_ANY_IS_TRUE)
			{
				currentRule = CheckAnyIsTrue;
			}
		}

		// Check if all the checks are true
		protected bool CheckAllAreTrue()
		{
			// if any of the checks fails, return false
			foreach (var check in checks)
			{
				if (!check()) { return false; }
			}
			return true;
		}

		// check if any is true
		protected bool CheckAnyIsTrue()
		{
			foreach(var check in checks)
			{
				if(check()) { return true; }
			}

			return false;
		}

		// an enum representing the rule to check
		// could have been a bool but this adds better readability
		public enum RuleType { CHECK_ALL_ARE_TRUE, CHECK_ANY_IS_TRUE }
	}
}
