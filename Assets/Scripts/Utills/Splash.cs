using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(splash());
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    IEnumerator splash()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("opening");
    }

}
