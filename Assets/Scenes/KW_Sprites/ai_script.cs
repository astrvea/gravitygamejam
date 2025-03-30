using System.Collections;
using UnityEngine;

public class ai_script : MonoBehaviour
{

    public Transform target_to_move;

    public float large_circle_radius = 0.5f;
    public float move_time = 10.0f;

    Vector3 target;
    float timer = 3;
    private void Start()
    {
        target = Random.insideUnitCircle * 25;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 1);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            target = Random.insideUnitCircle * 25;
            timer = Random.Range(2.0f, 4.0f);
        }
    }
}
