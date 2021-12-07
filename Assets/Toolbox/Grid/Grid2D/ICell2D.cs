using UnityEngine;

namespace Toolbox.Grid
{
    public interface ICell2D
    {
#pragma warning disable 657
        [field: SerializeReference] public Vector2Int GridPosition { get; set; }
        [field: SerializeReference] public int Index { get; set; }
#pragma warning restore 657
    }
}