/*******************************************************************************
 * iNethinkCMS - 网站内容管理系统
 * Copyright (C) 2012-2013 inethink.com
 * 
 * @author jackyang <69991000@qq.com>
 * @website http://cms.inethink.com
 * @version 1.3.6.0 (2013-08-14)
 * 
 * This is licensed under the GNU LGPL, version 3.0 or later.
 * For details, see: http://www.gnu.org/licenses/gpl-3.0.html
*******************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace iNethinkCMS.Helper
{
    public class Helper_Thumbnails
    {
        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="originalImagePath">原始图片路径</param>
        /// <param name="thumbnailPath">生成缩略图图片路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode">1,以最大高/宽，等比例缩放 2,缩放至指定高宽(可能变形) 3,以宽为标准,高自动适应进行缩放 4,以高为标准,宽自动适应进行缩放 5,以高宽为标准,进行裁切</param>
        /// <param name="mode">图片清晰度 1-100</param>
        public static void CreationThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, int quality)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int newWidth = width;
            int newHeight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (mode == "1")
            {
                if (height * ow > width * oh)
                {
                    mode = "3";
                }
                else
                {
                    mode = "4";
                }
            }
            switch (mode)
            {
                case "2":
                    break;
                case "3":
                    newHeight = originalImage.Height * width / originalImage.Width;
                    break;
                case "4":
                    newWidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "5":
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)newWidth / (double)newHeight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * newWidth / newHeight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / newWidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            System.Drawing.Image bitmap = new System.Drawing.Bitmap(newWidth, newHeight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);

            g.DrawImage(originalImage,
                new System.Drawing.Rectangle(0, 0, newWidth, newHeight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel
                );

            System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
            ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);
            System.Drawing.Imaging.ImageCodecInfo ici = GetImageEncodersInfo(Path.GetExtension(originalImagePath));

            try
            {
                bitmap.Save(thumbnailPath, ici, ep);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                ep.Dispose();
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 根据文件扩展名取得图片的编码信息
        /// </summary>
        /// <returns></returns>
        private static ImageCodecInfo GetImageEncodersInfo(string byExt)
        {
            switch (byExt)
            {
                case ".jpg":
                    return ImageCodecInfo.GetImageEncoders()[1];
                case ".gif":
                    return ImageCodecInfo.GetImageEncoders()[2];
                case ".png":
                    return ImageCodecInfo.GetImageEncoders()[4];
                case ".bmp":
                    return ImageCodecInfo.GetImageEncoders()[0];
                default:
                    return ImageCodecInfo.GetImageEncoders()[1];
            }
        }
    }
}
