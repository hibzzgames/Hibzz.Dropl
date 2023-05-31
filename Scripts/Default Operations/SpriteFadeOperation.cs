using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.Dropl
{
    public class SpriteFadeOperation : PropertyOperation<float>
    {
        public SpriteFadeOperation(SpriteRenderer renderer, float expectedAlpha, float duration, Easing easing)
            : base( getter: () => renderer.color.a,
                    setter: (alpha) => {
                        var color = renderer.color;
                        color.a = alpha;
                        renderer.color = color;
                    },
                    interpolator: (a, b, t) => Mathf.Lerp(a, b, t),
                    expectedValue: expectedAlpha,
                    duration,
                    easing,
                    target: renderer )
        { }
    }
}
