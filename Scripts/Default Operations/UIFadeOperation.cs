using UnityEngine;
using UnityEngine.UI;

namespace Hibzz.Dropl.DefaultOperations
{
    public class UIFadeOperation : PropertyOperation<float>
    {
        public UIFadeOperation(Image image, float expectedAlpha, float duration, Easing easing)
            : base( getter: () => image.color.a,
                    setter: (alpha) => {
                        var color = image.color;
                        color.a = alpha;
                        image.color = color;
                    },
                    interpolator: (a, b, t) => Mathf.Lerp(a, b, t),
                    expectedValue: expectedAlpha,
                    duration,
                    easing,
                    target: image )
        { }
    }
}
