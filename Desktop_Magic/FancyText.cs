using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DM
{
    public class FancyText
    {
        /// <summary>
        /// Độ rộng của viền
        /// </summary>
        //private const int blurAmount = 5;

        /// <summary>
        /// Tạo ra ảnh từ văn bản
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="fnt"></param>
        /// <param name="clrFore"></param>
        /// <param name="clrBack"></param>
        /// <returns></returns>
        public static Image ImageFromText(string strText, Font fnt, Color clrFore, Color clrBack, int blurAmount)
        {
            Bitmap bmpOut = null;

            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                SizeF sz = g.MeasureString(strText, fnt);
                using (Bitmap bmp = new Bitmap((int)sz.Width, (int)sz.Height))
                using (Graphics gBmp = Graphics.FromImage(bmp))
                using (SolidBrush brBack = new SolidBrush(Color.FromArgb(16, clrBack.R, clrBack.G, clrBack.B)))
                using (SolidBrush brFore = new SolidBrush(clrFore))
                {
                    gBmp.SmoothingMode = SmoothingMode.HighQuality;
                    gBmp.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    gBmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    // ảnh chữ ở vị trí 0,0
                    gBmp.DrawString(strText, fnt, brBack, 0, 0);


                    bmpOut = new Bitmap(bmp.Width + blurAmount, bmp.Height + blurAmount);
                    using (Graphics gBmpOut = Graphics.FromImage(bmpOut))
                    {
                        gBmpOut.SmoothingMode = SmoothingMode.HighQuality;
                        gBmpOut.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        gBmpOut.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                        // Vẽ các ảnh chữ sát nhau để tạo viền
                        for (int x = 0; x <= blurAmount; x += 2)
                            for (int y = 0; y <= blurAmount; y += 2)
                                gBmpOut.DrawImageUnscaled(bmp, x, y);

                        // Vẽ chữ ở trong
                        gBmpOut.DrawString(strText, fnt, brFore, blurAmount / 2, blurAmount / 2);
                    }
                }
            }

            return bmpOut;
        }
    }
}
