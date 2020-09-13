using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerManager player;
    public GameObject horizantalWrapper;

    public float sensitivity = 11f;
    public float clampAngle = 85f;
    private float mouseHorizontalZero;
    private float verticalRotation;
    private float horizontalRotation;
    private float cameraRotationH;
    private float cameraRotationV;
    //private float m_FieldOfView;
    private void Start()
    {
        //verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player.transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorMode();
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            
            Look();
        }
        //Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        
    }

    private void Look()
    {
        float _mouseVertical = -Input.GetAxisRaw("Mouse Y");
        float _mouseHorizontal = Input.GetAxisRaw("Mouse X");
        float m_FieldOfView = Input.GetAxisRaw("Mouse ScrollWheel");
        //Camera.FieldOfView
        gameObject.GetComponent<Camera>().fieldOfView -= m_FieldOfView* sensitivity;
        cameraRotationV += _mouseVertical * sensitivity * Time.deltaTime;
        cameraRotationH += _mouseHorizontal * sensitivity * Time.deltaTime;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    horizontalRotation -= (100f * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    horizontalRotation += (100f * Time.deltaTime);
        //}
        if (Input.GetKey(KeyCode.A))
		{
			horizontalRotation -= (100f * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			horizontalRotation += (100f * Time.deltaTime);
		}

        cameraRotationV = Mathf.Clamp(cameraRotationV, -clampAngle, clampAngle);
        cameraRotationH = Mathf.Clamp(cameraRotationH, -clampAngle, clampAngle);
        
        transform.localRotation = Quaternion.Euler(cameraRotationV, 0f, 0f);      //rotate vertical this way to only move camera
		player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f); //this will rotate the entire player
        horizantalWrapper.transform.rotation = Quaternion.Euler(cameraRotationV, horizontalRotation + cameraRotationH, 0f);
    }

    private void ToggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;

        if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
