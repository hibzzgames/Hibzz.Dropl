namespace Hibzz.Dropl
{
	public class LambdaOperation : Operation
	{
		// simple lambda action that needs to be executed once
		System.Action lambda = null;

		// lambda action that needs to be executed over time
		System.Action<float> progressLambda = null;

		/// <summary>
		/// A lambda operation that'll execute the given action exactly once
		/// </summary>
		/// <param name="lambda">The action to execute</param>
		public LambdaOperation(System.Action lambda) : base(target: null, useDefaultExpirationRules: false)
		{
			this.lambda = lambda;
		}

		/// <summary>
		/// A lambda operation that'll execute the given action over time
		/// </summary>
		/// <param name="duration">How long should the lambda operation be executed?</param>
		/// <param name="lambda">An action that recieves the progress as a parameter to work with</param>
		public LambdaOperation(float duration, System.Action<float> lambda) : base(target: null)
		{
			progressLambda = lambda;
			ExpirationTime = duration;
		}

		/// <summary>
		/// A lambda operation that'll execute the given action over time
		/// </summary>
		/// <param name="duration">How long should the lambda operation be executed?</param>
		/// <param name="easing">What type of easing should be applied?</param>
		/// <param name="lambda">An action that recieves the eased progress as a prameter to work with</param>
		public LambdaOperation(float duration, Easing easing, System.Action<float> lambda) : this(duration, lambda)
		{
			Easing = easing;
		}

		// on operation tick
		protected override void OnOperationTick()
		{
			// if a simple lambda action was provided, execute it and stop the operation
			if(lambda is not null)
			{
				lambda();
				Stop();
				return;
			}

			// lambda is null, so one of the over time constructors must have been used
			// execute the lambda over time action and pass in the eased progress
			progressLambda(EasedProgress);
		}
	}
}
