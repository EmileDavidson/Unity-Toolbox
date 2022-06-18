using System;
using System.Collections.Generic;
using Toolbox.Other;
using Toolbox.Required;
using UnityEngine.Events;

namespace Toolbox.Grid
{
    public abstract class MonoSingletonGridHandler<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        public UnityEvent onAddGrid = new UnityEvent();
        public UnityEvent onRemoveGrid = new UnityEvent();
        
        [ObsoleteAttribute("This property is not recommended. please use gridsCellsList if you want to get the cells and objects")]
        protected readonly List<object> grids = new List<object>();
        protected readonly List<List<ICell>> gridsCellsList = new List<List<ICell>>();
        public int GridCount => gridsCellsList.Count;

        /// <summary>
        /// For adding grids to the list the grid generic has to have to IHeatMapCell interface
        /// this function can be overriden but the generic constraint stays
        /// </summary>
        /// <param name="grid"></param>
        /// <typeparam name="TU"></typeparam>
        /// <returns>returns if the grid was added or not</returns>
        public virtual bool AddGrid<TU>(Grid2D<TU> grid) where TU : ICell
        {
            grids.Add(grid);
            gridsCellsList.Add(grid.cells.ConvertListItemsTo<TU, ICell>());
            
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
            if (!gridsCellsList.Contains(grid.cells.ConvertListItemsTo<TU, ICell>())) return false;
            gridsCellsList.Remove(grid.cells.ConvertListItemsTo<TU, ICell>());
            return true;
        }
        
        /// <summary>
        /// Gets the ICell from given gridIndex en cellIndex 
        /// </summary>
        /// <param name="gridIndex"></param>
        /// <param name="cellIndex"></param>
        /// <typeparam name="TU"></typeparam>
        /// <returns></returns>
        // public virtual ICell GetCell<TU>(int gridIndex, int cellIndex) where TU : ICell
        // {
        //     if (!grids.ContainsSlot(gridIndex)) return null;
        //     Grid2D<TU> grid = grids.Get(gridIndex) as Grid2D<TU>;
        //     if (grid == null) return null;
        //     if (!grid.cells.ContainsSlot(cellIndex)) return null;
        //     
        //     return grid.cells.Get(cellIndex) as ICell;
        // }
        
        public ICell GetCell(int gridIndex, int cellIndex)
        {
            if (!gridsCellsList.ContainsSlot(gridIndex)) return null;
            List<ICell> cellList = gridsCellsList.Get(gridIndex);
            if (cellList == null) return null;
            if (!cellList.ContainsSlot(cellIndex)) return null;

            return cellList.Get(cellIndex);
        }
    }
}