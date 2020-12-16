using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{

    GameObject Char;
 
    bool isJump;
    [HideInInspector]
    public bool isHit;

    // Use this for initialization
    void Start()
    {

        Char = this.gameObject;
        isJump = false;
        isHit = false;
        Camera.main.transform.position = new Vector3(Char.transform.position.x + 2, Char.transform.position.y + 2, -10);
    }

    // Update is called once per frame
    void Update()
    {

        KeyProc();

    }

    void KeyProc()
    {


        if (Input.GetButton("Horizontal"))
        {
            Char.transform.position = new Vector3(Char.transform.position.x + Input.GetAxis("Horizontal") * 0.08f, Char.transform.position.y, Char.transform.position.z);
            if (Input.GetAxis("Horizontal") < 0) Char.transform.localScale = new Vector3(-0.9f, 0.9f, 1);
            else Char.transform.localScale = new Vector3(0.9f, 0.9f, 1);

            if(!isJump)
            this.GetComponent<Animator>().SetBool("walk",true);

        }
        else
        {
            this.GetComponent<Animator>().SetBool("walk", false);
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {

            Char.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 1, 0) * 450.0f);
            isJump = true;
            this.GetComponent<Animator>().SetBool("jump", true);

        }
        if (Input.GetButtonDown("Pick") && !isJump)
        {
           
            Char.GetComponent<Animator>().SetBool("pick", true);
        }



        Camera.main.transform.position = new Vector3(Char.transform.position.x + 2, Char.transform.position.y + 2, -10);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Enemy") if (isHit) Destroy(col.gameObject);

        if (col.gameObject.tag == "Floor") { isJump = false; this.GetComponent<Animator>().SetBool("jump", false); }
        }

    IEnumerator AnimationTimer()
    {
        yield return new WaitForSeconds(0.3f);
        isHit = false;
    }



}
