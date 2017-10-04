/* Got conversion algorithms here:
 *  http://130.113.54.154/~monger/hsl-rgb.html
 * 
 * Let me know if the site ever goes down. I saved it just to be safe
 * as this info is somehow hard to find in such a simple form.
 */

using System;
using System.Drawing;

namespace MushROMs
{
    /// <summary>
    /// Can represent colors as RGB or HSL.
    /// </summary>
    public sealed class ExpandedColor
    {
        /// <summary>
        /// Represents a color that is null.
        /// </summary>
        public static readonly ExpandedColor Empty = new ExpandedColor(Color.Empty.A, Color.Empty.R, Color.Empty.G, Color.Empty.B, false);

        private float alpha;
        private float red;
        private float green;
        private float blue;
        private float hue;
        private float sat;
        private float lum;

        /// <summary>
        /// Gets or sets the Alpha component of the current ExpandedColor. (Note: value must be between 0.0 and 1.0)
        /// </summary>
        public float A
        {
            get
            {
                return this.alpha;
            }
            set
            {
                this.alpha = value;
            }
        }
        /// <summary>
        /// Gets or sets the Red component of the current ExpandedColor. (Note: value must be between 0.0 and 1.0)
        /// </summary>
        public float R
        {
            get
            {
                return this.red;
            }
            set
            {
                this.red = value;
                CalculateHSL();
            }
        }
        /// <summary>
        /// Gets ot sets the Green component of the current ExpandedColor. (Note: value must be between 0.0 and 1.0)
        /// </summary>
        public float G
        {
            get
            {
                return this.green;
            }
            set
            {
                this.green = value;
                CalculateHSL();
            }
        }
        /// <summary>
        /// Gets or sets the Blue component of the current ExpandedColor. (Note: value must be between 0.0 and 1.0)
        /// </summary>
        public float B
        {
            get
            {
                return this.blue;
            }
            set
            {
                this.blue = value;
                CalculateHSL();
            }
        }
        /// <summary>
        /// Gets or sets the Hue component of the current ExpandedColor. (Note: value must be between 0.0 and 1.0)
        /// </summary>
        public float H
        {
            get
            {
                return this.hue;
            }
            set
            {
                this.hue = value;
                CalculateRGB();
            }
        }
        /// <summary>
        /// Gets or sets the Saturation component of the current ExpandedColor. (Note: value must be between 0.0 and 1.0)
        /// </summary>
        public float S
        {
            get
            {
                return this.sat;
            }
            set
            {
                this.sat = value;
                CalculateRGB();
            }
        }
        /// <summary>
        /// Gets or sets the Luminosity component of the current ExpandedColor. (Note: value must be between 0.0 and 1.0)
        /// </summary>
        public float L
        {
            get
            {
                return this.lum;
            }
            set
            {
                this.lum = value;
                CalculateRGB();
            }
        }

        private ExpandedColor(float a, float r, float g, float b, bool hsl)
        {
            this.alpha = a;
            if (hsl)
            {
                this.hue = r;
                this.sat = g;
                this.lum = b;
                CalculateRGB();
            }
            else
            {
                this.red = r;
                this.green = g;
                this.blue = b;
                CalculateHSL();
            }
        }

        private void CalculateHSL()
        {
            float r = this.red;
            float g = this.green;
            float b = this.blue;

            float max = r > g ? r : g;
            max = max > b ? max : b;
            float min = r < g ? r : g;
            min = min < b ? min : b;

            float l = (max + min) / 2.0f;
            float s = 0.0f, h = 0.0f;

            if (max != min)
            {
                if (l < 0.5f)
                    s = (max - min) / (max + min);
                else
                    s = (max - min) / (2.0f - (max + min));

                if (max == r)
                    h = (g - b) / (max - min);
                else if (max == g)
                    h = 2.0f + (b - r) / (max - min);
                else
                    h = 4.0f + (r - g) / (max - min);

                if (h < 0.0)
                    while (h < 0.0f)
                        h += 1.0f;
                else if (h >= 1.0f)
                    while (h >= 1.0f)
                        h -= 1.0f;
            }

            this.hue = h;
            this.sat = s;
            this.lum = l;
        }

        private void CalculateRGB()
        {
            float h = this.hue;
            float s = this.sat;
            float l = this.lum;

            float r = 0.0f, g = 0.0f, b = 0.0f;

            if (l > 0.0f)
            {
                float x, y, z;

                if (l < 0.5f)
                    y = l * (1.0f + s);
                else
                    y = l + s - (l * s);

                x = (2.0f * l) - y;

                z = h + (1.0f / 3.0f);
                if (z > 1.0f)
                    z -= 1.0f;
                r = GetColorComponent(x, y, z);

                z = h;
                g = GetColorComponent(x, y, z);

                z = h - (1.0f / 3.0f);
                if (z < 0.0f)
                    z += 1.0f;
                b = GetColorComponent(x, y, z);
            }

            this.red = r;
            this.green = g;
            this.blue = b;
        }

        private float GetColorComponent(float x, float y, float z)
        {
            if (6.0f * z < 1.0f)
                return x + ((y - x) * 6.0f * z);
            else if (2.0f * z < 1.0f)
                return y;
            else if (3.0f * z < 2.0f)
                return x + ((y - x) * ((2.0f / 3.0f) - z) * 6.0f);
            else
                return x;
        }

