using Toolbox.Grid;

public static class GridHelper
{
    public static bool IsBorder(ICell cell, out BorderType type, int gridWidth, int gridHeight, int gridDepth = 0)
    {
        type = BorderType.NONE;

        if (cell.GridPosition.z == 0)
        {
            type = BorderType.front;
            return true;
        }

        if (cell.GridPosition.z == gridDepth - 1)
        {
            type = BorderType.back;
            return true;
        }

        if (cell.GridPosition.y == gridHeight - 1)
        {
            type = BorderType.Top;
            return true;
        }

        if (cell.GridPosition.y == 0)
        {
            type = BorderType.Bottom;
            return true;
        }

        if (cell.GridPosition.x == 0)
        {
            type = BorderType.Left;
            return true;
        }

        if (cell.GridPosition.x == gridWidth - 1)
        {
            type = BorderType.Right;
            return true;
        }

        return false;
    }

    public static bool IsCorner(ICell cell, out CornerType type, int gridWidth, int gridHeight, int gridDepth = 0)
    {
        type = CornerType.NONE;
        var x = cell.GridPosition.x;
        var y = cell.GridPosition.y;
        var z = cell.GridPosition.z;

        if (x == 0 && y == 0 && z == 0)
        {
            type = CornerType.BottomLeft;
            return true;
        }

        if (x == gridWidth - 1 && y == 0 && z == 0)
        {
            type = CornerType.BottomRight;
            return true;
        }

        if (x == gridWidth - 1 && y == 0 && z == gridDepth - 1)
        {
            type = CornerType.TopRight;
            return true;
        }

        if (x == 0 && y == 0 && z == gridDepth - 1)
        {
            type = CornerType.TopLeft;
            return true;
        }

        //3d
        if (x == 0 && y == gridHeight - 1 && z == 0)
        {
            type = CornerType.TopBottomLeft;
            return true;
        }

        if (x == gridWidth - 1 && y == gridHeight - 1 && z == 0)
        {
            type = CornerType.TopBottomRight;
            return true;
        }

        if (x == gridWidth - 1 && y == gridHeight - 1 && z == gridDepth - 1)
        {
            type = CornerType.TopTopRight;
            return true;
        }

        if (x == 0 && y == gridHeight - 1 && z == gridDepth - 1)
        {
            type = CornerType.TopTopLeft;
            return true;
        }

        return false;
    }
}