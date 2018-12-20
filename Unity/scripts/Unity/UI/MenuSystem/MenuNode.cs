using HinxCor.UI;
using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class MenuNode<T>: Selectable
{
    public string Name { get; set; }
    public List<Action> Actions { get; set; }
    public Action Action
    {
        get
        {
            if (Actions != null && Actions.Count > 0)
                return Actions[0];
            return null;
        }
        set
        {
            if (Actions == null)
                Actions = new List<Action>();
            Actions[0] = value;
        }
    }

    public List<T> ChildItems { get; set; }


    public virtual void AddChildNode(T item)
    {
        if (ChildItems == null) ChildItems = new List<T>();
        ChildItems.Add(item);
    }
    public virtual bool Contains(T item)
    {
        try
        {
            return ChildItems.Contains(item);
        }
        catch { return false; }
    }
    public virtual void RemoveChildNode(T item)
    {
        if (ChildItems.Contains(item))
            ChildItems.Remove(item);
    }

    public void Invoke()
    {
        try
        {
            foreach (var method in Actions)
                method();
        }
        catch (Exception e)
        {
            Debug.LogError(Name + " Invoke has error ." + e);
        }
    }


    //public virtual void OnEnter() { }
    //public virtual void OnExit() { }
    //public virtual void OnHover() { }

}

