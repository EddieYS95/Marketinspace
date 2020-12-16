using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MarketManager : MonoBehaviour {

  

    public GameObject eleBox;

    public GameObject Itemlist;
    public GameObject BoxPanel;

    public GameObject ConfirmPanel;



    int BoxIndex;
    bool isEmerald;

    int RandomNum ;
    int RandomTier ;
    // Use this for initialization
    void Start () {

        isEmerald = false;
        setBoxList();
        
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void OnClickElementBt(int index)
    {

        if (GameData.ElementBoxList[index].bricks != 0)
        {
            if (GameData.getMoney() < GameData.ElementBoxList[index].bricks) { BoxPanel.transform.FindChild("DealBt").GetComponent<Button>().interactable = false; BoxPanel.transform.FindChild("DealBt").transform.FindChild("Text").GetComponent<Text>().text = "브릭스 부족"; }
            else
            {
                GameData.soltElement();
                if (GameData.ElementInventory[GameData.getElementInventorySize()-1].NodeType == NodeType.Empty)
                {
                    BoxPanel.transform.FindChild("DealBt").GetComponent<Button>().interactable = true;
                    BoxPanel.transform.FindChild("DealBt").transform.FindChild("Text").GetComponent<Text>().text = "구매하기";
                   
                }
                else
                {
                    BoxPanel.transform.FindChild("DealBt").GetComponent<Button>().interactable = false;
                    BoxPanel.transform.FindChild("DealBt").transform.FindChild("Text").GetComponent<Text>().text = "인벤토리 초과";
                }
               
            }
            isEmerald = false;
        }
        else
        {
            if (GameData.getEmerald() < GameData.ElementBoxList[index].emerald) { BoxPanel.transform.FindChild("DealBt").GetComponent<Button>().interactable = false; BoxPanel.transform.FindChild("DealBt").transform.FindChild("Text").GetComponent<Text>().text = "에메랄드 부족"; }
            else
            {
                BoxPanel.transform.FindChild("DealBt").GetComponent<Button>().interactable = true;
                BoxPanel.transform.FindChild("DealBt").transform.FindChild("Text").GetComponent<Text>().text = "구매하기";
                
            }
            isEmerald = true;
        }
          
        BoxPanel.SetActive(true);

        string[] getData = GameData.ElementBoxList[index].reword.Split('_');

        for (int i = 0; i < 5; i++)
        {

            if(i < getData.Length)
            {
                BoxPanel.transform.FindChild("elements").GetChild(i).gameObject.SetActive(true);
                string[] tmp = getData[i].Split('|');

                if (tmp[0].Equals("El"))
                {
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Image").GetComponent<Image>().sprite =
                         Resources.Load<Sprite>("Element/" + GameData.ElementList[int.Parse(tmp[1])].id);
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text =
                         "" + GameData.ElementList[int.Parse(tmp[1])].name;
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().color = new Color32(199, 183, 171, 255);
                   BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Count").GetComponent<Text>().text =
                       "" + int.Parse(tmp[2]);
                }
                else if (tmp[0].Equals("RAN"))
                {
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Image").GetComponent<Image>().sprite =
                        Resources.LoadAll<Sprite>("char/random")[int.Parse(tmp[1])];

                    if (int.Parse(tmp[1]) == 0) BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text = "초급 랜덤재료";
                    else if (int.Parse(tmp[1]) == 1) BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text = "중급 랜덤재료";
                    else if (int.Parse(tmp[1]) == 2) BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text = "고급 랜덤재료";
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().color = new Color32(255,255, 255, 255);
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Count").GetComponent<Text>().text =
                       "" + int.Parse(tmp[2]);
                }
                else if (tmp[0].Equals("DO"))
                {
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Image").GetComponent<Image>().sprite =
                        Resources.LoadAll<Sprite>("char/do")[int.Parse(tmp[1])];

                    if (int.Parse(tmp[1]) == 0) BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text = "★ ~ ★★★";
                    else if (int.Parse(tmp[1]) == 1) BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text = "★★★ ~ ★★★★★";
                   
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().color = new Color32(255, 241, 0, 255);
                    BoxPanel.transform.FindChild("elements").GetChild(i).FindChild("Count").GetComponent<Text>().text =
                       "";
                }
            }
            else
            {
                BoxPanel.transform.FindChild("elements").GetChild(i).gameObject.SetActive(false);
            }
        }
        BoxIndex = index;
        BoxPanel.transform.FindChild("Image").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("char/elebox")[index];

        if (!isEmerald)
        {
            BoxPanel.transform.FindChild("Cost").GetComponent<Text>().color = new Color32(254, 204, 0, 255);
            BoxPanel.transform.FindChild("Cost").GetComponent<Text>().text = GameData.ElementBoxList[index].bricks + "B로 " + GameData.ElementBoxList[index].name + "를 구매하시겠습니까?";
        }
        else
        {
            BoxPanel.transform.FindChild("Cost").GetComponent<Text>().color = new Color32(129, 242, 0, 255);
            BoxPanel.transform.FindChild("Cost").GetComponent<Text>().text = "에메랄드 " + GameData.ElementBoxList[index].emerald + "개로 " + GameData.ElementBoxList[index].name + "를 구매하시겠습니까?"; }
    }

    public void setBoxList()
    {
       
         for (int i=0; i<GameData.ElementBoxList.Count; i++)
        {
            GameObject box;
            int index = i;
            box = Instantiate(eleBox, Itemlist.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            box.transform.SetParent(Itemlist.transform);
            box.transform.localScale = new Vector3(1, 1, 1);

            box.transform.FindChild("Name").GetComponent<Text>().text = "" + GameData.ElementBoxList[i].name;

            box.transform.FindChild("Image").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("char/elebox")[i];

            if (GameData.ElementBoxList[i].bricks != 0)
            {

                box.transform.FindChild("Cost").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/coin");
                box.transform.FindChild("coin").GetComponent<Text>().color = new Color32(254, 204, 0, 255);
                box.transform.FindChild("coin").GetComponent<Text>().text = "" + GameData.ElementBoxList[i].bricks;
                
            }
            else
            {

                box.transform.FindChild("Cost").GetComponent<Image>().sprite = Resources.Load<Sprite>("char/icn_emerald");
                box.transform.FindChild("coin").GetComponent<Text>().color = new Color32(129, 242, 0, 255);
                box.transform.FindChild("coin").GetComponent<Text>().text = "" + GameData.ElementBoxList[i].emerald;
            }

            box.GetComponent<Button>().onClick.AddListener(() => { OnClickElementBt(index); });
        }
        Itemlist.GetComponent<RectTransform>().sizeDelta = new Vector2(175 * GameData.ElementBoxList.Count, 240);
    }
    public void OnClickBuy()
    {
        if (!isEmerald)
        GameData.useMoney(GameData.ElementBoxList[BoxIndex].bricks);
        else GameData.useEmerald(GameData.ElementBoxList[BoxIndex].emerald);
        ConfirmPanel.SetActive(true);

        ConfirmPanel.transform.FindChild("Box").gameObject.SetActive(true);
        ConfirmPanel.transform.FindChild("Rewords").gameObject.SetActive(false);
        ConfirmPanel.transform.FindChild("Box").FindChild("Image").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("char/elebox")[BoxIndex];

        string[] getData = GameData.ElementBoxList[BoxIndex].reword.Split('_');
        for (int i = 0; i < 6; i++)
        {

            if (i < getData.Length)
            {
                
                string[] tmp = getData[i].Split('|');

                if (tmp[0].Equals("El"))
                {

                    ConfirmPanel.transform.FindChild("Rewords").FindChild("Cost").GetComponent<Text>().text = "재료를 성공적으로 획득하였습니다.";
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).gameObject.SetActive(true);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetComponent<GridLayoutGroup>().cellSize = new Vector2(120, 110);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Image").GetComponent<Image>().sprite =
                         Resources.Load<Sprite>("Element/" + GameData.ElementList[int.Parse(tmp[1])].id);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text =
                         "" + GameData.ElementList[int.Parse(tmp[1])].name;
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().color = new Color32(199, 183, 171, 255);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Count").GetComponent<Text>().text =
                       "" + int.Parse(tmp[2]);

                    GameData.setElement(int.Parse(tmp[1]), int.Parse(tmp[2]));
                }
                else if (tmp[0].Equals("RAN"))
                {
                    
                    int elecount = GameData.ElementList.Count;

                    while (true)
                    {
                        RandomNum = Random.Range(0, GameData.ElementList.Count);
                        RandomTier = GameData.ElementList[RandomNum].value;

                        if (RandomTier == int.Parse(tmp[1])) break;

                    }
                    
                    int randomNum = RandomNum;
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("Cost").GetComponent<Text>().text = "재료를 성공적으로 획득하였습니다.";
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).gameObject.SetActive(true);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Image").GetComponent<Image>().sprite =
                          Resources.Load<Sprite>("Element/" + GameData.ElementList[randomNum].id);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text =
                         "" + GameData.ElementList[randomNum].name;
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Count").GetComponent<Text>().text =
                       "" + int.Parse(tmp[2]);

                    GameData.setElement(randomNum, int.Parse(tmp[2]));
                }
                else if (tmp[0].Equals("DO"))
                {

                    i = 5;
                    if (int.Parse(tmp[1]) == 0) {

                        while (true)
                        {
                            RandomNum = Random.Range(14, GameData.ItemList.Count);
                            RandomTier = GameData.ItemList[RandomNum].Tier;

                            if (RandomTier == 1 || RandomTier == 2 || RandomTier == 3) break;

                        }

                    }
                    else if (int.Parse(tmp[1]) == 1) {
                        while (true)
                        {
                            RandomNum = Random.Range(14, GameData.ItemList.Count);
                            RandomTier = GameData.ItemList[RandomNum].Tier;

                            if (RandomTier == 3 || RandomTier == 4 || RandomTier == 5) break;

                        }
                    }

                    for (int j = 0; j < 5; j++)
                    {
                        ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(j).gameObject.SetActive(false);
                    }

                    ConfirmPanel.transform.FindChild("Rewords").FindChild("Cost").GetComponent<Text>().text = "도면을 성공적으로 획득하였습니다.";
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).gameObject.SetActive(true);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Image").GetComponent<Image>().sprite =
                          Resources.Load<Sprite>("Item/" + GameData.ItemList[RandomNum].id);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().text =
                         "" + GameData.ItemList[RandomNum].name;
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Name").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Count").GetComponent<Text>().text = "";

                    for (int j=0; j<RandomTier; j++)
                    {
                        ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).FindChild("Count").GetComponent<Text>().text += "★";
                    }
                    
                    GameData.setAbleMake(RandomNum);

                    break;
                }
            }
            else
            {
                ConfirmPanel.transform.FindChild("Rewords").FindChild("elements").GetChild(i).gameObject.SetActive(false);
            }
        }
      


    }
}
