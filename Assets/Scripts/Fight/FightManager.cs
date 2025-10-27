using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum FightType
{
    None,
    Init,
    Player,//ս�������غ�
    Enemy,//ս�������غ�
    Win,//ս��ʤ��
    Loss,//����
    end//��Ϸ����
}
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit;//ս����Ԫ


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

        fightUnit.Init();//��ʼ��

    }

    private void Update()
    {
    }

}
