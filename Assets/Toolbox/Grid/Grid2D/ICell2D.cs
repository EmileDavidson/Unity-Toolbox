using System;
using UnityEngine;

namespace Toolbox.Grid
{
    public interface ICell2D : ICell
    {
#pragma warning disable 657
        [field: SerializeReference] public Vector2Int GridPosition { get; set; }
#pragma warning restore 657
    }
}