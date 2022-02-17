using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public AgentController controller;


    private Vector3 generalDirection;
    private float FOVRadius;

    private float coefInertie;
    private float coefDirection;
    private float coefDistanciation;
    private float coefGroupement; 


    private Rigidbody body;
    private float radius;
    private Vector3 position;
    private Vector3 direction;
    private float speed;
    private Collider[] neighbors;



    void Awake()
    {
        body = GetComponent<Rigidbody>();
        radius = GetComponent<Renderer>().bounds.extents.magnitude;

        Debug.Log(radius);

        generalDirection = new Vector3(controller.generalDirection.x, 0f, controller.generalDirection.y);
        speed = controller.speed;
        FOVRadius = controller.FOVRadius;
        coefInertie = controller.coefInertie;
        coefDirection = controller.coefDirection;
        coefDistanciation = controller.coefDistanciation;
        coefGroupement = controller.coefGroupement;

        direction = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f));
        direction.Normalize();
    }


    private Vector3 Calc_Distanciation() {
        float norme;
        Vector3 tmp;
        Vector3 impact = new Vector3();

        foreach (var neighbor in neighbors)
        {
            if (neighbor.tag == "Agent") {
                tmp = neighbor.transform.position - position;
                norme = tmp.magnitude;

                if (norme != 0) {
                    tmp.Normalize();
                    impact -=  ((1/norme) * FOVRadius * tmp) - tmp;
                }
            }
        }

        impact.y = 0;
        return impact;
    }


    private Vector3 Calc_Groupement() {
        int n = 0;
        Vector3 impact = new Vector3();

        foreach (var neighbor in neighbors)
        {
            if (neighbor.tag == "Agent") {
                n++;
                impact += neighbor.transform.position;
            }
        }

        impact = (impact / n) - position;
        impact.y = 0;
        return impact;
    }


    private void MaJ_Position() {
        position = transform.position;
    }


    //attention, ne compte PAS QUE les agents: tout ce qui a un collider est compté (ie le sol par exemple)
    private void MaJ_Neighbors() {
        neighbors = Physics.OverlapSphere(position, FOVRadius);
    }


    private void MaJ_Direction() {
        Vector3 distanciation = Calc_Distanciation();
        Vector3 groupement = Calc_Groupement();

        direction = coefInertie * direction + generalDirection * coefDirection + distanciation * coefDistanciation + coefGroupement * groupement;
        direction.Normalize();
    }


    void Update()
    {
        MaJ_Position();
        MaJ_Neighbors();
        MaJ_Direction();

        body.velocity = direction * speed;
    }
}
