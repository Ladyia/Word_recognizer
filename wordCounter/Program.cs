using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace wordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            WordCounter wc = new WordCounter();
            wc.Count("F:\\Progr\\Csh\\wordCounter\\wordCounter\\bin\\Debug\\img\\Text.jpg");
            Console.Read();
        }
    }

    public class WordCounter {
        public int Count(Bitmap image) {
            MyBitmap text = new MyBitmap(image);
            text.ConvertBitmapToBlackAndWhite();
            text.gystogram = Gystogram.DoStringGystogram(text);
            Gystogram.SavePhotoWithGystogram(text.gystogram, text.bitmap, "F:\\Progr\\Csh\\wordCounter\\wordCounter\\bin\\Debug\\img\\Text_gystogram.jpg");
            text.DivideIntoStrings();
            int i = 0;
            foreach (TextString stroke in text.strings) {
                stroke.gystogram = Gystogram.DoWordGystogram(stroke.str);
                stroke.TrimString();
                Gystogram.SavePhotoWithGystogram(stroke.gystogram, stroke.str.bitmap, "F:\\Progr\\Csh\\wordCounter\\wordCounter\\bin\\Debug\\img\\" + i + ".jpg");
                stroke.DivideWords();
                i++;   
            }
            return 0;
        }

        public int Count(string path) {
            Bitmap bm = (Bitmap)Image.FromFile(path);
            return Count(bm);
        }
    }
}

