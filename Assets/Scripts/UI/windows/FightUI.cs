using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

//战斗界面
public class FightUI : UIBase
{
    private Text cardCountText;//摸牌堆数量
    private Text nocardCountText;//弃牌堆数量
    private Text hpText;//当前血量
    private Image hpImg;//血条变化
    private Text fytext;//防御值
    private List<CardItem> cardItemList;//存储卡牌物体集合
    public  Button Attackbtn;
    public  Button Defensebtn;
    private Image AttackImage;
    private Image DefenseImage;
    public  Text integral;//分数文本，实体
    public Text time;

    private float elapsedTime = 0f; // 经过的时间
    private bool isRunning = true; // 计时器是否在运行

    private Text Money;
    //获取组件
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

    //更新血量显示
    public void UpdateHP()
    {
        //hpText.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        //hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;

        hpText.text = EventManager.Instance.CurHP + "/" + EventManager.Instance.MaxHP;
        hpImg.fillAmount = (float)EventManager.Instance.CurHP / (float)EventManager.Instance.MaxHP;
    }
    //更新护盾值
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
    //更新摸牌堆
    public void UpdateCardCount()
    {
        cardCountText.text = FightCardManager.Instance.cardList.Count.ToString();
    }
    //更新弃牌堆
    public void UpdatesedCardCount()
    {
        nocardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }
    //更新分数
    public void UpdateintegralCount()
    {
        //integral.text = FightManager.Instance.integralint.ToString();
        integral.text = EventManager.Instance.integralint.ToString();
    }
    public void UpdateMoney()
    {
        Money.text = EventManager.Instance.money.ToString();
    }

    //创建卡牌物体//输入抽卡数量
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

    //创建卡牌时的移动
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

    //在非战斗状态下卡牌向下移动
    public void CardItemDownPos()
    {
        for(int i = 0; i < cardItemList.Count;i++)
        {
            cardItemList[i].transform.DOLocalMoveY(-600,1).SetEase(Ease.OutQuad);
        }
        StartCoroutine(Synergy());
    }

    //按下攻击键
    public void onAttackBtn(GameObject obj, PointerEventData pDate)
    {
        HieAttackBtn();
        int CardValue = FightCardManager.Instance.GetcardUPItemListValueAdd();
        StartCoroutine(DeAttactime(CardValue));
    }
    //按下防御键
    public void onDefenseBtn(GameObject obj, PointerEventData pDate)
    {
        HieDefenseBtn();//防御键消失
        int CardValue = FightCardManager.Instance.GetcardUPItemListValueAdd();
        StartCoroutine(Defensetime(CardValue));

    }

    //移除被选中的卡
    public void RemoveCard()
    {
        for (int i = 0; i < cardItemList.Count; i++)
        {
            if (cardItemList[i].Cardselect == true)
            {

                if(!(cardItemList[i].data.value == 12 && FightManager.Instance.fightUnit is Fight_PlayerTurn))
                {
                    //添加至弃牌集合
                    FightCardManager.Instance.usedCardList.Add(cardItemList[i].data);
                }
                cardItemList[i].enabled = false;
                //卡牌移动效果
                if(FightManager.Instance.fightUnit is Fight_PlayerTurn)
                {
                    cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(new Vector2(675,225), 0.5f);
                }
                else
                {
                    cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(new Vector2(-675, 225), 0.5f);

                }
                cardItemList[i].transform.DOScale(0, 0.5f);
                Destroy(cardItemList[i].gameObject, 1);//延迟1秒销毁游戏物体
            }
        }
        cardItemList.RemoveAll(item => item.Cardselect == true);
        UpdatesedCardCount();
        updateCardItemPos();
        
    }
    
    //判断当前卡牌是否可选
    public bool Cardsamevalueandtype( card temp)
    {
        int Cardint = temp.value;
        cardtype Cardtype = temp.type;

        int cardValue = temp.value;//用于判断卡牌总数是否大于10
        //判断卡牌是否为点数为12或者10的超级卡牌，选中且仅能选中他一张
        if (FightCardManager.Instance.handCardList.Count == 0 && (Cardint == 12 || Cardint == 10))
        {
            return true;
        }
        //判断卡牌是否为点数为1的宠物卡牌，仅在点数小于等于九时选中
        if (Cardint == 1 && FightCardManager.Instance.GetcardUPItemListValueAdd() < 10)                                                                                                                       
        {
            return true;
        }
        //判断总点数是否大于10
        if(FightCardManager.Instance.GetcardUPItemListValueAdd() + Cardint > 10)
        {
            return false;
        }
        //其他一般卡，仅在点数相同或是种类相同时可以被选中
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

    //判断出牌的卡牌里面是否有双剑 护甲 循环 抽卡
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

    //只显示攻击按钮以及攻击回合标识
    public void ShowAttack()
    {
        Attackbtn.gameObject.SetActive(true);
        Defensebtn.gameObject.SetActive(false);
        AttackImage.gameObject.SetActive(true);
        DefenseImage.gameObject.SetActive(false);
    }
    //只显示防御按钮以及防御回合标识
    public void ShowDefense()
    {
        Attackbtn.gameObject.SetActive(false);
        Defensebtn.gameObject.SetActive(true);
        AttackImage.gameObject.SetActive(false);
        DefenseImage.gameObject.SetActive(true);
    }

    //隐藏攻击按钮
    public void HieAttackBtn()
    {
        Attackbtn.gameObject.SetActive(false);
    }
    //隐藏防御按钮
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

    //防御键按下
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

    //攻击键按下
    public IEnumerator DeAttactime(int CardValue)
    {
        RemoveCard();
        ShieldCardItem.Instance.Cardpower(CardValue);
        ReturnCardItem.Instance.Cardpower(CardValue);
        DrawCardItem.Instance.Cardpower(CardValue);

        FightCardManager.Instance.handCardListintegral();
        
        UpdateintegralCount();//刷新积分
        
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
        //播放bgm
        AudioManager.Instance.PlayBGM("startBGM");
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // 更新经过的时间
            UpdateTimeDisplay(); // 更新时间显示
        }
    }

    // 开始计时
    public void StartTimer()
    {
        isRunning = true;
    }

    // 停止计时
    public void StopTimer()
    {
        isRunning = false;
    }

    // 重置计时器
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimeDisplay(); // 更新时间显示
    }

    // 更新时间显示
    private void UpdateTimeDisplay()
    {
        int hours = (int)(elapsedTime / 3600f); // 小时数
        int minutes = (int)((elapsedTime % 3600f) / 60f); // 分钟数
        int seconds = (int)(elapsedTime % 60f); // 秒数

        // 更新UI Text显示时间
        time.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}

