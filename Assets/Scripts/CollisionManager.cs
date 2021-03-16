using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audsrc;
    bool isActive = false;
    
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip fail;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem failParticle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audsrc = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Start":
                Debug.Log("Start pad");
                break;
            case "Finish":
                Debug.Log("You win");
                
                FinishSequence();
                break;
            default:
                Debug.Log("LOSER");
                
                CrashSequence();
                break;
        }

    }
    void CrashSequence()
    {
        if (!isActive)
        {
            audsrc.PlayOneShot(fail);
            failParticle.Play();

            isActive = true;
        }
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", 1f);
    }
    void ReloadScene()
    {   

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void FinishSequence()
    {
        if (!isActive)
        {
            audsrc.PlayOneShot(success);
            successParticle.Play();
            isActive = true;
        }
        GetComponent<Movement>().enabled = false;
        //rb.freezeRotation=false;

        Invoke("LoadNextScene", 1f);
    }
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
