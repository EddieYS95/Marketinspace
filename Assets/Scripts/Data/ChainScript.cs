using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ChainScript : MonoBehaviour {

    public GameObject Chain_list;
    public GameObject Chain;
    public GameObject Manage;
    public GameObject Stop;
    public GameObject Display;
    public GameObject Display_list;
    public GameObject c_Item;
    public GameObject AddC;

    int C_Store_index;
    public int ItemID;

    int ChargeTime;
    int RestTime;
    int EMoney;
    int IMoney;

    int[] smithCoin;

    bool[] smith;
    int[] item;
    int[] itemCount;
    int c_index;
    string pname;

    // Use this for initialization
    void Start () {
        C_Store_index = -1;
        EMoney = 0;
        IMoney = 0;
        ItemID = -1;
        smith = new bool[3];
        smithCoin = new int[3];
        item = new int[3];
        itemCount = new int[3];
        c_index = -1;
        for(int i=0; i<3; i++)
        {
            smith[i] = false;
            item[i] = -1;
            itemCount[i] = 0;
        }
        smithCoin[0] = 50;
        smithCoin[1] = 100;
        smithCoin[2] = 20;

        pname = "";
        SetChainList();
        ChargeTime = 900;
        for(int i=0; i<3; i++)
        {
            Manage.transform.FindChild("Panel").FindChild("Smiths").GetChild(i).FindChild("coin").FindChild("Text").GetComponent<Text>().text = "" + smithCoin[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
        Chain_list.GetComponent<RectTransform>().sizeDelta = new Vector2(Chain_list.transform.childCount * 240, 390);

        for(int i=0; i<GameData.getChainSize(); i++)
        {
            GameObject node = Chain_list.transform.GetChild(i).gameObject;

            if (GameData.ChainList[i].isIng)
            {
                DateTime date = GameData.ToTime(GameData.ChainList[i].STime);
                double TimeSpan = GameData.GetPassedTime(date);

                RestTime = ChargeTime - (int)TimeSpan;
                node.transform.FindChild("Info").FindChild("Active").transform.FindChild("Text").GetComponent<Text>().text = "매상 받기";

                if (RestTime < 0)
                {
                    node.transform.FindChild("Info").FindChild("Active").transform.FindChild("coin").gameObject.SetActive(true);
                    node.transform.FindChild("Info").FindChild("Time").GetComponent<Text>().text = "남은 시간 : 0:00 ";
                    node.transform.FindChild("Info").FindChild("Active").GetComponent<Button>().interactable = true;
                    transform.FindChild("ChainBt").FindChild("Coin").gameObject.SetActive(true);


                }
                else
                {
                    node.transform.FindChild("Info").FindChild("Active").transform.FindChild("coin").gameObject.SetActive(false);
                    if (RestTime % 60 < 10)
                        node.transform.FindChild("Info").FindChild("Time").GetComponent<Text>().text = "남은 시간 : " + RestTime / 60 + ":0" + RestTime % 60;
                    else node.transform.FindChild("Info").FindChild("Time").GetComponent<Text>().text = "남은 시간 : " + RestTime / 60 + ":" + RestTime % 60;
                    node.transform.FindChild("Info").FindChild("Active").GetComponent<Button>().interactable = false;
                }

            }
            else
            {
                node.transform.FindChild("Info").FindChild("Active").transform.FindChild("coin").gameObject.SetActive(false);
                node.transform.FindChild("Info").FindChild("Active").transform.FindChild("Text").GetComponent<Text>().text = "영업 시작";
                node.transform.FindChild("Info").FindChild("Active").GetComponent<Button>().interactable = true;
            }
        }

        if (Manage.activeInHierarchy)
        {
            EMoney = 0;
            IMoney = 0;
            

            for (int i=0; i<3; i++)
            {
                
                if (smith[i])
                {
                    Manage.transform.FindChild("Panel").FindChild("Smiths").FindChild("smith (" + i + ")").FindChild("Active").gameObject.SetActive(true);
                    Manage.transform.FindChild("Panel").FindChild("Smiths").FindChild("smith (" + i + ")").FindChild("Button").FindChild("Text").GetComponent<Text>().text = "고용됨";

                    EMoney += smithCoin[i];
                }

                else
                {
                    Manage.transform.FindChild("Panel").FindChild("Smiths").FindChild("smith (" + i + ")").FindChild("Active").gameObject.SetActive(false);
                    Manage.transform.FindChild("Panel").FindChild("Smiths").FindChild("smith (" + i + ")").FindChild("Button").FindChild("Text").GetComponent<Text>().text = "고용";
                }
                if (item[i] != -1)
                {

                    int cost = GameData.ItemList[GameData.ItemInventory[item[i]].id].Cost * itemCount[i];

                    if (smith[0]) // 대장장이 효과
                    { 
                       if (GameData.ItemList[GameData.ItemInventory[item[i]].id].Tier == 3 && GameData.ItemList[GameData.ItemInventory[item[i]].id].itemType == ItemType.Weapon)
                                cost += (int)(cost * 0.2f);
                    }
                    if (smith[1])
                    {
                        if (GameData.ItemList[GameData.ItemInventory[item[i]].id].Tier == 1 && GameData.ItemList[GameData.ItemInventory[item[i]].id].itemType == ItemType.Weapon)
                            cost += (int)(cost * 0.5f);
                    }
                    if (smith[2])
                    {
                        if (GameData.ItemList[GameData.ItemInventory[item[i]].id].Tier == 1 && GameData.ItemList[GameData.ItemInventory[item[i]].id].itemType == ItemType.Armor)
                            cost += (int)(cost * 0.1f);
                    }

                    Manage.transform.FindChild("Panel").FindChild("C_Items").FindChild("c_item (" + i + ")").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.ItemInventory[item[i]].id].id);
                    Manage.transform.FindChild("Panel").FindChild("C_Items").FindChild("c_item (" + i + ")").FindChild("coin").FindChild("Text").GetComponent<Text>().text = "" + cost;
                    Manage.transform.FindChild("Panel").FindChild("C_Items").FindChild("c_item (" + i + ")").FindChild("count").GetComponent<Text>().text = "x" + itemCount[i];
                    IMoney += cost;
                }

                else
                {
                    Manage.transform.FindChild("Panel").FindChild("C_Items").FindChild("c_item (" + i + ")").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
                    Manage.transform.FindChild("Panel").FindChild("C_Items").FindChild("c_item (" + i + ")").FindChild("coin").FindChild("Text").GetComponent<Text>().text = "";
                    Manage.transform.FindChild("Panel").FindChild("C_Items").FindChild("c_item (" + i + ")").FindChild("count").GetComponent<Text>().text = "";
                }

            }

            if (GameData.getMoney() < EMoney || IMoney ==0) Manage.transform.FindChild("Start_btn").GetComponent<Button>().interactable = false;
            else Manage.transform.FindChild("Start_btn").GetComponent<Button>().interactable = true;

            Manage.transform.FindChild("Monee").FindChild("employ").GetComponent<Text>().text = "" + EMoney;
            Manage.transform.FindChild("Monee").FindChild("sale").GetComponent<Text>().text = "" + IMoney;

        }
        if (Display.activeInHierarchy)
        {
            if(ItemID != -1)
            {
                if (item[0] != ItemID && item[1] != ItemID && item[2] != ItemID)
                {
                    Display.transform.FindChild("Item_").FindChild("item_content").FindChild("count").GetComponent<Text>().text = "" + GameData.ItemInventory[ItemID].Count + "개";
                    Display.transform.FindChild("Counts").gameObject.SetActive(true);
                    for(int k=0; k < Display.transform.FindChild("Counts").transform.childCount; k++)
                    {
                        if (GameData.ItemInventory[ItemID].Count < (k + 1) * 10) Display.transform.FindChild("Counts").transform.GetChild(k).GetComponent<Button>().interactable = false;
                        else Display.transform.FindChild("Counts").transform.GetChild(k).GetComponent<Button>().interactable = true;
                    }

                }
                else
                {
                    Display.transform.FindChild("Item_").FindChild("item_content").FindChild("count").GetComponent<Text>().text = "";
                    Display.transform.FindChild("Counts").gameObject.SetActive(false);
                }
                Display.transform.FindChild("Item_").FindChild("item_content").FindChild("content").GetComponent<Text>().text = GameData.ItemList[GameData.ItemInventory[ItemID].id].name +" (" + GameData.ItemList[GameData.ItemInventory[ItemID].id].Tier+"Tier)";
                Display.transform.FindChild("Item_").FindChild("item_content").FindChild("Coin").gameObject.SetActive(true);
                Display.transform.FindChild("Item_").FindChild("item_content").FindChild("Coin").FindChild("Text").GetComponent<Text>().text = "" + GameData.ItemList[GameData.ItemInventory[ItemID].id].Cost;

            }
            else
            {
                Display.transform.FindChild("Item_").FindChild("item_content").FindChild("content").GetComponent<Text>().text = "";
                Display.transform.FindChild("Item_").FindChild("item_content").FindChild("count").GetComponent<Text>().text = "";
                Display.transform.FindChild("Item_").FindChild("item_content").FindChild("Coin").FindChild("Text").GetComponent<Text>().text = "";
                Display.transform.FindChild("Item_").FindChild("item_content").FindChild("Coin").gameObject.SetActive(false);
                Display.transform.FindChild("Counts").gameObject.SetActive(false);
            }

        }


    }

    public void SetChainList()
    {
        if (Chain_list.transform.childCount > 0)
        {
            for (int i = 0; i < Chain_list.transform.childCount; i++)
            {
                Destroy(Chain_list.transform.GetChild(i).gameObject);
            }
        }

        if (GameData.getChainSize() == 0) pname = "오도르2호점";
        else if (GameData.getChainSize() == 1) pname = "오도르3호점";
        else if (GameData.getChainSize() == 2) pname = "토우2호점";
        else if (GameData.getChainSize() == 3) pname = "토우3호점";
        else if (GameData.getChainSize() == 4) pname = "프라이2호점";
        else if (GameData.getChainSize() == 5) pname = "프라이3호점";
        else if (GameData.getChainSize() == 6) pname = "튜바나2호점";
        else if (GameData.getChainSize() == 7) pname = "튜바나3호점";
        else if (GameData.getChainSize() == 8) pname = "이드2호점";
        else if (GameData.getChainSize() == 9) pname = "이드3호점";
        else if (GameData.getChainSize() == 10) pname = "세니2호점";
        else if (GameData.getChainSize() == 11) pname = "세니3호점";
        else if (GameData.getChainSize() == 12) pname = "Mr.R2호점";
        else if (GameData.getChainSize() == 13) pname = "Mr.R3호점";
        else pname = "";


        for (int i=0; i<GameData.getChainSize()+1; i++)
        {
            int index = i;
            GameObject node = Instantiate(Chain) as GameObject;
            node.transform.SetParent(Chain_list.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = "Chain" + i;


            if (i < GameData.getChainSize())
            {
                int space = -1;
                node.transform.FindChild("Info").gameObject.SetActive(true);
                node.transform.FindChild("Info").FindChild("Name").GetComponent<Text>().text = GameData.ChainList[i].name;
                if (i == 0 || i == 1) space = 0;
                else if (i == 2 || i == 3) space = 1;
                else if (i == 4 || i == 5) space = 2;
                else if (i == 6 || i == 7) space = 3;
                else if (i == 8 || i == 9) space = 4;
                else if (i == 10 || i == 11) space = 5;
                else if (i == 12 || i == 13) space = 6;

                node.transform.FindChild("Info").FindChild("Vill").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/Vill")[space];
                node.transform.FindChild("Info").FindChild("Active").GetComponent<Button>().onClick.AddListener(() => { OnClickActive(index); });
                node.transform.FindChild("Plus").gameObject.SetActive(false);
            }
            else
            {
                if (GameData.getChainSize() < (GameData.getSpaceOpen() + 1) * 2)
                {
                    node.transform.FindChild("Plus").GetComponent<Button>().interactable = true;
                    node.transform.FindChild("Pname").GetComponent<Text>().text = pname;
                }
                else
                {
                    node.transform.FindChild("Plus").GetComponent<Button>().interactable = false;
                    //node.transform.FindChild("Pname").GetComponent<Text>().text = ""; 
                }

                
                node.transform.FindChild("Info").gameObject.SetActive(false);
                node.transform.FindChild("Plus").gameObject.SetActive(true);
                node.transform.FindChild("Plus").GetComponent<Button>().onClick.AddListener(() => { OnClickAddC(pname); });
            }
        
            
        }
    }

    public void SetChainItemList()
    {
        if (Display_list.transform.childCount > 0)
        {
            for (int i = 0; i < Display_list.transform.childCount; i++)
            {
                Destroy(Display_list.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < GameData.getItemInventorySize(); i++)
        {
            GameObject node = Instantiate(c_Item) as GameObject;
            node.transform.SetParent(Display_list.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = i + "";
        }
    }
    public void OnClickAddC(string name)
    {
        AddC.SetActive(true);
        AddC.transform.FindChild("Name").GetComponent<Text>().text = name;
    }
    public void OnClickPlus()
    {

        AddC.SetActive(false);
        GameData.addChain(pname);
        SetChainList();
    }
    public void OnClickActive(int index)
    {
        if (!GameData.ChainList[index].isIng)
        {
            for (int i = 0; i < 3; i++)
            {
                smith[i] = false;
                item[i] = -1;
            }
            Manage.SetActive(true);
            C_Store_index = index;
            Manage.transform.FindChild("Panel").FindChild("name").GetComponent<Text>().text = GameData.ChainList[index].name;
           
        }

        else
        {
            C_Store_index = index;
            Stop.transform.FindChild("Panel").FindChild("Coin").GetComponent<Text>().text = "" + GameData.ChainList[C_Store_index].Money;
            Stop.SetActive(true);
            
        }
    }

    public void OnClickStop()
    {
        GameObject.Find("Audio").transform.FindChild("Coin_sound").GetComponent<AudioSource>().Play();
        if (GameObject.Find("DayManager")) GameObject.Find("DayManager").transform.FindChild("CoinAni").GetComponent<Animator>().Play("addCoin");
        transform.FindChild("ChainBt").FindChild("Coin").gameObject.SetActive(false);
        GameData.stopChain(C_Store_index);
        Stop.SetActive(false);
    }
    public void OnClickEmploy(int index)
    {
        smith[index] = !smith[index];
    }

    public void OnClickSelectItem(int index)
    {
        c_index = index;
       ItemID = -1;
        Display.SetActive(true);
        SetChainItemList();
    }


    public void OnClickCancleItem(int index)
    {
        item[index] = -1;
    }
    public void OnClickDecideItem(int count)
    {
        if (ItemID != -1)
        {
            item[c_index] = ItemID;
            itemCount[c_index] = count;
            ItemID = -1;
            Display.SetActive(false);
        }
    }
    public void OnClickStart()
    {
        if (C_Store_index != -1)
        {
           
            GameData.useMoney(EMoney);

            for(int i=0; i<3; i++)
            {
                if (item[i] != -1) GameData.removeItem(GameData.ItemInventory[item[i]].id, itemCount[i]);
            }
            GameData.soltItem();
            
            GameData.startChain(C_Store_index, DateTime.Now, IMoney);
            Manage.SetActive(false);
        }
    }

}
