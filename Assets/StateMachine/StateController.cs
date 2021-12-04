using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StateController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public StateMachineAgent stateMachineAgent;
    public State currentState;
    public Transform goal;
    public Transform goal2;
    public Transform Agent;
    public Transform Startpoint;
    public State remainState;


    


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachineAgent = GetComponent<StateMachineAgent>();
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
