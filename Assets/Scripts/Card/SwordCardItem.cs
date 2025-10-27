using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCardItem
{
    public static SwordCardItem Instance = new SwordCardItem();
    public int Cardpower( int cardValue)
    {
        if (UIManager.Instance.GetUI<FightUI>("FightUI").CardPower(cardtype.Sword))
        {
            cardValue *= 2;
        }
        //EnemyManager.Instance.EnemyHit(cardValue);
        return cardValue;
    }
}
