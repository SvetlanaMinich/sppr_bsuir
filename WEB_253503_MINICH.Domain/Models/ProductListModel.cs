﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_253503_MINICH.Domain.Models
{
    public class ProductListModel<T>
    {
        // запрошенный список объектов
        public List<T> Items { get; set; } = [];

        // номер текущей страницы
        public int CurrentPage { get; set; } = 1;

        // общее количество страниц
        public int TotalPages { get; set; } = 1;
    }
}
