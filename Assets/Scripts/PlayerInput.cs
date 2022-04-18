using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    public static event System.Action<Vector3> OnInputEvent;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnInputEvent?.Invoke(worldPos);
            Debug.Log("Tile coord: " + tilemap.WorldToCell(worldPos));
        }
    }
}
