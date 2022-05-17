using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemInver = new List<Item>();
    public List<Tool> toolInver = new List<Tool>();
    BulletWorks weapon;

    private static Inventory inventory;
    [SerializeField] private string[] weapons;    
    public string[] Weapons { get => weapons; }

    public string[] weaponry()
    {
        string[] weapons = new string[toolInver.Count];
        int counter = 0;
        foreach(Tool tool in toolInver)
        {
            weapons[counter] = tool.namer;
            counter += 1;
        }
        return weapons;
    }

    private void Awake()
    {
        inventory = this;
        weapon = GameObject.Find("HellFire").GetComponent<BulletWorks>();

    }
    private void Start()
    {
        LoadPlayerData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SavePlayerToJSON();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            bool duplicated = false;
            Item order = other.gameObject.GetComponent<Item>();
            foreach (Item item in itemInver)
            {
                if (order.namer == item.namer)
                {
                    item.amount += order.amount;
                    duplicated = true;
                    print("Added " + order.amount + " to " + item.namer + ", which makes " + item.amount);
                }
            }
            if (!duplicated)
            {
                itemInver.Add(order);
                print("First added " + order.namer + ", with the amount of " + order.amount);
            }
            other.gameObject.SetActive(false);
            
        }
        if (other.CompareTag("Tool"))
        {
            toolInver.Add(other.GetComponent<Tool>());
            if (weapon.activated == false)
            {
                weapon.ChangeTool(toolInver[0]);
                weapon.activated = true;
                print("Active weapon");
            }
            Destroy(other.gameObject);
        }
    }
    public void SavePlayerToJSON()
    {
        print("Saving...");
        DataMage.SaveInventoryDataJSON(this);
    }
    private void LoadPlayerData()
    {
        // Preparations
        PlayerData loadedData = DataMage.LoadInventoryDataFromJSON();
        GameObject player = GameObject.Find("Player");

        // Position
        player.transform.position = new Vector3(loadedData.position[0], loadedData.position[1], loadedData.position[2]);

        // Life
        int curLife = 10 - (int)loadedData.life;
        player.GetComponent<PlayerMage>().PlayerDamager(curLife);

        // Tools
        foreach(string wepon in loadedData.weapons)
        {
            GameObject chosenWepon = GameObject.Find(wepon);
            toolInver.Add(chosenWepon.GetComponent<Tool>());
            chosenWepon.SetActive(false);
            weapon.ChangeTool(toolInver[0]);
            weapon.activated = true;
        }
    }
}
