﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksEvaluation.Core.Mapper
{
    public interface IMapFrom
    {
        void Mapping(Profile profile);  
    }
}