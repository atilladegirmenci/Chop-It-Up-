using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TreeBase : MonoBehaviour
{
    public treeTypes treeType;
    public Material treeColour;
    protected int logAmount;
    public float treeHp;
    public GameObject root;

    public List<CollectableDrop> drops = new List<CollectableDrop>();
    public enum treeTypes
    {
        Tree1,
        Tree2,
        Tree3,
        tree4,
        tree5,
        tree6
    }
    
    public int SetLogAmount()
    {
        switch (treeType)
        {
            case treeTypes.Tree1:
                logAmount = 1;
                break;
            case treeTypes.Tree2:
                logAmount = 2;
                break;
            case treeTypes.Tree3:
                logAmount = 3;
                break;
            case treeTypes.tree4:
                logAmount = 4;
                break;
            case treeTypes.tree5:
                logAmount = 5;
                break;
            case treeTypes.tree6:
                logAmount = 6;  
                break;
            default:
                logAmount = 0;
                break;
        }
        return logAmount;
    }
    public virtual void GetHit(float damage) { }
    public virtual void GetDamage(float damage) { }
    public virtual void CheckForHp() { }
    public void Die()
    {
        Destroy(gameObject);

        foreach (CollectableDrop drop in drops)
        {
            CollecableSpawner.instance.SpawnCollectable(transform.position, drop.type , drop.amount);
        }

        Instantiate(root,transform.position,transform.rotation * Quaternion.Euler(0, Random.Range(0f, 360f), 0));
    }

}
