using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CharInfoScript : MonoBehaviour {

   GameObject Setting;
    public GameObject ConfirmP;
    public GameObject ChargeP;
    public GameObject ErrorP;

    public GameObject MyScorePanel;
    string BuyType;
    string itemT;
    public GameObject CouponPanel;

    public GameObject BricksScore;
    public GameObject StarScore;

    public GameObject myBricksRank;
    public GameObject myStarRank;

    public string rank_bricks;
    public string rank_star;

    int myrank_bricks;
    int myrank_star;

    // Use this for initialization
    void Start () {
        BuyType = "";
        itemT = "";
        Setting = transform.FindChild("SettingP").gameObject;
        StartCoroutine(getRank());
	}
	
	// Update is called once per frame
	void Update () {


        if (Setting.transform.FindChild("Panel").FindChild("SaveP").GetComponent<CanvasGroup>().alpha != 0)
            Setting.transform.FindChild("Panel").FindChild("SaveP").GetComponent<CanvasGroup>().alpha -= Time.deltaTime;

        transform.FindChild("name").GetComponent<Text>().text = "" + GameData.getName();
        transform.FindChild("level").GetComponent<Text>().text = "Lv. " + GameData.getLevel();
        transform.FindChild("level").transform.FindChild("Slider").GetComponent<Slider>().value = GameData.getExp();
        transform.FindChild("level").transform.FindChild("Slider").GetComponent<Slider>().maxValue = GameData.LevelUpExp[GameData.getLevel() - 1];
        transform.FindChild("Star").FindChild("star").GetComponent<Text>().text = "" + GameData.getStar();
        transform.FindChild("Emerald").FindChild("emerald").GetComponent<Text>().text = "" + GameData.getEmerald();

        if (GameData.getMoney() != 0) transform.FindChild("Block").FindChild("block").GetComponent<Text>().text = "" + string.Format("{0:#,###}", GameData.getMoney());
        else transform.FindChild("Block").FindChild("block").GetComponent<Text>().text = "0";

    }
    public void OnClickSetting()
    {
        Setting.SetActive(true);
        if (GameObject.Find("ShopManager")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = true;
    }
    public void OnClickSave()
    {
        GameData.SaveGame();
    }
    public void OnClickReset()
    {
        if (GameObject.Find("ShopManager")) {
            GameObject.Find("Audio").transform.FindChild("shop").gameObject.SetActive(false);
            GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().ActiveUI();
        }

        if (GameObject.Find("EquipManager")) GameObject.Find("Audio").transform.FindChild("space").gameObject.SetActive(false);

        //if (GameObject.Find("EquipManager")) transform.FindChild("shop").gameObject.SetActive(false);
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

    public void OnClickBuyItem(GameObject buyitem)
    {
        

        if (buyitem.transform.parent.name == "ChargeList_i") itemT = "브릭스";
        else itemT = "에메랄드"; 
        BuyType = buyitem.name;
        ConfirmP.SetActive(true);
        ConfirmP.transform.FindChild("Content").GetComponent<Text>().text = itemT + " " + string.Format("{0:#,###}", int.Parse(BuyType)) + "개를 구입하시겠습니까?";

    }
    public void OnClickBuyDecide()
    {
       int NeedEmerald = 0;
       int NeedMoney = 0;

        if (itemT == "브릭스") //브릭스일때
        {
            if (BuyType == "1000") NeedEmerald = 2;
            else if(BuyType  == "3000") NeedEmerald = 5;
            else if (BuyType == "5000") NeedEmerald = 8;
            else if (BuyType == "10000") NeedEmerald = 15;
            else if (BuyType == "50000") NeedEmerald = 70;
            else NeedEmerald = 130;

            if (GameData.getEmerald() < NeedEmerald) ErrorP.SetActive(true);
            else { ChargeP.SetActive(true); GameData.useEmerald(NeedEmerald); GameData.addMoney(int.Parse(BuyType)); }

            ConfirmP.SetActive(false);
        }
       else
        {

            //에메랄드 일때
            if (BuyType == "10") GetComponent<emeraldshopManager>().BuyItem("emerald_10");
            else if (BuyType == "33") GetComponent<emeraldshopManager>().BuyItem("emerald_30");
            else if (BuyType == "58") GetComponent<emeraldshopManager>().BuyItem("emerald_50");
            else if (BuyType == "120") GetComponent<emeraldshopManager>().BuyItem("emerald_100");
            else if (BuyType == "630") GetComponent<emeraldshopManager>().BuyItem("emerald_500");
            else GetComponent<emeraldshopManager>().BuyItem("emerald_1000");

            // ErrorP.SetActive(true);
            //{ ChargeP.SetActive(true); GameData.addEmerald(int.Parse(BuyType)); }

            ConfirmP.SetActive(false);
        }

        BuyType = "0";
    }


    public void SetScoreList()
    {

        // + 명성 랭킹 
        for (int i = 0; i < StarScore.transform.childCount; i++)
        {
            Debug.Log(rank_star);
            string text = rank_star.Split('-')[i];
            Debug.Log(i + "  " + text);
            StarScore.transform.GetChild(i).Find("Text (1)").GetComponent<Text>().text = text.Split('|')[1];
            StarScore.transform.GetChild(i).Find("Text (2)").GetComponent<Text>().text = "Lv." + text.Split('|')[2] + " " + text.Split('|')[0];
        }

        // + 브릭스 랭킹
        for (int i = 0; i < BricksScore.transform.childCount; i++)
        {
            string text = rank_bricks.Split('-')[i];
            if(int.Parse(text.Split('|')[1]) !=0)
            BricksScore.transform.GetChild(i).Find("Text (1)").GetComponent<Text>().text = string.Format("{0:#,###}", int.Parse(text.Split('|')[1]));
            else BricksScore.transform.GetChild(i).Find("Text (1)").GetComponent<Text>().text = "0";
            BricksScore.transform.GetChild(i).Find("Text (2)").GetComponent<Text>().text = "Lv." + text.Split('|')[2] + " " + text.Split('|')[0];
        }

        myBricksRank.GetComponent<Text>().text = myrank_bricks + " 위";
        myStarRank.GetComponent<Text>().text = myrank_star + " 위";
            
                // + 나의 랭킹

        MyScorePanel.transform.FindChild("Nick").GetComponent<Text>().text = "Lv." + GameData.getLevel() + " " + GameData.getName();


        int EQID = -1;
        int EQCost = 0;

        for (int i=0; i<6; i++)
        {
            if (GameData.getEquipmentID(i) != -1) {

                if (GameData.ItemList[GameData.getEquipmentID(i)].itemType == ItemType.Weapon || GameData.ItemList[GameData.getEquipmentID(i)].itemType == ItemType.Gun)
                {
                    if (EQCost < GameData.ItemList[GameData.getEquipmentID(i)].Cost) EQID = i;
                    
                }
                
             }
         }

        if (EQID == -1)
        {
            MyScorePanel.transform.FindChild("char").FindChild("weapon").gameObject.SetActive(true);
            MyScorePanel.transform.FindChild("char").FindChild("weapon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Img/IT999");
            MyScorePanel.transform.FindChild("char").FindChild("gun").gameObject.SetActive(false);
        }
        else
        {

            if (GameData.ItemList[GameData.getEquipmentID(EQID)].itemType == ItemType.Weapon)
            {
                MyScorePanel.transform.FindChild("char").FindChild("weapon").gameObject.SetActive(true);
                MyScorePanel.transform.FindChild("char").FindChild("weapon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(EQID)].id);
                MyScorePanel.transform.FindChild("char").FindChild("gun").gameObject.SetActive(false);
            }
            else
            {
                MyScorePanel.transform.FindChild("char").FindChild("gun").gameObject.SetActive(true);
                MyScorePanel.transform.FindChild("char").FindChild("gun").GetComponent<Image>().sprite = Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.getEquipmentID(EQID)].id);
                MyScorePanel.transform.FindChild("char").FindChild("weapon").gameObject.SetActive(false);
            }
        }   
    }

    public void ChkCouponCode()
    {

        StartCoroutine(chkCupon(CouponPanel.transform.FindChild("InputField").GetComponent<InputField>().text));
      
    }

    IEnumerator getRank()
    {
        WWW www = new WWW("http://52.79.56.182/mis_server.php?Mode=Rank_Bricks");
        yield return www;
    
        rank_bricks = www.text;
        Debug.Log(rank_bricks);
        string[] TT = www.text.Split('-');

        for (int i = 0; i < TT.Length-1; i++)
        {
            if (TT[i].Split('|')[0].Equals(GameData.getName()))
            {
                Debug.Log("r" + (i + 1));
                myrank_bricks = i + 1;
            }
        }

        WWW www2 = new WWW("http://52.79.56.182/mis_server.php?Mode=Rank_star");
        yield return www2;

        rank_star = www2.text;
        Debug.Log(rank_star);
        string[] TT2 = www2.text.Split('-');

        for (int i = 0; i < TT2.Length-1; i++)
        {
            if (TT2[i].Split('|')[0].Equals(GameData.getName()))
            {
                Debug.Log("s" + (i + 1));
                myrank_star = i + 1;
            }
        }
    }
    IEnumerator chkCupon(string cuponnum)
    {
        WWW www = new WWW("http://52.79.56.182/mis_server.php?Mode=chkCupon&cupon=" + cuponnum);
        yield return www;

        if (string.IsNullOrEmpty(www.text) == false)
        {
            if (www.text.StartsWith("ERR1"))
            {

                Debug.Log("코드실패");
                CouponPanel.transform.FindChild("ErrorP").gameObject.SetActive(true);
            }
            else if (www.text.StartsWith("ERR2"))
            {

                Debug.Log("코드실패");
                CouponPanel.transform.FindChild("ErrorP").gameObject.SetActive(true);
            }
            else
            {
                int RewordEme = 20;
                int RewordItemID = 21;
                CouponPanel.transform.FindChild("FinP").gameObject.SetActive(true);
                CouponPanel.transform.FindChild("FinP").FindChild("Reword").GetComponent<Text>().text = "보상 : 에메랄드" + RewordEme + "개, " + GameData.ItemList[RewordItemID].name;
                GameData.addEmerald(RewordEme);
                GameData.setItem(RewordItemID);
            }
        }
    }

}
