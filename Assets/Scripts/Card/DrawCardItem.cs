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

            //�鿨Ч��
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(cardValue);
            UIManager.Instance.GetUI<FightUI>("FightUI").updateCardItemPos();
            //�������ƶ�����
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        }
    }
}
