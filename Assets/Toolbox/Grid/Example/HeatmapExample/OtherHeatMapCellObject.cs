using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class OtherHeatMapCellObject : ICell
    {
        public Vector3Int GridPosition { get; set; }
        public int TestValue { get; set; }
        public int Index { get; set; }
    }
}