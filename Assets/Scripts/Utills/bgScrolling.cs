using UnityEngine;
using System.Collections;

public class bgScrolling : MonoBehaviour {

    public float scroll_speed;
    private bool OnScroll;

    void Awake()
    {
        OnScroll = true;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (true == OnScroll)
        {
            Vector2 tmpOffset = this.GetComponent<MeshRenderer>().material.mainTextureOffset;
            this.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(tmpOffset.x + (scroll_speed * Time.deltaTime), tmpOffset.y);
        }
    }

    public void setOnScroll(bool onScroll)
    {
        OnScroll = onScroll;
    }
}
