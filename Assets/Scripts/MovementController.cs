using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    [SerializeField] PathController pathController;
    [SerializeField] SelectObjects select;

    private void Awake()
    {
        PlayerInput.OnInputEvent += MoveUnits;
    }

    private void OnDestroy()
    {
        PlayerInput.OnInputEvent -= MoveUnits;
    }

    private void MoveUnits(Vector3 endPos)
    {
        var units = select.GetSelectObj();
        int nearUnitIndex = 0;
        for (int i = 1; i < units.Count; i++) 
        {
            if (Vector3.Distance(endPos, units[i].transform.position) < Vector3.Distance(endPos, units[nearUnitIndex].transform.position))
                nearUnitIndex = i;
        }

        if (units.Count == 0)
            return;

        var path = pathController.GetPath(units[nearUnitIndex].transform.position, endPos);

        StartCoroutine(Movement(path, units, nearUnitIndex));
    }

    IEnumerator Movement(List<Vector3> path, List<GameObject> units, int unitIndex)
    {
        Debug.Log("Movement!!!");
        int pathPointIndex = 0;
        var dir = (path[pathPointIndex] - units[unitIndex].transform.position).normalized;
        Debug.Log("Current target: " + path[pathPointIndex] + " Dir: " + dir);

        while (pathPointIndex != path.Count)
        {
            foreach (var unit in units)
                unit.transform.Translate(dir * Time.deltaTime * 10);

            if (Vector3.Distance(path[pathPointIndex], units[unitIndex].transform.position) <= 0.1f)
            {
                pathPointIndex++;

                if (pathPointIndex != path.Count)
                    dir = (path[pathPointIndex] - units[unitIndex].transform.position).normalized;
            }

            yield return null;
        }
    }
}
