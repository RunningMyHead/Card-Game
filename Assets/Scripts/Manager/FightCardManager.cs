using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


//
//战斗卡牌管理器
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<card> cardList;//卡堆集合

    public List<card> usedCardList;//弃牌堆

    public List<card> handCardList;//已选中手牌

    //初始化
    //将卡堆打乱
    public void Init()
    {
        cardList = new List<card>();
        usedCardList = new List<card>();
        handCardList = new List<card>();

        //定义临时集合
        List<card> tempList = new List<card>();
        //将玩家的卡牌存储到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);

        while (tempList.Count > 0)
        {
            //随机下标
            int tempIndex = Random.Range(0, tempList.Count);
            //添加到卡堆
            cardList.Add(tempList[tempIndex]);
            //临时集合删除
            tempList.RemoveAt(tempIndex);
        }

    }

    //判断卡牌数量是否大于零
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //抽卡,从摸牌堆顶部开始抽卡
    public card DrawCard()
    {
        card id = cardList[0];
        cardList.RemoveAt(0);
        return id;
    }

    //将弃牌堆的卡牌打乱添加至摸牌堆尾端
    public void usedCardListtoCardList()
    {
        int tempIndex = Random.Range(0, usedCardList.Count);//随机下标
        cardList.Add(usedCardList[tempIndex]);
        usedCardList.RemoveAt(tempIndex);
    }

    //清空被选中的手牌
    public void ClosehandCardList()
    {
        handCardList.Clear();
    }

    //计算选中的卡牌有多少同类型卡牌，多少不同类型卡牌
    public void handCardListintegral()
    {
        int[] ints = new int[4];
        for(int t = handCardList.Count-1; t >= 0; t--)
        {
            switch(handCardList[t].type)
            {
                case cardtype.Sword:
                    ints[0]++;
                    break;
                case cardtype.Shield:
                    ints[1]++;
                    break;
                case cardtype.Return:
                    ints[2]++;
                    break;
                case cardtype.Draw:
                    ints[3]++;
                    break;
            }
        }
        if(ints.Max() > 1)
        {
            EventManager.Instance.integralint += 2*ints.Max();
        }
        if(ints.Count(x => x != 0)>1)
        {
            EventManager.Instance.integralint += 2 * ints.Count(x => x != 0);
        }
 
    }

    public int GetcardUPItemListValueAdd()
    {
        int temp = 0;
        for (int i = 0; i < handCardList.Count; i++)
        {
           
            temp += handCardList[i].value;
        
        }
        return temp;
    }
}
