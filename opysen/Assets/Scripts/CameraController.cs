using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //config params
    //[SerializeField] private Transform target;
    public Transform farBackground; 
    public Transform middleBackground;
    public Transform mainPlayer;
    //public float minCameraHeight, maxCameraHeight;
    private Vector2 lastPosition;

    public Camera cam;

    private Transform secPlayer;

    private bool flag = true;

    private float distance = 14f;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        cam.orthographicSize = 6; // Size u want to start with
    }

    // Update is called once per frame
    void Update()
    {
        int totalPlayer = 0;
        //calculate the total number of players with the tag Player and Player2
        GameObject[] gameObjectsa = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in gameObjectsa)
        {
            if(player.activeSelf){
                totalPlayer++;
            }
        }

        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("Player2");

        foreach (GameObject player in gameObjects2)
        {
            if(player.activeSelf){
                totalPlayer++;
            }
        }

        distance = totalPlayer * 6f;

        if(flag == true){
            // Find the secPlayer by tag
            GameObject secPlayerObject = GameObject.FindGameObjectWithTag("Player2");
            
            if (secPlayerObject != null)
            {
                secPlayer = secPlayerObject.transform;
            }
            else
            {
                Debug.LogError("No object with tag 'Player2' found!");
                return;
            }

            flag = false;
        }

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in gameObjects)
        {
            distance -= Vector3.Distance(player.transform.position, mainPlayer.position)/ 2;
        }

        // Calculate the distance between mainPlayer and secPlayer
        if(secPlayer.gameObject.activeSelf)
            distance -= Vector3.Distance(mainPlayer.position, secPlayer.position)/ 2;

        if(distance < 3f){
            distance = 3f;
        }

        // Calculate the orthographic size based on the distance
        cam.orthographicSize = distance / 2 + 2; // 2 is the extra distance you want to keep between the players and the edge of the screen

        // Adjust the camera's x-position to follow the mainPlayer
        transform.position = new Vector3(mainPlayer.position.x, mainPlayer.position.y + 2, transform.position.z);

        Vector2 difference = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);

        farBackground.position += new Vector3(difference.x, difference.y, 0);
        middleBackground.position += new Vector3(difference.x, difference.y, 0) * 0.5f;

        // Update the lastPosition
        lastPosition = transform.position;
    }
}
