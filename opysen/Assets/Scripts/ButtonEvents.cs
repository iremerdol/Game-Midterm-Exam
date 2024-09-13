using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

public class ButtonEvents : MonoBehaviour
{
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Quit(){
        Application.Quit();//quits the game (only works on build)
        #if UNITY_EDITOR        
        UnityEditor.EditorApplication.isPlaying = false;//exit game mode (this code will only be executed in editor)
        #endif
    }

    public void NewGame()
    {
        //GameManager.instance.DeleteSave();
        //GameManager.instance.LoadTheData();
        // Get count of loaded Scenes and last index
        var lastSceneIndex = SceneManager.sceneCount - 1;
        
        // Get last Scene by index in all loaded Scenes
        var lastLoadedScene = SceneManager.GetSceneAt(lastSceneIndex);
        
        // Unload Scene
        SceneManager.UnloadSceneAsync(lastLoadedScene);
        SceneManager.LoadScene(0);

        SaveManager.instance.currentData.health = 30;
        //SceneManager.LoadSceneAsync(2);
        Time.timeScale = 1;
        SaveManager.instance.currentData.currentLevelIndex = 0;
        SaveManager.instance.Save();
    }
    public void Back(){
        SceneManager.LoadScene(6);
        Time.timeScale = 1;
    }
    public void Options(){
        SceneManager.LoadScene(7);
        Time.timeScale = 1;
    }
    public void Continue(){
        string destination = Application.persistentDataPath + "/" + "saveData" + ".save";
        if (File.Exists(destination))
        {
            // Get count of loaded Scenes and last index
            var lastSceneIndex = SceneManager.sceneCount - 1;
            
            // Get last Scene by index in all loaded Scenes
            var lastLoadedScene = SceneManager.GetSceneAt(lastSceneIndex);
            
            // Unload Scene
            SceneManager.UnloadSceneAsync(lastLoadedScene);

            GameManager.instance.LoadTheData();
            SceneManager.LoadSceneAsync(GameManager.instance.GetCurrentLevel());
            Time.timeScale = 1;
        }     
    }
    public void SoundVolume(){
        AudioManager.instance.ChangeSoundVolume(0.1f);
    }
    public void MusicVolume(){
        AudioManager.instance.ChangeMusicVolume(0.1f);
    }
}
