using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WipeData : MonoBehaviour
{
    public void DataWipe()
    {
        string nul = "DATANULLED";
        File.WriteAllText($"{Application.dataPath}/inventoryData.json", nul);
    }
}
