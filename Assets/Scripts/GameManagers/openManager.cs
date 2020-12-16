using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class openManager : MonoBehaviour {

    public Image Back;
    public Text Sub;

    public AudioSource ads;

    public Sprite o001;
    public Sprite o002;
    public Sprite o003;
    public Sprite o004;

    int index;
    bool pout;
    bool Tpout;
    bool final;
    // Use this for initialization
	void Start () {
        index = 0;
        pout = false;
        Tpout = false;
        final = false;
        Back.color = new Color(1, 1, 1, 0f);
        Sub.color = new Color(1, 1, 1, 0f);
        
        Back.sprite = o001;
        Sub.text = "2150년, 지구는 최악의 상황에 도달했다.";
    }
	
	// Update is called once per frame
	void Update () {

        if(ads.GetComponent<AudioSource>().volume !=1 && !final)
        ads.GetComponent<AudioSource>().volume += Time.deltaTime/4;

        if (pout == true)
        {
            if (final)
            {
                Back.color = Color.Lerp(Back.color, new Color(1, 1, 1, 0), Time.deltaTime);
                ads.GetComponent<AudioSource>().volume -= Time.deltaTime / 4;
            }
            else Back.color = Color.Lerp(Back.color, new Color(1, 1, 1, 0), Time.deltaTime * 3);
            
        }
        else
        {
            Back.color = Color.Lerp(Back.color, new Color(1, 1, 1, 1), Time.deltaTime * 3);
        }

        if (Tpout == true)
        {
            if (final) Sub.color = Color.Lerp(Sub.color, new Color(1, 1, 1, 0), Time.deltaTime);
            
            else { Sub.color = Color.Lerp(Sub.color, new Color(1, 1, 1, 0), Time.deltaTime * 2);
                transform.FindChild("skip").GetComponent<Image>().color = Color.Lerp(Sub.color, new Color(1, 1, 1, 0), Time.deltaTime*3);
            }

        }
        else
        {
            Sub.color = Color.Lerp(Sub.color, new Color(1, 1, 1, 1), Time.deltaTime * 2);
            transform.FindChild("skip").GetComponent<Image>().color = Color.Lerp(Sub.color, new Color(1, 1, 1, 1), Time.deltaTime*3);
        }
        

        if (Input.GetMouseButtonDown(0) && Sub.color.a >0.9f)
        {
           index++;
           StartCoroutine(movie());
        }
	}

    IEnumerator movie()
    {

        if(index==2 || index==4 || index==5 || index==7) pout = true;
        Tpout = true;
        yield return new WaitForSeconds(1f);
        if (index == 1) Sub.text = "기온은 70도에 육박해 대지가 끓어 올랐으며 사람들이 살아가기에 불가능한 지경에 올랐다.";

        else if (index == 2)
        {
            
            Back.sprite = o002;
            Sub.text = "사람들은 지구를 떠나 새로운 정착지를 찾아 떠났다.";

        }
        else if (index == 3) Sub.text = "나또한 지구를 떠나는 우주선에 타있었다.";

      
        else if (index == 4)
        {
            Back.sprite = o003;
            Sub.text = "하지만 우주선은 소행성과 충돌하여 부숴지고 나는 긴급 탈출포트에 태워져 미지의 행성으로 떨어졌다.";

        }
        else if (index == 5)
        {
            Back.sprite = o004;
            Sub.text = "내가 떨어진 행성은 오도르였고 떨어지는 충돌에 의해 정신을 잃은 나는 마르쿠스 할아버지에 의해 거둬졌다.";

        }
        else if (index == 6) Sub.text = "그리고 그날부터 할아버지의 손에서 공방일을 배우며 그곳의 생활에 적응해 나갔다.";

       
        else if (index == 7)
        {
            final = true;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("new");
        }
        Tpout = false;
        pout = false;
        
        
    }
    
}
