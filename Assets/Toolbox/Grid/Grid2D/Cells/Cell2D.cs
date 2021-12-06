using System;
using UnityEngine;

namespace Toolbox.Grid.Grid2D
{
    [Serializable]
    public class Cell2D
    {
        [SerializeReference] public Vector2Int gridPosition;
        [SerializeReference] public int index;

        public Cell2D(Vector2Int gridPosition, int index)
        {
            this.gridPosition = gridPosition;
            this.index = index;
        }
    }
}