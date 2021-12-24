﻿using System;
using UnityEngine;

namespace Toolbox.Grid
{
    public interface ICell3D
    {
#pragma warning disable 657
        [field: SerializeReference] public Vector3Int GridPosition { get; set; }
        [field: SerializeReference] public int Index { get; set; }
#pragma warning restore 657
    }
}