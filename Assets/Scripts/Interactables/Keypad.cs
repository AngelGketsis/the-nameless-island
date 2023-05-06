using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    MeshRenderer mesh;

    Color cRed = Color.red;
    Color cYellow = Color.yellow;
    Color cCyan = Color.cyan;

    public Color[] colors ;
    private int colourIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        colors = new Color[] {cRed, cYellow, cCyan};
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.green;
    }


    // this function is where we will design our interaction using code
    protected override void Interact()
    {
        Debug.Log("Interacted");
        /*
        Debug.Log("Interacted with " + gameObject.name);
        colourIndex++;
        Debug.Log("Size of colors : "  + colors.Length);
        Debug.Log("Colour index : " + colourIndex);
        if(colourIndex > colors.Length - 1)
        {
            colourIndex = 0;
        }

        mesh.material.color = colors[colourIndex];
    */
    }
}
