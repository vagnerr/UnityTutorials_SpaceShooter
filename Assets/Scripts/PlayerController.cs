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

    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButton touchButton;


    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    private Quaternion calibrationQuaternion;
    public Boundary boundary;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auds = GetComponent<AudioSource>();
        CalibrateAccelerometer();
    }

    void FixedUpdate()
    {
        // Keyboard input
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Mobile input ( accelerometer )
        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);
        //Vector3 movement = new Vector3(acceleration.x,0.0f,acceleration.y);

        // Touchpad Input
        Vector2 direction = touchPad.GetDirction();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        rb.velocity = movement * speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(rb.velocity.z * tiltx , 0.0f, rb.velocity.x * -tiltz);
    }

    //Used to calibrate the Iput.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    void Update()
    {
        //if (Input.GetButton("Fire1") && Time.time > nextFire)
        if (touchButton.CanFire() && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot, shotSpawn.position, Quaternion.Euler( 0.0f, 0.0f, shotSpawn.rotation.z )); // as GameObject;
            auds.Play();

        }
    }
}
