using UnityEngine;

namespace Toolbox.Attributes
{
    public class DropdownAttribute : PropertyAttribute
    {
        private bool _dropped = false;
        public bool Dropped { get; set ; }

        public DropdownAttribute() { }

        public DropdownAttribute(bool arg)
        {
            this._dropped = arg;
        }
    }
}