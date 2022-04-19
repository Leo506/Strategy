using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Following
{

    public class Tank : MonoBehaviour
    {
        [SerializeField] float moveSpeed;
        [SerializeField] float rotateSpeed;

        // Update is called once per frame
        void Update()
        {
            var movement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            var rotation = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

            transform.Translate(0, movement, 0);
            transform.Rotate(0, 0, -rotation);
        }
    }
}