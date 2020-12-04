using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int numberOfZombiesKilledRecord = 0;

    public SaveData LoadData()
    {
        string json = "";
        try
        {
            Debug.Log("Arquivo de save existe");
            json = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        }
        catch
        {
            Debug.Log("Arquivo de save não existe. Criando arquivo.");
            SaveDataToFile(this);
            json = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        }

        SaveData save = JsonUtility.FromJson<SaveData>(json);
        return save;
    }

    public void SaveDataToFile(SaveData save)
    {
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
    }

    public void CreateJson()
    {

    }
}
