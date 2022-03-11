using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisHand1 : MonoBehaviour
{

    public LineRenderer telekinesisLine;
    public float lineWidth = 0.1f;
    public float lineMaxLength = 1f;

    public bool toggled = false;

    private float hand1 = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);

    public bool somethingHit = false;

    private Selectable lastSelected = null;

    private GameObject selection;



    // Start is called before the first frame update
    void Start()
    {
        Vector3[] startLinePositions = new Vector3[2] {Vector3.zero, Vector3.zero};
        telekinesisLine.SetPositions(startLinePositions);
        telekinesisLine.enabled = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        hand1 = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        if (hand1 > 0.9)
        {
            toggled = true;
            telekinesisLine.enabled = true;
        }
        else
        {
            toggled = false;
            telekinesisLine.enabled = false;
            somethingHit = false;
        }

        if (toggled)
        {
            Telekinesis(transform.position, transform.forward, lineMaxLength);
        }
    }

    private void Telekinesis(Vector3 targetPosition, Vector3 direction, float length)
    {
        RaycastHit hit;
        Ray telekinesisOut = new Ray(targetPosition, direction);

        Vector3 endPosition = targetPosition + (length * direction);

        if(Physics.Raycast(telekinesisOut, out hit))
        {
            endPosition = hit.point;
            selection = hit.collider.gameObject;
            if (selection.GetComponent<Selectable>() != null)
            {
                if (lastSelected != null)
                {
                    lastSelected.DisableHighlight1();
                }
                somethingHit = true;
                lastSelected = selection.GetComponent<Selectable>();
                lastSelected.EnableHighlight1();

            }
            else
            {
                if (lastSelected != null)
                {
                    lastSelected.DisableHighlight1();
                }
                somethingHit = false;
            }
        }
        else
        {
            if (lastSelected != null)
                {
                    lastSelected.DisableHighlight1();
                }
            somethingHit = false;
        }

        telekinesisLine.SetPosition(0, targetPosition);
        telekinesisLine.SetPosition(0, endPosition);

    }
}
