using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] LineManager lineManager;
    public int connectedUtilCount;
    public List<Color> connectedColors;
    public List<GameObject> connectedLines;

    private void Awake()
    {
        connectedUtilCount = 0;
        connectedColors = new List<Color>();
        connectedLines = new List<GameObject> ();
    }
}
