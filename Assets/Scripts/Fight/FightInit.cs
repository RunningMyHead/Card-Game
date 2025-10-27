using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ø®≈∆’Ω∂∑≥ı ºªØ
public class FightInit : FightUnit
{
    public override void Init()
    {
        EventManager.Instance.ChangeType(eventtype.lnit);
    }
}
