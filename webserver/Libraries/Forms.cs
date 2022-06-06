using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WebServer.Libraries
{
    static class Forms
    {
        private static Dictionary<int, int> CONTENT_WIDTH = new Dictionary<int, int> { };

        /// <summary>
        /// Set gradient for form background
        /// </summary>
        public static void SetGradientBackground(Control control)
        {
            control.Paint += new PaintEventHandler(
                (object sender, PaintEventArgs e) => {
                    Graphics graphics = e.Graphics;

                    //the rectangle, the same size as our Form
                    Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);

                    //define gradient's properties
                    Brush b = new LinearGradientBrush(rect, Color.FromArgb(255, 255, 255), Color.FromArgb(210, 210, 210), 65f);

                    //apply gradient         
                    graphics.FillRectangle(b, rect);
                }
            );
        }

        /// <summary>
        /// Change font family for all controls in form using font name
        /// </summary>
        public static void ChangeFont(Control control, string font_family, Dictionary<float, float> size_mapping = null)
        {
            ChangeFont(control, new FontFamily(font_family), size_mapping);
        }

        /// <summary>
        /// Change font family for all controls in form using FontFamily
        /// </summary>
        public static void ChangeFont(Control control, FontFamily font_family, Dictionary<float, float> size_mapping = null)
        {
            foreach (Control c in control.Controls)
            {
                float size = size_mapping != null && size_mapping.ContainsKey(c.Font.Size) ? size_mapping[c.Font.Size] : c.Font.Size;
                c.Font = new Font(font_family, size, c.Font.Style);

                if (c.Controls.Count > 0)
                {
                    ChangeFont(c, font_family, size_mapping);
                }
            }
        }

        /// <summary>
        /// Auto fit controls width inside a container
        /// </summary>
        /// <param name="container"></param>
        public static void FitControlsWidth(ScrollableControl container)
        {
            int hash_code = container.GetHashCode();

            container.Padding = new Padding(0);

            if (!CONTENT_WIDTH.ContainsKey(hash_code))
            {
                CONTENT_WIDTH.Add(hash_code, 0);
            }

            container.ClientSizeChanged += new System.EventHandler((object s, System.EventArgs e) =>
            {
                container.SuspendLayout();

                if (CONTENT_WIDTH[hash_code] == container.ClientSize.Width)
                {
                    return;
                }

                foreach (Control ctl in container.Controls)
                {
                    ctl.Margin = new Padding(0);
                    ctl.Width = container.ClientSize.Width;
                }

                CONTENT_WIDTH[hash_code] = container.ClientSize.Width;

                container.ResumeLayout();
            });
        }

        /// <summary>
        /// Check control invoke state and do action
        /// </summary>
        public static void InvokeControl(Control control, Action f)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new MethodInvoker(delegate ()
                {
                    f();
                }));
            }
            else
            {
                f();
            }
        }

        /// <summary>
        /// Keep focus on a control until invisible
        /// </summary>
        public static void PreventLostFocus(Control control)
        {
            control.LostFocus += new EventHandler((object s, EventArgs e) =>
            {
                Control c = (Control)s;

                if (c != null && c.Visible)
                {
                    c.Focus();
                }
            });
        }
    }
}
