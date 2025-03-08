using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public string upgradeName;
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpgradePanel.instance.ChangeDescription(upgradeName);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       UpgradePanel.instance.ChangeDescription("");
    }
}
