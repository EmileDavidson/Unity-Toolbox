using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class HeatMapGridTester : MonoBehaviour
    {
        public Grid2D<HeatMapCellObject> grid = new Grid2D<HeatMapCellObject>(15, 15);
        public Grid2D<HeatMapCellObject> grid1 = new Grid2D<HeatMapCellObject>(15, 15); //copy
        public Grid2D<OtherHeatMapCellObject> grid2 = new Grid2D<OtherHeatMapCellObject>(15, 15);
        public Grid2D<OtherHeatMapCellObject2> grid3 = new Grid2D<OtherHeatMapCellObject2>(15, 15);
        
        private void Awake()
        {
            GridHeatmapHandler.Instance.AddGrid(grid);
            GridHeatmapHandler.Instance.AddGrid(grid1);
            //GridHeatmapHandler.Instance.AddGrid(grid2); //NOT POSSIBLE BECAUSE IT DOESN'T HAVE THE IHeatMapCell interface
            GridHeatmapHandler.Instance.AddGrid(grid3); 
            
            ICell heat = GridHeatmapHandler.Instance.GetCellInterface<HeatMapCellObject>(0, 0);
            heat.TestValue= 1000;


            //GridHeatmapHandler.Instance.GetHeatInterface<OtherHeatMapCellObject>(0, 0); //doesnt work because OtherHeatMapCellObject doesnt contain interface IHeatMapCell
            IHeatMapCell otherHeat = GridHeatmapHandler.Instance.GetHeatInterface<OtherHeatMapCellObject2>(0, 0);
            print(otherHeat);
        }
    }
}
