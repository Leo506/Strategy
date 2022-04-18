using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjects : MonoBehaviour
{
    public GameObject[] units;
    public List<GameObject> selectUnits;

    public GUISkin skin;

    private Rect rect;
    private bool draw;
    private Vector2 startPos;
    private Vector2 endPos;


    public List<GameObject> GetSelectObj()
    {
        return selectUnits;
    }

    private void Awake()
    {
        selectUnits = new List<GameObject>();
    }

    private bool CheckUnit(GameObject unit)
    {
        foreach (var item in selectUnits)
        {
            if (item == unit)
                return true;
        }

        return false;
    }

    private void Select()
    {
        if (selectUnits.Count == 0)
            return;

        foreach (var item in selectUnits)
        {
            if (CheckUnit(item))
                item.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void Deselect()
    {
        if (selectUnits.Count == 0)
            return;

        foreach (var item in selectUnits)
        {
            if (CheckUnit(item))
                item.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnGUI()
    {
        GUI.skin = skin;
        GUI.depth = 99;

        if (Input.GetMouseButtonDown(0))
        {
            Deselect();
            startPos = Input.mousePosition;
            draw = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            draw = false;
            Select();
        }

        if (draw)
        {
            selectUnits.Clear();
            endPos = Input.mousePosition;

            if (startPos == endPos)
                return;

            rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                            Screen.height - Mathf.Max(endPos.y, startPos.y),
                            Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                            Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y));

            GUI.Box(rect, "");

            for (int i = 0; i < units.Length; i++)
            {
                var x = Camera.main.WorldToScreenPoint(units[i].transform.position).x;
                var y = Screen.height - Camera.main.WorldToScreenPoint(units[i].transform.position).y;
                Vector2 tmp = new Vector2(x, y);

                if (rect.Contains(tmp))
                {
                    if (selectUnits.Count == 0)
                        selectUnits.Add(units[i]);
                    else if (!CheckUnit(units[i]))
                        selectUnits.Add(units[i]);
                }
            }
        }
    }
}
