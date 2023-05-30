using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Dropl
{
    /// <summary>
    /// A generic operation used to manipulate any property
    /// </summary>
    /// <typeparam name="T">The type of property to work with</typeparam>
    public class PropertyOperation<T> : Operation
    {
		// getter used to get the value from the property of type T
		protected System.Func<T> getter = null;

		// setter used to set the value of the property of type T
		protected System.Action<T> setter = null;

        // the function used to interpolate the property
        protected System.Func<T, T, float, T> interpolator = null;

        // the start value
        protected T startValue;

        // the end expected value
        protected T expectedValue;

        // public facing constructor
        public PropertyOperation(System.Func<T> getter, System.Action<T> setter, System.Func<T,T,float,T> interpolator, T expectedValue, float duration, Easing easing, object target = null) : base(target)
        {
            this.getter = getter;
            this.setter = setter;
            this.interpolator = interpolator;
            this.expectedValue = expectedValue;
            ExpirationTime = duration;
            Easing = easing;
        }

        // empty property constructor if a child needs to completely customize the behavior of the property operation
        protected PropertyOperation() : base(null) { }

		protected override void OnOperationStart()
		{
            startValue = getter();
		}

		protected override void OnOperationTick()
		{
            setter(interpolator(startValue, expectedValue, EasedProgress));
		}

		protected override void OnOperationComplete()
		{
            setter(expectedValue);
		}
	}
}
