using UnityEngine;
using System.Collections;

public class ShopGrid : MonoBehaviour {

    private GameObject Floor;
    public int FloorIndex;
    public int FloorWallIndex;
    public GameObject Shops;
    int ShopNum;

    public int ShopWallNum;
    float BackPos_x;
    Vector3 CurrentPos;

    ShopUIManager Shop_;
    GameObject Buildmark;

    // Use this for initialization
    void Start () {
        BackPos_x = 1.5f;
        CurrentPos = new Vector3(-1.25f, transform.position.y+ 1.6f, -2);
        ShopNum = -1;
        if (FloorIndex > 1)
        {
            ShopNum = (FloorIndex - 1) * 4 - 1;
            transform.FindChild("Stair").FindChild("Text").GetComponent<TextMesh>().text = FloorIndex + "F";
            FloorWallIndex = FloorIndex - 1;
        }

        ShopWallNum = GameData.getShopWallInfo(FloorWallIndex);
        transform.FindChild("space").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Shop/Wall")[ShopWallNum];
        Shop_ = GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>();
        Buildmark = transform.FindChild("space").FindChild("mark").gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        if (Shop_.isBuilding && Shop_.BuildingType == 1)
        {
            if (transform.FindChild("space").tag == "EnableW") Buildmark.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 225);
            else Buildmark.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
        }
        else Buildmark.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);

        ShopWallNum = GameData.getShopWallInfo(FloorWallIndex);

        if (CurrentPos.x <= BackPos_x)
            {
                ShopNum++;
                GameObject shops = Instantiate(Shops, CurrentPos, transform.rotation) as GameObject;
                shops.name = "Shop"+ShopNum;
                shops.GetComponent<ShopInfo>().ShopIndex = ShopNum;
                shops.transform.SetParent(transform);
                CurrentPos += new Vector3(0.82f, 0, 0);

                if (GameData.ShopInfo[ShopNum] == 0)
                {
                  shops.tag = "DisableB";
                }
         
            }
      

    }
}
