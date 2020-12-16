using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class TutorialScript : MonoBehaviour
{
    public Text talk;
   
    GameObject Sound;
    int talkid;
    
    string scripttext = "";
    Text textUi;
    char[] pieceArr;
    string msg;

    public GameObject Star;
    public GameObject TutoReword;
    IEnumerator tempCo;

    bool finishmsg = true;
    int EventID;

    bool MainFin;
    void Update()
    {
        if (GetComponent<CanvasGroup>().alpha == 1)
        {
            if (Input.GetButtonDown("Fire") || Input.GetMouseButtonDown(0))
            {
                NextEvent();
            }
        }
    }

    public void setEvent(int eventID)
    {
        if (eventID == 0)
        {
            EventID = eventID;
            scripttext = "이제 나는 나이가 들어 공방을 계속 운영하기 어려울 것 같구나.";
        }
        else if (eventID == 6)
        {
            EventID = eventID;
            scripttext = "대충 둘러봤으니 이제 퀘스트창에 들어가 간단한 퀘스트를 하나 해보자꾸나.";
        }
        else if (eventID == 11)
        {
            EventID = eventID;
            scripttext = "이런.. 재료가 부족하구나. 재료를 얻으려면 사냥을 나가야하니 장비용 단검을 하나 만들어보거라.";
        }
        else if (eventID == 17)
        {
            EventID = eventID;
            scripttext = "장비로 전환한 아이템은 내구도가 100%일때만 다시 판매용 아이템으로 전환시킬 수 있으니 꼭 알아두렴.";
        }
        else if (eventID == 20)
        {
            EventID = eventID;
            scripttext = "이곳이 사냥터로 나가는 길목이란다. 스테이지 레벨에 따라 몬스터의 난이도가 달라지니 잘 선택해서 가도록 하려무나.";
        }
        else if (eventID == 23)
        {
            EventID = eventID;
            scripttext = "첫번째 스테이지에서는 단검의 재료인 철과 나무를 많이 얻을 수 있지. 진행률이 100%가 되면 클리어가 되지만 귀환을 통해 도중에 상점으로 돌아갈 수 도 있단다.";
        }
        else if (eventID == 24)
        {
            EventID = eventID;
            scripttext = "잔가지가 나타났구나. 가운데있는 두 동그라미가 서로 만나는 타이밍에 아래에 있는 큰 동그라미를 누르면 전투가 시작된단다.";
        }
        else if (eventID == 25)
        {
            EventID = eventID;
            scripttext = "전투가 시작되면 아래 빨간 버튼을 빠르게 연타해 노란색 스태미나 바를 채워 공격을 하면 된단다. 지금은 테스트니 그냥 넘어가도록 하지.";
        }
        else if (eventID == 27)
        {
            EventID = eventID;
            scripttext = "재료도 동일한 방법으로 타이밍을 맞추고 빨간 버튼을 연타하면 쉽게 얻을 수 있지. 다음에 직접 해보려무나";
        }
        else if (eventID == 29)
        {
            EventID = eventID;
            scripttext = "이제 재료도 얻었으니 단검 2개를 만들어보자구나.";
        }
        else if (eventID == 31)
        {
            EventID = eventID;
            scripttext = "방금 만든 단검을 한번 판매해보자꾸나. 인테리어로 가서 나무 매대를 설치해보렴.";
        }
        else if (eventID == 34)
        {
            EventID = eventID;
            scripttext = "인테리어를 설치할때는 원하는 위치에 있는 화살표 부분을 선택한 뒤 결정을 누르면 된단다.";
        }
        else if (eventID == 37)
        {
            EventID = eventID;
            scripttext = "아이템을 판매하는 첫번째 방법은 판매용 아이템을 설치할 매대를 클릭하는 거란다.";
        }
        else if (eventID == 37)
        {
            EventID = eventID;
            scripttext = "아이템을 판매하는 첫번째 방법은 판매용 아이템을 설치할 매대를 클릭하는 거란다.";
        }
        else if (eventID == 41)
        {
            EventID = eventID;
            scripttext = "손님이 들어왔구나! 손님은 명성도가 높을 수록 더 많이 들어오지. 단검을 사려는 모양이구나.";
        }
        else if (eventID == 43)
        {
            EventID = eventID;
            scripttext = "매대나 아이템이 많을 땐 인벤토리의 자동배치 기능을 이용해 쉽게 아이템들을 올릴 수 있단다. 지금은 매대가 하나지만 그래도 한번 해보렴.";
        }
        else if (eventID == 44)
        {
            EventID = eventID;
            scripttext = "자, 이제 공방 운영에 대한 설명이 다 끝났구나. 당분간 나는 고향에 내려가 쉴테니 유나와 함께 열심히 공방을 키워나가길 바란다.";
        }
        else if (eventID == 45)
        {
            EventID = eventID;
            scripttext = "아, 가기 전에 아이템을 몇개 주고갈테니 잘 보관하거라. 공방을 운영하는데 많은 도움이 될거란다.";
        }
        else if (eventID == 46)
        {
            EventID = eventID;
            scripttext = "";
        }
        else if (eventID == 99)
        {
            EventID = eventID;
            scripttext = "간단하게 운영법에 대해 설명을 해줄테니 잘 들어보렴. \n설명이 필요없다면 언제든지 스킵버튼을 눌러 나갈수 있단다.";
            MainFin = true;
        }
        tempCo = texttest();
        StartCoroutine(tempCo);

        //사진 추가하기
        talkid++;
    }

    void NextEvent()
    {


        if (finishmsg)
        {
            Sound = GameObject.Find("Audio").transform.FindChild("button-21").gameObject;
            Sound.GetComponent<AudioSource>().Play();

            if (EventID != 44)
            {

                if (EventID != 45)
                {

                    if (EventID != 0)
                    {
                        GetComponent<CanvasGroup>().alpha = 0;
                        GetComponent<CanvasGroup>().interactable = false;
                        GetComponent<CanvasGroup>().blocksRaycasts = false;
                        if (MainFin) { MainFin = false; Star.gameObject.SetActive(true); }
                    }
                    else
                    {
                        setEvent(99);
                        finishmsg = false;
                    }
                }
                else
                {
                    setEvent(46);
                    finishmsg = false;
                }

            }
            else
            {
                setEvent(45);
                finishmsg = false;
            }
            
            if (EventID == 46)
            {
                GameObject.Find("Audio").transform.FindChild("levelup").GetComponent<AudioSource>().Play();
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                TutoReword.gameObject.SetActive(true);
                TutoReword.GetComponent<Animator>().Play("PopUpAni");
                GameData.addMoney(1000);
                GameData.addEmerald(10);
                GameData.setElement(0, 10);
                GameData.setElement(1, 20);
            }
            else if(EventID == 23)
            {
                GameObject.Find("Canvas").GetComponent<TutorialManager>().OnClickTuBtn(this.gameObject);
            }
        }
        else { StopCoroutine(tempCo); talk.text = scripttext; finishmsg = true; transform.FindChild("skip").gameObject.SetActive(true); }
    }



    IEnumerator texttest()

    {
        transform.FindChild("skip").gameObject.SetActive(false);
        msg = ""; talk.text = "";
        finishmsg = false;
        pieceArr = scripttext.ToCharArray();
        for (int i = 0; i < pieceArr.Length; i++)

        {
            msg += pieceArr[i];
            talk.text = msg;
            yield return new WaitForSeconds(0.05f);
            if (i == pieceArr.Length - 1) { finishmsg = true; transform.FindChild("skip").gameObject.SetActive(true); }
        }



    }
}

