﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using System.ServiceModel;
using System.ServiceModel.Diagnostics;
using System.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Drawing;
using System.Threading;

namespace Server
{
   
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
                    ConcurrencyMode = ConcurrencyMode.Multiple,
                    UseSynchronizationContext = true)]
    public class Service : IService
    {
        private Bitmap _image;
        private int _progress = 0;
        private bool _isAlive;
        private List<int> _worker = new List<int>();

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
            Thread filter = new Thread(() => { });
            switch (nameOfFilter)
            {
                case "red":
                    filter = new Thread(() => { Red(); });
                    break;
                case "blue":
                    filter = new Thread(() => { Blue(); });
                    break;
                case "green":
                    filter = new Thread(() => { Green(); });
                    break;
            }
            lock (_worker)
            {
                filter.Start();
                Monitor.Wait(_worker);
            }
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
            lock (_worker)
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
                }
                _progress = 100;
                Monitor.PulseAll(_worker);
            }
        }

        private void Blue()
        {
            lock (_worker)
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
                }
                _progress = 100;
                Monitor.PulseAll(_worker);
            }
        }

        private void Green()
        {
            lock (_worker)
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
                }
                _progress = 100;
                Monitor.PulseAll(_worker);
            }
        }
    }
}
