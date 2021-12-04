using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityChan;

public class learning2 : Agent
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform env;
    private Vector3 velocity;
    private Vector3 input;
    private Rigidbody rigidbody;
    public float forwardSpeed = 7.0f;
    private Animator anim;
    private float checkPointNum = 30f;
    public bool checkpointCheck;
    private int count;
    private bool Grounded;


    Checkpoint _checkpoint;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = startPoint.localPosition;
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        CheckPointReset();
        CountReset();
    }

    void CheckPointReset()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        foreach (GameObject checkpoint in checkpoints)
        {
            if (checkpoint.transform.position.x < env.position.x + 30F &&
                checkpoint.transform.position.x > env.position.x - 30F &&
                checkpoint.transform.position.z < env.position.z + 15F &&
                checkpoint.transform.position.z > env.position.z - 15F )
            {
                _checkpoint = checkpoint.GetComponent<Checkpoint>();
                _checkpoint.checkpointCheck = true;

            }
            
        }
    }

    void CountReset()
    {
        count = 0;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(-0.01f);

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        velocity = Vector3.zero;
        input = new Vector3(moveX, 0f, moveZ);

        anim.SetFloat("Speed", input.magnitude);

        if (input.magnitude > 0f)
        {
            transform.LookAt(rigidbody.position + input);

            velocity = rigidbody.transform.forward * forwardSpeed;
            
        }

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        if (transform.position.y < -5f)
        {
            EndEpisode();
        }

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Checkpoint>(out Checkpoint checkpoint))
        {
            if (checkpoint.GetComponent<Checkpoint>().checkpointCheck)
            {
                AddReward(+1f);
                checkpoint.GetComponent<Checkpoint>().checkpointCheck = false;
                //Debug.Log("check");
            }
            
            
        }

        if (other.TryGetComponent<GoalLearning>(out GoalLearning goalLearning))
        {
            SetReward(+10f);
            EndEpisode();
        }
        /*
        if (other.TryGetComponent<WallLearning>(out WallLearning wallLearning))
        {
            SetReward(-1f);
            Debug.Log("wall");
            EndEpisode();
        }
        */

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            AddReward(-0.1f);
            count += 1;
        }

        if (count >= 100)
        {
            EndEpisode();
        }        
    }
}
