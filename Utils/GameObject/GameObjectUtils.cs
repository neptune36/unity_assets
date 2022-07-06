using System.Collections.Generic;
using UnityEngine;

public class GameObjectUtils {

    public static void DeleteChildren(Transform t)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in t)
        {
            children.Add(child.gameObject);
        }
        children.ForEach(child => Object.Destroy(child));
    }
}
