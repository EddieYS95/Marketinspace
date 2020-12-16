using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpFloorPanel : MonoBehaviour {
    public GameObject Builds;
    public GameObject Build;
    
    
    
    Text floorNum;
    Text Coin;
    Text Star;
    Button Okbtn;

    int needCoin;
    int needStar;
    int myfloor;
    
	// Use this for initialization
	void Start () {
        floorNum = transform.FindChild("floorNum").GetComponent<Text>();
        Coin = transform.FindChild("Coin").FindChild("Text").GetComponent<Text>();
        Star = transform.FindChild("Star").FindChild("Text").GetComponent<Text>();
        Okbtn = transform.FindChild("Okbt").GetComponent<Button>();

        FloorSet();
       
    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<CanvasGroup>().interactable)
        {
            myfloor = GameData.getFloor();
            if (myfloor == 1) { needStar = 20; needCoin = 2000; }
            else if (myfloor == 2) { needStar = 100; needCoin = 4000; }
            else if (myfloor == 3) { needStar = 300; needCoin = 8000; }
            else if (myfloor == 4) { needStar = 500; needCoin = 20000; }
            else { needStar = 1000; needCoin = 100000; }

            Coin.text = "" + needCoin;
            Star.text = needStar + "↑";

            floorNum.text = myfloor + "   >   " + (myfloor + 1);

            if (GameData.getMoney() >= needCoin && GameData.getStar() >= needStar) Okbtn.interactable = true;
            else Okbtn.interactable = false;
        }
    }
    public void UpShopFloor()
    {
        GameData.useMoney(needCoin);
        GameData.upShopFloor();
        Builds.transform.FindChild("Plus").transform.position += new Vector3(0, Build.transform.position.y, 0);
        Vector3 pos = Builds.transform.position + new Vector3(0, Build.transform.position.y *(GameData.getFloor()-1), -(1+ GameData.getFloor()*0.01f));
        GameObject build;
        build = Instantiate(Build, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        build.transform.parent = Builds.transform;
        build.GetComponent<ShopGrid>().FloorIndex = GameData.getFloor();
        build.name = "Build (" + GameData.getFloor() + ")";
        Builds.transform.parent.FindChild("sky").transform.position += new Vector3(0, Build.transform.position.y, 0);

    }

    public void FloorSet()
    {
        Builds.transform.FindChild("Plus").transform.position += new Vector3(0, Build.transform.position.y * (GameData.getFloor()-1), 0);
        Builds.transform.parent.FindChild("sky").transform.position += new Vector3(0, Build.transform.position.y * (GameData.getFloor() - 1), 0);
        for (int i=1; i<GameData.getFloor(); i++)
        {
            Vector3 pos = Builds.transform.position + new Vector3(0, Build.transform.position.y * i, -(1 + i* 0.01f));
            GameObject build;
            build = Instantiate(Build, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
            build.transform.parent = Builds.transform;
            build.GetComponent<ShopGrid>().FloorIndex = i + 1;
            build.name = "Build (" + (i+1) + ")";
        }
    }
    public void OnClick()
    {
        if (GetComponent<CanvasGroup>().alpha == 1)
        {
            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
