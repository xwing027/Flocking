using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    public Flock parentFlock;
    public Collider2D AgentCollider;

    private void Start()
    {
        AgentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        parentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity; //point agent towards where we are going
        transform.position += (Vector3)velocity * Time.deltaTime; //move agent forward
    }
}
