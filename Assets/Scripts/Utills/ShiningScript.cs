using UnityEngine;
using System.Collections;

public class ShiningScript : MonoBehaviour {
    public byte shining_speed;
    public float shining_degree;
    private bool OnShining;
    private bool PlusDir;
    bool isUp;
    bool isLeft;

    // Use this for initialization
    void Awake () {
        PlusDir = false;
        OnShining = true;
    }
    void Start()
    {
        int ranNum = UnityEngine.Random.Range(0, 4);
        if (ranNum == 0) { isUp = true; isLeft = true; }
        else if (ranNum == 1) { isUp = true; isLeft = false; }
        else if (ranNum == 2) { isUp = false; isLeft = true; }
        else if (ranNum == 3) { isUp = false; isLeft = false; }
    }


    // Update is called once per frame
    void Update () {
        if (true == OnShining)
        {
            if (PlusDir)  GetComponent<SpriteRenderer>().color += new Color32(0, 0, 0, shining_speed); 
            else GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, shining_speed);

            if (GetComponent<SpriteRenderer>().color.a == 1) PlusDir = false;
            if (GetComponent<SpriteRenderer>().color.a < shining_degree) PlusDir = true;

          
                int ranNum = UnityEngine.Random.Range(0, 4);
                if (ranNum == 0) transform.position -= new Vector3(0, 0.0015f, 0);
                else if (ranNum == 1) transform.position += new Vector3(0f, 0.0015f, 0);
                else if (ranNum == 2) transform.position -= new Vector3(0.001f, 0, 0);
                else if (ranNum == 3) transform.position += new Vector3(0.001f, 0, 0);
          
        }
    }
}
