using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private enum CombatState
    {
        NONE,
        PUNCH1,
        PUNCH2,
        PUNCH3,
        KICK1,
        KCIK2

    }
    private Animator Anim;
    private AnimatorStateInfo Anista;
    private Rigidbody rb;

    [Header("PlayerStates")]
    [SerializeField] private float MoveSpeed = 5;
    public float JumpPower = 2;



    private float default_Combo_Timer = 0.5f;
    private float current_Combo_Timer;
    private CombatState current_Combo_State;
    private Vector3 dir = Vector3.zero;


    private bool isFlip = false;
    private bool isJump = false;
    private bool isAttack = false;
    private bool Attack_to_rest = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = CombatState.NONE;
        //current_Combo_State = (CombatState)1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Anista = Anim.GetCurrentAnimatorStateInfo(0);
        ResetCombo();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical") / 2;

        if (Anista.IsTag("Motion") || Anista.IsTag("WalkingRight") || Anista.IsTag("WalkingLeft") || Anista.IsTag("WalkFoward") || Anista.IsTag("WalkBakc"))
        {
            if (x > 0)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                //transform.localRotation = Quaternion.Euler(0, 90, 0);

                isFlip = false;
            }
            else if (x < 0)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                //transform.localRotation = Quaternion.Euler(0, 270, 0);
                isFlip = true;
            }
        }

        dir = new Vector3(x, 0, y);

        //애니매이션 끄기

        if (y == 0)
        {
            Anim.SetBool("Rightward", false);
            Anim.SetBool("Leftward", false);
        }

        if (x == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }



        if (Anista.IsTag("Motion") || Anista.IsTag("WalkingRight") || Anista.IsTag("WalkingLeft") || Anista.IsTag("WalkFoward") || Anista.IsTag("WalkBakc"))
        {

            Attack();


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

    private void Attack()
    {
        // if (!isAttack)
        //{
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (current_Combo_State == CombatState.PUNCH3 || current_Combo_State == CombatState.KICK1 ||
            current_Combo_State == CombatState.KCIK2)
                return;

            current_Combo_State++;
            current_Combo_Timer = default_Combo_Timer;
            Attack_to_rest = true;

            if (current_Combo_State == CombatState.PUNCH1)
            {
                Anim.SetTrigger("SImplePunchL");
            }

            if (current_Combo_State == CombatState.PUNCH2)
            {
                Anim.SetTrigger("PunchCombo1");
            }

            if (current_Combo_State == CombatState.PUNCH3)
            {
                Anim.SetTrigger("PunchCombo2");
            }

            isAttack = true;
            StartCoroutine("AttackCoolTime");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {

            Anim.SetTrigger("HeavyPunchR");
            isAttack = true;
            StartCoroutine("AttackCoolTime");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Anim.SetTrigger("SimpleKickL");
            isAttack = true;
            StartCoroutine("AttackCoolTime");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Anim.SetTrigger("HeavyKickR");
            isAttack = true;
            StartCoroutine("AttackCoolTime");
        }
        // }

    }

    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(0.1f);
        isAttack = false;
    }

    private void ResetCombo()
    {
        if (Attack_to_rest)
        {
            current_Combo_Timer -= Time.deltaTime;
            if (current_Combo_Timer <= 0)
            {
                current_Combo_State = CombatState.NONE;
                Attack_to_rest = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }
}
