using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : ITransation
{
    public IState To { get; }
    public IPredicie Condition { get; }


    public Transition(IState to, IPredicie condition)
    {
        To = to;
        Condition = condition;
    }
}