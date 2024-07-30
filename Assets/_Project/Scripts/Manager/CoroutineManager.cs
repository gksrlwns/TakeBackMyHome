using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager
{
    static Dictionary<float, WaitForSeconds> waitDict = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds DelaySeconds(float seconds)
    {
        if (!waitDict.TryGetValue(seconds, out var wait))
        {
            wait = new WaitForSeconds(seconds);
            waitDict[seconds] = wait;
        }

        return wait;
    }
}
