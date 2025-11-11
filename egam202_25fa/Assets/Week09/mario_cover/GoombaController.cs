using System.Collections;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    public bool isAttackActive = false;

    public Transform idleGoalHandle;
    public Transform marioGoalHandle;

    public float walkUpDuration = 1;
    public float walkBackDuration = 1;

    public bool IsAttacking()
    {
        return isAttackActive;
    }

    public IEnumerator ExecuteAttack(MarioController mario)
    {
        // Starting the attack
        isAttackActive = true;

        // First walk up to the Mario
        Vector3 idlePosition = idleGoalHandle.position;
        Vector3 goalPosition = mario.enemyGoalHandle.position;

        yield return StartCoroutine(ExecuteWalk(idlePosition, goalPosition, walkUpDuration));

        // TODO - attack
        yield return new WaitForSeconds(1);

        // At the end, walk back to our goal position
        yield return StartCoroutine(ExecuteWalk(goalPosition, idlePosition, walkBackDuration));

        // All done
        isAttackActive = false;
    }

    IEnumerator ExecuteWalk(Vector3 start, Vector3 end, float duration)
    {
        // Move from A to B
        float timer = 0;
        while (timer < duration)
        {
            float interp = timer / duration;
            Vector3 pos = Vector3.Lerp(start, end, interp);
            pos.y = transform.position.y;
            transform.position = pos;

            yield return null;
            timer += Time.deltaTime;
        }
    }
}
