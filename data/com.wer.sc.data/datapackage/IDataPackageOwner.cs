﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public interface IDataPackageOwner
    {
        IDataPackage DataPackage { get; }
    }
}