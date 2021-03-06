using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Classe gérant l'attribution de modèle et l'orientation du prefab Person.
//
// 
// A plusieurs attributs:
//
// - model, GameObject (prefab):
// Les 8 modèles possible que peut prendre le prefab.


public class Person : MonoBehaviour {
    public Modele model1;
    public Modele model2;
    public Modele model3;
    public Modele model4;
    public Modele model5;
    public Modele model6;
    public Modele model7;
    public Modele model8;

    private int personModel;        // le modèle pris par le GameObject
    private Rigidbody personBody;   // le RigidBody du Gameobject
    private float personAngle;      // l'angle que fait le GameObject avec Vector3.forward

    public bool isVictim;
    private Modele model;
    public bool isWoman;
    public float percentageOfInterestPerson = SubjectButton.percentageOfInterest;

    public float tmp_random;

    // Choisit un modèle au hasard et l'applique au GameObject
    private void Start() {
        personModel = Random.Range(1, 9);
        personBody = GetComponent<Rigidbody>();
        switch (personModel)
        {
            case 1: model = Instantiate(model1, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 2: model = Instantiate(model2, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 3: model = Instantiate(model3, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 4: model = Instantiate(model4, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 5: model = Instantiate(model5, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 6: model = Instantiate(model6, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 7: model = Instantiate(model7, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
            case 8: model = Instantiate(model8, transform.position, Quaternion.Euler(Vector3.zero), transform); break;
        }
        isWoman = model.IsModeleAWoman;
        if (isWoman)
        {
            tmp_random = Random.value;
            if (tmp_random < percentageOfInterestPerson)
            {
                SetYellow();

                isVictim = true;
            }
        }
    }


    // Oriente le GameObject dans le sens de sa vitesse
    private void Update() {
        personAngle = Vector3.SignedAngle(Vector3.forward, personBody.velocity, Vector3.up);
        transform.rotation = Quaternion.Euler(personAngle * Vector3.up);

        if (percentageOfInterestPerson != SubjectButton.percentageOfInterest)
        {
            if (isWoman)
            {
                //tmp_random = Random.value; pas besoin je crois 
                if (tmp_random < SubjectButton.percentageOfInterest)
                {
                    SetYellow();
                    isVictim = true;
                }
                else
                {
                    StopOutline();
                }
            }
            percentageOfInterestPerson = SubjectButton.percentageOfInterest;
        }
    }

    public void SetYellow()
    {
        model.SetModeleYellow();
    }

    public void SetRed()
    {
        model.SetModeleRed();
    }

    public void StopOutline()
    {
        model.StopOutlineModele();
    }

}
