using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManagerId
{
    UiManager = MangerTootle.msgConst,//属于UI层
    GameManager = MangerTootle.msgConst * 2,//属于3D场景层
    Mainlogic = MangerTootle.msgConst * 3,//主逻辑层
    Data = MangerTootle.msgConst * 4,// 工艺数据层
    Asset = MangerTootle.msgConst * 5,//资源层
}

public class MessageBase
{
    public int mesId = 0;
    public MessageBase()
    {
        mesId = 0;
    }

    public ManagerId GetID()
    {
        int id = mesId / MangerTootle.msgConst;
        return (ManagerId)(id * MangerTootle.msgConst);
    }

    public MessageBase(int tempId)
    {
        mesId = tempId;
    }

    //改变当前的状态
    public void AlterId(int tempId)
    {
        mesId = tempId;
    }
}

public class MangerTootle
{
    public const int msgConst = 1000;
}
