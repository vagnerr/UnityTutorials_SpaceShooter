using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

}
public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private AudioSource auds;
    public float speed;
    public float tiltx;
    public float tiltz;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    public Boundary boundary;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auds = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(rb.velocity.z * tiltx , 0.0f, rb.velocity.x * -tiltz);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot, shotSpawn.position, Quaternion.Euler( 0.0f, 0.0f, shotSpawn.rotation.z )); // as GameObject;
            auds.Play();

        }
    }
}
