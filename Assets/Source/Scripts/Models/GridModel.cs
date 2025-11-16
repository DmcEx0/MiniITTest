using System.Collections.Generic;
using MiniIT.Configs;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Models
{
    public class GridModel
    {
        private readonly GameConfig _gameConfig;
        private readonly Dictionary<CellView, bool> _cells = null;
        
        public IReadOnlyDictionary<CellView, bool> Cells => _cells;

        public GridModel(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _cells = new Dictionary<CellView, bool>();
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

                    _cells.Add(cellInstance, false);
                }
            }
        }
    }
}
