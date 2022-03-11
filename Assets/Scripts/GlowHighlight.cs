using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowHighlight : MonoBehaviour
{
    Dictionary<Renderer,Material[]> glowMaterial1Dictionary = new Dictionary<Renderer,Material[]>();
    
    Dictionary<Renderer,Material[]> originalMaterialDictionary = new Dictionary<Renderer,Material[]>();

    Dictionary<Color,Material> cachedGlowMaterial1 = new Dictionary<Color,Material>();

    public Material glowMaterial1;

    private bool isGlowing1 = false;

    private void Awake()
    {
        PrepareMaterialDictionaries();

    }

    private void PrepareMaterialDictionaries()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] originalMaterials = renderer.materials;
            originalMaterialDictionary.Add(renderer, originalMaterials);
            Material[] newMaterials1 = new Material[renderer.materials.Length];

            for (int i = 0; i <originalMaterials.Length; i++)
            {
                Material mat1 = null;
                if (cachedGlowMaterial1.TryGetValue(originalMaterials[i].color, out mat1) == false)
                {
                    mat1 = new Material(glowMaterial1);
                    mat1.color = originalMaterials[i].color;
                }
                newMaterials1[i] = mat1;
            }
            glowMaterial1Dictionary.Add(renderer, newMaterials1);
        }
    }

    public void ToggleGlow1()
    {
        if (isGlowing1 == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = glowMaterial1Dictionary[renderer];
            }
        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = originalMaterialDictionary[renderer];
            }
        }
        isGlowing1 = !isGlowing1;
    }

    public void ToggleGlow1(bool state)
    {
        if (isGlowing1 == state)
        {
            return;
        }
        isGlowing1 = !state;
        ToggleGlow1();
    }
}
