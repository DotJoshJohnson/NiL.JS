﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiL.JS.Modules
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
    public sealed class ImmutableAttribute : Attribute
    {
    }
}
