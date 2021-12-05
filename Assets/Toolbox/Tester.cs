using System;
using Toolbox.Grid.Grid2D;
using Toolbox.Grid.Grid2D.Cells;
using Toolbox.Grid.Grid3D.Cells;
using Toolbox.MethodExtensions;
using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public class Tester : MonoBehaviour
    {
        private Grid3D<Cell3D> cell3dGrid = new Grid3D<Cell3D>(5,5, 5);
        public void Test()
        {
            print(cell3dGrid);
            print(cell3dGrid.cells);
            print(cell3dGrid.cells.Count);
            print(cell3dGrid.cells.Get(0));
            print(cell3dGrid.cells.Get(0).GetType());
            
            print("--------------------");
            foreach (var VARIABLE in cell3dGrid.cells)
            {
                print(VARIABLE.gridPosition);
                Debug.Log("INDEX: " + VARIABLE.index);
            }
        }
    }
}