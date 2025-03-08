using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : CollectableBase
{
    [SerializeField] private int  weight; 
    public override void Collected()
    {
        bool canCollect = BackpackSystem.instance.AddItem(collectableTypes.Egg, weight);
        if (canCollect)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(AnimateCollect());
            UIScript.instance.UpdateBackpackUI(); 
        }
    }

}
