using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelUpCanvas : MonoBehaviour {
    
    public Text Script;
    public GameObject RewardNode;
    public GameObject RewardContents;


    public void Update()
    {
    }
    public void SetLevelUpCanvas()
    {
        GetComponent<Animator>().Play("PopUpAni");
        GameObject.Find("Audio").transform.FindChild("levelup").GetComponent<AudioSource>().Play();
       
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;


        int level = GameData.getLevel();
        Script.text = "Lv." + (level);

        string[] tmp = GameData.LevelUpReward[level-1].Split('_');
        for (int i = 0; i < tmp.Length; i++)
        {

            if (tmp[i].Split('|')[0].Equals("mon"))
            {
                GameObject Tmp = Instantiate(RewardNode);
                Tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("char/coin_re");
                Tmp.transform.SetParent(RewardContents.transform);
                Tmp.transform.localScale = new Vector3(1, 1, 1);
                Tmp.transform.FindChild("Text").GetComponent<Text>().text = "" + int.Parse(tmp[i].Split('|')[1]);

                GameData.addMoney(int.Parse(tmp[i].Split('|')[1]));
            }
            if (tmp[i].Split('|')[0].Equals("eme"))
            {
                GameObject Tmp = Instantiate(RewardNode);
                Tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("char/eme_re");
                Tmp.transform.SetParent(RewardContents.transform);
                Tmp.transform.localScale = new Vector3(1, 1, 1);
                Tmp.transform.FindChild("Text").GetComponent<Text>().text = "" + int.Parse(tmp[i].Split('|')[1]);

                GameData.addEmerald(int.Parse(tmp[i].Split('|')[1]));
            }
            if (tmp[i].Split('|')[0].Equals("bf"))
            {
                GameObject Tmp = Instantiate(RewardNode);
                Tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[int.Parse(tmp[i].Split('|')[1])].id);
                Tmp.transform.SetParent(RewardContents.transform);
                Tmp.transform.localScale = new Vector3(1, 1, 1);
                Tmp.transform.FindChild("Text").GetComponent<Text>().text = "";
                Item tt = GameData.ItemList[int.Parse(tmp[i].Split('|')[1])];// = true;
                tt.makeAble = true;
                GameData.ItemList[int.Parse(tmp[i].Split('|')[1])] = tt;
            }

        }
        RewardContents.GetComponent<RectTransform>().sizeDelta = new Vector2(80 * RewardContents.transform.childCount, 110);
        RewardContents.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().ActiveUI();
    }

    public void OnClickOk()
    {
        GetComponent<Animator>().Play("PopDownAni");
        if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().UnActiveUI();

        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.FindChild("Panel").localScale = new Vector3(0.05f, 0.05f, 1);
        int childNum = RewardContents.transform.childCount;
        for(int i = 0; i < childNum; i++){
            DestroyImmediate(RewardContents.transform.GetChild(0).gameObject);
        }
    }
   
}
