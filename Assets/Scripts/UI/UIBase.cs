using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//界面基类
public class UIBase: MonoBehaviour
{
    //注册事件
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }
    //显示
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    //隐藏
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    //关闭（销毁）
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }

    public void RightMoveAnimation()
    {
        // 使用DOTween创建移动动画，将物体向右移动指定距离
        transform.DOMoveX(2700, 1)
            .SetEase(Ease.OutQuad); // 使用缓动效果，先慢后快
    }
    //事件栏向左移动
    public void LeftMoveAnimation()
    {
        // 使用DOTween创建移动动画，将物体向左移动指定距离
        transform.DOMoveX(1250, 1)
            .SetEase(Ease.OutQuad); // 使用缓动效果，先慢后快
    }
}
