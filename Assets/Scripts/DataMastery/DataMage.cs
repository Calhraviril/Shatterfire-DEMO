using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataMage
{
    public static void SaveInventoryDataJSON(Inventory inventory) // Save to JSON
    {
        PlayerData data = new PlayerData(inventory);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText($"{Application.dataPath}/inventoryData.json", json);
    }

    public static PlayerData LoadInventoryDataFromJSON()
    {
        string json = File.ReadAllText($"{Application.dataPath}/inventoryData.json");

        PlayerData inventoryData = JsonUtility.FromJson<PlayerData>(json);

        return inventoryData;
    }
}
