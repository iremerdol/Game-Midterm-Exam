using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinKeyboard2 : MonoBehaviour
{
    public GameObject player2;
    public bool isPlayer2Joined = false;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player2, transform.position, transform.rotation);//Quaternion.identity rotation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
