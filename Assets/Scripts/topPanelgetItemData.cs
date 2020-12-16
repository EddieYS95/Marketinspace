using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class topPanelgetItemData : MonoBehaviour {

    public int id;
    public int count;

    public Text countT;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (count != 0) countT.text = "x" + count;
        
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[id].id);
	}
}
