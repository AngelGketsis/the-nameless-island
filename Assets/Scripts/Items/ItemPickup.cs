using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    [SerializeField]
    public string promptMessage;
    private GameObject obj;

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Debug.Log(getPromptMessage(Item));
        Destroy(gameObject);
    }

    private void OnMouseDown() 
    {
        Pickup();
    }

    public string getPromptMessage(Item item)
    {
        return item.getName()+" added to Inventory";
    }

}
