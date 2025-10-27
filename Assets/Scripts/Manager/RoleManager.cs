using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum cardtype
{
    Sword,//攻击
    Shield,//防御
    Return,//循环
    Draw,//抽卡
    NPC//显示最终boss
}
public struct card
{
    public int value;
    public cardtype type;
}
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<card> cardList;//存储玩家全部卡牌


    public void Init()
    {
        InitCard();
    }

    //初始卡牌，加入至卡堆
    public void InitCard()
    {
        cardList = new List<card>();
        card tempcard = new card();

        for (int i = 0; i < 4; i++)
        {
            tempcard.type = (cardtype)i;
            for(int j = 1; j < 11; j++)
            {
                tempcard.value = j;
                cardList.Add(tempcard);
            }
        }
    }
}
