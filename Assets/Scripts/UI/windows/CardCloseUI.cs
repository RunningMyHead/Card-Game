using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CardCloseUI : UIBase
{
    public List<CardClose> CardItemLSword;//存储卡牌物体集合
    public List<CardClose> CardItemShield;
    public List<CardClose> CardItemReturn;
    public List<CardClose> CardItemDraw;
    public List<card> cardClose;
    private List<card> CardSword;
    private List<card> CardShield;
    private List<card> CardReturn;
    private List<card> CardDraw;
    public bool CardListData;//为真为摸牌堆，为假为弃牌堆
    private void Awake()
    {
        CardItemLSword = new List<CardClose>();
        CardItemShield = new List<CardClose>();
        CardItemReturn = new List<CardClose>();
        CardItemDraw = new List<CardClose>();
        CardListData = false;

        cardClose = new List<card>(1);

        CardSword = new List<card>();
        CardShield = new List<card>();
        CardReturn = new List<card>();
        CardDraw = new List<card>();
        Register("CloseButton").onClick = CloseButton;
        Register("QuitButton").onClick = QuitButton;
    }

    private void QuitButton(GameObject obj, PointerEventData pData)
    {
        //隐藏删除卡牌界面
        UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").Hide();
        CloseAllobj();
    }
    private void CloseButton(GameObject obj, PointerEventData pDate)
    {
        if(cardClose.Count == 1)
        {
            if(CardListData)
            {
                FightCardManager.Instance.cardList.Remove(cardClose[0]);
                UIManager.Instance.GetUI<WinUI>("WinUI").HideOnButton("Button5");
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }
            else
            {
                FightCardManager.Instance.usedCardList.Remove(cardClose[0]);
                UIManager.Instance.GetUI<WinUI>("WinUI").HideOnButton("Button3");
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdatesedCardCount();
            }
            transform.Find("CloseButton").gameObject.SetActive(false);
            CloseCardtrue();
        }
        else
        {
            return;
        }
    }

    //卡牌展开
    public void CloseShow(List<card> cardList)
    {
        base.Show();
        cardclassify(cardList);
        CardSort();
        cardobj(CardSword,262.5f,CardItemLSword);
        cardobj(CardShield, 87.5f,CardItemShield);
        cardobj(CardReturn, -87.5f , CardItemReturn);
        cardobj(CardDraw, -262.5f,CardItemDraw);

    }

    //根据卡牌创建卡牌物体并且移动
    //卡牌类型，生成坐标
    public void cardobj(List<card> cardList , float vector2Y , List<CardClose> cardItem)
    {
        for(int i = 0; i < cardList.Count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("Card/" + cardList[i].type), transform) as GameObject;
            Text text = obj.transform.Find("Value/Text").GetComponent<Text>();
            text.text = cardList[i].value.ToString();
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800, vector2Y);
            var item = obj.AddComponent(System.Type.GetType("CardClose")) as CardClose;
            item.Init(cardList[i]);
            cardItem.Add(item);
        }
        CardItemPos(vector2Y,cardItem);
    }

    public void CardItemPos(float vector2Y , List<CardClose> cardItem)
    {
        float fixedPositionX = -750f; // 你希望的第一张卡片的固定位置的 x 坐标

        // 计算除去第一张卡片后的卡片数量和可用宽度
        int remainingCount = cardItem.Count - 1;
        float availableWidth = 1100.0f;

        // 计算除去第一张卡片后，后续卡片之间的水平间距
        float offset = availableWidth / remainingCount;

        // 设置第一张卡片的位置
        Vector2 firstCardPosition = new Vector2(fixedPositionX, vector2Y);
        if(cardItem.Count!=0)
        {
            cardItem[0].GetComponent<RectTransform>().DOAnchorPos(firstCardPosition, 0.5f);
        }
        // 从第二张卡片开始，依次设置位置
        for (int i = 1; i < cardItem.Count; i++)
        {
            // 计算当前卡片相对于第一张卡片的偏移
            float offsetX = i * offset;

            // 计算当前卡片的位置
            Vector2 currentPosition = new Vector2(fixedPositionX + offsetX, vector2Y);

            // 设置当前卡片的位置动画
            cardItem[i].GetComponent<RectTransform>().DOAnchorPos(currentPosition, 0.5f);
        }
    }

    //首先根据种类进行分类
    public void cardclassify(List<card> cardList)
    {
        for (int i = cardList.Count-1; i >= 0;i--)
        {
            switch (cardList[i].type)
            {
                case cardtype.Sword:
                    CardSword.Add(cardList[i]);
                    break;
                case cardtype.Shield:
                    CardShield.Add(cardList[i]);
                    break;
                case cardtype.Return:
                    CardReturn.Add(cardList[i]);
                    break;
                case cardtype.Draw:
                    CardDraw.Add(cardList[i]);
                    break;
            }
        }
    }

    //根据大小进行排序
    public void CardSort()
    {
        QuickSort(CardSword, 0, CardSword.Count - 1);
        QuickSort(CardShield, 0, CardShield.Count - 1);
        QuickSort(CardReturn, 0, CardReturn.Count - 1);
        QuickSort(CardDraw, 0, CardDraw.Count - 1);
    }
    //快速排序
    private void QuickSort(List<card> list, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, left, right);

            QuickSort(list, left, pivotIndex - 1);
            QuickSort(list, pivotIndex + 1, right);
        }
    }
    private int Partition(List<card> list, int left, int right)
    {
        card pivot = list[right]; // 选择最右边的元素作为基准元素
        int i = left - 1;                                       
        for (int j = left; j < right; j++)
        {
            if (list[j].value < pivot.value)
            {
                i++;
                
                // 交换元素
                card temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        // 将基准元素放到正确的位置上
        card temp2 = list[i + 1];
        list[i + 1] = list[right];
        list[right] = temp2;
        return i + 1;
    }

    private void CloseAllobj()
    {
        while(CardItemLSword.Count != 0)
        {
            Destroy(CardItemLSword[0].gameObject);
            CardItemLSword.RemoveAt(0);
        }
        while(CardItemShield.Count != 0)
        {
            Destroy(CardItemShield[0].gameObject);
            CardItemShield.RemoveAt(0);
        }
        while (CardItemReturn.Count != 0)
        {
            Destroy(CardItemReturn[0].gameObject);
            CardItemReturn.RemoveAt(0);
        }
        while (CardItemDraw.Count != 0)
        {
            Destroy(CardItemDraw[0].gameObject);
            CardItemDraw.RemoveAt(0);
        }
        cardClose.Clear();
        CardSword.Clear();
        CardShield.Clear();
        CardReturn.Clear();
        CardDraw.Clear();
        transform.Find("CloseButton").gameObject.SetActive(true);
    }
    
    private void CloseCardtrue()
    {
        for(int i = CardItemLSword.Count-1; i >= 0;i--)
        {
            if (CardItemLSword[i].Cardselect)
            {
                Destroy(CardItemLSword[i].gameObject); 
                CardItemLSword.Remove(CardItemLSword[i]);
                return;
            }
        }
        for (int i = CardItemShield.Count-1; i >= 0; i--)
        {
            if (CardItemShield[i].Cardselect)
            {
                Destroy(CardItemShield[i].gameObject);
                CardItemShield.Remove(CardItemShield[i]);
                return;
            }
        }
        for (int i = CardItemReturn.Count - 1; i >= 0; i--)
        {
            if (CardItemReturn[i].Cardselect)
            {
                Destroy(CardItemReturn[i].gameObject);
                CardItemReturn.Remove(CardItemReturn[i]);
                return;
            }
        }
        for (int i = CardItemDraw.Count - 1; i >= 0; i--)
        {
            if (CardItemDraw[i].Cardselect)
            {
                Destroy(CardItemDraw[i].gameObject);
                CardItemDraw.Remove(CardItemDraw[i]);
                return;
            }
        }
    }
}
