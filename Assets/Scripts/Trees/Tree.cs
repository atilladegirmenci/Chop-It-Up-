using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : TreeBase
{
    public static Tree instance;

    
    [SerializeField] public Image healthBar;
    private float maxTreeHp;
    private Transform playerTransform;

    public float fastRotationAngle;
    public float fastRotationSpeed;
    public float slowRotationSpeed;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        maxTreeHp = treeHp;
        playerTransform = PlayerMovement.instance.transform;
    }
    private void Update()
    {
       if(healthBar.gameObject.activeSelf == true)
       {
            Vector3 targetPos = playerTransform.position;
            Vector3 direction = targetPos - healthBar.transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f) 
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                healthBar.transform.rotation = targetRotation;
            }
        }
    }
  
    public override void GetHit(float damage)
    {
        GetDamage(damage);
        StartCoroutine(RotateOnHit());
       
    }
    public override void GetDamage(float damage)
    {
        treeHp -= damage;
        CheckForHp();
    }

    public override void CheckForHp()
    {
        if (treeHp<= 0 )
        {
            Die();
        }

        if (treeHp < maxTreeHp) { healthBar.gameObject.SetActive(true); }

        healthBar.fillAmount = Mathf.Clamp(treeHp / maxTreeHp, 0, 1);
        
    }
   
     private IEnumerator RotateOnHit()
     {
            
            float currentAngle = 0f;
            while (currentAngle > fastRotationAngle)
            {
                float rotationStep = fastRotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.back, rotationStep);
                currentAngle -= rotationStep;
                yield return null;
            }

            
            while (currentAngle < 0)
            {
                float rotationStep = slowRotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward, rotationStep);
                currentAngle += rotationStep;
                yield return null;
            }

     }

    

    
}
