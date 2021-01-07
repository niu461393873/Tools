﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : BaseClass
{

    public int[] msgIds;
    public void RegistSelf(BaseClass Blcs, params int[] msgs)
    {
        DataManager.Instance.RegisMsg(Blcs, msgs);
    }
    public void UnRegistSelf(BaseClass Blcs, params int[] msgs)
    {
        DataManager.Instance.UnRegist(Blcs, msgs);
    }
    public void SendMsg(MessageBase Blcs)
    {
        DataManager.Instance.SendData(Blcs);
    }

    /// <summary>
    /// 删除节点
    /// </summary>
    void OnDestroy()
    {
        if (msgIds != null)
        {
            UnRegistSelf(this, msgIds);
        }
    }
    public override void DisposeMesg(MessageBase Mesg)
    {

    }
}
