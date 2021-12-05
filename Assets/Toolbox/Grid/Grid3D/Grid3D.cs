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
            for (int gridX = 0; gridX < xAmount; gridX++)
            {
                for (int gridY = 0; gridY < yAmount; gridY++)
                {
                    for (int gridZ = 0; gridZ < zAmount; gridZ++)
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
        public void IsBorder(){}
        public void IsCorner(){}
    }
}