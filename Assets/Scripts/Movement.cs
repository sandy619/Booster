using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
     Rigidbody rb;
     AudioSource audsrc;
    [SerializeField] float forca = 1000;
    [SerializeField] float rotatespeed=100;
    [SerializeField] AudioClip launch;
    [SerializeField] ParticleSystem boost;
    [SerializeField] ParticleSystem leftboost;
    [SerializeField] ParticleSystem rightboost;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audsrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up*forca*Time.deltaTime);
            if (!audsrc.isPlaying)
            {
                audsrc.PlayOneShot(launch);
                
            }
            if(!boost.isPlaying)
                boost.Play();


            //Debug.Log("moving up");
        }
        else
        {
            audsrc.Stop();
            boost.Stop();
        }
        
        if(Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true;
            transform.Rotate(0, 0, 1*(Time.deltaTime)*(rotatespeed));
            rb.freezeRotation = false;
            if (!rightboost.isPlaying)
            {
                rightboost.Play();
            }
            //Debug.Log("rotate left");
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true;
            transform.Rotate(0, 0, -1*(Time.deltaTime) * (rotatespeed));
            rb.freezeRotation = false;
            if (!leftboost.isPlaying)
            {
                leftboost.Play();
            }
            //Debug.Log("rotate right");
        }
        else
        {
            leftboost.Stop();
            rightboost.Stop();
        }
    }
}
