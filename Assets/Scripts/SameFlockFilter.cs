using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original) 
    {
        List<Transform> filtered = new List<Transform>(); 

        foreach (Transform item in original) //ai will look at every around it 
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();

            if (itemAgent!=null) //will put flock agents into the filtered list
            {
                if (itemAgent.parentFlock == agent.parentFlock)
                {
                    filtered.Add(item);
                }
            }
        }
        return filtered;
    }
}
