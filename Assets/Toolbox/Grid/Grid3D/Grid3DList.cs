using System.Collections.Generic;

namespace Toolbox.Grid
{
    public class Grid3DList
    {
        public class Grid2DList<T> : List<Grid3D<T>> where T : ICell
        {
        
        }
    }
}