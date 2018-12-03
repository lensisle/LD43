using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ActionLogic : ScriptableObject
{
    protected Action _callNext;

    public void PrepareLogic(Action callNext) 
    {
        _callNext = callNext;
    }

    public abstract void Execute();
    public abstract void Finish();
}
