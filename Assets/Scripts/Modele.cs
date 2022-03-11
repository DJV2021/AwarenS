using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modele : MonoBehaviour
{
    [SerializeField]
    public Outline hair;
    [SerializeField]
    public Outline pants;
    [SerializeField]
    public Outline sleeves;
    public bool IsModeleAWoman;

    // Start is called before the first frame update
    void Awake()
    {
        hair.eraseRenderer = true;
        pants.eraseRenderer = true;
        sleeves.eraseRenderer = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetModeleRed()
    {
        hair.eraseRenderer = false;
        hair.color = 0;
        pants.eraseRenderer = false;
        pants.color = 0;
        sleeves.eraseRenderer = false;
        sleeves.color = 0;
    }

    public void SetModeleYellow()
    {
        hair.eraseRenderer = false;
        hair.color = 1;
        pants.eraseRenderer = false;
        pants.color = 1;
        sleeves.eraseRenderer = false;
        sleeves.color = 1;
    }

    public void StopOutlineModele()
    {
        hair.eraseRenderer = true;
        pants.eraseRenderer = true;
        sleeves.eraseRenderer = true;
    }
}
