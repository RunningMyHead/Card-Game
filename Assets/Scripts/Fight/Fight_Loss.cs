using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Loss: FightUnit
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        AudioManager.Instance.PlayBGM("Loss");
        UIManager.Instance.GetUI<FightUI>("FightUI").HieAllFightBtn();//����Fight��־


        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);

        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemDownPos();
        //AudioManager.Instance.PlayBGM("Win");//����win����
        //UIManager.Instance.ShowUI<TalkUI>("TalkUI");
    }

    public void talk()
    {
        texts.Add("���ֻ��������ô�㻹���Զ���������Ů����");
        imgs.Add(false);

        texts.Add("��������ѧ����˲��������������棬���������硣");
        imgs.Add(true);

        texts.Add("���Ҿ�������˶�ֹͣ����������ѧ�����¥����ô�ұ����һ��¥��");
        imgs.Add(true);

        texts.Add("˵�úã���˲�����������У�������ѧ������ľ���");
        imgs.Add(false);

        texts.Add("���ν�ǳ���������ǰ�ˣ��������˸Ͼ��ˡ�");
        imgs.Add(false);

        texts.Add("������Խ��Խ�Ͽ����ˣ��������������Ҫ�ú���Ϣ��");
        imgs.Add(false);

        texts.Add("���ߣ��㾿���ǡ���");
        imgs.Add(true);
    }
}
