using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTf;//画布变换组件

    private List<UIBase> uiList;//存储加载过的界面的集合

    private void Awake()
    {
        Instance = this;
        //找世界中的画布
        canvasTf = GameObject.Find("Canvas").transform;
        //初始化界面的集合
        uiList = new List<UIBase>();
    }

    //显示UI
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
    
    //获得某个界面的脚本
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


    //隐藏UI
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            ui.Hide();
        }
    }

    //销毁所有UI
    public void CloseAllUI()
    {
        for(int i = uiList.Count-1; i >= 0; i--)
        { 
            Destroy(uiList[i].gameObject);
        }

        uiList.Clear();
    }

    //销毁UI
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null) 
        { 
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    //从集合中找到对应的界面脚本
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

    //创建敌人头部的行动图标
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/Hit"),canvasTf) as GameObject;
        //obj.transform.SetAsFirstSibling();//设置在父级最后一位
        return obj;
    }

    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HPPlane"), canvasTf) as GameObject;
        //obj.transform.SetAsFirstSibling();//设置在父级最后一位
        return obj;
    }
}
