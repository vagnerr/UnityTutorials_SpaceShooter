using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if ( gameControllerObject != null )
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if ( gameController == null )
        {
            Debug.Log("Cannot file 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Boundary") || other.CompareTag("Enemy") )
        {
            return;
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);

        if( explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation); // as GameObject;
        }

        if (other.CompareTag("Player") )
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation); // as GameObject;
            gameController.GameOver();
        }
    }
}
