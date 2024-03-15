using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Tween : MonoBehaviour
{
    [SerializeField] RectTransform hint, slider, toggle, buttons;

    private void Start()
    {
        Invoke("StartAnimation", 10f);
    }
    void StartAnimation()
    {
        LeanTween.move(hint, new Vector3(0f, -77, 0), 1f).setEaseOutBack();
        LeanTween.move(slider, new Vector3(112.7f, 27.4f, 0), 1f).setEaseOutBack();
        LeanTween.move(toggle, new Vector3(592f, 271.8f, 0), 1f).setEaseOutBack();
        LeanTween.move(buttons, new Vector3(-571f, 254, 0), 1f).setEaseOutBack();
    }
}
