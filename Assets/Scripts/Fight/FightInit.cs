using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ս����ʼ��
public class FightInit : FightUnit
{
    public override void Init()
    {
        EventManager.Instance.ChangeType(eventtype.lnit);
    }
}
