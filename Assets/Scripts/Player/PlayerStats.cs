using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public Vector3 chopAreaSize;
    public float walkSpeed;
    public float chopStrenght;

    public List<UpgradeData> upgrades;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }
    private void Start()
    {
        // initial attributes
        upgrades = new List<UpgradeData>
        {
            new UpgradeData("walkSpeed", 1, 15, 1.4f,10, "Player walks 1.4x faster!"),
            new UpgradeData("chopStrength", 1, 17, 1.3f,19, "Chop trees down faster!"),
            new UpgradeData("chopAreaSize",1,20,2.5f,6, "Chop more trees at once "),
            new UpgradeData("truckPos",1,50,2f,4, "Move the truck forward but first make sure to clear the way to reach it!"),
            new UpgradeData("backpackSize",1,12,2f, 10, "Carry 2x more log in your backpack!")
        };
    }

    public UpgradeData GetUpgrade(string upgradeName)
    {
        var upgrade = upgrades.Find(upg => upg.upgradeName == upgradeName);
        if (upgrade == null)
        {
            Debug.LogWarning($"Upgrade '{upgradeName}' bulunamadi.");
        }
        return upgrade;
    }

    public void SavePlayerStats(string filePath)
    {
        PlayerStatsData playerStatsData = new PlayerStatsData(this);
        playerStatsData.upgrades = upgrades;

        string json = JsonUtility.ToJson(playerStatsData, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Player stats saved.");
    }

    public void LoadPlayerStats(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerStatsData playerStatsData = JsonUtility.FromJson<PlayerStatsData>(json);

            walkSpeed = playerStatsData.walkSpeed;
            chopStrenght = playerStatsData.chopStrength;
            chopAreaSize = playerStatsData.chopAreaSize;

            if (playerStatsData.upgrades == null)  
            {
                Debug.LogWarning("Upgrades list is null, initializing it.");
                playerStatsData.upgrades = new List<UpgradeData>();
            }

            upgrades.Clear();

            foreach (var upgrade in playerStatsData.upgrades)
            {
                upgrades.Add(upgrade);  
            }

            Debug.Log("Player stats loaded.");
        }
        else
        {
            Debug.LogWarning("Player stats file not found.");
        }
    }


}
