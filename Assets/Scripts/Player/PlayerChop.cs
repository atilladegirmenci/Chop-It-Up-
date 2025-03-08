using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChop : MonoBehaviour
{
    [SerializeField] float maxDistance;
    int layerMask;
    
    void Start()
    {
        layerMask = LayerMask.GetMask("Trees");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           StartCoroutine( Chop());
        }
      
    }
    private IEnumerator Chop()
    {
        RaycastHit[] trees = ChopArea();
        foreach (RaycastHit hit in trees)
        {
            if (hit.collider.TryGetComponent<TreeBase>(out TreeBase tree))
            {
                if (tree != null)
                {
                    Sound.instance.TreeChopSound();
                    tree.GetHit(PlayerStats.instance.chopStrenght);
                    yield return new WaitForSeconds(0.09f);
                }
            }
        }
    }
    public RaycastHit[] ChopArea()
    {
        RaycastHit[] hit =  Physics.BoxCastAll(transform.position, PlayerStats.instance.chopAreaSize, transform.forward, transform.rotation, maxDistance,layerMask);
        return hit;
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (PlayerStats.instance != null )
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * (maxDistance / 2), transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, PlayerStats.instance.chopAreaSize * 2);
        }
        

    }

}
