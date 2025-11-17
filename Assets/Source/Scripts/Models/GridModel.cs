using System.Collections.Generic;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Views;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace MiniIT.Models
{
    public class GridModel
    {
        private readonly GridConfig _gridConfig;

        private readonly List<CellData> _cellsData = null;

        public IReadOnlyList<CellData> CellsData => _cellsData;

        public GridModel(GridConfig gridConfig)
        {
            _gridConfig = gridConfig;

            _cellsData = new List<CellData>();
        }

        public bool TryGetFreeCell(out CellData freeCell)
        {
            Shuffle(_cellsData);

            for (int i = 0; i < _cellsData.Count; i++)
            {
                if (_cellsData[i].IsBusy == false)
                {
                    freeCell = _cellsData[i];
                    return true;
                }
            }

            freeCell = default;
            return false;
        }

        public void BuildGrid()
        {
            float width = _gridConfig.GridSize.x;
            float height = _gridConfig.GridSize.y;
            float cellSize = _gridConfig.CellSize;
            float spacing = _gridConfig.CellSpacing;

            float totalWidth = width * cellSize + (width - 1) * spacing;
            float totalHeight = height * cellSize + (height - 1) * spacing;

            Vector2 offset = new Vector2(-totalWidth * 0.5f, -totalHeight * 0.5f) +
                             new Vector2(cellSize * 0.5f, cellSize * 0.5f);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 localPos = new Vector2(i * (cellSize + spacing), j * (cellSize + spacing));

                    Vector2 worldPos = offset + localPos;

                    CellView cellInstance = Object.Instantiate(_gridConfig.CellPrefab, worldPos, Quaternion.identity);

                    cellInstance.transform.localScale =
                        new Vector3(_gridConfig.CellSize, _gridConfig.CellSize, _gridConfig.CellSize);

                    CellData newCellData = new CellData(cellInstance);
                    
#if ENABLE_DEBUG
                    cellInstance.name = $"Cell {i}-{j}";
                    newCellData.Name = cellInstance.name;
#endif

                    _cellsData.Add(newCellData);
                }
            }
        }

        private void Shuffle<T>(IList<T> cells)
        {
            int index = cells.Count;

            while (index > 1)
            {
                index--;

                int randomElement = Random.Range(0, index + 1);
                (cells[randomElement], cells[index]) = (cells[index], cells[randomElement]);
            }
        }
    }
}