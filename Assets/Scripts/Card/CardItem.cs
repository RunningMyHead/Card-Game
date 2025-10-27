using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public card data;
    public bool Cardselect;
    public void Init(card data)
    {
        this.data = data;
        Cardselect = false;
        //RectTransform rectTransform = transform.GetComponent<RectTransform>();
    }
    Vector2 initpos;
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.3f, 0.2f);
    }
    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.2f);
    }
    //鼠标点击
    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.Instance.GetUI<FightUI>("FightUI").Attackbtn.gameObject.activeSelf || UIManager.Instance.GetUI<FightUI>("FightUI").Defensebtn.gameObject.activeSelf)
        {
            initpos = transform.GetComponent<RectTransform>().anchoredPosition;
            if (Cardselect == true)
            {
                //已经选择，那么取消选择
                transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(initpos.x, -320), 0.2f);
                Cardselect = false;
                FightCardManager.Instance.handCardList.Remove(data);//向选中卡牌移除该卡牌，只会移除第一个与他值相等的卡
            }
            else
            {
                //还没选择，那么判断是否可以选择
                if (CardAdd() || FightManager.Instance.fightUnit is Fight_EnemyTurn)
                {

                    transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(initpos.x, -260), 0.2f);
                    //FightCardManager.Instance.UPcardList.Add(data);
                    Cardselect = true;
                    FightCardManager.Instance.handCardList.Add(data);//向选中卡牌添加该卡牌
                }
                else
                {
                    //不能选择
                    UIManager.Instance.ShowTip("不可选择", Color.red, delegate () { });
                }

            }
        }
    }

    //判断当前卡牌是否可以选择
    public bool CardAdd()
    {
        //可以同类型也可以同数值，但大小不能超过10；
        if (UIManager.Instance.GetUI<FightUI>("FightUI").Cardsamevalueandtype(data))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
