using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum FightType
{
    None,
    Init,
    Player,//战斗攻击回合
    Enemy,//战斗防御回合
    Win,//战斗胜利
    Loss,//死亡
    end//游戏结束
}
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit;//战斗单元


    private void Awake()
    {
        Instance = this;
    }

    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                fightUnit = new Fight_Loss();
                break;
        }

        fightUnit.Init();//初始化

    }

    private void Update()
    {
    }

}
