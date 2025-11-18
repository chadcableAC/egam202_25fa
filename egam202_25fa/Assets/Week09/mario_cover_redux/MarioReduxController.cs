using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarioReduxController : MonoBehaviour
{
    public Transform moveHandle;

    public Transform startHandle;
    public Transform enemyHandle;
    public Transform onEnemyHandle;

    public State currentState;
    public enum State
    {
        Idle,
        WalkTo,
        Jumps,
        JumpEnd,
        WalkAway
    }

    public enum InputState
    {
        Pending,
        Success,
        Fail
    }

    public Animator animator;

    public float walkDuration = 1f;
    public float pauseDuration = 1f;
    public List<float> jumpDurations;
    public float jumpEndDuration = 1f;

    public float validInputDuration = 0.33f;

    InputAction jumpAction;

    public CinemachineCamera idleCam;
    public CinemachineCamera followCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Input
        jumpAction = InputSystem.actions.FindAction("Jump");

        idleCam.Prioritize();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            // Start the attack only while idle
            case State.Idle:
                if (jumpAction.WasPressedThisFrame())
                {
                    StartCoroutine(ExecuteAttack());
                }
                break;
        }
    }

    IEnumerator ExecuteAttack()
    {
        // Switch to follow cam
        followCam.Prioritize();

        // Walk up
        currentState = State.WalkTo;
        yield return StartCoroutine(ExecuteWalk(startHandle.position, enemyHandle.position));
        yield return new WaitForSeconds(pauseDuration);

        // Attack / jump sequence
        yield return StartCoroutine(ExecuteJumps());

        // Walk back
        currentState = State.WalkAway;
        yield return new WaitForSeconds(pauseDuration);
        yield return StartCoroutine(ExecuteWalk(enemyHandle.position, startHandle.position));

        // Back to idle
        currentState = State.Idle;
        idleCam.Prioritize();
    }

    IEnumerator ExecuteWalk(Vector3 fromPosition, Vector3 toPosition)
    {
        // Set walking
        animator.SetBool("IsWalking", true);

        // Run an "update" loop to move the character
        float timer = 0;
        while (timer < walkDuration)
        {
            float interp = timer / walkDuration;
            Vector3 position = Vector3.Lerp(fromPosition, toPosition, interp);
            moveHandle.position = position;

            yield return null;
            timer += Time.deltaTime;
        }

        // Unset walking
        animator.SetBool("IsWalking", false);
    }

    IEnumerator ExecuteMove(Vector3 fromPosition, Vector3 toPosition)
    {
        // Run an "update" loop to move the character
        float timer = 0;
        while (timer < walkDuration)
        {
            float interp = timer / walkDuration;
            Vector3 position = Vector3.Lerp(fromPosition, toPosition, interp);
            moveHandle.position = position;

            yield return null;
            timer += Time.deltaTime;
        }
    }

    IEnumerator ExecuteJumps()
    {
        // Update state
        currentState = State.Jumps;

        // Move on top of the character
        // There's no "yield" so this happens simultaneously with jumping
        StartCoroutine(ExecuteMove(enemyHandle.position, onEnemyHandle.position));

        // Keep jumping!
        for (int i = 0; i < jumpDurations.Count; i++)
        {
            // Start the jump
            animator.SetTrigger("OnJump");

            // Wait for the animation to end
            float jumpDuration = jumpDurations[i];

            // Check for button presses
            InputState inputState = InputState.Pending;
            float timer = 0;
            while (timer < jumpDuration)
            {
                if (jumpAction.WasPressedThisFrame())
                {
                    float minimumTime = jumpDuration - validInputDuration;
                    if (timer > minimumTime)
                    {
                        switch (inputState)
                        {
                            // If we've already succeeded, but press again, fail
                            case InputState.Success:
                                inputState = InputState.Fail;
                                break;
                            // Otherwise it's our first success
                            case InputState.Pending:
                                inputState = InputState.Success;
                                break;
                        }
                    }
                    // This was bad timing - fail
                    else
                    {
                        inputState = InputState.Fail;
                    }
                }                

                yield return null;
                timer += Time.deltaTime;
            }

            // Decide - success (another jump) or fail (exit the jump)
            if (inputState == InputState.Success)
            {
                // Do nothing - allow the loop to continue
            }
            else
            {
                // We failed - break out of the loop
                break;
            }
        }

        // Move back to the approach position
        StartCoroutine(ExecuteMove(onEnemyHandle.position, enemyHandle.position));

        // Trigger the final jump off animation
        currentState = State.JumpEnd;
        animator.SetTrigger("OnFail");
        yield return new WaitForSeconds(jumpEndDuration);
    }
}
