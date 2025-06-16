using System;
using System.Collections.Generic;

[Serializable]
public class ShopData
{
    public int trickPoints;
    public string equippedHatName;
    public string equippedBodyName;
    public List<HatData> ownedHats = new List<HatData>();
    public List<BodyData> ownedBodies = new List<BodyData>();
}

[Serializable]
public class HatData
{
    public string hatName;
    public bool purchased;
}

[Serializable]
public class BodyData
{
    public string bodyName;
    public bool purchased;
}