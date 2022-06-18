using UnityEngine;

namespace Toolbox.Required
{
    public static class Textures
    {
        #region textures

        private static Texture2D _hoverTexture2D;

        public static Texture2D HoverTexture2D
        {
            get
            {
                if (_hoverTexture2D != null) return _hoverTexture2D;

                ColorUtility.TryParseHtmlString("#444444", out var color);
                _hoverTexture2D = new Texture2D(1, 1);
                _hoverTexture2D.SetPixels(new Color[] { color });
                _hoverTexture2D.Apply();

                return _hoverTexture2D;
            }
        }

        private static Texture2D _defaultTexture2D;

        public static Texture2D DefaultTexture2D
        {
            get
            {
                if (_defaultTexture2D != null) return _defaultTexture2D;

                ColorUtility.TryParseHtmlString("#383838", out var color);
                _defaultTexture2D = new Texture2D(1, 1);
                _defaultTexture2D.SetPixels(new Color[] { color });
                _defaultTexture2D.Apply();

                return _defaultTexture2D;
            }
        }

        private static Texture2D _arrowDownTexture2D;

        //todo: ADD WHEN THE TEXTURE IS NOT FOUND IN ASSET PATH WE LOAD A ' BLANK 1PX TEXTURE ' THIS PREVENTS ERRORS 
        //LOG A WARNING IN THE CONSOLE THAT TEXTURE PATH WAS NOT FOUND AND RE-CHECK EVERY TIME ITS REQUESTED AGAIN 
        public static Texture2D ArrowDownTexture2D
        {
            get
            {
                var texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Toolbox/Required/Resources/icons/small/icon dropdown.png");
                _arrowDownTexture2D = texture;

                return _arrowDownTexture2D;
            }
        }
        
        private static Texture2D _arrowRightTexture2D;

        public static Texture2D ArrowRightTexture2D
        {
            get
            {
                if (_arrowRightTexture2D != null) return _arrowRightTexture2D;
                var texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Toolbox/Required/Resources/icons/small/icon dropdown.png");
                texture = texture.RotateTexture();
                _arrowRightTexture2D = texture;
                

                return _arrowRightTexture2D;
            }
        }


        #endregion
    }
}