using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip landed;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem landedParticles;
    [SerializeField] float delay = 1f;

    bool isTransitioning = false;
    bool collisionDisabled = false;

void Start()
{
    audio = GetComponent<AudioSource>();
}

void Update()
{
    RespondToDebugKeys();
}

private void RespondToDebugKeys()
{
    if(Input.GetKeyDown(KeyCode.L))
    {
        LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
        collisionDisabled = !collisionDisabled;
    }
}

    void OnCollisionEnter(Collision other) 
{
    if (isTransitioning || collisionDisabled){return;}
    switch(other.gameObject.tag)
    {
        case "Friendly":
            Debug.Log("You're Good");
            break;
        case "Finish":
            Proceed();
            break;
        default:
            Crash();
            break;
    }
}

void ReloadLevel()
{
    int currentIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentIndex);
}
void LoadNextLevel()
{
    int currentIndex = SceneManager.GetActiveScene().buildIndex;
    int nextIndex = currentIndex + 1;
    if (nextIndex == SceneManager.sceneCountInBuildSettings)
    {
        nextIndex = 0;
    }
    SceneManager.LoadScene(nextIndex);
}

void Crash()
{
    isTransitioning = true;
    audio.Stop();
    audio.PlayOneShot(crash);
    crashParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", delay);
}
void Proceed()
{
    isTransitioning = true;
    audio.Stop();
    audio.PlayOneShot(landed);
    landedParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", delay);
}
}
