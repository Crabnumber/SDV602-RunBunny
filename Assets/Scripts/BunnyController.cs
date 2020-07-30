﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Collider2D myCollider;
    public float bunnyJumpForce = 500f;
    private Animator myAnim;
    string currentScene;
    private float bunnyHurtTime = -1;
    public Text scoreText;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;
        myCollider = GetComponent<Collider2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (bunnyHurtTime == -1) 
        {
            if(Input.GetButtonUp("Jump"))
            {
                myRigidBody.AddForce(transform.up * bunnyJumpForce);
            }
            myAnim.SetFloat("Velocity",System.Math.Abs(myRigidBody.velocity.y));
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            if(Time.time > bunnyHurtTime + 2)
            {
                SceneManager.LoadScene(currentScene);
                //Application.LoadLevel has been depreciated from the time of the tutorial.
                //Replaced with SceneManager.
                //Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
       if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") )
       {
           PrefabSpawner spawner = FindObjectOfType<PrefabSpawner>();
           spawner.enabled = false;
           foreach (MoveLeft movelefter in FindObjectsOfType<MoveLeft>())
           {
               movelefter.enabled = false;
           }
           bunnyHurtTime = Time.time;
           myAnim.SetBool("Bunny Hurt", true);
           myRigidBody.velocity = Vector2.zero;
           myRigidBody.AddForce(transform.up * bunnyJumpForce);
           myCollider.enabled = false;
       }
        
        


    }
}
