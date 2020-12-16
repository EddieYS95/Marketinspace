using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBoxScript : MonoBehaviour {
    GameObject BoxImage;
    GameObject Rewords;
    int Index;
    bool isEle = false;
    int itemID = 0;

    int itemTier = 0;

    // Use this for initialization
    void Start () {
        BoxImage = transform.FindChild("BoxPanel").FindChild("Panel").FindChild("Box").FindChild("Image").gameObject;
        Rewords = transform.FindChild("BoxPanel").FindChild("Panel").FindChild("Rewords").gameObject;
        Index = GameObject.Find("HuntManager").GetComponent<HuntManager>().ID / 10;

        BoxImage.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("char/ranbox")[Index-1];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickBox()
    {
        SetRandomItem();
        GameObject.Find("Audio").transform.FindChild("crash").GetComponent<AudioSource>().Play();
        transform.FindChild("BoxPanel").FindChild("Panel").GetComponent<Animator>().Play("Boxopen");
    }

    void SetRandomItem()
    {
        int Ele0 = 0; int Ele1 = 0; int Ele2 = 0;

        int It2 = 0; int It3 = 0; int It4 = 0; int It5 = 0;



        // 행성별 랜덤 밸류
        if (Index == 1)
        {
            Ele0 = 70000; Ele1 =25000; Ele2 = 4741;
            It2 = 125; It3 = 96; It4 = 25; It5 = 13;
        }
        else if (Index == 2)
        {
            Ele0 = 69750; Ele1 = 25000; Ele2 = 4741;
            It2 = 205; It3 = 146; It4 = 85; It5 = 63;
        }
        else if (Index == 3)
        {
            Ele0 = 69500; Ele1 = 25000; Ele2 = 4741;
            It2 = 265; It3 = 206; It4 = 155; It5 = 133;
        }
        

        int wholeNum = 100000;
        int randomNum = UnityEngine.Random.Range(0, wholeNum);


        Debug.Log(randomNum);

        if(randomNum >= Ele0)
        {
            if(randomNum >= Ele0 + Ele1)
            {
                if(randomNum >= Ele0 + Ele1 + Ele2)
                {
                    if (randomNum >= Ele0 + Ele1 + Ele2 + It2)
                    {
                        if (randomNum >= Ele0 + Ele1 + Ele2 + It2 + It3)
                        {
                            if (randomNum >= Ele0 + Ele1 + Ele2 + It2 + It3 +It4)
                            {
                                if (randomNum >= Ele0 + Ele1 + Ele2 + It2 + It3 + It4 + It5)
                                {
                                    isEle = false;
                                    itemTier = 5;
                                    Debug.Log("5티어");
                                    //5티어 아이템 뽑기
                                }
                                else
                                {
                                    isEle = false;
                                    itemTier = 5;
                                    //5티어 아이템 뽑기
                                    Debug.Log("5티어");
                                }
                            }
                            else
                            {
                                isEle = false;
                                itemTier = 4;
                                Debug.Log("4티어");
                                //4티어 아이템 뽑기
                            }
                        }
                        else
                        {
                            isEle = false;
                            itemTier = 3;
                            Debug.Log("3티어");
                            //3티어 아이템 뽑기
                        }
                    }
                    else {
                        isEle = false;
                        itemTier = 2;
                        Debug.Log("2티어");
                        //2티어 아이템 뽑기
                    }
                }
                else
                {
                    isEle = true;
                    itemTier = 2;
                    Debug.Log("고급 재료");
                    //고급 재료 뽑기
                }
            }
            else
            {
                isEle = true;
                itemTier = 1;
                Debug.Log("중급 재료");
                //중급 재료 뽑기
            }
        }
        else
        {
            isEle = true;
            itemTier = 0;
            Debug.Log("초급 재료");
            //초급 재료 뽑기
        }



        if (isEle)
        {
            int elecount = GameData.ElementList.Count;
            int RandomNum;
            int RandomTier;

            while (true)
            {
                RandomNum = Random.Range(0, GameData.ElementList.Count);
                RandomTier = GameData.ElementList[RandomNum].value;

                if (RandomTier == itemTier) break;

            }

            itemID = RandomNum;

            Rewords.transform.FindChild("Text").GetComponent<Text>().text = "재료를 성공적으로 획득하였습니다.";
            Rewords.transform.FindChild("Image").GetComponent<Image>().sprite =
                  Resources.Load<Sprite>("Element/" + GameData.ElementList[itemID].id);
            Rewords.transform.FindChild("Name").GetComponent<Text>().text = "" + GameData.ElementList[itemID].name;
            Rewords.transform.FindChild("Star").GetComponent<Text>().text = "x10";
            
            int randomCount =Random.Range(10, 30);
            GameData.setElement(itemID, randomCount);
        }
        else
        {
            Rewords.transform.FindChild("Star").GetComponent<Text>().text = "";
            int RandomNum;
            int RandomTier;

            while (true)
                {
                    RandomNum = Random.Range(14, GameData.ItemList.Count);
                    RandomTier = GameData.ItemList[RandomNum].Tier;

                    if (RandomTier == itemTier) break;

                }

            itemID = RandomNum;

            Rewords.transform.FindChild("Text").GetComponent<Text>().text = "도면을 성공적으로 획득하였습니다.";
            Rewords.transform.FindChild("Image").GetComponent<Image>().sprite =
                  Resources.Load<Sprite>("Item/" + GameData.ItemList[RandomNum].id);
            Rewords.transform.FindChild("Name").GetComponent<Text>().text = "" + GameData.ItemList[RandomNum].name;
            
            for (int j = 0; j < RandomTier; j++)
            {
                Rewords.transform.FindChild("Star").GetComponent<Text>().text += "★";
            }

            GameData.setAbleMake(itemID);
        }

    }
}
