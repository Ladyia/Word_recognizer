using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordCounter
{
    public class MyBitmap
    {
        public int width { get; private set; }
        public int height { get; private set; }
        public int size { get; private set; }
        public int[] pixels;
        public Bitmap bitmap;
        public List<GystMember> gystogram;
        public List<TextString> strings;
        public MyBitmap(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            width = bitmap.Width;
            height = bitmap.Height;
            size = width * height;
            pixels = new int[size];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixels[height * x + y] = this.bitmap.GetPixel(x, y).ToArgb();
                }
            }
        }

        public Bitmap ConvertBitmapToBlackAndWhite() {
            Bitmap bwImage = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int currentColor = bitmap.GetPixel(x, y).ToArgb();
                    int R = currentColor & 0xFF;
                    int G = (currentColor >> 8) & 0xFF;
                    int B = (currentColor >> 16) & 0xFF;
                    double luminance = (0.299 * R + 0.0F + 0.587 * G + 0.0f + 0.114 * B + 0.0f);
                    int threshold = 128;
                    pixels[bitmap.Height * x + y] = luminance > threshold ? (int)Color.White.ToArgb() : (int)Color.Black.ToArgb();
                    bwImage.SetPixel(x, y, Color.FromArgb(pixels[bitmap.Height * x + y]));
                }
            }
            try
            {
                bwImage.Save("F:\\Progr\\Csh\\wordCounter\\wordCounter\\bin\\Debug\\img\\black_white_image.jpg");
            }
            catch (Exception) {

            }
            this.bitmap = bwImage;
            return bwImage;
        }

        public void DivideIntoStrings() {
            strings = new List<TextString>();
            List<GystMember> spaceGyst = DoSpaceGystogram(gystogram);
            int firstSpace = spaceGyst[0].value;
            spaceGyst.RemoveAt(0);
            spaceGyst.RemoveAt(0);
            GystMember c = spaceGyst[0];
            foreach (GystMember gm in spaceGyst)
            {
                if (gm.count > c.count)
                    c = gm;
            }
            int space = c.value;
            int y = firstSpace;
            while (y < gystogram.Count)
            {
                if (gystogram[y].count != 0)
                {
                    int i = 0;
                    while (i + y < gystogram.Count && gystogram[y + i].count != 0 || y + i - 1 + space < gystogram.Count && gystogram[y + i - 1 + space].count != 0)
                        i++;
                    Bitmap strBitm = new Bitmap(bitmap.Width, i);
                    for (int coordY = 0; coordY < i; coordY++)
                    {
                        for (int coordX = 0; coordX < bitmap.Width; coordX++)
                        {
                            strBitm.SetPixel(coordX, coordY, bitmap.GetPixel(coordX, coordY + y - 2));
                        }
                    }
                    strings.Add(new TextString(strBitm));
                    y += i + space;
                }
                else
                {
                    y++;
                }
            }
        }



        public static List<GystMember> DoSpaceGystogram(List<GystMember> mGystogram) {
            List<GystMember> spaceGyst = new List<GystMember>();
            int counter = 0;
            for (int i = 0; i < mGystogram.Count; i++)
            {
                if (mGystogram[i].count == 0)
                    counter++;
                else
                {
                    bool hasThisElem = false;
                    int num = 0;
                    foreach (GystMember gm in spaceGyst)
                    {
                        if (gm.value == counter)
                        {
                            hasThisElem = true;
                            break;
                        }
                        num++;
                    }
                    if (hasThisElem)
                        spaceGyst[num].Add();
                    else
                        spaceGyst.Add(new GystMember(counter));
                    counter = 0;
                }
            }
            return spaceGyst;
        }
    }

    public class TextString {

        public MyBitmap str;
        public List<GystMember> gystogram;
        public List<Word> words;
        public TextString(Bitmap str) {
            this.str = new MyBitmap(str) ;
        }

        public void TrimString() {
            int start = 0;
            while (gystogram[start].count == 0)
                start++;
            int finish = gystogram.Count - 1;
            while (gystogram[finish].count == 0)
                finish--;
            MyBitmap newMBM = new MyBitmap(new Bitmap(str.width, finish - start));
            Console.WriteLine(str.width + "---" + str.height);
            //for (int y = start; y < finish - start - 1; y++)
            //{
            //    for (int x = 0; x < str.width; x++)
            //    {
            //        newMBM.bitmap.SetPixel(x, y - (start), str.bitmap.GetPixel(x, y));
            //    }
            //}
            //str = newMBM;
        }

        public void DivideWords() {
            List<GystMember> spaces = MyBitmap.DoSpaceGystogram(gystogram);
            foreach (GystMember gm in spaces) {
                Console.WriteLine(gm.value + "--------" + gm.count);
            }
            Console.WriteLine("____________________________________");
            //int startInd = spaces[0].value;
            //spaces.RemoveAt(0);
            //spaces.RemoveAt(0);
            //int space = FindMaxSpace(spaces);
            //int y = startInd;
            //while (y < gystogram.Count)
            //{
            //    if (gystogram[y].count != 0)
            //    {
            //        int i = 0;
            //        while (i + y < gystogram.Count && gystogram[y + i].count != 0 || y + i - 1 + space < gystogram.Count && gystogram[y + i - 1 + space].count != 0)
            //            i++;
            //        Bitmap strBitm = new Bitmap(str.width, i);
            //        for (int coordY = 0; coordY < i; coordY++)
            //        {
            //            for (int coordX = 0; coordX < str.width; coordX++)
            //            {
            //                strBitm.SetPixel(coordX, coordY, str.bitmap.GetPixel(coordX, coordY + y - 2));
            //            }
            //        }
            //        words.Add(new Word(new MyBitmap(strBitm)));
            //        y += i + space;
            //    }
            //    else
            //    {
            //        y++;
            //    }
            //}
        }
        private int FindMaxSpace(List<GystMember> gystogram) {
            int min = int.MaxValue;
            int min2 = int.MaxValue - 1;
            int num = 0;
            int num2 = 0;
            int count = 0;
            int max = 0;
            foreach (GystMember gm in gystogram) {
                if (gm.value < min) {
                    num = count;
                }
                count++;
            }
            //gystogram.RemoveAt(num);
            //foreach (GystMember gm in gystogram)
            //{
            //    if (gm.value < min)
            //    {
            //        num = count;
            //    }
            //    count++;
            //}
            //gystogram.RemoveAt(num);  //переделать
            //foreach (GystMember gm in gystogram)
            //{
            //    if (gm.value > max)
            //    {
            //        num = count;
            //    }
            //    count++;
            //}
            return gystogram[num].value;
        }
    }

    public class Word {
        MyBitmap word;

        public Word(MyBitmap word) {
            this.word = word;
        }
    }
}
