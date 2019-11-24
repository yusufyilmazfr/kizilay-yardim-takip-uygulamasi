﻿using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface ITownManager 
    {
        List<Town> GetAllTownsASCByCityId(int cityId);
    }
}
