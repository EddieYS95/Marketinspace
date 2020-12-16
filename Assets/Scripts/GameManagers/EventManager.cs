using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public GameObject EventPanel;

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        
        if (GameData.ingEvent != -1)
        {

            if(transform.childCount == 0)
            setEvent(GameData.ingEvent);

        }
        
    }
   
    public void setEvent(int id)
    {
        
        GameData.ingEvent = id;
        GameObject tmp = Instantiate(EventPanel) as GameObject;
        tmp.transform.SetParent(this.transform);
        tmp.transform.localScale = new Vector3(1, 1, 1);
        tmp.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        tmp.GetComponent<RectTransform>().offsetMax = new Vector2(-0, 0);
        tmp.GetComponent<EventPanel>().setEvent(id);
    }
}
