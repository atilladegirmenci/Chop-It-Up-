using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> sceneObjects;
    public bool isGameLoaded = false;
    [SerializeField] GameObject checkpoint;
    [SerializeField] GameObject upgradeArea;
    [SerializeField] private UpgradePanel upgradePanel;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        string inventoryFilePath = Path.Combine(Application.persistentDataPath, "inventory_" + sceneName + ".json");
        string backpackFilePath = Path.Combine(Application.persistentDataPath, "backpack_" + sceneName + ".json");
        string playerStatsFilePath = Path.Combine(Application.persistentDataPath, "playerStats_" + sceneName + ".json");
        string sceneFilePath = Path.Combine(Application.persistentDataPath, "scene_" + sceneName + ".json");

        if (System.IO.File.Exists(inventoryFilePath) && System.IO.File.Exists(backpackFilePath) && System.IO.File.Exists(playerStatsFilePath) && System.IO.File.Exists(sceneFilePath))
        {
            LoadSceneData(sceneName);
            PlayerMovement.instance.gameObject.transform.position = new Vector3(UpgradeArea.instance.transform.position.x + 1, PlayerMovement.instance.transform.position.y, UpgradeArea.instance.transform.position.z);
            Debug.Log("Save dosyaları bulundu, yükleme işlemi yapıldı.");
        }
        else
        {
            Debug.Log("Save dosyası bulunamadı, varsayılan ayarlarla başlanıyor.");
            InitialSpawns();
        }
    }
    public void NewGame()
    {
      //  PlayerPrefs.DeleteAll();
        string sceneName = SceneManager.GetActiveScene().name;

        string inventoryFilePath = Path.Combine(Application.persistentDataPath, "inventory_" + sceneName + ".json");
        string backpackFilePath = Path.Combine(Application.persistentDataPath, "backpack_" + sceneName + ".json");
        string playerStatsFilePath = Path.Combine(Application.persistentDataPath, "playerStats_" + sceneName + ".json");
        string sceneFilePath = Path.Combine(Application.persistentDataPath, "scene_" + sceneName + ".json");

        DeleteFileIfExists(inventoryFilePath);
        DeleteFileIfExists(backpackFilePath);
        DeleteFileIfExists(playerStatsFilePath);
        DeleteFileIfExists(sceneFilePath);

        TreeSpawner.Instance.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void DeleteFileIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Deleted file: {filePath}");
        }
        else
        {
            Debug.LogWarning($"File not found, could not delete: {filePath}");
        }
    }
    private void UpdateSceneObjectList()
    {
        sceneObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Saveable"));
    }
    private void InitialSpawns()
    {
        TreeSpawner.Instance.SpawnTrees();
        TreeSpawner.Instance.SpawnTrees();
       
        Instantiate(checkpoint, new Vector3(-9, 0, -37), transform.rotation);
        Instantiate(upgradeArea, new Vector3(-9.8f, 1.1f, -43), transform.rotation);
    }
    public UpgradePanel setUpgradePanel() { return upgradePanel; }
        
    public SceneData CollectSceneData()
    {
        UpdateSceneObjectList();
        SceneData data = new SceneData();
        data.objects = new List<SceneObjectData>();

        foreach (var obj in sceneObjects)
        {
            SceneObjectData objectData = new SceneObjectData
            {
                objectID = obj.name,
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                scale = obj.transform.localScale,
                isCollected = !obj.activeSelf,
                prefabName = obj.name.Contains("(Clone)") ? obj.name.Replace("(Clone)", "").Trim() : obj.name

            };

            data.objects.Add(objectData);

        }

        return data;
    }

    public void LoadSceneObjects(SceneData data)
    {

        if (sceneObjects == null || sceneObjects.Count == 0)
        {
            Debug.Log("sceneObjects list is empty. Loading from SceneData...");

            foreach (var objData in data.objects)
            {
                GameObject prefab = Resources.Load<GameObject>(objData.prefabName);

                if (prefab != null)
                {
                    GameObject obj = Instantiate(prefab, objData.position, objData.rotation);
                    obj.transform.localScale = objData.scale;
                    obj.SetActive(!objData.isCollected);  // Nesneyi toplanmış veya aktif olmayan olarak ayarla
                    obj.name = objData.objectID;
                    sceneObjects.Add(obj);
                }
                else
                {
                    Debug.LogError("Prefab not found. Updating data if it's not a prefab object: " + objData.prefabName);

                    GameObject obj = sceneObjects.Find(o => o.name == objData.objectID);

                    if (obj != null)
                    {
                        obj.transform.position = objData.position;
                        obj.transform.rotation = objData.rotation;
                        obj.transform.localScale = objData.scale;
                        obj.SetActive(!objData.isCollected);  // Nesneyi toplanmış veya aktif olmayan olarak ayarla
                    }
                    else
                    {
                        Debug.LogError("Object not found in sceneObjects: " + objData.objectID);
                    }
                }
            }
        }
        else
        {
            Debug.Log("sceneObjects list is already populated. Updating existing objects...");

            foreach (var objData in data.objects)
            {
                GameObject obj = sceneObjects.Find(o => o.name == objData.objectID);
                if (obj != null)
                {
                    obj.transform.position = objData.position;
                    obj.transform.rotation = objData.rotation;
                    obj.transform.localScale = objData.scale;
                    obj.SetActive(!objData.isCollected);  // Nesneyi toplanmış veya aktif olmayan olarak ayarla
                }
                else
                {
                    Debug.LogWarning("Object not found in sceneObjects list: " + objData.objectID);
                }
            }
        }

    }

    public void LoadSceneData(string sceneName)
    {
        
        string inventoryFilePath = Path.Combine(Application.persistentDataPath, "inventory_" + sceneName + ".json");
        InventorySystem.instance.LoadInventory(inventoryFilePath);

        string backpackFilePath = Path.Combine(Application.persistentDataPath, "backpack_" + sceneName + ".json");
        BackpackSystem.instance.LoadBackpack(backpackFilePath);

        string playerStatsFilePath = Path.Combine(Application.persistentDataPath, "playerStats_" + sceneName + ".json");
        PlayerStats.instance.LoadPlayerStats(playerStatsFilePath);

        string sceneFilePath = Path.Combine(Application.persistentDataPath, "scene_" + sceneName + ".json");
        if (System.IO.File.Exists(sceneFilePath))
        {
            string json = System.IO.File.ReadAllText(sceneFilePath);
            SceneData sceneData = JsonUtility.FromJson<SceneData>(json);
            LoadSceneObjects(sceneData); 
        }

        TreeSpawner.Instance.LoadData();

        isGameLoaded = true;

        Debug.Log($"Scene {sceneName} data loaded.");
    }

    public void SaveSceneData(string sceneName)
    {
        string inventoryFilePath = Path.Combine(Application.persistentDataPath, "inventory_" + sceneName + ".json");
        InventorySystem.instance.SaveInventory(inventoryFilePath);

        string backpackFilePath = Path.Combine(Application.persistentDataPath, "backpack_" + sceneName + ".json");
        BackpackSystem.instance.SaveBackpack(backpackFilePath);

        string playerStatsFilePath = Path.Combine(Application.persistentDataPath, "playerStats_" + sceneName + ".json");
        PlayerStats.instance.SavePlayerStats(playerStatsFilePath);

        SceneData sceneData = CollectSceneData();
        string sceneFilePath = Path.Combine(Application.persistentDataPath, "scene_" + sceneName + ".json");
        string json = JsonUtility.ToJson(sceneData, true);
        System.IO.File.WriteAllText(sceneFilePath, json);

        TreeSpawner.Instance.SaveData();

        Debug.Log($"Scene {sceneName} data saved.");
    }

    public void SaveGameProgress()
    {
        SaveSceneData(SceneManager.GetActiveScene().name);  
        Debug.Log("Game progress saved.");
    }
}
