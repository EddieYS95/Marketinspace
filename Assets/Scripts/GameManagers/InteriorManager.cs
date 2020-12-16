using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteriorManager : MonoBehaviour {

    GameObject Interior;
    GameObject Coin;
    GameObject Star;
    int InteriotType; // 0=stand 1=wall 2=door 3=counter
    int SelectedItem;

    public GameObject Interiorlist;
    public GameObject InItem;
    // Use this for initialization
    void Start () {
        Interior = transform.FindChild("Build").gameObject;
        Coin = Interior.transform.FindChild("Coin").gameObject;
        Star = Interior.transform.FindChild("Star").gameObject;
        InteriotType = 0;
        SelectedItem = -1;
        setInteriorList();
    }
	
	// Update is called once per frame
	void Update () {

        if (gameObject.activeInHierarchy)
        {
            if (InteriotType == 0)
            {
                Interiorlist.GetComponent<RectTransform>().sizeDelta = new Vector2(200 * GameData.InteriorList.Count + 10, 200);
                Interiorlist.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 220);
                Interiorlist.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, 0);
            }
            else if (InteriotType == 1)
            {
                Interiorlist.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * GameData.InteriorList.Count + 20 * (GameData.InteriorList.Count - 1) + 10, 200);
                Interiorlist.GetComponent<GridLayoutGroup>().cellSize = new Vector2(220, 220);
                Interiorlist.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 0);
            }
            else if (InteriotType == 2)
            {
                Interiorlist.GetComponent<RectTransform>().sizeDelta = new Vector2(200 * GameData.InteriorList.Count + 10, 200);
                Interiorlist.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 220);
                Interiorlist.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, 0);
            }
            else if (InteriotType == 3)
            {
                Interiorlist.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * GameData.InteriorList.Count + 30 * (GameData.InteriorList.Count - 1) + 10, 200);
                Interiorlist.GetComponent<GridLayoutGroup>().cellSize = new Vector2(220, 220);
                Interiorlist.GetComponent<GridLayoutGroup>().spacing = new Vector2(30, 0);
            }
        }
    }

    public void OnClickMenu(Button selected)
    {
        InteriotType = int.Parse(selected.name);
        setInteriorList();
        Interior.transform.parent.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "건축";
    }

    void setInteriorList()
    {
        Interior.transform.FindChild("build_content").transform.FindChild("name").GetComponent<Text>().text
              = "";
        Interior.transform.FindChild("build_content").transform.FindChild("content").GetComponent<Text>().text
        = "";
        Coin.SetActive(false);
        Coin.transform.FindChild("Text").GetComponent<Text>().text = "";

        Star.SetActive(false);
        Star.transform.FindChild("Text").GetComponent<Text>().text = "";

        for (int i = 0; i < Interiorlist.transform.childCount; i++)
        {
           Destroy(Interiorlist.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < GameData.InteriorList.Count; i++)
        {
            GameObject Iitem;
            Iitem = Instantiate(InItem, InItem.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            Iitem.name = "" + i;
            Iitem.transform.SetParent(Interiorlist.transform);
            Iitem.transform.localScale = new Vector3(1, 1, 1);
            Iitem.GetComponent<InteriorItem>().type = InteriotType;
            if (InteriotType == 0) Iitem.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Shop/Stand")[i];
            else if(InteriotType == 1) Iitem.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Shop/Wall")[i];
            else if (InteriotType == 2) Iitem.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Shop/Door")[i];
            else if (InteriotType == 3) Iitem.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Shop/Counter")[i];

        }
    }
}
