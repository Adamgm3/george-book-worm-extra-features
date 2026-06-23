using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    public Save saveData = new Save();
    public PlayerAbilities playerAbilities;
    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/save.txt";

            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log(Application.persistentDataPath);
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Saved to: " + savePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            saveData = new Save();
            return;
        }

        string json = File.ReadAllText(savePath);

        saveData = JsonUtility.FromJson<Save>(json);

        Debug.Log("Loaded save file");
    }
}