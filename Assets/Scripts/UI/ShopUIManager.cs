using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class ShopUIManager : MonoBehaviour {
    
    public GameObject Build;
    public GameObject Building;
    public GameObject Display;
    public GameObject Info;
    public GameObject Zoom;
    public GameObject Zoom0;
    public GameObject Sure;
    private GameObject XBuild;
    public GameObject SelectedBuild;
    private GameObject XSpace;
    private GameObject SelectedSpace;
    private GameObject SelectedStand;
    private GameObject Coin;
    private GameObject Star;
    private GameObject GoHunt;
    public bool UIisDown;

    public bool isActiveUI = false;
    public bool isBuilding = false;
    public bool isOut = false;
    RaycastHit hitInfo;

    public GameObject FirstFloor;
    public int BuildingType;
    // Use this for initialization
    void Start()
    {
        BuildingType = -1;
        UIisDown = false;
        Coin = Build.transform.FindChild("Coin").gameObject;
        Star = Build.transform.FindChild("Star").gameObject;
        // GoHunt = transform.FindChild("GoHunt").gameObject;

        FirstFloor.transform.FindChild("Door").GetComponent<SpriteRenderer>().sprite =
           Resources.LoadAll<Sprite>("Shop/Door")[GameData.getShopDoorInfo()];

        FirstFloor.transform.FindChild("counter").GetComponent<SpriteRenderer>().sprite =
           Resources.LoadAll<Sprite>("Shop/Counter")[GameData.getShopCounterInfo()];
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("EventPanel(Clone)")|| GameObject.Find("OrderPanel")|| GameObject.Find("HuntPanel")) isActiveUI = true;
        //if (GoHunt.activeInHierarchy) isActiveUI = true;

        

        if (isBuilding)
        {
            
            isActiveUI = true;
            Building.gameObject.SetActive(true);

            int id = int.Parse(SelectedBuild.name);

            if (BuildingType == 0)
            {

                if (GameData.getMoney() < GameData.getInteriorStandCoin(id)) { OnClickCancel(); Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false; }
                GameObject.Find("DayManager").transform.FindChild("Buildingpanel").gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    Ray hitRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    int layerMask = (-1) - ((1 << LayerMask.NameToLayer("notTouch")) | (1 << LayerMask.NameToLayer("notTouch2")));
                    Physics.Raycast(hitRay, out hitInfo, 1000.0f, layerMask);
                    if (hitInfo.collider)
                    {

                        if (hitInfo.collider.gameObject.tag == "EnableB")
                        {
                            SelectedSpace = hitInfo.collider.gameObject;
                            hitInfo.collider.gameObject.transform.FindChild("ShopsImg").GetComponent<SpriteRenderer>().sprite =
                                SelectedBuild.GetComponent<Image>().sprite;
                            hitInfo.collider.gameObject.transform.FindChild("ShopsImg").GetComponent<SpriteRenderer>().color
                                = new Color32(255, 255, 255, 150);
                            if (XSpace)
                            {

                                XSpace.transform.FindChild("ShopsImg").GetComponent<SpriteRenderer>().sprite = null;
                                XSpace.tag = "EnableB";
                            }
                            Building.transform.FindChild("OK").GetComponent<Button>().interactable = true;
                            hitInfo.collider.gameObject.tag = "DisableB";
                            XSpace = hitInfo.collider.gameObject;
                        }
                    }
                }
            }
            else if(BuildingType == 1)
            {
                if (GameData.getMoney() < GameData.getInteriorWallCoin(id)) { OnClickCancel(); Build.transform.parent.FindChild("Button").GetComponent<Button>().interactable = false; }
                GameObject.Find("DayManager").transform.FindChild("Buildingpanel").gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    Ray hitRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    int layerMask = (-1) - ((1 << LayerMask.NameToLayer("notTouch")) | (1 << LayerMask.NameToLayer("notTouch2")));
                    Physics.Raycast(hitRay, out hitInfo, 1000.0f, layerMask);
                    if (hitInfo.collider)
                    {
                        int SelectWallNum = -1;
                        if (hitInfo.collider.gameObject.transform.parent.GetComponent<ShopGrid>())
                        SelectWallNum = hitInfo.collider.gameObject.transform.parent.GetComponent<ShopGrid>().ShopWallNum;
                        if (hitInfo.collider.gameObject.tag == "EnableW" && SelectWallNum != id)
                        {
                            SelectedSpace = hitInfo.collider.gameObject;
                            hitInfo.collider.gameObject.GetComponent<SpriteRenderer>().sprite =
                                Resources.LoadAll<Sprite>("Shop/Wall")[id];
                            if (XSpace)
                            {
                                int FloorIndex = XSpace.transform.parent.GetComponent<ShopGrid>().FloorWallIndex;
                                XSpace.GetComponent<SpriteRenderer>().sprite
                                                = Resources.LoadAll<Sprite>("Shop/Wall")[GameData.getShopWallInfo(FloorIndex)];
                                XSpace.tag = "EnableW";
                            }
                            Building.transform.FindChild("OK").GetComponent<Button>().interactable = true;
                            hitInfo.collider.gameObject.tag = "DisableW";
                            XSpace = hitInfo.collider.gameObject;
                        }
                    }
                }
                
            }
            else if (BuildingType == 2)
            {
                SelectedSpace = FirstFloor.transform.FindChild("Door").gameObject;
                FirstFloor.transform.FindChild("Door").GetComponent<SpriteRenderer>().sprite =
                   Resources.LoadAll<Sprite>("Shop/Door")[id];
                Building.transform.FindChild("OK").GetComponent<Button>().interactable = true;
            }
            else if (BuildingType == 3)
            {

                SelectedSpace = FirstFloor.transform.FindChild("counter").gameObject;
                FirstFloor.transform.FindChild("counter").GetComponent<SpriteRenderer>().sprite =
                   Resources.LoadAll<Sprite>("Shop/Counter")[id];
                Building.transform.FindChild("OK").GetComponent<Button>().interactable = true;
            }

        }
        else
        {
            GameObject.Find("DayManager").transform.FindChild("Buildingpanel").gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0) && !isActiveUI)
            {
                Ray hitRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layerMask = (-1) - ((1 << LayerMask.NameToLayer("notTouch")) | (1 << LayerMask.NameToLayer("notTouch2")));
                Physics.Raycast(hitRay, out hitInfo, 1000.0f, layerMask);

                Vector3 mPos = Input.mousePosition;
                if (Input.touchCount >0) mPos = Input.GetTouch(0).position;
                bool isClick = true;
                if (UIisDown && mPos.y <= Screen.height / 8) isClick = false;
                if (mPos.y <= (Screen.height / 2) + 90 && !UIisDown) isClick = false;
                


                if (hitInfo.collider && isClick)
                {
                    if (hitInfo.collider.tag == "Stand")
                    {
                        SelectedStand = hitInfo.collider.gameObject;
                        if (SelectedStand.GetComponent<ShopInfo>().SaleItemID == -1)
                        {
                            Zoom.SetActive(false);
                            Zoom0.SetActive(true);
                            Zoom0.transform.FindChild("Panel").FindChild("Image").GetComponent<Image>().sprite =
                               SelectedStand.gameObject.transform.FindChild("ShopsImg").GetComponent<SpriteRenderer>().sprite;
                            Zoom0.transform.FindChild("information").FindChild("Name").GetComponent<Text>().text =
                               GameData.getInteriorName(SelectedStand.GetComponent<ShopInfo>().ShopInfoNum)+" 매대";
                            isActiveUI = true;

                        }
                        else
                        {
                            Zoom0.SetActive(false);
                            Zoom.SetActive(true);
                            Zoom.transform.FindChild("Panel").FindChild("Image").GetComponent<Image>().sprite =
                                Resources.Load<Sprite>("Item/" + GameData.ItemList[SelectedStand.GetComponent<ShopInfo>().SaleItemID].id);
                            Zoom.transform.FindChild("information").FindChild("Name").GetComponent<Text>().text =
                               "" + GameData.ItemList[SelectedStand.GetComponent<ShopInfo>().SaleItemID].name;
                            Zoom.transform.FindChild("information").FindChild("Image").FindChild("Coin").GetComponent<Text>().text =
                               "" + GameData.ItemList[SelectedStand.GetComponent<ShopInfo>().SaleItemID].Cost;
                            isActiveUI = true;

                        }
                        
                    }
                    if (hitInfo.collider.tag == "Player")
                    {
                        Info.SetActive(true);

                    }
                    if (hitInfo.collider.tag == "PlusBt" && GameData.getFloor() <5)
                    {

                        transform.FindChild("UpFloor").GetComponent<UpFloorPanel>().OnClick();

                    }
                   

                }

            }
        }
    }


    public void OnClickMainMenu(Button selectedB)
    {
        if (!isBuilding)
        {
            string selectedN = selectedB.name;
            GameObject BottomM;
            BottomM = selectedB.transform.parent.parent.FindChild("BottomView").gameObject;

            BottomM.transform.FindChild("Interior").gameObject.SetActive(false);
            int NumC = BottomM.transform.childCount;
            for (int i = 0; i < NumC; i++)
            {
                if (BottomM.transform.GetChild(i).name == "Forge" || BottomM.transform.GetChild(i).name == "Quest")
                {
                    BottomM.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
                    BottomM.transform.GetChild(i).GetComponent<CanvasGroup>().interactable = false;
                    BottomM.transform.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                else BottomM.transform.GetChild(i).gameObject.SetActive(false);

            }

            if (BottomM.transform.FindChild(selectedN).name == "Forge" || BottomM.transform.FindChild(selectedN).name == "Quest")
            {
                BottomM.transform.FindChild(selectedN).GetComponent<CanvasGroup>().alpha = 1;
                BottomM.transform.FindChild(selectedN).GetComponent<CanvasGroup>().interactable = true;
                BottomM.transform.FindChild(selectedN).GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else BottomM.transform.FindChild(selectedN).gameObject.SetActive(true);
        
        }
        
    }
    public void OnClickShops(Button SelectedB, int type)
    {
        if (XBuild != SelectedB)
        {
            int id = int.Parse(SelectedB.name);
            SelectedBuild = SelectedB.gameObject;
            SelectedBuild.GetComponent<Button>().interactable = false;
            if (XBuild) XBuild.GetComponent<Button>().interactable = true;
            XBuild = SelectedB.gameObject;

            BuildingType = type;

            Star.SetActive(true);
            Star.transform.FindChild("Text").GetComponent<Text>().text = GameData.getInteriorLevel(id) + "↑";
            if (type == 0)
            {
                Build.transform.FindChild("build_content").transform.FindChild("name").GetComponent<Text>().text
                = GameData.getInteriorName(id) + " 매대";

                Build.transform.FindChild("build_content").transform.FindChild("content").GetComponent<Text>().text
                = GameData.getInteriorContent(id) + " 매대";
                Coin.SetActive(true);
                Coin.transform.FindChild("Text").GetComponent<Text>().text = GameData.getInteriorStandCoin(id) + "";
            }
            else if (type == 1)
            {
                Build.transform.FindChild("build_content").transform.FindChild("name").GetComponent<Text>().text
                = GameData.getInteriorName(id) + " 벽지";

                Build.transform.FindChild("build_content").transform.FindChild("content").GetComponent<Text>().text
                = GameData.getInteriorContent(id) + " 벽지";
                Coin.SetActive(true);
                Coin.transform.FindChild("Text").GetComponent<Text>().text = GameData.getInteriorWallCoin(id) + "";

            }
            else if (type == 2)
            {
                Build.transform.FindChild("build_content").transform.FindChild("name").GetComponent<Text>().text
                = GameData.getInteriorName(id) + " 문";

                Build.transform.FindChild("build_content").transform.FindChild("content").GetComponent<Text>().text
                = GameData.getInteriorContent(id) + " 문";
                Coin.SetActive(true);
                Coin.transform.FindChild("Text").GetComponent<Text>().text = GameData.getInteriorDoorCoin(id) + "";

            }
            else if (type == 3)
            {
                Build.transform.FindChild("build_content").transform.FindChild("name").GetComponent<Text>().text
                = GameData.getInteriorName(id) + " 카운터";

                Build.transform.FindChild("build_content").transform.FindChild("content").GetComponent<Text>().text
                = GameData.getInteriorContent(id) + " 카운터";
                Coin.SetActive(true);
                Coin.transform.FindChild("Text").GetComponent<Text>().text = GameData.getInteriorCounterCoin(id) + "";

            }
        }
    }

    public void OnClickBuild()
    {

        if (SelectedBuild)
        {
            int id = int.Parse(SelectedBuild.name);
            if (GameData.getMoney() >= GameData.getInteriorStandCoin(id)){
               
                isBuilding = true;
                if (XBuild) XBuild.GetComponent<Button>().interactable = true; }
        }

    }




    public void OnClickOutB()
    {
        isOut = true;
        GameData.isOpen = false;
        SceneManager.LoadScene(4);
    }

    public void OnClickBack()
    {
        //행거 수정할것임
        SelectedBuild = null;
        if (XBuild) XBuild.GetComponent<Button>().interactable = true;
        XBuild = null;
        Build.transform.FindChild("build_content").transform.FindChild("name").GetComponent<Text>().text
               = "";
        Build.transform.FindChild("build_content").transform.FindChild("content").GetComponent<Text>().text
        = "";
        
        Coin.SetActive(false);
        Coin.transform.FindChild("Text").GetComponent<Text>().text = "";

        Star.SetActive(false);
        Star.transform.FindChild("Text").GetComponent<Text>().text = "";

        GameObject.Find("ShopManager").GetComponent<ShopManager>().ItemID = -1;
        GameObject.Find("ShopManager").GetComponent<ShopManager>().ItemButton = null;

        Display.SetActive(false);
        Info.SetActive(false);
        Zoom.SetActive(false);
        Zoom0.SetActive(false);
        if (XBuild) XBuild.GetComponent<Button>().interactable = true;
        isActiveUI = false;
    }

    public void OnClickDecide()
    {
        if (SelectedSpace) {

            int id = int.Parse(SelectedBuild.name);
            GameObject.Find("DayManager").transform.FindChild("CoinAni").GetComponent<Animator>().Play("useCoin");
            if (BuildingType == 0)
            {
                SelectedSpace.GetComponent<ShopInfo>().ShopInfoNum = id; //매대일때
                GameData.useMoney(GameData.getInteriorStandCoin(id));
                SelectedSpace.transform.FindChild("ShopsImg").GetComponent<SpriteRenderer>().color
                               = new Color32(255, 255, 255, 255);
            }
            if (BuildingType == 1)
            {
                int FloorIndex = SelectedSpace.transform.parent.GetComponent<ShopGrid>().FloorWallIndex;
                GameData.setShopWallInfo(FloorIndex,id);
                GameData.useMoney(GameData.getInteriorWallCoin(id));
                SelectedSpace.tag = "EnableW";
            }
            if (BuildingType == 2)
            {
                GameData.useMoney(GameData.getInteriorDoorCoin(id));
                GameData.setShopDoorInfo(id);

            }
            if (BuildingType == 3)
            {
                GameData.useMoney(GameData.getInteriorCounterCoin(id));
                GameData.setShopCounterInfo(id);
            }

            if (BuildingType > 1)
            {
                isBuilding = false; XSpace = null; SelectedSpace = null;
                Building.gameObject.SetActive(false);
                Building.transform.FindChild("OK").GetComponent<Button>().interactable = false;
                isActiveUI = false;
            }
            else
            {
                XSpace = null; SelectedSpace = null;
                Building.transform.FindChild("OK").GetComponent<Button>().interactable = false;
            }
            
        }

        }
    



    public void OnClickCancel()
    {


        if (SelectedSpace)
        {
            if (BuildingType == 0)
            {
                SelectedSpace.transform.FindChild("ShopsImg").GetComponent<SpriteRenderer>().sprite
                                = null;
                SelectedSpace.tag = "EnableB";

            }
            if (BuildingType == 1)
            {
                int FloorIndex = SelectedSpace.transform.parent.GetComponent<ShopGrid>().FloorWallIndex;
                SelectedSpace.GetComponent<SpriteRenderer>().sprite
                                = Resources.LoadAll<Sprite>("Shop/Wall")[GameData.getShopWallInfo(FloorIndex)];
                SelectedSpace.tag = "EnableW";

            }
            if (BuildingType == 2)
            {
                
                Debug.Log(GameData.ShopDoorInfo);
                FirstFloor.transform.FindChild("Door").GetComponent<SpriteRenderer>().sprite =
                   Resources.LoadAll<Sprite>("Shop/Door")[GameData.ShopDoorInfo];

            }
            if (BuildingType == 3)
            {
                FirstFloor.transform.FindChild("counter").GetComponent<SpriteRenderer>().sprite =
                   Resources.LoadAll<Sprite>("Shop/Counter")[GameData.ShopCounterInfo];

            }
            SelectedSpace = null;
        }
        Building.transform.FindChild("OK").GetComponent<Button>().interactable = false;
        XSpace = null;
        isBuilding = false;
        Building.gameObject.SetActive(false);
        isActiveUI = false;
    }

    public void OnSelectShopItem()
    {
        if (GameObject.Find("ShopManager").GetComponent<ShopManager>().InvenZoomImage)
        {

            //판매 아이템 변경
            if (SelectedStand.GetComponent<ShopInfo>().OnSale)
            {
                GameData.soltItem();
                GameData.setItem(SelectedStand.GetComponent<ShopInfo>().SaleItemID);
            }

            Zoom0.SetActive(false);
            GameData.onSale(GameObject.Find("ShopManager").GetComponent<ShopManager>().ItemID, SelectedStand.GetComponent<ShopInfo>().ShopIndex);
            SelectedStand.GetComponent<ShopInfo>().OnSale = true;
            SelectedStand.GetComponent<ShopInfo>().SaleItemID = GameObject.Find("ShopManager").GetComponent<ShopManager>().ItemID;
            Display.SetActive(false);
            isActiveUI = false;

            GameObject.Find("ShopManager").GetComponent<ShopManager>().InvenZoomImage = null;
            GameObject.Find("GameManager").GetComponent<ForgeManager>().UnselectedItem();
        }

}
    public void OnClickItemChange()
    {
        if (SelectedStand.GetComponent<ShopInfo>().OnSale)
        {
            GameData.soltItem();
            Zoom.SetActive(false);
            Display.SetActive(true);
        }
        else
        {
            GameData.soltItem();
            Display.SetActive(true);
        }
    }

    public void OnClickItemDelete()
    {
        if (SelectedStand.GetComponent<ShopInfo>().OnSale)
        {
            GameData.soltItem();
            Zoom.SetActive(false);
            //GameData.notSale(SelectedStand.GetComponent<ShopInfo>().SaleItemID);
            GameData.setItem(SelectedStand.GetComponent<ShopInfo>().SaleItemID);
            isActiveUI = false;
            SelectedStand.GetComponent<ShopInfo>().OnSale = false;
            SelectedStand.GetComponent<ShopInfo>().SaleItemID = -1;
            SelectedStand.GetComponent<ShopInfo>(). SaleItemImg.GetComponent<SpriteRenderer>().sprite = null;
            GameData.ShopSaleItemInfo[SelectedStand.GetComponent<ShopInfo>().ShopIndex] = -1;
        }
    }

    public void OnClickBuildDelete()
    {
        int id = SelectedStand.GetComponent<ShopInfo>().ShopInfoNum;
        if (Sure.activeInHierarchy) Sure.SetActive(false);
        else { Sure.SetActive(true);
            Sure.transform.FindChild("Coin").FindChild("Text").GetComponent<Text>().text = ""+(int)(GameData.getInteriorStandCoin(id)/2);
            isActiveUI = false; }
    }

    public void DecideBuildDelete()
    {
        int id = SelectedStand.GetComponent<ShopInfo>().ShopInfoNum;
        SelectedStand.GetComponent<ShopInfo>().ShopInfoNum = -1;
        SelectedStand.tag = "EnableB";
        if (GameObject.Find("DayManager")) GameObject.Find("DayManager").transform.FindChild("CoinAni").GetComponent<Animator>().Play("addCoin");
        GameData.addMoney((int)(GameData.getInteriorStandCoin(id)/2));
        Sure.SetActive(false);
        Zoom0.SetActive(false);
        SelectedStand = null;
        isActiveUI = false;
    }

    public void OnClickSlider(Button Slider)
    {
        GameObject.Find("Audio").transform.FindChild("slide").GetComponent<AudioSource>().Play();
        if (!UIisDown)
        {
            GameObject.Find("ShopAll").GetComponent<Animator>().Play("Popdown");
            UIisDown = true;
        }
        else {
            GameObject.Find("ShopAll").GetComponent<Animator>().Play("Popup");
            UIisDown = false;
        }

    }
    public void OnClickMainMenu()
    {
        if (!isBuilding)
        {
            if (UIisDown)
            {
                GameObject.Find("Audio").transform.FindChild("slide").GetComponent<AudioSource>().Play();
                GameObject.Find("ShopAll").GetComponent<Animator>().Play("Popup");
                UIisDown = false;
            }
        }
    }

    public void OnClickAutoSale()
    {
        for (int i = 0; i < GameData.ShopInfo.Length; i++)
        {

            if (GameData.ShopInfo[i] != -1 && GameData.ShopSaleItemInfo[i] == -1)
            {
                GameData.soltItem();
                if (GameData.ItemInventory[0].Count != 0)
                {
                    GameData.onSale(0, i);
                }

                GameObject.Find("GameManager").GetComponent<ForgeManager>().UnselectedItem();
                //SelectedStand.GetComponent<ShopInfo>().OnSale = true;
                //SelectedStand.GetComponent<ShopInfo>().SaleItemID = GameObject.Find("ShopManager").GetComponent<ShopManager>().ItemID;
            }
            
        }
        
      }

    public void ActiveUI()
    {
        isActiveUI = true;
    }
    public void UnActiveUI()
    {
        isActiveUI = false;
    }

}
