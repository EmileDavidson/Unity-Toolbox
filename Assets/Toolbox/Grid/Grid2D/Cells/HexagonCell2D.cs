using UnityEngine;

namespace Toolbox.Grid.Grid2D.Cells
{
    public abstract class HexagonCell2D : Cell2D
    {
        [SerializeReference] private GameObject[] _wallsObjects = new GameObject[6];
        [SerializeReference] private bool[] _walls = new bool[6];
        
        [SerializeReference] private GameObject _myGameObject = null;
        [SerializeReference] private Vector3 _position = Vector3.zero;
        
        [SerializeReference] private Vector2Int _gridPosition = Vector2Int.zero;
        [SerializeReference] private int _index = -1;
        
        //base constructor
        protected HexagonCell2D(Vector2Int gridPosition, int index, GameObject myGameObject) : base(gridPosition, index)
        {
        }
        
        //=========== GETTERS && SETTERS ===========

        public Vector2Int GridPosition
        {
            get => _gridPosition;
            set => _gridPosition = value;
        }

        public int Index
        {
            get => _index;
            set => _index = value;
        }

        public GameObject MyGameObject
        {
            get => _myGameObject;
            set => _myGameObject = value;
        }

        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }
        
        //========== helping methods =========
        
    }
}