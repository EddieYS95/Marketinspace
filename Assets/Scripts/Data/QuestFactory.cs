using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QuestFactory : MonoBehaviour {

    private int QuestID;
    public GameObject questDialog;
    public GameObject questDataDialog;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    
    public void makeQuest(int id)
    {
        GameObject Temp = Instantiate(questDialog) as GameObject;
        Temp.GetComponent<questDialog>().MakeDialog(id);
        Temp.transform.SetParent(this.transform);
        Temp.GetComponent<RectTransform>().offsetMin = new Vector2(100f, 400f);
        Temp.GetComponent<RectTransform>().offsetMax = new Vector2(-100f, -400f);

    }
    public void makeEndQuest(int id)
    {
        GameObject Temp = Instantiate(questDialog) as GameObject;
        Temp.GetComponent<questDialog>().MakeCompleteDialog(GameData.QuestInProgress[id]);
        Temp.transform.SetParent(this.transform);
        Temp.GetComponent<RectTransform>().offsetMin = new Vector2(100f, 400f);
        Temp.GetComponent<RectTransform>().offsetMax = new Vector2(-100f, -400f);
    }

    public void OnClickquestDataDialogOpen()
    {
        GameObject Temp = Instantiate(questDataDialog) as GameObject;
        Temp.transform.SetParent(this.transform); 
        Temp.GetComponent<RectTransform>().offsetMin = new Vector2(160f, 60);
        Temp.GetComponent<RectTransform>().offsetMax = new Vector2(-160f, -60);
    }
    
}
