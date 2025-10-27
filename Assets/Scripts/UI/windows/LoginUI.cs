using UnityEngine;
using UnityEngine.EventSystems;

//开始界面
public class LoginUI : UIBase
{
    private void Awake()
    {
        //开始游戏
        Register("startBtn").onClick = onStarGameBtn;
        Register("quitBtn").onClick = onquitBtn;
        Register("EditBtn").onClick = onEditBtn;
        Register("jixuGameBtn").onClick = onStarGameBtn;
    }

    //unity会自动传递onStarGameBtn所需要的参数
    private void onStarGameBtn(GameObject obj, PointerEventData pDate)
    {
        //关闭longinUI界面  
        Close();
        //EventManager.Instance.ChangeType(eventtype.lnit);
        FightManager.Instance.ChangeType(FightType.Init);
        //FightManager.Instance.ChangeType(FightType.Init);
    }

    private void onEditBtn(GameObject obj, PointerEventData pDate)
    {
        Hide();//隐藏该界面
        UIManager.Instance.ShowUI<EditUI>("EditUI");
    }

    private void onquitBtn(GameObject obj, PointerEventData pDate)
    {
        //需要打包成APP或exe文件才可以执行
        Application.Quit();
        Debug.Log("退出游戏");
    }

}
