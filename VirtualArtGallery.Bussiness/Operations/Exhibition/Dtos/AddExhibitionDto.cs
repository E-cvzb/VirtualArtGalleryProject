﻿using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.Operations.Exhibition.Dtos
{
    public  class AddExhibitionDto
    {
        public string Name { get; set; }
        public string Date { get; set; }
    }
}
