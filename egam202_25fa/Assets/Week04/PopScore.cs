using UnityEngine;
using TMPro;

public class PopScore : MonoBehaviour
{
    public TextMeshPro text;

    public float riseSpeed = 1;

    public float duration = 1;
    float timer = 0;

    public void Play(int score)
    {
        text.text = $"+{score}";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        Vector3 camPos = Camera.main.transform.position;
        transform.LookAt(camPos);

        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
