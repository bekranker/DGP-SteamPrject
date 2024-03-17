using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FuncPredicate: IPredicie
{
    readonly Func<bool> func;

    public FuncPredicate(Func<bool> func)
    {
        this.func = func;
    }

    public bool Evaluate()
    {
        return func.Invoke();
    }
}