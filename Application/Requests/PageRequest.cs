﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests
{
    public class PageRequest
    { //paginete yapısı için kullanıcıdan alınan request bilgisine karşılık gelecek class.
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
