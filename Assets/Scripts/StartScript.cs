using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    [SerializeField] GameObject gameManager, lineManager, HUD;
    public void startGame()
    {
        gameManager.SetActive(true);
        lineManager.SetActive(true);
        HUD.SetActive(true);
        gameObject.SetActive(false);
    }
}
