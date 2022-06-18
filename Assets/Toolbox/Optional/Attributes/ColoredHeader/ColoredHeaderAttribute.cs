using UnityEngine;

namespace Toolbox.Attributes
{
    public class ColoredHeaderAttribute : PropertyAttribute { 
        private Color color;
        public Color Color => color;

        private string label;
        public string Label => label;

        public ColoredHeaderAttribute(string text, float r=1, float g=0, float b=0, float a = 1) {
            this.color = new Color(r,g,b,a);
            this.label = text;
        }
    
        public ColoredHeaderAttribute(string text, float r=1, float g=0, float b=0) {
            this.color = new Color(r,g,b,1);
            this.label = text;
        }
        
        public ColoredHeaderAttribute(string text, string hex) {
            ColorUtility.TryParseHtmlString(hex, out color);
            this.label = text;
        }
    }
}