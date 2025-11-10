using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KillScore
{
    public static int Level = 0;
    public static float rotateLevel;
    public static int damageLevel;
    public static int jumpLevel;
    public static bool barrier;

    public static int killScore = 0;
    public static float timeScore = 0;
    public static int highScore = 0;
    public static int LifeOrBullet = 10;
    public static int Combo = 0;

    public static int ComboScore = 0;
    public static int MaxCombo = 0;
    public static int first = 0;
    public static int second = 0;
    public static int third = 0;

    public static void Reset()
    {
        Level = 0;
        rotateLevel = 1f;
        damageLevel = 1;
        jumpLevel = 1;
        barrier = false;

        killScore = 0;
        timeScore = 0;
        LifeOrBullet = 10;
        Combo = 0;
        ComboScore = 0;
        MaxCombo = 0;
    }

    public static void ComboReset()
    {
        if (MaxCombo < Combo) {
            MaxCombo = Combo;
        }
        Combo = 0;
    }


}
