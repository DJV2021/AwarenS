using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class March : MonoBehaviour
{
    public GameObject agent;
    public int maxPop = 10;
    public float spawnRate = 0.1f;
    

    public float speed = 1f;
    public float FOVRadius = 5f;

    public float coefInertia = 250f;
    public float coefDirection = 1f;
    public float coefDistancing = 1f;
    public float coefGrouping = 1f;

    public float varSpeed = 10f;
    public float varFOV = 10f;
    public float varInertia = 10f;
    public float varDirection = 10f;
    public float varDistancing = 10f;
    public float varGrouping = 10f;


    private AgentController controller;
    private GameObject start;
    private GameObject finish;

    private Vector3 startCenter;
    private Vector3 startSize;
    private Vector3 startScale;
    private Vector3 finishCenter;

    private int pop = 0;
    private float delay = 0f;
    private float tgtDelay;

    private Vector3 tmpVect;


    void Awake() {
        controller  = GetComponentInChildren<AgentController>();
        start       = transform.GetChild(1).gameObject;
        finish      = transform.GetChild(2).gameObject;

        startCenter  = start.transform.position;
        startSize    = start.GetComponent<BoxCollider>().size;
        startScale   = start.transform.localScale;
        finishCenter = finish.transform.position;

        tgtDelay     = 1/spawnRate;
        startSize.x *= 0.5f * startScale.x;
        startSize.y *= 0.5f * startScale.y;
        startSize.z *= 0.5f * startScale.z;

        
        controller.generalDirection = new Vector2((finishCenter.x - startCenter.x), (finishCenter.z - startCenter.z));
        controller.speed            = speed;
        controller.FOVRadius        = FOVRadius;

        controller.coefInertia    = coefInertia;
        controller.coefDirection  = coefDirection;
        controller.coefDistancing = coefDistancing;
        controller.coefGrouping   = coefGrouping;

        controller.varSpeed      = varSpeed;
        controller.varFOV        = varFOV;
        controller.varInertia    = varInertia;
        controller.varDirection  = varDirection;
        controller.varDistancing = varDistancing;
        controller.varGrouping   = varGrouping;

        controller.Awake();
    }


    void Update() {
        delay += Time.deltaTime;

        if (delay >= tgtDelay && pop < maxPop) {
            tmpVect = startCenter;
            tmpVect.x += Random.Range(-startSize.x,startSize.x);
            tmpVect.z += Random.Range(-startSize.z,startSize.z);

            Agent nAgent = Instantiate(agent, tmpVect, Quaternion.Euler(Vector3.zero), transform).GetComponent<Agent>();
            nAgent.controller = controller;

            delay = 0f;
            pop++;
        }
    }


    public void Kill(GameObject agent) {
        if (agent.transform.IsChildOf(transform)) {
            Destroy(agent);
            pop--;
        }
    }
}
