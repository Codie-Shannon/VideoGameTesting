using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables needed for movement
    private Rigidbody rb;
    public float speed;

    // Audio sound when items are collected
    private AudioSource collectableSound;

    
    // Start is called before the first frame update
    void Start()
    {
        
        // assign value to rb
        rb = GetComponent<Rigidbody>();

        // Audio
        collectableSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("pickup"))
        {
            // de-activate pickup game object
            other.gameObject.SetActive(false);

            // Play collectable sound
            collectableSound.Play();

        }
    }

   

}
