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
        UIManager.Instance.GetUI<FightUI>("FightUI").HieAllFightBtn();//隐藏Fight标志


        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);

        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemDownPos();
        //AudioManager.Instance.PlayBGM("Win");//播放win音乐
        //UIManager.Instance.ShowUI<TalkUI>("TalkUI");
    }

    public void talk()
    {
        texts.Add("如果只是这样那么你还差得远，异国的少女……");
        imgs.Add(false);

        texts.Add("东方的数学，如此博大精深，如此深邃神奇，我自愧不如。");
        imgs.Add(true);

        texts.Add("但我绝不对因此而停止，东方的数学宛如高楼，那么我便更上一层楼！");
        imgs.Add(true);

        texts.Add("说得好，如此不屈，如此钻研，正是数学所需求的精神！");
        imgs.Add(false);

        texts.Add("真可谓是长江后浪推前浪，世上新人赶旧人。");
        imgs.Add(false);

        texts.Add("我真是越来越认可你了，姑娘，但你现在需要好好休息。");
        imgs.Add(false);

        texts.Add("老者，你究竟是……");
        imgs.Add(true);
    }
}
