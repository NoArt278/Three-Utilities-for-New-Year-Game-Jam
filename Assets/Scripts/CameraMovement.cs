using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    private float moveSpeed = 0.2f;
    private const float scrollSpeed = 10f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    [SerializeField] Transform center;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        initialPosition = cam.transform.position;
        initialRotation = cam.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position += cam.transform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        float currRotation = cam.transform.rotation.eulerAngles.x < 180 ? cam.transform.rotation.eulerAngles.x : 360 - cam.transform.rotation.eulerAngles.x;
        if (currRotation < 80)
        {
            cam.transform.RotateAround(center.position, cam.transform.right, Input.GetAxis("Vertical") * moveSpeed);
        }
        cam.transform.RotateAround(center.position, cam.transform.up, Input.GetAxis("Horizontal") * moveSpeed);
        cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y,
                                    0f);
    }

    public void resetPosition()
    {
        cam.transform.position = initialPosition;
        cam.transform.rotation = initialRotation;
    }
    public void changeSpeed(float val)
    {
        moveSpeed = val;
    }
}
