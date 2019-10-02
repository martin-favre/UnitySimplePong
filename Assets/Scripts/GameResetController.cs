using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class GameResetController
{
    static List<Action> resetFunctions = new List<Action>();
    public static void Reset()
    {
        foreach(Action resetFunc in resetFunctions)
        {
            resetFunc();
        }
    }

    public static void RegisterResetFunction(Action resetFunc)
    {
        resetFunctions.Add(resetFunc);
    }

}
