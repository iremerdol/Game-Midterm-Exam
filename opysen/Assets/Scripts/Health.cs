using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")] 
    public float startingHealth;

    private float currentHealth;
    //public GameObject player;
    
    /* [Header ("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend; */

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    //private bool invulnerable;

    /* [Header ("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound; */

    private void Awake(){
        currentHealth = startingHealth;
        //anim = GetComponent<Animator>();
        //spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage){
        currentHealth = SaveManager.instance.currentData.health;
        if(currentHealth > 10){
            //SoundManager.instance.PlaySound(hurtSound);
            //GameObject.DontDestroyOnLoad(player);
            GameManager.instance.TakeDamage();
            GameManager.instance.SaveTheData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.instance.LoadTheData();
        }
        else{              
            //deactivate all attached component classes
            /* foreach(Behaviour component in components){
                component.enabled = false;
            } */
            //anim.SetBool("grounded", true);
            //anim.SetTrigger("die");

            //dead = true;
            //SoundManager.instance.PlaySound(deathSound);
            GameManager.instance.DeleteSave();
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            //GameManager.instance.Respawn();
            AudioManager.instance.Playa(4); //game over sound
        }  
    }
    public void AddHealth(float value){
        GameManager.instance.AddHealth();
        GameManager.instance.SaveTheData();
    }
    public int GetCurrentHealth(){
        return GameManager.instance.GetCurrentHealth();
    }
    public void Respawn(){
        //dead = false;
        AddHealth(startingHealth);
        //anim.ResetTrigger("die");
        //anim.Play("Idle");

        //activate all attached component classes
        foreach(Behaviour component in components){
            component.enabled = true;
        }

        SceneManager.LoadScene(2);
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
