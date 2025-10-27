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
        Register("Button1").onClick = OnButton1;//�ظ���Ұٷ�֮25������
        Register("Button2").onClick = OnButton2;//������5��Ѫ������
        Register("Button3").onClick = OnButton3;//�Ƴ����ƶ�1�ſ�
        Register("Button5").onClick = OnButton5;//�Ƴ����ƶ�1�ſ�
        Register("Button6").onClick = OnButton6;//��Ӷ�Ӧboss�Ŀ�������ֵ1-10֮��������ƶ�
        Register("Button7").onClick = OnButton7;//������ɱ����Ӷ�Ӧboss������ֵΪ12���������ƶ�
        Register("Button8").onClick = OnButton8;//�˳���Ϸ
        Register("Button9").onClick = OnButton9;//������Ϸ
    }

    private void Start()
    {
        
    }

    private void OnButton1(GameObject obj, PointerEventData pDate)
    {
        //��ȡ����������ֵ�Լ���ǰ����������ȡ����������
        //ѡ�������Button2��ʧ
        EventManager.Instance.CurHP += EventManager.Instance.MaxHP/4;
        if (EventManager.Instance.CurHP > EventManager.Instance.MaxHP)
        {
            EventManager.Instance.CurHP = EventManager.Instance.MaxHP;
        }
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHP();//ˢ��Ѫ��
        HideOnButton("Button1");
        HideOnButton("Button2");
    }
    private void OnButton2(GameObject obj, PointerEventData pDate)
    {
        //�������������ֵ����������ǰ����ֵ
        EventManager.Instance.MaxHP += 5;
        EventManager.Instance.CurHP += 5;
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHP();
        //ѡ�������Button1��ʧ
        HideOnButton("Button1");
        HideOnButton("Button2");
    }
    private void OnButton3(GameObject obj, PointerEventData pDate)
    {
        //�������������г�����ѡ��һ��ɾ��
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CloseShow(FightCardManager.Instance.usedCardList);
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CardListData = false;      
    }
    private void OnButton5(GameObject obj, PointerEventData pDate)
    {
        //�������������г�����ѡ��һ��ɾ��
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CloseShow(FightCardManager.Instance.cardList);
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").CardListData = true;
    }
    private void OnButton6(GameObject obj, PointerEventData pDate)
    {
        //��ȡ��һ��boss�����࣬���1-11֮��������������new card ��ӵ����ƶ�
        FightCardManager.Instance.usedCardList.Add(Newcard);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatesedCardCount();
        Destroy(NewCardobj.gameObject);
        HideOnButton("Button6");
    }
    private void OnButton7(GameObject obj, PointerEventData pDate)
    {
        //ֻ��������ɱ�ų��ֵİ�ť
        //��ȡ��һ��boss�����࣬��ֵ12�ĵ���������new card ��ӵ����ƶ�
        //���������ɾ��
        card Newcard = new card();
        Newcard.type = bosstype;
        Newcard.value = 12;
        FightCardManager.Instance.usedCardList.Add(Newcard);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatesedCardCount();

        HideOnButton("Button7");
    }
    //8.�˳���Ϸ
    private void OnButton8(GameObject obj, PointerEventData pDate)
    {
        //��Ҫ����������������ʹ��
        Application.Quit();
        //�ر�longinUI����  
    }
    private void OnButton9(GameObject obj, PointerEventData pDate)
    {
        Destroy(NewCardobj.gameObject);
        RightMoveAnimation();//����WinUI
        //UIManager.Instance.GetUI<FightUI>("FightUI").HieAttackBtn();
        EventManager.Instance.ChangeType(eventtype.NotEvent);
    }

    //������ɱʱ��ʾ�ð�ť
    public void ShowOnButton7()
    {
        Perfectkill.gameObject.SetActive(true);
        transform.Find("Button7").gameObject.SetActive(true);
    }
    //��������ɱʱ���ظð�ť
    public void HideOnbutton7()
    {
        Perfectkill.gameObject.SetActive(false);
        transform.Find("Button7").gameObject.SetActive(false);
    }

    public void HideOnButton(string Button)
    {
        transform.Find(Button).gameObject.SetActive(false);
    }

    //��ʾ1-6�����а�ť
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
