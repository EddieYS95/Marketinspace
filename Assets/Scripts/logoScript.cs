using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class logoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Next());
	}
	
	// Update is called once per frame
	void Update () {

    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(1);
    }
}
