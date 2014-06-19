using System;
using System.Collections.Generic;

namespace Mapper.Bin.OuterModel.Contracts
{
    public interface IDataStore : ICloneable
    {
        int Id { get; set; }

        string Name { get; set; }

        string Address { get; set; }

        string Description { get; set; }

        IList<IDataDepartment> Departments { get; set; }
    }
}
