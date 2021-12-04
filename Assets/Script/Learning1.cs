using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityChan;


public class Learning1 : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform startPoint;
    private Vector3 velocity;
    private Vector3 input;
    private Rigidbody rigidbody;
    public float forwardSpeed = 7.0f;
    private Animator anim;
    private int count;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-7f, 13f), 0, Random.Range(-3f, 14f));
        //targetTransform.localPosition = new Vector3(Random.Range(-7f, 13f), 0, Random.Range(-3f, 14f));
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        CountReset();
    }

    void CountReset()
    {
        count = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(-1f / MaxStep);

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        velocity = Vector3.zero;
        input = new Vector3(moveX, 0f, moveZ);

        anim.SetFloat("Speed", input.magnitude + 1f);

        if (input.magnitude > 0f)
        {
            transform.LookAt(rigidbody.position + input);

            velocity = rigidbody.transform.forward * forwardSpeed;
            //Å@ÉLÅ[ÇÃâüÇµÇ™è¨Ç≥Ç∑Ç¨ÇÈèÍçáÇÕà⁄ìÆÇµÇ»Ç¢
        }

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        if (transform.position.y < -5f)
        {
            AddReward(-10f);
            EndEpisode();
        }

        /*
        float moveSpeed = 10f;
        transform.position += new Vector3(unityChanControl.moveX, 0, unityChanControl.moveZ) * Time.deltaTime * moveSpeed;
        */
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.TryGetComponent<GoalLearning>(out GoalLearning GoalLearning))
        {
            SetReward(+1f);
            EndEpisode();
        }
        
        if (other.TryGetComponent<WallLearning>(out WallLearning WallLearning))
        {
            SetReward(-1f);
            EndEpisode();
        }*/

        if (other.TryGetComponent<Checkpoint>(out Checkpoint checkpoint))
        {
            SetReward(+1f);
            EndEpisode();
        }


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
