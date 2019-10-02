using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    /*
     Wrapper around GetComponent, throws exception if component is not available
    */
    public static T GetComponentMandatory<T>(GameObject gObj)
    {
        T comp = gObj.GetComponent<T>();
        if (comp == null) throw new System.Exception(gObj.name + " is missing component " + comp.GetType().Name);
        return comp;
    }
}