using UnityEngine;

public class MarioGameFlow : MonoBehaviour
{
    // State flow
    State currentState;
    public enum State
    {
        Idle,
        Attack,
        Block
    }

    // Scene references
    public MarioUi ui;

    MarioController mario;
    GoombaController enemy;


    void Start()
    {
        // Find all of the necessary references
        ui = FindFirstObjectByType<MarioUi>();
        mario = FindFirstObjectByType<MarioController>();
        enemy = FindFirstObjectByType<GoombaController>();

        SetState(State.Idle);
    }

    public void SetState(State newState)
    {
        currentState = newState;
        ui.SetState(currentState);

        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(mario.ExecuteAttack(enemy));
                break;
            case State.Block:
                StartCoroutine(enemy.ExecuteAttack(mario));
                break;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.Block:
                UpdateBlock();
                break;
        }
    }

    void UpdateIdle()
    {

    }

    void UpdateAttack()
    {
        // Exit the attack flow once Mario is done attacking
        if (mario.IsAttacking() == false)
        {
            SetState(State.Block);
        }
    }

    void UpdateBlock()
    {
        // Exit the block flow once the enemy is done attacking
        if (enemy.IsAttacking() == false)
        {
            SetState(State.Idle);
        }
    }
}
