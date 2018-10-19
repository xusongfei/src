using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Runtime.InteropServices;

namespace Lead.Detect.Helper
{
    public class ImgCommonHelper
    {
        #region MemoryShareToImg

        public static Bitmap MemoryShareToImage(byte[] bytes, Size imageSize, int imageDepth)
        {
            if (bytes == null)
            {
                return null;
            }

            Bitmap bmp = null;
            if (imageDepth == 8 || imageDepth == 12)
            {
                bmp = CreateGrayscaleImage(imageSize.Width, imageSize.Height); //这个函数在后面有定义
                BitmapData bd = bmp.LockBits(new Rectangle(0, 0, imageSize.Width, imageSize.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                byte[] bytes24 = bytes;
                IntPtr ptr = bd.Scan0;
                int bmpLen = bd.Stride * bd.Height;
                if (bmpLen > bytes.Length)
                {
                    bytes24 = new byte[bmpLen];
                    for (int i = 0; i < imageSize.Height; i++)
                    for (int j = 0; j < imageSize.Width; j++)
                        bytes24[i * imageSize.Width + j] = bytes[i * imageSize.Width + j];
                }

                Marshal.Copy(bytes24, 0, ptr, bmpLen);
                bmp.UnlockBits(bd);
                bytes24 = null;
            }
            else if (imageDepth == 24)
            {
                bmp = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format24bppRgb);
                BitmapData bd = bmp.LockBits(new Rectangle(0, 0, imageSize.Width, imageSize.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                byte[] bytes24 = bytes;
                IntPtr ptr = bd.Scan0;
                int bmpLen = bd.Stride * bd.Height;
                Marshal.Copy(bytes24, 0, ptr, bytes.Length);
                bmp.UnlockBits(bd);
                bytes24 = null;
            }

            return bmp;
        }

        public static Bitmap CreateGrayscaleImage(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            SetGrayscalePalette(bmp);
            return bmp;
        }

        public static void SetGrayscalePalette(Bitmap srcImg)
        {
            if (srcImg.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException();
            ColorPalette cp = srcImg.Palette;
            for (int i = 0; i < 256; i++)
            {
                cp.Entries[i] = Color.FromArgb(i, i, i);
            }

            srcImg.Palette = cp;
        }

        #endregion

        /// <summary>
        /// byte[]转换成Image
        /// </summary>
        /// <param name="byteArrayIn">二进制图片流</param>
        /// <returns>Image</returns>
        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Flush();
                return returnImage;
            }
        }

        //byte[] 转换 Bitmap
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image) new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

        //Bitmap转byte[]  
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        /*数据拼接*/
        public static byte[] CopyToBig(byte[] bBig, byte[] bSmall)
        {
            byte[] tmp = new byte[bBig.Length + bSmall.Length];
            System.Buffer.BlockCopy(bBig, 0, tmp, 0, bBig.Length);
            System.Buffer.BlockCopy(bSmall, 0, tmp, bBig.Length, bSmall.Length);
            return tmp;
        }

        //实现左右拼接图片  
        private static Image JoinImage(Image Img1, Image Img2)
        {
            int imgHeight = 0, imgWidth = 0;
            imgWidth = Img1.Width + Img2.Width;
            imgHeight = Math.Max(Img1.Height, Img2.Height);
            Bitmap joinedBitmap = new Bitmap(imgWidth, imgHeight);
            Graphics graph = Graphics.FromImage(joinedBitmap);
            graph.DrawImage(Img1, 0, 0, Img1.Width, Img1.Height);
            graph.DrawImage(Img2, Img1.Width, 0, Img2.Width, Img2.Height);
            return joinedBitmap;
        }

        /// <summary>  
        /// 将一个字节数组转换为8bit灰度位图  
        /// </summary>  
        /// <param name="rawValues">显示字节数组</param>  
        /// <param name="width">图像宽度</param>  
        /// <param name="height">图像高度</param>  
        /// <returns>位图</returns>  
        public static Bitmap ToGrayBitmap(byte[] rawValues, int width, int height)
        {
            //// 申请目标位图的变量，并将其内存区域锁定  
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //// 获取图像参数  
            int stride = bmpData.Stride; // 扫描线的宽度  
            int offset = stride - width; // 显示宽度与扫描线宽度的间隙  
            IntPtr iptr = bmpData.Scan0; // 获取bmpData的内存起始位置  
            int scanBytes = stride * height; // 用stride宽度，表示这是内存区域的大小  

            //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组  
            int posScan = 0, posReal = 0; // 分别设置两个位置指针，指向源数组和目标数组  
            byte[] pixelValues = new byte[scanBytes]; //为目标数组分配内存  

            for (int x = 0; x < height; x++)
            {
                //// 下面的循环节是模拟行扫描  
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }

                posScan += offset; //行扫描结束，要将目标位置指针移过那段“间隙”  
            }

            //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中  
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData); // 解锁内存区域  

            //// 下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度  
            System.Drawing.Imaging.ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }

            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;

            //// 算法到此结束，返回结果  
            return bmp;
        }
    }
}