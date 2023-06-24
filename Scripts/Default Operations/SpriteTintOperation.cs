using UnityEngine;

namespace Hibzz.Dropl.DefaultOperations
{
    public class SpriteTintOperation : PropertyOperation<Color>
    {
        public SpriteTintOperation(SpriteRenderer renderer, Color expectedColor, float duration, Easing easing)
            : base( getter: () => renderer.color,
                    setter: (color) => {
                        color.a = renderer.color.a;
                        renderer.color = color;
                    },
                    interpolator: (a, b, t) => Color.Lerp(a, b, t),
                    expectedValue: expectedColor,
                    duration,
                    easing,
                    target: renderer)
        { }
    }
}
