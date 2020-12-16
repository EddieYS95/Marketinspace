using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class ShopManager : MonoBehaviour {
    
    public GameObject ItemInventoryList;
    public GameObject ItemInventory;
    public Sprite InvenZoomImage;
    public int ItemID;
    public Button ItemButton;
    public GameObject Guests;
    private GameObject Guest;
    public GameObject Coin;
    public GameObject OrderPanel;
    private ShopUIManager Shop_;
    public GameObject GoHuntBt;
    public bool CloseShop;
    public GameObject HuntPanel;
    public GameObject HuntWating;
    private bool willHunt;
    [HideInInspector]
    public bool haveQuestGuest;
    public GameObject Quit;
    public GameObject DestroyItem;

    public Image VillImg;
    bool pop;
    bool moveHeart;
    GameObject heart_m;
    GameObject heart_dir;

    public int EleID;

    bool isHunt_E;
    void Awake()
    {

        //나중에지우십쇼
        Guest = Resources.Load<GameObject>("Guest/Guests1");
        haveQuestGuest = false;
        if (GameData.OnGame == false) GameData.LoadGame();
        StartCoroutine(CreateGuest());
        StartCoroutine(saveGame());
        willHunt = false;
        setInventory();
        ItemID = -1;
        EleID = -1;
        Shop_ = GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>();
        CloseShop = false;

        heart_m= GameObject.Find("DayManager").transform.FindChild("Heart_h").FindChild("heart_m").gameObject;
        heart_dir = GameObject.Find("DayManager").transform.FindChild("Heart_h").FindChild("heart_dir").gameObject;
        GameData.setSpaceNum(GameData.getSpaceOpen());
        isHunt_E = false;

    }
    // Use this for initialization
    void Start () {

        StartCoroutine(UpdateServerData());
    }
	// Update is called once per frame
	void Update () {
        
        ChkTime();

        if (GameData.GetPassedDay(DateTime.Now) != 0)
        {
            GameData.AddDay(GameData.GetPassedDay(DateTime.Now));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
          Quit.SetActive(!Quit.activeSelf);
        }



        if (HuntPanel.activeInHierarchy)
        {
            
            if (GameData.getEmerald() < 2) HuntPanel.transform.FindChild("Hunt_E").FindChild("Panel").FindChild("Button").GetComponent<Button>().interactable = false;
            else HuntPanel.transform.FindChild("Hunt_E").FindChild("Panel").FindChild("Button").GetComponent<Button>().interactable = true;

            if (GameData.getEmerald() < 3) HuntPanel.transform.FindChild("Hunt_R").FindChild("Panel").FindChild("Button").GetComponent<Button>().interactable = false;
            else HuntPanel.transform.FindChild("Hunt_R").FindChild("Panel").FindChild("Button").GetComponent<Button>().interactable = true;
        }
        VillImg.sprite = Resources.LoadAll<Sprite>("UI/Vill")[GameData.getSpaceOpen()];

    }
    
    void ChkTime()
    {
       
        if (willHunt == true)
        {
             
            HuntPanel.SetActive(false);
            HuntWating.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("shop").gameObject.SetActive(false);
            if (GameObject.Find("ShopManager")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().UnActiveUI();
            
            if (!moveHeart && !isHunt_E)
            {
                if (!pop)
                {
                    GameObject.Find("Audio").transform.FindChild("pop").GetComponent<AudioSource>().Play();
                    GameData.minusHeart_p(1);
                    pop = true;
                }


                heart_m.SetActive(true);
                if(heart_m.transform.localPosition.y < heart_dir.transform.localPosition.y)
                {
                    heart_m.SetActive(false);
                    moveHeart = true;
                }
                else
                {
                    heart_m.transform.position += new Vector3(0.5f, -2.5f, 0)*Time.deltaTime;
                }
            }
            else
            {
                
                if (GameObject.FindGameObjectsWithTag("Guest").Length == 0)
                {

                    Shop_.ActiveUI();
                    if (GameObject.Find("Main Camera"))
                    {
                        StopAllCoroutines();
                        
                        GameObject load = GameObject.Find("Loading").transform.FindChild("LoadingScene").gameObject;
                        GameObject.Find("Main Camera").SetActive(false);
                        load.SetActive(true);
                        load.transform.FindChild("Loading").GetComponent<loading>().targetName = "equip";
                        

                    }
                }
            }
        }
       
    }
  

    public void OnClickHunt()
    {
       
            willHunt = true;
            setCloseShop(true);
        
    }
    public void OnClickNomarlHunt()
    {
        GameData.isRandom = false;
        if (GameData.getHeart_p() <= 0)
        {
            HuntPanel.transform.FindChild("Hunt_E").gameObject.SetActive(true);

        }
        else
        {
            HuntPanel.transform.FindChild("Hunt_H").gameObject.SetActive(true);
        }
    }
    public void OnClickRandomHunt()
    {
        GameData.isRandom = true;
        if (GameData.getLevel() < 10)
        {
            HuntPanel.transform.FindChild("Error_lv").gameObject.SetActive(true);

        }
        else
        {
            HuntPanel.transform.FindChild("Hunt_R").gameObject.SetActive(true);
        }
    }

    public void OnClickHunt_E(int Eme)
    {
        GameData.useEmerald(Eme);
        isHunt_E = true;
        willHunt = true;
        setCloseShop(true);

    }

    public void setInventory()
    {
        for (int i = 0; i < GameData.getItemInventorySize(); i++)
        {
            GameObject node = Instantiate(ItemInventory) as GameObject;
            node.transform.SetParent(ItemInventoryList.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.name = i + "";
        }
    }

    public void setCloseShop(bool closeShop)
    {
        CloseShop = closeShop;
        if (CloseShop == true)
        {
            GameObject[] guests = GameObject.FindGameObjectsWithTag("Guest");
            foreach (GameObject guest in guests)
            {
                guest.SendMessage("setClose");
            }
        }
    }
    public void setCloseShop()
    {
        CloseShop = !CloseShop;
        if (CloseShop == true)
        {
            GameObject[] guests = GameObject.FindGameObjectsWithTag("Guest");
            foreach(GameObject guest in guests){
                guest.SendMessage("setClose");
            }
        }
    }

   
    IEnumerator CreateGuest()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (CloseShop == false)
            {
                int ranNum = UnityEngine.Random.Range(0, 1000);
                if (ranNum < GameData.getStar()+120 && !Shop_.isActiveUI)
                {
                    ranNum = UnityEngine.Random.Range(1+(4*GameData.getSpaceNum()), 5+ (4 * GameData.getSpaceNum()));
                    if (!Shop_.isActiveUI)
                    {
                        Guest = Resources.Load<GameObject>("Guest/Guests" + ranNum);
                        GameObject guest;
                        guest = Instantiate(Guest, Guests.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                        guest.transform.SetParent(Guests.transform);
                        if (haveQuestGuest == false)
                        {
                            int rand = UnityEngine.Random.Range(0, 100);
                            if (rand > 80)
                            {
                                GameObject.Find("Audio").transform.FindChild("shop_bell").GetComponent<AudioSource>().Play();
                                guest.GetComponent<GuestScript>().setGuestType(GuestScript.GuestType.HaveQuest);
                                haveQuestGuest = true;
                            }
                            else
                            {
                                guest.GetComponent<GuestScript>().setGuestType(GuestScript.GuestType.Normal);
                            }
                        }
                        else
                        {
                            guest.GetComponent<GuestScript>().setGuestType(GuestScript.GuestType.Normal);
                        }
                    }
                }
            }
        }
    }

    IEnumerator saveGame()
    {
        yield return new WaitForSeconds(4f);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameData.SaveGame();
        }
    }

   

    
    public void OnEquipment()
    {
        if (ItemID != -1)
        {
            GameData.setEquipment(GameData.ItemInventory[ItemID].id);
            ItemID = -1;
        }
    }
    public void OnDestroyItem()
    {

        DestroyItem.SetActive(true);
        DestroyItem.transform.FindChild("Slider").GetComponent<Slider>().minValue = 1;
        if (EleID == -1)
        {
           if(ItemID!=-1)
            DestroyItem.transform.FindChild("Slider").GetComponent<Slider>().maxValue = GameData.ItemInventory[ItemID].Count;
        }
        else
        {
            DestroyItem.transform.FindChild("Slider").GetComponent<Slider>().maxValue = GameData.ElementInventory[EleID].Count;
        }
    }

    public void OnChangeDSlider(Text sl)
    {
        sl.text = (int)(DestroyItem.transform.FindChild("Slider").GetComponent<Slider>().value) + "";
    }

    public void OnClickDestroyItem(Slider sl)
    {
        if (EleID == -1)
        {
            if (ItemID != -1)
                GameData.removeItem(GameData.ItemInventory[ItemID].id, (int)sl.value);
            
        }
        else
        {
            GameData.useElement(GameData.ElementInventory[EleID].id, (int)sl.value);
        }
        DestroyItem.SetActive(false);
        GameObject.Find("GameManager").GetComponent<ForgeManager>().UnselectedItem();

        DestroyItem.transform.FindChild("Slider").GetComponent<Slider>().value = 1;
    }


    IEnumerator UpdateServerData()
    {
        WWW www = new WWW("http://abricks.kr/mis_server.php?Mode=insultData&nickname=" + GameData.getName() + "&level="+GameData.getLevel()+"&star=" + GameData.getStar() + "&bricks=" + GameData.getMoney() + "&emerald="+ GameData.getEmerald());
        yield return www;
    }
}
