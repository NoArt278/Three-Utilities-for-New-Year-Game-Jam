using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [HideInInspector] public Color color;
    [HideInInspector] public Vector3 utilPos;
    [HideInInspector] public bool startDrawLine, reachedHouse, crossOver = false;
    [SerializeField] GameObject linePrefab;
    [SerializeField] GameManager gameManager;
    private GameObject lineRenderer;
    [HideInInspector] public House selectedHouse;
    private LineRenderer line;
    private List<GameObject> connectedLines;

    private void Awake()
    {
        connectedLines = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Util"))
                {
                    color = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color;
                    reachedHouse = false;
                    crossOver = false;
                    StartCoroutine(drawLine());
                }
            }
        } 
    }

    private IEnumerator drawLine()
    {
        lineRenderer = Instantiate(linePrefab, transform);
        lineRenderer.transform.position = utilPos;
        line = lineRenderer.GetComponent<LineRenderer>();
        line.positionCount = 0;
        Vector3 prevPoint = Vector3.zero;

        while (true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                yield return new WaitForSeconds(0.6f);
            }
            // Detect the position on the cube where the mouse clicks
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Get the hit position in local space
                Vector3 pos = hit.point - utilPos;
                float distFromPrev = Vector3.Distance(pos, prevPoint);
                if (hit.collider != null && ((distFromPrev <= 0.5f && distFromPrev >= 0.1f) || prevPoint == Vector3.zero))
                {
                    if (hit.collider.gameObject.CompareTag("Line") || (hit.collider.gameObject.CompareTag("Util") && prevPoint != Vector3.zero))
                    {
                        crossOver = true;
                    } else if (hit.collider.gameObject.CompareTag("House"))
                    {
                        if (prevPoint != Vector3.zero)
                        {
                            selectedHouse = hit.collider.gameObject.GetComponent<House>();
                            // Check that the house isn't connected to the utility yet
                            if (!selectedHouse.connectedColors.Contains(color) && !crossOver)
                            {
                                reachedHouse = true;
                            }
                        }
                    } else if (!hit.collider.gameObject.CompareTag("Util"))
                    {
                        // Draw the line based on the above position
                        // Move the line to the front a little bit to make sure it doesn't go into the box mesh
                        pos -= Camera.main.transform.forward * 0.2f;
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, pos);
                        prevPoint = pos;
                        reachedHouse = false;
                    }
                }
            }

            if (crossOver)
            {
                line.material.color = Color.red;
            }
            else if (reachedHouse)
            {
                line.material.color = Color.green;
            } else
            {
                line.material.color = color;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (reachedHouse)
                {
                    // Simplify the line
                    line.Simplify(0.2f);
                    // Add capsule colliders
                    addCapsuleCollider();
                    line.material.color = color;
                    selectedHouse.connectedUtilCount++;
                    selectedHouse.connectedColors.Add(color);
                    selectedHouse.connectedLines.Add(lineRenderer);
                    connectedLines.Add(lineRenderer);

                    if (selectedHouse.connectedUtilCount == gameManager.utilNum)
                    {
                        gameManager.connectedHouseCount++;
                    }
                } else
                {
                    // Delete line
                    Destroy(lineRenderer);
                }
                break;
            }
            yield return null;
        }
    }

    public void resetLines()
    {
        foreach(var line in connectedLines)
        {
            Destroy(line);
        }
        connectedLines.Clear();
    }

    private void addCapsuleCollider()
    {
        Vector3[] allPos = new Vector3[line.positionCount];
        line.GetPositions(allPos);
        for (int i=1; i<allPos.Length; i++)
        {
            // Create new collision game object
            GameObject collision = new GameObject();
            collision.tag = "Line";
            collision.transform.parent = lineRenderer.transform;
            collision.transform.localPosition = (allPos[i-1] + allPos[i])/2;
            // Add capsule collider and position it to the line
            CapsuleCollider cp = collision.AddComponent<CapsuleCollider>();
            cp.radius = line.startWidth;
            cp.height = Vector3.Distance(allPos[i-1], allPos[i]);
            cp.transform.up = (allPos[i] - allPos[i - 1]).normalized;
        }
    }
}
