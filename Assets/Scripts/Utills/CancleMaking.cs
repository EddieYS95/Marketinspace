using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CancleMaking : MonoBehaviour {
    
    Slider progress;
    private int IndexID;
    private int ItemID;
    bool complete = false;
    public int count;
   
    
    bool start;
    public bool ing;
    public double Ptime;


    public int Comcount;
    Animator ForgeAni;
    // Use this for initialization
    void Start () {
        start = false;
        GameData.SaveGame();
        progress = transform.parent.parent.FindChild("Slider").GetComponent<Slider>();
        ForgeAni = transform.parent.parent.FindChild("ForgeA").GetComponent<Animator>();
        
    }
	

	// Update is called once per frame
	void Update () {
        transform.FindChild("Count").GetComponent<Text>().text = "" + count;

        if(IndexID != -1)
        {
            if (GameObject.Find("ShopManager"))
            {
                
                    if(!start)
                    {
                        GameData.refreshMaking(IndexID, count, DateTime.Now.AddSeconds(-Ptime));
                        start = true;
                    }

                    DateTime date = GameData.ToTime(GameData.getMakingItemTime(IndexID));
                    double TimeSpan = GameData.GetPassedTime(date);
                    float value;

                if (!ing)
                    value = (float)(TimeSpan % GameData.ItemList[ItemID].progressTime) / GameData.ItemList[ItemID].progressTime;
                else
                value = (float)((TimeSpan + Ptime) % GameData.ItemList[ItemID].progressTime) / GameData.ItemList[ItemID].progressTime;
                
                    progress.value = value;


                int TimetoText;
                if (!ing)
                    TimetoText = (int)((GameData.ItemList[ItemID].progressTime * count) - TimeSpan);
                else
                    TimetoText = (int)((GameData.ItemList[ItemID].progressTime * count - Ptime) - TimeSpan);
               

                    if (TimetoText % 60 < 10)
                    progress.transform.FindChild("Text").GetComponent<Text>().text = TimetoText / 60 + ":0" + TimetoText % 60;
                    else progress.transform.FindChild("Text").GetComponent<Text>().text = TimetoText / 60 + ":" + TimetoText % 60;

                 if ((TimeSpan+Ptime) / GameData.ItemList[ItemID].progressTime >= 1 || TimetoText <0)
                {
                    ForgeAni.Play("Complete");
                    Comcount = (int)((TimeSpan + Ptime )/ GameData.ItemList[ItemID].progressTime);
                    complete = true;
                }
                else
                {
                    if(!ForgeAni.gameObject.transform.FindChild("Complete").gameObject.activeInHierarchy)
                    ForgeAni.Play("Making");
                }
            }
        }

        if (complete == true)
        {
            ing = false;
            if (Comcount > count) Comcount = count;
            GameData.setItem(ItemID,Comcount);
            GameObject.Find("Audio").transform.FindChild("complete").GetComponent<AudioSource>().Play();

            if (count != 0)
            {
                count -= Comcount;

                if (count == 0)
                {
                    Debug.Log(count);

                    GameData.removeMakingItem(IndexID);
                    progress.value = 0;
                    progress.transform.FindChild("Text").GetComponent<Text>().text = "";
                    Destroy(this.gameObject);
                }
                else {
                    GameData.refreshMaking(IndexID, count, DateTime.Now);
                    complete = false; }
            }


            GameData.SaveGame();

        }

    }
    public void OnClickCancle(Image item)
    {

        for (int i = 0; i < GameData.ItemList[ItemID].NeedElementID.Length; i++)
        {
            GameData.setElement(GameData.ItemList[ItemID].NeedElementID[i], GameData.ItemList[ItemID].NeedElementCount[i]*count);
        }
        
        GameData.removeMakingItem(IndexID);
        progress.value = 0;
        progress.transform.FindChild("Text").GetComponent<Text>().text = "";
        Destroy(this.gameObject);

        ForgeAni.Play("ForgeIdle");
        GameData.SaveGame();
    }

    public void SetNodeID(int id)
    {
        IndexID = id;
    }
    public void SetItemID(int id)
    {
        ItemID = id;
    }
}
