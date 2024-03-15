using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9Tween : MonoBehaviour
{
    [SerializeField] RectTransform hint;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartAnimation", 15f);
    }

    void StartAnimation()
    {
        LeanTween.move(hint, new Vector3(-5.3f, -78, 0), 1f).setEaseOutBack();
    }
}
