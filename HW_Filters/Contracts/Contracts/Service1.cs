using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Contracts
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
                    ConcurrencyMode = ConcurrencyMode.Multiple,
                    UseSynchronizationContext = true)]
    public class Service1 : IService1
    {
        private Bitmap _image;
        private int _progress = 0;
        private bool _isAlive;

        public List<string> GetListOfFilters()
        {
            List<string> listOfFilters = new List<string>();
            listOfFilters.Add("blue");
            listOfFilters.Add("red");
            listOfFilters.Add("green");
            return listOfFilters;
        }

        public Bitmap ApplyFilter(Bitmap image, string nameOfFilter)
        {
            _isAlive = true;
            _progress = 0;
            _image = new Bitmap(image);
            switch (nameOfFilter)
            {
                case "red":
                    Red();
                    break;
                case "blue":
                    Blue();
                    break;
                case "green":
                    Green();
                    break;
            }
            _progress = 100;
            if (_isAlive)
            {
                return _image;
            }
            else
            { 
                return image;
            }
        }


        public int GetProgress()
        {
           // Console.WriteLine(_progress); // helpful for debuging
            return _progress;
        }
        
        public void Stop()
        {
            _isAlive = false;
        }

        private void Red()
        {
            for (int i = 0; i < _image.Width && _isAlive; i++)
            {
                for (int j = 0; j < _image.Height && _isAlive; j++)
                {
                    Color cur = _image.GetPixel(i, j);
                    byte red = cur.R;
                    byte green = cur.G;
                    byte blue = cur.B;
                    Color newColor = Color.FromArgb((int)red, 0, 0);
                    _image.SetPixel(i, j, newColor);
                }
                _progress = i * 100 / _image.Width;
                Thread.Sleep(1);
            }
            _progress = 100;
        }

        private void Blue()
        {
            for (int i = 0; i < _image.Width && _isAlive; i++)
            {
                for (int j = 0; j < _image.Height && _isAlive; j++)
                {
                    Color cur = _image.GetPixel(i, j);
                    byte red = cur.R;
                    byte green = cur.G;
                    byte blue = cur.B;
                    Color newColor = Color.FromArgb(0, 0, (int)blue);

                    _image.SetPixel(i, j, newColor);
                }
                _progress = i * 100 / _image.Width;
                Thread.Sleep(1);
            }
            _progress = 100;
        }

        private void Green()
        {
            for (int i = 0; i < _image.Width && _isAlive; i++)
            {
                for (int j = 0; j < _image.Height && _isAlive; j++)
                {
                    Color cur = _image.GetPixel(i, j);
                    byte red = cur.R;
                    byte green = cur.G;
                    byte blue = cur.B;
                    Color newColor = Color.FromArgb(0, (int)green, 0);

                    _image.SetPixel(i, j, newColor);
                }
                _progress = i * 100 / _image.Width;
                Thread.Sleep(1);
            }
            _progress = 100;
        }
    }
}
