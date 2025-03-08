using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inventoryLogAmountText;
    [SerializeField] private TextMeshProUGUI inventoryEggAmountText;
    [SerializeField] private TextMeshProUGUI backpackLogAmountText;
    [SerializeField] private TextMeshProUGUI backpackEggAmountText;
    [SerializeField] private Image backpackFilledImage;
    [SerializeField] private GameObject pauseMenuPanel;

    [Header("NOTIFICATION")]
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float notificDisplayDuration = 2f;
    [SerializeField] public float moveDuration;
    [SerializeField] public float offscreenPositionX;
    [SerializeField] public float onscreenPositionX;
    private Coroutine currentCoroutine;
    
    static public UIScript instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        UpdateBackpackUI();
        UpdateInventoryUI();


    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
       
    }

    public void UpdateBackpackUI()
    {
        int currentWeight = BackpackSystem.instance.currentWeight;
        int maxCapacity = BackpackSystem.instance.maxCapacity;

        backpackFilledImage.fillAmount = (float)currentWeight / maxCapacity;
        backpackFilledImage.color = (currentWeight == maxCapacity) ? new Color(0.75f, 0f, 0f) : Color.black;

        backpackLogAmountText.text = GetBackpackItemAmount(CollectableBase.collectableTypes.Log);

        backpackEggAmountText.text = GetBackpackItemAmount(CollectableBase.collectableTypes.Egg);
    }

    public void UpdateInventoryUI()
    {
        inventoryLogAmountText.text = GetInventoryItemAmount(CollectableBase.collectableTypes.Log);

        inventoryEggAmountText.text = GetInventoryItemAmount(CollectableBase.collectableTypes.Egg);
    }

    private string GetInventoryItemAmount(CollectableBase.collectableTypes type)
    {
        return ": " + InventorySystem.instance.GetItemCount(type).ToString();
    }

    private string GetBackpackItemAmount(CollectableBase.collectableTypes type)
    {
        return BackpackSystem.instance.backpack.ContainsKey(type)
            ? $":{BackpackSystem.instance.backpack[type].CurrentAmount.ToString()}" 
            : ":0";
    }
    public void ShowNotification(string message)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(SlideNotification(message));
    }

    private IEnumerator SlideNotification(string message)
    {
        notificationText.text = message;

        float elapsedTime = 0f;
        Vector2 startPosition = new Vector2(offscreenPositionX, notificationText.rectTransform.anchoredPosition.y);
        Vector2 targetPosition = new Vector2(onscreenPositionX, notificationText.rectTransform.anchoredPosition.y);
        
        //slide in
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            notificationText.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }

        //wait
        yield return new WaitForSeconds(notificDisplayDuration);

        // slide out
        elapsedTime = 0f;
        startPosition = notificationText.rectTransform.anchoredPosition;
        targetPosition = new Vector2(offscreenPositionX, notificationText.rectTransform.anchoredPosition.y);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            notificationText.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }

        notificationText.text = "";

        currentCoroutine = null;
       
    }
}
