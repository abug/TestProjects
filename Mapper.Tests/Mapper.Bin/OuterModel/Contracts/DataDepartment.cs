namespace Mapper.Bin.OuterModel.Contracts
{
    public class DataDepartment : IDataDepartment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public object Clone()
        {
            var department = new DataDepartment
            {
                Id = this.Id,
                Name = this.Name
            };

            return department;
        }
    }
}