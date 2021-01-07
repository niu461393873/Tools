using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesProcess : MonoBehaviour
{
    public static MesProcess Instance = null;
    void Awake()
    {
        Instance = this;
        gameObject.AddComponent<UIManager>();
        gameObject.AddComponent<GameManager>();
        gameObject.AddComponent<MainLogicManager>();
        gameObject.AddComponent<DataManager>();
        gameObject.AddComponent<AssetManager>();
        
       // StartCoroutine(LogicInit());//开始逻辑初始化
    }

    public void SendMessage(MessageBase Mesg)
    {
        ProcessMsg(Mesg);
    }

    /// <summary>
    /// 处理不同模块的消息
    /// </summary>
    /// <param name="tmpMesg"></param>
    public void ProcessMsg(MessageBase tmpMesg)
    {
        ManagerId tmpId = tmpMesg.GetID();
        switch (tmpId)
        {
            case ManagerId.UiManager:
                UIManager.Instance.SendData(tmpMesg);
                break;
            case ManagerId.GameManager:
                GameManager.Instance.SendData(tmpMesg);
                break;
            case ManagerId.Mainlogic:
                MainLogicManager.Instance.SendData(tmpMesg);
                break;
            case ManagerId.Data:
                DataManager.Instance.SendData(tmpMesg);
                break;
            case ManagerId.Asset:
                AssetManager.Instance.SendData(tmpMesg);
                break;
        }
    }

    /// <summary>
    /// 项目逻辑开始
    /// </summary>
    /// <returns></returns>
    //IEnumerator LogicInit()
    //{
    //    yield return new WaitForEndOfFrame();
    //    //gameObject.AddComponent<Logic_Run>();
    //    yield return new WaitForEndOfFrame();
    //    StopCoroutine(LogicInit());//开始逻辑初始化之后要停止这个协程
    //}

}
