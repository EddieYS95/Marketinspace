using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ConsolTest : MonoBehaviour {

    public Text field;
    public Button enter;
    public Text text;
    public CanvasGroup group;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (group.alpha == 0)
            {
                group.alpha = 1;
                group.interactable = true;
                group.blocksRaycasts = true;
            }
            else
            {
                group.alpha = 0;
                group.interactable = false;
                group.blocksRaycasts = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
        }
	}

    public void Enter()
    {
        string EnterText = field.text;

        text.text += "\n" + EnterText;
        if (EnterText.StartsWith("setItem"))
        {
            try
            {
                string[] _EnterText = EnterText.Split('(');
                _EnterText[1] = _EnterText[1].Remove(_EnterText[1].Length-1);
                int id = int.Parse(_EnterText[1]);
                GameData.setItem(id);
            }
            catch (Exception e)
            {
                text.text += "\n" + "다시 입력";
            }
        }
        else if (EnterText.StartsWith("setElement"))
        {
            try
            {
               string[] _EnterText  = EnterText.Split('(')[1].Split(',');
               _EnterText[1] = _EnterText[1].Remove(_EnterText[1].Length - 1);
               text.text += "\n" + _EnterText[0] + " "+_EnterText[1];
               int id = int.Parse(_EnterText[0]);
               int Count = int.Parse(_EnterText[1]);
               GameData.setElement(id, Count);
            }
            catch (Exception e)
            {
                text.text += "\n" + "다시 입력";
            }
        }
        else if (EnterText.StartsWith("upExp"))
        {
            try
            {
                string[] _EnterText = EnterText.Split('(');
                _EnterText[1] = _EnterText[1].Remove(_EnterText[1].Length - 1);
                int id = int.Parse(_EnterText[1]);
                GameData.upExp(id);
                text.text += "\n" + "Level : " + GameData.getLevel() + " Exp : " + GameData.getExp();
            }
            catch (Exception e)
            {
                text.text += "\n" + "다시 입력";
            }
        }
        else if (EnterText.Equals("getInvenItemData"))
        {
            GameData.getInvenItemData();
        }
        else if (EnterText.Equals("getInvenElementData"))
        {
            GameData.getInvenElementData();
        }
        else if (EnterText.Equals("nowTime"))
        {
            long time = System.DateTime.Now.Ticks;
            text.text += "\n" + time;
        }

        if (EnterText.StartsWith("setQuest"))
        {
            try
            {
                string[] _EnterText = EnterText.Split('(');
                _EnterText[1] = _EnterText[1].Remove(_EnterText[1].Length - 1);
                int id = int.Parse(_EnterText[1]);
                GameObject.Find("QuestCanvas").GetComponent<QuestFactory>().makeQuest(id);
            }
            catch (Exception e)
            {
                text.text += "\n" + "다시 입력";
            }
        }
        if (EnterText.StartsWith("setEvent"))
        {
            try
            {
                string[] _EnterText = EnterText.Split('(');
                _EnterText[1] = _EnterText[1].Remove(_EnterText[1].Length - 1);
                int id = int.Parse(_EnterText[1]);
                GameData.setEvent(id);
            }
            catch (Exception e)
            {
                text.text += "\n" + "다시 입력";
            }
        }
        field.text = " ";
    }



}
