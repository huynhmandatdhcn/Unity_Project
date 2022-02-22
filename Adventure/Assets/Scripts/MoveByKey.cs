using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKey : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float runSpeed;
    public float jumpSpeed;
    public LayerMask groundLayers;
    public Transform body;

    // Update is called once per frame
    void Update()
    {
        UpdateRunning();
        UpdateJumping();
        UpdateFacing();
    }

    private void UpdateRunning()
    {
        var xInput = Input.GetAxis("Horizontal");
        Debug.Log(xInput);
        var newVelocity = rigid.velocity;
        newVelocity.x = xInput * runSpeed;
        rigid.velocity = newVelocity;
    }

    private void UpdateJumping()
    {
        if (JumpButtonPressed && IsOnGround())
        {
            var newVelocity = rigid.velocity;
            newVelocity.y = jumpSpeed;
            rigid.velocity = newVelocity;
        }
    }

    private void UpdateFacing()
    {
        var yAngle = (rigid.velocity.x < 0) ? 180 : 0;
        body.localRotation = Quaternion.Euler(0, yAngle, 0);
    }

    private bool JumpButtonPressed => Input.GetAxis("Jump") == 1;

    private bool IsOnGround() 
        => Physics2D.OverlapCircle(transform.position, 0.1f, groundLayers) != null;

    private void OnValidate() => rigid = GetComponent<Rigidbody2D>();
}
