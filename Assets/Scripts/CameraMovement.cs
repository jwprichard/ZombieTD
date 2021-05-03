using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    float speed = 0.2f;
    static Vector3 position;
    static Camera Camera;

    // Start is called before the first frame update
    void Start()
    {

        Camera = Camera.main;
        
    }

    public static void UpdatePosition()
    {
        //Vector3 pos = new Vector3(MapGenerator.GetMap().GetLength(0) / 2 * 4, MapGenerator.GetMap().GetLength(1) / 2 * 4, Camera.transform.position.z);
        Vector3 pos = new Vector3(0, 0, -10);
        position = pos;
        Camera.transform.position = position;
        Camera.fieldOfView = 140;


    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        
        //Move the camera using WASD
        if (Input.GetKey(KeyCode.W))
        {

            transform.position = new Vector3(position.x, position.y + speed, position.z);
            position = transform.position;
        }

        if (Input.GetKey(KeyCode.S))
        {

            transform.position = new Vector3(position.x, position.y - speed, position.z);
            position = transform.position;
        }

        if (Input.GetKey(KeyCode.D))
        {

            transform.position = new Vector3(position.x + speed, position.y, position.z);
            position = transform.position;
        }

        if (Input.GetKey(KeyCode.A))
        {

            transform.position = new Vector3(position.x - speed, position.y, position.z);
            position = transform.position;
        }

        //Scroll the Camera
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            if (Camera.fieldOfView > 20) { 
                Camera.fieldOfView *= 0.9f;
            }
        }
        if (Camera.fieldOfView < 150)
        {
            if (scroll < 0f)
            {
                Camera.fieldOfView *= 1.1f;
            }
        }
    }

}
