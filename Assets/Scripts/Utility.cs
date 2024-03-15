using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    [SerializeField] LineManager lineManager;
    [SerializeField] Color color;

    private void OnMouseDown()
    {
        // Start drawing when sprite is clicked
        lineManager.color = color;
        lineManager.utilPos = transform.position;
        lineManager.startDrawLine = true;
    }
}
