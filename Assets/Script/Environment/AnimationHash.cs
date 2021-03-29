using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHash
{
    public static class LeverDoorAnimationHash{
        public static int OPEN = Animator.StringToHash("open");
        public static int CLOSE = Animator.StringToHash("close");
        public static int STAY_OPEN = Animator.StringToHash("stayOpen");
        public static int STAY_CLOSE = Animator.StringToHash("stayClose");
    }

    public static class LeverAnimationHash{
        public static int ON = Animator.StringToHash("on");
        public static int OFF = Animator.StringToHash("off");
        public static int STAY_ON = Animator.StringToHash("stayOn");
        public static int STAY_OFF = Animator.StringToHash("stayOff");
    }
}
