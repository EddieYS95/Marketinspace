using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class searchPanel : MonoBehaviour {
    
    public GameObject search_panel;
    public ForgeManager forgeManager;
    public GameObject Content;
    Sprite Able;
    Sprite UnAble;

    GameObject xButton;
	// Use this for initialization
	void Start () {
        xButton = search_panel.transform.FindChild("weapon").gameObject;
        Able = Resources.LoadAll<Sprite>("UI/icn_btn")[6];
        UnAble = Resources.LoadAll<Sprite>("UI/icn_btn")[7];
        List<ItemType> types = new List<ItemType>();
        types.Add(ItemType.Weapon);
        GameData.setForgeAtItemList(types, 3);
        forgeManager.setMakeItemList();
    }

    // Update is called once per frame
    void Update()
    {
    }
        

    public void OnClickTagBt(GameObject bt)
    {
        Content.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            bool isMake = false;

      

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
                else if (bt.name.Equals("gun"))
                {
                    types.Add(ItemType.Gun);
                }
                else if (bt.name.Equals("etc"))
                {
                    types.Add(ItemType.Drink);
                }
                else if (bt.name.Equals("all"))
                {
                    isMake = true;
                }
                GameData.setForgeAtItemList(types, 3);
                forgeManager.setMakeItemList();

            }
            else
            {
                List<ItemType> types = new List<ItemType>();
                GameData.setForgeAtItemList(types, 3);
                forgeManager.setMakeItemList();

            }
        
    }
}
