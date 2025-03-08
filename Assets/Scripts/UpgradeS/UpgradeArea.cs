using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private UpgradePanel upgradePanel;
    

    public static UpgradeArea instance;
    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        text.SetActive(false);
        upgradePanel = GameManager.instance.setUpgradePanel();
    }

    // Update is called once per frame
    void Update()
    {
        if(text.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            OpenUpgradeMenu();
        }
    }

    //public void SetUpgradePanel(UpgradePanel panel)
    //{
    //    upgradePanel = panel;
    //}
    private void OpenUpgradeMenu()
    {
        
        Cursor.lockState = CursorLockMode.None;
        upgradePanel.gameObject.SetActive(true);
        Cursor.visible = true;
    }
    public IEnumerator movePos()
    {
        Vector3 dest = transform.position + TreeSpawner.Instance.NewSpawnPos();
        while (Vector3.Distance(transform.position, dest) > 0.1f)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, dest, 3 * Time.deltaTime);
            yield return null; 
        }
        //transform.position = Vector3.MoveTowards(transform.position, dest, 3 * Time.deltaTime);
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   

            text.SetActive(true);
            BackpackSystem.instance.TransferToInv();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            upgradePanel.turnOffPanel();
            text.SetActive(false);
        }
    }
  
}
