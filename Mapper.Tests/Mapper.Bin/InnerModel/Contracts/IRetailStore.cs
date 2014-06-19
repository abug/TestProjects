using System.Collections.Generic;

namespace Mapper.Bin.InnerModel.Contracts
{
    public interface IRetailStore
    {
        int Id { get; set; }

        string Name { get; set; }

        string Address { get; set; }

        string Description { get; set; }

        IList<IStoreDepartment> Departments { get; set; }
    }
}
