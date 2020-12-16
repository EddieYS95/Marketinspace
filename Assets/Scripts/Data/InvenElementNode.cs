using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InvenElementNode : MonoBehaviour {

    public Sprite nullImg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int id = int.Parse(name);

        if (GameData.ElementInventory[id].Count <= 0)
        {
            GameData.ElementInventory[id].NodeType = NodeType.Empty;
        }

        if (GameData.ElementInventory[id].NodeType == NodeType.Element)
        {
            transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[GameData.ElementInventory[id].id].id);
            if (GameData.ElementInventory[id].Count != 0) 
            transform.FindChild("Count").gameObject.GetComponentInChildren<Text>().text = "x"+GameData.ElementInventory[id].Count ;
        }
        else
        {
            transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = nullImg;
            transform.FindChild("Count").gameObject.GetComponentInChildren<Text>().text = "";
        }
	}

    public void OnClick()
    {
        int id = int.Parse(name);
        if (GameData.ElementInventory[id].NodeType == NodeType.Element)
        {
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[GameData.ElementInventory[id].id].id);
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomName.text = GameData.ElementList[GameData.ElementInventory[id].id].name + "";
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomContent.text = GameData.ElementList[GameData.ElementInventory[id].id].script + "";
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Equipment").gameObject.SetActive(false);
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Remove").GetComponent<Button>().interactable = true;

            GameObject.Find("ShopManager").GetComponent<ShopManager>().EleID = id;
        }
    }
}
