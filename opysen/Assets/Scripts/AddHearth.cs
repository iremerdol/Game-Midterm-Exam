using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddHearth : MonoBehaviour
{
    public Transform mainPlayer;
    public float health;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.x == mainPlayer.position.x)
        {
            AudioManager.instance.Playa(3); //hurt sound
            other.GetComponent<Health>().AddHealth(10);
            Destroy(gameObject);
        }
    }
}
