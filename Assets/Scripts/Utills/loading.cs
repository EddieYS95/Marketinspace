using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class loading : MonoBehaviour {
    bool IsDone = false;
    bool startCo = false;

    float fTime = 0f;
    public Text tip;
    public GameObject Char;
    AsyncOperation async_operation;

    public string targetName;

	// Use this for initialization
	void Start () {
        int ran = Random.Range(0, 12);
        if (ran == 0) tip.text = "Tip. 오도르에서는 나무와 꽃들도 주민입니다.";
        else if(ran ==1) tip.text = "Tip. 프라이에서는 심각한 노동착취가 일어나고 있습니다.";
        else if (ran == 2) tip.text = "Tip. 토우에서는 100년에 한번 눈이 옵니다.";
        else if (ran == 3) tip.text = "Tip. 방어력이 데미지보다 높을 경우 일정확률로 회피합니다.";
        else if (ran == 4) tip.text = "Tip. 자신보다 공격력이 강한 몬스터는 빨갛게 표시됩니다.";
        else if (ran == 5) tip.text = "Tip. 더 많은 손가락을 사용하면 빠르게 공격할 수 있습니다!";
        else if (ran == 6) tip.text = "Tip. 이드의 주민들은 물 속에서 숨을 쉴 수 있습니다.";
        else if (ran == 7) tip.text = "Tip. 세니에는 절대 꺼지지 않는 불꽃이 있습니다.";
        else if (ran == 8) tip.text = "Tip. 상점메뉴 가운데 화살표를 누르면 숨겨진 하늘이 나타납니다.";
        else if (ran == 9) tip.text = "Tip. 튜바나에서 피는 꽃의 잎은 얼음으로 되어있습니다.";
        else if (ran == 10) tip.text = "Tip. Mr.R은 특정인의 사유지입니다.";
        else tip.text = "Tip. 돈과 명성을 모아 상점을 확장해 보세요!";

        Char.transform.GetChild(ran).gameObject.SetActive(true);
       
	}
	
	// Update is called once per frame
	void Update () {
        fTime += Time.deltaTime;

        if (fTime > 1f && startCo == false)
        {
            startCo = true;

               // StartCoroutine(StartLoad(targetName));
            SceneManager.LoadScene(targetName);
        }

        if (fTime > 2f && IsDone == true)
        {
            async_operation.allowSceneActivation = true;
           
        }
	}
    IEnumerator StartLoad(string strSceneName)
    {
        async_operation = SceneManager.LoadSceneAsync(strSceneName);
       // Debug.Log("씬 : " + SceneManager.GetSceneAt(1).name);
       // async_operation.allowSceneActivation = false;

        while (IsDone == false)
        {
            if (async_operation.progress >= 0.9f)
            {
                IsDone = true;
                yield return (!IsDone);
               
            }
            yield return (!IsDone);
        }


    }
}
