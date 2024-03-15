using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider speedSlider;
    [SerializeField] Text speedText;
    [SerializeField] CameraMovement camMove;
    [SerializeField] Toggle front, left, back, right;

    // Start is called before the first frame update
    void Start()
    {
        camMove.changeSpeed(speedSlider.value);
        speedText.text = "Cam Move Speed : " + System.Math.Round(speedSlider.value, 2).ToString();
        speedSlider.onValueChanged.AddListener(delegate
        {
            camMove.changeSpeed(speedSlider.value);
            speedText.text = "Cam Move Speed : " + System.Math.Round(speedSlider.value, 2).ToString();
        });
    }

    // Update is called once per frame
    void Update()
    {
        // Add keyboard shortcuts
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            front.isOn = !front.isOn;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            back.isOn = !back.isOn;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            left.isOn = !left.isOn;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            right.isOn = !right.isOn;
        }

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
