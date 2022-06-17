using System.Collections.Generic;
using System.Linq;
using Toolbox.MethodExtensions;

namespace Toolbox.Grid
{
    public class GridHeatmapHandler : MonoSingletonGridHandler<GridHeatmapHandler>
    {
        public override bool AddGrid<T>(Grid2D<T> grid)
        {
            if (!typeof(T).GetInterfaces().Contains(typeof(ICell))) return false;
            if (!typeof(T).GetInterfaces().Contains(typeof(IHeatMapCell))) return false;
            
            //base.AddGrid(grid) handles all the adding im using this so I can't forget something
            base.AddGrid(grid);
            return true;
        }

        public IHeatMapCell GetHeatCell(int gridIndex, int cellIndex)
        {
            if (!gridsCellsList.ContainsSlot(gridIndex)) return null;
            List<IHeatMapCell> heatCellList = gridsCellsList.Get(gridIndex).ConvertListItemsTo<ICell, IHeatMapCell>();
            if (heatCellList == null) return null;
            if (!heatCellList.ContainsSlot(cellIndex)) return null;

            return heatCellList.Get(cellIndex);
        }
    }
}