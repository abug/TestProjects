using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Mapper.Bin.InnerModel.Contracts;
using Mapper.Bin.InnerModel.Implementation;
using Mapper.Bin.OuterModel.Contracts;
using NUnit.Framework;

namespace Mapper.Tests
{
    [TestFixture]
    public class AutoMapperTests
    {
        private ILogger _logger;

        [SetUp]
        public void SetUpTests()
        {
            _logger = new SimpleLogger();
        }

        [Test]
        public void Automapper_Flattening_Test()
        {
            _logger.Debug("Init mapping;");

            AutoMapper.Mapper.CreateMap<IDataStore, IRetailStore>();
            AutoMapper.Mapper.CreateMap<IDataDepartment, IStoreDepartment>();

            _logger.Debug("Init source data;");

            IDataStore source = new DataStore
            {
                Id = 1,
                Name = "K3 Business Solutions BV",
                Address = "Gildeweg 9, 2632 BD Nootdorp",
                Description = "Компанія-розробник програмного забезпечення",
                Departments =
                {
                    new DataDepartment
                    {
                        Id = 1,
                        Name = "Auto 'Romeo'"
                    },
                    new DataDepartment
                    {
                        Id = 2,
                        Name = "De Koning Self-storage"
                    }
                }
            };

            var result = AutoMapper.Mapper.Map<IRetailStore>(source);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(source.Name));
            Assert.That(result.Departments, Is.Not.Null);
            Assert.That(result.Departments.Count, Is.EqualTo(source.Departments.Count));
        }

        [Test]
        public void Automapper_Flattening_SpeedTest(
            [Values(10, 100, 1000, 10000, 100000, 1000000)]int objectsCount)
        {
            _logger.Debug("Init mapping;");

            AutoMapper.Mapper.CreateMap<IDataStore, IRetailStore>();
            AutoMapper.Mapper.CreateMap<IDataDepartment, IStoreDepartment>();

            _logger.Debug("Init source data;");

            IDataStore source = new DataStore
            {
                Id = 1,
                Name = "K3 Business Solutions BV",
                Address = "Gildeweg 9, 2632 BD Nootdorp",
                Description = "Компанія-розробник програмного забезпечення",
                Departments =
                {
                    new DataDepartment
                    {
                        Id = 1,
                        Name = "Auto 'Romeo'"
                    },
                    new DataDepartment
                    {
                        Id = 2,
                        Name = "De Koning Self-storage"
                    }
                }
            };

            _logger.Debug("Create {0} list;", objectsCount);
            _logger.Debug("Begin");

            var list = new List<IDataStore>();

            for (int i = 0; i < objectsCount; i++)
            {
                list.Add((IDataStore) source.Clone());
            }

            _logger.Debug("End");

            var stopWatch = new Stopwatch();

            _logger.Debug("Stopwatch start;");
            stopWatch.Start();

            var result = AutoMapper.Mapper.Map<IList<IRetailStore>>(list);

            stopWatch.Stop();
            _logger.Debug("Stopwatch stop;");

            Assert.That(ReferenceEquals(list.First(), list.Last()), Is.Not.True);
            Assert.That(result, Is.Not.Null);

            _logger.Debug("Measured time: {0};", stopWatch.Elapsed);
        }

        [Test]
        public void Automapper_Projection_SpeedTest(
            [Values(10, 100, 1000, 10000, 100000, 1000000)]int objectsCount)
        {
            _logger.Debug("Init mapping;");

            AutoMapper.Mapper.CreateMap<IDataStore, IRetailStore>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.Departments));

            AutoMapper.Mapper.CreateMap<IDataDepartment, IStoreDepartment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

            _logger.Debug("Init source data;");

            IDataStore source = new DataStore
            {
                Id = 1,
                Name = "K3 Business Solutions BV",
                Address = "Gildeweg 9, 2632 BD Nootdorp",
                Description = "Компанія-розробник програмного забезпечення",
                Departments =
                {
                    new DataDepartment
                    {
                        Id = 1,
                        Name = "Auto 'Romeo'"
                    },
                    new DataDepartment
                    {
                        Id = 2,
                        Name = "De Koning Self-storage"
                    }
                }
            };

            _logger.Debug("Create {0} list;", objectsCount);
            _logger.Debug("Begin");

            var list = new List<IDataStore>();

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            for (int i = 0; i < objectsCount; i++)
            {
                list.Add((IDataStore) source.Clone());
            }

            stopWatch.Stop();

            _logger.Debug("End at: {0};", stopWatch.Elapsed);

            _logger.Debug("Stopwatch start;");
            stopWatch.Reset();
            stopWatch.Start();

            var result = AutoMapper.Mapper.Map<IList<IRetailStore>>(list);

            stopWatch.Stop();
            _logger.Debug("Stopwatch stop;");

            Assert.That(ReferenceEquals(list.First(), list.Last()), Is.Not.True);
            Assert.That(result, Is.Not.Null);

            _logger.Debug("Measured time: {0};", stopWatch.Elapsed);
        }

        [Test]
        public void Handmapping_SpeedTest(
            [Values(10, 100, 1000, 10000, 100000, 1000000)]int objectsCount)
        {
            IDataStore source = new DataStore
            {
                Id = 1,
                Name = "K3 Business Solutions BV",
                Address = "Gildeweg 9, 2632 BD Nootdorp",
                Description = "Компанія-розробник програмного забезпечення",
                Departments =
                {
                    new DataDepartment
                    {
                        Id = 1,
                        Name = "Auto 'Romeo'"
                    },
                    new DataDepartment
                    {
                        Id = 2,
                        Name = "De Koning Self-storage"
                    }
                }
            };

            _logger.Debug("Create {0} list;", objectsCount);
            _logger.Debug("Begin");

            var list = new List<IDataStore>();

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            for (int i = 0; i < objectsCount; i++)
            {
                list.Add((IDataStore) source.Clone());
            }

            stopWatch.Stop();

            _logger.Debug("End at: {0};", stopWatch.Elapsed);

            _logger.Debug("Stopwatch start;");
            stopWatch.Reset();
            stopWatch.Start();

            var result = MapDataStoreToRetailStore(list);

            stopWatch.Stop();
            _logger.Debug("Stopwatch stop;");

            Assert.That(ReferenceEquals(list.First(), list.Last()), Is.Not.True);
            Assert.That(result, Is.Not.Null);

            _logger.Debug("Measured time: {0};", stopWatch.Elapsed);
        }

        private IList<IRetailStore> MapDataStoreToRetailStore(IList<IDataStore> source)
        {
            if (source != null)
            {
                var result = new List<IRetailStore>();

                foreach (var dataStore in source)
                {
                    var retailStore = new RetailStore
                    {
                        Id = dataStore.Id,
                        Name = dataStore.Name,
                        Address = dataStore.Address,
                        Description = dataStore.Description
                    };

                    foreach (var dataDepartment in dataStore.Departments)
                    {
                        retailStore.Departments.Add(new StoreDepartment
                        {
                            Id = dataDepartment.Id,
                            Name = dataDepartment.Name
                        });
                    }

                    result.Add(retailStore);
                }

                return result;
            }

            return null;
        }
    }

    public class SimpleLogger : ILogger
    {
        public void Debug(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(message, args);
        }
    }

    public interface ILogger
    {
        void Debug(string message, params object[] args);
    }
}
