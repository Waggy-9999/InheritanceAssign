using UnityEngine;
using UnityEngine.InputSystem;

// Elf character class with movement, jump, and stealth abilities
public class ElfCharacter : Character
{
    // Define elf character movement properties
    public float elfMoveSpeed = 8f;
    public float elfJumpForce = 5f;
    public float stealthDuration = 2f;
    private bool isStealthMode = false;

    // Update elf character's state on each frame
    public override void Update()
    {
        base.Update();

        // Activate stealth when left shift key is pressed
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            ActivateStealth();
        }
        // If not in stealth mode, update character movement and jump
        if (!isStealthMode)
        {
            rb.velocity = new Vector3(moveInput * elfMoveSpeed, rb.velocity.y, 0f);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0f, elfJumpForce, 0f), ForceMode.Impulse);
            }
        }
    }

    // Activate stealth ability
    public void ActivateStealth()
    {
        if (!isStealthMode)
        {
            isStealthMode = true;
            GetComponent<MeshRenderer>().enabled = false;
            Invoke(nameof(DeactivateStealth), stealthDuration);
            Debug.Log("Elf character is in stealth mode.");
        }
    }

    // Deactivate stealth ability
    private void DeactivateStealth()
    {
        isStealthMode = false;
        GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("Elf character is out of stealth mode.");
    }

    // Trigger stealth ability with the ability button
    public override void AbilityButton()
    {
        ActivateStealth();
    }
}


