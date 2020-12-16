using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class ForgeManager : MonoBehaviour
{
    public GameObject MakeItemMainImage;

    public GameObject MakeItemList;
    public GameObject MakeItem;
    public GameObject NeedElementList;
    public GameObject NeedElement;

    public GameObject ItemInventoryList;
    public GameObject ItemInventory;

    public GameObject ElementInventoryList;
    public GameObject ElementInventory;

    public Text makeItemNameTag;

    public Image InvenZoomImage;
    public Text InvenZoomName;
    public Text InvenZoomContent;

    public Button MakeButton;

    private GameObject SelectedMakeItem;

    public GameObject Coin;
    public GameObject ItemInfo;
    public bool isMaking;
    public GameObject MakingItem;

    bool isFull;
    bool invenFull;

    int MakeItemCount = 0;
    double TimeSpan = 0;

    void Awake()
    {
        isFull = false;
        isMaking = false;
        invenFull = false;
        if (GameData.OnGame == false)
        {
            GameData.LoadGame();
        }
        setMakeItemList();
        setInventory();
    }
    // Use this for initialization
    void Start()
    { //나중에 지우십쇼
        
        //업데이트 1.0.12용
        if (GameData.getLevel() > 26) GameData.setAbleMake(118);


        for(int i=0; i<5; i++)
        {
            if (GameData.MakingItem[i].NodeType == NodeType.Item)
            {
                string zero = "0";
                int id = GameData.getMakingItemID(i);
                if (id < 10) zero = "00";
                else if (id < 100 && id >= 10) zero = "0";
                else zero = "";
                MakeItemCount = GameData.getMakingItemCount(i);

                DateTime date = GameData.ToTime(GameData.getMakingItemTime(i));
                TimeSpan = GameData.GetPassedTime(date);

                if (TimeSpan / GameData.ItemList[id].progressTime > 1)
                {
                    MakeItemCount = GameData.getMakingItemCount(i);
                    int complete = (int)(TimeSpan / GameData.ItemList[id].progressTime);
                    if (complete > GameData.getMakingItemCount(i))
                        complete = GameData.getMakingItemCount(i);
                    GameObject.Find("Audio").transform.FindChild("complete").GetComponent<AudioSource>().Play();

                    MakeItemCount = GameData.getMakingItemCount(i) - complete;
                    if (MakeItemCount < 0) MakeItemCount = 0;
                    GameData.setItem(id, complete);
                  
                }
                else
                {
                    MakeItemCount = GameData.getMakingItemCount(i);
                   
                }
                GameData.refreshMaking(i, MakeItemCount, GameData.ToTime(GameData.getMakingItemTime(i)));

                if (MakeItemCount == 0)
                {
                    GameData.removeMakingItem(i);
                }
                else
                {
                    
                    GameObject Mitem = Instantiate(MakingItem, transform.position, transform.rotation) as GameObject;

                    Mitem.transform.SetParent(GameObject.Find("Makinglist").transform.FindChild("list_item").FindChild("" + i).FindChild("Item"));
                    Mitem.transform.localPosition = new Vector3(0, 0, 0);
                    Mitem.transform.localScale = new Vector3(1, 1, 1);
                    Mitem.transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/IT" + zero + id);
                    Mitem.GetComponent<CancleMaking>().SetNodeID(i);
                    Mitem.GetComponent<CancleMaking>().SetItemID(id);
                    Mitem.GetComponent<CancleMaking>().count = MakeItemCount;
                    Mitem.GetComponent<CancleMaking>().ing = true;

                    if (TimeSpan >= GameData.ItemList[id].progressTime) Mitem.GetComponent<CancleMaking>().Ptime = TimeSpan % GameData.ItemList[id].progressTime;
                    else Mitem.GetComponent<CancleMaking>().Ptime = TimeSpan;
                    
                    MakeItemCount = 1;

                    if (Mitem.transform.localPosition.x != 0) Mitem.transform.localPosition = new Vector3(0, 0, 0);
                    
                    
                }
                
            }
        }


        
    }
    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Makinglist").transform.FindChild("list_item").transform.childCount != 0) isMaking = true;
        else isMaking = false;
        CheckAbleMake();
        
        for (int i = 0; i < 5; i++)
        {
            isFull = true;
            if (GameData.getMakingItemType(i) == NodeType.Empty)
            {
                isFull = false;
                break;
            }

        }
      

    }

    public void UnselectedItem()
    {
        InvenZoomImage.sprite = Resources.Load<Sprite>("char/nill");
        InvenZoomContent.text = "";
        InvenZoomName.text = "";
        InvenZoomImage.transform.parent.FindChild("Remove").GetComponent<Button>().interactable = false;
        InvenZoomImage.transform.parent.FindChild("Equipment").gameObject.SetActive(false);
    }

    public void CheckAbleMake()
    {
        bool ableMade;
        bool notHaveClass = false;

        

        if (NeedElementList.transform.childCount != 0)
        {
            ableMade = true;
        }
        else
        {
            ableMade = false;
        }

        for (int i = 0; i < NeedElementList.transform.childCount; i++)
        {
            if (NeedElementList.transform.GetChild(i).GetComponent<MakingAbleChecker>().Enough == false)
            {
                ableMade = false;
            }
        }

        if (SelectedMakeItem != null)
        {
            if (GameData.ItemList[int.Parse(SelectedMakeItem.name.Remove(0,2))].makeAble == false)
            {
                ableMade = false;
                notHaveClass = true;
            }
        }


        if (GameData.ItemInventory[GameData.getItemInventorySize() - 1].NodeType == NodeType.Empty)
        {
            invenFull = false;
        }
        else
        {
            bool isHave = false;
            for (int i = 0; i < GameData.getItemInventorySize(); i++)
            {
                if (GameData.ItemInventory[i].NodeType != NodeType.Empty && SelectedMakeItem)
                {
                    if (GameData.ItemInventory[i].id == int.Parse(SelectedMakeItem.name.Remove(0, 2))) isHave = true;
                }
            }

            if (isHave) invenFull = false;
            else invenFull = true;
        }




        if (ableMade == true && !isFull && !invenFull)
        {
            MakeButton.interactable = true;
            MakeButton.GetComponentInChildren<Text>().text = "제작";
        }
        else
        {
            
            MakeButton.GetComponentInChildren<Text>().text = "재료 부족";

         
            if (notHaveClass)
            {
                MakeButton.GetComponentInChildren<Text>().text = "설계도 없음";
            }

            MakeButton.interactable = false;
        }

            if (isFull)
            {
                MakeButton.GetComponentInChildren<Text>().text = "제작창 초과";

                if (notHaveClass)
                {
                    MakeButton.GetComponentInChildren<Text>().text = "설계도 없음";
                }
            MakeButton.interactable = false;

        }
        if (invenFull)
        {
            MakeButton.GetComponentInChildren<Text>().text = "인벤토리 초과";

            if (notHaveClass)
            {
                MakeButton.GetComponentInChildren<Text>().text = "설계도 없음";
            }
            MakeButton.interactable = false;

        }

    }

    public void OnClickMakeButton()
    {

        GameObject.Find("Audio").transform.FindChild("forge").GetComponent<AudioSource>().Play();
        string zero = "0";
        int id = int.Parse(SelectedMakeItem.name.Remove(0, 2));
        if (id < 10) zero = "00";
        else if (id < 100 && id >= 10) zero = "0";
        else zero = "";
        //GameData.MakeList.Add(id);
        GameObject Mitem = Instantiate(MakingItem, transform.position, transform.rotation) as GameObject;

        for (int i = 0; i < 5; i++)
        {
            if (GameData.getMakingItemType(i) == NodeType.Empty)
            {
                Mitem.GetComponent<CancleMaking>().SetNodeID(i);

                Mitem.transform.SetParent(GameObject.Find("Makinglist").transform.FindChild("list_item").FindChild("" + i).FindChild("Item"));
                break;
            }
        }
        
        GameData.setMaking(id, MakeItemCount, DateTime.Now);

        Mitem.transform.localPosition= new Vector3(0, 4, 0);
        Mitem.transform.localScale = new Vector3(1, 1, 1);
        Mitem.transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/IT" + zero + id);
        Mitem.GetComponent<CancleMaking>().SetItemID(id);
        Mitem.GetComponent<CancleMaking>().count = MakeItemCount;
        Mitem.GetComponent<CancleMaking>().Ptime = 0;
        for (int i = 0; i < GameData.ItemList[id].NeedElementID.Length; i++)
        {
            GameData.useElement(GameData.ItemList[id].NeedElementID[i], GameData.ItemList[id].NeedElementCount[i]*MakeItemCount);
        }
        MakeItemCount = 1;
    }

    public void setMakeItemList(){
        

        if (MakeItemList.transform.childCount>0){
            for (int i = 0; i < MakeItemList.transform.childCount; i++)
            {
                Destroy(MakeItemList.transform.GetChild(i).gameObject);
            }
        }

        for (int i =0; i < GameData.forgeAtItemList.Count; i++)
        {
            Item item = GameData.forgeAtItemList[i];
            GameObject node = Instantiate(MakeItem) as GameObject;
            node.transform.SetParent(MakeItemList.transform);
            node.transform.localScale = new Vector3(1,1,1);
            node.name = item.id;
            node.GetComponentInChildren<Text>().text = item.name;
            node.transform.FindChild("ItemImg").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/"+item.id);
            if (!GameData.ItemList[int.Parse(node.name.Remove(0, 2))].makeAble) {
                node.GetComponent<Button>().interactable = false;
                node.transform.FindChild("lock").gameObject.SetActive(true);
                if(GameData.ItemList[int.Parse(node.name.Remove(0, 2))].LevelAble !=0)
                node.transform.FindChild("lock").FindChild("Text").GetComponent<Text>().text = "Lv."+GameData.ItemList[int.Parse(node.name.Remove(0, 2))].LevelAble;
                node.transform.FindChild("ItemImg").GetComponent<Image>().color = new Color32(125, 125, 125, 255);
                node.transform.FindChild("ItemName").GetComponent<Text>().color = new Color32(125, 125, 125, 255);
            }
            node.GetComponent<Button>().onClick.AddListener(() => { OnClickMakeItem(node); });
        }
      
    }

    public void setInventory()
    {
        

        for (int i = 0; i < GameData.getItemInventorySize(); i++)
        {
            GameObject node = Instantiate(ItemInventory) as GameObject;
            node.transform.SetParent(ItemInventoryList.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = i + "";
        }

        for (int i = 0; i < GameData.getElementInventorySize(); i++)
        {
            GameObject node = Instantiate(ElementInventory) as GameObject;
            node.transform.SetParent(ElementInventoryList.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = i + "";
        }
    }


    public void OnClickMakeItem(GameObject item)
    {

        NeedElementList.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

        MakeItemCount = 1;
        ItemInfo.SetActive(true);
        SelectedMakeItem = item;

        string id = item.name.Substring(2, 3);
        makeItemNameTag.text = GameData.ItemList[int.Parse(id)].name;
        Coin.transform.parent.FindChild("M_time").GetComponent<Text>().text
                ="";
        Coin.transform.FindChild("Text").GetComponent<Text>().text = ""+GameData.ItemList[int.Parse(id)].Cost;
        if (GameData.ItemList[int.Parse(id)].progressTime >= 60)
            Coin.transform.parent.FindChild("M_time").GetComponent<Text>().text
                = GameData.ItemList[int.Parse(id)].progressTime / 60 + "분";
        if(GameData.ItemList[int.Parse(id)].progressTime % 60 !=0)
            Coin.transform.parent.FindChild("M_time").GetComponent<Text>().text
                += " "+GameData.ItemList[int.Parse(id)].progressTime % 60 + "초";

        Coin.transform.parent.FindChild("Attack").GetComponent<Text>().text = "";

        if (GameData.ItemList[int.Parse(id)].Power == "0" && GameData.ItemList[int.Parse(id)].Def == "0") Coin.transform.parent.FindChild("Attack").GetComponent<Text>().text = "<기타 아이템>";


        if (GameData.ItemList[int.Parse(id)].Power != "0")
        {
            int min = int.Parse(GameData.ItemList[int.Parse(id)].Power.Split('_')[0]);
            int max = int.Parse(GameData.ItemList[int.Parse(id)].Power.Split('_')[1]);

            Coin.transform.parent.FindChild("Attack").GetComponent<Text>().text
                = "공격력: " + min+"~"+max;
       }
        if(GameData.ItemList[int.Parse(id)].Def != "0")
        {
            int min = int.Parse(GameData.ItemList[int.Parse(id)].Def.Split('_')[0]);
            int max = int.Parse(GameData.ItemList[int.Parse(id)].Def.Split('_')[1]);

            if (Coin.transform.parent.FindChild("Attack").GetComponent<Text>().text != "")
            {
                Coin.transform.parent.FindChild("Attack").GetComponent<Text>().text
                    += "\n방어력: " + min + "~" + max;
            }
            else
            {
                Coin.transform.parent.FindChild("Attack").GetComponent<Text>().text
                    = "방어력: " + min + "~" + max;
            }
        }

        MakeItemMainImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[int.Parse(id)].id);

        for (int i = 0; i < NeedElementList.transform.childCount; i++)
        {
            Destroy(NeedElementList.transform.GetChild(i).gameObject);
        }


        for (int i = 0; i < GameData.ItemList[int.Parse(id)].NeedElementID.Length; i++)
        {
            GameObject node = Instantiate(NeedElement) as GameObject;
            node.transform.SetParent(NeedElementList.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = id + ":" + GameData.ItemList[int.Parse(id)].NeedElementCount[i] + ":" + GameData.ItemList[int.Parse(id)].NeedElementID[i];

            node.transform.FindChild("ItemName").GetComponent<Text>().text = GameData.ElementList[GameData.ItemList[int.Parse(id)].NeedElementID[i]].name;
            node.transform.FindChild("ItemImg").GetComponent<Image>().sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[GameData.ItemList[int.Parse(id)].NeedElementID[i]].id);
            node.transform.FindChild("ItemCount").GetComponent<Text>().text = 0 + " / " + GameData.ItemList[int.Parse(id)].NeedElementCount[i];
        }
        GameData.soltElement();
    }

    public void OnClickPM(Button btn)
    {
        if (btn.name == "plus" && MakeItemCount != 10) MakeItemCount++;
        else if (btn.name == "minus" && MakeItemCount != 1) MakeItemCount--;
        btn.transform.parent.FindChild("Text").GetComponent<Text>().text =""+ MakeItemCount;
         for (int i = 0; i<NeedElementList.transform.childCount; i++)
            {
            NeedElementList.transform.GetChild(i).GetComponent<MakingAbleChecker>().count = MakeItemCount;
            }
        
    }

}
