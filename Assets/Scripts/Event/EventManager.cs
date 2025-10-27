using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eventtype
{
    None,
    lnit,//初始化
    NotEvent,
    Event,
    Die,
    End,
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Dictionary<string, EventClass> EventDictionary;//前面存放事件ID，后面存放事件

    public GameObject Player;
    public GameObject PlayerNotFight;
    public GameObject PlayerFight;
    public int MaxHP;
    public int CurHP;
    public int DefenseCount;

    public int MaxCardSize;//最大卡牌数
    public int integralint;//玩家分数
    public int money;//金币
    public int Maxpopotion;//最大药水瓶


    public EventUnit eventUnit;//事件单元
    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        EventDictionary = new Dictionary<string, EventClass>();
        MaxHP = 20;
        CurHP = 20;
        DefenseCount = 0;
        MaxCardSize = 15;//最大15张，实测只有14张
        Player = Instantiate(Resources.Load("playeandboss/Player") as GameObject);
        PlayerNotFight = Player.transform.Find("notFight").gameObject;
        PlayerFight = Player.transform.Find("Fight").gameObject;
        integralint = 0;
        money = 0;
        Maxpopotion = 0;

        //初始化事件
        EventDictionary.Add("Enemy_Sword", new Enemy_Sword());
        EventDictionary.Add("Enemy_Return", new Enemy_Return());
        EventDictionary.Add("Enemy_Draw", new Enemy_Draw());
        EventDictionary.Add("Enemy_Shield", new Enemy_Shield());
    }

    public void ChangeType(eventtype eventtype)
    {
        switch(eventtype)
        {
            case eventtype.None:
                break;
            case eventtype.lnit:
                eventUnit = new Event_Init();
                break;
            case eventtype.NotEvent:
                eventUnit = new Event_NotEvent();
                break;
            case eventtype.Event:
                eventUnit = new Event_Event();
                break;
            case eventtype.Die:
                eventUnit = new Event_Die();
                break;
            case eventtype.End:
                eventUnit = new Event_End();
                break;   
        }
        eventUnit.Init();
        
    }

    
    public void ToPlayerFight()//玩家立绘变成战斗姿态
    {
        PlayerNotFight.gameObject.SetActive(false);
        PlayerFight.gameObject.SetActive(true);
    }
   
    public void ToPlayetNotFight() //玩家立绘变成非战斗姿态
    {
        PlayerNotFight.gameObject.SetActive(true);
        PlayerFight.gameObject.SetActive(false);
    }
    
    public void PlayerAttactAnimo()//玩家攻击动画耗时0.4秒
    {
        Player.transform.DOMove(new Vector2(-7, -3.5f), 0.3f)
        .SetEase(Ease.OutQuad) // 设置移动曲线为线性
        .OnComplete(() =>
        {
            // 移动完成后将物体位置设置回原始位置
            Player.transform.DOMove(new Vector2(-5, -3.5f), 0.1f)
            .SetEase(Ease.InBack);
        });
    }
    
    public void PlayerDieAnimo()//玩家死亡动画 耗时2.5秒
    {
        Player.transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() =>
        {
            ToPlayetNotFight();
            Player.transform.DORotate(new Vector3(0, 0, 60), 2f, RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear);
            Player.transform.DOMove(Player.transform.position + new Vector3(-0.5f, -1, 0), 2f).SetRelative().SetEase(Ease.Linear);
        });
    }
    
    public void PlyaerHitAnimo()//玩家受击动画 耗时0.9秒
    {
        ToPlayetNotFight();
        // 使用 DOTween 来创建闪烁效果
        Sequence rotationSequence = DOTween.Sequence();
        rotationSequence.Append(Player.transform.DORotate(new Vector3(0f, 180f, 0f), 0.1f))
            .SetEase(Ease.Linear).SetLoops(8, LoopType.Restart);
        Sequence blinkSequence = DOTween.Sequence();
        blinkSequence.Append(PlayerNotFight.GetComponent<Renderer>().material.DOColor(Color.red, 0.1f)); // 切换颜色为闪烁颜色
        blinkSequence.Append(PlayerNotFight.GetComponent<Renderer>().material.DOColor(Color.white, 0.1f)); // 恢复原始颜色
        blinkSequence.SetLoops(4);
        blinkSequence.OnComplete(() =>
        {
            // 闪烁完成后将物体角度设置回正确的朝向
            ToPlayerFight();
            Player.transform.DORotate(new Vector3(0f, 0f, 0f), 0.1f).SetEase(Ease.OutSine);
        });
        // 启动动画序列
        //PlayertonotFight();
        rotationSequence.Play();
        blinkSequence.Play();
    }
    public void GetPlayerHit(int Hit)//受伤
    {
        //先消耗护盾
        if (DefenseCount >= Hit)
        {
            DefenseCount -= Hit;
            if (DefenseCount == 0)
            {
                integralint += 5;//完美格挡5分
            }
            else
            {
                integralint += DefenseCount;
            }
        }
        else
        {
            Hit = Hit - DefenseCount;

            DefenseCount = 0;
            CurHP -= Hit;

            if (CurHP <= 0)
            {
                CurHP = 0;
                PlayerDieAnimo();
            }
            else
            {
                PlyaerHitAnimo();
                Camera.main.DOShakePosition(1f, 0.2f, 10, 45);
            }

        }
        //更新界面
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateintegralCount();
    }
}
