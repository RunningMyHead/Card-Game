using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ϸ��ڽű�
public class GameApp : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
        //��ʼ����Ƶ������
        AudioManager.Instance.Init();
        //��ʼ���û���Ϣ
        RoleManager.Instance.Init();
        //��ʾloginUI
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        //����bgm
        AudioManager.Instance.PlayBGM("startBGM");

    }
 


}
