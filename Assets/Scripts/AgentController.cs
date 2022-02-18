using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public Vector2 generalDirection = new Vector2(1f,0f);
    public bool isDestination = false;
    public float speed = 1f;
    public float FOVRadius = 5f;

    public float coefInertie = 250f;
    public float coefDirection = 1f;
    public float coefDistanciation = 1f;
    public float coefGroupement = 1f;

    public float varSpeed = 0f;
    public float varFOV = 0f;
    public float varInertie = 0f;
    public float varDirection = 0f;
    public float varDistanciation = 0f;
    public float varGroupement = 0f;



    void Awake() {
        varSpeed /= 100;
        varFOV /= 100;
        varInertie /= 100;
        varDirection /= 100;
        varDistanciation /= 100;
        varGroupement /= 100;

        if (!isDestination) {
            generalDirection.Normalize();
        }
    }
}
