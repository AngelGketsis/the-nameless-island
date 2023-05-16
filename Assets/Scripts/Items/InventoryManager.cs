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
    private GameObject weapon;
    
    private GameObject arxant;
    private GameObject ant1;
    private GameObject ant2;
    private GameObject ant3;
    private GameObject ant4;
    private GameObject ant5;

    public GameObject woman;
    public GameObject ant;
    public GameObject arxfrouros;
    public GameObject endPanel;

    private bool gaveKey = false;
    private bool gaveMap = false;
    private bool gaveWeapon = false;

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

    public void GiveMap()
    {

        if(gaveMap) {return;}

        gaveMap = true;

        map = GameObject.Find("Map");

        Instantiate(map, new Vector3((float)459.6132, (float)10, (float)423), Quaternion.Euler(0f, 90f, 0f));

        map = GameObject.Find("Map");

        Destroy(map);

    }

    public void GiveKey()
    {
        if(gaveKey) {return;}

        gaveKey = true;

        key = GameObject.Find("Key");

        Instantiate(key, new Vector3((float)476.8546, (float)10, (float)535.32), Quaternion.Euler(90f, 90f, 0f));

        key = GameObject.Find("Key");

        Destroy(key);
    }

    public void GiveWeapon()
    {
        if(gaveWeapon) {return;}

        gaveWeapon = true;

        weapon = GameObject.Find("Weapon");

        Instantiate(weapon, new Vector3((float)432.166, (float)10, (float)445.9534), Quaternion.Euler(90f, 90f, 0f));

        weapon = GameObject.Find("Weapon");

        Destroy(weapon);
    }

    public void FindRing()
    {
        dial = woman.GetComponent<DialogueNPC>();

        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 11)
            {
               dial.enabled = true;
               return;
            }
        }

        StartCoroutine(ShowMessage());

    }

    public void FindGoldBar()
    {
        GameObject ant1Prefab = GameObject.Find("anti1(Clone)"); 

        if(ant1Prefab == null){ant1Prefab = GameObject.Find("anti1");}

        dial = ant1Prefab.GetComponent<DialogueNPC>();

        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 9)
            {
               dial.enabled = true;
               return;
            }
        }

        StartCoroutine(ShowMessage());

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

     public void FindJewels()
    {
        dial = arxfrouros.GetComponent<DialogueNPC>();

        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 10)
            {
                arxfrouros.layer = 0;
                Items.Remove(Items[i]);
                dial.enabled = false;
                return;
            }
        }

        dial.enabled = true;

    }

    public IEnumerator MeltCoins()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Coins melted");
        goldBar = GameObject.Find("GoldBar");
        Instantiate(goldBar, new Vector3((float)490.532, (float)10.7, (float)442.9), Quaternion.Euler(-180f, 0.5f, 5f));
        Destroy(goldBar);

        teleportResistance();

    }

    public void teleportResistance()
    {
        GameObject arxantPrefab = GameObject.Find("arxant"); 
        GameObject ant1Prefab = GameObject.Find("anti1"); 
        GameObject ant2Prefab = GameObject.Find("anti2"); 
        GameObject ant3Prefab = GameObject.Find("anti3"); 
        GameObject ant4Prefab = GameObject.Find("anti4"); 
        GameObject ant5Prefab = GameObject.Find("anti5"); 


        GameObject newArxant = Instantiate(arxantPrefab, new Vector3(425.0091f, 10f, 458.523f), Quaternion.Euler(0f, 90f, 0f));
        GameObject newAnt1 = Instantiate(ant1Prefab, new Vector3(430.8664f, 10f, 445.937f), Quaternion.Euler(0f, 90f, 0f));
        GameObject newAnt2 = Instantiate(ant2Prefab, new Vector3(429.6132f, 10f, 454.6f), Quaternion.Euler(0f, 90f, 0f));
        GameObject newAnt3 = Instantiate(ant3Prefab, new Vector3(429.6132f, 10f, 462.21f), Quaternion.Euler(0f, 90f, 0f));
        GameObject newAnt4 = Instantiate(ant4Prefab, new Vector3(426.6132f, 10f, 455.37f), Quaternion.Euler(0f, 90f, 0f));
        GameObject newAnt5 = Instantiate(ant5Prefab, new Vector3(425.5132f, 9.4f, 432.33f), Quaternion.Euler(0f, 90f, 0f));

        Destroy(arxantPrefab);
        Destroy(ant1Prefab);
        Destroy(ant2Prefab);
        Destroy(ant3Prefab);
        Destroy(ant4Prefab);
        Destroy(ant5Prefab);

    }

    public bool HasKeY()
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 6)
            {
               return true;
            }
        }

        return false;
    }

    public bool HasCoins()
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 8)
            {
               return true;
            }
        }

        return false;
    }

    public bool HasMap()
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 7)
            {
               return true;
            }
        }

        return false;
    }

    public bool HasWeapon()
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].getId() == 12)
            {
               return true;
            }
        }

        return false;
    }

}
