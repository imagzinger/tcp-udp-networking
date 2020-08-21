using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerManager player;
    public float sensitivity = 11f;
    public float clampAngle = 85f;
    private float mouseHorizontalZero = Input.GetAxis("Mouse X");
    private float verticalRotation;
    private float horizontalRotation;

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
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    private void Look()
    {
        //float _mouseVertical = -Input.GetAxis("Mouse Y");
        //float _mouseHorizontalZero = 0;
        float joyStick = Input.GetAxis("Mouse X");
        if(joyStick > mouseHorizontalZero)
            horizontalRotation += .5f;
        if (joyStick < mouseHorizontalZero)
            horizontalRotation -= .5f;

        //verticalRotation += _mouseVertical * sensitivity * Time.deltaTime;
        //horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime;

        //if (Input.GetKey(KeyCode.A))
        //{
        //	horizontalRotation -= 1f;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //	horizontalRotation += 1f;
        //}

        //verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        //transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);//rotate vertical this way to only move camera
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);//this will rotate the entire player
	}

    private void ToggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;

        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
