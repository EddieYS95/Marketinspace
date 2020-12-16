using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class questDataDialog : MonoBehaviour {

    public GameObject Mark;
    public GameObject QuestList;
    public GameObject QuestListNode;
    public Button RewordBtn;
    public Text Title;
    public Text Contents;
    public Text Goal;
    public Text Reward;

    public int QuestID;
    QuestListNode ClickNode;

    public GameObject TypeBtn;
    
    bool ListType = false; // 준비 : false , 진행중 : true
    bool isDone = false;
    public bool isNew = false;
    void Start () {
        QuestID = -1;
        OnClickPrograssQuestBt();

        
        // 새로운 퀘스트 추가
        AddNewQuest(63);
        AddNewQuest(69);
    }
	
	// Update is called once per frame
	void Update () {

        isDone = false;

        if (ListType)
        {
            TypeBtn.transform.FindChild("Ing").GetComponent<Button>().interactable = false;
            TypeBtn.transform.FindChild("Ready").GetComponent<Button>().interactable = true;
        }
        else
        {

            TypeBtn.transform.FindChild("Ing").GetComponent<Button>().interactable = true;
            TypeBtn.transform.FindChild("Ready").GetComponent<Button>().interactable = false;
        }
        TypeBtn.transform.FindChild("Ready").transform.FindChild("Text").GetComponent<Text>().text = "시작가능 퀘스트 (" + GameData.QuestInReady.Count + ")";
        TypeBtn.transform.FindChild("Ing").transform.FindChild("Text").GetComponent<Text>().text = "진행중인 퀘스트 (" + GameData.QuestInProgress.Count + ")";

        for (int i = 0; i < GameData.QuestInProgress.Count; i++)
        {
            if (GameData.chkQuest(i) == true) isDone = true;
        }
        if (isDone) TypeBtn.transform.FindChild("Ing").transform.FindChild("Done").gameObject.SetActive(true);
        else TypeBtn.transform.FindChild("Ing").transform.FindChild("Done").gameObject.SetActive(false);
        if (isNew) Mark.gameObject.SetActive(true);
        else Mark.gameObject.SetActive(false);

    }


    void AddNewQuest(int QuestID)
    {
        
        bool isHave = false;

        for (int i = 0; i < GameData.QuestList.Count; i++)
        {
            if (GameData.QuestInReady.Count > i)
            {
                if (GameData.QuestInReady[i] == QuestID) isHave = true;
            }
            if (GameData.QuestInProgress.Count > i)
            {
                if (GameData.QuestInProgress[i] == QuestID) isHave = true;
            }
            if (GameData.QuestInComplete.Count > i)
            {
                if (GameData.QuestInComplete[i] == QuestID) isHave = true;
            }

            if (isHave) break;
        }

        if (!isHave) GameData.setQuestReady(QuestID);
    }

    public void OnClickQuest()
    {
        if(GameData.QuestInReady.Count != 0)
        {
            OnClickReadyQuestBt();
        }
        else
        {
            OnClickPrograssQuestBt();
        }
    }

    public void OnClickReadyQuestBt()
    {
        QuestID = -1;
        isNew = false;
        ListType = false;

        QuestList.GetComponent<RectTransform>().rect.Set(0, 0, 0, 0);
        QuestList.GetComponent<RectTransform>().sizeDelta = new Vector2(345, 95 * GameData.QuestInReady.Count);

        RewordBtn.interactable = false;
        RewordBtn.transform.FindChild("Text").GetComponent<Text>().text = "퀘스트 수락";

        for (int i = 0; i < QuestList.transform.childCount; i++)
        {
            Destroy(QuestList.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < GameData.QuestInReady.Count; i++)
        {
            GameObject node = Instantiate(QuestListNode);
            node.transform.SetParent(QuestList.transform);
            node.transform.localScale = new Vector3(1, 1, 1);
            node.GetComponent<QuestListNode>().QuestID = GameData.QuestInReady[i];
            node.name = "Quest" + GameData.QuestInReady[i];

            
            node.GetComponent<QuestListNode>().Papa = this;
        }

        Title.text = "";
        Goal.text = "";
        Reward.text = "";
        Contents.text = "";

    }

    public void OnClickPrograssQuestBt()
    {
        QuestID = -1;
        ListType = true;

        
        QuestList.GetComponent<RectTransform>().rect.Set(0,0,0,0);
        QuestList.GetComponent<RectTransform>().sizeDelta = new Vector2(345, 95 * GameData.QuestInProgress.Count);

        RewordBtn.interactable = false;
        RewordBtn.transform.FindChild("Text").GetComponent<Text>().text = "퀘스트 완료";

        for (int i = 0; i < QuestList.transform.childCount; i++)
            {
                Destroy(QuestList.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < GameData.QuestInProgress.Count; i++)
            {
                GameObject node = Instantiate(QuestListNode);
                node.transform.SetParent(QuestList.transform);
                node.transform.localScale = new Vector3(1, 1, 1);
                node.GetComponent<QuestListNode>().QuestID = GameData.QuestInProgress[i];
                node.name = "Quest" + GameData.QuestInProgress[i];
            /*
            if (GameData.QuestList[i].mainQuest == true)
            {
                node.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/icn_btn")[7];
                node.transform.FindChild("Text").GetComponent<Text>().color = new Color32(231, 223, 217, 255);
                node.transform.FindChild("Text").GetComponent<Outline>().effectColor = new Color32(0, 0, 0, 125);
            }*/

                if (GameData.chkQuest(i) == true)
                {
                    node.transform.FindChild("Chk").GetComponent<CanvasGroup>().alpha = 1;
                    
                }
                
            node.GetComponent<QuestListNode>().Papa = this;
            }
            Title.text = "";
            Goal.text = "";
            Reward.text = "";
            Contents.text = "";

    }

    public void OnClickNode(QuestListNode ql)
    {
        int id = ql.QuestID;
        ClickNode = ql;
        Goal.text = "";
        Reward.text = "";
        Contents.text = "";
        
        
        Title.text = GameData.QuestList[id].name;
        string[] tempT = GameData.QuestList[id].goalText.Split('_');
        foreach (string t in tempT)
        {
            Goal.text +=  t + "\n";
        }

        tempT = GameData.QuestList[id].script.Split('_');
        foreach (string t in tempT)
        {
            Contents.text = Contents.text + t + "\n";
        }
 
        tempT = GameData.QuestList[id].rewardText.Split('_');
        foreach (string t in tempT)
        {
            Reward.text = Reward.text + t + "\n";
        }

       

        if (ListType)
        {
            for (int i = 0; i < QuestList.transform.childCount; i++)
            {
                if (ql.name == QuestList.transform.GetChild(i).name)
                {
                        if (GameData.chkQuest(i) == true)
                        {
                            RewordBtn.interactable = true;
                            RewordBtn.transform.FindChild("Text").GetComponent<Text>().text = "퀘스트 완료";
                            QuestID = i;
                    }
                        else
                        {

                            ql.transform.FindChild("Mark").GetComponent<CanvasGroup>().alpha = 0;
                            RewordBtn.interactable = false;
                            RewordBtn.transform.FindChild("Text").GetComponent<Text>().text = "퀘스트 완료";
                        }
                   
                }
            }
        }
        else
        {

            for (int i = 0; i < QuestList.transform.childCount; i++)
            {
                if (ql.name == QuestList.transform.GetChild(i).name)
                {
                   
                        QuestID = GameData.QuestInReady[i];
                        RewordBtn.interactable = true;
                        RewordBtn.transform.FindChild("Text").GetComponent<Text>().text = "퀘스트 수락";

                   
                }
            }
        }
      

    }

   public void OnClickReword()
    {

        int TalkID = -1;
    
        
        if (ListType)
        {
            if (GameData.chkQuest(QuestID) == true && QuestID != -1)
            {
                for (int i = 0; i < GameData.EventList.Count; i++)
                {
                    if (GameData.EventList[i].questSet == GameData.QuestInProgress[QuestID])
                    {
                        TalkID = i;
                        break;
                    }
                }
                GameObject.Find("QuestCanvas").GetComponent<QuestFactory>().makeEndQuest(QuestID);
                if (GameData.EventList[TalkID + 1].questSet == -1)
                {
                    Debug.Log("시작");
                    if (GameObject.Find("TalkCanvas") && TalkID != -1)
                        GameObject.Find("TalkCanvas").GetComponent<EventManager>().setEvent(TalkID + 1);
                }

            }
            OnClickPrograssQuestBt();
        }
        else
        {
            for (int i = 0; i < GameData.EventList.Count; i++)
            {
                if (GameData.EventList[i].questSet == QuestID)
                {
                    TalkID = i;
                    break;
                }
            }
            GameData.QuestInProgress.Add(QuestID);
            GameData.QuestInReady.Remove(QuestID);
            if (GameObject.Find("TalkCanvas") && TalkID != -1)
                GameObject.Find("TalkCanvas").GetComponent<EventManager>().setEvent(TalkID);
            OnClickReadyQuestBt();
        }
       
    }
}
