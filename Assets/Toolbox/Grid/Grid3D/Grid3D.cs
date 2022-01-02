using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Toolbox.Grid
{
    [Serializable]
    public class Grid3D<T> where T : ICell
    {
        [field: SerializeReference] public List<T> cells = new List<T>();
        [SerializeReference] public UnityEvent onResetGrid = new UnityEvent();
        [SerializeReference] public Type cellType;

        [field: Min(0)]
        [field: SerializeReference]
        public int Width { get; private set; }

        [field: Min(0)]
        [field: SerializeReference]
        public int Height { get; private set; }

        [field: Min(0)]
        [field: SerializeReference]
        public int Depth { get; private set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zAmount"></param>
        /// <param name="generate">do we want to generate in constructor if not you can use GenerateGrid() methode </param>
        /// <param name="xAmount"></param>
        /// <param name="yAmount"></param>
        public Grid3D(int xAmount, int yAmount, int zAmount, bool generate = true)
        {
            this.Width = xAmount;
            this.Height = yAmount;
            this.Depth = zAmount;
            cellType = typeof(T);
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
            for (int gridZ = 0; gridZ < Depth; gridZ++)
            {
                for (int gridY = 0; gridY < Height; gridY++)
                {
                    for (int gridX = 0; gridX < Width; gridX++)
                    {
                        T cell = (T)Activator.CreateInstance(typeof(T));
                        cell.Index = cellIndex;
                        cell.GridPosition = new Vector3Int(gridX, gridY, gridZ);

                        cells.Add(cell);
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

        public Type CellType => cellType;

        //========== helping methods ===========
        public bool IsBorder(ICell cell3D, out BorderType type)
        {
            type = BorderType.NONE;

            if (cell3D.GridPosition.z == 0)
            {
                type = BorderType.front;
                return true;
            }

            if (cell3D.GridPosition.z == Depth - 1)
            {
                type = BorderType.back;
                return true;
            }

            if (cell3D.GridPosition.y == Height - 1)
            {
                type = BorderType.Top;
                return true;
            }

            if (cell3D.GridPosition.y == 0)
            {
                type = BorderType.Bottom;
                return true;
            }

            if (cell3D.GridPosition.x == 0)
            {
                type = BorderType.Left;
                return true;
            }

            if (cell3D.GridPosition.x == Width - 1)
            {
                type = BorderType.Right;
                return true;
            }

            return false;
        }

        public bool IsCorner(ICell cell3D, out CornerType type)
        {
            type = CornerType.NONE;
            var x = cell3D.GridPosition.x;
            var y = cell3D.GridPosition.y;
            var z = cell3D.GridPosition.z;

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

            if (x == Width - 1 && y == 0 && z == Depth - 1)
            {
                type = CornerType.TopRight;
                return true;
            }

            if (x == 0 && y == 0 && z == Depth - 1)
            {
                type = CornerType.TopLeft;
                return true;
            }
            
            //3d
            if (x == 0 && y == Height - 1 && z == 0)
            {
                type = CornerType.TopBottomLeft;
                return true;
            }

            if (x == Width - 1 && y == Height - 1 && z == 0)
            {
                type = CornerType.TopBottomRight;
                return true;
            }

            if (x == Width - 1 && y == Height - 1 && z == Depth - 1)
            {
                type = CornerType.TopTopRight;
                return true;
            }

            if (x == 0 && y == Height - 1 && z == Depth - 1)
            {
                type = CornerType.TopTopLeft;
                return true;
            }
            
            return false;
        }
    }
}