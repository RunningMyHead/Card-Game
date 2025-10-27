using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WinUI : UIBase
{
    public Text Perfectkill;
    public cardtype bosstype;
    public card Newcard;
    GameObject NewCardobj;
    private void Awake()
    {
        //bosstype = new cardtype();
        Newcard = new card();
        GameObject NewCardobj = new GameObject();
        Register("Button1").onClick = OnButton1;//回复玩家百分之25的生命
        Register("Button2").onClick = OnButton2;//提高玩家5点血量上限
        Register("Button3").onClick = OnButton3;//移除摸牌堆1张卡
        Register("Button5").onClick = OnButton5;//移除弃牌堆1张卡
        Register("Button6").onClick = OnButton6;//添加对应boss的卡牌且数值1-10之间加入弃牌堆
        Register("Button7").onClick = OnButton7;//完美击杀，添加对应boss卡牌数值为12，加入弃牌堆
        Register("Button8").onClick = OnButton8;//退出游戏
        Register("Button9").onClick = OnButton9;//继续游戏
    }

    private void Start()
    {
        
    }

    private void OnButton1(GameObject obj, PointerEventData pDate)
    {
        //获取玩家最大生命值以及当前生命，向下取整增加生命
        //选择这个后Button2消失
        EventManager.Instance.CurHP += EventManager.Instance.MaxHP/4;
        if (EventManager.Instance.CurHP > EventManager.Instance.MaxHP)
        {
            EventManager.Instance.CurHP = EventManager.Instance.MaxHP;
        }
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHP();//刷新血条
        HideOnButton("Button1");
        HideOnButton("Button2");
    }
    private void OnButton2(GameObject obj, PointerEventData pDate)
    {
        //提高玩家最大生命值，不提升当前生命值
        EventManager.Instance.MaxHP += 5;
        EventManager.Instance.CurHP += 5;
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHP();
        //选择这个后Button1消失
        HideOnButton("Button1");
        HideOnButton("Button2");
    }
    private void OnButton3(GameObject obj, PointerEventData pDate)
    {
        //将所有弃牌罗列出来，选择一张删除
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CloseShow(FightCardManager.Instance.usedCardList);
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CardListData = false;      
    }
    private void OnButton5(GameObject obj, PointerEventData pDate)
    {
        //将所有摸牌罗列出来，选择一张删除
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CloseShow(FightCardManager.Instance.cardList);
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CardListData = true;
    }
    private void OnButton6(GameObject obj, PointerEventData pDate)
    {
        //获取上一个boss的种类，获得1-11之间的随机数，做成new card 添加到弃牌堆
        FightCardManager.Instance.usedCardList.Add(Newcard);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatesedCardCount();
        Destroy(NewCardobj.gameObject);
        HideOnButton("Button6");
    }
    private void OnButton7(GameObject obj, PointerEventData pDate)
    {
        //只有完美击杀才出现的按钮
        //获取上一个boss的种类，赋值12的点数，做成new card 添加到弃牌堆
        //打出后立即删除
        card Newcard = new card();
        Newcard.type = bosstype;
        Newcard.value = 12;
        FightCardManager.Instance.usedCardList.Add(Newcard);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatesedCardCount();

        HideOnButton("Button7");
    }
    //8.退出游戏
    private void OnButton8(GameObject obj, PointerEventData pDate)
    {
        //需要打包成软件才能正常使用
        Application.Quit();
        //关闭longinUI界面  
    }
    private void OnButton9(GameObject obj, PointerEventData pDate)
    {
        Destroy(NewCardobj.gameObject);
        RightMoveAnimation();//隐藏WinUI
        //UIManager.Instance.GetUI<FightUI>("FightUI").HieAttackBtn();
        EventManager.Instance.ChangeType(eventtype.NotEvent);
    }

    //完美击杀时显示该按钮
    public void ShowOnButton7()
    {
        Perfectkill.gameObject.SetActive(true);
        transform.Find("Button7").gameObject.SetActive(true);
    }
    //不完美击杀时隐藏该按钮
    public void HideOnbutton7()
    {
        Perfectkill.gameObject.SetActive(false);
        transform.Find("Button7").gameObject.SetActive(false);
    }

    public void HideOnButton(string Button)
    {
        transform.Find(Button).gameObject.SetActive(false);
    }

    //显示1-6的所有按钮
    public void ShowAllButton()
    {
        transform.Find("Button1").gameObject.SetActive(true);
        transform.Find("Button2").gameObject.SetActive(true);
        transform.Find("Button3").gameObject.SetActive(true);
        transform.Find("Button5").gameObject.SetActive(true);
        transform.Find("Button6").gameObject.SetActive(true);
        Newcardcake();
    }

    public void Newcardcake()
    {
        Newcard.type = bosstype;
        Newcard.value = Random.Range(1, 11);
        NewCardobj = Instantiate(Resources.Load("Card/" + Newcard.type), transform) as GameObject;
        Text text = NewCardobj.transform.Find("Value/Text").GetComponent<Text>();
        text.text = Newcard.value.ToString();
        NewCardobj.GetComponent<RectTransform>().anchoredPosition = new Vector2(450,-40);
        NewCardobj.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 400);
    }
}
