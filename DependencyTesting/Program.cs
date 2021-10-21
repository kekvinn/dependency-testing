using NUnit.Framework;
using NUnitLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;

namespace DependencyTesting
{
    public class Program
    {
        static int Main(string[] args)
        {
            // Runs all unit tests
            return new AutoRun(Assembly.GetCallingAssembly()).Execute(new String[] {"--labels=All"});
        }

        // ----------------- TESTS
        [TestFixture]
        public class IntegrationTests
        {
            [Test]
            public void SampleTest1()
            {
                // Arrange , Act , Assert

                // Arrange
                var mockDatabase = new Mock<IDatabase>();
                mockDatabase.Setup(m => m.GetEntityById(It.IsAny<int>())).Returns(1);
                BusinessService service = new BusinessService(mockDatabase.Object);

                // Act
                int? domainValue = service.RetrieveEntityWithBusinessLogicApplied(1);

                // Assert
                Assert.AreEqual(1, domainValue);
                
            }
            
            [Test]
            public void SampleTest2()
            {
                // Arrange , Act , Assert

                // Arrange
                var mockDatabase = new Mock<IDatabase>();
                mockDatabase.Setup(m => m.GetEntityById(It.IsAny<int>())).Returns(3);
                BusinessService service = new BusinessService(mockDatabase.Object);

                // Act
                int? domainValue = service.RetrieveEntityWithBusinessLogicApplied(1);

                // Assert
                Assert.AreEqual(6, domainValue);
            }

            [Test]
            public void SampleTest3()
            {
                // Arrange , Act , Assert

                // Arrange
                var mockDatabase = new Mock<IDatabase>();
                mockDatabase.Setup(m => m.GetEntityById(It.IsAny<int>())).Returns(5);
                BusinessService service = new BusinessService(mockDatabase.Object);

                // Act
                int? domainValue = service.RetrieveEntityWithBusinessLogicApplied(1);

                // Assert
                Assert.AreEqual(15, domainValue);
            }

            [Test]
            public void SampleTest4()
            {
                // Arrange , Act , Assert

                // Arrange
                var mockDatabase = new Mock<IDatabase>();
                mockDatabase.Setup(m => m.GetEntityById(It.IsAny<int>())).Returns((int? m) => null);
                BusinessService service = new BusinessService(mockDatabase.Object);

                // Act
                int? domainValue = service.RetrieveEntityWithBusinessLogicApplied(1);

                // Assert
                Assert.AreEqual(-1, domainValue);
            }
        }

        //-----------------* DEPENDENCIES
        public interface IDatabase
        {
            public int? GetEntityById(int entityId);
        }

        //-----------------* BUSINESS LOGIC
        public class BusinessService
        {
            
            private IDatabase myDatabase;
            
            public BusinessService(IDatabase database)
            {
                myDatabase = database;
            }
            
            public int? RetrieveEntityWithBusinessLogicApplied(int entityId)
            {
                int? entity = myDatabase.GetEntityById(entityId);

                if (entity % 3 == 0)
                {
                    return (int) entity * 2;
                }
                else if (entity % 5 == 0)
                {
                    return (int) entity * 3;
                }
                else if (entity == null)
                    return -1;
                else
                {
                    return entity;
                }
            }
        }
        
    }
}