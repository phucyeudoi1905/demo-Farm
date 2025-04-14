using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    // Start is called before the first frame update  
    void Start()
    {
        // Define save location  
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    // References  
    public void SaveGame()
    {
        SaveData saveData = new SaveData()
        {//vi tri vector cua player duoc saved
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
        //vi tri cam duoc dat 
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name
        };
        //url .User/Appdata/LocalLow/DefaultCompany/Demofarm
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
        }
        else
        {
            SaveGame();
        }
    }
}

