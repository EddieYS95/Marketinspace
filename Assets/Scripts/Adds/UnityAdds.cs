using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

    

public class UnityAdds : MonoBehaviour {


    public Text TimeText;
    int restTime;
    int chargeTime;
    public Button AdBtn1;
    public Button AdBtn2;

    GameObject LockPanel;
    void Start()
    {
        LockPanel = GameObject.Find("DayManager").transform.FindChild("LockPanel").gameObject;
        chargeTime = 900;
        Advertisement.Initialize("1289984",false);
    }
    void Update()
    {
       restTime =  chargeTime - (int)(GameData.GetPassedfromADTime());

        
        if (restTime > 0)
        {
            if (restTime % 60 < 10)
                TimeText.text = "남은 시간 : " + restTime / 60 + ":0" + restTime % 60;
            else TimeText.text = "남은 시간 : " + restTime / 60 + ":" + restTime % 60;
            AdBtn1.interactable = false;
        }
        else { TimeText.text = ""; AdBtn1.interactable = true; }

    }

    public void ShowAd()
    {
        Debug.Log(Advertisement.isInitialized);
      
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var Options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", Options);
            LockPanel.SetActive(true);
            if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().ActiveUI();
            LockPanel.transform.FindChild("Panel").gameObject.SetActive(false);
            // Advertisement.Show();
        }
        else
        {
            Debug.Log("erer");
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                LockPanel.transform.FindChild("Panel").gameObject.SetActive(true); if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().UnActiveUI();
                break;
            case ShowResult.Skipped:
                Debug.Log("광고스킵");
                break;
            case ShowResult.Failed:
                Debug.Log("광고실패");
                break;
        }
    }

    public void ShowAd_hunt()
    {
        Debug.Log(Advertisement.isInitialized);

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var Options = new ShowOptions { resultCallback = HandleShowResult_hunt };
            Advertisement.Show("rewardedVideo", Options);
            if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().ActiveUI();
            // Advertisement.Show();
        }
        else
        {
            Debug.Log("erer");
        }
    }
    private void HandleShowResult_hunt(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameObject.Find("ShopManager").GetComponent<ShopManager>().OnClickHunt_E(0); if (GameObject.Find("ShopCanvas")) GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().UnActiveUI();
                break;
            case ShowResult.Skipped:
                Debug.Log("광고스킵");
                break;
            case ShowResult.Failed:
                Debug.Log("광고실패");
                break;
        }
    }

    public void Reward()
    {
        GameData.SetADTimeNow();
        GameData.addEmerald(2);
        Debug.Log("Reward");
        LockPanel.SetActive(false);
    }

}
