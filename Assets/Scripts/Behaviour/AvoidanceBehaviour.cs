using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent currentAgent, List<Transform> context,Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 avoidanceMove = Vector2.zero;
        int count = 0;

        List<Transform> filteredContext = context;

        if (filter != null)
        {
            filteredContext = filter.Filter(currentAgent,context);
        }
        
        foreach(Transform item in filteredContext)
        {
            // or if (Vector2.Distance(item.transform.position, currentAgent.transform.position) < flock.avoidanceRadiusMultiplier)
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
