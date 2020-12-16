using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MakingAbleChecker : MonoBehaviour {

    public bool Enough = false;
    public int count;

    int ID;
	// Use this for initialization
	void Start () {
        count = 1;
	}
	
	// Update is called once per frame
	void Update () {
        string ItemId = name.Split(':')[0];
        string NeedCount = name.Split(':')[1];
        string ElementId = name.Split(':')[2];
        ID = int.Parse(ElementId);

        NeedCount = ""+int.Parse(NeedCount) * count;

        int myInvenCount = 0;

        for (int i = 0; i < GameData.ElementInventory.Length; i++)
        {
            if (GameData.ElementInventory[i].NodeType == NodeType.Element)
            if (GameData.ElementInventory[i].id == int.Parse(ElementId))
            {
                myInvenCount = GameData.ElementInventory[i].Count;
            }
        }

        transform.FindChild("ItemCount").GetComponent<Text>().text = myInvenCount + " / " + NeedCount;
        if(myInvenCount < int.Parse(NeedCount)) transform.FindChild("ItemCount").GetComponent<Text>().color = new Color(0.7f, 0, 0);
        else transform.FindChild("ItemCount").GetComponent<Text>().color = new Color(0.9f, 0.9f, 0.9f);


        if (myInvenCount >= int.Parse(NeedCount))
        {
            Enough = true;
        }
        else {
            Enough = false;
        }
    }

    public void OnClickElement()
    {
        GameObject EleInfo = GameObject.Find("ShopCanvas").transform.FindChild("EleNodeData").gameObject;
        EleInfo.SetActive(true);

        EleInfo.transform.FindChild("Item").FindChild("nameTag").GetComponent<Text>().text = GameData.ElementList[ID].name;
        EleInfo.transform.FindChild("Item").FindChild("accountTag").GetComponent<Text>().text = GameData.ElementList[ID].script;
        EleInfo.transform.FindChild("Item").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[ID].id);

        EleInfo.transform.FindChild("Item").FindChild("GetMask").FindChild("Get").GetComponent<RectTransform>().localPosition = new Vector2(0, -35);
        EleInfo.transform.FindChild("Item").FindChild("GetMask").FindChild("Get").GetComponent<Text>().text = GameData.ElementList[ID].get;
    }
    
}
