using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant le GameObject auquel elle est attachée comme l'observer vis-à-vis de l'algorithme de flocking.
// Son seul rôle est d'en définir les paramètres: tous les agents de la simulation doivent lui être liés.
//
//
// Ses paramètres modifiables sont:
//
// - generalDirection, Vector2: 
// Vecteur 2D définissant soit la direction générale suivie par les agents, soit les 
// coordonnées du point qu'ils essaient de rejoindre (cf isDestination),
//
// - isDestination, bool: 
// Détermine si le vecteur generalDestination est à considérer comme une direction (si false)
// ou comme une destination (si true) (cf generalDirection),
//
// - speed, float: 
// Vitesse (constante) des agents, en unité de vitesse de Unity
//
// - FOVRadius, float: 
// Détermine la distance jusqu'à laquelle l'agent est capable de détecter les autres agents. Il
// ne prendra pas ceux hors de ce cercle en compte dans ses calculs,
//
// - coefInertia, float:
// Multplicateur appliqué à la direction suivie au moment du calcul. Plus il est élevé, plus
// les agents auront de mal à changer de direction,
//
// - coefDirection, float:
// Multiplicateur appliqué à la direction/destination imposée par l'observer (cf generalDirection).
// Plus il est élevé, plus rapidement les agents suivront la suivront,
//
// - coefDistancing, float:
// Multiplicateur appliqué à l'éloignement des agents. Plus il est élevé, plus les agents auront
// tendance à se garder à ditance des autres.
//
// - coefGrouping, float:
// Multiplicateur appliqué au regroupement des agents. Plus il est élevé, plus les agents auront
// tendance à se regrouper.
//
// - varSpeed, float:
// Pourcentage de variation possible sur speed d'un agent à l'autre. Les valeurs finales seront
// comprises dans [speed-varSpeed%, speed+varSpeed%], avec une probabilité uniforme,
//
// - varFOV, float:
// Pourcentage de variation possible sur FOVRadius d'un agent à l'autre. Les valeurs finales seront
// comprises dans [FOVRadius-varFOV%, FOVRadius+varFOV%], avec une probabilité uniforme,
//
// - varInertia, float:
// Pourcentage de variation possible sur coefInertia d'un agent à l'autre. Les valeurs finales seront
// comprises dans [coefInertia-varInertia%, coefInertia+varInertia%], avec une probabilité uniforme,
//
// - varDirection, float:
// Pourcentage de variation possible sur coefDirection d'un agent à l'autre. Les valeurs finales seront
// comprises dans [coefDirection-varDirection%, coefDirection+varDirection%], avec une probabilité uniforme,
//
// - varDistancing, float:
// Pourcentage de variation possible sur coefDistancing d'un agent à l'autre. Les valeurs finales seront
// comprises dans [coefDistancing-varDistancing%, coefDistancing+varDistancing%], avec une probabilité uniforme,
//
// - varGrouping, float:
// Pourcentage de variation possible sur coefGrouping d'un agent à l'autre. Les valeurs finales seront
// comprises dans [coefGrouping-varGrouping%, coefGrouping+varGrouping%], avec une probabilité uniforme,

public class AgentController : MonoBehaviour
{
    public Vector2 generalDirection = new Vector2(1f,0f);
    public bool isDestination = false;
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


    // calcule les pourcentages, normalise le vecteur direction s'il y a lieu
    public void Awake() {
        varSpeed /= 100;
        varFOV /= 100;
        varInertia /= 100;
        varDirection /= 100;
        varDistancing /= 100;
        varGrouping /= 100;

        if (!isDestination) {
            generalDirection.Normalize();
        }
    }
}
