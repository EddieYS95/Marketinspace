using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayManager : MonoBehaviour {

    Text Times;
    Text Day;
    GameObject heart_h;

    int ChargeTime;
    int PassedTime;
    int RestTime;

    int SpaceNum;

    public bool ADD;
	// Use this for initialization
	void Start () {
        Day = transform.FindChild("Day").FindChild("Text").GetComponent<Text>();
        heart_h = transform.FindChild("Heart_h").gameObject;
        Times = heart_h.transform.FindChild("Times").FindChild("Text").GetComponent<Text>();
        
        ChargeTime = 900;
        PassedTime = 0;
        RestTime = ChargeTime;
    }
	
	// Update is called once per frame
	void Update () {
        

        SpaceNum = GameData.getSpaceOpen();
        transform.FindChild("PlanetAni").FindChild("Image").GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("UI/Planets")[SpaceNum];
        if (SpaceNum == 0) transform.FindChild("PlanetAni").FindChild("Text").GetComponent<Text>().text = "새로운 행성 오도르!";
        else if (SpaceNum == 1) transform.FindChild("PlanetAni").FindChild("Text").GetComponent<Text>().text = "새로운 행성 토우!";
        else if (SpaceNum == 2) transform.FindChild("PlanetAni").FindChild("Text").GetComponent<Text>().text = "새로운 행성 프라이!";
        else if (SpaceNum == 3) transform.FindChild("PlanetAni").FindChild("Text").GetComponent<Text>().text = "새로운 행성 튜바나!";
        else if (SpaceNum == 4) transform.FindChild("PlanetAni").FindChild("Text").GetComponent<Text>().text = "새로운 행성 이드!";
        else if (SpaceNum == 5) transform.FindChild("PlanetAni").FindChild("Text").GetComponent<Text>().text = "새로운 행성 세니!";
        else if (SpaceNum == 6) transform.FindChild("PlanetAni").FindChild("Text").GetComponent<Text>().text = "새로운 행성 Mr.R!";

        for (int i = 0; i < 5; i++)
            {
                if (i < GameData.getHeart_p())
                    heart_h.transform.FindChild("Heart_list").GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                else
                    heart_h.transform.FindChild("Heart_list").GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            }


        if (GameData.getHeart_p() >= 5)
        {
            Times.text = "MAX";

        }
        else
        {
            PassedTime = (int)GameData.GetPassedfromHTime();
            RestTime = ChargeTime - PassedTime;

            if (RestTime % 60 < 10)
                Times.text = RestTime / 60 + ":0" + RestTime % 60;
            else Times.text = RestTime / 60 + ":" + RestTime % 60;


            if (RestTime <= 0)
            {
                GameData.addHeart_p((int)((PassedTime + 0.1f) / ChargeTime));
            }


        }
        
        
        Day.text = "DAY "+GameData.GetDay();
	}
    

    public void SpaceOpen()
    {
        GameData.OpenSpace(++SpaceNum);
    }
}
