using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

//ս������
public class FightUI : UIBase
{
    private Text cardCountText;//���ƶ�����
    private Text nocardCountText;//���ƶ�����
    private Text hpText;//��ǰѪ��
    private Image hpImg;//Ѫ���仯
    private Text fytext;//����ֵ
    private List<CardItem> cardItemList;//�洢�������弯��
    public  Button Attackbtn;
    public  Button Defensebtn;
    private Image AttackImage;
    private Image DefenseImage;
    public  Text integral;//�����ı���ʵ��
    public Text time;

    private float elapsedTime = 0f; // ������ʱ��
    private bool isRunning = true; // ��ʱ���Ƿ�������

    private Text Money;
    //��ȡ���
    private void Awake()
    {
        cardItemList = new List<CardItem>();
        Attackbtn = transform.Find("Attack").GetComponent<Button>(); 
        Defensebtn = transform.Find("Defense").GetComponent<Button>();

        AttackImage = transform.Find("AttackImage").GetComponent<Image>();
        DefenseImage = transform.Find("DefenseImage").GetComponent<Image>();

        cardCountText = transform.Find("Draw/Drawint/int").GetComponent<Text>();
        nocardCountText = transform.Find("NoDraw/NoDrawint/int").GetComponent<Text>();
        
        integral = transform.Find("Infobar/integral/Text").GetComponent<Text>();
        hpText = transform.Find("Infobar/HPPlane/HPPlane1/HPint/int").GetComponent<Text>();
        //fytext = transform.Find("Amor/int").GetComponent<Text>();
        fytext = transform.Find("Infobar/Amor/int").GetComponentInParent<Text>();
        //hpImg = transform.Find("HPPlane/HPPlane1/HPint").GetComponent<Image>();
        hpImg = transform.Find("Infobar/HPPlane/HPPlane1/HPint").GetComponent<Image>();
        Money = transform.Find("Infobar/money/Text").GetComponent<Text>();
        time = transform.Find("Infobar/time/Text").GetComponent<Text>();

        Register("Attack").onClick = onAttackBtn;
        Register("Defense").onClick = onDefenseBtn;
        Register("Infobar/Button").onClick = onButton;

    }

    private void Start()
    {
        UpdateHP();
        UpdateDefense();
        UpdateCardCount();
        UpdatesedCardCount();
        UpdateintegralCount();
        UpdateMoney();
    }

    //����Ѫ����ʾ
    public void UpdateHP()
    {
        //hpText.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        //hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;

        hpText.text = EventManager.Instance.CurHP + "/" + EventManager.Instance.MaxHP;
        hpImg.fillAmount = (float)EventManager.Instance.CurHP / (float)EventManager.Instance.MaxHP;
    }
    //���»���ֵ
    public void UpdateDefense()
    {
        if (EventManager.Instance.DefenseCount == 0)
        {
            transform.Find("Infobar/Amor").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Infobar/Amor").gameObject.SetActive(true);
            fytext.text = EventManager.Instance.DefenseCount.ToString();
        }
 
    }
    //�������ƶ�
    public void UpdateCardCount()
    {
        cardCountText.text = FightCardManager.Instance.cardList.Count.ToString();
    }
    //�������ƶ�
    public void UpdatesedCardCount()
    {
        nocardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }
    //���·���
    public void UpdateintegralCount()
    {
        //integral.text = FightManager.Instance.integralint.ToString();
        integral.text = EventManager.Instance.integralint.ToString();
    }
    public void UpdateMoney()
    {
        Money.text = EventManager.Instance.money.ToString();
    }

    //������������//����鿨����
    public void CreateCardItem(int count)
    {
        if (count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        if(count + cardItemList.Count > EventManager.Instance.MaxCardSize)
        {
            count = EventManager.Instance.MaxCardSize - cardItemList.Count;
        }
        for(int i = 0; i< count; i++)
        {
            card cardId = FightCardManager.Instance.DrawCard();
            GameObject obj = Instantiate(Resources.Load("Card/"+ cardId.type),transform) as GameObject;
            Text text = obj.transform.Find("Value/Text").GetComponent<Text>();
            text.text = cardId.value.ToString();
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-700,-320);
            var item = obj.AddComponent(System.Type.GetType("CardItem")) as CardItem;
            item.Init(cardId);
            cardItemList.Add(item);
        }
    }

