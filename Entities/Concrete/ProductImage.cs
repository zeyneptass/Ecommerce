﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductImage : IEntity
    {
        [Key]
        public int ImageID { get; set; }
        public int ProductID { get; set; }
        public string ImagePath { get; set; }
    }

}
