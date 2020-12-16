using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharMessage : MonoBehaviour {

    float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(timer == 0)
        {

            int ranNum;
            ranNum = Random.Range(0, 2);
            if (ranNum == 0)
                GameObject.Find("Audio").transform.FindChild("toy-1").GetComponent<AudioSource>().Play();
            else
                GameObject.Find("Audio").transform.FindChild("toy-2").GetComponent<AudioSource>().Play();
            
            ranNum = Random.Range(0, 5);
            if(ranNum ==0) transform.FindChild("Text").GetComponent<TextMesh>().text = "궁시렁 궁시렁";
            else if (ranNum == 1) transform.FindChild("Text").GetComponent<TextMesh>().text = "재료가 어딨더라..";
            else if (ranNum == 2) transform.FindChild("Text").GetComponent<TextMesh>().text = "하암..";
            else if (ranNum == 3) transform.FindChild("Text").GetComponent<TextMesh>().text = "방금 누가 찌른거같은데";
            else transform.FindChild("Text").GetComponent<TextMesh>().text = "다음 행성은 어디지?";
           
        }
        timer += Time.deltaTime;

        if (timer >= 2) { timer = 0; transform.FindChild("Text").GetComponent<TextMesh>().text = ""; gameObject.SetActive(false); }
	}
}