        /// <summary>
        /// Rotates the ExpandedColor's hue by a defined rotation (in degrees).
        /// </summary>
        /// <param name="degrees">
        /// The angle of rotation (in degrees) to rotate the hue.
        /// </param>
        public void RotateColor(float degrees)
        {
            float h = this.hue * 360.0f;
            h += degrees;
            while (h < 0.0)
                h += 360.0f;
            while (h >= 360.0f)
                h -= 360.0f;
            this.hue = h / 360.0f;
            CalculateRGB();
        }

        /// <summary>
        /// Creates an ExpandedColor from a System.Drawing.Color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ExpandedColor FromColor(Color color)
        {
            return color;
        }

        /// <summary>
        /// Creates an ExpandedColor from given ARGB values.
        /// </summary>
        /// <param name="alpha">
        /// The alpha value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="red">
        /// The red value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="green">
        /// The green value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="blue">
        /// The blue value (must be between 0.0 and 1.0).
        /// </param>
        /// <returns>
        /// An ExpandedColor from the given ARGB values.
        /// </returns>
        public static ExpandedColor FromARGB(float alpha, float red, float green, float blue)
        {
            return new ExpandedColor(alpha, red, green, blue, false);
        }

        /// <summary>
        /// Creates an ExpandedColor from given RGB values.
        /// </summary>
        /// <param name="red">
        /// The red value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="green">
        /// The green value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="blue">
        /// The blue value (must be between 0.0 and 1.0).
        /// </param>
        /// <returns>
        /// An ExpandedColor from the given RGB values.
        /// </returns>
        public static ExpandedColor FromRGB(float red, float green, float blue)
        {
            return new ExpandedColor(1.0f, red, green, blue, false);
        }

        /// <summary>
        /// Creates an ExpandedColor from given AHSL values.
        /// </summary>
        /// <param name="alpha">
        /// The alpha value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="hue">
        /// The hue value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="sat">
        /// The saturation value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="lum">
        /// The luminosity value (must be between 0.0 and 1.0).
        /// </param>
        /// <returns>
        /// An ExpandedColor from the given AHSL values.
        /// </returns>
        public static ExpandedColor FromAHSL(float alpha, float hue, float sat, float lum)
        {
            return new ExpandedColor(alpha, hue, sat, lum, true);
        }

        /// <summary>
        /// Creates an ExpandedColor from given HSL values.
        /// </summary>
        /// <param name="hue">
        /// The hue value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="sat">
        /// The saturation value (must be between 0.0 and 1.0).
        /// </param>
        /// <param name="lum">
        /// The luminosity value (must be between 0.0 and 1.0).
        /// </param>
        /// <returns>
        /// An ExpandedColor from the given HSL values.
        /// </returns>
        public static ExpandedColor FromHSL(float hue, float sat, float lum)
        {
            return new ExpandedColor(1.0f, hue, sat, lum, true);
        }

        /// <summary>
        /// Creates an ExpandedColor from the specified predefined color.
        /// </summary>
        /// <param name="color">
        /// A System.Drawing.KnwonColor to use
        /// </param>
        /// <returns>
        /// An ExpandedColor from the specified predefined color.
        /// </returns>
        public static ExpandedColor FromKnownColor(KnownColor color)
        {
            return Color.FromKnownColor(color);
        }

        /// <summary>
        /// Creates an ExpandedColor from the specified name of a predefined color.
        /// </summary>
        /// <param name="name">
        /// A string that is the name of the predefined color.
        /// </param>
        /// <returns></returns>
        public static ExpandedColor FromName(string name)
        {
            return Color.FromName(name);
        }

        /// <summary>
        /// Returns a System.Drawing.Color that is equivalent to the ExpandedColor.
        /// </summary>
        /// <returns>
        /// A System.Drawing.Color equivalent to the ExpandedColor.
        /// </returns>
        public Color ToColor()
        {
            return (Color)this;
        }

        /// <summary>
        /// Gets the System.Drawing.KnownColor value of the ExpandedColor.
        /// </summary>
        /// <returns>
        /// A System.Drawing.KnownColor that is equivalent to the ExpandedColor.
        /// </returns>
        public KnownColor ToKnownColor()
        {
            return ((Color)this).ToKnownColor();
        }

        /// <summary>
        /// Inverts the ExpandedColor.
        /// </summary>
        public void Invert()
        {
            this.red = 1.0f - this.red;
            this.green = 1.0f - this.green;
            this.blue = 1.0f - this.blue;
            CalculateHSL();
        }

        /// <summary>
        /// Determines the ExpandedColor that will have the optimum contrast with the current ExpandedColor.
        /// </summary>
        /// <returns>
        /// An ExpandedColor with the maximum contrast with the current ExpandedColor.
        /// </returns>
        public ExpandedColor GetMaximumContrast()
        {
            ExpandedColor x = new ExpandedColor(this.alpha, this.hue, this.sat, this.lum, true);
            float l = x.lum;
            if (l < 0.5f)
                x.lum = 1.0f;
            else
                x.lum = 0.0f;
            x.CalculateRGB();
            return x;
        }

        public static implicit operator Color(ExpandedColor x)
        {
            return Color.FromArgb((int)(x.alpha * 255.0f), (int)(x.red * 255.0f), (int)(x.green * 255.0f), (int)(x.blue * 255.0f));
        }

        public static implicit operator ExpandedColor(Color c)
        {
            return new ExpandedColor(c.A / 255.0f, c.R / 255.0f, c.G / 255.0f, c.B / 255.0f, false);
        }
    }
}