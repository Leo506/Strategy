using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Following
{
    public class EnemyTank : MonoBehaviour
    {
        [SerializeField] GraphNode graph;

        [SerializeField] Transform target;
        [SerializeField] float speed;

        private SearchInWidth search;

        private Vector2 nextPoint;

        private bool nearTarget = false;

        // Start is called before the first frame update
        void Start()
        {
            search = new SearchInWidth(graph);


        }

        // Update is called once per frame
        void Update()
        {
            if (Vector2.Distance(target.position, transform.position) <= 5)
            {
                nearTarget = true;
                return;
            }
            else if (nearTarget)
            {
                nextPoint = search.GetNextPoint(transform.position, target.position);
                nearTarget = false;
            }

            if (Vector2.Distance((Vector2)transform.position, nextPoint) <= 0.1f)
            {
                nextPoint = search.GetNextPoint(transform.position, target.position);
            }

            var dir = (nextPoint - (Vector2)(transform.position)).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }

       
    }
}