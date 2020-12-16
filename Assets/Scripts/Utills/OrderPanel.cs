using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour {

    public Text Contents;
    public Image itemImg;
    public Text NeedData;
    public Button OkBt;

    private int itemid;
    private int count;
    private float percent;

    private GuestScript guest;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Contents.text = GameData.ItemList[itemid].name + " " + count + "개 있나요??" +"\n"+ percent +"% 더 쳐서 "+ (GameData.ItemList[itemid].Cost + (int)(GameData.ItemList[itemid].Cost * percent* 0.01))* count + "B에 살게요.";
        itemImg.sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[itemid].id);
        NeedData.text = GameData.ItemList[itemid].name + " " + GameData.getItemNum(itemid) + " / " + count;

        if (GameData.getItemNum(itemid) >= count)
        {
            OkBt.GetComponentInChildren<Text>().text = "여기 있습니다.";
        }
        else
        {
            OkBt.GetComponentInChildren<Text>().text = "잠시만 기다려주세요 !";
        }

	}

    public void setOrderPanel(int id, int Count, float Percent, GuestScript guest){
        itemid = id;
        count = Count;
        percent = Percent;
        this.guest = guest;
        gameObject.SetActive(true);
    }

    public void OnClickOkBt()
    {
        if (GameData.getItemNum(itemid) >= count)
        {
            guest.OrderComplete();
        }
        else
        {
            
        }
        gameObject.SetActive(false);
        if (GameObject.Find("ShopManager")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = false;
    }
    public void OnClickCancelBt()
    {
        guest.OrderCancel();
        gameObject.SetActive(false);
        if (GameObject.Find("ShopManager")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = false;
    }
}
