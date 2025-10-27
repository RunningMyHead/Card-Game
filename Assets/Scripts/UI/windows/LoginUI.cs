using UnityEngine;
using UnityEngine.EventSystems;

//��ʼ����
public class LoginUI : UIBase
{
    private void Awake()
    {
        //��ʼ��Ϸ
        Register("startBtn").onClick = onStarGameBtn;
        Register("quitBtn").onClick = onquitBtn;
        Register("EditBtn").onClick = onEditBtn;
        Register("jixuGameBtn").onClick = onStarGameBtn;
    }

    //unity���Զ�����onStarGameBtn����Ҫ�Ĳ���
    private void onStarGameBtn(GameObject obj, PointerEventData pDate)
    {
        //�ر�longinUI����  
        Close();
        //EventManager.Instance.ChangeType(eventtype.lnit);
        FightManager.Instance.ChangeType(FightType.Init);
        //FightManager.Instance.ChangeType(FightType.Init);
    }

    private void onEditBtn(GameObject obj, PointerEventData pDate)
    {
        Hide();//���ظý���
        UIManager.Instance.ShowUI<EditUI>("EditUI");
    }

    private void onquitBtn(GameObject obj, PointerEventData pDate)
    {
        //��Ҫ�����APP��exe�ļ��ſ���ִ��
        Application.Quit();
        Debug.Log("�˳���Ϸ");
    }

}
