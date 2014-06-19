using System.Collections.Generic;
using Mapper.Bin.InnerModel.Contracts;

namespace Mapper.Bin.InnerModel.Implementation
{
    public class RetailStore : IRetailStore
    {
        public RetailStore()
        {
            Departments = new List<IStoreDepartment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public IList<IStoreDepartment> Departments { get; set; }
    }
}