using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController HardController;

    void Start()
    {
        GameObject HardControllerObject = GameObject.FindWithTag("GameController");
        if (HardControllerObject != null)
        {
            HardController = HardControllerObject.GetComponent<GameController>();
        }
        if (HardController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            HardController.GameOver();
        }

        HardController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
