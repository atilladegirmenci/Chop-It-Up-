using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject fences;
    [SerializeField] private GameObject roads;

    static public Checkpoint instance;


    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Vector3 newPos = TreeSpawner.Instance.NewSpawnPos();

            TreeSpawner.Instance.SpawnTrees();
            transform.position = transform.position + newPos;


          
            Instantiate(fences,fences.transform.position + newPos* (TreeSpawner.Instance.spawnedGroupAmount-1) , Quaternion.identity);
            Instantiate(roads,roads.transform.position+ newPos* (TreeSpawner.Instance.spawnedGroupAmount-1) , Quaternion.identity);

           
        }
    }
    public void InitialSpawn()
    {
        Instantiate(gameObject, new Vector3(-9, 0, -37), transform.rotation);
    }
    
    
}
