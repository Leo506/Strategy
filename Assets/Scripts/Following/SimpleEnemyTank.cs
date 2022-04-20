using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyTank : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;

    

    // Update is called once per frame
    void Update()
    {

        var dir = (target.position - transform.position).normalized;

        transform.Translate(0, speed * Time.deltaTime, 0);

        var angle = Vector2.SignedAngle(transform.up, dir);

        Debug.Log(angle);

        var angles = this.transform.eulerAngles;
        angles.z += angle;
        transform.eulerAngles = angles;
        
    }
}
