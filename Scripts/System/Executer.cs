using Hibzz.Singletons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Dropl
{
    public class Executer : Singleton<Executer>
    {
		#region Public Static Content

		/// <summary>
		/// The default executer that's available everywhere
		/// </summary>
		/// <remarks>
		/// This is the same as the singleton instance, however, this variable 
		/// name gives better context as to what the singleton instance represents
		/// </remarks>
		public static Executer DefaultExecutor => Instance;

        /// <summary>
        /// Request a brand new executer that the user can use to add operations
        /// </summary>
        /// <returns>A new executer that the user can add operations to</returns>
        public static Executer RequestNewExecuter()
        {
            // this is a dummy instruction to make sure that a default
            // executer is constructed in case it's not. This will prevent the
            // newly requested constructor becoming the default executer
            // causing unexpected behavior.
            var _ = Executer.Instance;

			// construct a new gameobject add a new executer as a component
			// also, giving it a unique name so that it's useful for debugging
			var executer_gameobject = new GameObject();
            executer_gameobject.name = $"Custom Executer {executer_gameobject.GetInstanceID()}";
            var new_executer = executer_gameobject.AddComponent<Executer>();

            // the object has been constructed successfully, return it
            return new_executer;
        }

		#endregion

		#region Properties

		/// <summary>
		/// A list of operations that needs to get executed
		/// </summary>
		public List<Operation> Operations { get; protected set; } = new List<Operation>();

		/// <summary>
		/// How fast or slow the executor is running
		/// </summary>
		/// <remarks>
		/// Default time scale representing real-time is 1. The executer runs 
		/// faster when the value is higher than 1 and slower when the value 
		/// is lower than 1.
		/// </remarks>
		public float TimeScale { get; protected set; } = 1;

		#endregion

		#region Functions

		/// <summary>
		/// Set the timescale to the given value
		/// </summary>
		/// <param name="timeScale">The timescale in which the executor runs. Must be greater that 0</param>
		public void SetTimescale(float timeScale)
		{
			// Error checking to make sure that timescale is a positive
			// number... A negative timescale represents rewinding which is
			// not possible with this system
			if(timeScale < 0) 
			{
				Debug.LogWarning($"Requested timescale cannot be below 0: User requested a timescale of {timeScale}");
				return; 
			}

			// update the time scale
			TimeScale = timeScale;
		}

		/// <summary>
		/// Reset the timescale so that the executer runs in realtime
		/// </summary>
		public void ResetTimescale() => SetTimescale(1);

		#endregion

		#region Unity Events

		#endregion
	}
}
