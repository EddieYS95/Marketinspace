using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopInfo : MonoBehaviour {

    public int ShopIndex;
    public int ShopInfoNum;
    public bool OnSale;
    public bool isLook;
    private GameObject ShopImage;
    private ShopUIManager Shop_;
    public GameObject SaleItemImg;
    public int SaleItemID;


	// Use this for initialization
	void Start () {

        ShopImage = transform.FindChild("ShopsImg").gameObject;
        Shop_ =GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>();
        ShopInfoNum = GameData.ShopInfo[ShopIndex];
        OnSale = false;
        isLook = false;
        SaleItemImg = ShopImage.transform.FindChild("ItemImg").gameObject;
        SaleItemID = -1;

    }

    // Update is called once per frame
    void Update () {

        if (Shop_.isBuilding && Shop_.BuildingType ==0)
        { 
            if (tag == "EnableB") GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 225);
            else GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
        }
        else GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);


        if (OnSale) ShopImage.tag = "OnSale";
        else ShopImage.tag = "Untagged";

        SaleItemID = GameData.ShopSaleItemInfo[ShopIndex];

        GameData.SetShopBuild(ShopIndex, ShopInfoNum);

        if (SaleItemID != -1)
        {
            SaleItemImg.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Img/" + GameData.ItemList[SaleItemID].id);
            OnSale = true;
        }

        if (!Shop_.isBuilding)
        {
            if (ShopInfoNum == -1)
            {

                tag = "EnableB";
                ShopImage.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("char/nill");
                ShopImage.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
            else
            { 
                tag = "Stand";
                ShopImage.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Shop/Stand")[ShopInfoNum];
                ShopImage.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
        }

       

    }

    public void SoldOut()
    {
        GameData.ShopSaleItemInfo[ShopIndex] = -1;
        GameData.soltItem();
        OnSale = false;
        SaleItemID = -1;
        SaleItemImg.GetComponent<SpriteRenderer>().sprite = null;
    }
}
