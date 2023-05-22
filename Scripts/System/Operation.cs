using UnityEngine;

using Object = System.Object;
using RuleType = Hibzz.Dropl.Conditional.RuleType;

namespace Hibzz.Dropl
{
    public abstract class Operation
    {
        /// <summary>
        /// Can this operation tick?
        /// </summary>
        public Conditional CanTick { get; set; } = new Conditional(RuleType.CHECK_ALL_ARE_TRUE);

        /// <summary>
        /// How long has the operation been ticking?
        /// </summary>
        public float TimeElapsed { get; protected set; } = 0;

        /// <summary>
        /// How long should the operation tick?
        /// </summary>
        public float ExpirationTime { get; protected set; }

        /// <summary>
        /// How much time is left for the operation to tick?
        /// </summary>
        public float TimeLeft => Mathf.Max(ExpirationTime - TimeElapsed, 0);

        /// <summary>
        /// What's the progress on the operation?
        /// </summary>
        public float Progress => Mathf.Clamp01(TimeElapsed / ExpirationTime);

        /// <summary>
        /// The progress made but eased by the specified easing method
        /// </summary>
        public float EasedProgress => Easing.Apply(Progress, EasingType);

        /// <summary>
        /// Has the operation expired
        /// </summary>
        public Conditional HasExpired { get; set; } = new Conditional(RuleType.CHECK_ANY_IS_TRUE);

        /// <summary>
        /// The target on which the operation is being performed on
        /// </summary>
        public Object Target { get; protected set; } = null;

        /// <summary>
        /// Is the operation marked to expire forcefully
        /// </summary>
        protected bool forceExpire { get; private set; } = false;

        /// <summary>
        /// The current easing method applied for this operation
        /// </summary>
        public Easing.Type EasingType { get; protected set; } = Easing.Type.LINEAR;

        /// <summary>
        /// The executer that this operation is inside of
        /// </summary>
        public Executer BelongsTo { get; internal set; } = null;

        /// <summary>
        /// The default construction of an operation
        /// </summary>
        /// <param name="target">Target to which the operation will be applied to</param>
        /// <param name="useDefaultExpirationRules">
        /// When set to true, default operation expiry rules gets applied. The 
        /// default behavior expires an operation when the time specified at 
        /// object construction has elapsed.
        /// <br /><br />
        /// When set to false, there are no expiration rules are applied at 
        /// all and the user must add the rules manually
        /// </param>
        public Operation(Object target, Easing.Type easingType = Easing.Type.LINEAR, bool useDefaultExpirationRules = true)
        {
            // when the default expiration rules are applied, the operation
            // expires when the entire time has elapsed or if force expire
            // request was set
            if(useDefaultExpirationRules)
            {
                HasExpired += () => TimeElapsed >= ExpirationTime;
                HasExpired += () => forceExpire;
            }

            // set the target and easing type
            Target = target;
            EasingType = easingType;
        }

		/// <summary>
		/// Forcefully expire an operation
		/// </summary>
		public void ForceExpire()
		{
			forceExpire = true;
		}

        /// <summary>
        /// Perform a tick operation
        /// </summary>
        internal void Tick(float dt)
        {
            // if the delta time is 0, there's nothing to proceed either
            if(dt == 0) { return; }

            // if it can't tick, don't proceed... all conditionals must return trye for CanTick to be true
            if(!CanTick) { return; }

            // if it's the first tick, call start
            if(TimeElapsed <= 0) { OnOperationStart(); }

            // increment the time and perform the tick operation
            TimeElapsed += dt;
            OnOperationTick();

            // if the operation has exprired, invoke a method that can let operations handle on operation end
            if(HasExpired) { OnOperationComplete(); }
        }

        // invoked when operation started
        protected virtual void OnOperationStart() { }

        // invoked when an operation ticks forward
        protected virtual void OnOperationTick() { }

        // invoked when operation completes/expires
        protected virtual void OnOperationComplete() { }

        /// <summary>
        /// Add this operation to the default executer
        /// </summary>
        /// <returns>The executer to add to</returns>
        public bool AddToDefaultExecuter()
        {
            // can't add duplicates operations to the default executer
            if(Executer.DefaultExecutor.Operations.Contains(this)) { return false; }

            // add self to the default executer
            Executer.DefaultExecutor.Add(this);
            return true;
        }
	}
}
