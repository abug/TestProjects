using System.Collections.Generic;
using System.Diagnostics;

namespace Mapper.Bin.OuterModel.Contracts
{
    public class DataStore : IDataStore
    {
        public DataStore()
        {
            Departments = new List<IDataDepartment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public IList<IDataDepartment> Departments { get; set; }

        public object Clone()
        {
            var store = new DataStore
            {
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                Description = this.Description
            };

            foreach (var dataDepartment in Departments)
            {
                store.Departments.Add((IDataDepartment) dataDepartment.Clone());
            }

            return store;
        }
    }
}