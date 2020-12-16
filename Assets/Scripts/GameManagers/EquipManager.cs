using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipManager : MonoBehaviour {

    public GameObject EquipInList;
    public Text InvenZoomName;
    public Text InvenZoomPower;
    public Text InvenZoomDef;
    int xItemIndex;
    public int ItemIndex;
    int xAItemIndex;
    public int AItemIndex;
    int xAcItemIndex;
    public int AcItemIndex;
    int xHItemIndex;
    public int HItemIndex;
    int xSItemIndex;
    public int SItemIndex;
    public GameObject Info;
    public GameObject Select;
    public Image UI_Weapon;
    public Image UI_Gun;
    private Sprite EquippedW;
    public GameObject Quit;
    public GameObject SpaceList;
    public GameObject MyItem;

    private Button xStage;
    public int StageNum;
    private string StageName;

    public int EwNum;
    public int EaNum;
    public int EacNum;

    bool isRandom;


    // Use this for initialization
    void Start () {
       

        EwNum = -1;
        EaNum = -1;
        EacNum = -1;
        StageNum = -1;
        xItemIndex = 999;
        ItemIndex = 999;
        xAItemIndex = 999;
        AItemIndex = 999;
        xAcItemIndex = 999;
        AcItemIndex = 999;
        xHItemIndex = 999;
        HItemIndex = 999;
        xSItemIndex = 999;
        SItemIndex = 999;


        GameData.setEquippedItem(999);
        GameData.setEquippedArmor(999);
        GameData.setEquippedAcc(999);
        GameData.setEquippedHead(999);
        GameData.setEquippedShoes(999);

        if (GameData.OnGame == false) GameData.LoadGame();
        EquippedW = Resources.Load<Sprite>("Img/IT999");


        //테스트용 if (GameData.getSpaceOpen()!=1) GameData.OpenSpace(2);
        OnClickSpace(GameData.getSpaceOpen());


        if (!GameData.isRandom) {
            isRandom = false;
            Select.transform.FindChild("Toggle").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.FindChild("Stage").FindChild("stages").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.FindChild("Stage").FindChild("stages_r").gameObject.SetActive(false);
        }
        else
        {
            isRandom = true;
            Info.transform.FindChild("Space").FindChild("Text").GetComponent<Text>().text = "랜덤";
            Select.transform.FindChild("Toggle").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.FindChild("Stage").FindChild("stages").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.FindChild("Stage").FindChild("stages_r").gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit.SetActive(!Quit.activeSelf);
        }
        
        for(int i=0; i<7; i++)
        {
            if (i < GameData.getSpaceOpen() + 1)
            {
                SpaceList.transform.GetChild(i).GetComponent<Button>().interactable = true;
                SpaceList.transform.GetChild(i).FindChild("Image").gameObject.SetActive(false);
                SpaceList.transform.GetChild(i).FindChild("Text").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                SpaceList.transform.GetChild(i).FindChild("Text").GetComponent<Text>().color = new Color32(255, 255, 255, 155);
                SpaceList.transform.GetChild(i).FindChild("Image").gameObject.SetActive(true);
                SpaceList.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            
        }

        if (StageNum != -1)
        {
            Select.GetComponent<Button>().transform.FindChild("Text").GetComponent<Text>().text
                = "사냥 시작";
            Select.GetComponent<Button>().interactable = true;
        }
        else
        {
            Select.GetComponent<Button>().transform.FindChild("Text").GetComponent<Text>().text
               = "스테이지\n미선택";
            Select.GetComponent<Button>().interactable = false;
        }

        
        int EQsize = GameData.getEquipmentSize();

        for (int i = 0; i < EquipInList.transform.childCount; i++)
        {

            if (i > EQsize - 1)
            {
                EquipInList.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            else
            {
                EquipInList.transform.GetChild(i).GetComponent<Button>().interactable = true;

                if (GameData.getEquipmentID(i) != -1)
                {
                    EquipInList.transform.GetChild(i).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.getEquipmentID(i)].id);
                    EquipInList.transform.GetChild(i).FindChild("Text").GetComponent<Text>().text = GameData.getEquipmentPercent(i) + "%";

                    if (GameData.getEquipmentPercent(i) < 50)
                    {
                        if (GameData.getEquipmentPercent(i) < 20)
                            EquipInList.transform.GetChild(i).FindChild("Text").GetComponent<Text>().color = new Color32(250, 30, 30, 255);
                        else EquipInList.transform.GetChild(i).FindChild("Text").GetComponent<Text>().color = new Color32(130, 250, 90, 255);
                    }
                    else EquipInList.transform.GetChild(i).FindChild("Text").GetComponent<Text>().color = new Color32(66, 239, 222, 255);

                    if (GameData.getEquipmentPercent(i) == 0) {
                        EquipInList.transform.GetChild(i).GetComponent<Button>().interactable = false;
                        EquipInList.transform.GetChild(i).FindChild("Image").GetComponent<Image>().color = new Color32(120, 120, 120, 255);
                    }
                    else EquipInList.transform.GetChild(i).FindChild("Image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    
                }
                else
                {
                    EquipInList.transform.GetChild(i).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
                    EquipInList.transform.GetChild(i).FindChild("Text").GetComponent<Text>().text = "";

                }
            }

        }

        if(GameData.getItemDef() !=0)
        {
            Info.transform.FindChild("Defense").FindChild("Text").GetComponent<Text>().text = (GameData.getDeffens() + GameData.getItemDef()) + "";
            Info.transform.FindChild("Defense").FindChild("Text_+").gameObject.SetActive(true);
            Info.transform.FindChild("Defense").FindChild("Text_+").GetComponent<Text>().text = "( + " + GameData.getItemDef() + " )";

        }
        else Info.transform.FindChild("Defense").FindChild("Text_+").gameObject.SetActive(false);
        if (GameData.getItemPower()!=0)
        {

            Info.transform.FindChild("Attack").FindChild("Text").GetComponent<Text>().text = (GameData.getPower() + GameData.getItemPower()) + "";
            Info.transform.FindChild("Attack").FindChild("Text_+").gameObject.SetActive(true);
            Info.transform.FindChild("Attack").FindChild("Text_+").GetComponent<Text>().text = "( + " + GameData.getItemPower() + " )";
        }
        else Info.transform.FindChild("Attack").FindChild("Text_+").gameObject.SetActive(false);

        if (ItemIndex == 999)
        {
            UI_Gun.gameObject.SetActive(false);
            UI_Weapon.gameObject.SetActive(true);
            UI_Weapon.sprite = Resources.Load<Sprite>("Img/IT999");
            MyItem.transform.GetChild(0).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
            GameData.setEquippedItem(999);
        }
        if (xItemIndex !=ItemIndex)
        {
            if (xItemIndex != 999)
            {
                EquipInList.transform.GetChild(xItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[1];
                EquipInList.transform.GetChild(xItemIndex).transform.FindChild("Check").gameObject.SetActive(false);


            }
            if (ItemIndex != 999)
            {
                EquipInList.transform.GetChild(ItemIndex).GetComponent<Image>().sprite= Resources.LoadAll<Sprite>("UI/icn_item")[5];
                EquipInList.transform.GetChild(ItemIndex).transform.FindChild("Check").gameObject.SetActive(true);
                if (GameData.ItemList[GameData.getEquipmentID(ItemIndex)].itemType == ItemType.Weapon)
                {
                    if (Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(ItemIndex)].id))
                    {
                        UI_Gun.gameObject.SetActive(false);
                        UI_Weapon.gameObject.SetActive(true);
                        UI_Weapon.sprite = Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(ItemIndex)].id);
                        EquippedW = Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(ItemIndex)].id);
                    }
                }

                if (GameData.ItemList[GameData.getEquipmentID(ItemIndex)].itemType == ItemType.Gun)
                {
                    if (Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(ItemIndex)].id))
                    {
                        UI_Gun.gameObject.SetActive(true);
                        UI_Weapon.gameObject.SetActive(false);
                        UI_Gun.sprite = Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(ItemIndex)].id);
                        EquippedW = Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(ItemIndex)].id);
                    }
                }

                MyItem.transform.GetChild(0).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.getEquipmentID(ItemIndex)].id);
            }
            else
            {
                InvenZoomName.text = "";
                InvenZoomPower.text = "";
                InvenZoomDef.text = "";
            }


            xItemIndex = ItemIndex;
        }
        if (AItemIndex == 999)
        {
            GameData.setEquippedArmor(999);
            MyItem.transform.GetChild(2).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
        }
        if (xAItemIndex != AItemIndex)
        {

            if (xAItemIndex != 999)
            {
                EquipInList.transform.GetChild(xAItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[1];
                EquipInList.transform.GetChild(xAItemIndex).transform.FindChild("Check").gameObject.SetActive(false);
            }
            if (AItemIndex != 999)
            {
                EquipInList.transform.GetChild(AItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[6];
                EquipInList.transform.GetChild(AItemIndex).transform.FindChild("Check").gameObject.SetActive(true);

                MyItem.transform.GetChild(2).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.getEquipmentID(AItemIndex)].id);
            }
            else
            {


                InvenZoomName.text = "";
                InvenZoomPower.text = "";
                InvenZoomDef.text = "";
                GameData.setEquippedArmor(999);
                MyItem.transform.GetChild(2).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
            }


            xAItemIndex = AItemIndex;
        }

        if (HItemIndex == 999)
        {
            GameData.setEquippedHead(999);
            MyItem.transform.GetChild(1).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
        }
        if (xHItemIndex != HItemIndex)
        {

            if (xHItemIndex != 999)
            {
                EquipInList.transform.GetChild(xHItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[1];
                EquipInList.transform.GetChild(xHItemIndex).transform.FindChild("Check").gameObject.SetActive(false);
            }
            if (HItemIndex != 999)
            {
                EquipInList.transform.GetChild(HItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[6];
                EquipInList.transform.GetChild(HItemIndex).transform.FindChild("Check").gameObject.SetActive(true);

                MyItem.transform.GetChild(1).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.getEquipmentID(HItemIndex)].id);
            }
            else
            {
                InvenZoomName.text = "";
                InvenZoomPower.text = "";
                InvenZoomDef.text = "";
                GameData.setEquippedHead(999);
                MyItem.transform.GetChild(1).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
            }


            xHItemIndex = HItemIndex;
        }

        if (SItemIndex == 999)
        {
            GameData.setEquippedShoes(999);
            MyItem.transform.GetChild(3).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
        }
        if (xSItemIndex != SItemIndex)
        {

            if (xSItemIndex != 999)
            {
                EquipInList.transform.GetChild(xSItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[1];
                EquipInList.transform.GetChild(xSItemIndex).transform.FindChild("Check").gameObject.SetActive(false);
            }
            if (SItemIndex != 999)
            {
                EquipInList.transform.GetChild(SItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[6];
                EquipInList.transform.GetChild(SItemIndex).transform.FindChild("Check").gameObject.SetActive(true);

                MyItem.transform.GetChild(3).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.getEquipmentID(SItemIndex)].id);
            }
            else
            {
                InvenZoomName.text = "";
                InvenZoomPower.text = "";
                InvenZoomDef.text = "";
                GameData.setEquippedShoes(999);
                MyItem.transform.GetChild(3).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
            }


            xSItemIndex = SItemIndex;
        }

        if (AcItemIndex == 999)
        {
            GameData.setEquippedAcc(999);
            MyItem.transform.GetChild(4).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
        }
        if (xAcItemIndex != AcItemIndex)
        {

            if (xAcItemIndex != 999)
            {
                EquipInList.transform.GetChild(xAcItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[1];
                EquipInList.transform.GetChild(xAcItemIndex).transform.FindChild("Check").gameObject.SetActive(false);
            }
            if (AcItemIndex != 999)
            {
                EquipInList.transform.GetChild(AcItemIndex).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[4];
                EquipInList.transform.GetChild(AcItemIndex).transform.FindChild("Check").gameObject.SetActive(true);

                MyItem.transform.GetChild(4).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.getEquipmentID(AcItemIndex)].id);
            }
            else
            {

                InvenZoomName.text = "";
                InvenZoomPower.text = "";
                InvenZoomDef.text = "";
                GameData.setEquippedAcc(999);
                MyItem.transform.GetChild(4).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
            }


            xAcItemIndex = AcItemIndex;
        }


        if (StageNum != -1)
        {
            Info.transform.FindChild("Stage").FindChild("Text").GetComponent<Text>().text = StageName;
            Info.transform.FindChild("Stage").FindChild("Text").GetComponent<Text>().color = new Color(0.6f, 1, 1);
        }
        else
        {
            if (xStage) xStage.interactable = true;
            Info.transform.FindChild("Stage").FindChild("Text").GetComponent<Text>().text = "미선택";
            Info.transform.FindChild("Stage").FindChild("Text").GetComponent<Text>().color = new Color32(238, 233, 230, 200);
        }

        if (AcItemIndex == 999 && ItemIndex == 999)
            Info.transform.FindChild("Attack").FindChild("Text").GetComponent<Text>().text = "" + GameData.getPower();
        
        if(AcItemIndex ==999 && AItemIndex ==999)
            Info.transform.FindChild("Defense").FindChild("Text").GetComponent<Text>().text = "" + GameData.getDeffens();

    }
    public void OnClickEquipment(Button EQbtn)
    {
        int EQindex = int.Parse(EQbtn.name);
        int EQid = GameData.getEquipmentID(EQindex);
        if (EQid != -1 && GameData.getEquipmentPercent(EQindex)!=0)
        {
            
            InvenZoomName.text = GameData.ItemList[EQid].name + "";

            InvenZoomPower.text
                  = "0";
            InvenZoomDef.text
                  = "0";

            if (GameData.ItemList[EQid].Power != "0")
            {
                int min = int.Parse(GameData.ItemList[EQid].Power.Split('_')[0]);
                int max = int.Parse(GameData.ItemList[EQid].Power.Split('_')[1]);

                InvenZoomPower.text
                    = min + "~" + max;
            }
            if (GameData.ItemList[EQid].Def != "0")
            {
                int min = int.Parse(GameData.ItemList[EQid].Def.Split('_')[0]);
                int max = int.Parse(GameData.ItemList[EQid].Def.Split('_')[1]);
                
                    InvenZoomDef.text
                         =  min + "~" + max;
                
            }
            

            if (GameData.ItemList[EQid].itemType == ItemType.Weapon || GameData.ItemList[EQid].itemType == ItemType.Gun)
            {
                if (ItemIndex == EQindex) ItemIndex = 999;
                else ItemIndex = EQindex; GameData.setEquippedItem(EQindex);
            }
            else if (GameData.ItemList[EQid].itemType == ItemType.Armor)
            {

                if (AItemIndex == EQindex)
                {
                    AItemIndex = 999;
                }
                else
                { 
                    AItemIndex = EQindex; GameData.setEquippedArmor(EQindex);
                }
            }
            else if (GameData.ItemList[EQid].itemType == ItemType.Head)
            {

                if (HItemIndex == EQindex)
                {
                    HItemIndex = 999;
                }
                else
                {
                    HItemIndex = EQindex; GameData.setEquippedHead(EQindex);
                }
            }
            else if (GameData.ItemList[EQid].itemType == ItemType.Shoes)
            {

                if (SItemIndex == EQindex)
                {
                    SItemIndex = 999;
                }
                else
                {
                    SItemIndex = EQindex; GameData.setEquippedShoes(EQindex);
                }
            }
            else if (GameData.ItemList[EQid].itemType == ItemType.Accessory)
            {

                if (AcItemIndex == EQindex)
                {
                    AcItemIndex = 999;
                }
                else
                {
                    AcItemIndex = EQindex; GameData.setEquippedAcc(EQindex);
                }
            }
            else { }
        }
        else
        {
        }
    }
    public void OnClickBack()
    {
        if (GameObject.Find("Main Camera"))
        {
            StopAllCoroutines();
            GameObject.Find("Audio").transform.FindChild("space").gameObject.SetActive(false);
            GameObject load = GameObject.Find("Loading").transform.FindChild("LoadingScene").gameObject;
            GameObject.Find("Main Camera").SetActive(false);
            load.SetActive(true);
            load.transform.FindChild("Loading").GetComponent<loading>().targetName = "shop";
        }
    }
    public void OnClickGo()
    {
        
        if (StageNum != -1)
        {
            //GameData.loadingScene = "hunt";
            //SceneManager.LoadScene("loading");

            if (Select.transform.FindChild("Toggle").GetComponent<Toggle>().isOn) GameData.isAutoHunt = true;
            else GameData.isAutoHunt = false;

            if (GameObject.Find("Main Camera"))
            {
                StopAllCoroutines();
                GameObject.Find("Audio").transform.FindChild("space").gameObject.SetActive(false);
                GameObject load = GameObject.Find("Loading").transform.FindChild("LoadingScene").gameObject;
                GameObject.Find("Main Camera").SetActive(false);
                load.SetActive(true);
                load.transform.FindChild("Loading").GetComponent<loading>().targetName = "hunt";
               
            }
        }
       
    }
    public void OnClickSpace(int SpaceNum)
    {
        if (!isRandom)
        {
            GameData.setSpaceNum(SpaceNum);
            string SpaceName = "";

            if (SpaceNum == 0) SpaceName = "오도르";
            else if (SpaceNum == 1) SpaceName = "토우";
            else if (SpaceNum == 2) SpaceName = "프라이";
            else if (SpaceNum == 3) SpaceName = "튜바나";
            else if (SpaceNum == 4) SpaceName = "이드";
            else if (SpaceNum == 5) SpaceName = "세니";
            else SpaceName = "Mr.R";

            Info.transform.FindChild("Space").FindChild("Text").GetComponent<Text>().text = SpaceName;
            StageNum = -1;
        }
    }
    public void OnClickStage(Button SelectedS)
    {
        if(xStage) xStage.interactable = true;
        SelectedS.GetComponent<Button>().interactable = false;
        StageNum = int.Parse(SelectedS.name);
        StageName = SelectedS.transform.FindChild("Text").GetComponent<Text>().text;
        GameData.StageNum = (GameData.getSpaceNum()+1)*10 + StageNum;
        xStage = SelectedS;
    }
    public void OnClickRStage(Button SelectedS)
    {
        if (!SelectedS.transform.FindChild("Lock").gameObject.activeInHierarchy)
        {
            if (xStage) xStage.interactable = true;
            SelectedS.GetComponent<Button>().interactable = false;
            StageNum = int.Parse(SelectedS.name);
            StageName = SelectedS.transform.FindChild("Text").GetComponent<Text>().text;
            GameData.StageNum = 90 + StageNum;
            GameData.setSpaceNum(StageNum - 1);
            xStage = SelectedS;
        }
    }

    public void OnClickSave()
    {
        GameData.SaveGame();
    }
    public void OnClickReset()
    {
        GameObject.Find("Audio").transform.FindChild("space").gameObject.SetActive(false);
        if (GameObject.Find("Main Camera"))
        {
            StopAllCoroutines();
            GameObject load = GameObject.Find("Loading").transform.FindChild("LoadingScene").gameObject;
            GameObject.Find("Main Camera").SetActive(false);
            load.SetActive(true);
            load.transform.FindChild("Loading").GetComponent<loading>().targetName = "new";
        }
    }
    public void OnClickQuit()
    {
        GameData.SaveGame();
        Application.Quit();
    }
}
