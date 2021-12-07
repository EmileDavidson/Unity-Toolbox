using System;
using UnityEngine;

namespace Toolbox.Grid
{
    [Serializable]
    public class Cell2D : ICell2D
    {
        public Vector2Int GridPosition { get; set; }
        public int Index { get; set; }
    }
}