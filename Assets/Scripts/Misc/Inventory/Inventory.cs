using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Component> itemInver = new List<Component>();
    private List<Component> toolInver = new List<Component>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemInver.Add(other.GetComponent<Item>());
        }
        if (other.CompareTag("Tool"))
        {
            toolInver.Add(other.GetComponent<Tool>);
        }
    }
}
