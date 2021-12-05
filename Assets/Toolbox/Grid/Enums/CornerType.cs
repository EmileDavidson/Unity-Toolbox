namespace Toolbox.Grid.Enums
{
    /// <summary>
    /// list of all corners of 2d/3d grid {Layer: Bottom or top?} {side
    /// </summary>
    public enum CornerType
    {
        NONE,
        //2D
        BottomUpRight,
        BottomUpLeft,
        BottomDownRight,
        BottomDownLeft,
        //3d
        TopUpRight,
        TopUpLeft,
        TopDownRight,
        TopDownLeft,
    }
}