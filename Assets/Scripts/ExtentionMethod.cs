using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class ExtentionMethod
{
    public static bool IsPlayer(this Collider collider)
    {
        return collider.CompareTag("Player");
    }

    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    public static int[] ToArray(this Vector3Int v3i)
    {
        return new int[3] { v3i.x, v3i.y, v3i.z };
    }

    public static LayerMask ToLayerMask(this int layer)
    {
        return 1 << layer;
    }

    public static LayerMask AddLayerMask(this LayerMask before, LayerMask toAdd)
    {
        return before | toAdd;
    }

    public static Vector3[] ToVector3Array(this Vector2[] vector2s, float z)
    {
        List<Vector3> vector3s = new List<Vector3>();

        foreach (Vector2 vector2 in vector2s)
        {
            vector3s.Add(new Vector3(vector2.x, vector2.y, z));
        }

        return vector3s.ToArray();
    }
}
