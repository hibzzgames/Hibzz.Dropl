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
		/// Has the operation expired
		/// </summary>
		public Conditional HasExpired { get; set; } = new Conditional(RuleType.CHECK_ANY_IS_TRUE);

        /// <summary>
        /// Any delay that you want to add before starting the operation
        /// </summary>
        public float Delay { get; set; } = 0;

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
        public float EasedProgress => Easing.Evaluate(Progress);

        /// <summary>
        /// The target on which the operation is being performed on
        /// </summary>
        public Object Target { get; protected set; } = null;

        /// <summary>
        /// The current easing method applied for this operation
        /// </summary>
        public Easing Easing { get; protected set; } = Interpolations.LINEAR;

        /// <summary>
        /// The executer that this operation is inside of
        /// </summary>
        public Executer BelongsTo { get; internal set; } = null;

        /// <summary>
        /// Is this operation paused?
        /// </summary>
        public bool IsPaused { get; private set; } = false;

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
        public Operation(Object target, bool useDefaultExpirationRules = true)
        {
            // when the default expiration rules are applied, the operation
            // expires when the entire time has elapsed
            if(useDefaultExpirationRules)
            {
                HasExpired += () => TimeElapsed >= ExpirationTime;
            }

            // set the target and easing type
            Target = target;
        }

        /// <summary>
        /// Perform a tick operation
        /// </summary>
        internal void Tick(float dt)
        {
            // if the delta time is 0, there's nothing to proceed either
            if(dt == 0) { return; }

            // if it can't tick, don't proceed... all conditionals must return true for CanTick to be true
            if(!CanTick || IsPaused) { return; }

            // tick down the delay timer and if there's some more delay left in the operation, do not proceed
            Delay -= dt;
            if(Delay > 0) { return; }

			// if it's the first tick, call start
			if (TimeElapsed <= 0) { OnOperationStart(); }

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
        /// If in a paused state, resume the operation
        /// </summary>
        public void Resume()
        {
            IsPaused = false;
        }

        /// <summary>
        /// Pause the operation from ticking further
        /// </summary>
        public void Pause()
        {
            IsPaused = true;
        }

		/// <summary>
		/// Stop the execution of an operation immediately
		/// </summary>
		public void Stop()
		{
			// we forcefully expire this operation by making the HasExpired conditional always retur true
			HasExpired += () => true;
		}

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
