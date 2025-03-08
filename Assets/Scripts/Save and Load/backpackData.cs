using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackpackData 
{
    public List<BackpackItem> backpack;
    public BackpackData()
    {
        backpack = new List<BackpackItem>();
    }
}

[System.Serializable]
public class BackpackItem
{
    public string collectableType;
    public ItemData itemData;

    public BackpackItem(CollectableBase.collectableTypes type, ItemData data)
    {
        collectableType= type.ToString();
        itemData= data;
    }
}
