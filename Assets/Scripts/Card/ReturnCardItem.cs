using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnCardItem
{
    public static ReturnCardItem Instance = new ReturnCardItem();
    public void Cardpower(int cardValue)
    {
        if (UIManager.Instance.GetUI<FightUI>("FightUI").CardPower(cardtype.Return))
        {
            //循环效果
            int count = cardValue;
            if (count > FightCardManager.Instance.usedCardList.Count)
            {
                count = FightCardManager.Instance.usedCardList.Count;
            }
            for (int i = 0; i < count; i++)
            {
                FightCardManager.Instance.usedCardListtoCardList();
            }
            //更新弃牌堆摸牌堆
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatesedCardCount();
        }

    }
}

