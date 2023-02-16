using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS -  for tuningi typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables


    [SerializeField] float crashDelayTime = 2f;
    [SerializeField] float completeDelayTime = 4f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip levelSuccesfulAudio;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem succesParticles;
   
    AudioSource audioSource;
    
    bool isTransitioning = false;
    bool collisionDisabled=false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
    }
    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // Toggle Collision
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }
       
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;

            case "Finish":
                StartFinishSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }
    void StartFinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelSuccesfulAudio);
        succesParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), completeDelayTime);        
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();       
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), crashDelayTime);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
