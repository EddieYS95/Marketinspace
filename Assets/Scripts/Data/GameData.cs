using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
using System;
public enum NodeType
{
    Empty ,Item, Element
}
public enum ItemType
{
    Weapon, Gun, Armor, Drink, Accessory, Head, Shoes
}

public struct Event
{
    public int id;
    public string talkData;
    public int questSet;

    public Event(int id, string talkData, int questSet)
    {
        this.id = id;
        this.talkData = talkData;
        this.questSet = questSet;
    }

}
public struct Talk
{
    public int id;
    public int NpcID;
    public string name;
    public string talk;

    public Talk(int id, int NpcID, string name, string talk)
    {
        this.id = id;
        this.NpcID = NpcID;
        this.name = name;
        this.talk = talk;
    }
}
public struct Quest
{
    public int id;
    public string name;
    public string script;
    public string goal;
    public string reward;
    public string goalText;
    public string rewardText;
    public bool mainQuest;
    public int charNum;
    public int nextQuest;

    public Quest(int id, string name, string script, string goal, string reward, string goalText, string rewardText, bool mainQuest, int charNum, int nextQuest)
    {
        this.id = id;
        this.name = name;
        this.script = script;
        this.goal = goal;
        this.reward = reward;
        this.goalText = goalText;
        this.rewardText = rewardText;
        this.mainQuest = mainQuest;
        this.nextQuest = nextQuest;
        this.charNum = charNum;
    }
}



public struct InventoryElementNode
{
    public NodeType NodeType;//true : 아이템, false : Element
    public int id;
    public int Count;

    

    public InventoryElementNode(int id, int Count, NodeType Type)
    {
        NodeType = Type;
        this.id = id;
        if (NodeType == NodeType.Element)
            this.Count = Count;
        else if (NodeType == NodeType.Item)
            this.Count = 1;
        else
        {
            this.Count = 0;
        }
    }
}

public struct InventoryItemNode
{
    public NodeType NodeType;//true : 아이템, false : Element
    public int id;
    public int Count;

    public InventoryItemNode(int id, int Count, NodeType Type)
    {
        NodeType = Type;
        this.id = id;
        
        if (NodeType == NodeType.Item)
            this.Count = Count;
        else
        {
            this.Count = 0;
        }
    }
}

public struct EquipmentItemNode
{
    public NodeType NodeType;
    public int id;
    public int percent;

    public EquipmentItemNode(int id, int percent, NodeType Type)
    {
        NodeType = Type;
        this.id = id;
        if (NodeType == NodeType.Item)
            this.percent = percent;

        else this.percent = 0;     
    }
}

public struct MakingItemNode
{
    public NodeType NodeType;
    public int id;
    public int Count;
    public string StartTime;

    public MakingItemNode(int id, int count, string st, NodeType Type)
    {
        NodeType = Type;
        this.id = id;
        this.StartTime = st;
        if (NodeType == NodeType.Item)
        {
            this.Count = count;
            this.StartTime = st;
        }
        else 
        {
            this.StartTime = "";
            this.Count = 0;
        }
    }
}

public struct Stage
{
    public int id;
    public string name;
    public string drop;
    public int level;

    public Stage(int id, string name, string drop, int level)
    {
        this.id = id;
        this.name = name;
        this.drop = drop;
        this.level = level;
    }
}

public struct Interior
{
    public int id;
    public string name;
    public string content;
    public int standcoin;
    public int wallcoin;
    public int doorcoin;
    public int countercoin;
    public int level;

    public Interior(int id, string name, string content, int standcoin, int wallcoin, int doorcoin, int countercoin, int level)
    {
        this.id = id;
        this.name = name;
        this.content = content;
        this.standcoin = standcoin;
        this.wallcoin = wallcoin;
        this.doorcoin = doorcoin;
        this.countercoin = countercoin;
        this.level = level;
    }
}

public struct Chain
{
    public string name;
    public bool isIng;
    public string STime;
    public int Money;

    public Chain(string name, string STime, int Money, bool isIng)
    {
        this.name = name;
        this.STime = STime;
        this.Money = Money;
        this.isIng = isIng;
    }
}

public struct Element
{
    public string id;
    public string name;
    public string script;
    public bool bookAble;
    public string get;
    public int value;

    public Element(string id, string name, string script, int value){
        this.id = id;
        this.name = name;
        this.script = script;
        this.value = value;
        get = "";
        bookAble = false;
    }
}
public struct ElementBox
{
    public int id;
    public string name;
    public int bricks;
    public int emerald;
    public string reword;

    public ElementBox(int id, string name, int bricks, int emerald, string reword)
    {
        this.id = id;
        this.name = name;
        this.bricks = bricks;
        this.emerald = emerald;
        this.reword = reword;
    }
}
public struct Item
{
    public string id;
    public string name;
    public string script;
    public int[] NeedElementID;
    public int[] NeedElementCount;
    public float inProgressTime;
    public int Cost;
    public int progressTime;
    public bool makeAble;
    public bool bookAble;
    public int LevelAble;

    public ItemType itemType;
    public string Power;
    public string Def;
    public int Tier;

    public Item(string id, string name, string script, int[] NeedElementID, int[] NeedElementCount, int Cost, int progressTime, ItemType itemType, string power, string def,int tier)
    {
        this.id = id;
        this.name = name;
        this.script = script;
        this.NeedElementID = NeedElementID;
        this.NeedElementCount = NeedElementCount;
        inProgressTime = progressTime;
        this.Cost = Cost;
        this.progressTime = progressTime;
        this.itemType = itemType;
        this.Power = power;
        this.Def = def;
        this.Tier = tier;
        LevelAble = 0;
        makeAble = false;
        bookAble = false; 
    }
}
public struct Monster
{
    public int id;
    public string name;
    public int hp;
    public int att;
    public int speed;
    public string reward;

    public Monster(int id, string name, int hp, int att, int speed, string reward)
    {
        this.name = name;
        this.id = id;
        this.hp = hp;
        this.att = att;
        this.speed = speed;
        this.reward = reward;
    }


}

public class GameData : MonoBehaviour
{

    static GameData Instance = new GameData();

    private GameData()
    {
        ingEvent = -1;

        OnGame = false;
        MaxLevel = 40;
        setMonsterList();
        setElementList();
        setElementBoxList(); //재료박스
        setQuestList();
        setItemList();
        forgeAtItemList = new List<Item>();
        forgeAtItemList.AddRange(ItemList);
        setLevelUpData();
        setEventList();
        setTalkList();
        setStageList();
        setInteriorList();
        ShopFloorSize = 1;
        ShopInfoSize = ShopFloorSize * 4;
        ShopInfo = new int[ShopInfoSize];
        ShopDoorInfo=0;
        ShopCounterInfo=0;
        ShopWallInfo = new int[ShopFloorSize];
        ShopSaleItemInfo = new int[ShopInfoSize];
        setGetInfo();
        for (int i = 0; i < ShopInfoSize; i++)
        {
            ShopInfo[i] = -1;
            ShopSaleItemInfo[i] = -1;
        }
        for (int i = 0; i < ShopFloorSize; i++)
        {
            ShopWallInfo[i] = 0;
        }

        string Name = "save.sav";

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            Name = Path.Combine(Path.Combine(path, "Documents"), Name);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            Name = Path.Combine(path, Name);
        }
        else
        {
        }

        var fileName = Name;

