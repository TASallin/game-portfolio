using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityEngineUtil
{
    public static Transform RecursiveFind(this Transform t, string child) {
        Transform found = t.Find(child);

        if (found != null) return found;

        foreach(Transform c in t) {
            found = c.RecursiveFind(child);
            if (found != null) return found;

        }

        return null;
    }
}
