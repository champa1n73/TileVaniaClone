using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 50;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            GetComponent<AudioSource>().PlayOneShot(coinPickupSFX);
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
    }
}
