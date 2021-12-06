using UnityEngine;

namespace Toolbox.Grid.Grid3D.Cells
{
    public class Cell3D
    {
        [SerializeReference] public Vector3Int gridPosition;
        [SerializeReference] public int index;

        public Cell3D(Vector3Int gridPosition, int index)
        {
            this.gridPosition = gridPosition;
            this.index = index;
        }
    }
}