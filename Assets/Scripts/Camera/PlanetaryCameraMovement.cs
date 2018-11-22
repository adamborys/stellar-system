using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetaryCameraMovement : MonoBehaviour
{
    public static bool IsLocked = false;
    public static float Magnitude;
    private GameObject shipyard;
    private GameObject detailedGrid, grid;
    private Toggle camToggle;

    void Start()
    {
        if (StellarSystem.IsStarted)
        {
            transform.position = SystemCreator.EditorCameraPosition;
            transform.rotation = SystemCreator.EditorCameraRotation;
        }

        detailedGrid = GameObject.Find("DetailedGrid");
        grid = GameObject.Find("Grid");
        shipyard = GameObject.Find("Shipyard");

        camToggle = GameObject.Find("Cam Toggle").GetComponent<Toggle>();
        camToggle.onValueChanged.AddListener(delegate { toggleChange(); });

        detailedGrid.SetActive(false);
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (!IsLocked)
        {
            float angleX;
            // Spherical camera with limited rotation
            if (Input.GetMouseButton(2))
            {
                GameObject dummyCamera = Instantiate(transform.gameObject);
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
                // Zoom with scrollwheel
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                Magnitude = Vector3.Magnitude(transform.localPosition);
                if (scroll > 0)
                {
                    if (camToggle.isOn && Magnitude > 15f)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                    else if (PlanetarySelectionController.isPlanetSelected())
                    {
                        if(Magnitude > 200f)
                            transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                    }
                    //Minimal distance from camera equal to selection bounds
                    else if (Magnitude > PlanetarySelectionController.Selection
                                        .GetComponent<MeshCollider>().bounds.size.z)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                    
                }
                else if (scroll < 0)
                {
                    if (camToggle.isOn && Magnitude < 700f)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                    else if (PlanetarySelectionController.Selection.name == "Planet" && Magnitude < 700f)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                    else if (Magnitude < 50f)
                        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
                }

                angleX = transform.rotation.eulerAngles.x;
                // Moving free camera
                if (camToggle.isOn)
                {
                    float speed = 0.01f;
                    speed += Magnitude;
                    Vector3 freeCamForward = new Vector3(transform.forward.x, 0, transform.forward.z);
                    if (Input.mousePosition.y >= Screen.height * 0.95)
                    {
                        transform.parent.Translate(freeCamForward * Time.deltaTime * speed, Space.World);
                    }
                    else if (Input.mousePosition.y <= Screen.height * 0.05)
                    {
                        transform.parent.Translate(-freeCamForward * Time.deltaTime * speed, Space.World);
                    }
                    if (Input.mousePosition.x >= Screen.width * 0.95)
                    {
                        transform.parent.Translate(transform.right * Time.deltaTime * speed, Space.World);
                    }
                    else if (Input.mousePosition.x <= Screen.width * 0.05)
                    {
                        transform.parent.Translate(-transform.right * Time.deltaTime * speed, Space.World);
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

    private void toggleChange()
    {
        if(!camToggle.isOn)
        {
            if(PlanetarySelectionController.isPlanetSelected())
                transform.localPosition =
                    Vector3.Normalize(transform.localPosition) * 500f;
            else
                transform.localPosition =
                Vector3.Normalize(transform.localPosition) * 25f;
        }
        else
        {
            if(PlanetarySelectionController.isPlanetSelected())
                transform.localPosition =
                    Vector3.Normalize(transform.localPosition) * Mathf.Clamp(Magnitude, 200f, 700f);
            else
                transform.localPosition =
                    Vector3.Normalize(transform.localPosition) * Mathf.Clamp(Magnitude, 15f, 700f);
        }
    }
}
