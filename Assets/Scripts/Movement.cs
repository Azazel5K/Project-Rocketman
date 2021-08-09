using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSrc;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip engineSFX;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThursterParticles;
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
               audioSrc.PlayOneShot(engineSFX);
           } 
           if (!mainEngineParticles.isPlaying)
           {
                mainEngineParticles.Play();
           } 
       }
       else
       {
           audioSrc.Stop();
           mainEngineParticles.Stop();
       }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightThursterParticles.isPlaying)
           {
                rightThursterParticles.Play();
           } 
        }
        else if (Input.GetKey(KeyCode.D))
       {
           ApplyRotation(-rotationThrust);
           if (!leftThrusterParticles.isPlaying)
           {
                leftThrusterParticles.Play();
           } 
       }
       else
       {
           rightThursterParticles.Stop();
           leftThrusterParticles.Stop();
       }
    }

     void ApplyRotation(float rotateFrameBy)
    {
        rb.freezeRotation = true; // freeze to allow manual rotation
        transform.Rotate(Vector3.forward * rotateFrameBy * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze to allow system takeover
    }
}
