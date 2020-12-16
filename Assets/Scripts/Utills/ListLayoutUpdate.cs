using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ListLayoutUpdate : MonoBehaviour {

    public GameObject Node;

    float size;

	// Use this for initialization
	void Start () {
        size = Node.GetComponent<LayoutElement>().preferredHeight;
	}
	
	// Update is called once per frame
	void Update () {

        
            int ContentCount = transform.childCount;

            if (this.name == "InventoryContent" || this.name == "ElementContent")
            {
                int xtra =12;
                if (ContentCount % 3.06f == 0) xtra = 0;
                GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x,((ContentCount / 3.06f) + xtra) * size);
            }
            else if(this.name == "getItem"){
                 GetComponent<RectTransform>().sizeDelta = new Vector2(ContentCount * 90, GetComponent<RectTransform>().sizeDelta.y);
            }
            else if (this.name == "MakeBookContent" || this.name =="EleBookContent")
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, ((ContentCount/5f)+1) * (size+20));
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, ContentCount*size);
            }
          
       
	}
}
