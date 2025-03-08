using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public Dictionary<CollectableBase.collectableTypes, int> inventory = new Dictionary<CollectableBase.collectableTypes, int>();
    public static InventorySystem instance;
    void Awake()
    {
          instance = this;
    }
    public void AddItem(CollectableBase.collectableTypes type,int amount)
    {
       
        if (inventory.ContainsKey(type))
        {
            inventory[type]= inventory[type]+amount;
        }
      
        else
        {
            inventory.Add(type, amount);
        }

        Debug.Log(type + " envantere eklendi. Şu anki sayısı: " + inventory[type]);
    }
    public void RemoveItem(CollectableBase.collectableTypes type,int amount)
    {
        if (inventory.ContainsKey(type))
        {
            inventory[type] = inventory[type] - amount;
            if (inventory[type] <= 0)
            {
                inventory.Remove(type);
            }

            Debug.Log(type + " çıkarıldı. Kalan sayısı: " + (inventory.ContainsKey(type) ? inventory[type].ToString() : "0"));
        }
        else
        {
            Debug.Log(type + " envanterde bulunamadı.");
        }
    }
    public bool HasEnoughAmount(float amount)
    {
      if(inventory.ContainsKey(CollectableBase.collectableTypes.Log) && inventory[CollectableBase.collectableTypes.Log]>= amount)
            return true;
      else return false;
    }

    public int GetItemCount(CollectableBase.collectableTypes type)
    {
        return inventory.ContainsKey(type) ? inventory[type] : 0;
    }

    public InventoryData GetInventoryData()
    {
        InventoryData data = new InventoryData();
        foreach (var item in inventory)
        {
            data.inventory.Add(new InventoryItem(item.Key, item.Value));
        }
        return data;
    }

    public void LoadInventoryData(InventoryData data)
    {
        inventory.Clear();
        foreach (var item in data.inventory)
        {
            CollectableBase.collectableTypes type = (CollectableBase.collectableTypes)System.Enum.Parse(typeof(CollectableBase.collectableTypes), item.collectableType);
            inventory[type] = item.amount;
        }
    }

    public void SaveInventory(string filePath)
    {
        InventoryData data = GetInventoryData();
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log($"Inventory saved to {filePath}");
    }

    public void LoadInventory(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            LoadInventoryData(data);
            Debug.Log($"Inventory loaded from {filePath}");
        }
        else
        {
            Debug.LogWarning($"Save file not found at {filePath}");
        }
    }
}
