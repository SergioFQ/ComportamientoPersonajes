using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseAndAvoid : MonoBehaviour
{
    [SerializeField] private BaseAgent BS;
    [SerializeField] private string[] TagsToAvoid;
    public Vector2 dir;

    public Vector2 lastPos;
    public float movespeed = 10;

    private bool isHit = false;

    private int isRight;
    private float distance;

    void Start()
    {
        lastPos = new Vector2(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 1, 0) * movespeed * Time.deltaTime);
        if (isHit)
        {
            transform.Rotate(0f, 0f, isRight * 180f * Time.deltaTime * (BS.maxPerceptionL - distance), Space.Self);
            isHit = false;
        }
    }

    public void HitSnA(Collider2D col)
    {
        Vector3 direct = col.transform.position - transform.position;
        if (Vector3.Dot(direct, transform.right) < 0) isRight = -1;
        else isRight = 1;
        distance = Mathf.Sqrt(Mathf.Pow((col.transform.position.x - transform.position.x), 2) + Mathf.Pow((col.transform.position.y - transform.position.y), 2));
        isHit = true;
    }
}
