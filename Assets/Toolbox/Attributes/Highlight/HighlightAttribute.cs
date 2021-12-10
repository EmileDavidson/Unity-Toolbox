using UnityEngine;

namespace Toolbox.Attributes
{
    public class HighlightAttribute : PropertyAttribute
    {
        private Color color;
        public Color Color => color;

        public HighlightAttribute(float r=1, float g=0, float b=0, float a = 1) {
            color = new Color(r,g,b,a);
        }
        
        public HighlightAttribute(float r=1, float g=0, float b=0) {
            color = new Color(r,g,b,1);
        }
    
        public HighlightAttribute(string hex) {
            ColorUtility.TryParseHtmlString(hex, out color);
        }
    }
}