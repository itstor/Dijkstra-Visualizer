using UnityEngine;
using System.Collections;

public class Anim: MonoBehaviour {
    public enum ease {
        easeOutBack,
    }

    public static float easeOutBack(float t, float b, float c, float d) {
        float s = 1.70158f;
        return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
    }
}