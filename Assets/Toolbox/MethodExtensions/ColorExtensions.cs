using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class ColorExtensions
    {
        public static Color TryGetFromHex()
        {
            ColorUtility.TryParseHtmlString("#0AC742", out var color);
            return color;
        }
        
        public static Color TryGetFromRGB(){ return Color.red;}
        public static Color TryGetFromRGBA(){ return Color.red;}
        public static Color TryGetFromHSL(){ return Color.red;}
        public static Color TryGetFromHSV(){ return Color.red;}
        public static Color TryGetFromCMYK(){ return Color.red;}
        public static Color TryGetFromHSB(){ return Color.red;}
        public static Color TryGetFromHSI(){ return Color.red;}
        public static Color TryGetFromHWB(){ return Color.red;}
        public static Color TryGetFromNCol(){ return Color.red;}
        public static Color TryGetFromCIELAB(){ return Color.red;}
        public static Color TryGetFromCIEXYZ(){ return Color.red;}
    }
}
