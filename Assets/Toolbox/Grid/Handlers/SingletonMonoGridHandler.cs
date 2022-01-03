using System.Collections.Generic;
using Toolbox.MethodExtensions;
using Toolbox.Other;
using UnityEngine.Events;

namespace Toolbox.Grid
{
    public abstract class SingletonMonoGridHandler<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        public UnityEvent onAddGrid = new UnityEvent();
        public UnityEvent onRemoveGrid = new UnityEvent();
        
        
        protected readonly List<object> _grids = new List<object>();
        public int GridCount => _grids.Count;

        /// <summary>
        /// For adding grids to the list the grid generic has to have to IHeatMapCell interface
        /// this function can be overriden but the generic constraint stays
        /// </summary>
        /// <param name="grid"></param>
        /// <typeparam name="TU"></typeparam>
        /// <returns>returns if the grid was added or not</returns>
        public virtual bool AddGrid<TU>(Grid2D<TU> grid) where TU : ICell
        {
            _grids.Add(grid);
            return true;
        }

        /// <summary>
        /// For removing grids from the handler.
        /// </summary>
        /// <param name="grid"></param>
        /// <typeparam name="TU"></typeparam>
        /// <returns>returns if the grid was removed or not</returns>
        public virtual bool RemoveGrid<TU>(Grid2D<TU> grid) where TU : ICell
        {
            if (!_grids.Contains(grid)) return false;
            
            _grids.Remove(grid);
            return true;
        }
        
        /// <summary>
        /// Gets the ICell from given gridIndex en cellIndex 
        /// </summary>
        /// <param name="gridIndex"></param>
        /// <param name="cellIndex"></param>
        /// <typeparam name="TU"></typeparam>
        /// <returns></returns>
        public virtual ICell GetCell<TU>(int gridIndex, int cellIndex) where TU : ICell
        {
            if (!_grids.ContainsSlot(gridIndex)) return null;
            Grid2D<TU> grid = _grids.Get(gridIndex) as Grid2D<TU>;
            if (grid == null) return null;
            if (!grid.cells.ContainsSlot(cellIndex)) return null;
            
            return grid.cells.Get(cellIndex) as ICell;
        }
    }
}