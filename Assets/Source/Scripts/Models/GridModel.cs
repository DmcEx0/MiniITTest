using System.Collections.Generic;
using MiniIT.Configs;
using MiniIT.Data;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace MiniIT.Models
{
    public class GridModel
    {
        private readonly GameConfig _gameConfig;

        private readonly List<CellData> _cellsData = null;

        public IReadOnlyList<CellData> CellsData => _cellsData;

        public GridModel(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;

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
            float width = _gameConfig.GridSize.x;
            float height = _gameConfig.GridSize.y;
            float cellSize = _gameConfig.CellSize;
            float spacing = _gameConfig.CellSpacing;

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

                    var cellInstance = Object.Instantiate(_gameConfig.CellPrefab, worldPos, Quaternion.identity);

                    cellInstance.transform.localScale =
                        new Vector3(_gameConfig.CellSize, _gameConfig.CellSize, _gameConfig.CellSize);

                    var newCellData = new CellData(cellInstance, false);

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