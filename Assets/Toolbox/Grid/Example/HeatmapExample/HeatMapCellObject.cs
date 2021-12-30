using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class HeatMapCellObject : ICell2D, IHeatMapCell, ICell
    {
        public Vector2Int GridPosition { get; set; }
        public int Index { get; set; }
        public float HeatValue { get; set; }
    }
}
