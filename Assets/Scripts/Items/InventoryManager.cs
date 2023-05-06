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

    private GameObject obj;
    private GameObject obj2;
    private GameObject obj3;
    private GameObject ring;
    public GameObject woman;
    public Item wand;
    private bool crafted = false;

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

    public void FindInventoryItem()
    {
        Debug.Log("Inside FindInventoryItem");

        if(crafted) {return;}

        obj = GameObject.Find("GreenWand");
        obj2 = GameObject.Find("BlueWand");
        obj3 = GameObject.Find("RedWand");
        sobj = ScriptableObject.CreateInstance<Item>();

        if(obj == null && obj2 == null)
        {

            transform.rotation = Quaternion.Euler(90, 0, 0);
            Debug.Log("OBJJJJJJJJJJJJJJJ");
            Instantiate(obj3, new Vector3((float)-1.5, (float)0.7, (float)2), transform.rotation);
  
            for(int i = 0; i < Items.Count; i++)
            {
                if(Items[i].getId() == 1)
                {
                    Items.Remove(Items[i]);
                    break;
                }
            }

            for(int i = 0; i < Items.Count ; i++)
            {
                if(Items[i].getId() == 3)
                {
                    Items.Remove(Items[i]);
                    break;
                }
            }

            crafted = true;

        }
        else if(obj == null)
        {
            textComponent.enabled = true;
            textComponent.text = "Missing Blue Wand !";
            StartCoroutine(ShowMessage());
        }
        else if(obj2 == null)
        {
            textComponent.enabled = true;
            textComponent.text = "Missing Green Wand !";
            StartCoroutine(ShowMessage());
        }

        else
        {
            textComponent.enabled = true;
            textComponent.text = "Missing Blue Wand and Green Wand !"; 
            StartCoroutine(ShowMessage());
        }
    

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

        /*
        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 5)
            {
                textComponent.enabled = true;
                textComponent.text =  "This was yout wife's ring, they took her away...";
                StartCoroutine(ShowMessage());
                return;
            }
        }
        textComponent.enabled = true;
        textComponent.text =  "*crying noises*";
        StartCoroutine(ShowMessage());
        */
    }

}
