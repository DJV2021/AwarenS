using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Classe gérant le Trigger finish de March pour détruire les agents qui en sortent.
//
//
// A un attribut:
// - march, March:
// Le script March dont celui-ci est le finish (cf March).


public class MarchFinishCollider : MonoBehaviour {
    public March march;

    // Quand un agent quitte le Trigger, essaie de le détruire
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Agent") {
            march.Kill(other.gameObject);
        }
    }
}
