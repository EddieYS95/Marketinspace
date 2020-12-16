using UnityEngine;
using System.Collections;

public class ShopSlider : MonoBehaviour {
    Vector3 InitPos;
    Vector3 xPos;
    Vector3 mPos;
    int Floor;
    bool UIisDown;
    bool isUpLock;
    bool isDownLock;

    bool ActiveUI;
	// Use this for initialization
	void Start () {

        ActiveUI = false;
        InitPos = this.transform.position;
	}

    // Update is called once per frame
    void Update()
    {

        UIisDown = GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().UIisDown;

        Floor = GameData.getFloor();

        if (GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isActiveUI) {
            if (GameObject.Find("ShopCanvas").GetComponent<ShopUIManager>().isBuilding) ActiveUI = false;
            else ActiveUI = true;
        }
        else {
            ActiveUI = false;
        }
        
        if (!ActiveUI)
        {
           
            if (Vector3.Distance(InitPos, this.transform.position) < 0.1f || this.transform.position.y < InitPos.y)
            {
                this.transform.position = InitPos;
                isDownLock = true;
            }
            else isDownLock = false;

            if (Floor >= 2)
            {
                if (Vector3.Distance(new Vector3(InitPos.x, 0.72f + 1.95f * (Floor - 1), InitPos.z), this.transform.position) < 0.1f || this.transform.position.y > 0.72f + 1.95f * (Floor - 1))
                {
                    this.transform.position = new Vector3(InitPos.x, 0.72f + 1.95f * (Floor - 1), InitPos.z);
                    isUpLock = true;
                }
                else isUpLock = false;
            }

            if (Input.touchCount == 1 && Floor >= 2)
            {
                mPos = Input.GetTouch(0).position;

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (UIisDown && mPos.y > Screen.height / 8)
                    {
                        float dis = (mPos.y - xPos.y) * 0.4f;


                        if (dis > 0)
                        {
                            if (!isDownLock)
                                this.transform.position = Vector3.Lerp(this.transform.position,
                                               this.transform.position - new Vector3(0, dis, 0),
                                               Time.deltaTime * 0.5f);
                        }
                        else
                        {
                            if (!isUpLock)
                                this.transform.position = Vector3.Lerp(this.transform.position,
                                              this.transform.position - new Vector3(0, dis, 0),
                                              Time.deltaTime * 0.5f);
                        }


                    }
                    else if (mPos.y > (Screen.height / 2)+90 && !UIisDown)
                    {
                        float dis = (mPos.y - xPos.y) * 0.5f;
                        if (dis > 0)
                        {
                            if (!isDownLock)
                                this.transform.position = Vector3.Lerp(this.transform.position,
                                               this.transform.position - new Vector3(0, dis, 0),
                                               Time.deltaTime * 0.5f);
                        }
                        else
                        {
                            if (!isUpLock)
                                this.transform.position = Vector3.Lerp(this.transform.position,
                                              this.transform.position - new Vector3(0, dis, 0),
                                              Time.deltaTime * 0.5f);
                        }

                    }
                }
                xPos = Input.GetTouch(0).position;
            }

        }
    }
}
