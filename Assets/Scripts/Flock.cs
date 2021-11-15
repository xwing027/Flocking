using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    #region Variables
    public FlockAgent agentPrefab;
    public FlockBehaviour fBehaviour;

    [Range(1,500)]
    public int startingCount = 20;
    public float agentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f; //how the agent moves forward
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    public float squareMaxSpeed;
    public float squareNeighbourRadius;
    public float squareAvoidanceRadius;

    public List<FlockAgent> agents = new List<FlockAgent>();
    #endregion

    void Start()
    {
        //squaring the variables
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius* avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int n = 0; n < startingCount; n++) //for the agents in the flock
        {
            Vector2 newPosition = ((Vector2)transform.position) + Random.insideUnitCircle * startingCount * agentDensity; //set random pos
            Quaternion newRotation = Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)); //set random rotation
            FlockAgent newAgent = Instantiate(agentPrefab,newPosition,newRotation,transform); //spawn in with the pos and rot

            newAgent.Initialize(this);
            newAgent.name = "Agent " + n; //label the agent
            agents.Add(newAgent); //add new agent to the list
        }
    }

    private void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            
            //Testing
            //agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.green,context.Count/6f); //change the colour from white to green as they spread out

            Vector2 move = fBehaviour.CalculateMove(agent,context,this);
            move *= driveFactor;

            if (move.sqrMagnitude>squareMaxSpeed) //if above max speed
            {
                move = move.normalized * maxSpeed; //set it back down to the max (keep them at a consistent speed)
            }

            agent.Move(move);
        }
    }

    public List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius); //get the collider

        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider) //make sure its not it's own collider
            {
                context.Add(c.transform); //if not add to the list
            }
        }

        return context;
    }
}
