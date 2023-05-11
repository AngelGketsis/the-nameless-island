using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Windows;
using UnityEngine.UI;
using System;
 


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();


    public Transform ItemContent;
    public GameObject InventoryItem;

    public TextMeshProUGUI textComponent;

    public Toggle EnableRemove;

    public InventoryItemsController[] InventoryItems;

    private GameObject key;
    private GameObject map;
    private GameObject ring;
    private GameObject goldBar;

    public GameObject woman;
    public Item wand;
    private bool gaveKeyAndMap = false;

    private ScriptableObject sobj;
    private DialogueNPC dial;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
        textComponent.enabled = true;
        textComponent.text = item.getName() + " added to your Inventory";
        Debug.Log(item.getName() + " added to your Inventory");
        StartCoroutine(ShowMessage());
    }

    public void Remove(Item item)
    {
        Instantiate(item, new Vector3(0, 0, 0), Quaternion.Euler(90,0,0));
        Items.Remove(item);
    }

    public void ListItems()
    {
        // Clean content before open
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if(EnableRemove.isOn)
                removeButton.gameObject.SetActive(true);

        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if(EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }

        else
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemsController>();
        Debug.Log(Items.Count);
        for(int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }

    public IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(2);
        textComponent.enabled = false;
    }

    public void GiveMapAndKey()
    {

        if(gaveKeyAndMap) {return;}

        gaveKeyAndMap = true;

        key = GameObject.Find("Key");
        map = GameObject.Find("Map");

        Instantiate(key, new Vector3((float)459.232, (float)10, (float)424), Quaternion.Euler(90f, 90f, 0f));
        Instantiate(map, new Vector3((float)459.6132, (float)10, (float)423), Quaternion.Euler(0f, 90f, 0f));

        key = GameObject.Find("Key");
        map = GameObject.Find("Map");

        Destroy(key);
        Destroy(map);

    }

    public void FindRing()
    {
        dial = woman.GetComponent<DialogueNPC>();

        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 5)
            {
               dial.enabled = true;
               return;
            }
        }

    }

    public void FindCoins()
    {

        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 8)
            {
                Items.Remove(Items[i]);
                StartCoroutine(MeltCoins());
            }
        }

    }

    public IEnumerator MeltCoins()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Coins melted");
        goldBar = GameObject.Find("GoldBar");
        Instantiate(goldBar, new Vector3((float)490.532, (float)10.7, (float)442.9), Quaternion.Euler(-180f, 0.5f, 5f));
        Destroy(goldBar);
    }

    public bool HasKeY()
    {
        return gaveKeyAndMap;
    }

}