        if (File.Exists(fileName))
        {
            LoadGame();
            OnGame = true;
        }
       
    }

    public static void newGame(string name)
    {
       
        Name = name;
        Level = 1;
        Exp = 0;
        MaxHP = 30;
        MaxStamina = 100;
        Stamina = MaxStamina;
        ItemInventorySize = 60;
        ElementInventorySize = 60;
        EquipmentInventorySize = 6;
        EquipmentSize = 6;
        Star = 0;
        Heart_p = 5;
        Money = 0;
        SpaceNum = 0;
        SpaceOpen = 0;
        Emerald = 0;
        Day = 1;
        ItemInventory = new InventoryItemNode[ItemInventorySize];
        ElementInventory = new InventoryElementNode[ElementInventorySize];
        EquipmentInventory = new EquipmentItemNode[EquipmentInventorySize];
        MakingItem = new MakingItemNode[5];
        HP = MaxHP;
        power = 5;
        speed = 5;
        deffens = 5;
        StageNum = 11;
        STime = DateTime.Now;
        HTime = DateTime.Now;
        ADTime = DateTime.Today;
        ShopDoorInfo = 0;
        ShopCounterInfo = 0;
        BackSoundV = 1;
        EffectSoundV = 1;

        for (int i = 0; i < ItemInventorySize; i++)
        {
            ItemInventory[i] = new InventoryItemNode(-1, 0, NodeType.Empty);
        }
        for (int i = 0; i < ElementInventorySize; i++)
        {
            ElementInventory[i] = new InventoryElementNode(-1, 0, NodeType.Empty);
        }
        for (int i = 0; i < EquipmentInventorySize; i++)
        {
            EquipmentInventory[i] = new EquipmentItemNode(-1, 0, NodeType.Empty);
        }
        for (int i = 0; i < 5; i++)
        {
            MakingItem[i] = new MakingItemNode(-1, 0, "", NodeType.Empty);
        }
        for (int i = 0; i < ShopInfoSize; i++)
        {
            ShopInfo[i] = -1;
            ShopSaleItemInfo[i] = -1;
        }
        for (int i = 0; i < ShopFloorSize; i++)
        {
            ShopWallInfo[i] = 0;
        }

        ShopFloorSize = 1;
        ShopInfoSize = 4;
        ChainSize = 0;
        MakeList = new List<int>();
        QuestInComplete = new List<int>();
        QuestInReady = new List<int>();
        QuestInProgress = new List<int>();
        ChainList = new List<Chain>();
        OnGame = true;
        

        for (int i=0; i<ItemList.Count; i++)
        {
            Item tmp = ItemList[i];
            tmp.makeAble = false;
            tmp.bookAble = false;
            ItemList[i] = tmp;
        }
        for (int i = 0; i < ElementList.Count; i++)
        {
            Element tmp = ElementList[i];
            tmp.bookAble = false;
            tmp.get = "";
            ElementList[i] = tmp;
        }
        setAbleMake(0);
        setAbleMake(1);
        setAbleMake(12);
        setAbleMake(13);

        setQuestReady(5);

        setItem(0);
        setEquipment(0);
        ShopInfo[2] = 0;

        //SubQuest
        setQuestReady(18);
        setQuestReady(30);
        setQuestReady(35);
        setQuestReady(63);
        setQuestReady(69);



        setGetInfo();


        SaveGame();


    }

    public static void setFullHP()
    {
        HP = MaxHP;
    }

    public static void setAbleMake(int ItemID)
    {
       Item tmp = ItemList[ItemID];
       tmp.makeAble = true;
       ItemList[ItemID] = tmp;
      
    }
    public static void setAbleBook(int ItemID)
    {
        Item tmp = ItemList[ItemID];
        tmp.bookAble = true;
        ItemList[ItemID] = tmp;

    }
    public static void setAbleEleBook(int EleID)
    {
        Element tmp = ElementList[EleID];
        tmp.bookAble = true;
        ElementList[EleID] = tmp;

    }


    public static GameData getInstance(){
        return Instance;
    }

    //Player Data
    public static int alba = 1;
    public static bool OnGame;
    public static int StageNum;
    public static bool isRandom;


    private static string Name;
    private static int Money;
    private static int SpaceNum;
    private static int SpaceOpen;
    private static int Emerald;
    private static int MaxHP;
    private static int HP;
    private static int Star;
    private static int MaxStamina;
    private static int Stamina;
    private static int Level;
    private static int Exp;
    private static int Day;
    private static int Heart_p;

    private static int power;
    private static int speed;
    private static int deffens;

    private static int EquippedItem;
    private static int EquippedHead;
    private static int EquippedArmor;
    private static int EquippedShoes;
    private static int EquippedAcc;

    private static int ItemInventorySize;
    private static int ElementInventorySize;
    private static int EquipmentInventorySize;
    private static int EquipmentSize;
    private static int ChainSize;

    public static DateTime STime;
    public static DateTime HTime;
    public static DateTime ADTime;

    public static InventoryItemNode[] ItemInventory;
    public static InventoryElementNode[] ElementInventory;
    public static EquipmentItemNode[] EquipmentInventory;
    public static MakingItemNode[] MakingItem;
    public static List<int> MakeList;
    public static List<int> QuestInProgress;
    public static List<int> QuestInComplete;
    public static List<int> QuestInReady;
    private static int ShopFloorSize;
    private static int ShopInfoSize;
    public static int[] ShopInfo;
    public static int[] ShopSaleItemInfo;
    public static int[] ShopWallInfo;
    public static int ShopDoorInfo;
    public static int ShopCounterInfo;

    public static int ingEvent;
    public static bool isAutoHunt;

    public static string loadingScene;

    public static float BackSoundV;
    public static float EffectSoundV;

    //Game Use Data
    private static int MaxLevel;
    public static List<Item> ItemList;
    public static List<Item> forgeAtItemList;
    public static List<Element> ElementList;
    public static List<Quest> QuestList;
    public static List<Monster> MonsterList;
    public static List<ElementBox> ElementBoxList;
    public static List<Event> EventList;
    public static List<Talk> TalkList;
    public static List<Stage> StageList;
    public static List<Interior> InteriorList;
    public static List<Chain> ChainList;
    public static int[] LevelUpExp;
    public static string[] LevelUpReward;
    public static bool isOpen;

    //function
    
        public static void SetTimeNow()
        {
            STime = DateTime.Now;
        }
        public static void SetHTimeNow()
        {
            HTime = DateTime.Now;
        }
        public static void SetADTimeNow()
        {
            ADTime = DateTime.Now;
        }

    //저장된 시간과 현재가 다른 날짜인지 확인함
    public static bool IsDiffrentDay()
        {
            if (STime.Year != DateTime.Now.Year ||
                STime.Month != DateTime.Now.Month ||
                STime.Day!= DateTime.Now.Day
             
                )
                return true;

            return false;
        }

        //저장된 시간값을 문자열로 변환
        public static string ToString(DateTime T)
        {
            string time = string.Format("{0},{1},{2},{3},{4},{5}", T.Year, T.Month, T.Day, T.Hour, T.Minute, T.Second);
            return time;
        }

        public static string STimeToString()
        {
            string stime = string.Format("{0},{1},{2},{3},{4},{5}", STime.Year, STime.Month, STime.Day, STime.Hour, STime.Minute, STime.Second);
            return stime;
        }
        
        public static DateTime ToTime(String S)
        {
            String[] time = S.Split(',');
            DateTime Day = new DateTime(short.Parse(time[0]), short.Parse(time[1]), short.Parse(time[2]), short.Parse(time[3]), short.Parse(time[4]), short.Parse(time[5]));
            return Day;
        }
    
    
        public static int GetPassedDay(DateTime date)
        {
            int day = 0;

            if (STime.Year < date.Year)
            {
                int YearGap = date.Year - STime.Year;
                for(int i=0; i<YearGap; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 1; j < date.Month; j++)
                            day += DateTime.DaysInMonth(date.Year, i);
                    }
                    else
                    {
                        for (int j = 1; j < 13; j++)
                            day += DateTime.DaysInMonth(date.Year - i, j);
                    }
                }
                for (int k = STime.Month; k < 13; k++)
                    day += DateTime.DaysInMonth(STime.Year, k);
            }
            else
            {
                if (STime.Month < date.Month)
                {
                    for (int i = STime.Month; i < date.Month; i++)
                    {
                        day += DateTime.DaysInMonth(date.Year, i);
                    }
                }
                
            }
            
            day += date.Day;
            day -= STime.Day;
            
            return day;
        }
    
    
        public static int GetDay() { return Day; }
        public static void AddDay(int sday) { Day += sday; SaveGame(); }

        public static bool CheckTime()
            {
                if (DateTime.Now > STime)
                    return true;

                return false;
            }

    //저장된 시간과 현재 시간의 차이를 구해서 몇초 경과했는지 확인
        public static double GetPassedTime(DateTime StartTime)
        {
            TimeSpan span = DateTime.Now - StartTime;

            return span.TotalSeconds;
        }

        public static double GetPassedfromSTime()
        {
            TimeSpan span = DateTime.Now - STime;

            return span.TotalSeconds;
        }
        public static double GetPassedfromHTime()
        {
            TimeSpan span = DateTime.Now - HTime;

            return span.TotalSeconds;
        }
        public static double GetPassedfromADTime()
        {
            TimeSpan span = DateTime.Now - ADTime;

            return span.TotalSeconds;
        }


    public static int getDamage()
    {
        int resultPower = power;

        if (EquippedItem != 999 && EquipmentInventory[EquippedItem].id != 999)
        {
            int min = int.Parse(ItemList[EquipmentInventory[EquippedItem].id].Power.Split('_')[0]);
            int max = int.Parse(ItemList[EquipmentInventory[EquippedItem].id].Power.Split('_')[1]);

            int addpower = UnityEngine.Random.Range(min, max);

            resultPower += addpower;
        }
        if (EquippedAcc != 999 && EquipmentInventory[EquippedAcc].id != 999 )
        {
            if (ItemList[EquipmentInventory[EquippedAcc].id].Power != "0")
            {
                int min = int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Power.Split('_')[0]);
                int max = int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Power.Split('_')[1]);

                int addpower = UnityEngine.Random.Range(min, max);

                resultPower += addpower;
            }
        }
        return resultPower;
    }

    public static int getProtect()
    {
        int resultDefnese = deffens;

        if (EquippedArmor != 999 && EquipmentInventory[EquippedArmor].id != 999)
        {
            int min = int.Parse(ItemList[EquipmentInventory[EquippedArmor].id].Def.Split('_')[0]);
            int max = int.Parse(ItemList[EquipmentInventory[EquippedArmor].id].Def.Split('_')[1]);

            int adddefense = UnityEngine.Random.Range(min, max);

            resultDefnese += adddefense;
        }
        if (EquippedHead != 999 && EquipmentInventory[EquippedHead].id != 999)
        {
            int min = int.Parse(ItemList[EquipmentInventory[EquippedHead].id].Def.Split('_')[0]);
            int max = int.Parse(ItemList[EquipmentInventory[EquippedHead].id].Def.Split('_')[1]);

            int adddefense = UnityEngine.Random.Range(min, max);

            resultDefnese += adddefense;
        }
        if (EquippedShoes != 999 && EquipmentInventory[EquippedShoes].id != 999)
        {
            int min = int.Parse(ItemList[EquipmentInventory[EquippedShoes].id].Def.Split('_')[0]);
            int max = int.Parse(ItemList[EquipmentInventory[EquippedShoes].id].Def.Split('_')[1]);

            int adddefense = UnityEngine.Random.Range(min, max);

            resultDefnese += adddefense;
        }
        if (EquippedAcc != 999 && EquipmentInventory[EquippedAcc].id != 999)
        {
            if (ItemList[EquipmentInventory[EquippedAcc].id].Def != "0")
            {
                int min = int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Def.Split('_')[0]);
                int max = int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Def.Split('_')[1]);

                int adddefense = UnityEngine.Random.Range(min, max);

                resultDefnese += adddefense;
            }
        }
        return resultDefnese;
    }
    public static int getPower()
    {
        return power;
    }
    public static int getItemPower()
    {
        int MyPower = 0;

        if (EquippedItem != 999) MyPower += (int.Parse(ItemList[EquipmentInventory[EquippedItem].id].Power.Split('_')[0]) + int.Parse(ItemList[EquipmentInventory[EquippedItem].id].Power.Split('_')[1])) / 2;
        if (EquippedAcc != 999)
        {
            if (ItemList[EquipmentInventory[EquippedAcc].id].Power != "0")
                MyPower += (int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Power.Split('_')[0]) + int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Power.Split('_')[1])) / 2;
        }
            
        return MyPower;
      
    }
    public static int getItemDef()
    {
        int MyDef = 0;

        if (EquippedArmor != 999) MyDef += (int.Parse(ItemList[EquipmentInventory[EquippedArmor].id].Def.Split('_')[0]) + int.Parse(ItemList[EquipmentInventory[EquippedArmor].id].Def.Split('_')[1])) / 2;
        if (EquippedHead != 999) MyDef += (int.Parse(ItemList[EquipmentInventory[EquippedHead].id].Def.Split('_')[0]) + int.Parse(ItemList[EquipmentInventory[EquippedHead].id].Def.Split('_')[1])) / 2;
        if (EquippedShoes != 999) MyDef += (int.Parse(ItemList[EquipmentInventory[EquippedShoes].id].Def.Split('_')[0]) + int.Parse(ItemList[EquipmentInventory[EquippedShoes].id].Def.Split('_')[1])) / 2;
        if (EquippedAcc != 999) {
            if(ItemList[EquipmentInventory[EquippedAcc].id].Def != "0")
            MyDef += (int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Def.Split('_')[0]) + int.Parse(ItemList[EquipmentInventory[EquippedAcc].id].Def.Split('_')[1])) / 2;
        }


        return MyDef;
    }
    public static string getmyPower()
    {
        return ""+power;
    }
    public static int getSpeed()
    {
        return speed;
    }
    public static int getDeffens()
    {
        return deffens;
    }

    public static int getItemInventorySize()
    {
        return ItemInventorySize;
    }

    public static int getElementInventorySize()
    {
        return ElementInventorySize;
    }
    public static int getEquipmentSize()
    {
        return EquipmentSize;
    }
    public static void addEquipmentSize()
    {
        EquipmentSize++;
    }

    public static int getEquippedItem()
    {
        return EquippedItem;
    }

    public static int getEquippedArmor()
    {
        return EquippedArmor;
    }
    public static int getEquippedHead()
    {
        return EquippedHead;
    }
    public static int getEquippedShoes()
    {
        return EquippedShoes;
    }

    public static int getEquippedAcc()
    {
        return EquippedAcc;
    }
    public static Sprite getEquippedImg()
    {
       
        return Resources.Load<Sprite>("img/" + ItemList[EquipmentInventory[EquippedItem].id].id);
    }

    public static void setEquippedItem(int i)
    {
        EquippedItem = i;
    }

    public static void setEquippedAcc(int i)
    {
        EquippedAcc = i;
    }
    //get making info
    public static int getMakingItemID(int id)
    {
        return MakingItem[id].id;
    }
    public static int getMakingItemCount(int id)
    {
        return MakingItem[id].Count;
    }
    public static string getMakingItemTime(int id)
    {
        return MakingItem[id].StartTime;
    }
    public static NodeType getMakingItemType(int id)
    {
        return MakingItem[id].NodeType;
    }
    public static void setEquippedArmor(int i)
    {
        EquippedArmor= i;
    }
    public static void setEquippedHead(int i)
    {
        EquippedHead = i;
    }
    public static void setEquippedShoes(int i)
    {
        EquippedShoes = i;
    }
    public static void SetShopBuild(int index, int id)
    {
        ShopInfo[index] = id;
    }

    private static void setQuestList()
    {
        QuestList = new List<Quest>();

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/QuestList");
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    bool mainQuest = false;
                    if (text[7].Equals("Y"))
                    {
                        mainQuest = true;
                    }
                    QuestList.Add(new Quest(int.Parse(text[0]), text[1], text[2], text[3], text[4], text[5], text[6], mainQuest, int.Parse(text[8]), int.Parse(text[9])));

                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/QuestList.csv", Encoding.Default, true);

            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    bool mainQuest = false;
                    if (text[7].Equals("Y"))
                    {
                        mainQuest = true;
                    }
                    QuestList.Add(new Quest(int.Parse(text[0]), text[1], text[2], text[3], text[4], text[5], text[6], mainQuest, int.Parse(text[8]), int.Parse(text[9])));
                 
                }
            }
        }
        //PrologueMassage Load
        
    }

    public static void setForgeAtItemList(List<ItemType> tags, int SoltType)
    {
        //solttype 0 = 기본 1 = 티어순
        forgeAtItemList = null;
        forgeAtItemList = new List<Item>();

        int cTier = 0;
        int aTier = 1;

        bool isAll = false;
        if (tags.Count == 0)
        {
            isAll = true;
        }

        if (isAll == true)
        {
            while (aTier <7)
            {
                for (int i = 0; i < ItemList.Count; i++)
                {
                        if (ItemList[i].Tier == aTier && aTier != 6)
                        {
                            forgeAtItemList.Add(ItemList[i]);
                        }

                        if(aTier ==6 && ItemList[i].itemType == ItemType.Drink) forgeAtItemList.Add(ItemList[i]);
                }
                aTier++;
            }
            
        }
        else
        {
            
            while (cTier < 6)
            {
                for (int i = 0; i < ItemList.Count; i++)
                {
                    for (int j = 0; j < tags.Count; j++)
                    {
                        if (ItemList[i].itemType.Equals(tags[j]) && ItemList[i].Tier == cTier)
                        {
                            forgeAtItemList.Add(ItemList[i]);
                        }
                    }
                }
                cTier++;
            }
        }
    }

    private static void setStageList()
    {
        StageList = new List<Stage>();
        //PrologueMassage Load

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/StageData");
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {
                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("Id"))
                {
                }
                else
                {
                    StageList.Add(new Stage(int.Parse(text[0]), text[1], text[2], int.Parse(text[3])));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/StageData.csv", Encoding.Default, true);

            while (sr.Peek() > -1)
            {
                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("Id"))
                {
                }
                else
                {
                    StageList.Add(new Stage(int.Parse(text[0]), text[1], text[2], int.Parse(text[3])));
                }
            }
        }
       
    }

    private static void setInteriorList()
    {
        InteriorList = new List<Interior>();
        //PrologueMassage Load

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/InteriorData");
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {
                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("Id"))
                {
                }
                else
                {
                    InteriorList.Add(new Interior(int.Parse(text[0]), text[1], text[2], int.Parse(text[3]), int.Parse(text[4]), int.Parse(text[5]), int.Parse(text[6]), int.Parse(text[7])));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/InteriorData.csv", Encoding.Default, true);

            while (sr.Peek() > -1)
            {
                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("Id"))
                {
                }
                else
                {
                    InteriorList.Add(new Interior(int.Parse(text[0]), text[1], text[2], int.Parse(text[3]), int.Parse(text[4]), int.Parse(text[5]), int.Parse(text[6]), int.Parse(text[7])));
                }
            }
        }

    }
    private static void setElementList()
    {
        ElementList = new List<Element>();
        //PrologueMassage Load

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/ElementList");
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    ElementList.Add(new Element(text[0], text[1], text[2], int.Parse(text[3])));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/ElementList.csv", Encoding.Default, true);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    ElementList.Add(new Element(text[0], text[1], text[2], int.Parse(text[3])));
                }
            }
        }

      
    }

    private static void setElementBoxList()
    {
        ElementBoxList = new List<ElementBox>();
        //PrologueMassage Load



        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/ElementBoxList");
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    ElementBoxList.Add(new ElementBox(int.Parse(text[0]), text[1], int.Parse(text[2]), int.Parse(text[3]), text[4]));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/ElementBoxList.csv", Encoding.Default, true);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    ElementBoxList.Add(new ElementBox(int.Parse(text[0]), text[1], int.Parse(text[2]), int.Parse(text[3]), text[4]));
                }
            }
        }
        
    }

    private static void setEventList()
    {
        EventList = new List<Event>();
        //PrologueMassage Load

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/EventData");
            StringReader sr = new StringReader(txtFile.text);

            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("eventID"))
                {
                }
                else
                {
                    EventList.Add(new Event(int.Parse(text[0]), text[1], int.Parse(text[2])));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/EventData.csv", Encoding.Default, true);

            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("eventID"))
                {
                }
                else
                {
                    EventList.Add(new Event(int.Parse(text[0]), text[1], int.Parse(text[2])));
                }
            }
        }
        
    }
    private static void setTalkList()
    {
        TalkList = new List<Talk>();

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/TalkData");
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    TalkList.Add(new Talk(int.Parse(text[0]), int.Parse(text[1]), text[2], text[3]));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/TalkData.csv", Encoding.Default, true);

            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    TalkList.Add(new Talk(int.Parse(text[0]), int.Parse(text[1]), text[2], text[3]));
                }
            }
        }
        //PrologueMassage Load

    }
    private static void setMonsterList()
    {
        MonsterList = new List<Monster>();
        //PrologueMassage Load
        //StreamReader sr = new StreamReader("Assets/Resources/Data/MonsterList.csv", Encoding.Default, true);

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load("Data/MonsterList") as TextAsset;
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    MonsterList.Add(new Monster(int.Parse(text[0]), text[1], int.Parse(text[2]), int.Parse(text[3]), int.Parse(text[4]), text[5]));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/MonsterList.csv", Encoding.Default, true);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    MonsterList.Add(new Monster(int.Parse(text[0]), text[1], int.Parse(text[2]), int.Parse(text[3]), int.Parse(text[4]), text[5]));
                }
            }
        }
    }

    private static void setLevelUpData()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/LevelDataList");
            StringReader sr = new StringReader(txtFile.text);
            LevelUpExp = new int[MaxLevel];
            LevelUpReward = new string[MaxLevel];
            while (sr.Peek() > -1)
            {
                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("Level"))
                {
                }
                else
                {
                    LevelUpExp[int.Parse(text[0]) - 1] = int.Parse(text[1]);
                    LevelUpReward[int.Parse(text[0]) - 1] = text[2];

                    string[] tmp = text[2].Split('_');
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (tmp[i].Split('|')[0].Equals("bf"))
                        {
                            Item tt = GameData.ItemList[int.Parse(tmp[i].Split('|')[1])];// = true;
                            tt.LevelAble = int.Parse(text[0]);
                            GameData.ItemList[int.Parse(tmp[i].Split('|')[1])] = tt;
                        }

                    }
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //PrologueMassage Load
            StreamReader sr = new StreamReader("Assets/Resources/Data/LevelDataList.csv", Encoding.Default, true);

            LevelUpExp = new int[MaxLevel];
            LevelUpReward = new string[MaxLevel];
            while (sr.Peek() > -1)
            {
                string[] text;
                text = sr.ReadLine().Split(',');

                if (text[0].StartsWith("Level"))
                {
                }
                else
                {
                    LevelUpExp[int.Parse(text[0]) - 1] = int.Parse(text[1]);
                    LevelUpReward[int.Parse(text[0]) - 1] = text[2];

                    string[] tmp = text[2].Split('_');
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (tmp[i].Split('|')[0].Equals("bf"))
                        {
                            Item tt = GameData.ItemList[int.Parse(tmp[i].Split('|')[1])];// = true;
                            tt.LevelAble = int.Parse(text[0]);
                            GameData.ItemList[int.Parse(tmp[i].Split('|')[1])] = tt;
                        }

                    }
                }
            }
        }
    }

    private static void setItemList()
    {

        ItemList = new List<Item>();

        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtFile = Resources.Load<TextAsset>("Data/ItemList");
            StringReader sr = new StringReader(txtFile.text);
            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');
                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    string[] splite0 = text[8].Split('|');

                    int[] NeedElementID = new int[splite0.Length / 2];
                    int[] NeedElementCount = new int[splite0.Length / 2];

                    for (int i = 0; i < splite0.Length; i++)
                    {

                        if (i % 2 == 0)
                        {
                            NeedElementID[i / 2] = int.Parse(splite0[i]);
                        }
                        else
                        {
                            NeedElementCount[i / 2] = int.Parse(splite0[i]);
                        }
                    }
                    ItemType itemType = ItemType.Weapon;

                    if (text[1].Equals("WP"))
                    {
                        itemType = ItemType.Weapon;
                    }
                    else if (text[1].Equals("GN"))
                    {
                        itemType = ItemType.Gun;
                    }
                    else if (text[1].Equals("AR"))
                    {
                        itemType = ItemType.Armor;
                    }
                    else if (text[1].Equals("HD"))
                    {
                        itemType = ItemType.Head;
                    }
                    else if (text[1].Equals("SH"))
                    {
                        itemType = ItemType.Shoes;
                    }
                    else if (text[1].Equals("AC"))
                    {
                        itemType = ItemType.Accessory;
                    }
                    else if (text[1].Equals("DR"))
                    {
                        itemType = ItemType.Drink;
                    }

                    

                    ItemList.Add(new Item(text[0], text[3], text[6], NeedElementID, NeedElementCount,
                            int.Parse(text[5]), int.Parse(text[9]), itemType, text[10], text[11], int.Parse(text[4])));
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader sr = new StreamReader("Assets/Resources/Data/ItemList.csv", Encoding.Default, true);

            while (sr.Peek() > -1)
            {

                string[] text;
                text = sr.ReadLine().Split(',');
                if (text[0].StartsWith("ID"))
                {
                }
                else
                {
                    string[] splite0 = text[8].Split('|');

                    int[] NeedElementID = new int[splite0.Length / 2];
                    int[] NeedElementCount = new int[splite0.Length / 2];

                    for (int i = 0; i < splite0.Length; i++)
                    {
                        
                        if (i % 2 == 0)
                        {

                            NeedElementID[i / 2] = int.Parse(splite0[i]);
                        }
                        else
                        {
                            NeedElementCount[i / 2] = int.Parse(splite0[i]);
                        }
                    }
                    ItemType itemType = ItemType.Weapon;

                    if (text[1].Equals("WP"))
                    {
                        itemType = ItemType.Weapon;
                    }
                    else if (text[1].Equals("GN"))
                    {
                        itemType = ItemType.Gun;
                    }
                    else if (text[1].Equals("AR"))
                    {
                        itemType = ItemType.Armor;
                    }
                    else if (text[1].Equals("HD"))
                    {
                        itemType = ItemType.Head;
                    }
                    else if (text[1].Equals("SH"))
                    {
                        itemType = ItemType.Shoes;
                    }
                    else if (text[1].Equals("AC"))
                    {
                        itemType = ItemType.Accessory;
                    }
                    else if (text[1].Equals("DR"))
                    {
                        itemType = ItemType.Drink;
                    }


                    ItemList.Add(new Item(text[0], text[3], text[6], NeedElementID, NeedElementCount,
                            int.Parse(text[5]), int.Parse(text[9]), itemType, text[10], text[11], int.Parse(text[4])));
                }
            }
        }
        //PrologueMassage Load
       
    }

    public static int getHP()
    {
        return HP;
    }

    public static int getMaxHp()
    {
        return MaxHP;
    }

    public static void setDamage(int Damage)
    {
        HP -= Damage;
        if (HP < 0)
        {
            HP = 0;
        }
    }

    public static void useElement(int id, int Count){
        int invenId = -1;
        for (int i = 0; i < ElementInventorySize; i++)
        {
            if (ElementInventory[i].id == id)
            {
                invenId = i;
                break;
            }
        }
        if (invenId != -1)
        {
            ElementInventory[invenId].Count -= Count;
            if (ElementInventory[invenId].Count == 0)
            {
                ElementInventory[invenId].NodeType = NodeType.Empty;
                ElementInventory[invenId].id = -1;
            }
        }
        soltElement();
        
    }
    public static void removeItem(int id, int Count)
    {
        int invenId = -1;
        int removeCount = Count;
        int tempCount = 0;
        while (removeCount != 0)
        {
            tempCount++;
            for (int i = 0; i < getItemInventorySize(); i++)
            {
                if (ItemInventory[i].id == id)
                {
                    invenId = i;
                    break;
                }
            }
            if (invenId != -1)
            {
                removeCount--;
                ItemInventory[invenId].Count -= 1;
                if (ItemInventory[invenId].Count == 0)
                {
                    ItemInventory[invenId].NodeType = NodeType.Empty;
                    ItemInventory[invenId].id = -1;
                }
            }
            if (tempCount > 10000000)
            {
                Debug.Log("error");
                break;
            }
        }
        if(GameObject.Find("GameManager")) GameObject.Find("GameManager").GetComponent<ForgeManager>().UnselectedItem();
        soltItem();
    }
    public static void returnEquipment(int id)
    {
        int invenId = -1;
            for (int i = 0; i < getEquipmentSize(); i++)
            {
                if (id == i)
                {
                    invenId = i;
                    break;
                }
            }
            if (invenId != -1)
            {
            Debug.Log(getEquipmentID(invenId));
                    setItem(getEquipmentID(invenId));
                    EquipmentInventory[invenId].NodeType = NodeType.Empty;
                    EquipmentInventory[invenId].percent = 0;
                    EquipmentInventory[invenId].id = -1;

            for (int i = 0; i < EquipmentInventorySize; i++)
            {
                if (i == EquipmentInventorySize - 1)
                {
                    EquipmentInventory[i] = new EquipmentItemNode(-1, 0, NodeType.Empty);
                }
                else
                {
                    if (EquipmentInventory[i].NodeType == NodeType.Empty)
                    {
                        EquipmentInventory[i] = EquipmentInventory[i + 1];
                        EquipmentInventory[i + 1] = new EquipmentItemNode(-1, 0, NodeType.Empty);
                    }
                }
            }
        }
    }
    public static void removeEquipment(int id)
    {
        int invenId = -1;
        for (int i = 0; i < getEquipmentSize(); i++)
        {
            if (id == i)
            {
                invenId = i;
                break;
            }
        }
        if (invenId != -1)
        {
            EquipmentInventory[invenId].NodeType = NodeType.Empty;
            EquipmentInventory[invenId].percent = 0;
            EquipmentInventory[invenId].id = -1;

            for (int i = 0; i < EquipmentInventorySize; i++)
            {
                if (i == EquipmentInventorySize - 1)
                {
                    EquipmentInventory[i] = new EquipmentItemNode(-1, 0, NodeType.Empty);
                }
                else
                {
                    if (EquipmentInventory[i].NodeType == NodeType.Empty)
                    {
                        EquipmentInventory[i] = EquipmentInventory[i + 1];
                        EquipmentInventory[i + 1] = new EquipmentItemNode(-1, 0, NodeType.Empty);
                    }
                }
            }
        }
    }



    public static void removeMakingItem(int id)
    { 
        MakingItem[id].NodeType = NodeType.Empty;
        MakingItem[id].Count = 0;
        MakingItem[id].id = -1;
        MakingItem[id].StartTime = "";


    }

    public static void soltItem()
    {
        for (int i = 0; i < ItemInventorySize; i++)
        {
            if (i == ItemInventorySize - 1)
            {
               // ItemInventory[i] = new InventoryItemNode(-1,0,NodeType.Empty);
            }
            else
            {
                if (ItemInventory[i].NodeType == NodeType.Empty)
                {
                    ItemInventory[i] = ItemInventory[i + 1];
                    ItemInventory[i + 1] = new InventoryItemNode(-1, 0, NodeType.Empty);
                }
            }
        }
    }

    public static void soltElement()
    {
        for (int i = 0; i < ElementInventorySize; i++)
        {
            if (i == ElementInventorySize - 1)
            {
                //ElementInventory[i] = new InventoryElementNode(-1, 0, NodeType.Empty);
            }
            else
            {
                if (ElementInventory[i].NodeType == NodeType.Empty)
                {
                    ElementInventory[i] = ElementInventory[i + 1];
                    ElementInventory[i + 1] = new InventoryElementNode(-1, 0, NodeType.Empty);
                }
            }
        }
    }
    public static void setItem(int id)
    {
        bool isHave = false;
        int InsertNode = -1;
        for (int i = 0; i < ItemInventorySize; i++)
        {
            if (ItemInventory[i].NodeType == NodeType.Item)
            {
                if (ItemInventory[i].id == id)
                {
                    InsertNode = i;
                    isHave = true;
                    break;
                }
            }
            else if (ItemInventory[i].NodeType == NodeType.Empty)
            {
                InsertNode = i;
                break;
            }
        }
        if (InsertNode != -1)
        {
            if (true == isHave)
            {
                ItemInventory[InsertNode].Count++;
                if (ItemInventory[InsertNode].Count > 999) ItemInventory[InsertNode].Count = 999;
            }
            else
            {
                ItemInventory[InsertNode].id = id;
                ItemInventory[InsertNode].NodeType = NodeType.Item;
                ItemInventory[InsertNode].Count = 1;
                setAbleBook(id);
            }
        }
        soltItem();
    }

    
    public static void setMaking(int id, int count, DateTime start)
    {
        
        int InsertNode = -1;
        for (int i = 0; i < 5; i++)
        {
            if (MakingItem[i].NodeType == NodeType.Empty)
            {
                InsertNode = i;
                break;
            }
        }
        if (InsertNode != -1 && InsertNode < 5)
        {
            MakingItem[InsertNode].id = id;
            MakingItem[InsertNode].NodeType = NodeType.Item;
            MakingItem[InsertNode].Count = count;
            MakingItem[InsertNode].StartTime = GameData.ToString(start);

        }
    }

    public static void refreshMaking(int index,int count, DateTime start)
    {
        if (index !=-1)
        {
            MakingItem[index].Count = count;
            MakingItem[index].StartTime = GameData.ToString(start);
        }
    }



    public static void setEquipment(int id)
    {

        int InsertNode = -1;
        for (int i = 0; i < EquipmentInventorySize; i++)
        {
            if (EquipmentInventory[i].NodeType == NodeType.Empty)
            {
                InsertNode = i;
                break;
            }
        }
        if (InsertNode != -1 && InsertNode < EquipmentSize)
        {
            EquipmentInventory[InsertNode].id = id;
            EquipmentInventory[InsertNode].NodeType = NodeType.Item;
            EquipmentInventory[InsertNode].percent= 100;
            removeItem(id, 1);

            if (GameObject.Find("GameManager"))
            {
                GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.sprite = Resources.Load<Sprite>("char/nill");
                GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomName.text = "";
                GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomContent.text = "";
                GameObject.Find("GameManager").GetComponent<ForgeManager>().InvenZoomImage.transform.parent.FindChild("Equipment").gameObject.SetActive(false);
            }
            soltItem();
        }
    }
    public static int getEquipmentID(int index)
    {
        return EquipmentInventory[index].id;
    }
    public static int getEquipmentPercent(int index)
    {
        return EquipmentInventory[index].percent;
    }
    public static void setEquipmentPercent(int index, int Spercent)
    {
        if (Spercent < 0) Spercent = 0;
        if (Spercent > 100) Spercent = 100;
        EquipmentInventory[index].percent = Spercent;
      
    }
    public static void setItem(int id, int Count)
    {
        bool isHave = false;
        int InsertNode = -1;
        for (int i = 0; i < ItemInventorySize; i++)
        {
            if (ItemInventory[i].NodeType == NodeType.Item)
            {
                if (ItemInventory[i].id == id)
                {
                    InsertNode = i;
                    isHave = true;
                    break;
                }
            }
            else if (ItemInventory[i].NodeType == NodeType.Empty)
            {
                InsertNode = i;
                break;
            }
        }
        if (InsertNode != -1)
        {
            if (true == isHave)
            {
                ItemInventory[InsertNode].Count += Count;
                if (ItemInventory[InsertNode].Count > 999) ItemInventory[InsertNode].Count = 999;
            }
            else
            {
                ItemInventory[InsertNode].id = id;
                ItemInventory[InsertNode].NodeType = NodeType.Item;
                ItemInventory[InsertNode].Count = Count;
                if (ItemInventory[InsertNode].Count > 999) ItemInventory[InsertNode].Count = 999;
                setAbleBook(id);
            }
        }
    }
    public static void setElement(int id, int Count)
    {
        soltElement();
        int InsertNode = -1;
        bool has = false;
        for (int i = 0; i < ElementInventorySize; i++)
        {
            if (ElementInventory[i].id == id)
            {
                InsertNode = i;
                has = true;
                break;
            }
        }
        if (has == false) { 
        for (int i = 0; i < ElementInventorySize; i++)
        {
            if (ElementInventory[i].NodeType == NodeType.Empty)
            {
                InsertNode = i;
                break;
            }
        }
        }


        if (has)
        {
            if (InsertNode != -1)
            {
                ElementInventory[InsertNode].id = id;
                ElementInventory[InsertNode].Count += Count;
                if (ElementInventory[InsertNode].Count > 999) ElementInventory[InsertNode].Count = 999;
                ElementInventory[InsertNode].NodeType = NodeType.Element;

                setAbleEleBook(id);
            }
            else
            {
                Debug.Log("Full");
            }
        }
        else
        {
            if (InsertNode != -1)
            {
                ElementInventory[InsertNode].id = id;
                ElementInventory[InsertNode].Count = Count;
                ElementInventory[InsertNode].NodeType = NodeType.Element;
                if (ElementInventory[InsertNode].Count > 999) ElementInventory[InsertNode].Count = 999;
                Debug.Log("Done");
                setAbleEleBook(id);
            }
            else
            {
                Debug.Log("Full");
            }
        }
    }
    public static int getMoney()
    {
        return Money;
    }
    public static int getSpaceNum()
    {
        return SpaceNum;
    }
    public static void setSpaceNum(int Num)
    {
        SpaceNum = Num;
    }

    public static int getSpaceOpen()
    {
        return SpaceOpen;
    }
    public static void OpenSpace(int Num)
    {
        SpaceOpen = Num;
        if (GameObject.Find("DayManager"))
        {
            Debug.Log("Dd");
            GameObject.Find("DayManager").transform.FindChild("PlanetAni").GetComponent<Animator>().Play("PlanetOpen");
            GameData.setSpaceNum(GameData.getSpaceOpen());
        }
            SaveGame();
    }
    public static int getEmerald()
    {
        return Emerald;
    }
    public static int getStar()
    {
        return Star;
    }

    public static int getHeart_p()
    {
        return Heart_p;
    }
    public static string getName()
    {
        return Name;
    }
    public static void addMoney(int Money)
    {
        GameData.Money += Money;
        SaveGame();
    }


    public static void useMoney(int Money)
    {
        GameData.Money -= Money;
        SaveGame();
    }

    public static void addEmerald(int Emerald)
    {
        GameData.Emerald += Emerald;
        SaveGame();
    }


    public static void useEmerald(int Emerald)
    {
        GameData.Emerald -= Emerald;
        if (GameData.Emerald < 0) GameData.Emerald = 0;
        SaveGame();
    }
    public static void addStar(int Star)
    {
        GameData.Star += Star;
        SaveGame();
    }
    public static void minusStar(int Star)
    {
        GameData.Star -= Star;
        if (GameData.Star < 0) GameData.Star = 0;
        SaveGame();
    }
    public static void addHeart_p(int heart_p)
    {
        GameData.Heart_p += heart_p;
        if (GameData.Heart_p > 5) GameData.Heart_p = 5;
        GameData.SetHTimeNow();
        SaveGame();
    }
    public static void minusHeart_p(int heart_p)
    {

        if (GameData.Heart_p >= 5) { GameData.Heart_p = 5; GameData.SetHTimeNow(); }
        GameData.Heart_p -= heart_p;
        if (GameData.Heart_p < 0) GameData.Heart_p = 0;
        SaveGame();
    }
    public static void onSale(int id, int index)
    {
        ShopSaleItemInfo[index] = ItemInventory[id].id;
        ItemInventory[id].Count--;
        if (ItemInventory[id].Count == 0)
        {
            ItemInventory[id].NodeType = NodeType.Empty;
        }
        GameData.soltItem();
    }
    public static void upExp(int exp)
    {

        if (getLevel() < MaxLevel)
        {
            Exp += exp;

            while (Exp >= LevelUpExp[Level - 1])
            {
                Exp -= LevelUpExp[Level - 1];
                LevelUp();
            }
        }
    }
    public static void LevelUp()
    {
        Level++;
        if (Level > MaxLevel) Level = MaxLevel;
        else
        {
            GameObject.Find("LevelUpCanvas").GetComponent<LevelUpCanvas>().SetLevelUpCanvas();
            MaxHP = (int)(MaxHP * 1.2f);
            HP = MaxHP;
            power += 5;
            deffens += 5;
            SaveGame();
        }
    }
    public static int getExp(){
        return Exp;
    }
    public static int getLevel()
    {
        return Level;
    }
    public static void getInvenItemData()
    {
        for (int i = 0; i < ItemInventorySize; i++)
        {
            if (ItemInventory[i].NodeType == NodeType.Item)
            {
                Debug.Log(ItemList[ItemInventory[i].id].name);
            }
        }
    }
    public static void getInvenElementData()
    {
        for (int i = 0; i < ElementInventorySize; i++)
        {
            if (ElementInventory[i].NodeType == NodeType.Element)
            {
                Debug.Log(ElementList[ElementInventory[i].id].name);
                Debug.Log(ElementInventory[i].Count);
            }
        }
    }
    public static void setGetInfo()
    {
        
        for (int i = 0; i < GameData.StageList.Count; i++)
        {
            Stage nowStage = GameData.StageList[i];
            string[] Droptmp = nowStage.drop.Split('|');
            

            for (int j = 0; j < Droptmp.Length; j++)
            {
                
                string[] tmp = Droptmp[j].Split('_');

                if (tmp[0].Equals("El"))
                {

                    int id = int.Parse(tmp[1]);
                        Element tmp2 = ElementList[id];

                    string stageNum = ((nowStage.id / 5) + 1) + "-" + ((nowStage.id - ((nowStage.id / 5) * 5)) + 1) +" ";
                    if (ElementList[id].get!="") tmp2.get += ", " + stageNum+ nowStage.name;
                    else tmp2.get += "" + stageNum + nowStage.name;

                    ElementList[id] = tmp2;
                    
                }
               
                
            }
        }
        for (int i = 0; i < GameData.MonsterList.Count; i++)
        {
            Monster nowMonster = GameData.MonsterList[i];
            string[] Droptmp = nowMonster.reward.Split('_');


            string SpaceName = "";


            if (i < 67) SpaceName = "(프라이)";
            if (i < 45) SpaceName = "(토우)";
            if (i < 24) SpaceName = "(오도르)";

            for (int j = 0; j < Droptmp.Length; j++)
            {

                string[] tmp = Droptmp[j].Split('|');
                
                if (tmp[0].Equals("El"))
                {
                    int id = int.Parse(tmp[1]);
                    Element tmp2 = ElementList[id];
                    

                    if (ElementList[id].get != "") tmp2.get += ", " + nowMonster.name +SpaceName;
                    else tmp2.get += "" + nowMonster.name + SpaceName;

                    ElementList[id] = tmp2;

                }


            }
        }


        for (int id = 0; id < ElementList.Count; id++)
        {
            Element tmp2 = ElementList[id];

            string BoxName = "";


            if (ElementList[id].value == 0) BoxName = "초급 재료박스";
            else if (ElementList[id].value == 1) BoxName = "중급 재료박스";
            else if (ElementList[id].value == 2) BoxName = "고급 재료박스";


            if (ElementList[id].get != "") tmp2.get += ", " + BoxName;
            else tmp2.get += "" + BoxName;

            ElementList[id] = tmp2;
        }

    }
    public static bool chkQuest(int i)
    {

        bool result = false;
           
            int Complete;
            int Count = 0;

            string[] questGoals = QuestList[QuestInProgress[i]].goal.Split('_');
            Complete = questGoals.Length;
            for (int j = 0; j < questGoals.Length; j++)
            {
                string[] questData = questGoals[j].Split('|');

                if (questData[0].Equals("IT"))
                {
                    if (int.Parse(questData[2]) <= getItemNum(int.Parse(questData[1])))
                    {
                        Count++;
                    }
                }
                else if (questData[0].Equals("BI"))//가구 설치
                {
                    if (shopBuildCount(int.Parse(questData[1])) >= int.Parse(questData[2]))
                    {
                        Count++;
                    }
                }
                else if (questData[0].Equals("SE"))//가구에 아이템 설치
                {
                    for (int k = 0; k < ShopInfoSize; k++)
                    {
                        if (ShopSaleItemInfo[k] == int.Parse(questData[1]))
                        {
                            Count++;
                            break;
                        }
                    }
                }
                else if (questData[0].Equals("GO"))//Scene이동
                {

                    if (SceneManager.GetActiveScene().name.Equals(questData[1]))
                    {
                        Count++;
                    }
            }
                else if (questData[0].Equals("CL"))//Scene이동
                {
                Count++;
            }
            else if (questData[0].Equals("EL"))//
                {
                    if (getElementNum(int.Parse(questData[1])) >= int.Parse(questData[2]))
                    {
                        Count++;
                    }
                }
            }
            if (Complete == Count)
            {
                result = true;
            }
        
        return result;
    }

    public static int getElementNum(int id)
    {
        int result = 0;

        for (int i = 0; i < ElementInventorySize; i++)
        {
            if (ElementInventory[i].id == id)
            {
                result += ElementInventory[i].Count;
            }
        }

            return result;
    }

    public static string getInteriorName(int id) { return InteriorList[id].name; }
    public static string getInteriorContent(int id) { return InteriorList[id].content; }
    public static int getInteriorStandCoin(int id) { return InteriorList[id].standcoin; }
    public static int getInteriorWallCoin(int id) { return InteriorList[id].wallcoin; }
    public static int getInteriorDoorCoin(int id) { return InteriorList[id].doorcoin; }
    public static int getInteriorCounterCoin(int id) { return InteriorList[id].countercoin; }
    public static int getInteriorLevel(int id) { return InteriorList[id].level; }



    public static int shopBuildCount(int id)
    {
        int result = 0;
        for (int i = 0; i < ShopInfoSize; i++)
        {
            if (ShopInfo[i] == id)
            {
                result++;
            }
        }
        return result;
    }

    public static void upShopFloor()
    {
        ShopFloorSize++;
        ShopInfoSize = ShopFloorSize * 4;

        int[] TempSinfo = new int[ShopInfoSize];
        int[] TempIinfo = new int[ShopInfoSize];
        int[] TempWallInfo = new int[ShopFloorSize];
        
        for (int i=0; i<ShopInfoSize; i++)
        {
            if (i < ShopInfoSize / ShopFloorSize)
            {
                TempSinfo[i] = ShopInfo[i];
                TempIinfo[i] = ShopSaleItemInfo[i];
            }
            else
            {
                
                TempSinfo[i] = -1;
                TempIinfo[i] = -1;
            }
        }
        for (int i = 0; i < ShopFloorSize; i++)
        {
            if (i<=ShopFloorSize-2)
            {
                TempWallInfo[i] = ShopWallInfo[i];
            }
            else
            {
                TempWallInfo[i] = 0;
            }
        }
        ShopInfo = TempSinfo;
        ShopSaleItemInfo = TempIinfo;
        ShopWallInfo = TempWallInfo;

        for (int i = 0; i < ShopInfoSize; i++)
        {
           
            Debug.Log(i+","+ShopInfo[i]+"+"+ ShopSaleItemInfo[i]);
        }

    }
    public static int getFloor()
    {
        return ShopFloorSize;
    }
    public static int getShopWallInfo(int index)
    {
        return ShopWallInfo[index];
    }
    public static int setShopWallInfo(int index, int id)
    {
        return ShopWallInfo[index] = id;
    }

    public static int getShopDoorInfo()
    {
        return ShopDoorInfo;
    }
    public static int getShopCounterInfo()
    {
        return ShopCounterInfo;
    }
    public static int setShopDoorInfo( int id)
    {
        return ShopDoorInfo = id;
    }
    public static int setShopCounterInfo(int id)
    {
        return ShopCounterInfo = id;
    }

    public static void setEvent(int id)
    {
        if (GameObject.Find("TalkCanvas"))
            GameObject.Find("TalkCanvas").GetComponent<EventManager>().setEvent(id);
    }

    public static void setQuestReady(int id)
    {
        if(id!=-1)
        GameData.QuestInReady.Add(id);
        if (GameObject.Find("ShopManager")) GameObject.Find("QuestDataDialog").GetComponent<questDataDialog>().isNew = true;
    }


    public static void CompleteQuest(int id)
    {
        int questID = id;

        QuestInProgress.Remove(id);
        QuestInComplete.Add(id);

        string []reward = QuestList[id].reward.Split('_');

        string[] questGoals = QuestList[questID].goal.Split('_');
        for (int j = 0; j < questGoals.Length; j++)
        {
          string[] questData = questGoals[j].Split('|');

          if (questData[0].Equals("IT"))
          {
              removeItem(int.Parse(questData[1]), int.Parse(questData[2]));
            }
            else if (questData[0].Equals("EL"))
            {
                useElement(int.Parse(questData[1]), int.Parse(questData[2]));
            }
        }
        

        for (int i = 0; i < reward.Length; i++)
        {
            string[] Rtext = reward[i].Split('|');
            if (Rtext[0].Equals("nq"))
            {
                if (GameObject.Find("QuestCanvas"))
                    GameObject.Find("QuestCanvas").GetComponent<QuestFactory>().makeQuest(int.Parse(Rtext[1]));
            }
            else if (Rtext[0].Equals("exp"))
            {
                upExp(int.Parse(Rtext[1]));
            }
            else if (Rtext[0].Equals("mon"))
            {
                addMoney(int.Parse(Rtext[1]));
            }
            else if (Rtext[0].Equals("op"))
            {
                OpenSpace(int.Parse(Rtext[1]));
            }
            else if (Rtext[0].Equals("bf"))
            {
                setAbleMake(int.Parse(Rtext[1]));
            }
            if (Rtext[0].Equals("IT"))
            {
                setItem(int.Parse(Rtext[1]), int.Parse(Rtext[2]));
            }
            if (Rtext[0].Equals("El"))
            {
                setElement(int.Parse(Rtext[1]), int.Parse(Rtext[2]));
            }
        }

        if(QuestList[id].nextQuest != -1) setQuestReady(QuestList[id].nextQuest);
    }

    public static int getItemNum(int id)
    {
        int result = 0;

        for (int i = 0; i < ItemInventorySize; i++)
        {
            if (ItemInventory[i].id == id)
                result+=ItemInventory[i].Count;
        }
        return result;
    }

    public static void addChain(string name)
    {
        Debug.Log(ChainSize);
        ChainList.Add(new Chain(name, "", 0, false));
        ChainSize++;
        SaveGame();
    }
    public static void startChain(int index, DateTime Stime, int money)
    {
        ChainList[index] = new Chain(ChainList[index].name, ToString(Stime), money, true);
        SaveGame();
    }

    public static void stopChain(int index)
    {
        addMoney(ChainList[index].Money);
        ChainList[index] = new Chain(ChainList[index].name, "", 0, false);
        SaveGame();
    }

    public static int getChainSize()
    {
        return ChainSize;
    }

 



    public static void SaveGame()
    {


        SetTimeNow();
        string Name = "save.sav";

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            Name = Path.Combine(Path.Combine(path, "Documents"), Name);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            Name = Path.Combine(path, Name);
        }
        else
        {
        }

        var fileName = Name;

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        var sr = File.CreateText(fileName);

        sr.WriteLine("N " + GameData.Name);
        sr.WriteLine("M " + GameData.Money);
        sr.WriteLine("SPN " + GameData.SpaceNum);
        sr.WriteLine("SPO " + GameData.SpaceOpen);
        sr.WriteLine("EM " + GameData.Emerald);
        sr.WriteLine("Star " + GameData.Star);
        sr.WriteLine("H_p " + GameData.Heart_p);
        sr.WriteLine("L " + GameData.Level);
        sr.WriteLine("E " + GameData.Exp);
        sr.WriteLine("Hp " + GameData.MaxHP);
        sr.WriteLine("MSt " + GameData.MaxStamina);
        sr.WriteLine("St " + GameData.Stamina);
        sr.WriteLine("Is " + GameData.ItemInventorySize);
        sr.WriteLine("Es " + GameData.ElementInventorySize);
        sr.WriteLine("Eqis " + GameData.EquipmentInventorySize);
        sr.WriteLine("Eqs " + GameData.EquipmentSize);
        sr.WriteLine("Chs " + GameData.ChainSize);
        sr.WriteLine("Day " + GameData.Day);

        sr.WriteLine("BaV " + GameData.BackSoundV);
        sr.WriteLine("EfV " + GameData.EffectSoundV);

        sr.WriteLine("EqIt " + GameData.EquippedItem);
        sr.WriteLine("EqAr " + GameData.EquippedArmor);
        sr.WriteLine("EqAc " + GameData.EquippedAcc);
        sr.WriteLine("P " + GameData.power);
        sr.WriteLine("S " + GameData.speed);
        sr.WriteLine("D " + GameData.deffens);

        sr.WriteLine("Sis " + GameData.ShopInfoSize);
        sr.WriteLine("SdInfo " + GameData.ShopDoorInfo);
        sr.WriteLine("ScInfo " + GameData.ShopCounterInfo);
        sr.WriteLine("Sfs " + GameData.ShopFloorSize);
        sr.WriteLine("STime " + STimeToString());
        sr.WriteLine("HTime " + ToString(HTime));
        sr.WriteLine("ADTime " + ToString(ADTime));



        for (int i = 0; i < ShopInfoSize; i++)
        {
            sr.WriteLine("Sh " + i + " " + GameData.ShopInfo[i]);
            sr.WriteLine("ShItem " + i + " " + GameData.ShopSaleItemInfo[i]);
        }
        for (int i = 0; i < ShopFloorSize; i++)
        {
            sr.WriteLine("SwInfo " + i + " " + GameData.ShopWallInfo[i]);
        }

        for (int i = 0; i < ItemInventorySize; i++)
        {
            if (GameData.ItemInventory[i].NodeType == NodeType.Empty)
            {
                sr.WriteLine("It " + i + " " + -1 + " " + 0 + " " + "No");
            }
            else
            {
                sr.WriteLine("It " + i + " " + GameData.ItemInventory[i].id + " " + GameData.ItemInventory[i].Count + " " + "Yes");
            }
        }
        for (int i = 0; i < ElementInventorySize; i++)
        {
            if (GameData.ElementInventory[i].NodeType == NodeType.Empty)
            {
                sr.WriteLine("El " + i + " " + GameData.ElementInventory[i].id + " " + GameData.ElementInventory[i].Count + " " + "No");

            }
            else
            {
                sr.WriteLine("El " + i + " " + GameData.ElementInventory[i].id + " " + GameData.ElementInventory[i].Count + " " + "Yes");

            }
        }
        for (int i = 0; i < EquipmentInventorySize; i++)
        {
            if (GameData.EquipmentInventory[i].NodeType == NodeType.Empty)
            {
                sr.WriteLine("Eqi " + i + " " + GameData.EquipmentInventory[i].id + " " + GameData.EquipmentInventory[i].percent + " " + "No");

            }
            else
            {
                sr.WriteLine("Eqi " + i + " " + GameData.EquipmentInventory[i].id + " " + GameData.EquipmentInventory[i].percent + " " + "Yes");

            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (GameData.MakingItem[i].NodeType == NodeType.Empty)
            {
                sr.WriteLine("Mil " + i + " " + GameData.MakingItem[i].id + " " + GameData.MakingItem[i].Count + " " + GameData.MakingItem[i].StartTime + " " + "No");

            }
            else
            {
                sr.WriteLine("Mil " + i + " " + GameData.MakingItem[i].id + " " + GameData.MakingItem[i].Count + " " + GameData.MakingItem[i].StartTime + " " + "Yes");

            }
        }

        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].makeAble == true)
                sr.WriteLine("ITMA " + i + " " + ItemList[i].makeAble);
            if (ItemList[i].bookAble == true)
                sr.WriteLine("ITBO " + i + " " + ItemList[i].bookAble);

        }

        for (int i = 0; i < ElementList.Count; i++)
        {
            if (ElementList[i].bookAble == true)
                sr.WriteLine("ELBO " + i + " " + ElementList[i].bookAble);

        }
        for (int i = 0; i < ChainSize; i++)
        {
            if(GameData.ChainList[i].isIng)
           sr.WriteLine("Chain " + i + " " + GameData.ChainList[i].name + " " + GameData.ChainList[i].STime + " " + GameData.ChainList[i].Money + " " + "Yes");
            else
           sr.WriteLine("Chain " + i + " " + GameData.ChainList[i].name + " " + GameData.ChainList[i].STime + " " + GameData.ChainList[i].Money + " " + "No");


        }
        for (int i = 0; i < QuestInProgress.Count; i++)
        {
            sr.WriteLine("QIP " + QuestInProgress[i]);
        }
        for (int i = 0; i < QuestInComplete.Count; i++)
        {
            sr.WriteLine("QIC " + QuestInComplete[i]);
        }
        for (int i = 0; i < QuestInReady.Count; i++)
        {
            sr.WriteLine("QIR " + QuestInReady[i]);
        }

        if (ingEvent != -1)
        {
            sr.WriteLine("iE " + ingEvent);
        }
        sr.Close();

    }

    public static void LoadGame()
    {
        try
        {
            QuestInProgress = new List<int>();
            QuestInComplete = new List<int>();
            QuestInReady = new List<int>();
            ChainList = new List<Chain>();

            string Name = "save.sav";

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
                path = path.Substring(0, path.LastIndexOf('/'));
                Name = Path.Combine(Path.Combine(path, "Documents"), Name);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                string path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                Name = Path.Combine(path, Name);
            }
            else
            {
            }
            StreamReader sr = new StreamReader(Name, Encoding.UTF8, true);

            MakeList = new List<int>();
            while (sr.Peek() > -1)
            {
                string[] text;
                text = sr.ReadLine().Split(' ');
                if (text[0].Equals("N"))
                {
                    GameData.Name = text[1];
                }
           

                else if (text[0].Equals("M"))
                {
                    GameData.Money = int.Parse(text[1]);
                }
                else if (text[0].Equals("SPN"))
                {
                    GameData.SpaceNum = int.Parse(text[1]);
                }

                else if (text[0].Equals("SPO"))
                {
                    GameData.SpaceOpen = int.Parse(text[1]);
                }
                else if (text[0].Equals("EM"))
                {
                    GameData.Emerald = int.Parse(text[1]);
                }
                else if (text[0].Equals("Star"))
                {
                    GameData.Star = int.Parse(text[1]);
                }
                else if (text[0].Equals("H_p"))
                {
                    GameData.Heart_p = int.Parse(text[1]);
                }
                else if (text[0].Equals("L"))
                {
                    GameData.Level = int.Parse(text[1]);
                }
                else if (text[0].Equals("E"))
                {
                    GameData.Exp = int.Parse(text[1]);
                }
                else if (text[0].Equals("Hp"))
                {
                    GameData.MaxHP = int.Parse(text[1]);
                }
                else if (text[0].Equals("MSt"))
                {
                    GameData.MaxStamina = int.Parse(text[1]);
                }
                else if (text[0].Equals("St"))
                {
                    GameData.Stamina = int.Parse(text[1]);
                }
                else if (text[0].Equals("BaV"))
                {
                    GameData.BackSoundV = float.Parse(text[1]);
                }
                else if (text[0].Equals("EfV"))
                {
                    GameData.EffectSoundV = float.Parse(text[1]);
                }
                else if (text[0].Equals("Is"))
                {
                    GameData.ItemInventorySize = int.Parse(text[1]);
                    GameData.ItemInventory = new InventoryItemNode[GameData.ItemInventorySize];
                    
                }
                else if (text[0].Equals("Es"))
                {
                    GameData.ElementInventorySize = int.Parse(text[1]);
                    GameData.ElementInventory = new InventoryElementNode[GameData.ElementInventorySize];
                }
                else if (text[0].Equals("Eqs"))
                {
                    GameData.EquipmentSize = int.Parse(text[1]);
                }
                else if (text[0].Equals("Chs"))
                {
                    GameData.ChainSize = int.Parse(text[1]);
                }
                else if (text[0].Equals("Eqis"))
                {
                    GameData.EquipmentInventorySize = int.Parse(text[1]);
                    GameData.EquipmentInventory = new EquipmentItemNode[GameData.EquipmentInventorySize];
                    GameData.MakingItem = new MakingItemNode[5];

                }
                else if (text[0].Equals("STime"))
                {
                    GameData.STime = ToTime(text[1]);
                    
                }

                else if (text[0].Equals("HTime"))
                {
                    GameData.HTime = ToTime(text[1]);

                }
                else if (text[0].Equals("ADTime"))
                {
                    GameData.ADTime = ToTime(text[1]);

                }
                else if (text[0].Equals("Day"))
                {
                    GameData.Day = int.Parse(text[1]);

                }


                else if (text[0].Equals("It"))
                {
                    if (text[4].Equals("No"))
                        GameData.ItemInventory[int.Parse(text[1])] = new InventoryItemNode(int.Parse(text[2]), int.Parse(text[3]), NodeType.Empty);
                    else
                    {
                        GameData.ItemInventory[int.Parse(text[1])] = new InventoryItemNode(int.Parse(text[2]), int.Parse(text[3]), NodeType.Item);
                    }
                }

                else if (text[0].Equals("El"))
                {
                    if (text[4].Equals("No"))
                        GameData.ElementInventory[int.Parse(text[1])] = new InventoryElementNode(int.Parse(text[2]), int.Parse(text[3]), NodeType.Empty);
                    else
                        GameData.ElementInventory[int.Parse(text[1])] = new InventoryElementNode(int.Parse(text[2]), int.Parse(text[3]), NodeType.Element);
                }
                else if (text[0].Equals("Eqi"))
                {
                    if (text[4].Equals("No"))
                        GameData.EquipmentInventory[int.Parse(text[1])] = new EquipmentItemNode(int.Parse(text[2]), int.Parse(text[3]), NodeType.Empty);
                    else
                        GameData.EquipmentInventory[int.Parse(text[1])] = new EquipmentItemNode(int.Parse(text[2]), int.Parse(text[3]), NodeType.Item);
                }
                else if (text[0].Equals("Mil"))
                {
                    

                    if (text[5].Equals("No"))
                        GameData.MakingItem[int.Parse(text[1])] = new MakingItemNode(int.Parse(text[2]), int.Parse(text[3]), text[4], NodeType.Empty);
                    else
                    {
                        GameData.MakingItem[int.Parse(text[1])] = new MakingItemNode(int.Parse(text[2]), int.Parse(text[3]), text[4], NodeType.Item);
                    }
                    
                }
                else if (text[0].Equals("Chain"))
                {
                    if (text[5].Equals("No"))
                        GameData.ChainList.Add(new Chain(text[2], text[3], int.Parse(text[4]), false));
                    else
                    {
                        GameData.ChainList.Add(new Chain(text[2], text[3], int.Parse(text[4]), true));
                    }
                }
                else if (text[0].Equals("P"))
                {
                    GameData.power = int.Parse(text[1]);
                }
                else if (text[0].Equals("S"))
                {
                    GameData.speed = int.Parse(text[1]);
                }
                else if (text[0].Equals("D"))
                {
                    GameData.deffens = int.Parse(text[1]);
                }

                else if (text[0].Equals("QIP"))
                {
                    GameData.QuestInProgress.Add(int.Parse(text[1]));
                }
                else if (text[0].Equals("QIC"))
                {
                    GameData.QuestInComplete.Add(int.Parse(text[1]));
                }
                else if (text[0].Equals("QIR"))
                {
                    GameData.setQuestReady(int.Parse(text[1]));
                }

                else if (text[0].Equals("EqIt"))
                {
                    GameData.EquippedItem = int.Parse(text[1]);
                }

                else if (text[0].Equals("EqAr"))
                {
                    GameData.EquippedArmor = int.Parse(text[1]);
                }

                else if (text[0].Equals("EqAc"))
                {
                    GameData.EquippedAcc = int.Parse(text[1]);
                }
                else if (text[0].Equals("Sis"))
                {
                    GameData.ShopInfoSize = int.Parse(text[1]);
                    ShopInfo = new int[ShopInfoSize];
                    ShopSaleItemInfo = new int[ShopInfoSize];
                }
                else if (text[0].Equals("Sfs"))
                {
                    GameData.ShopFloorSize = int.Parse(text[1]);
                    ShopWallInfo = new int[ShopFloorSize];
                }

               
                else if (text[0].Equals("Sh"))
                {
                    ShopInfo[int.Parse(text[1])] = int.Parse(text[2]);
                }
                else if (text[0].Equals("ShItem"))
                {
                    ShopSaleItemInfo[int.Parse(text[1])] = int.Parse(text[2]);
                }
                else if (text[0].Equals("SwInfo"))
                {
                    ShopWallInfo[int.Parse(text[1])] = int.Parse(text[2]);
                }

                else if (text[0].Equals("SdInfo"))
                {
                    ShopDoorInfo = int.Parse(text[1]);
                }

                else if (text[0].Equals("ScInfo"))
                {
                    ShopCounterInfo = int.Parse(text[1]);
                }

                else if (text[0].Equals("ITMA"))
                {
                    if (text[2].Equals("True"))
                    {
                        Item tmp = ItemList[int.Parse(text[1])];
                        tmp.makeAble = true;
                        ItemList[int.Parse(text[1])] = tmp;
                    }
                }
                else if (text[0].Equals("ITBO"))
                {
                    if (text[2].Equals("True"))
                    {
                        Item tmp = ItemList[int.Parse(text[1])];
                        tmp.bookAble = true;
                        ItemList[int.Parse(text[1])] = tmp;
                    }
                }
                else if (text[0].Equals("ELBO"))
                {
                    if (text[2].Equals("True"))
                    {
                        Element tmp = ElementList[int.Parse(text[1])];
                        tmp.bookAble = true;
                        ElementList[int.Parse(text[1])] = tmp;
                    }
                }
                else if (text[0].Equals("iE"))
                {
                    ingEvent = int.Parse(text[1]);
                }
            }
            HP = MaxHP;
            OnGame = true;
        }
        catch (Exception e)
        {
        }

        
    }

}
