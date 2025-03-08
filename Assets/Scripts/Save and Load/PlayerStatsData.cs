using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStatsData 
{
    public float walkSpeed;
    public float chopStrength;
    public Vector3 chopAreaSize;
    public List<UpgradeData> upgrades;
    public PlayerStatsData(PlayerStats playerStats)
    {
        walkSpeed = playerStats.walkSpeed;
        chopStrength = playerStats.chopStrenght;
        chopAreaSize = playerStats.chopAreaSize;
        upgrades = new List<UpgradeData>(playerStats.upgrades);
    }
}
