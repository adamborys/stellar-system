using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetaryCameraMovement : MonoBehaviour
{
    public static bool IsLocked = false;
    private GameObject dummyCamera;
    private GameObject shipyard;
    private GameObject detailedGrid, grid;
    private Toggle camToggle;
    private Transform camParent;

    void Start()
    {
        if (StellarSystem.IsStarted)
        {
            transform.position = SystemCreator.EditorCameraPosition;
            transform.rotation = SystemCreator.EditorCameraRotation;
        }

        detailedGrid = GameObject.Find("Detailed Grid");
        grid = GameObject.Find("Grid");
        shipyard = GameObject.Find("Shipyard");

        camToggle = GameObject.Find("CamToggle").GetComponent<Toggle>();
        camParent = GameObject.Find("Cam Parent").transform;

        detailedGrid.SetActive(false);
    }

    void Update()
    {
        shipyard.transform.RotateAround(new Vector3(), Vector3.up, Time.deltaTime);
    }

    void LateUpdate()
    {
        if (!IsLocked)
        {
            float angleX;
            // Spherical camera with limited rotation
            if (Input.GetMouseButton(2))
            {
                dummyCamera = Instantiate(transform.gameObject);
                Transform dummyTransform = dummyCamera.transform;
                dummyTransform.SetParent(transform.parent, false);
                dummyTransform.RotateAround(transform.parent.position, Vector3.up, Input.GetAxis("Mouse X") * 2f);
                dummyTransform.RotateAround(transform.parent.position, dummyTransform.right, Input.GetAxis("Mouse Y") * -2f);
                dummyTransform.rotation = Quaternion.Euler(dummyTransform.rotation.eulerAngles.x, dummyTransform.rotation.eulerAngles.y, 0f);
                
                angleX = dummyTransform.rotation.eulerAngles.x;
                if ((0 <= angleX && angleX <= 70) || (290 <= angleX && angleX < 360))
                {
                    transform.LookAt(transform.parent);
                    transform.localPosition = dummyTransform.localPosition;
                    transform.localRotation = dummyTransform.localRotation;
                }
                Destroy(dummyCamera);
            }
            else
            {
                angleX = transform.rotation.eulerAngles.x;

                // Zoom with scrollwheel
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                float distance = Vector3.Magnitude(transform.localPosition);
                if (scroll > 0)
                {
                    if(PlanetarySelectionController.Selection.name == "Planet")
                    {
                        if(distance > 200f)
                            transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                    }
                    else if(distance > PlanetarySelectionController.Selection
                    //Minimal distance from camera equal to selection bounds
                    .GetComponent<MeshCollider>().bounds.size.z)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                }
                else if (scroll < 0)
                {
                    if(PlanetarySelectionController.Selection.name == "Planet" && distance < 700f)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                    else if(distance < 50f)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                }

                // Moving free camera
                if(camToggle.isOn)
                {
                    if ( Input.mousePosition.y >= Screen.height * 0.95)
                    {
                        // Translation speed adjusted to X angle
                        float speed;
                        if(0 <= angleX && angleX <= 80) speed = Mathf.Sin((angleX * Mathf.PI)/180);
                        else
                        {
                            angleX %= 90;
                            speed = -Mathf.Cos((angleX * Mathf.PI)/180);
                        }
                        Vector3 freeCamForward = new Vector3(transform.forward.x, 0, transform.forward.z);
                        camParent.Translate(freeCamForward * Time.deltaTime * speed
                        * 1000f, Space.World);
                        Debug.Log(speed);
                    }
                    else if ( Input.mousePosition.y <= Screen.height * 0.05)
                    {
                        float speed;
                        if(0 <= angleX && angleX <= 80) speed = -Mathf.Sin((angleX * Mathf.PI)/180);
                        else
                        {
                            angleX %= 90;
                            speed = Mathf.Cos((angleX * Mathf.PI)/180);
                        }
                        Vector3 freeCamForward = new Vector3(transform.forward.x, 0, transform.forward.z);
                        camParent.Translate(freeCamForward * Time.deltaTime * speed
                        * 1000f, Space.World);
                    }
                    if ( Input.mousePosition.x >= Screen.width * 0.95)
                    {
                        camParent.Translate(transform.right * Time.deltaTime * 700f, Space.World);
                    }
                    else if ( Input.mousePosition.x <= Screen.width * 0.05)
                    {
                        camParent.Translate(-transform.right * Time.deltaTime * 700f, Space.World);
                    }
                }
            }
        }

        // Grid displayed dependent on camera altitude
        if (transform.position.y < 100 && grid.activeSelf)
        {
            detailedGrid.SetActive(true);
            grid.SetActive(false);
        }
        else if (transform.position.y > 100 && detailedGrid.activeSelf)
        {
            detailedGrid.SetActive(false);
            grid.SetActive(true);
        }
    }
}
