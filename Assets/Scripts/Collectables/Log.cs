using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : CollectableBase
{
    [SerializeField] private int weight;
    public override void Collected()
    {
        bool canCollect = BackpackSystem.instance.AddItem(collectableTypes.Log, weight);
        if (canCollect)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false; 
            StartCoroutine(AnimateCollect());
            UIScript.instance.UpdateBackpackUI();
        }
    }

   

}
