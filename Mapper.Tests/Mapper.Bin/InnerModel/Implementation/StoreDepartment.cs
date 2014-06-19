using Mapper.Bin.InnerModel.Contracts;

namespace Mapper.Bin.InnerModel.Implementation
{
    public class StoreDepartment : IStoreDepartment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }
    }
}