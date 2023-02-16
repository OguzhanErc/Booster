using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem rocketMainParticle;
    [SerializeField] ParticleSystem rocketParticle1;
    [SerializeField] ParticleSystem rocketParticle2;
    [SerializeField] ParticleSystem rocketParticle3;


    Rigidbody rb;
    AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * speed);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        PlayParticles();
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        StopParticles();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        rb.transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }

    void PlayParticles()
    {
        if (!rocketMainParticle.isPlaying)
        {
            rocketMainParticle.Play();
        }
        if (!rocketParticle1.isPlaying)
        {
            rocketMainParticle.Play();
            Debug.Log("Particle 0 working");

        }
        if (!rocketParticle2.isPlaying)
        {
            rocketMainParticle.Play();
            Debug.Log("Particle 1 working");
        }
        if (!rocketParticle3.isPlaying)
        {
            rocketMainParticle.Play();
            Debug.Log("Particle 2 working");
        }

    }
    void StopParticles()
    {
        rocketMainParticle.Stop();
        rocketParticle1.Stop();
        rocketParticle2.Stop();
        rocketParticle3.Stop();
    }
}
