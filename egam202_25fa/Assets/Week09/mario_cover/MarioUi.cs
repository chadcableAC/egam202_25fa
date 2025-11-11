using UnityEngine;
using UnityEngine.UI;

public class MarioUi : MonoBehaviour
{
    public Button attackButton;

    public void SetState(MarioGameFlow.State state)
    {
        // Default to hidden
        bool isButtonVisible = false;
        switch (state)
        {
            // Show during the Idle Phase
            case MarioGameFlow.State.Idle:
                isButtonVisible = true;
                break;
        }

        attackButton.gameObject.SetActive(isButtonVisible);
    }

    public void OnAttackButton()
    {
        // On press, find the game flow and switch to the Attack state
        MarioGameFlow gameFlow = FindFirstObjectByType<MarioGameFlow>();
        if (gameFlow != null)
        {
            gameFlow.SetState(MarioGameFlow.State.Attack);
        }
    }
}
