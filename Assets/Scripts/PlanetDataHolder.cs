using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDataHolder : SingletonBehaviour<PlanetDataHolder>
{
    public List<PlanetData> Data = new List<PlanetData>();
    public Material PlanetOrbitCircleMat, OrbiterCircleMat;
    public Texture2D[] planetTextures;
}

[System.Serializable]
public class PlanetData
{
    public Color Color;
    public LayerMask Layer;
    public LayerMask LayerMaskWithRocket => LayerMask.GetMask(LayerMask.LayerToName(Layer.value), "Rocket");
}
