using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private bool lockLook = false;

    public void ProcessLook(Vector2 input)
    {
        if(!lockLook)
        {
            //Cursor.lockState = CursorLockMode.Confined; // cursor won't leave screen
            float mouseX = input.x;
            float mouseY = input.y;
            //calculate camera rotation for looking up and down
            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            //apply this to our camera transform
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            //rotate player to look left and right
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
        else
        {
            Debug.Log(lockLook);
            Debug.Log("In Else Loop");
        }
    }

    public void LockEyes()
    {
        lockLook = true;
        Debug.Log("LockEyes");
    }

    public void UnlockEyes()
    {
        lockLook = false;
        Debug.Log("UnlockEyes");
    }


}
