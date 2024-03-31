using Business;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    class ImageModel
    {
        public string img;

        public ImageModel()
        {
            ImageLoader imageLoader = new ImageLoader();
            img = imageLoader.GetImagePath(8);
        }
    }
}
