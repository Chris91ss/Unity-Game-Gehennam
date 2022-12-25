using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public float MovementSpeed = 1f;   
    public float JumpForce = 1f;       
    private Rigidbody2D _rigidbody;

    private bool m_FacingRight = true;  

    

    private GameObject[] players;    

    public GameObject CanvasHealthBar;  
    private GameObject[] HealthBARS;   
    public GameObject keepCamera;  
    private GameObject[] Cameras;   

    private float slideSpeed;     
    private Vector3 RollDirection; 
    public float RollRate = 1.2f;
    private float nextRollTime = 0f;
    private State state;   
    private enum State   
    {
        Normal,
        DodgeRollSliding,
    }

  
    private void Start()
    {
        DontDestroyOnLoad(CanvasHealthBar);
        DontDestroyOnLoad(gameObject);     
        DontDestroyOnLoad(keepCamera);    
        _rigidbody = GetComponent<Rigidbody2D>();   
        state = State.Normal;
    }

   
    private void Update()
    {
        switch (state)                 
        {
            case State.Normal:             
                Movement();               
                HandleDodgeRoll();         
                break;
            case State.DodgeRollSliding:   
                HandleDodgeRollSliding();  
                break;
        }
    }

    private void HandleDodgeRoll()
    {
        if(Time.time >= nextRollTime)  
        {
        if (Input.GetButtonDown("Roll"))
        {
            state = State.DodgeRollSliding;
            RollDirection = new Vector2(Input.GetAxis("Horizontal"), 0); 
            slideSpeed = 45f;  
            nextRollTime = Time.time + 1f / RollRate; 
        }
        }
    }

    private void HandleDodgeRollSliding()
    {
        transform.position += RollDirection * slideSpeed * Time.deltaTime;  
        slideSpeed -= slideSpeed * 10f * Time.deltaTime;   

        if (slideSpeed < 1f)                              
        {
            state = State.Normal;
        }
    }

    private void Movement()
    {
        var movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if (movement > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (movement < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnLevelWasLoaded(int level)
    {
            FindStartPos();
            Cameras = GameObject.FindGameObjectsWithTag("MainCamera"); // Cautam obiectul cu tag-ul "MainCamera";
            players = GameObject.FindGameObjectsWithTag("Player");  // Cautam obiectul cu tag-ul "Player";
            HealthBARS = GameObject.FindGameObjectsWithTag("HealthBar");

            if (players.Length > 1)  
            {
                Destroy(players[0]);
            }

            if(Cameras.Length > 1)  
            {
                Destroy(Cameras[0]);
            }

            if(HealthBARS.Length > 1)  
            {
                Destroy(HealthBARS[0]);
            }
    }

    void FindStartPos()   
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }

     
}

