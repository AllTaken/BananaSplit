﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaSplit;
public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Settings, Settings>();
    }
}
