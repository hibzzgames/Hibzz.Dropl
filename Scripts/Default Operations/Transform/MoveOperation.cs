using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Dropl.DefaultOperations
{
    public class MoveOperation : Operation
    {
        // the expected position during the completion of the operation
        Vector3 expectedPosition;

        // the start position when the operation starts
        Vector3 startPosition;

        Transform transform;

        public MoveOperation(Transform transform, Vector3 expectedPosition, float duration, Easing easing) : base(transform)
        {
            this.expectedPosition = expectedPosition;
            ExpirationTime = duration;
            Easing = easing;
        }

		protected override void OnOperationStart()
		{
            transform = Target as Transform;
            startPosition = transform.position;
		}

		protected override void OnOperationTick()
		{
            transform.position = Vector3.Lerp(startPosition, expectedPosition, EasedProgress);
		}

		protected override void OnOperationComplete()
		{
            transform.position = expectedPosition;
		}

		#region Move in the X axis

        public class X : Operation
        {
            // the expected value of x during the completion of the operation
            float expectedX;

            // the started value of x when the operation starts
            float startX;

            Transform transform;

            public X(Transform transform, float expectedX, float duration, Easing easing) : base(transform)
            {
                this.expectedX = expectedX;
                ExpirationTime = duration;
                Easing = easing;
            }

			protected override void OnOperationStart()
			{
                transform = Target as Transform;
                startX = transform.position.x;
			}

			protected override void OnOperationTick()
			{
                var pos = transform.position;
                pos.x = Mathf.Lerp(startX, expectedX, EasedProgress);
                transform.position = pos;
			}

			protected override void OnOperationComplete()
			{
                var pos = transform.position;
                pos.x = expectedX;
                transform.position = pos;
			}
		}

		#endregion

		#region Move in the Y axis

		public class Y : Operation
		{
			// the expected value of y during the completion of the operation
			float expectedY;

			// the started value of y when the operation starts
			float startY;

			Transform transform;

			public Y(Transform transform, float expectedY, float duration, Easing easing) : base(transform)
			{
				this.expectedY = expectedY;
				ExpirationTime = duration;
				Easing = easing;
			}

			protected override void OnOperationStart()
			{
				transform = Target as Transform;
				startY = transform.position.y;
			}

			protected override void OnOperationTick()
			{
				var pos = transform.position;
				pos.y = Mathf.Lerp(startY, expectedY, EasedProgress);
				transform.position = pos;
			}

			protected override void OnOperationComplete()
			{
				var pos = transform.position;
				pos.y = expectedY;
				transform.position = pos;
			}
		}

		#endregion

		#region Move in the Z axis

		public class Z : Operation
		{
			// the expected value of z during the completion of the operation
			float expectedZ;

			// the started value of z when the operation starts
			float startZ;

			Transform transform;

			public Z(Transform transform, float expectedZ, float duration, Easing easing) : base(transform)
			{
				this.expectedZ = expectedZ;
				ExpirationTime = duration;
				Easing = easing;
			}

			protected override void OnOperationStart()
			{
				transform = Target as Transform;
				startZ = transform.position.z;
			}

			protected override void OnOperationTick()
			{
				var pos = transform.position;
				pos.z = Mathf.Lerp(startZ, expectedZ, EasedProgress);
				transform.position = pos;
			}

			protected override void OnOperationComplete()
			{
				var pos = transform.position;
				pos.z = expectedZ;
				transform.position = pos;
			}
		}

		#endregion
	}
}
