using UnityEngine;

public class PopObject : MonoBehaviour
{
    public PopScore scorePrefab;
    public int scoreValue = 100;


    public void Popped()
    {
        Destroy(gameObject);

        PopScore score = Instantiate(scorePrefab);
        score.transform.position = transform.position;
        score.Play(scoreValue);
    }
}
