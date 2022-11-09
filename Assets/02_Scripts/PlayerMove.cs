using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator Anim;
    private AnimatorStateInfo Anista;
    private Rigidbody rb;

    [Header("PlayerStates")]
    [SerializeField] private float MoveSpeed = 5;
    public float JumpPower = 2;

    private Vector3 dir = Vector3.zero;


    private bool isFlip = false;
    private bool isJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Anista = Anim.GetCurrentAnimatorStateInfo(0);
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical") / 2;

        if (x > 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            isFlip = false;
        }
        else if (x < 0)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            isFlip = true;
        }
        dir = new Vector3(x, 0, y);

        if (Anista.IsTag("Motion") || Anista.IsTag("WalkingRight") || Anista.IsTag("WalkingLeft") || Anista.IsTag("WalkFoward") || Anista.IsTag("WalkBakc"))
        {
            rb.MovePosition(this.gameObject.transform.position + dir * MoveSpeed * Time.deltaTime);
            if (dir != Vector3.zero)
            {

                if (x != 0)
                {
                    if (x > 0)
                    {
                        Anim.SetBool("Forward", true);
                    }
                    else
                    {
                        Anim.SetBool("Backward", true);
                    }
                }
                else
                {
                    Anim.SetBool("Forward", false);
                    Anim.SetBool("Backward", false);
                }

                if (y > 0)
                {
                    if (isFlip)
                    {
                        Anim.SetBool("Rightward", true);
                    }
                    else
                    {
                        Anim.SetBool("Leftward", true);
                    }
                }
                else if (y < 0)
                {
                    if (isFlip)
                    {
                        Anim.SetBool("Leftward", true);
                    }
                    else
                    {
                        Anim.SetBool("Rightward", true);
                    }
                }
                else
                {
                    Anim.SetBool("Rightward", false);
                    Anim.SetBool("Leftward", false);
                }
            }
            // else
            // {
            //     Anim.SetBool("Forward", false);
            //     Anim.SetBool("Backward", false);
            //     Anim.SetBool("Rightward", false);
            //     Anim.SetBool("Leftward", false);
            // }
        }

        if (Input.GetAxisRaw("Jump") != 0 && !isJump)
        {
            Anim.SetTrigger("Jump");
            StartCoroutine(JumpPaus());
            //Jump();
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

    public void JumpUp()
    {
        rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }

    private IEnumerator JumpPaus()
    {
        isJump = true;
        yield return new WaitForSeconds(1.0f);
        isJump = false;
    }
}
