﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstracs
{
    public interface IMessageDal: IRepository<Message>
    {
    }
}
