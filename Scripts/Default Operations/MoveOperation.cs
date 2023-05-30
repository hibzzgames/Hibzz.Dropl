using UnityEngine;

namespace Hibzz.Dropl.DefaultOperations
{
	/// <summary>
	/// Move the given transform to the expected position over the defined duration
	/// </summary>
	public class MoveOperation : PropertyOperation<Vector3>
	{
		public MoveOperation(Transform transform, Vector3 expectedPosition, float duration, Easing easing) 
			: base(getter:        () => transform.position, 
				   setter:        (pos) => { transform.position = pos; }, 
				   interpolator:  (a, b, t) => Vector3.Lerp(a, b, t), 
				   expectedValue: expectedPosition, 
				   duration, 
				   easing, 
				   target: transform) 
		{ }
	}

	/// <summary>
	/// Move the transform's X position to the expected value over the defined duration
	/// </summary>
	public class MoveXOperation : PropertyOperation<float>
	{
		public MoveXOperation(Transform transform, float expectedX, float duration, Easing easing)
			: base(getter: () => transform.position.x,
				   setter: (val) => {
					   var pos = transform.position;
					   pos.x = val;
					   transform.position = pos;
				   },
				   interpolator: (a, b, t) => Mathf.Lerp(a, b, t),
				   expectedValue: expectedX,
				   duration,
				   easing,
				   target: transform)
		{ }
	}

	/// <summary>
	/// Move the transform's Y position to the expected value over the defined duration
	/// </summary>
	public class MoveYOperation : PropertyOperation<float>
	{
		public MoveYOperation(Transform transform, float expectedY, float duration, Easing easing)
			: base(getter: () => transform.position.y,
				   setter: (val) => {
					   var pos = transform.position;
					   pos.y = val;
					   transform.position = pos;
				   },
				   interpolator: (a, b, t) => Mathf.Lerp(a, b, t),
				   expectedValue: expectedY,
				   duration,
				   easing,
				   target: transform)
		{ }
	}

	/// <summary>
	/// Move the transform's Z position to the expected value over the defined duration
	/// </summary>
	public class MoveZOperation : PropertyOperation<float>
	{
		public MoveZOperation(Transform transform, float expectedZ, float duration, Easing easing)
			: base(getter: () => transform.position.z,
				   setter: (val) => {
					   var pos = transform.position;
					   pos.z = val;
					   transform.position = pos;
				   },
				   interpolator: (a, b, t) => Mathf.Lerp(a, b, t),
				   expectedValue: expectedZ,
				   duration,
				   easing,
				   target: transform)
		{ }
	}
}
