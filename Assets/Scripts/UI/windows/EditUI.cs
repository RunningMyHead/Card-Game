using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditUI : UIBase
{
    public Image UI1;
    public Image UI2;
    public bool UI;
    public Text textUI1;
    public Text textUI2;
    public void Awake()
    {
        UI1 = transform.Find("UI1").GetComponent<Image>();
        UI2 = transform.Find("UI2").GetComponent<Image>();
        UI = true;
        textUI1 = transform.Find("UI1/card").GetComponent<Text>();
        textUI2 = transform.Find("UI2/Boss").GetComponent<Text>();
        Register("Button2").onClick = onButton2;
        Register("Button3").onClick = onButton3;
        Register("UI1/0").onClick = onUI10;
        Register("UI1/1").onClick = onUI11;
        Register("UI1/2").onClick = onUI12;
        Register("UI1/3").onClick = onUI13;

        Register("UI2/0").onClick = onUI20;
        Register("UI2/1").onClick = onUI21;
        Register("UI2/2").onClick = onUI22;
        Register("UI2/3").onClick = onUI23;

        Register("UI1/Player").onClick = onUI1Player;
        Register("UI2/Player").onClick = onUI2Player;
    }

    private void onUI10 (GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "点数能够造成双倍伤害";
    }
    private void onUI11(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "获得等点数的护盾";
    }
    private void onUI12(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "摸牌堆加入等点数的弃牌，注意弃牌堆不会自动清0";
    }
    private void onUI13(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "抽等点数的卡，上限为14张，注意卡牌不会自动增加";
    }

    private void onUI20(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "“双剑”效果失效";
    }
    private void onUI21(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "“盾牌”效果失效";
    }
    private void onUI22(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "“循环”效果失效";
    }
    private void onUI23(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "“抽卡”效果失效";
    }
    private void onButton2(GameObject obj, PointerEventData pDate)
    {
        if (UI)
        {
            ShowUI2();
            UI = false;
        }
        else
        {
            ShowUI1();
            UI = true;
        }
    }
    private void onButton3(GameObject obj, PointerEventData pDate)
    {
        Close();//销毁UI
        UIManager.Instance.GetUI<LoginUI>("LoginUI").Show();//显示UI
    }

    private void onUI1Player(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "我来此，便是追寻数学真理";
    }
    private void onUI2Player(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "还有位老先生，包含深邃的智慧";
    }
    private void ShowUI1()
    {
        UI2.gameObject.SetActive(false);
        UI1.gameObject.SetActive(true);
    }
    private void ShowUI2()
    {
        UI1.gameObject.SetActive(false);
        UI2.gameObject.SetActive(true);
    }
}
