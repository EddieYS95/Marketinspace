using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GuestScript : MonoBehaviour
{

    public enum GuestType
    {
        Normal, HaveQuest
    }

    ShopUIManager Shop_;
    bool isCollide;
    bool isBuying;
    int BuyIndex;
    int BuyCost;
    float speed;
    TextMesh g_text;
    Vector3 Dir;
    bool CloseShop;
    RaycastHit hitInfo;
    GuestType guestType;

    GameObject questBt;
    GameObject StarBt;
    GameObject Star;
    int needItemId;
    int needItemcount;
    int needItempercent;
    // Use this for initialization

   void Awake()
    {

        questBt = transform.Find("GuestCanvas").Find("questBt").gameObject;
        StarBt = transform.Find("GuestCanvas").Find("starBt").gameObject;
        Star = transform.Find("GuestCanvas").Find("Star").gameObject;
    }

    void Start()
    {
        
        int ranNum = UnityEngine.Random.Range(0, 10);
        if (ranNum > 6) { StarBt.SetActive(true); }
        isCollide = false;
        isBuying = false;
        BuyIndex = -1;
       // int ranNum = UnityEngine.Random.Range(11, 13);
        speed = 0.8f;
        
        StartCoroutine(Enter());
        g_text = transform.FindChild("Text").GetComponent<TextMesh>();
        g_text.text = "";
        StartCoroutine(CreateMessage(1));
    }

    public void setGuestType(GuestType type)
    {
        guestType = type;

        needItemId = UnityEngine.Random.Range(0, 13);
        if (needItemId == 6) needItemId = 0;
        needItemcount = UnityEngine.Random.Range(1, 10);
        needItempercent = UnityEngine.Random.Range(10, 20);
        

        if (guestType == GuestType.HaveQuest)
        {
            questBt.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Star.activeInHierarchy) Star.transform.position += new Vector3(0, Time.deltaTime, 0);
        if (Star.transform.localPosition.y > 15f) Star.GetComponent<Image>().color -= new Color32(0, 0, 0, (byte)(300 * Time.deltaTime));
        if (Star.transform.localPosition.y > 20f) Star.SetActive(false);

        if (GameObject.Find("ShopManager")) Shop_ = GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>();

        if(isCollide || Shop_.isActiveUI) GetComponent<Animator>().Play("Idle");

        if (guestType == GuestType.Normal)
        {

            if (!isCollide && GameObject.Find("ShopManager") && !Shop_.isActiveUI && CloseShop == false) { transform.position += Dir * Time.deltaTime;
                GetComponent<Animator>().Play("walk");
            }

            else if (!isCollide && GameObject.Find("ShopManager") && !Shop_.isActiveUI && CloseShop == true)
            {
                GetComponent<Animator>().Play("walk");
                Dir = new Vector3(-speed, 0, 0);
                transform.position += Dir * Time.deltaTime *2;
                transform.FindChild("Text").transform.localScale = new Vector3(-0.06f, 0.06f, 0);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            
        }
        else if (guestType == GuestType.HaveQuest)
        {
            StarBt.SetActive(false);
            if (!isCollide && GameObject.Find("ShopManager") && !Shop_.isActiveUI && CloseShop == false) { transform.position += Dir * Time.deltaTime; GetComponent<Animator>().Play("walk"); }
            else if (CloseShop) { transform.position += Dir * Time.deltaTime; GetComponent<Animator>().Play("walk"); }
            else GetComponent<Animator>().Play("Idle");

        }

    }

    public void setClose()
    {
        CloseShop = true;
        Out();
    }
    public void OnClickStar()
    {
        GameData.addStar(1);
        GameObject.Find("Audio").transform.FindChild("pop").GetComponent<AudioSource>().Play();
        StarBt.SetActive(false);
        Star.SetActive(true);
    }
    void Out()
    {

        if (guestType == GuestType.HaveQuest) OrderCancel();
        if (isBuying)
        {
            if (BuyCost != 0)
            {
                GameObject.Find("Audio").transform.FindChild("Coin_sound").GetComponent<AudioSource>().Play();
                if (GameObject.Find("DayManager")) GameObject.Find("DayManager").transform.FindChild("CoinAni").GetComponent<Animator>().Play("addCoin");
            }
            GameData.addMoney(BuyCost);
            BuyCost = 0;
            transform.FindChild("coin").FindChild("Text").GetComponent<TextMesh>().text = "";
            isCollide = false;
            Dir = new Vector3(-speed, 0, 0);
            transform.position += Dir * Time.deltaTime;
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (guestType == GuestType.Normal)
        {
            if (col.tag == "OnSale" && !isBuying)
            {
                isCollide = true;
                ShopInfo Shopinfo = col.transform.GetComponentInParent<ShopInfo>();
                if (Shopinfo.OnSale && !Shopinfo.isLook) { Shopinfo.isLook = true; StartCoroutine(Buying(col)); }
                else isCollide = false;
            }
            if (col.tag == "Counter")
            {
                isCollide = true;
                if (isBuying) transform.FindChild("coin").gameObject.SetActive(true);
                else
                {
                    StartCoroutine(CreateMessage(5));
                    Dir = new Vector3(-speed, 0, 0);
                    transform.FindChild("Text").transform.localScale = new Vector3(-0.06f, 0.06f, 0);
                    isCollide = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                    isBuying = true;
                }
            }
            if (col.tag == "Door")
            {
                DestroyObject(this.gameObject);
            }
        }
        else if (guestType == GuestType.HaveQuest)
        {

            if (col.tag == "Counter")
            {
                isCollide = true;

            }
            if (col.tag == "Door")
            {
                DestroyObject(this.gameObject);
            }
        }

    }

    public void OrderCancel()
    {

        if (questBt.activeInHierarchy)
        {
            int ranNum = UnityEngine.Random.Range(0, 3);

            if (ranNum == 0) g_text.text = "뭐야!!";
            if (ranNum == 1) g_text.text = "다신 안와!";
            if (ranNum == 2) g_text.text = "아쉽네요..";
            Star.GetComponent<Image>().sprite = Resources.Load<Sprite>("char/Star3");
            Star.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("angry").GetComponent<AudioSource>().Play();
            if (GameData.getStar() > 0) GameData.minusStar(1);
            
            Dir = new Vector3(-speed, 0, 0);
            GameObject.Find("ShopManager").GetComponent<ShopManager>().haveQuestGuest = false;
            isCollide = false;
            questBt.SetActive(false);
            transform.FindChild("Text").transform.localScale = new Vector3(-0.06f, 0.06f, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void OrderComplete()
    {
        GameObject.Find("ShopManager").GetComponent<ShopManager>().haveQuestGuest = false;
        questBt.SetActive(false);
        for (int i = 0; i < GameData.getItemInventorySize(); i++)
        {
            if (GameData.ItemInventory[i].id == needItemId)
            {
                GameData.ItemInventory[i].Count -= needItemcount;
                BuyCost = (GameData.ItemList[needItemId].Cost + (int)(GameData.ItemList[needItemId].Cost * needItempercent * 0.01)) * needItemcount;
                break;
            }
        }
        isCollide = true;
        if(GameData.getStar()<100) GameData.addStar(3);
        Star.SetActive(true);
        StartCoroutine(BuyCounter(0));
    }
    public void OnClickQuestBt()
    {
        if (!GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI)
            GameObject.Find("ShopManager").GetComponent<ShopManager>().OrderPanel.GetComponent<OrderPanel>().setOrderPanel(needItemId, needItemcount, needItempercent, this);
    }
    IEnumerator Enter()
    {
        isCollide = true;
        yield return new WaitForSeconds(1f);
        isCollide = false;
    }
 

    IEnumerator Buying(Collider2D col)
    {
         StartCoroutine(CreateMessage(2));
        yield return new WaitForSeconds(2f);


        ShopInfo Shopinfo = col.transform.GetComponentInParent<ShopInfo>();
        int ranNum = UnityEngine.Random.Range(0, 100);

        if ((Shopinfo.OnSale))
        {
            if (ranNum < 50)
            {
                isBuying = true;
                BuyIndex = Shopinfo.SaleItemID;
                BuyCost = GameData.ItemList[BuyIndex].Cost;
                Shopinfo.SoldOut();
                StartCoroutine(BuyCounter(BuyIndex));
            }
            else isCollide = false; 
        }
        else isCollide = false;
        Shopinfo.isLook = false;
        StopCoroutine(Buying(col));
    }


    IEnumerator BuyCounter(int id)
    {
        transform.FindChild("coin").gameObject.SetActive(true);
        transform.FindChild("coin").FindChild("Text").GetComponent<TextMesh>().text = BuyCost + "";
        if (GameObject.Find("DayManager")) GameObject.Find("DayManager").transform.FindChild("CoinAni").GetComponent<Animator>().Play("addCoin");
        StartCoroutine(CreateMessage(4));
        GameObject.Find("Audio").transform.FindChild("Coin_sound").GetComponent<AudioSource>().Play();

        GameData.addMoney(BuyCost);
        BuyCost = 0;
        yield return new WaitForSeconds(1f);
        
        transform.FindChild("coin").gameObject.SetActive(false);
        Dir = new Vector3(-speed, 0, 0);
        transform.FindChild("Text").transform.localScale = new Vector3(-0.06f, 0.06f, 0);
        transform.localScale = new Vector3(-1, 1, 1);
        isBuying = true;
        isCollide = false;
        StopCoroutine(BuyCounter(BuyIndex));
    }

    IEnumerator CreateMessage(int Type)
    {

        if (guestType == GuestType.Normal)
        {
            int ranNum = UnityEngine.Random.Range(0, 10);
            if (Type == 1) // enter
            {
                Dir = new Vector3(speed, 0, 0);
                yield return new WaitForSeconds(1f);
                if (0 < ranNum && ranNum < 4) g_text.text = "물건이 있나?";

                else if (4 < ranNum && ranNum < 8) g_text.text = "안녕하세요";

                else g_text.text = "";
                
                int floorNum = UnityEngine.Random.Range(0, GameData.getFloor());
                if(floorNum!=0) transform.position += new Vector3(0, 1.95f *floorNum, 0);
            }
            if (Type == 2) // looking
            {
                if (0 < ranNum && ranNum < 4) g_text.text = "흠..";
                else if (4 < ranNum && ranNum < 8) g_text.text = "좀 비싼데..";
                else g_text.text = "";
            }
            if (Type == 3) //buy
            {
                if (0 < ranNum && ranNum < 4) g_text.text = "사야지";
                else if (4 < ranNum && ranNum < 8) g_text.text = "주섬주섬";
                else g_text.text = "";
            }
            if (Type == 4) //counter
            {
                if (0 < ranNum && ranNum < 4) g_text.text = "여기요";
                else if (4 < ranNum && ranNum < 8) g_text.text = "얼마예요?";
                else g_text.text = "";
            }

            if (Type == 5) //counter
            {
                if (0 < ranNum && ranNum < 4) g_text.text = "살게없네..";
                else if (4 < ranNum && ranNum < 8) g_text.text = "다음에 올게요";
                else g_text.text = "";
            }
            if (Type == 6) //counter
            {
                if (0 < ranNum && ranNum < 4) g_text.text = "신난다~";
                else if (4 < ranNum && ranNum < 8) g_text.text = "안녕히 계세요";
                else g_text.text = "";
            }
            yield return new WaitForSeconds(2f);
            g_text.text = "";
        }
        else {
            if(Type == 1) Dir = new Vector3(speed, 0, 0);
            
            if (Type == 4)
            {
                int ranNum = UnityEngine.Random.Range(0, 3);
                if (ranNum == 0) g_text.text = "감사합니다~";
                if (ranNum == 1) g_text.text = "또 올게요!";
                if (ranNum == 2) g_text.text = "여기요";
                yield return new WaitForSeconds(3f);
                g_text.text = "";
            }
        }

    }


}
