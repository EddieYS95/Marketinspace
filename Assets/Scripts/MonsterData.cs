using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterData : MonoBehaviour {

    public int id;
    private int hp;
    private float stamina;

    public Slider hpBar;
    public Slider staminaBar;
    public HuntManager huntManager;

    private bool setting;
    public bool isFight;
    private bool isDie;

    private float tmpTime;

    void OnDestory()
    {
        StopAllCoroutines();
         hpBar = null;
         staminaBar = null;
         huntManager = null;
    }

    void Awake()
    {
        isDie = false;
        tmpTime = 0;
        setting = false;
        hp = 0;
        stamina = 0;
        isFight = false;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
      
        if (hp <= 0 && isDie == false)
        {
            isDie = true;
            if(GetComponentInChildren<Animator>())
         
            if (id != -1)
            {
                huntManager.MonsterDie(id);
                GetComponentInChildren<Animator>().Play("mo" + id + "_die");

                    hpBar.value = (float)hp / GameData.MonsterList[id].hp;

                    StartCoroutine(DieCoroutine());
                }
            
          
        }
            
        else if(hp > 0 && huntManager.getPlayType() == HuntManager.HuntPlayType.Fighting)
        {
            if (setting == true && isFight == false)
            {
                stamina += (Time.deltaTime * GameData.MonsterList[id].speed);
                UpdateSlider();
            }
            if (isFight == true)
            {
                tmpTime += Time.deltaTime;
                if (tmpTime > 1)
                {
                    tmpTime = 0;
                    isFight = false;
                }
            }
        }
	}

    void UpdateSlider()
    {
        hpBar.value = (float)hp / GameData.MonsterList[id].hp;
        staminaBar.value = (float)stamina / 10;


        if (staminaBar.value == 1)
        {
            stamina = 0;
            staminaBar.value = 0;
            GetComponentInChildren<Animator>().Play("mo" + id + "_attack");

            Debug.Log(GameData.getProtect());

            int damage = GameData.MonsterList[id].att - GameData.getProtect();
            if (damage < GameData.MonsterList[id].att / 3)
            {
                damage = GameData.MonsterList[id].att / 3;
            }
            if (GameData.getProtect() > GameData.MonsterList[id].att)
            {
                int Miss = UnityEngine.Random.Range(0, 10);
                if(Miss>7)
                damage = 0;
            }
            Debug.Log(damage);
            huntManager.hited(damage);
        }
    }

    public void setMonster(int id)
    {
        setting = true;
        this.id = id;
        hp = GameData.MonsterList[id].hp;
    }

    public void hitted()
    {
        StartCoroutine(hit());
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    IEnumerator hit()
    {
        int damage;
        damage = GameData.getDamage();
        isFight = true;
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Animator>().Play("mo" + id + "_hit");
        hp -= damage;
        
        if(huntManager.GetComponent<HuntManager>().HitType==0)
        GameObject.Find("Audio").transform.FindChild("hit_sword").GetComponent<AudioSource>().Play();
        else if (huntManager.GetComponent<HuntManager>().HitType == 1)
        //huntManager.transform.FindChild("").GetComponent<AudioSource>().Play();
        Debug.Log(damage);
        huntManager.monsterName.transform.parent.parent.FindChild("Damage_m").GetComponent<Text>().text = "-" + damage;
        huntManager.monsterName.transform.parent.parent.FindChild("Damage_m").gameObject.GetComponent<CanvasGroup>().alpha = 1;

    }
}
