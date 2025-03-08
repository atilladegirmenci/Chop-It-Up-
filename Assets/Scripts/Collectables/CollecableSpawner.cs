using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject log;
    [SerializeField] private GameObject egg;
    //[SerializeField] private int woodAmount;
    private GameObject spawnedWood;
    public static CollecableSpawner instance;
    void Start()
    {
        instance = this;  
    }

    void Update()
    {
        
    }

    //public void SpawnCollectable(Vector3 pos, int _amount)
    //{
    //    spawnedWood = Instantiate(log, pos + new Vector3(0,1,0), transform.rotation);
    //    if(spawnedWood.TryGetComponent<Log>(out Log l))
    //    {
    //        l.amount = _amount;
    //    }
    //}
    public void SpawnCollectable(Vector3 position, CollectableBase.collectableTypes type, int amount)
    {
        GameObject prefab = null;

        switch (type)
        {
            case CollectableBase.collectableTypes.Log:
                prefab = log;
                break;
            case CollectableBase.collectableTypes.Egg:
                prefab = egg;
                break;
        }

        if (prefab != null)
        {
            for (int i = 0; i < amount; i++) 
            {
                Instantiate(prefab, position + new Vector3(0,1,0) /*new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f))*/, Quaternion.identity);
            }
        }
    }
}
