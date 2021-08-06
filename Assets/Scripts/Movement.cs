using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSrc;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
       {
           rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
           if (!audioSrc.isPlaying)
           {
               audioSrc.PlayOneShot(mainEngine);
           } 
       }
       else
       {
           audioSrc.Stop();
       }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
       {
           ApplyRotation(-rotationThrust);
       }
    }

     void ApplyRotation(float rotateFrameBy)
    {
        rb.freezeRotation = true; // freeze to allow manual rotation
        transform.Rotate(Vector3.forward * rotateFrameBy * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze to allow system takeover
    }
}
