using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StateMachine : MonoBehaviour
{
    
    
    StateNode _current;


    Dictionary<Type, StateNode> _nodes = new();
    HashSet<ITransation> _anyTransitions = new();


    public void Update()
    {
        var transition = GetTransation();
        if (transition != null)
        {
            ChangeState(transition.To);
        }
        _current.State?.Update();
    }
    public void FixedUpdate()
    {
        _current.State?.FixedUpdate();
    }
    public void SetState(IState state)
    {
        _current = _nodes?[state.GetType()];
        _current.State?.OnEnter();
    }
    void ChangeState(IState state)
    {
        if (state == _current.State) return;

        var previousState = _current.State;
        var nextState = _nodes[state.GetType()].State;
        previousState.OnExit();
        nextState?.OnEnter();
        _current = _nodes[state.GetType()];

    }
    ITransation GetTransation()
    {
        foreach (var transition in _anyTransitions)
            if (transition.Condition.Evaluate())
                return transition;


        foreach(var transition in _current.Transitions)
            if(transition.Condition.Evaluate())
                return transition;

        return null;
    }

    public void AddTransition(IState from, IState to, IPredicie condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }
    public void AddAnyTransition(IState to, IPredicie condition)
    {
        _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }
    StateNode GetOrAddNode(IState state)
    {
        var node = _nodes.GetValueOrDefault(state.GetType());

        if (node == null)
        {
            node = new StateNode(state);
            _nodes.Add(state.GetType(), node);
        }
        return node;
    }

    class StateNode
    {
        public IState State {get;}
        public HashSet<ITransation> Transitions {get;}
        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<ITransation>(); 
        }

        public void AddTransition(IState to, IPredicie condition)
        {
            Transitions.Add(new Transition(to, condition));
        }
    }
}