using System;
using UnityEngine;

namespace Toolbox.Grid.Grid2D
{
    [Serializable]
    public class Cell2D : ICell2D
    {
        public Vector2Int GridPosition { get; set; }
        public int Index { get; set; }
    }
}