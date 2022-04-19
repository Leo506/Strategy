using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Construction
{

    public class Builder : MonoBehaviour
    {
        private Camera mainCamera;
        private Building selectedBuilding;
        private GridController grid;

        private void Awake()
        {
            grid = new GridController();
            mainCamera = Camera.main;
        }

        public void CreateNewBuilding(Building buildingPrefab)
        {
            if (selectedBuilding != null)
                Destroy(selectedBuilding.gameObject);

            selectedBuilding = Instantiate(buildingPrefab);
        }

        // Update is called once per frame
        void Update()
        {
            if (selectedBuilding != null)
            {
                Vector2 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                worldPos.x = Mathf.RoundToInt(worldPos.x);
                worldPos.y = Mathf.RoundToInt(worldPos.y);

                selectedBuilding.transform.position = worldPos;

                bool available = grid.CanPlaceObj(new Vector2Int((int)worldPos.x, (int)worldPos.y), selectedBuilding.Size);

                selectedBuilding.SetColor(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    grid.PlaceObj(new Vector2Int((int)worldPos.x, (int)worldPos.y), selectedBuilding.Size);
                    selectedBuilding = null;
                }
            }
        }
    }
}