using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Transform mainPlayer;
    public float damage;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.x == mainPlayer.position.x)
        {
            AudioManager.instance.Playa(2); //hurt sound
            other.GetComponent<Health>().TakeDamage(10);
        }
        else if(other.tag == "Player2" || other.tag == "Player"){
            //other.GetComponent<Collider2D>().enabled = false;
            GameObject.Find(other.gameObject.name).SetActive(false);
            AudioManager.instance.Playa(2); //hurt sound
        }
    }
}
