using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StateController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public State currentState;
    public Transform goal;
    public Transform goal2;
    public Transform Agent;
    public State remainState;


    private bool aiActive;


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();        
    }

    
    private void Update()
    {
        
        currentState.UpdateState(this);        
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}
