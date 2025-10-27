using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Init : EventUnit
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()//��ʼ��
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();

        FightCardManager.Instance.Init();//��ʼ������
        EventManager.Instance.Init();//��ʼ�������ֵ����ʼ���¼�

        UIManager.Instance.ShowUI<FightUI>("FightUI");//����ս��UI
        UIManager.Instance.ShowUI<WinUI>("WinUI");//����ʤ��UI
        UIManager.Instance.ShowUI<CardCloseUI>("CardCloseUI");//����ɾ��UI
        UIManager.Instance.ShowUI<EventUI>("EventUI");//�����¼���

 
        //UIManager.Instance.GetUI<FightUI>("FightUI").Stooptime(1f);
        UIManager.Instance.ShowUI<TalkUI>("TalkUI");
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
        
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(8);//���ɳ�ʼ����
        UIManager.Instance.GetUI<FightUI>("FightUI").updateCardItemPos();//�ƶ�����
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemDownPos();
    }

    public void talk()
    {
        texts.Add("�������Ŷ����ľ���������ˣ���ѧ�ĸ�¥�ڴ˽�����");
        imgs.Add(true);

        texts.Add("�������ѧ֮���أ����ǰ����ѧ��");
        imgs.Add(true);

        texts.Add("��װ�������ŮӴ�����ѧ���أ���ɻ������ĸ�������");
        imgs.Add(false);

        texts.Add("�㣬����˭��ʲôʱ��");
        imgs.Add(true);

        texts.Add("���������ң������Ů�������������ѧ���أ��Ǿ������ɡ�");
        imgs.Add(false);

        texts.Add("���������ҵļ��������������ǲ�ˬ��");
        imgs.Add(true);

        texts.Add("��ô��֤�����ҿ��ɣ�����ġ����͡���������������");
        imgs.Add(false);

        texts.Add("���С���ѧ����");
        imgs.Add(false);
    }

}
