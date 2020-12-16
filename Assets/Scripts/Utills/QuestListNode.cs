using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QuestListNode : MonoBehaviour {

    public int QuestID;
    public questDataDialog Papa;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponentInChildren<Text>().text = GameData.QuestList[QuestID].name;

        transform.FindChild("Image").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("charimg/char_q")[GameData.QuestList[QuestID].charNum];
        if (GameData.QuestList[QuestID].mainQuest) {
            GetComponent<Image>().color = new Color32(255, 240,140, 255);
            transform.FindChild("Text").GetComponent<Text>().color = new Color32(255, 220, 50, 255);
        }
    }

    public void OnClickThis()
    {
        Papa.OnClickNode(this);
    }
}
