using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TestScripts
{

    public class Test : MonoBehaviour
    {
        [SerializeField] Tilemap tilemap;
        [SerializeField] PathFinder finder;
        [SerializeField] Transform unit;
        [SerializeField] Vector3Int endPoint;

        // Start is called before the first frame update
        void Start()
        {
            var path = finder.GetPath(unit.position, endPoint);
            var current = path[endPoint];
            Debug.Log(current);
            while (current != null)
            {
                current = path[current.Value];
                Debug.Log(current);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(tilemap.WorldToCell(worldPos));
            }
        }
    }
}