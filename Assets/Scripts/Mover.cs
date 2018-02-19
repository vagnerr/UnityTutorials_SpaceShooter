using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Rigidbody rb;

    private GameController gameController;


    public float speed;
    public float multiplyer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot file 'GameController' script");

            rb.velocity = transform.forward * (speed);  // no gamecontroller no waves ( eg testing with GC off )
        }
        else
        {
            rb.velocity = transform.forward * (speed - (gameController.Wave() * multiplyer));  // subtract the multiplier as we are traveling in -ve direction
        }
    }
}
