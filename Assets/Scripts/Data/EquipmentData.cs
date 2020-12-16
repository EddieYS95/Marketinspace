using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentData : MonoBehaviour {

    int EQsize;
    int ItemID;
    int EIndex;
    int Percent;

    int RepairPoint;
    int RepairCoin;

    public GameObject EQ;

    // Use this for initialization
    void Start () {
        Percent = 0;
        EIndex= -1;
        ItemID = -1;
        RepairCoin = 100;
        RepairPoint = 20;
	}
	
	// Update is called once per frame
	void Update () {

        EQsize = GameData.getEquipmentSize();
       
        for(int i=0; i<transform.childCount; i++)
        {
           
            if (i > EQsize-1)
            {
                transform.GetChild(i).GetComponent<Button>().interactable = false;
                transform.GetChild(i).FindChild("eq").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
                if (i == EQsize) transform.GetChild(i).FindChild("Plus").gameObject.SetActive(true);
                
            }
            else
            {
                transform.GetChild(i).GetComponent<Button>().interactable =true;

                transform.GetChild(i).FindChild("eq").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                transform.GetChild(i).FindChild("Plus").gameObject.SetActive(false);
                if (GameData.getEquipmentID(i) != -1)
                {
                    transform.GetChild(i).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.getEquipmentID(i)].id);
                    transform.GetChild(i).FindChild("Text").GetComponent<Text>().text = GameData.getEquipmentPercent(i) + "%";
                    if (GameData.getEquipmentPercent(i) < 50) {
                            transform.GetChild(i).FindChild("Text").GetComponent<Text>().color = new Color32(250, 30, 30, 255);
                    }
                    else transform.GetChild(i).FindChild("Text").GetComponent<Text>().color = new Color32(231, 223, 217, 255);
                }
            
                else
                {
                    transform.GetChild(i).FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill");
                    transform.GetChild(i).FindChild("Text").GetComponent<Text>().text = "";
                    
                }
            }

        }
	}
    public void OnClickItem(string Mnum)
    {
        EIndex = int.Parse(Mnum);
        ItemID = GameData.getEquipmentID(EIndex);
        Percent = GameData.getEquipmentPercent(EIndex);

       
            if (ItemID != -1)
            {
                EQ.transform.FindChild("Image").GetComponent<Image>().sprite= Resources.Load<Sprite>("Item/" + GameData.ItemList[ItemID].id);
                EQ.transform.FindChild("Name").GetComponent<Text>().text = GameData.ItemList[ItemID].name + "";
                EQ.SetActive(true);
                EQ.transform.FindChild("Percent").GetComponent<Text>().text = "내구도: " + Percent + "%";
                if (Percent < 50)
                {
                        EQ.transform.FindChild("Percent").GetComponent<Text>().color = new Color32(250, 30, 30, 255);
                }
                else EQ.transform.FindChild("Percent").GetComponent<Text>().color = new Color32(231, 223, 217, 255);

          
            EQ.transform.FindChild("Info").GetComponent<Text>().text = "";

            if (GameData.ItemList[ItemID].Power != "0")
            {
                EQ.transform.FindChild("Type").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[5];
                int min = int.Parse(GameData.ItemList[ItemID].Power.Split('_')[0]);
                int max = int.Parse(GameData.ItemList[ItemID].Power.Split('_')[1]);

                EQ.transform.FindChild("Info").GetComponent<Text>().text
                    = "공격력: " + min + "~" + max;
                EQ.transform.FindChild("Info").GetComponent<Text>().color
               = new Color32(66, 239, 222, 255);
            }
            if (GameData.ItemList[ItemID].Def != "0")
            {
                int min = int.Parse(GameData.ItemList[ItemID].Def.Split('_')[0]);
                int max = int.Parse(GameData.ItemList[ItemID].Def.Split('_')[1]);

                if (EQ.transform.FindChild("Info").GetComponent<Text>().text != "")
                {

                    EQ.transform.FindChild("Type").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[4];
                    EQ.transform.FindChild("Info").GetComponent<Text>().text
                        += "\n방어력: " + min + "~" + max;
                    EQ.transform.FindChild("Info").GetComponent<Text>().color
                    = new Color32(255, 100, 240, 255);
                }
                else
                {

                    EQ.transform.FindChild("Type").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_item")[2];
                    EQ.transform.FindChild("Info").GetComponent<Text>().text
                        = "방어력: " + min + "~" + max;
                    EQ.transform.FindChild("Info").GetComponent<Text>().color
                    = new Color32(247, 174, 74, 255);
                }
               
            }


            EQ.transform.FindChild("repair_").FindChild("Text").GetComponent<Text>().text = "내구도를 "+RepairPoint+"% 수리합니다.";
            EQ.transform.FindChild("repair_").FindChild("Coin").FindChild("Text").GetComponent<Text>().text = RepairCoin+"";

            if (Percent != 100)
                {
                    EQ.transform.FindChild("Repair").gameObject.SetActive(true);
                    EQ.transform.FindChild("Return").gameObject.SetActive(false);
                }
                else
                {
                    EQ.transform.FindChild("Repair").gameObject.SetActive(false);
                    EQ.transform.FindChild("Return").gameObject.SetActive(true);
                }
                }
            else
            {
                ClearContent();
            }
        
      
    }


    void ClearContent()
    {
        GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.sprite = Resources.Load<Sprite>("char/nill");
        GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomName.text = "";
        GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomContent.text = "";
        GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Equipment").gameObject.SetActive(false);
        ItemID = -1;
        EIndex = -1;
    }
    public void OnReturnItem()
    {
        if (ItemID != -1 && GameData.getEquipmentPercent(EIndex) == 100)
        {
            GameData.returnEquipment(EIndex);
            ClearContent();
        }
    }

    public void OnRepairItem()
    {
        if (ItemID != -1)
        {
            GameData.setEquipmentPercent(EIndex,GameData.getEquipmentPercent(EIndex)+RepairPoint);
            Percent = GameData.getEquipmentPercent(EIndex);
            OnClickItem(EIndex+"");
            EQ.transform.FindChild("Percent").GetComponent<Text>().text = "내구도: " + Percent + "%";
            GameData.useMoney(RepairCoin);
        }
    }
    
    public void OnRemoveItem()
    {
        if (EIndex != -1)
        {
            GameData.removeEquipment(EIndex);
            ClearContent();
        }
    }
    public void OnClickPlus()
    {
        GameData.addEquipmentSize();
    }
}
