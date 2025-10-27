using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Return : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.Return);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        //UIManager.Instance.GetUI<FightUI>("FightUI").ShowHP();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }

    public void talk()
    {
        texts.Add("����׼�������ҹ�ء��ഫ���Ǵ���ʱ��ˮ��������");
        imgs.Add(false);

        texts.Add("��֪����˵����ʲô�������Ů��");
        imgs.Add(false);

        texts.Add("׼�������桢���ǻ�������ʹ�õ���ѧ���ߣ�");
        imgs.Add(true);

        texts.Add("ȷʵ��ˣ�����˵������ѧ����������սʤһ�С�");
        imgs.Add(false);

        texts.Add("�����û�п̿����еľ���ʵ�����ǵ������ѧ������΢����΢��");
        imgs.Add(true);

        texts.Add("���չ���ѧֻ�ǹ��ߣ����ÿ�ʹ�õ��ˡ��𣿺�����˼���⡣");
        imgs.Add(false);
    }
}
