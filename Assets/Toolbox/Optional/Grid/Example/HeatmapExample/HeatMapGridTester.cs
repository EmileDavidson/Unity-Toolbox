using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class HeatMapGridTester : MonoBehaviour
    {
        public Grid2D<HeatMapCellObject> grid = new Grid2D<HeatMapCellObject>(15, 15);
        public Grid2D<HeatMapCellObject> grid1 = new Grid2D<HeatMapCellObject>(15, 15); 
        
        public Grid2D<OtherHeatMapCellObject> grid2 = new Grid2D<OtherHeatMapCellObject>(15, 15); 
        public Grid2D<OtherHeatMapCellObject> grid3 = new Grid2D<OtherHeatMapCellObject>(15, 15); 

        private void Awake()
        {
            //adding all grids to handler
            GridHeatmapHandler.Instance.AddGrid(grid);
            print(GridHeatmapHandler.Instance.GridCount);
            GridHeatmapHandler.Instance.AddGrid(grid1);
            print(GridHeatmapHandler.Instance.GridCount);
            GridHeatmapHandler.Instance.AddGrid(grid2);
            print(GridHeatmapHandler.Instance.GridCount);
            GridHeatmapHandler.Instance.AddGrid(grid3);
            print(GridHeatmapHandler.Instance.GridCount);
        }
    }
}
