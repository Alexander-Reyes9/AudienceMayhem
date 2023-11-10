using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VIHistoryOfItems : MonoBehaviour
{
    public GameObject player; //
    public GameObject canvasText;
    public Scrollbar scrollbar;
    private int itemCount;

    public TextMeshProUGUI historyText; //only used to set text on screen
    private string history;

    private GameObject[] pickupsArr;
    private GameObject[] powerupsArr;
    private GameObject[] weaponsArr;

    private List<GameObject> pickupsList;
    private List<GameObject> powerupsList;
    private List<GameObject> weaponsList;

    private List<int> pickupsCount;
    private List<int> powerupsCount;
    private List<int> weaponsCount;

    private string pickupsStr;
    private string powerupsStr;
    private string weaponsStr;

    private List<GameObject> inventory;
    private List<int> inventoryCount;

    void Start()
    {
        //adds items to arrays based on tags
        pickupsArr = GameObject.FindGameObjectsWithTag("Pickup");
        powerupsArr = GameObject.FindGameObjectsWithTag("Powerup");
        weaponsArr = GameObject.FindGameObjectsWithTag("Weapon");

        //declares and initializes arraylists with arrays
        pickupsList = new List<GameObject>();
        pickupsList.AddRange(pickupsArr);
        powerupsList = new List<GameObject>();
        powerupsList.AddRange(powerupsArr);
        weaponsList = new List<GameObject>();
        weaponsList.AddRange(weaponsArr);

        //declares and intializes arrays to store the count of each GameObject (count is in the same index of the array as its GameObject)
        pickupsCount = new List<int>();
        for (int i = 0; i < pickupsList.Count; i++)
        {
            pickupsCount.Add(1);
        }
        powerupsCount = new List<int>();
        for (int i = 0; i < powerupsList.Count; i++)
        {
            powerupsCount.Add(1);
        }
        weaponsCount = new List<int>();
        for (int i = 0; i < weaponsList.Count; i++)
        {
            weaponsCount.Add(1);
        }

        inventory = new List<GameObject>();
        inventoryCount = new List<int>();

        //checks for duplicates of each item at the start, before new ones are instantiated
        SetStrings();
        CheckHistoryAtStart();

        //sets history with all items that are in the scene at the start (before anything is instantiated)
        SetHistory();
    }

    //checks for multiples of items (only to be called at the start)
    private void CheckHistoryAtStart()
    {
        for (int i = 0; i < pickupsList.Count; i++)
        {
            for (int x = i + 1; x < pickupsList.Count; x++)
            {
                if (pickupsList[i].ToString().Equals(pickupsList[x].ToString()))
                {
                    pickupsCount[i]++;

                    pickupsList.RemoveAt(x);
                    pickupsCount.RemoveAt(x);
                    SetStrings();
                }
            }
        }

        for (int i = 0; i < powerupsList.Count; i++)
        {
            for (int x = i + 1; x < powerupsList.Count; x++)
            {
                if (powerupsList[i].ToString().Equals(powerupsList[x].ToString()))
                {
                    powerupsCount[i]++;

                    powerupsList.RemoveAt(x);
                    powerupsCount.RemoveAt(x);
                    SetStrings();
                }
            }
        }

        for (int i = 0; i < weaponsList.Count; i++)
        {
            for (int x = i + 1; x < weaponsList.Count; x++)
            {
                if (weaponsList[i].ToString().Equals(weaponsList[x].ToString()))
                {
                    weaponsCount[i]++;

                    weaponsList.RemoveAt(x);
                    weaponsCount.RemoveAt(x);
                    SetStrings();
                }
            }
        }
    }

    void Update()
    {
        historyText.SetText(history);

        //uses space bar as a hotkey for the history of items
        if (canvasText.gameObject.activeSelf)
        {
            if (Input.GetKeyDown("space"))
            {
                canvasText.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKeyDown("space"))
            {
                canvasText.gameObject.SetActive(true);
            }
        }
        
    }

    //adds new instantiated objects to the arraylists and strings
    public void AddNewGameObject(GameObject i)
    {
        string item = i.ToString();
        string tag = i.tag;

        switch(tag)
        {
            case "Pickup":
                if (CheckHistory(item))
                {
                    pickupsList.Add(i);
                    pickupsCount.Add(1);
                }
                break;
            case "Powerup":
                if (CheckHistory(item))
                {
                    powerupsList.Add(i);
                    powerupsCount.Add(1);
                }
                break;
            case "Weapon":
                if (CheckHistory(item))
                {
                    weaponsList.Add(i);
                    weaponsCount.Add(1);
                }
                break;
        }

        SetStrings(); 
        SetHistory();
    }

    //checks if there are multiples of any items; increments item's count if there are
    private bool CheckHistory(string item)
    {
        int count = 0;

        foreach (GameObject i in pickupsList)
        {
            if (i.ToString().Equals(item))
            {
                count = ++pickupsCount[pickupsList.IndexOf(i)];

                if (count > 1)
                {
                    SetStrings();
                    return false;
                }
            }
        }
        foreach (GameObject i in powerupsList)
        {
            if (i.ToString().Equals(item))
            {
                count = ++powerupsCount[powerupsList.IndexOf(i)];

                if (count > 1)
                {
                    SetStrings();
                    return false;
                }
            }
        }
        foreach (GameObject i in weaponsList)
        {
            if (i.ToString().Equals(item))
            {
                count = ++weaponsCount[weaponsList.IndexOf(i)];

                if (count > 1)
                {
                    SetStrings();
                    return false;
                }
            }
        }

        return true;
    }

    //sets pickupsStr, powerupsStr, and weaponsStr
    private void SetStrings()
    {
        //adds object names and counts to each string
        pickupsStr = "Pickups:\n";
        for (int i = 0; i < pickupsList.Count; i++)
        {
            string item = pickupsList[i].ToString();
            string strItem = item.Remove(item.IndexOf("("));

            if (pickupsCount[i] > 1)
            {
                int index = CheckInventory(strItem);
                if (index > -1)
                {
                    pickupsStr += strItem + "(" + inventoryCount[index].ToString() + "/" + pickupsCount[i].ToString() + ")" + "\n";
                }
                else
                {
                    pickupsStr += strItem + "(" + pickupsCount[i].ToString() + ")" + "\n";
                }
            }
            else
            {
                pickupsStr += strItem + "\n";
            }

            itemCount++;
        }

        powerupsStr = "Powerups:\n";
        for (int i = 0; i < powerupsList.Count; i++)
        {
            string item = powerupsList[i].ToString();
            string strItem = item.Remove(item.IndexOf("("));

            if (powerupsCount[i] > 1)
            {
                int index = CheckInventory(strItem);
                if (index > -1)
                {
                    powerupsStr += strItem + "(" + inventoryCount[index].ToString() + "/" + powerupsCount[i].ToString() + ")" + "\n";
                }
                else
                {
                    powerupsStr += strItem + "(" + powerupsCount[i].ToString() + ")" + "\n";
                }
            }
            else
            {
                powerupsStr += strItem + "\n";
            }

            itemCount++;
        }
     
        weaponsStr = "Weapons:\n";
        for (int i = 0; i < weaponsList.Count; i++)
        {
            string item = weaponsList[i].ToString();
            string strItem = item.Remove(item.IndexOf("("));

            if (weaponsCount[i] > 1)
            {
                int index = CheckInventory(strItem);
                if (index > -1)
                {
                    weaponsStr += strItem + "(" + inventoryCount[index].ToString() + "/" + weaponsCount[i].ToString() + ")" + "\n";
                }
                else
                {
                    weaponsStr += strItem + "(" + weaponsCount[i].ToString() + ")" + "\n";
                }
            }
            else
            {
                weaponsStr += strItem + "\n";
            }

            itemCount++;
        }
    }

    //combines pickupsStr, powerupsStr, and weaponsStr to be set equal to history string
    private void SetHistory()
    {
        history = "<b>Items:</b>\n\n";
        history += pickupsStr + powerupsStr + weaponsStr;

        ChangeColor();
    }

    //adds pickups, powerups, and weapons to inventory arraylist on trigger
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Pickup") || collider.tag.Equals("Powerup") || collider.tag.Equals("Weapon"))
        {
            if (inventory.Count > 0)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if(collider.gameObject.ToString().Equals(inventory[i].ToString()))
                    {
                        //increments count for the item if the item is already in inventory
                        inventoryCount[i]++;
                        break;
                    }
                }

                inventory.Add(collider.gameObject);
                inventoryCount.Add(1);
            }
            else
            {
                inventory.Add(collider.gameObject);
                inventoryCount.Add(1);
            }

            SetStrings();
            SetHistory();
        }
    }

    //checks if an item is already in inventory (only called in SetStrings())
    private int CheckInventory(string item)
    {
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (item.Substring(item.Length - 1).Equals(" "))
                {
                    item = item.Remove(item.IndexOf(" "));
                }

                string compare = inventory[i].ToString();
                string strCompare = compare.Remove(compare.IndexOf("("));
                if (strCompare.Substring(strCompare.Length - 1).Equals(" "))
                {
                    strCompare = strCompare.Remove(strCompare.IndexOf(" "));
                }

                if (item.Equals((strCompare)))
                {
                    //returns index of the item in inventory and inventoryCount
                    return i;
                }
            }
        }

        return -1;
    }

    //chnages the string color of all items in inventory arraylist
    private void ChangeColor()
    {
        foreach (GameObject i in inventory)
        {
            string gotItem = i.ToString();
            string gotItemStr = gotItem.Remove(gotItem.IndexOf("("));

            //gets the part of a substring of the name of the item from the history string
            int startIndex = history.IndexOf(gotItemStr.Substring(0, 3));
            string color = history.Substring(startIndex, gotItemStr.Length);

            int start = history.IndexOf(color);
            int end = history.IndexOf(color) + color.Length;

            //changes color of the item in the history string
            history = history.Substring(0, start) + ("<color=blue>" + color + "</color>") + history.Substring(end);
        }
    }

}
