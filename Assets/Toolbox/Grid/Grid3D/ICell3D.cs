using System;
using UnityEngine;

namespace Toolbox.Grid
{
    public interface ICell3D : ICell
    {
#pragma warning disable 657
        [field: SerializeReference] public Vector3Int GridPosition { get; set; }
#pragma warning restore 657
    }
}