﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ITitleService
    {
        List<Title> GetList();
        List<Title> GetListByWriter(int id);
        void TitleAdd(Title title);
        Title GetById(int id);
        void TitleDelete(Title title);
        void TitleUpdate(Title title);
    }
}
