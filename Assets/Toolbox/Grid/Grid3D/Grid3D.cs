using UnityEngine;
using System;
using System.Collections.Generic;
using Toolbox.Grid.Enums;
using Toolbox.Grid.Grid3D.Cells;
using UnityEngine.Events;

namespace Toolbox.Grid.Grid2D
{
    [Serializable]
    public class Grid3D<T> where T : Cell3D
    {
        [field: SerializeReference] public List<T> cells = new List<T>();
        [Min(0), SerializeReference] private int xAmount;
        [Min(0), SerializeReference] private int yAmount;
        [Min(0), SerializeReference] private int zAmount;
        [SerializeReference] public UnityEvent onResetGrid = new UnityEvent();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rowAmount">how many rows does the grid have?</param>
        /// <param name="columnAmount">How many Columns does the grid have?</param>
        /// <param name="generate">do we want to generate in constructor if not you can use GenerateGrid() methode </param>
        public Grid3D(int xAmount, int yAmount, int zAmount, bool generate = true)
        {
            this.xAmount = xAmount;
            this.yAmount = yAmount;
            this.zAmount = zAmount;
            if(generate) GenerateGrid();
        }

        /// <summary>
        /// resets the grid and creates new one with given type
        /// </summary>
        /// <typeparam name="T">Type of Cell2D as long as it derives from Cell2D it works.</typeparam>
        /// <returns></returns>
        public Grid3D<T> GenerateGrid()
        {
            ResetGrid();

            int cellIndex = 0;    
            for (int gridZ = 0; gridZ < zAmount; gridZ++)
            {
                for (int gridY = 0; gridY < yAmount; gridY++)
                {
                    for (int gridX = 0; gridX < xAmount; gridX++)
                    {
                        cells.Add((T)Activator.CreateInstance(typeof(T), new Vector3Int(gridX, gridY, gridZ), cellIndex));
                        cellIndex++;
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Resets the grid by clearing everything to default values.
        /// </summary>
        /// <returns></returns>
        public Grid3D<T> ResetGrid()
        {
            onResetGrid.Invoke();   
            cells.Clear();
            return this;
        }
        
        //========== getters && Setters ===========
        public List<T> Cells => cells;

        /// <summary>
        /// x axis
        /// </summary>
        public int Width => xAmount;
        
        /// <summary>
        /// y axis
        /// </summary>
        public int height => yAmount;
        /// <summary>
        /// z axis
        /// </summary>
        public int lenght  => zAmount;

        public int XAmount
        {
            get => xAmount;
            private set => xAmount = value;
        }

        public int YAmount
        {
            get => yAmount;
            private set => yAmount = value;
        }

        public int ZAmount
        {
            get => zAmount;
            private set => zAmount = value;
        }

        public T this[int i]
        {
            get => cells[i];
            set => cells[i] = value;
        }

        public Grid3D<T> ChainSetCell(int index, T value)
        {
            cells[index] = value;
            return this;
        }

        //========== helping methods ===========
        public bool IsBorder(Cell3D cell3D, out BorderType type)
        {
            type = BorderType.NONE;

            if (cell3D.gridPosition.z == 0)
            {
                type = BorderType.front;
                return true;
            }

            if (cell3D.gridPosition.z == lenght - 1)
            {
                type = BorderType.back;
                return true;
            }

            if (cell3D.gridPosition.y == height - 1)
            {
                type = BorderType.Top;
                return true;
            }

            if (cell3D.gridPosition.y == 0)
            {
                type = BorderType.Bottom;
                return true;
            }

            if (cell3D.gridPosition.x == 0)
            {
                type = BorderType.Left;
                return true;
            }

            if (cell3D.gridPosition.x == Width - 1)
            {
                type = BorderType.Right;
                return true;
            }

            return false;
        }

        public bool IsCorner(Cell3D cell3D, out CornerType type)
        {
            type = CornerType.NONE;
            var x = cell3D.gridPosition.x;
            var y = cell3D.gridPosition.y;
            var z = cell3D.gridPosition.z;

            if (x == 0 && y == 0 && z == 0)
            {
                type = CornerType.BottomLeft;
                return true;
            }

            if (x == Width - 1 && y == 0 && z == 0)
            {
                type = CornerType.BottomRight;
                return true;
            }

            if (x == Width - 1 && y == 0 && z == lenght - 1)
            {
                type = CornerType.TopRight;
                return true;
            }

            if (x == 0 && y == 0 && z == lenght - 1)
            {
                type = CornerType.TopLeft;
                return true;
            }
            
            //3d
            if (x == 0 && y == height - 1 && z == 0)
            {
                type = CornerType.TopBottomLeft;
                return true;
            }

            if (x == Width - 1 && y == height - 1 && z == 0)
            {
                type = CornerType.TopBottomRight;
                return true;
            }

            if (x == Width - 1 && y == height - 1 && z == lenght - 1)
            {
                type = CornerType.TopTopRight;
                return true;
            }

            if (x == 0 && y == height - 1 && z == lenght - 1)
            {
                type = CornerType.TopTopLeft;
                return true;
            }
            
            return false;
        }
    }
}