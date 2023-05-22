using UnityEngine;

namespace Hibzz.Dropl
{
    public static class Easing
    {
        public static float Linear(float t) 
        { 
            return t; 
        }

        public static float InSine(float t)
        {
			return Mathf.Sin(Mathf.PI / 2f * (t - 1f)) + 1f;
		}

        public static float OutSine(float t)
        {
			return Mathf.Sin(t * (Mathf.PI / 2f));
		}

        public static float InOutSine(float t)
        {
		    return (Mathf.Sin(Mathf.PI * (t - 0.5f)) + 1f) * 0.5f;
		}

        public static float InQuad(float t)
        {
            return t * t;
        }

        public static float OutQuad(float t)
        {
            return t * (2f - t);
        }

        public static float InOutQuad(float t)
        {
			t *= 2f;
			if (t < 1f)
			{
				return t * t * 0.5f;
			}

			return -0.5f * ((t - 1f) * (t - 3f) - 1f);
		}

		public static float InCubic(float t)
		{
			return InPower(t, 3);
		}

		public static float OutCubic(float t)
		{
			return OutPower(t, 3);
		}

		public static float InOutCubic(float t)
		{
			return InOutPower(t, 3);
		}

		public static float InPower(float t, int power)
		{
			return Mathf.Pow(t, power);
		}

		public static float OutPower(float t, int power)
		{
			int num = (power % 2 != 0) ? 1 : (-1);
			return num * (Mathf.Pow(t - 1f, power) + num);
		}

		public static float InOutPower(float t, int power)
		{
			t *= 2f;
			if (t < 1f)
			{
				return InPower(t, power) * 0.5f;
			}

			int num = (power % 2 != 0) ? 1 : (-1);
			return num * 0.5f * (Mathf.Pow(t - 2f, power) + (num * 2));
		}

		public static float InBounce(float t)
		{
			return 1f - OutBounce(1f - t);
		}

		public static float OutBounce(float t)
		{
			if (t < 0.363636374f)
			{
				return 7.5625f * t * t;
			}

			if (t < 0.727272749f)
			{
				float num = (t -= 0.545454562f);
				return 7.5625f * num * t + 0.75f;
			}

			if (t < 0.909090936f)
			{
				float num2 = (t -= 0.8181818f);
				return 7.5625f * num2 * t + 0.9375f;
			}

			float num3 = (t -= 21f / 22f);
			return 7.5625f * num3 * t + 63f / 64f;
		}

		public static float InOutBounce(float t)
		{
			if (t < 0.5f)
			{
				return InBounce(t * 2f) * 0.5f;
			}

			return OutBounce((t - 0.5f) * 2f) * 0.5f + 0.5f;
		}

		public static float InElastic(float t)
		{
			if (t == 0f)
			{
				return 0f;
			}

			if (t == 1f)
			{
				return 1f;
			}

			float num = 0.3f;
			float num2 = num / 4f;
			float num3 = Mathf.Pow(2f, 10f * (t -= 1f));
			return 0f - num3 * Mathf.Sin((t - num2) * (Mathf.PI * 2f) / num);
		}

		public static float OutElastic(float t)
		{
			if (t == 0f)
			{
				return 0f;
			}

			if (t == 1f)
			{
				return 1f;
			}

			float num = 0.3f;
			float num2 = num / 4f;
			return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - num2) * (Mathf.PI * 2f) / num) + 1f;
		}

		public static float InOutElastic(float t)
		{
			if (t < 0.5f)
			{
				return InElastic(t * 2f) * 0.5f;
			}

			return OutElastic((t - 0.5f) * 2f) * 0.5f + 0.5f;
		}

		public static float InBack(float t)
		{
			float num = 1.70158f;
			return t * t * ((num + 1f) * t - num);
		}

		public static float OutBack(float t)
		{
			return 1f - InBack(1f - t);
		}

		public static float InOutBack(float t)
		{
			if (t < 0.5f)
			{
				return InBack(t * 2f) * 0.5f;
			}

			return OutBack((t - 0.5f) * 2f) * 0.5f + 0.5f;
		}

		public static float InBack(float t, float s)
		{
			return t * t * ((s + 1f) * t - s);
		}

		public static float OutBack(float t, float s)
		{
			return 1f - InBack(1f - t, s);
		}

		public static float InOutBack(float t, float s)
		{
			if (t < 0.5f)
			{
				return InBack(t * 2f, s) * 0.5f;
			}

			return OutBack((t - 0.5f) * 2f, s) * 0.5f + 0.5f;
		}

		public static float InCirc(float t)
		{
			return 0f - (Mathf.Sqrt(1f - t * t) - 1f);
		}

		public static float OutCirc(float t)
		{
			t -= 1f;
			return Mathf.Sqrt(1f - t * t);
		}

		public static float InOutCirc(float t)
		{
			t *= 2f;
			if (t < 1f)
			{
				return -0.5f * (Mathf.Sqrt(1f - t * t) - 1f);
			}

			t -= 2f;
			return 0.5f * (Mathf.Sqrt(1f - t * t) + 1f);
		}

		public static float Apply(float t, Type method)
        {
            switch (method)
            {
                case Type.LINEAR:         return Linear(t);
                case Type.IN_SINE:        return InSine(t);
                case Type.OUT_SINE:       return OutSine(t);
                case Type.IN_OUT_SINE:    return InOutSine(t);
                case Type.IN_QUAD:        return InQuad(t);
                case Type.OUT_QUAD:       return OutQuad(t);
                case Type.IN_OUT_QUAD:    return InOutQuad(t);
                case Type.IN_CUBIC:       return InCubic(t);
                case Type.OUT_CUBIC:      return OutCubic(t);
                case Type.IN_OUT_CUBIC:   return InOutCubic(t);
				case Type.IN_BOUNCE:      return InBounce(t);
				case Type.OUT_BOUNCE:     return OutBounce(t);
				case Type.IN_OUT_BOUNCE:  return InOutBounce(t);
				case Type.IN_ELASTIC:     return InElastic(t);
				case Type.OUT_ELASTIC:    return OutElastic(t);
				case Type.IN_OUT_ELASTIC: return InOutElastic(t);
				case Type.IN_BACK:        return InBack(t);
				case Type.OUT_BACK:       return OutBack(t);
				case Type.IN_OUT_BACK:    return InOutBack(t);
				case Type.IN_CIRC:        return InCirc(t);
				case Type.OUT_CIRC:       return OutCirc(t);
				case Type.IN_OUT_CIRC:    return InOutCirc(t);
                default:                  return 0;
            }
        }

        public static float Apply(float t, Type method, int power)
        {
            switch (method)
            {
				case Type.IN_POWER:     return InPower(t, power);
				case Type.OUT_POWER:    return OutPower(t, power);
				case Type.IN_OUT_POWER: return InOutPower(t, power);
				default:                return Apply(t, method);
            }
        }

        public enum Type
        {
            LINEAR,
            IN_SINE,
            OUT_SINE,
            IN_OUT_SINE,
            IN_QUAD,
            OUT_QUAD,
            IN_OUT_QUAD,
            IN_CUBIC,
            OUT_CUBIC,
            IN_OUT_CUBIC,
            IN_POWER,
            OUT_POWER,
            IN_OUT_POWER,
			IN_BOUNCE,
			OUT_BOUNCE,
			IN_OUT_BOUNCE,
			IN_ELASTIC,
			OUT_ELASTIC,
			IN_OUT_ELASTIC,
			IN_BACK,
			OUT_BACK,
			IN_OUT_BACK,
			IN_CIRC,
			OUT_CIRC,
			IN_OUT_CIRC
        }
    }
}
