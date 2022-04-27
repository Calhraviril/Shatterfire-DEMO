using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Component> itemInver = new List<Component>();
    public List<Component> toolInver = new List<Component>();
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
            Destroy(other.gameObject);
            
        }
        if (other.CompareTag("Tool"))
        {
            toolInver.Add(other.GetComponent<Tool>());
        }
    }
}
