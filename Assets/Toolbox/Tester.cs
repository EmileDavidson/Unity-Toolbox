using System.Collections.Generic;
using Toolbox.Grid.Grid2D;
using Toolbox.Grid.Grid3D.Cells;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Toolbox
{
    public class Tester : MonoBehaviour
    {
        private Grid3D<Cell3D> cell3dGrid = new Grid3D<Cell3D>(5, 5, 5);
        private List<GameObject> cubes = new List<GameObject>();

        public void Start()
        {
            // print(cell3dGrid);
            // print(cell3dGrid.cells);
            // print(cell3dGrid.cells.Count);
            // print(cell3dGrid.cells.Get(0));
            // print(cell3dGrid.cells.Get(0).GetType());
            //
            // print("--------------------");
            foreach (var VARIABLE in cell3dGrid.cells)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(
                    VARIABLE.gridPosition.x * 1.5f,
                    VARIABLE.gridPosition.y * 1.5f,
                    VARIABLE.gridPosition.z * 1.5f);

                cube.transform.name = "gridPos: " + VARIABLE.gridPosition + " Index: " + VARIABLE.index;
                cubes.Add(cube);
            }
        }

        [SerializeField] private int INDEX = 0;

        private void Update()
        {
            if (!cubes.ContainsSlot(INDEX)) return;

            foreach (var cube in cubes)
            {
                cube.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            }

            cubes[INDEX].GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);

            cell3dGrid.IsBorder(cell3dGrid[INDEX], out var type);
            print(type);

            cell3dGrid.IsCorner(cell3dGrid[INDEX], out var type2);
            print(type2);
        }
    }
}