using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.RandomRange(-4f, 4f), 0, Random.RandomRange(-4f, 2f));
        target.localPosition = new Vector3(Random.RandomRange(-4f, 4f), 0, 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
            SetReward(-1f);
        else
            SetReward(1f);

        EndEpisode();
    }

}
