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

    public static class DebrisAnimationHash{
        public static int Debris_Unbreak = Animator.StringToHash("debris_idle_0");
        public static int Debris_Break = Animator.StringToHash("debris_break_0");
        public static int Debris_Broken = Animator.StringToHash("debris_broken_0");
    }

    public static class MechanicLiftHash{
        public static int Stay_Up = Animator.StringToHash("mechanic_lift_stay_up_0");
        public static int Stay_Down = Animator.StringToHash("mechanic_lift_stay_down_0");
        public static int Go_Up = Animator.StringToHash("mechanic_lift_go_up_0");
        public static int Go_Down = Animator.StringToHash("mechanic_lift_go_down_0");
    }

    public static class MagicBall{
        public static int Fly = Animator.StringToHash("magic_ball_fly");
        public static int Hit = Animator.StringToHash("magic_ball_hit");
    }
}
