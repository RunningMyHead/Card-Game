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
            //ѭ��Ч��
            int count = cardValue;
            if (count > FightCardManager.Instance.usedCardList.Count)
            {
                count = FightCardManager.Instance.usedCardList.Count;
            }
            for (int i = 0; i < count; i++)
            {
                FightCardManager.Instance.usedCardListtoCardList();
            }
            //�������ƶ����ƶ�
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatesedCardCount();
        }

    }
}

