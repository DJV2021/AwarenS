using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchFinishCollider : MonoBehaviour
{
    public March march;

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Agent") {
            march.Kill(other.gameObject);
        }
    }
}
