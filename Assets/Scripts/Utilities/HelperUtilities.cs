using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static bool ValidateCheckEmptyString(Object thisObject, string name, string stringToCheck)
    {
        if (stringToCheck != "") return false;
        Debug.Log(name + " is empty in object " + thisObject.name);
        return true;
    }

    public static bool ValidateEnumerableValues(Object thisObject, string name, IEnumerable enumerableToCheck)
    {
        var error = false;
        var count = 0;

        foreach (var item in enumerableToCheck)
        {
            if (item != null)
            {
                count++;
                continue;
            }
            Debug.Log(name + " has null values, object: " + thisObject.name);
            error = true;
        }

        if (count != 0) return error;
        Debug.Log(name + " has no values, object: " + thisObject.name);
        return true;
    }
}
