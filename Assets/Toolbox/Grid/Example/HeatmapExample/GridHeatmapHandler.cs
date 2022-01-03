using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.MethodExtensions;
using Toolbox.Other;
using UnityEngine;

namespace Toolbox.Grid
{
    public class GridHeatmapHandler : SingletonMonoGridHandler<GridHeatmapHandler>
    {
        public override bool AddGrid<T>(Grid2D<T> grid)
        {
            if (!typeof(T).GetInterfaces().Contains(typeof(IHeatMapCell))) return false;
            _grids.Add(grid);
            return true;
        }

        public IHeatMapCell GetHeatCell<T>(int gridIndex, int cellIndex) where T : IHeatMapCell
        {
            if (!_grids.ContainsSlot(gridIndex)) return null;
            Grid2D<T> grid = _grids.Get(gridIndex) as Grid2D<T>;
            if (grid == null) return null;
            if (!grid.cells.ContainsSlot(cellIndex)) return null;

            return grid.cells.Get(cellIndex) as IHeatMapCell;
        }

        public void Damn()
        {
        }
    }
}