using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class OtherHeatMapCellObject : IHeatMapCell
    {
        public Vector3Int GridPosition { get; set; }
        public int Index { get; set; }
        public float HeatValue { get; set; }
    }
}
