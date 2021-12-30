using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class HeatMapGridTester : MonoBehaviour
    {
        public Grid2D<HeatMapCellObject> grid = new Grid2D<HeatMapCellObject>(15, 15);

        private void Awake()
        {
            GridHeatmapHandler.Singleton.Instance.grids.Add(grid); // error Argument type 'Toolbox.Grid.Grid2D<Toolbox.Grid.Example.HeatmapExample.HeatMapCellObject>' is not assignable to parameter type 'Toolbox.Grid.Grid2D<Toolbox.Grid.ICell2D>
        }
    }
}