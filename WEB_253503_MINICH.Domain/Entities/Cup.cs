using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_253503_MINICH.Domain.Entities
{
    public class Cup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
        public double Price { get; set; }
        public string? ImgPath { get; set; }
        public string? MimeImgType { get; set; }
    }
}
