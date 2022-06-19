﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.Attributes;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace Toolbox.Grid
{
    [Serializable]
    public class Grid2D<T> where T : ICell
    {
        [field: SerializeReference] public List<T> cells = new List<T>();
        [SerializeReference] public UnityEvent onResetGrid = new UnityEvent();
        [SerializeReference] private Vector2 pivotPoint = new Vector2(20, 20);

        [field: Min(0)]
        [field: SerializeReference]
        public int Width { get; }

        [field: Min(0)]
        [field: SerializeReference]
        public int Height { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xAmount">how many rows does the grid have?</param>
        /// <param name="yAmount">How many Columns does the grid have?</param>
        /// <param name="generate">do we want to generate in constructor if not you can use GenerateGrid() methode </param>
        public Grid2D(int xAmount, int yAmount, bool generate = true)
        {
            this.Width = xAmount;
            this.Height = yAmount;
            if (generate) GenerateGrid();
        }

        /// <summary>
        /// resets the grid and creates new one with given type
        /// </summary>
        /// <typeparam name="T">Type of Cell2D as long as it derives from Cell2D it works.</typeparam>
        /// <returns></returns>
        public Grid2D<T> GenerateGrid()
        {
            ResetGrid();
            for (int gridX = 0; gridX < Height; gridX++)
            {
                for (int gridY = 0; gridY < Width; gridY++)
                {
                    int index = gridX + Width * gridY;

                    T cell = (T)Activator.CreateInstance(typeof(T));
                    cell.Index = index;
                    cell.GridPosition = new Vector3Int(gridX, gridY, 0);

                    cells.Add(cell);
                }
            }
            LineDrawer();
            return this;
        }

        public void LineDrawer()
        {
            foreach (var cell in cells)
            {
                var gridX = cell.GridPosition.x;
                var gridY = cell.GridPosition.y;
                
                Debug.DrawLine(new Vector3(gridX + pivotPoint.x, gridY + pivotPoint.y, 0),
                    new Vector3(gridX + pivotPoint.x, gridY + 1 + +pivotPoint.y, 0), Color.red, int.MaxValue);
                Debug.DrawLine(new Vector3(gridX + pivotPoint.x, gridY + pivotPoint.y, 0),
                    new Vector3(gridX + 1 + pivotPoint.x, gridY + pivotPoint.y, 0), Color.red, int.MaxValue);
            }
            
            Debug.DrawLine(new Vector3(0 + pivotPoint.x, Cells.Last().GridPosition.y + 1 + pivotPoint.y, 0),
                new Vector3(cells.Last().GridPosition.x + 1 + pivotPoint.x, cells.Last().GridPosition.y + 1 + pivotPoint.y, 0), Color.red, 
                int.MaxValue);
            Debug.DrawLine(
                new Vector3(cells.Last().GridPosition.x + 1 + pivotPoint.x, Cells.Last().GridPosition.y + 1 + pivotPoint.y, 0),
                new Vector3(cells.Last().GridPosition.x + 1 + pivotPoint.x, 0 + pivotPoint.y, 0), Color.red, 
                int.MaxValue);
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

        public Type CellType => typeof(T);

        //========== helping methods ===========
        public bool IsBorder(Cell2D cell, out BorderType type)
        {
            return GridHelper.IsBorder(cell, out type, Width, Height);
        }

        public bool IsCorner(Cell2D cell, out CornerType type)
        {
            return GridHelper.IsCorner(cell, out type, Width, Height);
        }
    }
}