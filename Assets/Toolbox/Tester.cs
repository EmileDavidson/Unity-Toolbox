using System;
using Toolbox.Attributes;
using Toolbox.Grid.Grid2D;
using Toolbox.Grid.Grid3D.Cells;
using UnityEngine;

namespace Toolbox
{
    public class Tester : MonoBehaviour
    {
        private Grid2D<Cell2D> grid = new Grid2D<Cell2D>(5,5);

        private void Awake()
        {
            print(grid.cells.Count);
            print(grid.cells[0].GetType());

            foreach (var VARIABLE in grid.cells)
            {
                print(VARIABLE.Index + " gridpos: " + VARIABLE.GridPosition);
            }
        }
    }
}