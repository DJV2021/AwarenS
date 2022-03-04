using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Classe gérant l'attribution de modèle et l'orientation du prefab Person.
//
// 
// A plusieurs attributs:
//
// - model, GameObject (prefab):
// Les 8 modèles possible que peut prendre le prefab.


public class Person : MonoBehaviour {
    public GameObject model1;
    public GameObject model2;
    public GameObject model3;
    public GameObject model4;
    public GameObject model5;
    public GameObject model6;
    public GameObject model7;
    public GameObject model8;

    private int personModel;        // le modèle pris par le GameObject
    private Rigidbody personBody;   // le RigidBody du Gameobject
    private float personAngle;      // l'angle que fait le GameObject avec Vector3.forward


    // Choisit un modèle au hasard et l'applique au GameObject
    private void Start() {
        personModel = Random.Range(1, 9);
        personBody = GetComponent<Rigidbody>();

        switch (personModel) {
            case 1: Instantiate(model1, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 2: Instantiate(model2, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 3: Instantiate(model3, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 4: Instantiate(model4, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 5: Instantiate(model5, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 6: Instantiate(model6, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 7: Instantiate(model7, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 8: Instantiate(model8, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
        }
    }


    // Oriente le GameObject dans le sens de sa vitesse
    private void Update() {
        personAngle = Vector3.SignedAngle(Vector3.forward, personBody.velocity, Vector3.up);
        transform.rotation = Quaternion.Euler(personAngle * Vector3.up);
    }
}
