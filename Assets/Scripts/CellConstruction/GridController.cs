using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Construction
{

    public class GridController
    {
        private Vector2 gridSize = new Vector2() { x = 8, y = 6};

        private List<Vector2> usedCells = new List<Vector2>();

        /// <summary>
        /// Проверяет можно ли расположить объект в этой позиции
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="size">Размер объекта</param>
        /// <returns></returns>
        public bool CanPlaceObj(Vector2Int pos, Vector2Int size)
        {
            if (size.x % 2 == 1) size.x += 1;
            if (size.y % 2 == 1) size.y += 1;

            var cells = GetPlacedCell(pos, size);
;

            return cells.Count == size.x * size.y && cells.Intersect(usedCells).Count() == 0;
        }

        /// <summary>
        /// Получить клетки, которые займет объект
        /// </summary>
        /// <param name="objPos">Положение объекта</param>
        /// <param name="objSize">Размер объекта</param>
        /// <returns></returns>
        public List<Vector2> GetPlacedCell(Vector2Int objPos, Vector2Int objSize)
        {
            List<Vector2> result = new List<Vector2>();

            if (objSize.x % 2 == 1) objSize.x += 1;
            if (objSize.y % 2 == 1) objSize.y += 1;

            int startX = objPos.x - objSize.x / 2;
            int stopX = objPos.x + objSize.x / 2;

            int startY = objPos.y - objSize.y / 2;
            int endY = objPos.y + objSize.y / 2;


            for (int x = startX; x < stopX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    result.Add(new Vector2() { x = x, y = y });
                }
            }

            result = CheckBorders(result);

            return result;
        }

        private List<Vector2> CheckBorders(List<Vector2> cells)
        {
            List<Vector2> toRemove = new List<Vector2>();
            foreach (var cell in cells)
            {
                if (cell.x > gridSize.x || cell.x < 0 || cell.y > gridSize.y || cell.y < 0)
                    toRemove.Add(cell);
            }

            foreach (var cell in toRemove)
            {
                cells.Remove(cell);
            }

            return cells;   
        }


        public void PlaceObj(Vector2Int pos, Vector2Int size)
        {
            foreach (var cell in GetPlacedCell(pos, size))
                usedCells.Add(cell);
        }
    }

    
}
