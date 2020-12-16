using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class RunPanel : MonoBehaviour {

    struct elData
    {
        public int id;
        public int count;

        public elData(int id, int count)
        {
            this.id = id;
            this.count = count;
        }
    }

    public CanvasGroup thisGroup;
    public Text reword;

    int money;
    int exp;
    List<elData> elDatas;

    void Start()
    {
        elDatas = new List<elData>();
        money = 0;
        exp = 0;
    }
    public void SetPanel(string rewords)
    {
      
        string[] reword = rewords.Split('_');

        for (int i = 0; i < reword.Length; i++)
        {

            string[] tmp = reword[i].Split('|');
            Debug.Log(tmp.Length);
            if (tmp[0].Equals("mon"))
            {
                money += int.Parse(tmp[1]);
            }
            else if (tmp[0].Equals("exp"))
            {
                exp += int.Parse(tmp[1]);
            }
            else if (tmp[0].Equals("El"))
            {
                bool complete = false;

                for (int j = 0; j < elDatas.Count; j++)
                {
                    if (elDatas[j].id == int.Parse(tmp[1]))
                    {
                        elData tm = elDatas[j];
                        tm.count += int.Parse(tmp[2]);
                        elDatas[j] = tm;
                        complete = true;
                        break;
                    }
                }
                if (complete == false)
                {
                    elDatas.Add(new elData(int.Parse(tmp[1]), int.Parse(tmp[2])));
                }
            }
        }

        this.reword.text += money + " 브릭스 \n";
        this.reword.text += exp + " 경험치 \n";
        for (int i = 0; i < elDatas.Count; i++)
        {
            this.reword.text += GameData.ElementList[elDatas[i].id].name + " " + elDatas[i].count + " 개 \n";
        }

        thisGroup.alpha = 1;
        thisGroup.interactable = true;
        thisGroup.blocksRaycasts = true;

    }

    public void OnClickGoHome()
    {
        GameData.addMoney(money);
        GameData.upExp(exp);

        for (int i = 0; i < elDatas.Count; i++)
        {
            GameData.setElement(elDatas[i].id, elDatas[i].count);
        }
        SceneManager.LoadScene("shop");
    }

}
