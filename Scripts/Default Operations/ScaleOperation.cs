using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Dropl
{
    public class ScaleOperation : PropertyOperation<Vector3>
    {
        public ScaleOperation(Transform transform, Vector3 expectedScale, float duration, Easing easing)
            : base( getter: () => transform.localScale,
                    setter: (scale) => { transform.localScale = scale; },
                    interpolator: (a,b,t) => Vector3.Lerp(a,b,t),
                    expectedValue: expectedScale,
                    duration,
                    easing,
                    target: transform)
        { }
    }

    public class ScaleXOperation : PropertyOperation<float>
    {
        public ScaleXOperation(Transform transform, float expectedX, float duration, Easing easing) 
            : base( getter: () => transform.localScale.x,
                    setter: (val) => {
                        Vector3 scale = transform.localScale;
                        scale.x = val;
                        transform.localScale = scale;
                    },
                    interpolator: (a,b,t) => Mathf.Lerp(a,b,t),
                    expectedValue: expectedX,
                    duration,
                    easing,
                    target: transform )
        { }
    }

	public class ScaleYOperation : PropertyOperation<float>
	{
		public ScaleYOperation(Transform transform, float expectedY, float duration, Easing easing)
			: base(getter: () => transform.localScale.y,
					setter: (val) => {
						Vector3 scale = transform.localScale;
						scale.y = val;
						transform.localScale = scale;
					},
					interpolator: (a, b, t) => Mathf.Lerp(a, b, t),
					expectedValue: expectedY,
					duration,
					easing,
					target: transform)
		{ }
	}

	public class ScaleZOperation : PropertyOperation<float>
	{
		public ScaleZOperation(Transform transform, float expectedZ, float duration, Easing easing)
			: base(getter: () => transform.localScale.z,
					setter: (val) => {
						Vector3 scale = transform.localScale;
						scale.z = val;
						transform.localScale = scale;
					},
					interpolator: (a, b, t) => Mathf.Lerp(a, b, t),
					expectedValue: expectedZ,
					duration,
					easing,
					target: transform)
		{ }
	}
}
