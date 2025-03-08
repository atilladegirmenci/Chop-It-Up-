using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{ 
    static public Sound instance;

    [SerializeField] private AudioSource treeChopSound;
    [SerializeField] private AudioSource collectSound;
    
    void Awake()
    {
      instance = this; 
    }
   

    public void TreeChopSound()
    {
        treeChopSound.Play();
    }
    public void CollectSound()
    {
        collectSound.Play();
    }
}
