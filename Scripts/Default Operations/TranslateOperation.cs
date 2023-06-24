using UnityEngine;

namespace Hibzz.Dropl.DefaultOperations
{
    public class TranslateOperation : PropertyOperation<Vector3>
    {
        public TranslateOperation(Transform transform, Vector3 offset, float duration, Easing easing)
        {
            getter = () =>
            {
                expectedValue = transform.position + offset;
                return transform.position;
            };
            setter = (pos) => transform.position = pos;
            interpolator = (a, b, t) => Vector3.Lerp(a, b, t);
            ExpirationTime = duration;
            Easing = easing;
            Target = transform;
        }
	}

    public class TranslateXOperation : PropertyOperation<float>
    {
        public TranslateXOperation(Transform transform, float offset, float duration, Easing easing)
        {
			getter = () =>
			{
				expectedValue = transform.position.x + offset;
				return transform.position.x;
			};
            setter = (x) => 
            {
                var pos = transform.position;
                pos.x = x;
                transform.position = pos; 
            };
			interpolator = (a, b, t) => Mathf.Lerp(a, b, t);
			ExpirationTime = duration;
			Easing = easing;
			Target = transform;
		}
    }

	public class TranslateYOperation : PropertyOperation<float>
	{
		public TranslateYOperation(Transform transform, float offset, float duration, Easing easing)
		{
			getter = () =>
			{
				expectedValue = transform.position.y + offset;
				return transform.position.y;
			};
			setter = (y) =>
			{
				var pos = transform.position;
				pos.y = y;
				transform.position = pos;
			};
			interpolator = (a, b, t) => Mathf.Lerp(a, b, t);
			ExpirationTime = duration;
			Easing = easing;
			Target = transform;
		}
	}

	public class TranslateZOperation : PropertyOperation<float>
	{
		public TranslateZOperation(Transform transform, float offset, float duration, Easing easing)
		{
			getter = () =>
			{
				expectedValue = transform.position.z + offset;
				return transform.position.z;
			};
			setter = (z) =>
			{
				var pos = transform.position;
				pos.z = z;
				transform.position = pos;
			};
			interpolator = (a, b, t) => Mathf.Lerp(a, b, t);
			ExpirationTime = duration;
			Easing = easing;
			Target = transform;
		}
	}
}
