using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordCounter
{
    public class GystMember {
        public int value { get; private set; }
        public int count { get; set; }

        public GystMember(int value) {
            this.value = value;
            count = 0;
        }

        public void Add() {
            count++;
        }
    }

    public class Gystogram {

        public static List<GystMember> DoWordGystogram(MyBitmap str) {
            Bitmap newBm = new Bitmap(str.height, str.width);

            for (int y = 0; y < str.height; y++)
            {
                for (int x = 0; x < str.width; x++)
                {
                    newBm.SetPixel(y, newBm.Height - x - 1, str.bitmap.GetPixel(x, y));
                }
            }
            MyBitmap turnedString = new MyBitmap(newBm);
            str = turnedString;
            List<GystMember> wordGyst = DoStringGystogram(turnedString);
            //SavePhotoWithGystogram(wordGyst, newBm, "F:\\Progr\\Csh\\wordCounter\\wordCounter\\bin\\Debug\\img\\" + 12 + ".jpg");
            return wordGyst;
        }
        public static List<GystMember> DoStringGystogram(MyBitmap bitmap) {
            List<GystMember> gystogram = new List<GystMember>();
            for (int x = 0; x < bitmap.height; x++)
            {
                gystogram.Add(new GystMember(x));
                for (int y = 0; y < bitmap.width; y++)
                {
                    if (bitmap.bitmap.GetPixel(y, x).ToArgb() != Color.White.ToArgb())
                    {
                        gystogram[x].Add();
                    }
                }
            }
            return gystogram;
        }

        public static void SavePhotoWithGystogram(List<GystMember> gystogram, Bitmap bitmap, string path) {

            int width = bitmap.Width; 
            int height = bitmap.Height;
            Bitmap bitm = new Bitmap(width + 50, height);
            for (int y = 0; y < bitm.Height; y++)
            {
                if (gystogram[y].count > 0)
                    gystogram[y].count = 25;
                for (int x = 0; x < bitm.Width; x++)
                {
                    if (x < 50)
                    {
                        if (x < gystogram[y].count)
                            bitm.SetPixel(x, y, Color.BlueViolet);
                        else
                            bitm.SetPixel(x, y, Color.White);
                    }
                    else
                        bitm.SetPixel(x, y, bitmap.GetPixel(x - 50, y));
                }
            }
            bitm.Save(path);
        }
    }
}
