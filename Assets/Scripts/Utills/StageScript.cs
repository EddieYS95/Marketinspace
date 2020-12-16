using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageScript : MonoBehaviour {

    GameObject StageInfo;
    GameObject StageLevel;

    int Index;
    string Name;

    int SpaceNum;

    public int Limitlevel;
	// Use this for initialization
	void Start () {
        StageLevel = transform.FindChild("Level").gameObject;
        StageInfo = transform.FindChild("Text").gameObject;
        Index = int.Parse(name);

    }

    // Update is called once per frame
    void Update() {
        if (transform.parent.name != "stage_r_list")
        {
            SpaceNum = GameData.getSpaceNum() + 1;
            int listNum = (SpaceNum - 1) * 5 + (Index - 1);


            transform.FindChild("Image").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Stage/" + SpaceNum)[Index - 1];
            StageInfo.GetComponent<Text>().text = SpaceNum + "-" + Index + " " + GameData.StageList[listNum].name;
            if (GameData.StageList[listNum].level > GameData.getLevel())
                StageLevel.GetComponent<Text>().color = new Color32(250, 30, 30, 255);
            else StageLevel.GetComponent<Text>().color = new Color32(130, 250, 90, 255);
            StageLevel.GetComponent<Text>().text = "Lv." + GameData.StageList[listNum].level;


            int EleNum = 0;
            Stage nowStage = GameData.StageList[listNum];
            string[] Droptmp = nowStage.drop.Split('|');

            for (int i = 0; i < Droptmp.Length; i++)
            {
                string[] tmp = Droptmp[i].Split('_');

                if (tmp[0].Equals("El"))
                {
                    transform.FindChild("Ele").GetChild(EleNum).GetComponent<Image>().sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[int.Parse(tmp[1])].id);
                    EleNum++;
                }


            }
        }
        else
        {
            SpaceNum = GameData.getSpaceNum() + 1;
            int listNum = (SpaceNum - 1) * 5 + (Index - 1);

            if (Index * 10 > GameData.getLevel() || GameData.getSpaceOpen() < Index-1)
            {
                transform.FindChild("Lock").gameObject.SetActive(true);
                //GetComponent<Button>().interactable = false;
                StageLevel.GetComponent<Text>().color = new Color32(250, 30, 30, 255);
            }
            else
            {
                transform.FindChild("Lock").gameObject.SetActive(false);
                //GetComponent<Button>().interactable = true;
                StageLevel.GetComponent<Text>().color = new Color32(130, 250, 90, 255); }
            StageLevel.GetComponent<Text>().text = "Lv." + Index * 10;


        }
        // if(GameData.getLevel() < Limitlevel) this.GetComponent<Button>().interactable = false;

    }
}
