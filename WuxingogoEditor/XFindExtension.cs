using UnityEngine;
using System.Collections;
using UnityEngine;

public static class XFindExtension {

    public static string GetReleativePath(this Transform target)
    {
        string path = target.name;
        Transform parent = target.parent;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            target = parent;
            parent = target.parent;
        }
        return path;
    }

    
}
