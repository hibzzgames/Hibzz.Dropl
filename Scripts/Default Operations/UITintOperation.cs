using UnityEngine;
using UnityEngine.UI;

namespace Hibzz.Dropl.DefaultOperations
{
    public class UITintOperation : PropertyOperation<Color>
    {
        public UITintOperation(Image image, Color expectedColor, float duration, Easing easing)
            : base( getter: () => image.color,
                    setter: (color) => {
                        color.a = image.color.a; // preserve alpha value, this operation just adjusts the tint
                        image.color = color;
                    },
                    interpolator: (a, b, t) => Color.Lerp(a, b, t),
                    expectedValue: expectedColor,
                    duration,
                    easing,
                    target: image )
        { }
    }
}
