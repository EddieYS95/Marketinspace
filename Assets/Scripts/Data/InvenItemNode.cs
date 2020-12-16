using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InvenItemNode : MonoBehaviour
{
    public bool isEquipped;
	// Use this for initialization
	void Start () {
        isEquipped = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        int id = int.Parse(name);

        if (GameData.ItemInventory[id].Count <= 0) {
            GameData.ItemInventory[id].NodeType = NodeType.Empty;
            GameData.ItemInventory[id].id = -1;
        }

        if (GameData.ItemInventory[id].NodeType == NodeType.Item) { 

            transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.ItemInventory[id].id].id);
            if (GameData.ItemInventory[id].Count != 0)
                GetComponentInChildren<Text>().text = "x" + GameData.ItemInventory[id].Count;
            
        }
        if (GameData.ItemInventory[id].NodeType == NodeType.Empty) { transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("char/nill"); GetComponentInChildren<Text>().text = ""; }


        else
        {
            transform.FindChild("Image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            transform.FindChild("onsale").gameObject.SetActive(false);
            GetComponent<Button>().interactable = true;
        }

        
        
    }

    public void OnClick(Button selectItem)
    {
        int id = int.Parse(name);
        
        if (GameData.ItemInventory[id].NodeType == NodeType.Item)
        {
            if (GameObject.Find("GameManager")) {
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.ItemInventory[id].id].id);
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomName.text = GameData.ItemList[GameData.ItemInventory[id].id].name + "";
            GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomContent.text = GameData.ItemList[GameData.ItemInventory[id].id].script + "";
                if (GameData.ItemList[GameData.ItemInventory[id].id].itemType != ItemType.Drink)
                GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Equipment").gameObject.SetActive(true);
                else GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Equipment").gameObject.SetActive(false);

                GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Remove").GetComponent<Button>().interactable = true;


                if (GameData.getEquipmentID(GameData.getEquipmentSize() - 1) != -1)
                    GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Equipment").GetComponent<Button>().interactable = false;
                else GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Equipment").GetComponent<Button>().interactable = true;

                GameObject.Find("ShopManager").GetComponent<ShopManager>().EleID = -1;

            }
            if (GameObject.Find("ShopManager"))
            {
                if (transform.parent.name != "chain_item")
                {
                    GameObject.Find("ShopManager").GetComponent<ShopManager>().InvenZoomImage = Resources.Load<Sprite>("Img/" + GameData.ItemList[GameData.ItemInventory[id].id].id);
                    GameObject.Find("ShopManager").GetComponent<ShopManager>().ItemID = id;
                    GameObject.Find("ShopManager").GetComponent<ShopManager>().Coin.transform.parent.FindChild("content").GetComponent<Text>().text = "" + GameData.ItemList[GameData.ItemInventory[id].id].name;
                    GameObject.Find("ShopManager").GetComponent<ShopManager>().Coin.transform.parent.FindChild("count").GetComponent<Text>().text = "재고: " + GameData.ItemInventory[id].Count;
                    GameObject.Find("ShopManager").GetComponent<ShopManager>().Coin.SetActive(true);
                    GameObject.Find("ShopManager").GetComponent<ShopManager>().Coin.transform.FindChild("Text").GetComponent<Text>().text = "" + GameData.ItemList[GameData.ItemInventory[id].id].Cost;
                    GameObject.Find("ShopManager").GetComponent<ShopManager>().ItemButton = selectItem;
                }
                else
                {
                    GameObject.Find("Chain_store").GetComponent<ChainScript>().ItemID = id;
                }
            }
           


        }
    }
}
