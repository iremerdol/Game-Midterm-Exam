using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public Transform mainPlayer;
    
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.transform.position.x == mainPlayer.position.x){
            AudioManager.instance.Playa(3); //pass sound
            SaveManager.instance.currentData.currentLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SaveManager.instance.Save();
        }        
    }
}
