using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.TryGetComponent<CollectableBase>(out CollectableBase collectBase))
        {
            collectBase.Collected();
            
        }


    }
}
