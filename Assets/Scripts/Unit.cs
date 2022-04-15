using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float speed;

    List<Vector3> path;

    bool canMove = false;
    int pointIndex = 0;

    public void Move(List<Vector3> path)
    {
        canMove = true;
        this.path = path;

        for (int i = 0; i < path.Count; i++)
        {
            path[i] += new Vector3(0, 0.415f, 0);
        }
    }

    private void Update()
    {
        if (canMove)
        {
            var dir = path[pointIndex] - transform.position;
            transform.Translate(dir * speed * Time.deltaTime);

            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, path[pointIndex]) <= 0.1f)
        {
            if (pointIndex + 1 >= path.Count)
            {
                canMove = false;
                pointIndex = 0;
            }
            else
                pointIndex++;
        }
    }
}
