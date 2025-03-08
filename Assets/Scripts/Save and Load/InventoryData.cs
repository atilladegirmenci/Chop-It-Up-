using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    
    public List<InventoryItem> inventory;

    public InventoryData()
    {
        inventory = new List<InventoryItem>();
    }
}

[System.Serializable]
public class InventoryItem 
{
    public string collectableType; 
    public int amount;

    public InventoryItem(CollectableBase.collectableTypes type, int amount)
    {
        collectableType = type.ToString(); 
        this.amount = amount;
    }
}

