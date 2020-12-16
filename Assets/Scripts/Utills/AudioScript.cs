using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioScript : MonoBehaviour {

    public GameObject SettingP;
    // Use this for initialization
    void Start () {
        SetUpVolume();

    }
	
	// Update is called once per frame
	void Update () {
        if (SettingP)
        {
            if (SettingP.activeInHierarchy)
            {
                SettingP.transform.FindChild("Panel").FindChild("Menu").FindChild("SliderB").GetComponent<Slider>().value = GameData.BackSoundV;
                SettingP.transform.FindChild("Panel").FindChild("Menu").FindChild("SliderE").GetComponent<Slider>().value = GameData.EffectSoundV;
            }
        }

    }

    public void SetUpVolume()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "BackSound") transform.GetChild(i).GetComponent<AudioSource>().volume = GameData.BackSoundV;
            else transform.GetChild(i).GetComponent<AudioSource>().volume = GameData.EffectSoundV;
        }

        if (GameObject.Find("HuntManager"))
        {
            for (int j = 0; j < GameObject.Find("Character").transform.FindChild("Audio").childCount; j++)
            {
                GameObject.Find("Character").transform.FindChild("Audio").GetChild(j).GetComponent<AudioSource>().volume = GameData.EffectSoundV;
            }
        }
    }
    public void OnAudioSlider(Slider Sl)
    {
        if (Sl.name == "SliderB") GameData.BackSoundV = Sl.value;
        else if(Sl.name =="SliderE") { GameData.EffectSoundV = Sl.value;
            transform.FindChild("pop").GetComponent<AudioSource>().Play();
        }

        SetUpVolume();
    }
}
