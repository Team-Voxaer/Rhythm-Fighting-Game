using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonParameter
{
    // skills and combo
    public static int comboThreshold = 10;

    public static string[] midiFiles = { "Never_Gonna_Give_You_Up_Kindergarten.mid", 
                                         "Never_Gonna_Give_You_Up.mid", 
                                         "All_The_Way_North_Simplified.mid" };

    public static class Skill {
        public static int none = 0;
        public static int one = 1;
    }

    
    public static class Debuff {
        public static int none = 0;
        public static int one = 1;
    }

    public static class Buff {
        public static int none = 0;
        public static int autoPlay = 1;
    }

    // public static class KeyCode {
    //     // TODO: to survey how to represent KeyCode
    //     public static KeyCode LeftSkill = KeyCode.D;
    //     public static KeyCode RightSkill = KeyCode.L;

    // }


}
