using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods 
{
    public static bool IsDestroyed(this Object obj)
    {
        // Check if the object is null or if it has been destroyed
        return obj == null || obj.Equals(null);
    }
}
