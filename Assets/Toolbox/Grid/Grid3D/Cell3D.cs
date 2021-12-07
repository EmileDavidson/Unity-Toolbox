using Toolbox.Grid.Grid3D.Cells;
using UnityEngine;

namespace Toolbox.Grid.Grid3D
{
    public class Cell3D : ICell3D
    {
        public Vector3Int GridPosition { get; set; }
        public int Index { get; set; }
    }
}