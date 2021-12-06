namespace Toolbox.Grid.Enums
{
    /// <summary>
    /// list of all corners of 2d/3d grid {Layer: Bottom or top?} {side
    /// </summary>
    public enum CornerType
    {
        NONE,
        //2D
        BottomLeft,
        BottomRight,
        TopLeft,
        TopRight,
        //3d
        TopBottomLeft,
        TopBottomRight,
        TopTopLeft,
        TopTopRight,
    }
}