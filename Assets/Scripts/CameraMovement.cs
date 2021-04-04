using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    float speed = 1f;
    static Vector3 position;
    static Camera camera;

    // Start is called before the first frame update
    void Start()
    {

        camera = Camera.main;
        
    }

    public static void UpdatePosition()
    {
        Vector3 pos = new Vector3(MapGenerator.mapWidth / 2 * 4, MapGenerator.mapHeight / 2 * 4, camera.transform.position.z);
        position = pos;
        Debug.Log(pos);
        camera.transform.position = position;
        camera.fieldOfView = 140;


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
            if (camera.fieldOfView > 20) { 
                camera.fieldOfView *= 0.9f;
            }
        }
        if (camera.fieldOfView < 150)
        {
            if (scroll < 0f)
            {
                camera.fieldOfView *= 1.1f;
            }
        }
    }

}
