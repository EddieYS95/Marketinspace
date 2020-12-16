using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class EventPanel : MonoBehaviour {

    public Image LeftImage;
    public Image RightImage;

    public Text name;
    public Text talk;
    
    [HideInInspector]
    public int EventID;
    List<int> talkIDs;

    GameObject Sound;
    int talkid;
    

    string scripttext = "";
    Text textUi;
    char[] pieceArr;
    string msg;

    IEnumerator tempCo;

    bool finishmsg = true;


    void OnDestroy() {
       // Resources.UnloadAsset(LeftImage.sprite);
       // Resources.UnloadAsset(RightImage.sprite);
      
        LeftImage = null;
        RightImage = null;
    }


    void Update()
    {
     
        if (Input.GetButtonDown("Fire")|| Input.GetMouseButtonDown(0))
        {
            NextEvent();
        }
    }

    public void setEvent(int eventID){
        

        EventID = eventID;
        talkIDs = new List<int>();
        talkid = 0;
        string[] talks = GameData.EventList[EventID].talkData.Split('|');
        for (int i = 0; i < talks.Length; i++)
        {
            string[] tmp = talks[i].Split('_');
            if (tmp.Length > 1)
            {
                for (int j = int.Parse(tmp[0]); j <= int.Parse(tmp[1]); j++)
                {
                    talkIDs.Add(j);
                }
            }
        }
        
        if (GameData.TalkList[talkIDs[talkid]].name.Equals("player"))
        {
            name.text = GameData.getName();
            LeftImage.gameObject.SetActive(true);
            RightImage.gameObject.SetActive(false);
            if (Resources.Load<Sprite>("charimg/ch0"))
            {
                LeftImage.sprite = Resources.Load<Sprite>("charimg/ch0");
            }

        }
        else
        {
            name.text = GameData.TalkList[talkIDs[talkid]].name;
            RightImage.gameObject.SetActive(true);
            LeftImage.gameObject.SetActive(false);

            if (Resources.Load<Sprite>("charimg/ch" + GameData.TalkList[talkIDs[talkid]].NpcID))
            {
                RightImage.sprite = Resources.Load<Sprite>("charimg/ch" + GameData.TalkList[talkIDs[talkid]].NpcID);
            }
            else
            {
                RightImage.sprite = Resources.Load<Sprite>("charimg/ch-1"); 
            }
        }


        scripttext = GameData.TalkList[talkIDs[talkid]].talk;

        tempCo = texttest();
        StartCoroutine(tempCo);

        //사진 추가하기
        talkid++;
        if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = true;
    }

    void NextEvent()
    {


        if (finishmsg)
        {
            Sound = GameObject.Find("Audio").transform.FindChild("button-21").gameObject;
            Sound.GetComponent<AudioSource>().Play();
            if (talkid >= talkIDs.Count)
            {
                GameData.ingEvent = -1;

                
                Debug.Log("대화종료");
                if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = false;
                Destroy(this.gameObject);
            }
            else
            {
                if (GameData.TalkList[talkIDs[talkid]].name.Equals("player"))
                {
                    name.text = GameData.getName();
                    LeftImage.gameObject.SetActive(true);
                    RightImage.gameObject.SetActive(false);
                    RightImage.sprite = null;
                    if (Resources.Load<Sprite>("charimg/ch0"))
                    {
                        LeftImage.sprite = Resources.Load<Sprite>("charimg/ch0");
                    }
                }
                else
                {
                    name.text = GameData.TalkList[talkIDs[talkid]].name;
                    RightImage.gameObject.SetActive(true);
                    LeftImage.gameObject.SetActive(false);
                    LeftImage.sprite = null;
                    if (Resources.Load<Sprite>("charimg/ch" + GameData.TalkList[talkIDs[talkid]].NpcID))
                    {
                        RightImage.sprite = Resources.Load<Sprite>("charimg/ch" + GameData.TalkList[talkIDs[talkid]].NpcID);
                    }
                }

                scripttext = GameData.TalkList[talkIDs[talkid]].talk;

                tempCo = texttest();
                StartCoroutine(tempCo);
                talkid++;
            }
        }
        else { StopCoroutine(tempCo); talk.text = scripttext; finishmsg = true; transform.FindChild("skip").gameObject.SetActive(true); }
    }

    

    IEnumerator texttest()

    {
        transform.FindChild("skip").gameObject.SetActive(false);
        msg = ""; talk.text = "";
        finishmsg = false;
        pieceArr = scripttext.ToCharArray();
        for (int i = 0; i < pieceArr.Length; i++)

        {
            msg += pieceArr[i];
            talk.text = msg;
            yield return new WaitForSeconds(0.05f);
            if (i == pieceArr.Length - 1) { finishmsg = true; transform.FindChild("skip").gameObject.SetActive(true); }
        }



    }
}

