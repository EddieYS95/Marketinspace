using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System;
public class CharUIManager : MonoBehaviour {



    private GameObject Name;

    public GameObject InputName;
    public GameObject SelectBt;


    // Use this for initialization

    void Start () {

        Name = GameObject.Find("Select_name");

      

    }
    

 

    public void SelectName(GameObject Input)
    {
        if (Input.GetComponent<InputField>().text!="")
        {
            StartCoroutine(Registration(Input.GetComponent<InputField>().text));
        }
       
    }

    public void OnClickCancel()
    {
        Name.GetComponent<CanvasGroup>().alpha = 0;
        Name.GetComponent<CanvasGroup>().interactable = false;
        Name.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (InputName.transform.FindChild("Text").GetComponent<Text>().text == "") SelectBt.GetComponent<Button>().interactable = false;
        else SelectBt.GetComponent<Button>().interactable = true;

    }

    IEnumerator Registration(string userName)
    {
        Debug.Log(userName);
        string escName = WWW.EscapeURL(userName);


        WWW www = new WWW("http://abricks.kr/mis_server.php?Mode=registration&nickname=" + escName);
        yield return www;

        if (string.IsNullOrEmpty(www.text) == false)
        {
            if (www.text.StartsWith("ERR0"))
            {
                GameObject.Find("Overlapped").GetComponent<CanvasGroup>().alpha = 1;
                GameObject.Find("Overlapped").GetComponent<CanvasGroup>().interactable = true;
                GameObject.Find("Overlapped").GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                GameObject.Find("Audio").transform.FindChild("new").gameObject.SetActive(false);
                GameData.newGame(userName);
                if (GameObject.Find("Main Camera"))
                {
                    StopAllCoroutines();
                    GameObject load = GameObject.Find("Loading").transform.FindChild("LoadingScene").gameObject;
                    GameObject.Find("Main Camera").SetActive(false);
                    load.SetActive(true);
                    load.transform.FindChild("Loading").GetComponent<loading>().targetName = "tutorial";
                }
            }
        }
        else
        {

        }
    }
}
