using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent currentAgent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return currentAgent.transform.up;
        }

        Vector2 alignmentMove = Vector2.zero;
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
                alignmentMove += (Vector2)item.transform.up; 
                count++;
            }
        }
        alignmentMove /= count;

        return alignmentMove;
    }
}
