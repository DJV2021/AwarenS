using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public Vector2 generalDirection = new Vector2(1f,0f);
    public float speed = 1f;
    public float FOVRadius = 5f;

    public float coefInertie = 250f;
    public float coefDirection = 1f;
    public float coefDistanciation = 1f;
    public float coefGroupement = 1f;



    void Awake() {
        generalDirection.Normalize();
    }
}
