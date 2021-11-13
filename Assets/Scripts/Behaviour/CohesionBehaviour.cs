using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent currentAgent, List<Transform> context, Flock flock) //this will calculate the move for each individual agent
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 cohesionMove = Vector2.zero;
        int count = 0;

        List<Transform> filteredContext = context;
        if (filter!=null)
        {
            filteredContext = filter.Filter(currentAgent, context);
        }

        foreach(Transform item in filteredContext) //for every agent in the flock
        {
            if ((item.transform.position - currentAgent.transform.position).sqrMagnitude <= flock.squareNeighbourRadius) //find the distance bewteen item and current agent
            {
                //move them towards each other
                cohesionMove += (Vector2)item.transform.position;
                count++;
            }
        }
        cohesionMove /= count; //now we have calculated the avg pos of all the agents within a radius

        //to get direction from a to b we do b-a
        cohesionMove = cohesionMove-(Vector2)currentAgent.transform.position;

        //this will return the direction to the avg agent pos
        return cohesionMove;
    }
}
