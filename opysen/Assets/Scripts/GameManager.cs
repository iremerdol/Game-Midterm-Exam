using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //private Checkpoints[] checkpoints;
    public Vector3 lastCheckPointPos;

    public static bool flag = true;
    private void Awake()
    {
        if(instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            instance = this;
        } else if(instance != this) // If there is already an instance and it's not this instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }

        if(flag){
            SceneManager.LoadSceneAsync(6, LoadSceneMode.Additive);
            flag = false;
            LoadTheData();
        }   
    }
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //checkpoints = FindObjectsOfType<Checkpoints>();
    }

    public void SaveTheData()
    {
        UpdateSaveSystem();
        SaveManager.instance.Save();
    }
    public void LoadTheData()
    {
        SaveManager.instance.Load();
        SceneManager.LoadScene(SaveManager.instance.currentData.currentLevelIndex);
    }

    void UpdateSaveSystem()
    {
        SaveManager.instance.currentData.currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void TakeDamage(){
        SaveManager.instance.currentData.health -= 10;
        SaveTheData();
    }

    public void AddHealth(){
        SaveManager.instance.currentData.health += 10;
        SaveTheData();
    }

    public void DeleteSave(){
        SaveManager.instance.Delete();
    }

    public int GetCurrentLevel(){
        return SaveManager.instance.currentData.currentLevelIndex;
    }

    public int GetCurrentHealth(){
        return SaveManager.instance.currentData.health;
    }

    public void Respawn()
    {
        SaveManager.instance.currentData.health = 30;
    }

}
