using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarioController : MonoBehaviour
{
    public bool isAttackActive = false;

    public Transform idleGoalHandle;
    public Transform enemyGoalHandle;

    public float walkUpDuration = 1;
    public float walkBackDuration = 1;

    public float preJumpDuration = 1;
    public float postJumpDuration = 1;

    public float jumpOnDuration = 1;
    public float bounceDuration1 = 1;
    public float bounceDuration2 = 1;
    public float bounceDuration3 = 1;
    public float jumpOffDuration = 1;

    public AnimationCurve jumpOnCurve;
    public AnimationCurve jumpOffCurve;

    float originalPositionY;

    int bounceCounter;

    public float timingWindow = 0.25f;

    InputAction jumpAction;

    public Animator animator;

    private void Start()
    {
        // Keep a consistent Y position
        originalPositionY = transform.position.y;

        // Input
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    public bool IsAttacking()
    {
        return isAttackActive;
    }

    public IEnumerator ExecuteAttack(GoombaController enemy)
    {
        // Starting the attack
        isAttackActive = true;

        // First walk up to the enemy
        yield return StartCoroutine(ExecuteWalkUp(enemy));
        yield return new WaitForSeconds(preJumpDuration);

        // Attack!
        yield return StartCoroutine(ExecuteJump(enemy));

        // At the end, walk back to our goal position
        yield return new WaitForSeconds(postJumpDuration);
        yield return StartCoroutine(ExecuteWalkBack(enemy));

        // All done
        isAttackActive = false;
    }

    IEnumerator ExecuteWalkUp(GoombaController enemy)
    {
        animator.SetBool("IsWalking", true);

        // Move from A to B
        Vector3 originalPosition = idleGoalHandle.position;
        Vector3 walkPosition = enemy.marioGoalHandle.position;

        // Start a timer that lasts walkUpDuration
        float timer = 0;
        while (timer < walkUpDuration)
        {
            // Find the interp
            float interp = timer / walkUpDuration;

            // Lerp to the right position
            Vector3 pos = Vector3.Lerp(originalPosition, walkPosition, interp);
            // Keep the same Y position
            pos.y = originalPositionY;
            transform.position = pos;

            // Wait a frame
            yield return null;
            timer += Time.deltaTime;
        }

        animator.SetBool("IsWalking", false);
    }

    IEnumerator ExecuteWalkBack(GoombaController enemy)
    {
        animator.SetBool("IsWalking", true);

        // Move from A to B
        Vector3 originalPosition = idleGoalHandle.position;
        Vector3 walkPosition = enemy.marioGoalHandle.position;

        // Start a timer that lasts walkBackDuration
        float timer = 0;
        while (timer < walkBackDuration)
        {
            // Find the interp
            float interp = timer / walkBackDuration;

            // Lerp to the right position
            Vector3 pos = Vector3.Lerp(walkPosition, originalPosition, interp);
            // Keep the same Y position
            pos.y = originalPositionY;
            transform.position = pos;

            // Wait a frame
            yield return null;
            timer += Time.deltaTime;
        }

        animator.SetBool("IsWalking", false);
    }

    IEnumerator ExecuteJump(GoombaController enemy)
    {
        animator.SetTrigger("OnJump");

        Vector3 originalPosition = enemy.marioGoalHandle.position;
        Vector3 jumpPosition = enemy.transform.position;

        // We want to jump onto the enemy
        bool isComboJump = false;
        bounceCounter = 0;

        // Start a timer
        float timer = 0;
        while (timer < jumpOnDuration)
        {
            // Find the interp
            float interp = timer / jumpOnDuration;

            // Lerp for the XZ positions
            Vector3 pos = Vector3.Lerp(originalPosition, jumpPosition, interp);

            // Use the curve for the Y position (Allows us to define an arc)
            pos.y = originalPositionY + jumpOnCurve.Evaluate(interp);

            transform.position = pos;

            // Wait a frame
            yield return null;
            timer += Time.deltaTime;

            // Check for input
            if (jumpAction.WasPressedThisFrame())
            {
                // How close are we to the end of a jump?
                if (timer > jumpOnDuration - timingWindow)
                {
                    // Only set this once
                    if (isComboJump == false)
                    {
                        isComboJump = true;
                        bounceCounter++;
                        animator.SetInteger("BounceCount", bounceCounter);
                    }
                }
            }
        }

        // Run the logic for success/fail jumps?
        if (isComboJump)
        {
            isComboJump = false;

            timer = 0;
            while (timer < bounceDuration1)
            {
                yield return null;
                timer += Time.deltaTime;

                if (jumpAction.WasPressedThisFrame())
                {
                    // How close are we to the end of a jump?
                    if (timer > bounceDuration1 - timingWindow)
                    {
                        if (isComboJump == false)
                        {
                            isComboJump = true;
                            bounceCounter++;
                            animator.SetInteger("BounceCount", bounceCounter);
                        }
                    }
                }
            }
        }

        bounceCounter = 0;
        animator.SetInteger("BounceCount", bounceCounter);

        // Then we want to bounce back to our position
        timer = 0;
        while (timer < jumpOffDuration)
        {
            float interp = timer / jumpOffDuration;
            Vector3 pos = Vector3.Lerp(jumpPosition, originalPosition, interp);

            pos.y = originalPositionY + jumpOffCurve.Evaluate(interp);

            transform.position = pos;

            yield return null;
            timer += Time.deltaTime;
        }
    }
}