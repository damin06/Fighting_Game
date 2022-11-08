using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator Anim;
    private Rigidbody rb;
    [SerializeField] private float MoveSpeed = 5;

    private Vector3 dir = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        dir = new Vector3(x, 0, y);
        if (dir != Vector3.zero)
        {
            rb.MovePosition(this.gameObject.transform.position + dir * MoveSpeed * Time.deltaTime);
            if (x != 0)
            {
                Anim.SetBool("Forward", true);
            }

            if (y > 0)
            {
                Anim.SetBool("Leftward", true);
            }
            else if (y < 0)
            {
                Anim.SetBool("Rightward", true);
            }
            else
            {
                Anim.SetBool("Rightward", false);
                Anim.SetBool("Leftward", false);
            }
        }
        else
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Rightward", false);
            Anim.SetBool("Leftward", false);
        }



        // if (Input.GetAxisRaw("Horizontal") > 0)
        // {
        //     Anim.SetBool("Forward", true);
        //     rb.AddForce(-MoveSpeed * Time.deltaTime, 0, 0);
        // }
        // if (Input.GetAxisRaw("Horizontal") < 0)
        // {
        //     //Anim.SetBool("Backward", true);
        //     Anim.SetBool("Forward", true);
        //     rb.AddForce(MoveSpeed * Time.deltaTime, 0, 0);
        // }
        // if (Input.GetAxisRaw("Vertical") > 0)
        // {
        //     //Anim.SetTrigger("Jump");
        //     Anim.SetBool("Forward", true);
        //     rb.AddForce(0, 0, MoveSpeed * Time.deltaTime);
        // }
        // if (Input.GetAxisRaw("Vertical") < 0)
        // {
        //     //Anim.SetBool("Crouch", true);
        //     Anim.SetBool("Forward", true);
        //     rb.AddForce(0, 0, -MoveSpeed * Time.deltaTime);
        // }

        // if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        // {
        //     Anim.SetBool("Backward", false);
        //     Anim.SetBool("Forward", false);
        // }

    }
}
