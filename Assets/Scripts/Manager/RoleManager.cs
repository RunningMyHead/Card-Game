using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum cardtype
{
    Sword,//����
    Shield,//����
    Return,//ѭ��
    Draw,//�鿨
    NPC//��ʾ����boss
}
public struct card
{
    public int value;
    public cardtype type;
}
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<card> cardList;//�洢���ȫ������


    public void Init()
    {
        InitCard();
    }

    //��ʼ���ƣ�����������
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
