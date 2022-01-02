using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class OtherHeatMapCellObject2 : IWeirdCell, IHeatMapCell
    {
        public Vector3Int GridPosition { get; set; }
        public int TestValue { get; set; }
        public int Index { get; set; }
        public float HeatValue { get; set; }
    }

    public interface IWeirdCell : ICell
    {
        
    }
}