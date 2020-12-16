using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteriorItem : MonoBehaviour {

    Text ItemName;
    public int type;
    int ItemID;

    GameObject Build;
    // Use this for initialization
    void Start()
    {
        Build = transform.parent.parent.parent.gameObject;
        ItemName = transform.FindChild("Text").GetComponent<Text>();
        ItemName.text = GameData.getInteriorName(int.Parse(name));

        ItemID = int.Parse(name);
    }
	
	// Update is called once per frame
	void Update () {

        if (GameData.getStar() >= GameData.InteriorList[ItemID].level)
        {
            
            transform.FindChild("lock").gameObject.SetActive(false);
            ItemName.color = new Color32(232, 223, 217, 255);
        }
        else
        {
            transform.FindChild("lock").gameObject.SetActive(true);
            ItemName.color = new Color32(161, 143, 124, 255);
        }
    }
    public void OnClickInteriorItem(Button selected)
    {
        int id = int.Parse(selected.name);
        int NeedMoney = 0;
        if (type == 0) NeedMoney = GameData.InteriorList[id].standcoin;
        else if (type == 1) NeedMoney = GameData.InteriorList[id].wallcoin;
        else if (type == 2) NeedMoney = GameData.InteriorList[id].doorcoin;
        else if (type == 3) NeedMoney = GameData.InteriorList[id].countercoin;

        if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().OnClickShops(selected,type);

        if (GameData.getStar() >= GameData.InteriorList[id].level)
        {
            if (type == 2)
            {
                if (GameData.getShopDoorInfo() != GameData.InteriorList[id].id)
                {
                    if (GameData.getMoney() < NeedMoney)
                    {
                        Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false;
                        Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "소지금 부족";
                    }
                    else
                    {
                        Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = true;
                        Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "건축";
                    }
                    
                }
                else
                {
                    Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false;
                    Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "사용중";
                }
            }
            else if (type == 3)
            {

                if (GameData.getShopCounterInfo() != GameData.InteriorList[id].id)
                {
                    if (GameData.getMoney() < NeedMoney)
                    {
                        Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false;
                        Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "소지금 부족";
                    }
                    else
                    {
                        Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = true;
                        Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "건축";
                    }
                }
                else
                {
                    Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false;
                    Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "사용중";
                }
            }
            else
            {
                if (GameData.getMoney() < NeedMoney)
                {
                    Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false;
                    Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "소지금 부족";
                }
                else
                {
                    Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = true;
                    Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "건축";
                }
            }
            
        }
        else {
            Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false;
            Build.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "명성 부족";
        }
    }
}
