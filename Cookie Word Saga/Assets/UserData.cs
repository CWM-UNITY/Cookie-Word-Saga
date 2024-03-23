using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public Dictionary<string, FacebookData> DicUserData = null;
}

[System.Serializable]
public class FacebookData
{
    public string ID;
    public string Name;
    public int Score;
}
