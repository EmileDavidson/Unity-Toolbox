using UnityEngine;
using System;
using System.Collections.Generic;
using Toolbox.Grid;
using UnityEngine.Events;

namespace Toolbox.Grid
{
    [Serializable]
    public class Grid2D<T> where T : ICell2D
    {
        [field: SerializeReference] public List<T> cells = new List<T>();
        [Min(0), SerializeReference] private int xAmount;
        [Min(0), SerializeReference] private int yAmount;
        [SerializeReference] public UnityEvent onResetGrid = new UnityEvent();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xAmount">how many rows does the grid have?</param>
        /// <param name="yAmount">How many Columns does the grid have?</param>
        /// <param name="generate">do we want to generate in constructor if not you can use GenerateGrid() methode </param>
        public Grid2D(int xAmount, int yAmount, bool generate = true)
        {
            this.xAmount = xAmount;
            this.yAmount = yAmount;
            if(generate) GenerateGrid();
        }

        /// <summary>
        /// resets the grid and creates new one with given type
        /// </summary>
        /// <typeparam name="T">Type of Cell2D as long as it derives from Cell2D it works.</typeparam>
        /// <returns></returns>
        public Grid2D<T> GenerateGrid()
        {
            ResetGrid();
            for (int gridX = 0; gridX < yAmount; gridX++)
            {
                for (int gridY = 0; gridY < xAmount; gridY++)
                {
                    int index = gridX + xAmount * gridY;

                    T cell = (T)Activator.CreateInstance(typeof(T));
                    cell.Index = index;
                    cell.GridPosition = new Vector2Int(gridX, gridY);
                    
                    cells.Add(cell);
                }
            }
            return this;
        }

        /// <summary>
        /// Resets the grid by clearing everything to default values.
        /// </summary>
        /// <returns></returns>
        public Grid2D<T> ResetGrid()
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
        public int width => xAmount;
        /// <summary>
        /// y axis
        /// </summary>
        public int height => yAmount;
        
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

        public T this[int i]
        {
            get => cells[i];
            set => cells[i] = value;
        }

        public Grid2D<T> ChainSetCell(int index, T value)
        {
            cells[index] = value;
            return this;
        }

        //========== helping methods ===========
        public bool IsBorder(Cell2D cell, out BorderType type)
        {
            type = BorderType.NONE;
        
            if (cell.GridPosition.x == 0)
            {
                type = BorderType.Left;
                return true;
            }
            if (cell.GridPosition.y == 0)
            {
                type = BorderType.Top;
                return true;
            }
            if (cell.GridPosition.y == xAmount - 1)
            {
                type = BorderType.Bottom;
                return true;
            }
            if (cell.GridPosition.x == yAmount - 1)
            {
                type = BorderType.Right;
                return true;
            }

            return false;
        }
        
        public bool IsCorner(Cell2D cell, out CornerType type)
        {
            type = CornerType.NONE;
                
            if (cell.GridPosition.x == 0 && cell.GridPosition.y == 0)
            {
                type = CornerType.TopLeft;
                return true;
            }
        
            if (cell.GridPosition.x == yAmount - 1 && cell.GridPosition.y == 0)
            {
                type = CornerType.TopRight;
                return true;
            }
        
            if (cell.GridPosition.x == yAmount - 1 && cell.GridPosition.y == xAmount - 1)
            {
                type = CornerType.BottomRight;
                return true;
            }
        
            if (cell.GridPosition.x == 0 && cell.GridPosition.y == xAmount - 1)
            {
                type = CornerType.BottomLeft;
                return true;
            }
        
            return false;
        }
    }
}