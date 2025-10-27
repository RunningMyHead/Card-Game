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
        textUI1.text = "�����ܹ����˫���˺�";
    }
    private void onUI11(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "��õȵ����Ļ���";
    }
    private void onUI12(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "���ƶѼ���ȵ��������ƣ�ע�����ƶѲ����Զ���0";
    }
    private void onUI13(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "��ȵ����Ŀ�������Ϊ14�ţ�ע�⿨�Ʋ����Զ�����";
    }

    private void onUI20(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "��˫����Ч��ʧЧ";
    }
    private void onUI21(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "�����ơ�Ч��ʧЧ";
    }
    private void onUI22(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "��ѭ����Ч��ʧЧ";
    }
    private void onUI23(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "���鿨��Ч��ʧЧ";
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
        Close();//����UI
        UIManager.Instance.GetUI<LoginUI>("LoginUI").Show();//��ʾUI
    }

    private void onUI1Player(GameObject obj, PointerEventData pDate)
    {
        textUI1.text = "�����ˣ�����׷Ѱ��ѧ����";
    }
    private void onUI2Player(GameObject obj, PointerEventData pDate)
    {
        textUI2.text = "����λ������������������ǻ�";
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
