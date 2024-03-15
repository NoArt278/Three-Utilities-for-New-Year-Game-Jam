using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    public int houseNum, utilNum;
    public int connectedHouseCount;
    [SerializeField] LineManager lineManager;
    [SerializeField] GameObject houses, utils, HUD, levelFinish;
    [SerializeField] Material skyboxMat;
    CameraMovement cam;
    private int currSceneIdx;

    private void Start()
    {
        RenderSettings.skybox = skyboxMat;
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cam = GetComponent<CameraMovement>();
        connectedHouseCount = 0;
        houseNum = houses.transform.childCount;
        utilNum = utils.transform.childCount;
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (connectedHouseCount == houseNum)
        {
            if (!levelFinish.activeSelf)
            {
                audioSource.Play();
            }
            HUD.SetActive(false);
            levelFinish.SetActive(true);
        }
    }

    public void resetLine()
    {
        lineManager.resetLines();
        foreach (Transform child in houses.transform)
        {
            House h = child.gameObject.GetComponent<House>();
            h.connectedColors.Clear();
            h.connectedUtilCount = 0;
        }
        connectedHouseCount = 0;
    }

    public void resetLevel()
    {
        resetLine();
        cam.resetPosition();
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(currSceneIdx + 1);
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}
