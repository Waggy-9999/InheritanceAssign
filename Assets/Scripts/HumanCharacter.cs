using UnityEngine;
using UnityEngine.InputSystem;

// Human character class with movement, jump, and dash abilities
public class HumanCharacter : Character
{
// Define human character movement properties
    public float humanMoveSpeed = 6f;
    public float humanJumpForce = 8f;
    public float dashSpeed = 50f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;

    // Update human character's state on each frame
    public override void Update()
    {
        base.Update();

        // Activate dash when left alt key is pressed
        if (Keyboard.current.leftAltKey.wasPressedThisFrame) ActivateDash();

        // If not dashing, update character movement and jump
        if (!isDashing)
        {
            rb.velocity = new Vector3(moveInput * humanMoveSpeed, rb.velocity.y, 0f);
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(new Vector3(0f, humanJumpForce, 0f), ForceMode.Impulse);
        }
    }

    // Activate dash ability
    public void ActivateDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            float dashDirection = Mathf.Sign(Input.GetAxis("Horizontal"));
            rb.velocity = new Vector3(dashDirection * dashSpeed, rb.velocity.y, 0f);
            Invoke(nameof(DeactivateDash), dashDuration);
        }
    }

    // Deactivate dash ability
    private void DeactivateDash()
    { 
        isDashing = false; 
    }

    // Trigger dash ability with the ability button
    public override void AbilityButton()
    {
        ActivateDash();
    }
}



