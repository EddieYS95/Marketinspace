using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MakeBookScript : MonoBehaviour {


    public GameObject Search;
    public GameObject BookItemList;

    public GameObject EleBookItemList;
    public GameObject ItemInfo;
    
    public GameObject EleInfo;

    public GameObject BookItem;
    Sprite Able;
    Sprite UnAble;

    Image bookItemImg;
    Text bookItemNameTag;
    Text bookItemScriptTag;

    Text Attack;
    Text Star;

    Image bookEleImg;
    Text bookEleNameTag;
    Text bookEleScriptTag;
    Text Get;

    GameObject xButton;
    // Use this for initialization
    void Start () {
        xButton = Search.transform.FindChild("weapon").gameObject;
        Able = Resources.LoadAll<Sprite>("UI/icn_btn")[6];
        UnAble = Resources.LoadAll<Sprite>("UI/icn_btn")[7];
        bookItemImg = ItemInfo.transform.FindChild("Item").FindChild("Image").GetComponent<Image>();
        bookItemNameTag = ItemInfo.transform.FindChild("Item").FindChild("nameTag").GetComponent<Text>();
        bookItemScriptTag = ItemInfo.transform.FindChild("Item").FindChild("accountTag").GetComponent<Text>();
        
        Attack = ItemInfo.transform.FindChild("Item").FindChild("Attack").GetComponent<Text>();
        Star = ItemInfo.transform.FindChild("Item").FindChild("Star").GetComponent<Text>();

        bookEleImg = EleInfo.transform.FindChild("Item").FindChild("Image").GetComponent<Image>();
        bookEleNameTag = EleInfo.transform.FindChild("Item").FindChild("nameTag").GetComponent<Text>();
        bookEleScriptTag = EleInfo.transform.FindChild("Item").FindChild("accountTag").GetComponent<Text>();
        Get = EleInfo.transform.FindChild("Item").FindChild("GetMask").FindChild("Get").GetComponent<Text>();

        List<ItemType> types = new List<ItemType>();
        types.Add(ItemType.Weapon);
        GameData.setForgeAtItemList(types, 4);
        setBookItemList();
        setEleItemList();
    }
	
	// Update is called once per frame
	void Update () {
	
	}



    public void OnClickTagBt(GameObject bt)
    {
        bool isMake = false;
        /*
        if (xButton == bt && bt.GetComponent<Image>().sprite == UnAble && !bt.name.Equals("weapon"))
        {
            bt.GetComponent<Image>().sprite = Able;
            isMake = true;
            xButton = Search.transform.FindChild("weapon").gameObject;
            xButton.GetComponent<Image>().sprite = UnAble;

            Debug.Log(xButton);
        }*/

        if (isMake == false)
        {

            if (xButton)
                xButton.GetComponent<Image>().sprite = Able;
            xButton = bt;
            bt.GetComponent<Image>().sprite = UnAble;
            List<ItemType> types = new List<ItemType>();

            //setForgeAtItemList(ItemType[] tags, bool isAll, int SoltType)

            if (bt.name.Equals("weapon"))
            {
                types.Add(ItemType.Weapon);
            }
            else if (bt.name.Equals("armor"))
            {
                types.Add(ItemType.Armor);
                types.Add(ItemType.Head);
                types.Add(ItemType.Shoes);
            }
            else if (bt.name.Equals("accessory"))
            {
                types.Add(ItemType.Accessory);
            }
            else if (bt.name.Equals("etc"))
            {
                types.Add(ItemType.Drink);
            }
            else if (bt.name.Equals("gun"))
            {
                types.Add(ItemType.Gun);
            }
            GameData.setForgeAtItemList(types, 4);
            setBookItemList();

        }
        else
        {
            List<ItemType> types = new List<ItemType>();
            GameData.setForgeAtItemList(types, 4);
            setBookItemList();

        }

    }

    public void OnClickItemNode(GameObject Select)
    {
        
        ItemInfo.SetActive(true);

        string id = Select.name.Substring(2, 3);
        bookItemNameTag.text = GameData.ItemList[int.Parse(id)].name;
        bookItemScriptTag.text = GameData.ItemList[int.Parse(id)].script;
        Star.text = Select.transform.FindChild("Text").GetComponent<Text>().text;
        
        Attack.text = "";

        if (GameData.ItemList[int.Parse(id)].Power != "0")
        {
            int min = int.Parse(GameData.ItemList[int.Parse(id)].Power.Split('_')[0]);
            int max = int.Parse(GameData.ItemList[int.Parse(id)].Power.Split('_')[1]);

            Attack.text
                = "공격력: " + min + "~" + max;
        }
        if (GameData.ItemList[int.Parse(id)].Def != "0")
        {
            int min = int.Parse(GameData.ItemList[int.Parse(id)].Def.Split('_')[0]);
            int max = int.Parse(GameData.ItemList[int.Parse(id)].Def.Split('_')[1]);

            if (Attack.text != "")
            {
                Attack.text
                   += "\n방어력: " + min + "~" + max;
            }
            else
            {
                Attack.text
                   = "방어력: " + min + "~" + max;
            }
        }

        bookItemImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[int.Parse(id)].id);

      
    }


    public void OnClickEleNode(GameObject Select)
    {

        EleInfo.SetActive(true);

        string id = Select.name.Substring(2, 3);
        bookEleNameTag.text = GameData.ElementList[int.Parse(id)].name;
        bookEleScriptTag.text = GameData.ElementList[int.Parse(id)].script;
        bookEleImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[int.Parse(id)].id);

        Get.GetComponent<RectTransform>().localPosition = new Vector2(0, -35);
        Get.text = GameData.ElementList[int.Parse(id)].get ;
      
    }
    public void setBookItemList()
    {


        if (BookItemList.transform.childCount > 0)
        {
            for (int i = 0; i < BookItemList.transform.childCount; i++)
            {
                Destroy(BookItemList.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < GameData.forgeAtItemList.Count; i++)
        {
            Item item = GameData.forgeAtItemList[i];
            GameObject node = Instantiate(BookItem) as GameObject;
            node.transform.SetParent(BookItemList.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = item.id;
            
            for (int j = 0; j < item.Tier; j++)
            {
                node.transform.FindChild("Text").gameObject.GetComponent<Text>().text += "★";
            }
            if (!item.bookAble) {
                node.GetComponent<Button>().interactable = false;
                node.transform.FindChild("Image").gameObject.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                node.transform.FindChild("lock").gameObject.SetActive(true);
            }
            else {
                node.GetComponent<Button>().interactable = true;
                node.transform.FindChild("Image").gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                node.transform.FindChild("lock").gameObject.SetActive(false);
            }

            node.transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + item.id);

          
           node.GetComponent<Button>().onClick.AddListener(() => { OnClickItemNode(node); });
        }
        

    }
    public void setEleItemList()
    {


        if (EleBookItemList.transform.childCount > 0)
        {
            for (int i = 0; i < EleBookItemList.transform.childCount; i++)
            {
                Destroy(EleBookItemList.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < GameData.ElementList.Count; i++)
        {
            Element item = GameData.ElementList[i];
            GameObject node = Instantiate(BookItem) as GameObject;
            node.transform.SetParent(EleBookItemList.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = item.id;
            
                node.transform.FindChild("Text").gameObject.GetComponent<Text>().text = item.name;
            
            if (!item.bookAble)
            {
                node.transform.FindChild("Image").gameObject.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            }
            else
            {
                node.transform.FindChild("Image").gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }

            node.transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Element/" + item.id);


            node.GetComponent<Button>().onClick.AddListener(() => { OnClickEleNode(node); });
        }


    }

   


}
