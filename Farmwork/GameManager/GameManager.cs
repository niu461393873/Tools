using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerCenter
{
    public static GameManager Instance = null;
    private Dictionary<string, GameObject> ObjList = new Dictionary<string, GameObject>();
    public int[] msgId;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }
    void Update()
    {
    }
    //对象注册
    public void RegistObj(string name, GameObject obj)
    {
        if (!ObjList.ContainsKey(name))
        {
            ObjList.Add(name, obj);
        }
    }
    /// <summary>
    /// 移除对象
    /// </summary>
    /// <param name="name"></param>
    public void UnRegistObj(string name)
    {
        if (ObjList.ContainsKey(name))
        {
            ObjList.Remove(name);
        }
    }
    /// <summary>
    /// 根据名字得到对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string name)
    {
        if (ObjList.ContainsKey(name))
        {
            return ObjList[name];
        }
        return null;
    }
    /// <summary>
    /// 发送消息到指定的模块
    /// </summary>
    /// <param name="msg"></param>
    public void SendData(MessageBase msg)
    {
        if (msg.GetID() == ManagerId.GameManager)
        {
            DisposeMesg(msg);
        }
        else
        {
            MesProcess.Instance.SendMessage(msg);
        }
    }


    void OnDestrooy()
    {
        if (msgId != null)
        {
            UnRegist(this, msgId);
        }
        foreach (KeyValuePair<string, GameObject> kv in ObjList)
        {
            UnRegistObj(kv.Key);
        }
        if (ObjList.Count > 0)
        {
            ObjList.Clear();
        }

    }

}
