using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HuntManager : MonoBehaviour {

    public enum HuntPlayType
    {
        Noting, Fighting, Coming, Waiting, Die, Hitted
    }
    public int ID;

    //UI모음
    public Slider StaminaBar;
    public Slider HpBar;
    public Slider Minimap;
    public CanvasGroup TPanel;
    public CanvasGroup TimingPanel;
    public Scrollbar TimingScroll;
    public GameObject TimingTarget;
    public GameObject getItemNode;
    public GameObject getRewordNode;
    public CanvasGroup DiePanel;
    public CanvasGroup ClearPanel;
    public CanvasGroup BackPanel;
    public GameObject Equipped;

    public Button BackHomeBt;

    
    private HuntPlayType playType;
    private GameObject Player;
    private GameObject TargetObject;
    private bool OnTiming;
    private float time;
    public GameObject ClickImg;
    private GameObject Stage;
    private float Percent;
    private float mapSpeed;
    public GameObject Rewordlist;
    public Text monsterName;
    public GameObject Weapon;
    public GameObject Gun;
    private int AllCoin;
    private int ClearBonus;
    bool AllClear;

    public int HitType; //0 = weapon, 1 = gun



    private bool Pause;
    private bool isAuto;
    bool TimePlus;


    bool isRandom;
    int MinusPercent; // 내구도 깎이는 값



    void OnDestory()
    {
      StaminaBar = null;
      HpBar = null;
      Minimap = null;
      TPanel = null;
      TimingPanel = null;
      TimingScroll = null;
      TimingTarget = null;
      getItemNode = null;
      getRewordNode = null;
      DiePanel = null;
      ClearPanel = null;
      BackPanel = null;
      BackHomeBt = null;
      Player = null;
      TargetObject = null;
      ClickImg = null;
      Stage = null;
      Rewordlist = null;
      monsterName = null;
      Weapon = null;
      Gun = null;
    }

    public HuntPlayType getPlayType() {
        return playType;
    }

    void Awake()
    {

        Debug.Log("awake");
        if (GameData.OnGame == false) GameData.LoadGame();
            playType = HuntPlayType.Noting;
            OnTiming = false;
            Pause = false;
            time = 1;
            ID = GameData.StageNum;
            Percent = 0;
            //time
            mapSpeed = 0.03f;
            AllCoin = 0;
            MinusPercent = 20;
            if (ID > 90) isRandom = true;
            else isRandom = false;
            
            if (isRandom)
        {
            ClearPanel.transform.FindChild("Panel").FindChild("OKButton").gameObject.SetActive(false);
            ClearPanel.transform.FindChild("Panel").FindChild("RandomOK").gameObject.SetActive(true);
            ClickImg.transform.parent.FindChild("Random").gameObject.SetActive(true);
            if (ID == 91) ID = UnityEngine.Random.Range(11, 16);
                else if (ID == 92) ID = UnityEngine.Random.Range(21, 26);
                else if (ID == 93) ID = UnityEngine.Random.Range(31, 36);
                else if (ID == 94) ID = UnityEngine.Random.Range(41, 46);
                else if (ID == 95) ID = UnityEngine.Random.Range(51, 56);
                else if (ID == 96) ID = UnityEngine.Random.Range(61, 66);
                else if (ID == 97) ID = UnityEngine.Random.Range(71, 76);
            }

            ClearBonus = ((ID-10)*(ID-10))*(GameData.getSpaceNum()+2);
            GameData.setFullHP();
            AllClear = true;
            GameObject stage;
            stage = GameObject.Find("Stage").transform.FindChild(ID+"").gameObject;
            stage.SetActive(true);
            GameObject.Find("Audio").transform.FindChild("" + GameData.getSpaceNum()).gameObject.SetActive(true);
            
           
            if (GameData.getEquippedItem() != 999)
            {
                Equipped.transform.FindChild("Weapon").gameObject.SetActive(true);
                Equipped.transform.FindChild("Weapon").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.EquipmentInventory[GameData.getEquippedItem()].id].id);
        }
        if (GameData.getEquippedHead() != 999)
        {
            Equipped.transform.FindChild("Head").gameObject.SetActive(true);
            Equipped.transform.FindChild("Head").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.EquipmentInventory[GameData.getEquippedHead()].id].id);
        }
        if (GameData.getEquippedArmor() != 999)
            {
                Equipped.transform.FindChild("Armor").gameObject.SetActive(true);
                Equipped.transform.FindChild("Armor").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.EquipmentInventory[GameData.getEquippedArmor()].id].id);
        }
        if (GameData.getEquippedShoes() != 999)
        {
            Equipped.transform.FindChild("Shoes").gameObject.SetActive(true);
            Equipped.transform.FindChild("Shoes").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.EquipmentInventory[GameData.getEquippedShoes()].id].id);
        }
        if (GameData.getEquippedAcc() != 999)
            {
                Equipped.transform.FindChild("Acc").gameObject.SetActive(true);
                Equipped.transform.FindChild("Acc").FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GameData.ItemList[GameData.EquipmentInventory[GameData.getEquippedAcc()].id].id);
            }
            Player = GameObject.Find("Character");
            Player.GetComponent<Animator>().SetBool("walk", true);
            TimePlus = false;

    }

	// Use this for initialization
	void Start () {
        isAuto = false;
        if (GameData.isAutoHunt) { isAuto = true;
            ClickImg.transform.parent.FindChild("Auto").gameObject.SetActive(true);
            ClickImg.transform.parent.GetComponent<Image>().raycastTarget = false;
        }
        if (GameData.getEquippedItem() != 999)
        {
            if (GameData.ItemList[GameData.EquipmentInventory[GameData.getEquippedItem()].id].itemType == ItemType.Weapon)
            {
                Weapon.transform.parent.FindChild("hand").gameObject.SetActive(true);
                Weapon.transform.FindChild("Weapon").GetComponent<SpriteRenderer>().sprite = GameData.getEquippedImg();
                Weapon.SetActive(true);
                Gun.SetActive(false);
                HitType = 0;
            }
            else
            {

                Weapon.transform.parent.FindChild("hand_g").gameObject.SetActive(true);
                Gun.transform.FindChild("Gun").GetComponent<SpriteRenderer>().sprite = GameData.getEquippedImg();
                Weapon.SetActive(false);
                Gun.SetActive(true);
                HitType = 1;
            }
        }
        else
        {
            Weapon.transform.parent.FindChild("hand").gameObject.SetActive(true);
            Weapon.transform.FindChild("Weapon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Img/IT999");
            Weapon.SetActive(true);
            Gun.SetActive(false);
            HitType = 0;
        }
        //  SendMessage("setOnScroll", true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && BackHomeBt.interactable)
        {
            ClickBackHome();
        }

        if(TimingPanel.transform.parent.FindChild("Great").GetComponent<CanvasGroup>().alpha != 0)
        {
            TimingPanel.transform.parent.FindChild("Great").transform.localPosition += new Vector3(0, 50* Time.deltaTime,0);
            TimingPanel.transform.parent.FindChild("Great").GetComponent<CanvasGroup>().alpha -= 2* Time.deltaTime;
        }

        if (DiePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (GameData.getEmerald() < 2)
                DiePanel.transform.FindChild("aConfirmP").FindChild("Button").GetComponent<Button>().interactable = false;
            else
                DiePanel.transform.FindChild("aConfirmP").FindChild("Button").GetComponent<Button>().interactable = true;
        }

        if (monsterName.transform.parent.parent.FindChild("Damage").gameObject.GetComponent<CanvasGroup>().alpha != 0)
            monsterName.transform.parent.parent.FindChild("Damage").gameObject.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        if (monsterName.transform.parent.parent.FindChild("Damage_m").gameObject.GetComponent<CanvasGroup>().alpha != 0) 
            monsterName.transform.parent.parent.FindChild("Damage_m").gameObject.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;

        if (Rewordlist.activeInHierarchy )
        { if(Rewordlist.transform.localPosition.y < 100)
                Rewordlist.transform.localPosition += new Vector3(0, 40 * Time.deltaTime, 0); }
        else Rewordlist.transform.localPosition = new Vector2(200, 50);

        
       
        if (Pause == false) {
            time += Time.deltaTime;
         
            HpBar.value = (float)GameData.getHP() / (float)GameData.getMaxHp();
            HpBar.transform.FindChild("Text").GetComponent<Text>().text = GameData.getHP()+"/"+ GameData.getMaxHp();
            Minimap.value = Percent;
            Minimap.transform.FindChild("Text").GetComponent<Text>().text = "" + (int)(Percent * 100)+"%";
            if (playType == HuntPlayType.Coming)
            {

                if (TimingScroll.value == 0) TimePlus = true;
                if (TimingScroll.value == 1) TimePlus = false;

                //time

                if (!isRandom)
                {
                    if (TimePlus) TimingScroll.value += 0.75f * Time.deltaTime;
                    else TimingScroll.value -= 0.75f * Time.deltaTime;
                }
                else
                {
                    TimingScroll.transform.FindChild("Sliding Area").FindChild("Handle").localPosition = TimingTarget.transform.localPosition;
                }

                if (isAuto)
                {
                    GameObject.Find("Audio").transform.FindChild("click").GetComponent<AudioSource>().Play();
                    TPanel.alpha = 0.5f;
                    TimingPanel.interactable = false;
                    TimingPanel.alpha = 0;

                    playType = HuntPlayType.Fighting;
                }
                
            } 
            else if (playType == HuntPlayType.Waiting)
            {
                ClickImg.SetActive(false);
                if (time > 2f)
                {
                    playType = HuntPlayType.Noting;
                    Player.GetComponent<Animator>().SetBool("walk", true);

                    GameObject[] Scrollers = GameObject.FindGameObjectsWithTag("Scroller");
                    foreach (GameObject Scroller in Scrollers)
                    {
                        Scroller.SendMessage("setOnScroll", true);
                    }
                    time = 1;
                }
            }
            else if (playType == HuntPlayType.Hitted)
            {
                if (time > 0.5)
                {
                    playType = HuntPlayType.Fighting;
                }
            }
     
            else if (playType == HuntPlayType.Noting)
            {

                monsterName.transform.parent.gameObject.SetActive(false);
                if (Percent < 1 )
                {
                    if (GameObject.Find("LevelUpCanvas").GetComponent<CanvasGroup>().alpha == 0)
                    {

                        int NumC = Rewordlist.transform.childCount;
                        Rewordlist.SetActive(false);
                        for (int i=0; i<NumC; i++)
                        {
                            Destroy(Rewordlist.transform.GetChild(0).gameObject);
                        }
                        

                        Percent += mapSpeed * Time.deltaTime;
                        BackHomeBt.interactable = true;


                        float randNum = UnityEngine.Random.Range(0.2f, 0.6f);
                        if (time % 5 < randNum)
                        {
                                Player.GetComponent<Animator>().SetBool("walk", false);
                                GameObject[] Scrollers = GameObject.FindGameObjectsWithTag("Scroller");
                                foreach (GameObject Scroller in Scrollers)
                                {
                                    Scroller.SendMessage("setOnScroll", false);
                                }
                                //
                                playType = HuntPlayType.Coming;
                            //세팅

                            if (!isRandom)
                            {
                                int StarID = ID / 10;
                                int StageID = ID % 10;
                                StageID = ((StarID - 1) * 5) + (StageID - 1);
                                Stage nowStage = GameData.StageList[StageID];
                                string[] Droptmp = nowStage.drop.Split('|');

                                int fullProb = 0;
                                int xProb = 0;

                                for (int i = 0; i < Droptmp.Length; i++)
                                {
                                    fullProb += int.Parse(Droptmp[i].Split('_')[2]);
                                }

                                int probability = UnityEngine.Random.Range(0, fullProb);

                                for (int i = 0; i < Droptmp.Length; i++)
                                {
                                    string[] tmp = Droptmp[i].Split('_');
                                    if (probability <= (int.Parse(tmp[2]) + xProb))
                                    {
                                        GameObject.Find("Audio").transform.FindChild("popup").GetComponent<AudioSource>().Play();
                                        if (tmp[0].Equals("mo"))
                                        {
                                            GameObject monster = Instantiate(Resources.Load<GameObject>("monster/mo" + tmp[1])) as GameObject;
                                            monster.transform.SetParent(GameObject.Find("Enemy").transform);
                                            monster.transform.position = monster.transform.position + monster.transform.parent.position;
                                            monster.GetComponent<MonsterData>().setMonster(int.Parse(tmp[1]));
                                            TargetObject = monster;
                                            monster.GetComponent<MonsterData>().huntManager = this;
                                            monsterName.transform.parent.gameObject.SetActive(true);
                                            monsterName.GetComponent<Text>().text = GameData.MonsterList[int.Parse(tmp[1])].name;
                                            if (GameData.MonsterList[int.Parse(tmp[1])].att > (GameData.getPower() + GameData.getItemPower()))
                                                monsterName.GetComponent<Text>().color = new Color32(250, 30, 30, 255);
                                            else monsterName.GetComponent<Text>().color = new Color32(130, 250, 90, 255);
                                        }
                                        else if (tmp[0].Equals("El"))
                                        {
                                            GameObject element = Instantiate(Resources.Load<GameObject>("monster/ElementNode")) as GameObject;
                                            element.transform.SetParent(GameObject.Find("Enemy").transform);
                                            element.transform.position = element.transform.parent.position + new Vector3(0, 0.2f, 0);
                                            element.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                                            element.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Element/" + GameData.ElementList[int.Parse(tmp[1])].id);
                                            element.name = tmp[1];
                                            TargetObject = element;
                                            monsterName.transform.parent.gameObject.SetActive(true);
                                            monsterName.GetComponent<Text>().text = GameData.ElementList[int.Parse(tmp[1])].name;
                                            monsterName.GetComponent<Text>().color = new Color32(230, 230, 230, 255);
                                        }
                                        break;
                                    }
                                    xProb = int.Parse(tmp[2]) + xProb;

                                }
                            }
                            else
                            {

                                int MonID = 0;
                                //행성 추가
                                if ((int)(ID / 10) == 1) MonID = UnityEngine.Random.Range(0, 24);
                                else if ((int)(ID / 10) == 2) MonID = UnityEngine.Random.Range(24, 45);
                                else if ((int)(ID / 10) == 3) MonID = UnityEngine.Random.Range(45, 67);

                               
                                GameObject.Find("Audio").transform.FindChild("popup").GetComponent<AudioSource>().Play();
                                GameObject monster = Instantiate(Resources.Load<GameObject>("monster/mo" + MonID)) as GameObject;
                                monster.transform.SetParent(GameObject.Find("Enemy").transform);
                                monster.transform.position = monster.transform.position + monster.transform.parent.position;
                                monster.GetComponent<MonsterData>().setMonster(MonID);
                                TargetObject = monster;
                                monster.GetComponent<MonsterData>().huntManager = this;
                                monsterName.transform.parent.gameObject.SetActive(true);
                                monsterName.GetComponent<Text>().text = GameData.MonsterList[MonID].name;
                                if (GameData.MonsterList[MonID].att > (GameData.getPower() + GameData.getItemPower()))
                                    monsterName.GetComponent<Text>().color = new Color32(250, 30, 30, 255);
                                else monsterName.GetComponent<Text>().color = new Color32(130, 250, 90, 255);
                            }
                            //

                                int ranPos = 0;
                                if (!isRandom) ranPos = UnityEngine.Random.Range(-330, 0);
                                TimingTarget.transform.localPosition = new Vector3(ranPos, TimingTarget.transform.position.y, 0);

                                int ranPos2 = 500;
                                if (!isRandom) ranPos2 = UnityEngine.Random.Range(0, 330);
                                TimingTarget.transform.parent.FindChild("Target2").localPosition = new Vector3(ranPos2, TimingTarget.transform.position.y, 0);
                                TPanel.alpha = 1;
                                TimingPanel.alpha = 1;
                                TimingPanel.interactable = true;
                            }
                        }
                    
                   
                }
                else
                {
                    Player.GetComponent<Animator>().SetBool("walk", false);
                    GameObject[] Scrollers = GameObject.FindGameObjectsWithTag("Scroller");
                    foreach (GameObject Scroller in Scrollers)
                    {
                        Scroller.SendMessage("setOnScroll", false);
                    }
                    GameObject.Find("Audio").transform.FindChild("clear").GetComponent<AudioSource>().gameObject.SetActive(true);
                    ClearPanel.alpha = 1;
                    ClearPanel.interactable = true;
                    ClearPanel.blocksRaycasts = true;

                    if (AllClear && !isRandom)
                    {
                            GameObject.Find("AllCoin").transform.FindChild("Text").GetComponent<Text>().text = AllCoin + " + 퍼펙트보너스 " + ClearBonus + "Exp";
                            GameData.upExp(ClearBonus);
                            AllClear = false;
                    }
                }
            }
            else if (playType == HuntPlayType.Fighting)
            {
                if (isAuto)
                {
                    if (!TargetObject.GetComponent<MonsterData>().isFight)
                    {
                        ClickImg.GetComponent<Animator>().Play("Click");
                        GameObject.Find("Audio").transform.FindChild("click").GetComponent<AudioSource>().Play();
                        StaminaBar.value += 0.3f * Time.deltaTime;
                    }
                    else if (TargetObject.tag.Equals("Element"))
                    {
                        ClickImg.GetComponent<Animator>().Play("Click");
                        GameObject.Find("Audio").transform.FindChild("click").GetComponent<AudioSource>().Play();
                        StaminaBar.value += 0.3f * Time.deltaTime;
                    }
                }
                //BackHomeBt.interactable = false;
                ClickImg.SetActive(true);
                
                if (StaminaBar.value == 1)
                {
                    if (TargetObject.tag.Equals("Element"))
                    {
                         time = 0;
                        GameObject.Find("Audio").transform.FindChild("get").GetComponent<AudioSource>().Play();
                        int count = UnityEngine.Random.Range(1, 20);
                        GameData.setElement(int.Parse(TargetObject.name), count);
                        Debug.Log(GameData.ElementList[int.Parse(TargetObject.name)].name);

                        GameObject tp = Instantiate(getItemNode);
                        tp.name = getItemNode.name;
                        tp.transform.position = GameObject.Find("topContents").transform.position 
                            + new Vector3((350 * Percent / 100), 0, 0);
                        tp.transform.SetParent(GameObject.Find("topContents").transform);
                        tp.transform.localScale = new Vector3(1, 1, 1);
                        tp.GetComponent<topPanelgetItemData>().id = int.Parse(TargetObject.name);
                        
                        GameObject rw = Instantiate(getRewordNode);
                        rw.name = getRewordNode.name;
                        rw.transform.position = Rewordlist.transform.position;
                        rw.transform.SetParent(Rewordlist.transform);
                        rw.transform.localScale = new Vector3(1, 1, 1);
                        rw.transform.FindChild("Image").GetComponent<Image>().sprite
                            = Resources.Load<Sprite>("Element/" + GameData.ElementList[int.Parse(TargetObject.name)].id);
                        rw.transform.FindChild("Text").GetComponent<Text>().text = "x" + count;

                        Rewordlist.SetActive(true);

                        AllItems(int.Parse(TargetObject.name), count,0);

                        Destroy(TargetObject);
                        playType = HuntPlayType.Waiting;
                        StaminaBar.value = 0;
                    }
                    else
                    {
                        if(HitType==0)
                        Player.GetComponent<Animator>().Play("Hit");
                        else if(HitType==1) Player.GetComponent<Animator>().Play("Shot");

                        TargetObject.GetComponent<MonsterData>().hitted();
                        StaminaBar.value = 0;
                    }
                }
                if (GameData.getHP() <= 0)
                {
                    //DiePanel.alpha = 1;
                    // DiePanel.interactable = true;
                    // DiePanel.blocksRaycasts = true;
                    Player.GetComponent<Animator>().Play("die");
                    playType = HuntPlayType.Die;
                    time = 0;

                    GameObject.Find("Audio").transform.FindChild("lose").GetComponent<AudioSource>().Play();
                }
            }
            else if (playType == HuntPlayType.Die)
            {
                if (time > 2)
                {
                    
                    DiePanel.alpha = 1;
                    DiePanel.interactable = true;
                    DiePanel.blocksRaycasts = true;
                }
            }
        }     
	}

    public void ClickBackHome()
    {
        BackPanel.alpha = 1;
        BackPanel.interactable = true;
        BackPanel.blocksRaycasts = true;
        Pause = true;
    }
    public void ClickBackReplay()
    {
        DiePanel.alpha = 0;
        DiePanel.interactable = false;
        DiePanel.blocksRaycasts = false;
        Pause = false;
        
        GameData.useEmerald(2);
        GameData.setFullHP();
        playType = HuntPlayType.Fighting;
        Player.GetComponent<Animator>().Play("idle");
    }

    public void setPlay()
    {
        Pause = false;
    }
    public void setPause()
    {
        Pause = true;
    }

    public void ClickOnTap()
    {
        if (!isAuto)
        {
            Debug.Log("hi");
            if (playType == HuntPlayType.Noting)
            {

            }
            else if (playType == HuntPlayType.Coming)
            {
                if (OnTiming == false)
                {
                    AllClear = false;

                    GameObject.Find("Audio").transform.FindChild("fail").GetComponent<AudioSource>().Play();
                    Debug.Log("실패!");
                    TPanel.alpha = 0.5f;
                    TimingPanel.interactable = false;
                    TimingPanel.alpha = 0;
                    Player.GetComponent<Animator>().SetBool("walk", true);
                    Destroy(TargetObject);
                    playType = HuntPlayType.Noting;
                    GameObject[] Scrollers = GameObject.FindGameObjectsWithTag("Scroller");
                    foreach (GameObject Scroller in Scrollers)
                    {
                        Scroller.SendMessage("setOnScroll", true);
                    }
                }
                else
                {

                    GameObject.Find("Audio").transform.FindChild("button-6").GetComponent<AudioSource>().Play();
                    Debug.Log("Good");
                    TPanel.alpha = 0.5f;
                    TimingPanel.interactable = false;
                    TimingPanel.alpha = 0;
                    TimingPanel.transform.parent.FindChild("Great").localPosition = new Vector2(TimingScroll.transform.FindChild("Sliding Area").FindChild("Handle").localPosition.x, 160);
                    TimingPanel.transform.parent.FindChild("Great").GetComponent<CanvasGroup>().alpha = 1;
                    playType = HuntPlayType.Fighting;
                }

                TimingScroll.value = 1;
            }

            else if (playType == HuntPlayType.Fighting)
            {
                //time
                if (!TargetObject.GetComponent<MonsterData>().isFight)
                {
                    ClickImg.GetComponent<Animator>().Play("Click");
                    GameObject.Find("Audio").transform.FindChild("click").GetComponent<AudioSource>().Play();
                    StaminaBar.value += 2 * Time.deltaTime;
                }
                else if (TargetObject.tag.Equals("Element"))
                {
                    ClickImg.GetComponent<Animator>().Play("Click");
                    GameObject.Find("Audio").transform.FindChild("click").GetComponent<AudioSource>().Play();
                    StaminaBar.value += 2 * Time.deltaTime;
                }
            }
            else if (playType == HuntPlayType.Die)
            {
            }
        }
    
    }

    public void BackShop()
    {
        Player.GetComponent<Animator>().SetBool("walk", false);
        if (GameData.getEquippedItem() != 999)
            GameData.setEquipmentPercent(GameData.getEquippedItem(), GameData.getEquipmentPercent(GameData.getEquippedItem()) - MinusPercent);
        if (GameData.getEquippedHead() != 999)
            GameData.setEquipmentPercent(GameData.getEquippedHead(), GameData.getEquipmentPercent(GameData.getEquippedHead()) - MinusPercent);
        if (GameData.getEquippedArmor()!=999)
            GameData.setEquipmentPercent(GameData.getEquippedArmor(), GameData.getEquipmentPercent(GameData.getEquippedArmor()) - MinusPercent);
        if (GameData.getEquippedShoes() != 999)
            GameData.setEquipmentPercent(GameData.getEquippedShoes(), GameData.getEquipmentPercent(GameData.getEquippedShoes()) - MinusPercent);
        if (GameData.getEquippedAcc() != 999)
            GameData.setEquipmentPercent(GameData.getEquippedAcc(), GameData.getEquipmentPercent(GameData.getEquippedAcc()) - MinusPercent);

        GameData.SaveGame();
        //GameData.loadingScene = "shop";
        // SceneManager.LoadScene("loading");
        GameObject.Find("Audio").transform.FindChild("" + GameData.getSpaceNum()).gameObject.SetActive(false);
        if (GameObject.Find("Main Camera"))
        {
            StopAllCoroutines();
            GameObject load = GameObject.Find("Loading").transform.FindChild("LoadingScene").gameObject;
            GameObject.Find("Main Camera").SetActive(false);
            load.SetActive(true);
            load.transform.FindChild("Loading").GetComponent<loading>().targetName = "shop";
        }
    }
    
    public void SetOnTiming(bool ontiming)
    {
        OnTiming = ontiming;
    }

    public void hited(int Damage)
    {
        GameData.setDamage(Damage);
        if (Damage != 0)
        {
            Player.GetComponent<Animator>().Play("hited");
            monsterName.transform.parent.parent.FindChild("Damage").GetComponent<Text>().text = "-" + Damage;
        }
        else { monsterName.transform.parent.parent.FindChild("Damage").GetComponent<Text>().text = "MISS!"; GameObject.Find("Audio").transform.FindChild("miss").GetComponent<AudioSource>().Play(); }
        monsterName.transform.parent.parent.FindChild("Damage").gameObject.GetComponent<CanvasGroup>().alpha = 1;
        playType = HuntPlayType.Hitted;
        time = 0;
    }

    public void MonsterDie(int id)
    {
        int coin = 0;
        StaminaBar.value = 0;
        TargetObject = null;
        playType = HuntPlayType.Waiting;
        time = 0;
        string[] getData = GameData.MonsterList[id].reward.Split('_');
        Rewordlist.SetActive(true);
        for (int i = 0; i < getData.Length; i++)
        {
            string[] tmp = getData[i].Split('|');
            if (tmp[0].Equals("mon"))
            {
                coin = int.Parse(tmp[1]);
                GameData.addMoney(int.Parse(tmp[1]));

                GameObject rw = Instantiate(getRewordNode);
                rw.name = getRewordNode.name;
                rw.transform.position = Rewordlist.transform.position;
                rw.transform.SetParent(Rewordlist.transform);
                rw.transform.localScale = new Vector3(1, 1, 1);
                rw.transform.FindChild("Image").GetComponent<Image>().sprite
                    = Resources.Load<Sprite>("char/coin_re");
                rw.transform.FindChild("Text").GetComponent<Text>().text = "x" + int.Parse(tmp[1]);
            }
            else if (tmp[0].Equals("exp"))
            {
                GameData.upExp(int.Parse(tmp[1]));

                GameObject rw = Instantiate(getRewordNode);
                rw.name = getRewordNode.name;
                rw.transform.position = Rewordlist.transform.position;
                rw.transform.SetParent(Rewordlist.transform);
                rw.transform.localScale = new Vector3(1, 1, 1);
                rw.transform.FindChild("Text").GetComponent<Text>().text = "+ " + int.Parse(tmp[1]) + "Exp";
            }
            else if (tmp[0].Equals("El"))
            {
                GameObject.Find("Audio").transform.FindChild("get").GetComponent<AudioSource>().Play();
                int count = UnityEngine.Random.Range(1, int.Parse(tmp[2]) + 1);
                GameData.setElement(int.Parse(tmp[1]),count);
                GameObject tp = Instantiate(getItemNode);
                tp.name = getItemNode.name;
                tp.transform.position = GameObject.Find("topContents").transform.position
                           + new Vector3((350 * Percent / 100), 0, 0);
                tp.transform.SetParent(GameObject.Find("topContents").transform);
                tp.transform.localScale = new Vector3(1, 1, 1);
                tp.GetComponent<topPanelgetItemData>().id = int.Parse(tmp[1]);

                GameObject rw = Instantiate(getRewordNode);
                rw.name = getRewordNode.name;
                rw.transform.position = Rewordlist.transform.position;
                rw.transform.SetParent(Rewordlist.transform);
                rw.transform.localScale = new Vector3(1, 1, 1);
                rw.transform.FindChild("Image").GetComponent<Image>().sprite 
                    = Resources.Load<Sprite>("Element/" + GameData.ElementList[int.Parse(tmp[1])].id);
                rw.transform.FindChild("Text").GetComponent<Text>().text = "x"+count;

                AllItems(int.Parse(tmp[1]), count,coin);
            }

            monsterName.transform.parent.gameObject.SetActive(false);

        }
      

    }

    public void AllItems(int id, int count, int coin)
    {
        AllCoin += coin;
        bool isExist = false;
        int NumC;
        NumC = GameObject.Find("AllItems").transform.childCount;
        for (int i=0; i<NumC; i++)
        {
            if(GameObject.Find("AllItems").transform.GetChild(i).GetComponent<topPanelgetItemData>().id == id)
            {
                GameObject.Find("AllItems").transform.GetChild(i).GetComponent<topPanelgetItemData>().count += count;
                isExist = true;
            }
            
        }
        if (!isExist)
        {
            GameObject tp = Instantiate(getItemNode);
            tp.name = getItemNode.name;
            tp.transform.position = GameObject.Find("AllItems").transform.position;
            tp.transform.SetParent(GameObject.Find("AllItems").transform);
            tp.transform.localScale = new Vector3(1, 1, 1);
            tp.GetComponent<topPanelgetItemData>().id = id;
            tp.GetComponent<topPanelgetItemData>().count = count;
        }
        GameObject.Find("AllCoin").transform.FindChild("Text").GetComponent<Text>().text = "" + AllCoin;
    }
   
}
