using System;
using UnityEngine;

namespace Toolbox.Grid
{
    [Serializable]
    public class Cell2D : ICell2D
    {
        [SerializeReference] public Action onValueChange;
        [field: SerializeReference] public Vector2Int GridPosition { get; set; }
        [field: SerializeReference] public int Index { get; set; }

        Vector2Int ICell2D.GridPosition
        {
            get => this.GridPosition;
            set
            {
                this.GridPosition = value;
                onValueChange?.Invoke();
            }
        }
        
        int ICell2D.Index
        {
            get => this.Index;
            set
            {
                this.Index = value;
                onValueChange?.Invoke();
            }
        }

    }
}