    //��������ʱ���ƶ�
    public void updateCardItemPos()
    {
        float offset = 1400.0f/cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count/2.0f * offset + offset * 0.5f,-320);
        for (int i = 0; i < cardItemList.Count; i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x = startPos.x + offset;
        }
    }

    public void CardItemUpPos()
    {
        for (int i = 0; i < cardItemList.Count; i++)
        {
            cardItemList[i].transform.DOLocalMoveY(-320, 1).SetEase(Ease.OutQuad);
        }
    }

    //�ڷ�ս��״̬�¿��������ƶ�
    public void CardItemDownPos()
    {
        for(int i = 0; i < cardItemList.Count;i++)
        {
            cardItemList[i].transform.DOLocalMoveY(-600,1).SetEase(Ease.OutQuad);
        }
        StartCoroutine(Synergy());
    }

    //���¹�����
    public void onAttackBtn(GameObject obj, PointerEventData pDate)
    {
        HieAttackBtn();
        int CardValue = FightCardManager.Instance.GetcardUPItemListValueAdd();
        StartCoroutine(DeAttactime(CardValue));
    }
    //���·�����
    public void onDefenseBtn(GameObject obj, PointerEventData pDate)
    {
        HieDefenseBtn();//��������ʧ
        int CardValue = FightCardManager.Instance.GetcardUPItemListValueAdd();
        StartCoroutine(Defensetime(CardValue));

    }

    //�Ƴ���ѡ�еĿ�
    public void RemoveCard()
    {
        for (int i = 0; i < cardItemList.Count; i++)
        {
            if (cardItemList[i].Cardselect == true)
            {

                if(!(cardItemList[i].data.value == 12 && FightManager.Instance.fightUnit is Fight_PlayerTurn))
                {
                    //��������Ƽ���
                    FightCardManager.Instance.usedCardList.Add(cardItemList[i].data);
                }
                cardItemList[i].enabled = false;
                //�����ƶ�Ч��
                if(FightManager.Instance.fightUnit is Fight_PlayerTurn)
                {
                    cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(new Vector2(675,225), 0.5f);
                }
                else
                {
                    cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(new Vector2(-675, 225), 0.5f);

                }
                cardItemList[i].transform.DOScale(0, 0.5f);
                Destroy(cardItemList[i].gameObject, 1);//�ӳ�1��������Ϸ����
            }
        }
        cardItemList.RemoveAll(item => item.Cardselect == true);
        UpdatesedCardCount();
        updateCardItemPos();
        
    }
    
    //�жϵ�ǰ�����Ƿ��ѡ
    public bool Cardsamevalueandtype( card temp)
    {
        int Cardint = temp.value;
        cardtype Cardtype = temp.type;

        int cardValue = temp.value;//�����жϿ��������Ƿ����10
        //�жϿ����Ƿ�Ϊ����Ϊ12����10�ĳ������ƣ�ѡ���ҽ���ѡ����һ��
        if (FightCardManager.Instance.handCardList.Count == 0 && (Cardint == 12 || Cardint == 10))
        {
            return true;
        }
        //�жϿ����Ƿ�Ϊ����Ϊ1�ĳ��￨�ƣ����ڵ���С�ڵ��ھ�ʱѡ��
        if (Cardint == 1 && FightCardManager.Instance.GetcardUPItemListValueAdd() < 10)                                                                                                                       
        {
            return true;
        }
        //�ж��ܵ����Ƿ����10
        if(FightCardManager.Instance.GetcardUPItemListValueAdd() + Cardint > 10)
        {
            return false;
        }
        //����һ�㿨�����ڵ�����ͬ����������ͬʱ���Ա�ѡ��
        for(int i = FightCardManager.Instance.handCardList.Count -1; i >= 0; i--)
        {
            if (Cardint != FightCardManager.Instance.handCardList[i].value &&
                Cardtype != FightCardManager.Instance.handCardList[i].type &&
                FightCardManager.Instance.handCardList[i].value != 1)
            {
                return false;
            }
        }
        return true;
    }

    //�жϳ��ƵĿ��������Ƿ���˫�� ���� ѭ�� �鿨
    public bool CardPower(cardtype temp)
    {
 
        for (int i = FightCardManager.Instance.handCardList.Count - 1; i >= 0; i--)
        {
            if (FightCardManager.Instance.handCardList[i].type == temp && !(temp == EnemyManager.Instance.GetBOSSdate()))
            {
                return true;
            }
        }
        return false;
    }

    //ֻ��ʾ������ť�Լ������غϱ�ʶ
    public void ShowAttack()
    {
        Attackbtn.gameObject.SetActive(true);
        Defensebtn.gameObject.SetActive(false);
        AttackImage.gameObject.SetActive(true);
        DefenseImage.gameObject.SetActive(false);
    }
    //ֻ��ʾ������ť�Լ������غϱ�ʶ
    public void ShowDefense()
    {
        Attackbtn.gameObject.SetActive(false);
        Defensebtn.gameObject.SetActive(true);
        AttackImage.gameObject.SetActive(false);
        DefenseImage.gameObject.SetActive(true);
    }

    //���ع�����ť
    public void HieAttackBtn()
    {
        Attackbtn.gameObject.SetActive(false);
    }
    //���ط�����ť
    public void HieDefenseBtn()
    {
        Defensebtn.gameObject.SetActive(false);
    }

    public void HieAllFightBtn()
    {
        Attackbtn.gameObject.SetActive(false);
        Defensebtn.gameObject.SetActive(false);
        AttackImage.gameObject.SetActive(false);
        DefenseImage.gameObject.SetActive(false);
    }

    //����������
    public IEnumerator Defensetime(int CardValue)
    {
        RemoveCard();
        EventManager.Instance.DefenseCount += CardValue;
        UpdateDefense();
        if(CardValue > 0)
        {
            yield return new WaitForSeconds(0.65f);
        }
        
        StartCoroutine(EnemyManager.Instance.DoAttack());
    }

    //����������
    public IEnumerator DeAttactime(int CardValue)
    {
        RemoveCard();
        ShieldCardItem.Instance.Cardpower(CardValue);
        ReturnCardItem.Instance.Cardpower(CardValue);
        DrawCardItem.Instance.Cardpower(CardValue);

        FightCardManager.Instance.handCardListintegral();
        
        UpdateintegralCount();//ˢ�»���
        
        if (CardValue > 0)
        {
            //FightManager.Instance.PlayerAttackAnimo();
            EventManager.Instance.PlayerAttactAnimo();
            yield return new WaitForSeconds(0.2f);
        }
        
        EnemyManager.Instance.EnemyHit(SwordCardItem.Instance.Cardpower(CardValue));
    }

    public IEnumerator Synergy()
    {
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayBGM("startBGM");
        UIManager.Instance.GetUI<TalkUI>("TalkUI").TALK();
    }

    public void onButton(GameObject obj, PointerEventData pDate)
    {
        UIManager.Instance.CloseAllUI();
        Destroy(EventManager.Instance.Player);
        if (EnemyManager.Instance.enemyList.Count != 0)
        {
            EnemyManager.Instance.DeleteEnemy();
        }
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        //����bgm
        AudioManager.Instance.PlayBGM("startBGM");
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // ���¾�����ʱ��
            UpdateTimeDisplay(); // ����ʱ����ʾ
        }
    }

    // ��ʼ��ʱ
    public void StartTimer()
    {
        isRunning = true;
    }

    // ֹͣ��ʱ
    public void StopTimer()
    {
        isRunning = false;
    }

    // ���ü�ʱ��
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimeDisplay(); // ����ʱ����ʾ
    }

    // ����ʱ����ʾ
    private void UpdateTimeDisplay()
    {
        int hours = (int)(elapsedTime / 3600f); // Сʱ��
        int minutes = (int)((elapsedTime % 3600f) / 60f); // ������
        int seconds = (int)(elapsedTime % 60f); // ����

        // ����UI Text��ʾʱ��
        time.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}

