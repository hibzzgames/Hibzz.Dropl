using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Dropl.DefaultOperations
{
    public class RotateOperation : PropertyOperation<Vector3>
    {
		public RotateOperation(Transform transform, Vector3 expectedRotation, float duration, Easing easing) 
			: base( getter: () => transform.eulerAngles,
				    setter: (rot) => { transform.eulerAngles = rot; },
				    interpolator: (a,b,t) => {
					    Vector3 rot;
					    rot.x = Mathf.LerpAngle(a.x, b.x, t);
					    rot.y = Mathf.LerpAngle(a.y, b.y, t);
					    rot.z = Mathf.LerpAngle(a.z, b.z, t);
					    return rot;
				    },
				    expectedValue: expectedRotation,
				    duration,
				    easing,
				    target: transform )
		{ }
	}

	public class RotateXOperation : PropertyOperation<float>
	{
		public RotateXOperation(Transform transform, float expectedX, float duration, Easing easing) 
			: base( getter: () => transform.eulerAngles.x,
				    setter: (val) => {
					    var rot = transform.eulerAngles;
					    rot.x = val;
					    transform.eulerAngles = rot;
				    },
				    interpolator: (a,b,t) => Mathf.LerpAngle(a,b,t),
				    expectedValue: expectedX,
				    duration,
				    easing,
				    target: transform )
		{ }
	}

	public class RotateYOperation : PropertyOperation<float>
	{
		public RotateYOperation(Transform transform, float expectedY, float duration, Easing easing)
			: base(getter: () => transform.eulerAngles.y,
					setter: (val) => {
						var rot = transform.eulerAngles;
						rot.y = val;
						transform.eulerAngles = rot;
					},
					interpolator: (a, b, t) => Mathf.LerpAngle(a, b, t),
					expectedValue: expectedY,
					duration,
					easing,
					target: transform)
		{ }
	}

	public class RotateZOperation : PropertyOperation<float>
	{
		public RotateZOperation(Transform transform, float expectedZ, float duration, Easing easing)
			: base(getter: () => transform.eulerAngles.z,
					setter: (val) => {
						var rot = transform.eulerAngles;
						rot.z = val;
						transform.eulerAngles = rot;
					},
					interpolator: (a, b, t) => Mathf.LerpAngle(a, b, t),
					expectedValue: expectedZ,
					duration,
					easing,
					target: transform)
		{ }
	}
}
