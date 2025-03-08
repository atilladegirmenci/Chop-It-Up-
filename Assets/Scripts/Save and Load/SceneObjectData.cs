using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SceneObjectData
{ 
    public string objectID;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public bool isCollected;
    public string prefabName;
}
[System.Serializable]
public class SceneData
{
    public List<SceneObjectData> objects;
}