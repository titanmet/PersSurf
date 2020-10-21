using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float distance = 3f;
    private float time = 1.583f;
    private float currentDistance = 0f;
    private float currentDir = 0f;
    private Animator animator;
    private bool isInMovement = false;
    private CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxisRaw("Horizontal");
        if (isInMovement == false && dir !=0)
        {
            isInMovement = true;
            currentDir = dir;
            currentDistance = distance;
            if (dir > 0)
                animator.SetTrigger("Right");
            if (dir < 0)
                animator.SetTrigger("Left");
        }  
        
        if (isInMovement)
        {
            Move();
        }
    }

    private void Move()
    {
        if (currentDistance <= 0)
        {
            isInMovement = false;
            return;
        }
        float speed = distance / time;
        float tmpDist = Time.deltaTime * speed;
        character.Move(Vector3.right * currentDir * tmpDist);
        currentDistance -= tmpDist;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
        {
            animator.SetTrigger("Fail");
        }
    }
}
