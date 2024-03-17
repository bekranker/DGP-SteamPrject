using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITransation
{
    IState To {get;}
    IPredicie Condition {get;}
}