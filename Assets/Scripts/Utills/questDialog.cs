using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class questDialog : MonoBehaviour {

    public Text Title;
    public Text Reward;
    public Button OklBt;

    public int id;

    bool setID = false;
    bool Complete = false;
    // Use this for initialization
    void Start()
    {


        GetComponent<Animator>().Play("PopUpAni_q");
        GameObject.Find("Audio").transform.FindChild("complete").GetComponent<AudioSource>().Play();
        if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = true;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (setID == true)
        {
            Reward.text = "";

            Title.text = GameData.QuestList[id].name;

            
            string[] tempT = GameData.QuestList[id].rewardText.Split('_');
            foreach (string t in tempT)
            {
                Reward.text = Reward.text + t + "\n";
            }
        }
        if (Complete == true)
        {
            Reward.text = "";

            OklBt.GetComponentInChildren<Text>().text = "완료";
            Title.text = GameData.QuestList[id].name;
            

            string[] tempT = GameData.QuestList[id].rewardText.Split('_');
            foreach (string t in tempT)
            {
                Reward.text = Reward.text + t + "\n";
            }
        }
	}

    public void MakeDialog(int id)
    {
        setID = true;
        this.id = id;
    }
    public void MakeCompleteDialog(int id)
    {
        Complete = true;
        this.id = id;
    }


    public void OnClickOk()
    {
        if (setID == true)
        {
            GameData.setQuestReady(id);
            Destroy(this.gameObject);
        }
        else if(Complete == true)
        {
            GameData.CompleteQuest(id);
            Destroy(this.gameObject);
            
        }

        if (GameObject.Find("Audio")) GameObject.Find("Audio").transform.FindChild("button-19").GetComponent<AudioSource>().Play();
        GameObject.Find("QuestDataDialog").GetComponent<questDataDialog>().OnClickPrograssQuestBt();
        if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = false;
    }

    public void OnClickCancel()
    {
        Destroy(this.gameObject);

        if (GameObject.Find("Audio")) GameObject.Find("Audio").transform.FindChild("button-19").GetComponent<AudioSource>().Play();
        if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI = false;
    }
}
