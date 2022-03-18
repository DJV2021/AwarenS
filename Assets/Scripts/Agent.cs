using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Classe définissant le GameObject auquel elle est attachée comme un agent vis-à-vis de l'algorithme de flocking,
// puis en génère le déplacement en temps réel lorsque le jeu tourne. Ceci se fait en modifiant, à chaque frame, le
// vecteur vitesse du GameObject (qui doit donc avoir un RigidBody et un Collider) et non via une translation. De plus, 
// l'algo ne gère les déplacement qu'en deux dimensions (selon les axes X et Z, au sol), la gravité n'est pas affectée (TODO).
//
// Le seul paramètre à régler pour l'agent est son controller: il faut lui indiquer quel GameObject doté d'un
// AgentController supervise sa simulation. C'est à travers lui que seront déterminés les paramètres de la
// simulation eux-mêmes (et c'est dans son script qu'ils sont explicités).


public class Agent : MonoBehaviour {
    public AgentController controller;  // l'observer gérant la simulation

    // --- Paramètres de simulation ---
    private Vector3 generalDirection;
    private bool isDestination;
    private float FOVRadius;
    private float coefInertia;
    private float coefDirection;
    private float coefDistancing;
    private float coefGrouping;


    private Rigidbody body;        // le RigidBody de l'agent
    private Vector3 position;      // sa position actuelle
    private Vector3 direction;     // sa direction actuelle
    private float speed;           // sa vitesse (constante)
    private Vector3 velocity;      // son vecteur vitesse actuel
    private Collider[] neighbors;  // la liste des GameObjects actuellement voisins

    // Initialise l'agent
    private void Start() {
        gameObject.tag = "Agent";

        body = GetComponent<Rigidbody>();
        direction = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f));
        direction.Normalize();
        velocity = new Vector3();

        // --- Apparopriation des paramètres de la simulation par l'agent ---
        generalDirection = new Vector3(controller.generalDirection.x, 0f, controller.generalDirection.y);
        isDestination = controller.isDestination;
        speed = controller.speed * Random.Range(1-controller.varSpeed, 1+controller.varSpeed);
        FOVRadius = controller.FOVRadius * Random.Range(1-controller.varFOV, 1+controller.varFOV);
        coefInertia = controller.coefInertia * Random.Range(1-controller.varInertia, 1+controller.varInertia);
        coefDirection = controller.coefDirection * Random.Range(1-controller.varDirection, 1+controller.varDirection);
        coefDistancing = controller.coefDistancing * Random.Range(1-controller.varDistancing, 1+controller.varDistancing);
        coefGrouping = controller.coefGrouping * Random.Range(1-controller.varGrouping, 1+controller.varGrouping);
    }


    // Calcule le vecteur de distanciation
    private Vector3 Calc_Distancing() {
        float norm;
        Vector3 tmp;
        Vector3 impact = new Vector3();

        foreach (Collider neighbor in neighbors) {
            if (neighbor.tag == "Agent") {                      // Si c'est un autre agent
                tmp = neighbor.transform.position - position;
                norm = tmp.magnitude;

                if (norm != 0) {
                    tmp.Normalize();
                    impact -=  ((1/norm) * FOVRadius * tmp) - tmp;
                }
            }

            if (neighbor.tag == "Obstacle") {                               // Si c'est un obstacle
                tmp = neighbor.ClosestPointOnBounds(position) - position;
                norm = tmp.magnitude;

                if (norm != 0) {
                    tmp.Normalize();
                    impact -=  ((1/norm) * FOVRadius * tmp) - tmp;
                }
            }
        }

        impact.y = 0;
        return impact;
    }

    // Calcule le vecteur de regroupement
    private Vector3 Calc_Grouping() {
        int n = 0;
        Vector3 impact = new Vector3();

        foreach (var neighbor in neighbors) {
            if (neighbor.tag == "Agent") {               // Si c'est un autre agent
                n++;
                impact += neighbor.transform.position;
            }
        }

        impact = (impact / n) - position;
        impact.y = 0;
        return impact;
    }


    // Met à jour le vecteur position mis en mémoire (pour ne pas multiplier les appels à transform)
    private void MaJ_Position() {
        position = transform.position;
    }

    // Met à jour la liste des GameObjects voisins de l'agent (pour ne surtout pas multiplier les appels à OverlapSphere)
    // attention, ne compte PAS QUE les agents: tout ce qui a un collider est compté (ie le sol par exemple)
    private void MaJ_Neighbors() {
        neighbors = Physics.OverlapSphere(position, FOVRadius);
    }

    // Met à jour le vecteur direction à l'aide des méthodes implémentées
    private void MaJ_Direction() {
        Vector3 Distancing = Calc_Distancing();
        Vector3 Grouping = Calc_Grouping();

        // isDestination change la façon de comprendre le vecteur direction
        if (isDestination) {
            direction = coefInertia * direction + (generalDirection-position) * coefDirection + Distancing * coefDistancing + coefGrouping * Grouping;
        }
        else {
            direction = coefInertia * direction + generalDirection * coefDirection + Distancing * coefDistancing + coefGrouping * Grouping;
        }

        direction.Normalize();  // la vitesse de l'agent reste constante, seule sa direction varie
    }


    // Met à jour les paramètres puis le body.velocity
    private void Update() {
        MaJ_Position();
        MaJ_Neighbors();
        MaJ_Direction();

        // pour ne pas interférer avec l'accélération verticale, ie la gravité
        velocity.x = speed * direction.x; 
        velocity.y = body.velocity.y; 
        velocity.z = speed * direction.z;

        body.velocity = velocity;
    }
}
