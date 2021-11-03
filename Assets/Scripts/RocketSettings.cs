using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RocketSettings : ScriptableObject
{
    public float StartForce;
    public Quaternion StartRotation;
    public Vector3 LookAtPos;
    public Vector3 RocketPos;
    public List<Path> previousPaths;
}

[System.Serializable]
public class Path
{
    public Vector3[] Points;
}
