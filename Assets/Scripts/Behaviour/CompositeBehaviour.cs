using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    [System.Serializable]
    public struct BehaviourGroup
    {
        public FlockBehaviour behaviour;
        public float weight;
    }

    public BehaviourGroup[] behaviours;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 move = Vector2.zero;

        foreach(BehaviourGroup currentBehaviour in behaviours) //this goes through the behaviours and averages them together which controls the movement
        {
            Vector2 partialMove = currentBehaviour.behaviour.CalculateMove(agent, context, flock);
            partialMove.Normalize();
            partialMove *= currentBehaviour.weight;

            move += partialMove;
        }
        return move;
    }
}
