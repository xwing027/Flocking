using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent currentAgent, List<Transform> context,Flock flock)
    {
        if (context.Count == 0) //if list is empty
        {
            return Vector2.zero; //exit early
        }

        Vector2 avoidanceMove = Vector2.zero;
        int count = 0;

        List<Transform> filteredContext = context;

        if (filter != null) //if there is a filter
        {
            filteredContext = filter.Filter(currentAgent,context); //put filters in the list
        }
        
        foreach(Transform item in filteredContext) //for each item in the list
        {
            //or if (Vector2.Distance(item.transform.position, currentAgent.transform.position) < flock.avoidanceRadiusMultiplier)
            if((item.transform.position - currentAgent.transform.position).sqrMagnitude <= flock.squareNeighbourRadius)
            { 
                count++;
                avoidanceMove += (Vector2)(currentAgent.transform.position - item.transform.position); 
            }
        }
        avoidanceMove /= count;

        return avoidanceMove;
    }
}
