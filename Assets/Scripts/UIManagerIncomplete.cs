using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManagerIncomplete : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
