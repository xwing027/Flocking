using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent currentAgent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 cohesionMove = Vector2.zero;
        int count = 0;

        List<Transform> filteredContext = context;
        if (filter != null)
        {
            filteredContext = filter.Filter(currentAgent, context);
        }

        foreach (Transform item in filteredContext)
        {
            if ((item.transform.position - currentAgent.transform.position).sqrMagnitude <= flock.squareNeighbourRadius)
            {
                cohesionMove += (Vector2)item.position;
                count++;
            }
        }

        if (count !=0)
        {
            cohesionMove /= count;
        }

        //cohesion is the average position agents want to move towards
        //this makes cohesion the direction towards that position
        cohesionMove = cohesionMove- (Vector2)currentAgent.transform.position;
        //this makes it so instad of jumping towards the direction, it takes a small step (becomes smooth)
        cohesionMove = Vector2.SmoothDamp(currentAgent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
