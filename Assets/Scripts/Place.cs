using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe gérant les préfabs Place, créant et maintenant une foule en mouvement centrée sur une place.
// Fait apparaître spawnRate agents par seconde, jusqu'à un maximum de maxPop, et les lie à la simulation
// gérée par controller.
//
//
// A plusieurs attributs:
//
// - agent, GameObject (prefab):
// L'agent à initialiser pour la simulation. 
// Doit avoir un component Agent, un Rigidbody et un Collider.
//
// - maxPop, int (par défaut 10):
// Nombre maximal d'agents initialisés en même temps.
//
// - spawnRate, float (par défaut 0.1f):
// Nombre d'agents à initialiser par seconde.
//
// - Les paramètres de AgentController, cf sa description.
//
//
// Propose une méthode:
//
// - Kill(GameObject agent), void:
// S'il y a lieu, détruit l'agent.


public class Place : MonoBehaviour {
    public GameObject agent;
    public int maxPop = 10;
    public float spawnRate = 0.1f;
    

    // --- Paramètres de AgentController ---
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


    private AgentController controller;   // l'AgentController responsable de la simulation
    private GameObject start;             // le Trigger dans lequel apparaissent les agents
    private GameObject finish;            // le Trigger hors duquel disparaissent les agents

    private Vector3 startCenter;          // la position du centre de start
    private Vector3 startSize;            // la taille (diminuée de moitié) de start
    private Vector3 startScale;           // l'échelle de start
    private Vector3 finishCenter;         // la position du centre de finish

    private int pop = 0;                  // le nombre d'agents simulés
    private float delay = 0f;             // le temps depuis la dernière apparition d'agent
    private float tgtDelay;               // le temps au bout duquel un agent doit apparaître

    private Vector3 tmpVect;              // la position à laquelle faire apparaître un agent
    private Quaternion angle;             // l'angle que fait le prefab avec Vector3.forward
    private float varX;                   // la variation X de la position d'apparition
    private float varY;                   // la variation Y de la position d'apparition


    // Initialise les paramètres de March et controller puis le force à Awake (au cas où il l'a déjà fait)
    private void Awake() {
        controller  = GetComponentInChildren<AgentController>();
        start       = transform.GetChild(1).gameObject;
        finish      = transform.GetChild(2).gameObject;

        startCenter  = start.transform.position;
        startSize    = start.GetComponent<BoxCollider>().size;
        startScale   = start.transform.localScale;
        finishCenter = finish.transform.position;
        angle        = transform.rotation;

        tgtDelay     = 1/spawnRate;
        startSize.x *= 0.5f * startScale.x;
        startSize.y *= 0.5f * startScale.y;
        startSize.z *= 0.5f * startScale.z;

        
        controller.generalDirection = new Vector2(startCenter.x, startCenter.z);
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


    // Fait potentiellement apparaître un agent
    private void Update() {
        delay += Time.deltaTime;

        if (delay >= tgtDelay && pop < maxPop) {                   // S'il faut faire apparaître un agent et qu'il n'y en a pas déjà trop
            tmpVect    = Vector3.zero;
            tmpVect.x += Random.Range(-startSize.x,startSize.x);
            tmpVect.z += Random.Range(-startSize.z,startSize.z);
            tmpVect    = angle * tmpVect + startCenter;

            Agent nAgent = Instantiate(agent, tmpVect, Quaternion.Euler(Vector3.zero), transform).GetComponent<Agent>();
            nAgent.controller = controller;

            delay = 0f;
            pop++;
        }
    }


    // Détruit potentiellement l'agent
    public void Kill(GameObject agent) {
        if (agent.transform.IsChildOf(transform)) {   // Si c'est un agent de controller
            Destroy(agent);
            pop--;
        }
    }
}
