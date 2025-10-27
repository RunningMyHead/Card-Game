using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardItem
{
    public static DrawCardItem Instance = new DrawCardItem();
    public void Cardpower( int cardValue)
    {
        if (UIManager.Instance.GetUI<FightUI>("FightUI").CardPower(cardtype.Draw))
        {

            //抽卡效果
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(cardValue);
            UIManager.Instance.GetUI<FightUI>("FightUI").updateCardItemPos();
            //更新摸牌堆数量
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        }
    }
}
