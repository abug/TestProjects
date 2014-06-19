using System;

namespace Mapper.Bin.OuterModel.Contracts
{
    public interface IDataDepartment : ICloneable
    {
        int Id { get; set; }

        string Name { get; set; }

        string Department { get; set; }
    }
}