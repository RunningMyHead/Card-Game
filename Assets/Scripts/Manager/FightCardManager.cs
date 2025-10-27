using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


//
//ս�����ƹ�����
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<card> cardList;//���Ѽ���

    public List<card> usedCardList;//���ƶ�

    public List<card> handCardList;//��ѡ������

    //��ʼ��
    //�����Ѵ���
    public void Init()
    {
        cardList = new List<card>();
        usedCardList = new List<card>();
        handCardList = new List<card>();

        //������ʱ����
        List<card> tempList = new List<card>();
        //����ҵĿ��ƴ洢����ʱ����
        tempList.AddRange(RoleManager.Instance.cardList);

        while (tempList.Count > 0)
        {
            //����±�
            int tempIndex = Random.Range(0, tempList.Count);
            //��ӵ�����
            cardList.Add(tempList[tempIndex]);
            //��ʱ����ɾ��
            tempList.RemoveAt(tempIndex);
        }

    }

    //�жϿ��������Ƿ������
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //�鿨,�����ƶѶ�����ʼ�鿨
    public card DrawCard()
    {
        card id = cardList[0];
        cardList.RemoveAt(0);
        return id;
    }

    //�����ƶѵĿ��ƴ�����������ƶ�β��
    public void usedCardListtoCardList()
    {
        int tempIndex = Random.Range(0, usedCardList.Count);//����±�
        cardList.Add(usedCardList[tempIndex]);
        usedCardList.RemoveAt(tempIndex);
    }

    //��ձ�ѡ�е�����
    public void ClosehandCardList()
    {
        handCardList.Clear();
    }

    //����ѡ�еĿ����ж���ͬ���Ϳ��ƣ����ٲ�ͬ���Ϳ���
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
