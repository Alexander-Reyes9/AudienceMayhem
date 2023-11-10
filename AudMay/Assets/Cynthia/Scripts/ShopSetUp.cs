using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ShopSetUp : MonoBehaviour
{
    //Grab all items within game with array
    public List<GameObject> allItems;
    public int allItemsLength;
   
    //Example for Sophia's array
    public List<GameObject> allUsedItems;

    public bool check;
    public bool lengthCheck;
    public int RandNumber;

    //For three specific items in shop
    public int RandItem_1;
    public int RandItem_2;
    public int RandItem_3;
    //Use for storing the Heart GameObject.
    public int HeartID;


    void Start()
    { 
        // v in order to store variables within items list by checking if = 1000
        RandItem_1 = 1000;
        RandItem_2 = 1000;
        RandItem_3 = 1000;
        
        //To make sure that CheckItems method runs
        check = true;
        GetAllItems();
        InstantiateItems(allItems);
    }


    public void GetAllItems()
    {
        //Loads all prefabs in the folder called "Items" into a list
        allItems = new List<GameObject>(Resources.LoadAll<GameObject>("Items"));
        allItemsLength = allItems.Count;
        print("the length of all items is " + allItemsLength);

        //To help with testing CheckItemsInUse()
        //allUsedItems.Add(allItems[1]);
        //allUsedItems.Add(allItems[0]);
        //allUsedItems.Add(allItems[2]);
        //allUsedItems.Add(allItems[3]);


        //Used in order to find the heart Game Object.
        int count = 0;
        foreach(GameObject items in allItems)
        {
            if("heart" == items.name)
            {
                HeartID = count;
                allUsedItems.Add(allItems[count]);
                print("HeartID is " + HeartID);
                count++;
            }
        }

        //This method grabs the three randomized items
        for (int i = 0; i < 3; i++)
        {
            //Checks if the length of useditems is = to items
            if(lengthCheck == true)
            {
                print("There are no more items available!");
                return;
            }
            //Used in order to ensure that each item is different (No repeats!)
            while (check == true)
            {
                CheckItemsInUse(allUsedItems, allItems, RandomizeItems());
            }
        }
        
        //Testing if useditems list is being changed.
        foreach (GameObject u_items in allUsedItems)
        {
            print(u_items.name);
        }
        
    }

    public int RandomizeItems()
    {
        //This method randomizes number in order to make the shop more random
        RandNumber = UnityEngine.Random.Range(0, allItemsLength);
        return RandNumber;
    }


    public void CheckItemsInUse(List<GameObject> usedItems, List<GameObject> items, int randomNum)
    {
        check = true;
        //Once merged into scene -> use Sophia's History of Items array for "usedItems"
        //Use Sophia's code (History of item) in order to check if randomized # game object = sophia's array.


        //Once the lengths are = to eachother and we start getting repeats... -> ends the method
        if (usedItems.Count == allItemsLength)
        {
            lengthCheck = true;
            check = false;
            return; 
        }

        //Ends method and tries again bc check is still true
        foreach (GameObject u_items in usedItems)
        {
            if(u_items.name == items[randomNum].name)
            {
                return;
            }
            
        }

        check = false;
        SetItemNumbers(randomNum);
    }

    public void SetItemNumbers(int Num)
    {
        //This method sets the randomied number into their respective variables.
        
        if(RandItem_1 == 1000)
        {
            RandItem_1 = Num;
            print("RandItem_1 = " + RandItem_1);
            allUsedItems.Add(allItems[Num]);
            check = true;
            return;
        }
        if (RandItem_2 == 1000)
        {
            RandItem_2 = Num;
            print("RandItem_2 = " + RandItem_2);
            allUsedItems.Add(allItems[Num]);
            check = true;
            return;
        }
        if (RandItem_3 == 1000)
        {
            RandItem_3 = Num;
            print("RandItem_3 = " + RandItem_3);
            allUsedItems.Add(allItems[Num]);
            check = true;
            return;
        }
    }
    public void InstantiateItems(List<GameObject> items)
    {
        //4 items; Heart, RandItem_(1 , 2 and 3)
        //Instatiate would be in a circle pattern on the floor
        //In order to pick up -> use collider then update Sophia's History of Items.

        for(int i = 0; i < 4; i++)
        {
            var shopPos = GameObject.Find("ShopPlane").transform.position;

            //Forms the circle
            var radians = 2 * MathF.PI / 4 * i;
            var vertical = MathF.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            //Makes sure the circle will always be on top of "ShopPlane"
            var spawnDir = new Vector3(horizontal, 1, vertical);
            var spawnPos = shopPos + spawnDir * 2;

            //This Instantiates the item depending on what # i is.

            //*NOTE: change "ItemsShop script to have have combination of Sammies pick ups and Sarah's coin value check.
            switch (i)
            {
                case 0:
                    //This instantiates heartID
                    Instantiate(allItems[HeartID], spawnPos, Quaternion.identity).AddComponent<ItemsShop>();
                    break;
                case 1:
                    //This instantiates RandomItem_1 if = 1000 -> instanties heartID
                    if (RandItem_1 == 1000)
                    {
                        Instantiate(allItems[HeartID], spawnPos, Quaternion.identity).AddComponent<ItemsShop>(); 
                    }
                    else
                    {
                        Instantiate(allItems[RandItem_1], spawnPos, Quaternion.identity).AddComponent<ItemsShop>(); 
                    }
                    break;
                case 2:
                    //This instantiates RandomItem_2 if = 1000 -> instanties heartID
                    if (RandItem_1 == 1000)
                    {
                        Instantiate(allItems[HeartID], spawnPos, Quaternion.identity).AddComponent<ItemsShop>();
                    }
                    else
                    {
                        Instantiate(allItems[RandItem_2], spawnPos, Quaternion.identity).AddComponent<ItemsShop>();
                    }
                    break;
                case 3:
                    //This instantiates RandomItem_3 if = 1000 -> instanties heartID
                    if (RandItem_1 == 1000)
                    {
                        Instantiate(allItems[HeartID], spawnPos, Quaternion.identity).AddComponent<ItemsShop>();
                    }
                    else
                    {
                        Instantiate(allItems[RandItem_3], spawnPos, Quaternion.identity).AddComponent<ItemsShop>();
                    }
                    break;
            
            }

        }

    }

}
