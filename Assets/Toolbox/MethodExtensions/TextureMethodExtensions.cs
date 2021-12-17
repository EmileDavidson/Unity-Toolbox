using System.Runtime.CompilerServices;
using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class TextureMethodExtensions
    {
        public static Texture2D RotateTexture(this Texture2D originalTexture, bool clockwise = false)
        {
            var original = originalTexture.GetPixels32();
            var rotated = new Color32[original.Length];
            var w = originalTexture.width;
            var h = originalTexture.height;

            int iRotated;
            int iOriginal;
 
            for (var j = 0; j < h; ++j)
            {
                for (var i = 0; i < w; ++i)
                {
                    iRotated = (i + 1) * h - j - 1;
                    iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                    rotated[iRotated] = original[iOriginal];
                }
            }
 
            var rotatedTexture = new Texture2D(h, w);
            rotatedTexture.SetPixels32(rotated);
            rotatedTexture.Apply();

            return rotatedTexture;
        }
    }
}