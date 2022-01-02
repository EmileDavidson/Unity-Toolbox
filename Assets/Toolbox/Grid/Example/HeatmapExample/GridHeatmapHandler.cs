using System;
using System.Collections.Generic;
using Toolbox.MethodExtensions;
using Toolbox.Other;
using UnityEngine;

namespace Toolbox.Grid
{
    public class GridHeatmapHandler : MonoSingleton<GridHeatmapHandler>
    {
        private readonly List<object> _grids = new List<object>();

        /// <summary>
        /// For adding grids to the list the grid generic has to have to IHeatMapCell interface
        /// </summary>
        /// <param name="grid"></param>
        /// <typeparam name="T"></typeparam>
        public void AddGrid<T>(Grid2D<T> grid) where T : IHeatMapCell
        {
            _grids.Add(grid);
            
            var newGrid = _grids[_grids.Count - 1] as Grid2D<T>;
            print(newGrid);

            print("boem: " + GetHeatInterface<T>(0, 0));

            // ICell iCell = grid.cells.Get(0);
            // IHeatMapCell iHeat = grid.cells.Get(0);
            //
            // Grid2D<IHeatMapCell> newGrid = grid as Grid2D<IHeatMapCell>;
            // print(newGrid); // returns null 
            // print(iHeat); //returns the object (HeatMapCellObject & OtherHeatMapCellObject)
            // print(iCell); //returns the object (HeatMapCellObject & OtherHeatMapCellObject)
        }

        /// <summary>
        /// adds one to the test value of a cell 
        /// </summary>
        /// <param name="index"></param>
        public void AddOneToTestValue(int gridIndex, int cellIndex)
        {
            
        }

        public ICell GetCellInterface<T>(int gridIndex, int cellIndex) where T : ICell
        {
            if (!_grids.ContainsSlot(gridIndex)) return null;
            Grid2D<T> grid = _grids.Get(gridIndex) as Grid2D<T>;
            if (grid == null) return null;
            if (!grid.cells.ContainsSlot(cellIndex)) return null;
            
            return grid.cells.Get(cellIndex) as ICell;
        }

        public IHeatMapCell GetHeatInterface<T>(int gridIndex, int cellIndex) where T : IHeatMapCell
        {
            if (!_grids.ContainsSlot(gridIndex)) return null;
            Grid2D<T> grid = _grids.Get(gridIndex) as Grid2D<T>;
            if (grid == null) return null;
            if (!grid.cells.ContainsSlot(cellIndex)) return null;

            grid.cells[0].TestValue = 999999;
            
            return grid.cells.Get(cellIndex) as IHeatMapCell;
        }
    }
}
