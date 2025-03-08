using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public static TreeSpawner Instance;

    public List<GameObject> treePrefabs;
    public int treeAmountOnX;
    public int treeAmountOnZ;
    public float spacingX;
    public float spacingZ;
    private Color gizmoColor = Color.blue;
    public int spawnedGroupAmount;
    private Vector3[,] gridPositions;
    public Vector3 groupOffset = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
       
    }

    public void SpawnTrees()
    {
        groupOffset = new Vector3(0, 0, spawnedGroupAmount * treeAmountOnZ * spacingZ);

        if (gridPositions == null || gridPositions.GetLength(0) != treeAmountOnX || gridPositions.GetLength(1) != treeAmountOnZ)
        {
            CalculateGridPositions();
        }

        for (int x = 0; x < treeAmountOnX; x++)
        {
            for (int z = 0; z < treeAmountOnZ; z++)
            {
                Vector3 spawnPosition = gridPositions[x, z] + groupOffset;

                GameObject selectedTreePrefab = SelectTreePrefab();

                GameObject spawnedTree = Instantiate(selectedTreePrefab, spawnPosition, Quaternion.identity);

                float randomTiltX = Random.Range(-5f, 5f); 
                float randomTiltZ = Random.Range(-5f, 5f); 
                spawnedTree.transform.rotation = Quaternion.Euler(randomTiltX, 0, randomTiltZ);

                float randomScale = Random.Range(0.6f, 0.7f);
                spawnedTree.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            }
        }

        spawnedGroupAmount++;
    }
    private GameObject SelectTreePrefab()
    {
        return treePrefabs[spawnedGroupAmount];
    }

    void CalculateGridPositions()
    {
        gridPositions = new Vector3[treeAmountOnX, treeAmountOnZ];
        for (int x = 0; x < treeAmountOnX; x++)
        {
            for (int z = 0; z < treeAmountOnZ; z++)
            {
                float xPos = x * spacingX + Random.Range(-0.3f, 0.3f);
                float zPos = z * spacingZ + Random.Range(-0.3f, 0.3f);
                float yPos = 0.0f;
                gridPositions[x, z] = new Vector3(xPos, yPos, zPos) + transform.position;
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        if (gridPositions == null || gridPositions.GetLength(0) != treeAmountOnX || gridPositions.GetLength(1) != treeAmountOnZ)
        {
            CalculateGridPositions();
        }

        for (int x = 0; x < treeAmountOnX; x++)
        {
            for (int z = 0; z < treeAmountOnZ; z++)
            {
                Vector3 position = gridPositions[x, z];
                Gizmos.DrawWireCube(position, Vector3.one * 0.5f);
            }
        }
    }

    public Vector3 NewSpawnPos()
    {
        return new Vector3(0, 0, treeAmountOnZ * spacingZ);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("SpawnedGroupAmounts", spawnedGroupAmount);
        PlayerPrefs.Save(); 
        Debug.Log($"spawnedGroupAmounts kaydedildi: {spawnedGroupAmount}");
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("SpawnedGroupAmounts"))
        {
            spawnedGroupAmount = PlayerPrefs.GetInt("SpawnedGroupAmounts");
            Debug.Log($"spawnedGroupAmounts yüklendi: {spawnedGroupAmount}");
        }
        else
        {
            spawnedGroupAmount = 0;
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey("SpawnedGroupAmounts");
        Debug.Log("spawnedGroupAmounts sıfırlandı.");
    }
}
