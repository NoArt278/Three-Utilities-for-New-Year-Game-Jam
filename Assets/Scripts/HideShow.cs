using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShow : MonoBehaviour
{
    [SerializeField] List<GameObject> listShow;
    [SerializeField] List<GameObject> listHide;
    public float waitSec;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in listShow)
        {
            go.SetActive(false);
        }
        Invoke("ShowObjects", waitSec);
    }

    private void ShowObjects()
    {
        foreach (GameObject go in listShow)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in listHide)
        {
            go.SetActive(false);
        }
    }
}
