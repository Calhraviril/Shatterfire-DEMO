using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class PlayerData
{
    public float[] position;
    public float life;
    public string[] weapons;

    public PlayerData(Inventory inventory)
    {
        life = GameObject.Find("Player").GetComponent<PlayerMage>().HP;
        weapons = inventory.weaponry();
        position = new float[3];
        position[0] = inventory.transform.position.x;
        position[1] = inventory.transform.position.y;
        position[2] = inventory.transform.position.z;
    }
}
