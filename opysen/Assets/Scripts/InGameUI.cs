using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject pauseScreen;

    private void Awake(){
        pauseScreen.SetActive(false);
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){ 
            //if pause screem already active unpause and viceversa
            if(pauseScreen.activeInHierarchy){
                PauseGame(false);
            }
            else{
                PauseGame(true);
            }
        }
    }
    public void Resume(){
        PauseGame(false); 
    }
    public void PauseGame(bool status){
        //if true pause, if false unpause
        pauseScreen.SetActive(status);

        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }
}
