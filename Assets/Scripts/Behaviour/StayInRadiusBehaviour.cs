using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    [SerializeField]
    private Vector2 center;

    [SerializeField]
    private float radius = 15f;

    [SerializeField]
    private float innerRadiusPercent =0.9f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float radiusFraction = centerOffset.magnitude / radius;

        if (radiusFraction < innerRadiusPercent)
        {
            return Vector2.zero;
        }
        return centerOffset * radiusFraction * radiusFraction;
    }
}
