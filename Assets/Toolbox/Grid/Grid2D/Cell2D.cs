using System;
using UnityEngine;

namespace Toolbox.Grid
{
    [Serializable]
    public class Cell2D : ICell
    {
        [SerializeReference] public Action onValueChange;
        [field: SerializeReference] public Vector3Int GridPosition { get; set; }
        [field: SerializeReference] public int Index { get; set; }

        Vector3Int ICell.GridPosition
        {
            get => this.GridPosition;
            set
            {
                this.GridPosition = value;
                onValueChange?.Invoke();
            }
        }
        
        int ICell.Index
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