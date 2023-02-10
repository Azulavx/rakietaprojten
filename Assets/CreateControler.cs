using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateControler : MonoBehaviour
{
    Rigidbody rb;


    Vector2 input;

    public float enginePower = 10;
    public float gyroPower = 2;
    private camera cs;

    public GameObject bulletPrefab;
    Transform GunLeft, GunRight;

    public float bulletSpeed = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cs = Camera.main.transform.GetComponent<camera>();
        input = Vector2.zero;
        GunLeft = transform.Find("GunLeft").transform;
        GunRight = transform.Find("GunRight").transform;


    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        input.x = x;
        input.y = y;

        if (Mathf.Abs(transform.position.x) > cs.gameWidth / 2)
        {
            Vector3 newPosicition = new Vector3(transform.position.x * (-1 + float.Epsilon),
                                                 0,
                                                 transform.position.z);
            transform.position = newPosicition;
        }
        if (Mathf.Abs(transform.position.z) > cs.gameHeight / 2)
        {
            Vector3 newPosicition = new Vector3(transform.position.x * (-1 + float.Epsilon),
                                                 0,
                                                 transform.position.z);
            transform.position = newPosicition;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();

        void FixedUpdate()
        {
            rb.AddForce(transform.forward * input.y * enginePower, ForceMode.Acceleration);
            rb.AddTorque(transform.up * input.x * gyroPower, ForceMode.Acceleration);
        }
    }
    void Fire()
    {
        GameObject leftBullet = Instantiate(bulletPrefab, GunLeft.position, Quaternion.identity);

        leftBullet.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.VelocityChange);
        Destroy(leftBullet, 5);
        
        GameObject rightBullet = Instantiate(bulletPrefab, GunRight.position, Quaternion.identity);
        rightBullet.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.VelocityChange);
        Destroy(rightBullet, 5);



    }
}
