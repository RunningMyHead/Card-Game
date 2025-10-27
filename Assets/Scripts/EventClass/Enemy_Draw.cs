using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Draw : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.Draw);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        //UIManager.Instance.GetUI<FightUI>("FightUI").ShowHP();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }

    public void talk()
    {
        texts.Add("���Ƿ�֪����������������");
        imgs.Add(false);

        texts.Add("�ǡ��㾭ʮ�顱�е�һ������Ҫ������ʱ�ĸ���˵���ķ�������");
        imgs.Add(true);

        texts.Add("�����ǻ�����ʷ�˽���Ȼ����Ҫ��һ��������ѧ����Ȼ��ϵľ�����");
        imgs.Add(false);

        texts.Add("��ô��֮��Ӧ���������ľ������ǡ�̫ƽ�������˰ɣ�");
        imgs.Add(true);

        texts.Add("��������˼�ļ��⣬�����Ů��");
        imgs.Add(false);
    }
}
