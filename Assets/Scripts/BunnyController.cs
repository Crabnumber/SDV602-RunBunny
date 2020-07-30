using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public float bunnyJumpForce = 500f;
    private Animator myAnim;
    string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Jump"))
        {
            myRigidBody.AddForce(transform.up * bunnyJumpForce);
        }
        myAnim.SetFloat("Velocity",System.Math.Abs(myRigidBody.velocity.y));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
       if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
       {
           string currentScene = SceneManager.GetActiveScene().name;
           SceneManager.LoadScene(currentScene);
       }
        
        //Application.LoadLevel has been made redundant from the time of the tutorial.
        //Replaced with SceneManager. Functionality appears identical.
        //Application.LoadLevel(Application.loadedLevel);


    }
}
