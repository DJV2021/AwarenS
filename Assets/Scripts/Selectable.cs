using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    [SerializeField]
    private GlowHighlight highlight;

    private void Awake()
    {
        highlight = GetComponent<GlowHighlight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHighlight1()
    {           
        highlight.ToggleGlow1(true);
    }

    public void DisableHighlight1()
    {
        highlight.ToggleGlow1(false);
    }
}
