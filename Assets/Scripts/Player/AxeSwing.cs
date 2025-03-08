using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    [SerializeField] public float startAngle ;
    [SerializeField] public float endAngle ;   
    [SerializeField] public float swingDuration; 
    [SerializeField] public float returnDuration; 

    private bool isSwinging = false; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            StartCoroutine(AxeSwingAnimation());
        }
    }

    private IEnumerator AxeSwingAnimation()
    {
        isSwinging = true;

        float elapsedTime = 0f;
        while (elapsedTime < swingDuration)
        {
            float angle = Mathf.LerpAngle(startAngle, endAngle, elapsedTime / swingDuration);
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        elapsedTime = 0f;
        while (elapsedTime < returnDuration)
        {
            float angle = Mathf.LerpAngle(endAngle, startAngle, elapsedTime / returnDuration);
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0f, startAngle, 0f);

        isSwinging = false;
    }
}
