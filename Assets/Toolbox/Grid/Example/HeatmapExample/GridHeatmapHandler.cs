using System.Collections.Generic;
using System.Linq;
using Toolbox.Other;
using UnityEngine;

namespace Toolbox.Grid
{
    public class GridHeatmapHandler : MonoBehaviour
    {
        public static readonly MonoSingleton<GridHeatmapHandler> Singleton = new MonoSingleton<GridHeatmapHandler>();
        public List<object> grids = new List<object>();


        
    }
}
