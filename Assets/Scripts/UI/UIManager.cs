using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTf;//�����任���

    private List<UIBase> uiList;//�洢���ع��Ľ���ļ���

    private void Awake()
    {
        Instance = this;
        //�������еĻ���
        canvasTf = GameObject.Find("Canvas").transform;
        //��ʼ������ļ���
        uiList = new List<UIBase>();
    }

    //��ʾUI
    public UIBase ShowUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if(ui == null)
        {
            GameObject obj = Instantiate(Resources.Load("UI/"+uiName), canvasTf) as GameObject;
            obj.name = uiName;
            ui = obj.GetComponent<T>();
            
            uiList.Add(ui);
        }
        else
        {
            ui.Show();
        }
        return ui;
    }
    
    //���ĳ������Ľű�
    public T GetUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            return ui.GetComponent<T>();
        }
        else
        {
            return null;
        }
        
    }


    //����UI
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            ui.Hide();
        }
    }

    //��������UI
    public void CloseAllUI()
    {
        for(int i = uiList.Count-1; i >= 0; i--)
        { 
            Destroy(uiList[i].gameObject);
        }

        uiList.Clear();
    }

    //����UI
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null) 
        { 
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    //�Ӽ������ҵ���Ӧ�Ľ���ű�
    public UIBase Find(string name)
    {
        for(int i = 0;  i < uiList.Count; i++)
        {
            if (uiList[i].name == name)
            {
                return uiList[i];
            }
        }
        return null;
    }


    public void ShowTip(string msg1,Color color  ,System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Tips"),canvasTf) as GameObject;
        Text Text1 = obj.transform.Find("bg/Text1").GetComponent<Text>();
        Text1.color = color;
        Text1.text = msg1;
        Tween scale1 = obj.transform.Find("bg").DOScale(1,0.1f);
        //Tween scale2 = obj.transform.Find("bg").DOScale(0, 0.1f);


        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        //seq.Append(scale2);
        seq.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();
            }
        });
        MonoBehaviour.Destroy(obj,1);
    }

    //��������ͷ�����ж�ͼ��
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/Hit"),canvasTf) as GameObject;
        //obj.transform.SetAsFirstSibling();//�����ڸ������һλ
        return obj;
    }

    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HPPlane"), canvasTf) as GameObject;
        //obj.transform.SetAsFirstSibling();//�����ڸ������һλ
        return obj;
    }
}
