using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNode//节点
{
    public BaseClass data;//当前数据
    public EventNode next;//下一个节点

    public EventNode(BaseClass tmpBas)
    {
        this.data = tmpBas;
        this.next = null;
    }
}

/// <summary>
/// 数据管理中心
/// </summary>
public class ManagerCenter : BaseClass
{
    //存储事件
    private Dictionary<int, EventNode> eventNode = new Dictionary<int, EventNode>();//int-消息ID,EventNode-data链表
    private Dictionary<int, MessageBase> MsgDic = null;//int-消息ID,MessageBase-消息

    public ManagerCenter()
    {
        MsgDic = new Dictionary<int, MessageBase>();
    }

    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="baseClass"></param>
    /// <param name="msg"></param>
    public void RegisMsg(BaseClass baseClass, params int[] msg)
    {
        for (int i = 0; i < msg.Length; i++)
        {
            EventNode tmpNode = new EventNode(baseClass);
            RegisMsg(msg[i], tmpNode);
        }
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="node"></param>
    public void RegisMsg(int id, EventNode node)
    {
        if (!eventNode.ContainsKey(id))
        {
            eventNode.Add(id, node);
        }
        else
        {
            EventNode tmp = eventNode[id];
            if (tmp == null)
            {
                if (node != null)
                {
                    eventNode[id] = node;
                    Proces_MsgDic(id);
                }
                else
                {
                    Debug.LogError("node为空==  " + id);
                }

            }
            else
            {
                while (tmp.next != null)
                {
                    tmp = tmp.next;
                }
                tmp.next = node;
            }
        }
    }


    /// <summary>
    /// 删除节点
    /// </summary>
    /// <param name="bclass"></param>
    /// <param name="msgs"></param>
    public void UnRegist(BaseClass bclass, params int[] msgs)
    {
        for (int i = 0; i < msgs.Length; i++)
        {
            unRegisMsg(msgs[i], bclass);
        }
    }

    /// <summary>
    /// 注销事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="node"></param>
    public void unRegisMsg(int id, BaseClass node)
    {
        if (!eventNode.ContainsKey(id))
        {
            return;
        }
        else
        {
            EventNode tmp = eventNode[id];
            if (tmp.data == node)//删除头部
            {
                EventNode header = tmp;

                if (header.next != null)
                {
                    eventNode[id] = tmp.next;
                    header.next = null;
                }
                else//只有一个节点
                {
                    eventNode.Remove(id);
                }
            }
            else//去掉尾部和中间节点
            {
                while (tmp.next != null && tmp.next.data != node)
                {
                    tmp = tmp.next;
                }
                if (tmp.next.next != null)//去掉中间节点 
                {
                    EventNode curNode = tmp.next;
                    tmp.next = curNode.next;
                    curNode.next = null;//把相关联的指针释放
                }
                else//去掉尾部的
                {
                    tmp.next = null;
                }
            }
        }
    }


    /// <summary>
    /// 用来处理多个消息
    /// </summary>
    /// <param name="Mesg"></param>
    public override void DisposeMesg(MessageBase Mesg)
    {
        if (!eventNode.ContainsKey(Mesg.mesId))
        {
            eventNode.Add(Mesg.mesId, null);
            MsgDic.Add(Mesg.mesId, Mesg);
            return;
        }
        else
        {
            EventNode tmpNode = eventNode[Mesg.mesId];

            if (tmpNode == null)
            {
                return;
            }
            do
            {
                tmpNode.data.DisposeMesg(Mesg);
                tmpNode = tmpNode.next;
            }
            while (tmpNode != null);
        }
    }

    /// <summary>
    ///  处理后注册的对象(注：主要针对初始化)
    /// </summary>
    /// <param name="MsgEvent"></param>
    public void Proces_MsgDic(int MsgEvent)
    {
        if (MsgDic.Count > 0)
        {
            List<MessageBase> mesList = new List<MessageBase>(MsgDic.Values);
            List<int> keyList = new List<int>(MsgDic.Keys);
            for (int i = 0; i < keyList.Count; i++)
            {
                if (keyList[i] == MsgEvent)
                {
                    if (mesList[i] != null)
                    {
                        DisposeMesg(mesList[i]);
                    }
                }
            }
        }
    }

}
