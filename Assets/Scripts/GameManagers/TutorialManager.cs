using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public GameObject Shop;

    public GameObject Menu;
    public GameObject Hunt;

    public GameObject ScriptPanel;

    public GameObject ExitPanel;
    int TuNum;
	// Use this for initialization
	void Start () {
        TuNum = 0;
        OnScript();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(true);
        }
    }

    public void OnClickOut()
    {

        GameObject.Find("Audio").transform.FindChild("shop").gameObject.SetActive(false);
        if (GameObject.Find("Main Camera"))
        {
            StopAllCoroutines();
            GameObject load = GameObject.Find("Loading").transform.FindChild("LoadingScene").gameObject;
            GameObject.Find("Main Camera").SetActive(false);
            load.SetActive(true);
            load.transform.FindChild("Loading").GetComponent<loading>().targetName = "shop";
        }
    }

    public void OnClickTuBtn(GameObject Btn)
    {
        TuNum++;
        GameObject.Find("Audio").transform.FindChild("button-19").GetComponent<AudioSource>().Play();
        Debug.Log(TuNum);
        if (TuNum == 1)
        {
            Shop.transform.FindChild("Interier").gameObject.SetActive(true);
            Menu.transform.FindChild("Interier").gameObject.SetActive(true);
            Btn.SetActive(false);
            Btn.transform.parent.FindChild("ForgeBtn").gameObject.SetActive(true);
            
        }
        else if (TuNum == 2)
        {
            Shop.transform.FindChild("Interier").gameObject.SetActive(false);
            Menu.transform.FindChild("Interier").gameObject.SetActive(false);
            Menu.transform.FindChild("Forge").gameObject.SetActive(true);
            Btn.SetActive(false);
            Btn.transform.parent.FindChild("InvenBtn").gameObject.SetActive(true);
        }
        else if (TuNum == 3)
        {
            Shop.transform.FindChild("Interier").gameObject.SetActive(false);
            Shop.transform.FindChild("Inven").gameObject.SetActive(true);
            Menu.transform.FindChild("Forge").gameObject.SetActive(false);
            Menu.transform.FindChild("Inven").gameObject.SetActive(true);
            Btn.SetActive(false);
            Btn.transform.parent.FindChild("QuestBtn").gameObject.SetActive(true);
        }
        else if (TuNum == 4)
        {
            Shop.transform.FindChild("Inven").gameObject.SetActive(false);
            Shop.transform.FindChild("Quest").gameObject.SetActive(true);
            Menu.transform.FindChild("Inven").gameObject.SetActive(false);
            Menu.transform.FindChild("Quest").gameObject.SetActive(true);
            Btn.SetActive(false);
            Btn.transform.parent.FindChild("VillBtn").gameObject.SetActive(true);
        }
        else if (TuNum == 5)
        {
            Shop.transform.FindChild("Quest").gameObject.SetActive(false);
            Shop.transform.FindChild("Vill").gameObject.SetActive(true);
            Menu.transform.FindChild("Quest").gameObject.SetActive(false);
            Menu.transform.FindChild("Vill").gameObject.SetActive(true);
            
        }
        else if (TuNum == 6)
        {
            OnScript();
            Menu.transform.FindChild("Vill").gameObject.SetActive(false);
            Btn.transform.parent.FindChild("VillBtn").gameObject.SetActive(false);
            Shop.transform.FindChild("QuestChk").gameObject.SetActive(true);
        }
        else if (TuNum == 7)
        {
            Shop.transform.FindChild("Vill").gameObject.SetActive(false);
            Shop.transform.FindChild("QuestChk").gameObject.SetActive(false);
            Shop.transform.FindChild("Quest").gameObject.SetActive(true);
            Shop.transform.FindChild("Quest0").gameObject.SetActive(true);
        }
        else if (TuNum == 8)
        {
            Shop.transform.FindChild("Quest0_1").gameObject.SetActive(true);
        }
        else if (TuNum == 9)
        {

            Shop.transform.FindChild("Quest0_1").gameObject.SetActive(false);
            Shop.transform.FindChild("Quest0").gameObject.SetActive(false);
            Shop.transform.FindChild("Quest").gameObject.SetActive(false);
            Menu.transform.FindChild("Item").gameObject.SetActive(true);

        }
        else if (TuNum == 10)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Make").gameObject.SetActive(true);
            Menu.transform.FindChild("Plus").gameObject.SetActive(true);
        }
        else if (TuNum == 11)
        {
            OnScript();
            Btn.SetActive(false);
            Shop.transform.FindChild("Make1").gameObject.SetActive(true);
            Menu.transform.FindChild("Minus").gameObject.SetActive(true);
        }
        else if (TuNum == 12)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Make1").gameObject.SetActive(false);
            Menu.transform.FindChild("MakeBtn").gameObject.SetActive(true);
        }
        else if (TuNum == 13)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Make").gameObject.SetActive(false);

            GameObject.Find("Audio").transform.FindChild("forge").GetComponent<AudioSource>().Play();
            Shop.transform.FindChild("Making").gameObject.SetActive(true);
        }
        else if (TuNum == 14)
        {
            Btn.SetActive(false);

            GameObject.Find("Audio").transform.FindChild("complete").GetComponent<AudioSource>().Play();
            Shop.transform.FindChild("Inven").gameObject.SetActive(true);
            Shop.transform.FindChild("Item1").gameObject.SetActive(true);
            Menu.transform.FindChild("Item1").gameObject.SetActive(true);

        }
        else if (TuNum == 15)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("ItemContent").gameObject.SetActive(true);
        }
        else if (TuNum == 16)
        {
            Shop.transform.FindChild("EquipBt").gameObject.SetActive(true);
        }
        else if (TuNum == 17)
        {

            Btn.SetActive(false);
            Shop.transform.FindChild("Item1").gameObject.SetActive(false);

            GameObject.Find("Audio").transform.FindChild("crash").GetComponent<AudioSource>().Play();
            Shop.transform.FindChild("ItemContent").gameObject.SetActive(false);
            Shop.transform.FindChild("Equipment").gameObject.SetActive(true);
            Menu.transform.FindChild("VillBtn").gameObject.SetActive(true);

            OnScript();
        }
        else if (TuNum == 18)
        {
            Btn.SetActive(false);

            Shop.transform.FindChild("Equipment").gameObject.SetActive(false);
            Menu.transform.FindChild("Hunt").gameObject.SetActive(true);
            Shop.transform.FindChild("Inven").gameObject.SetActive(false);
            Shop.transform.FindChild("Vill").gameObject.SetActive(true);
        }
        else if (TuNum == 19)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("GoHunt").gameObject.SetActive(true);

        }
        else if (TuNum == 20)
        {

            GameObject.Find("Audio").transform.FindChild("shop").gameObject.SetActive(false);
            GameObject.Find("Audio").transform.FindChild("space").gameObject.SetActive(true);
            Btn.SetActive(false);
            Shop.SetActive(false);
            Hunt.SetActive(true);
            Menu.transform.FindChild("Stage").gameObject.SetActive(true);
            OnScript();
            //Equip으로 넘어감
        }
        else if (TuNum == 21)
        {
            Btn.SetActive(false);
            Hunt.transform.FindChild("Stage").gameObject.SetActive(true);
            Menu.transform.FindChild("Equipped").gameObject.SetActive(true);
        }
        else if (TuNum == 22)
        {
            Btn.SetActive(false);
            Hunt.transform.FindChild("Item").gameObject.SetActive(true);
            Menu.transform.FindChild("Go").gameObject.SetActive(true);
        }
        else if (TuNum == 23)
        {
            Btn.SetActive(false);
            Hunt.transform.FindChild("Stage").gameObject.SetActive(false);
            Hunt.transform.FindChild("Hunt").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("space").gameObject.SetActive(false);
            GameObject.Find("Audio").transform.FindChild("0").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 24)
        {
            Hunt.transform.FindChild("Mo").gameObject.SetActive(true);
            Hunt.transform.FindChild("Tim").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("popup").GetComponent<AudioSource>().Play(); OnScript();
        }
        else if (TuNum == 25)
        {
            Btn.SetActive(false);
            Hunt.transform.FindChild("Click").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 26)
        {
            Btn.SetActive(false);

            Hunt.transform.FindChild("Die").gameObject.SetActive(true);
            Menu.transform.FindChild("Drop").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("get").GetComponent<AudioSource>().Play();
        }
        else if (TuNum == 27)
        {
            Btn.SetActive(false);

            Hunt.transform.FindChild("Die").gameObject.SetActive(false);
            Hunt.transform.FindChild("Mo").gameObject.SetActive(false);
            Hunt.transform.FindChild("Ele").gameObject.SetActive(true);
            Hunt.transform.FindChild("Tim").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 28)
        {
            Btn.SetActive(false);
            Hunt.transform.FindChild("Ele").gameObject.SetActive(false);
            Hunt.transform.FindChild("Get").gameObject.SetActive(true);
            Menu.transform.FindChild("Drop2").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("get").GetComponent<AudioSource>().Play();
        }
        else if (TuNum == 29)
        {
            GameObject.Find("Audio").transform.FindChild("shop").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("0").gameObject.SetActive(false);
            Btn.SetActive(false);
            Hunt.SetActive(false);
            Shop.SetActive(true);

            Shop.transform.FindChild("Vill").gameObject.SetActive(false);
            Shop.transform.FindChild("Make").gameObject.SetActive(true);
            Shop.transform.FindChild("Make2").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 30)
        {
            Btn.SetActive(false);

            Shop.transform.FindChild("Make").gameObject.SetActive(false);
            Shop.transform.FindChild("Make2").gameObject.SetActive(false);
            Shop.transform.FindChild("Making2").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("forge").GetComponent<AudioSource>().Play();
        }
        else if (TuNum == 31)
        {
            Btn.SetActive(false);
            Menu.transform.FindChild("IntBtn").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("complete").GetComponent<AudioSource>().Play(); OnScript();
        }
        else if (TuNum == 32)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Interier").gameObject.SetActive(true);
            Menu.transform.FindChild("Wood").gameObject.SetActive(true);
        }
        else if (TuNum == 33)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("InteContent").gameObject.SetActive(true);
        }
        else if (TuNum == 34)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Building").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 35)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Building2").gameObject.SetActive(true);
        }
        else if (TuNum == 36)
        {
            Shop.transform.FindChild("Building3").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("Build").GetComponent<AudioSource>().Play();
        }
        else if (TuNum == 37)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Stand").gameObject.SetActive(true);
            Shop.transform.FindChild("Building2").gameObject.SetActive(false);
            Shop.transform.FindChild("Building").gameObject.SetActive(false);
            Menu.transform.FindChild("Sta").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 38)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("OnStand").gameObject.SetActive(true);
        }
        else if (TuNum == 39)
        {
            Btn.SetActive(false);
            Btn.transform.parent.FindChild("Sale").gameObject.SetActive(true);
        }
        else if (TuNum == 40)
        {
            Btn.SetActive(false);
            Btn.transform.parent.parent.FindChild("Decide").gameObject.SetActive(true);
        }
        else if (TuNum == 41)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Guest").gameObject.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("shop_bell").GetComponent<AudioSource>().Play(); OnScript();
        }
        else if (TuNum == 42)
        {
            Btn.SetActive(false);
            GameObject.Find("Audio").transform.FindChild("Coin_sound").GetComponent<AudioSource>().Play();
            Shop.transform.FindChild("Guest2").gameObject.SetActive(true);
        }
        else if (TuNum == 43)
        {
            Btn.SetActive(false);
            Shop.transform.FindChild("Inven").gameObject.SetActive(true);
            Shop.transform.FindChild("Item1").gameObject.SetActive(true);
            Menu.transform.FindChild("Auto").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 44)
        {
            Btn.SetActive(false);
            GameObject.Find("Audio").transform.FindChild("crash").GetComponent<AudioSource>().Play();
            Shop.transform.FindChild("Item1").gameObject.SetActive(false);
            Shop.transform.FindChild("StandItem").gameObject.SetActive(true); OnScript();
        }
        else if (TuNum == 45)
        {
            Btn.SetActive(false);
        }
        else if (TuNum == 46)
        {
            Btn.SetActive(false);
        }
        else if (TuNum == 47)
        {
            Btn.SetActive(false);
        }
        else if (TuNum == 48)
        {
            Btn.SetActive(false);
        }
    }

    public void OnScript()
    {

        ScriptPanel.GetComponent<CanvasGroup>().alpha = 1;
        ScriptPanel.GetComponent<CanvasGroup>().interactable = true;
        ScriptPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ScriptPanel.GetComponent<TutorialScript>().setEvent(TuNum);
    }
}
