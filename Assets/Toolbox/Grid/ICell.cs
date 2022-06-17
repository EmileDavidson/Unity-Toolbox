using UnityEngine;

namespace Toolbox.Grid
{
    public interface ICell
    {
#pragma warning disable 657
        [field: SerializeReference] public int Index { get; set; }
        [field: SerializeReference] public Vector3Int GridPosition { get; set; }
#pragma warning restore 657
    }
}