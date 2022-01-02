using System;
using Toolbox.Grid;
using UnityEngine;

namespace Toolbox.Grid
{
    public class Cell3D : ICell
    {
        [SerializeReference] private Action _onValueChange;
        [field: SerializeReference] private Vector3Int GridPosition { get; set; }
        public int TestValue { get; set; }
        [field: SerializeReference] private int Index { get; set ; }


        Vector3Int ICell.GridPosition
        {
            get => this.GridPosition;
            set
            {
                this.GridPosition = value;
                _onValueChange?.Invoke();
            }
        }
        
        int ICell.Index
        {
            get => this.Index;
            set
            {
                this.Index = value;
                _onValueChange?.Invoke();
            }
        }
    }
}