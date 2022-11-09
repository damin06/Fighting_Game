using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    PlayerMove playerMove;
    void Start()
    {
        playerMove = transform.parent.gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jump()
    {
        //playerMove.transform.Translate(0, playerMove.JumpPower, 0);
        //playerMove.rb.AddForce(Vector3.up * playerMove.JumpPower, ForceMode.Impulse);
        playerMove.JumpUp();
    }
}
