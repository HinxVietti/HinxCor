using System;
using System.Collections.Generic;


public class OperationHistory : IOperationHistory
{
    [Obsolete("OHCUR 用双链表实现")]
    public List<IOperation> History { get; set; }

    public bool CodRedo
    {
        get
        {
            return current != null;
        }

        set
        {
            if (!value)
            {
                Do(null);
            }
        }
    }
    public bool CodUndo
    {
        get
        {
            return current != null;
        }

        set
        {
            if (!value)
            {
                Do(null);
            }
        }
    }

    private IOperation current;

    public void Do(IOperation cmd)
    {
        //if (History == null) History = new List<IOperation>();
        //History.Add(cmd);
        if (current != null)
        {
            Clear(current);
            current.next = cmd;
            cmd.previour = current;
        }
        current = cmd;
    }


    public void Redo()
    {
        if (current != null)
        {
            current.Redo();
            if (current.next != null)
                current = current.next;
        }
    }


    public void Undo()
    {
        if (current != null)
        {
            current.Undo();
            if (current.previour != null)
                current = current.previour;
        }
    }

    /// <summary>
    /// 清除改项的子集
    /// </summary>
    /// <param name="current"></param>
    public static void Clear(IOperation current)
    {
        try
        {

            if (current != null)
                if (current.next != null)
                {
                    Clear(current.next);
                    current.next.previour = null;
                    current.next = null;
                }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }
    }
    /// <summary>
    /// 清除改项的子集
    /// </summary>
    /// <param name="current"></param>
    public static void ClearReverse(IOperation current)
    {
        if (current != null)
            if (current.previour != null)
            {
                ClearReverse(current.previour);
                current.previour.next = null;
                current.previour = null;
            }
    }

    //清除该项所在的链表
    public static void CLC(IOperation current)
    {
        Clear(current);
        ClearReverse(current);
        current = null;
    }

}

