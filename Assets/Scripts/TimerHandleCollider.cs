using UnityEngine;
using System.Collections;

public class TimerHandleCollider : MonoBehaviour {

    private HuntManager huntManager;

    void Awake()
    {
        huntManager = GameObject.Find("HuntManager").GetComponent<HuntManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Equals("Target") || col.name.Equals("Target2"))
        {
            huntManager.SetOnTiming(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name.Equals("Target") || col.name.Equals("Target2"))
        {
            huntManager.SetOnTiming(false);
        }
    }

}
