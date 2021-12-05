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
        [Min(0), SerializeReference] private int rowAmount;
        [Min(0), SerializeReference] private int columnAmount;
        [Min(0), SerializeReference] private int heightAmount;
        [SerializeReference] public UnityEvent onResetGrid = new UnityEvent();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rowAmount">how many rows does the grid have?</param>
        /// <param name="columnAmount">How many Columns does the grid have?</param>
        /// <param name="generate">do we want to generate in constructor if not you can use GenerateGrid() methode </param>
        public Grid3D(int rowAmount, int columnAmount, int heightAmount, bool generate = true)
        {
            this.rowAmount = rowAmount;
            this.columnAmount = columnAmount;
            this.heightAmount = heightAmount;
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

            for (int gridY = 0; gridY < heightAmount; gridY++)
            {
                for (int gridX = 0; gridX < rowAmount; gridX++)
                {
                    for (int gridZ = 0; gridZ < columnAmount; gridZ++)
                    {
                        int index = (rowAmount * heightAmount * gridZ) + (rowAmount * gridY) + gridX;
                        Debug.Log(index);
                        cells.Add((T)Activator.CreateInstance(typeof(T), new Vector3Int(gridX, gridY, gridZ), index));                 
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

        public int RowAmount
        {
            get => rowAmount;
            private set => rowAmount = value;
        }

        public int ColumnAmount
        {
            get => columnAmount;
            private set => columnAmount = value;
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
        public bool IsBorder(Cell2D cell, out BorderType type)
        {
            type = BorderType.NONE;
        
            if (cell.gridPosition.x == 0)
            {
                type = BorderType.BorderBottomLeft;
                return true;
            }
            if (cell.gridPosition.y == 0)
            {
                type = BorderType.BorderBottomTop;
                return true;
            }
            if (cell.gridPosition.y == RowAmount - 1)
            {
                type = BorderType.BorderBottomBottom;
                return true;
            }
            if (cell.gridPosition.x == ColumnAmount - 1)
            {
                type = BorderType.BorderBottomRight;
                return true;
            }

            return false;
        }
        
        public bool IsCorner(Cell2D cell, out CornerType type)
        {
            type = CornerType.NONE;
                
            if (cell.gridPosition.x == 0 && cell.gridPosition.y == 0)
            {
                type = CornerType.BottomUpLeft;
                return true;
            }
        
            if (cell.gridPosition.x == ColumnAmount - 1 && cell.gridPosition.y == 0)
            {
                type = CornerType.BottomUpRight;
                return true;
            }
        
            if (cell.gridPosition.x == ColumnAmount - 1 && cell.gridPosition.y == RowAmount - 1)
            {
                type = CornerType.BottomDownRight;
                return true;
            }
        
            if (cell.gridPosition.x == 0 && cell.gridPosition.y == RowAmount - 1)
            {
                type = CornerType.BottomDownLeft;
                return true;
            }
        
            return false;
        }
    }
}