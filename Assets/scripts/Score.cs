using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KillScore
{
    public static int killScore = 0;
    public static int Level = 0;
    public static float rotateLevel;
    public static int damageLevel;
    public static int jumpLevel;
    public static bool barrier;


public static void Reset()
    {
        killScore = 0;
        Level = 0;
        rotateLevel = 1f;
        damageLevel = 1;
        jumpLevel = 1;
        barrier = false;
    }


}
