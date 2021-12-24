using System;
using System.Collections.Generic;
using Toolbox.Other;
using UnityEngine;

namespace Toolbox.Grid.Example.HeatmapExample
{
    public class HeatMapGridTester : MonoBehaviour
    {
        public Grid2D<HeatMapCellObject> grid = new Grid2D<HeatMapCellObject>(15, 15);

        private void Awake()
        {
            GridHeatmapHandler.Singleton.Instance.grids.Add(grid);
            Grid2D<ICell2D> cell = GridHeatmapHandler.Singleton.Instance.grids[0] as Grid2D<ICell2D>;
            
            Debug.Log(grid);
            Debug.Log(cell);
        }

    }
}