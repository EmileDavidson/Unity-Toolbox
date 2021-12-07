using Toolbox.Grid;
using UnityEngine;

namespace Toolbox.Grid
{
    public class Cell3D : ICell3D
    {
        public Vector3Int GridPosition { get; set; }
        public int Index { get; set; }
    }
}