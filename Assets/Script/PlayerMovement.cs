using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private bool playingFootSteps =false;
    private float footStepSpeed =0.5f;

    // Start is called before the first frame update  
    
void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame  
   
void Update()
    {
        //if (PauseController.IsGamePaused)
        //{
        //    rb.velocity = Vector2.zero; // Stop movement  
        //    animator.SetBool("isWalking", false);
        //    StopFootsteps();
        //    return;
        //} 

        rb.velocity = moveInput * moveSpeed;
        animator.SetBool("isWalking", rb.velocity.magnitude > 0);

        if (rb.velocity.magnitude > 0 && !playingFootSteps)
        {
            StartFootsteps();
        }
        else if (rb.velocity.magnitude == 0)
        {
            StopFootsteps();
        }
    }

  
public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastinputX", moveInput.x);
            animator.SetFloat("LastinputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

    }
    void StartFootsteps()
    {
        playingFootSteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, footStepSpeed);
    }

    void StopFootsteps()
    {
        playingFootSteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    void PlayFootstep()
    {
        SoundEffectManager.Play("Footstep");
    }
}
