using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.Barracuda;
using Unity.MLAgentsExamples;


public class StateMachineAgent : Agent
{
    int m_Configuration;
    public NNModel getTarget;
    public NNModel reachGoal;
    public NNModel reachStartpoint;
    public NNModel goBack;

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform env;
    [SerializeField] private Transform targetTransform;
    [SerializeField] public GameObject target;
    private Vector3 velocity;
    private Vector3 input;
    private Rigidbody rb;
    public float forwardSpeed = 10f;
    private Animator anim;
    public bool checkpointCheck;
    public bool targetOnTriggerEnter;



    string m_GetTargetBehaviorName = "GetTarget";
    string m_ReachGoalBehaviorName = "ReachGoal";
    string m_ReachStartpointBehaviorName = "ReachStartpoint";
    string m_GoBackBehaviorName = "GoBack";

    public override void Initialize()
    {
        m_Configuration = 1;
        targetOnTriggerEnter = false;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Debug.Log("Initialize");

        // Update model references if we're overriding
        var modelOverrider = GetComponent<ModelOverrider>();
        if (modelOverrider.HasOverrides)
        {
            getTarget = modelOverrider.GetModelForBehaviorName(m_GetTargetBehaviorName);
            m_GetTargetBehaviorName = ModelOverrider.GetOverrideBehaviorName(m_GetTargetBehaviorName);

            reachGoal = modelOverrider.GetModelForBehaviorName(m_ReachGoalBehaviorName);
            m_ReachGoalBehaviorName = ModelOverrider.GetOverrideBehaviorName(m_ReachGoalBehaviorName);

            reachStartpoint = modelOverrider.GetModelForBehaviorName(m_ReachStartpointBehaviorName);
            m_ReachStartpointBehaviorName = ModelOverrider.GetOverrideBehaviorName(m_ReachStartpointBehaviorName);

            goBack = modelOverrider.GetModelForBehaviorName(m_GoBackBehaviorName);
            m_GoBackBehaviorName = ModelOverrider.GetOverrideBehaviorName(m_GoBackBehaviorName);
        }


    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //AddReward(-1f / MaxStep);

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        velocity = Vector3.zero;
        input = new Vector3(moveX, 0f, moveZ);

        anim.SetFloat("Speed", input.magnitude + 1f);

        if (input.magnitude > 0f)
        {
            transform.LookAt(rb.position + input);

            velocity = rb.transform.forward * forwardSpeed;           
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        /*
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");*/
    }

    public override void OnEpisodeBegin()
    {
        switch (m_Configuration)
        {
            case 0:
                //transform.localPosition = new Vector3(Random.Range(-7f, 13f), 0, Random.Range(-3f, 14f));
                //Instantiate(target, new Vector3(Random.Range(-7f, 13f), 0, Random.Range(-3f, 14f)), Quaternion.identity);
                //targetTransform.localPosition = new Vector3(Random.Range(-7f, 13f), 0, Random.Range(-3f, 14f));
                targetOnTriggerEnter = false;
                break;            
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GoalLearning>(out GoalLearning GoalLearning))
        {
            targetOnTriggerEnter = true;
            targetTransform.localPosition = new Vector3(0, -200f, 0);
            //EndEpisode();
        }
        /*
        if (other.TryGetComponent<WallLearning>(out WallLearning WallLearning))
        {
            SetReward(-1f);
            EndEpisode();
        }
        */

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("change");
            if (m_Configuration == 0)
            {
                m_Configuration = 1;
                ConfigureAgent(m_Configuration);
                EndEpisode();
                
            }
            else if(m_Configuration == 1)
            {
                m_Configuration = 0;
                ConfigureAgent(m_Configuration);
                EndEpisode();
            }
        }        
    }

    public void ConfigureAgent(int config)
    {
        
        if (config == 0)
        {
            if (m_Configuration != 0)
            {
                SetModel(m_GetTargetBehaviorName, getTarget);
                m_Configuration = 0;
                EndEpisode();
            }
            
        }
        else if (config == 1)
        {
            if (m_Configuration != 1)
            {
                SetModel(m_ReachStartpointBehaviorName, reachStartpoint);
                m_Configuration = 1;
                
            }
        }
        else if (config == 2)
        {
            if (m_Configuration != 2)
            {
                SetModel(m_ReachGoalBehaviorName, reachGoal);
                m_Configuration = 2;
                
            }
            
        }
        else if (config == 3)
        {
            if (m_Configuration != 3)
            {
                SetModel(m_GoBackBehaviorName, goBack);
                m_Configuration = 3;
                targetTransform.localPosition = new Vector3(Random.Range(-7f, 13f), 0, Random.Range(-3f, 14f));

            }
        }
        //EndEpisode();
    }


}

    


