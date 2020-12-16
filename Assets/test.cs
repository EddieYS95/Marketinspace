using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0) 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("a");
                SceneManager.LoadScene("test");
            }
        }
	}
}
