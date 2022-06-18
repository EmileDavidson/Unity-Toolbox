using System.Collections.Generic;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Toolbox.Grid
{
    public class MonoGridHandler : MonoBehaviour
    {
        private readonly List<object> _grids = new List<object>();

        /// <summary>
        /// For adding grids to the list the grid generic has to have to IHeatMapCell interface
        /// </summary>
        /// <param name="grid"></param>
        /// <typeparam name="T"></typeparam>
        public virtual void AddGrid<T>(Grid2D<T> grid) where T : ICell
        {
            _grids.Add(grid);
        }
        
        public virtual ICell GetCell<T>(int gridIndex, int cellIndex) where T : ICell
        {
            if (!_grids.ContainsSlot(gridIndex)) return null;
            Grid2D<T> grid = _grids.Get(gridIndex) as Grid2D<T>;
            if (grid == null) return null;
            if (!grid.cells.ContainsSlot(cellIndex)) return null;
            
            return grid.cells.Get(cellIndex) as ICell;
        }
    }
}