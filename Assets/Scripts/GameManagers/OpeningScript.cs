using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningScript : MonoBehaviour {

    public Image Fade;

    bool fade;

	// Use this for initialization
	void Start () {
        fade = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1080, 1920, true);
    }

// Update is called once per frame
void Update () {
        if (fade == true)
        {
            Fade.color = Color.Lerp(Fade.color, new Color(0, 0, 0, 1), Time.deltaTime * 3);
        }

        if (Input.GetMouseButtonDown(0)||Input.touchCount>0)
        {
            if (GameData.OnGame) OnClickLoad();
            else OnClickNew();
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("다음씬!");
                if (GameData.OnGame) OnClickLoad();
                else OnClickNew();
            }
        }

	}
    public void OnClickNew()
    {
        fade = true;
        Fade.gameObject.SetActive(true);
        StartCoroutine(inFade());
    }
    public void OnClickLoad()
    {
        fade = true;
        Fade.gameObject.SetActive(true);
        StartCoroutine(inFadeLoad());
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    IEnumerator inFade()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("open");
    }
    IEnumerator inFadeLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("shop");
    }
}
