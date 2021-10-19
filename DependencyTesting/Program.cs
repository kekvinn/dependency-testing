using NUnit.Framework;
using NUnitLite;
using System;
using System.Collections.Generic;
using System.Reflection;

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
          var database = new Database();
          //  public void SaveEntity(int entityId, int entity)
          database.SaveEntity(1,1);
            BusinessService service = new BusinessService(database);
           
          // Act
            int? domainValue =
                service.RetrieveEntityWithBusinessLogicApplied(1);
            
          // Assert
            Assert.AreEqual(1, domainValue);
        }

        [Test]
        public void SampleTest2()
        {
          // Arrange , Act , Assert
          
          // Arrange
          var database = new Database();
          //  public void SaveEntity(int entityId, int entity)
          database.SaveEntity(1,3);
            BusinessService service = new BusinessService(database);
           
          // Act
            int? domainValue =
                service.RetrieveEntityWithBusinessLogicApplied(1);
            
          // Assert
            Assert.AreEqual(6, domainValue);
        }
      
        [Test]
        public void SampleTest3()
        {
          // Arrange , Act , Assert
          
          // Arrange
          var database = new Database();
          //  public void SaveEntity(int entityId, int entity)
          database.SaveEntity(1,5);
            BusinessService service = new BusinessService(database);
           
          // Act
            int? domainValue =
                service.RetrieveEntityWithBusinessLogicApplied(1);
            
          // Assert
            Assert.AreEqual(15, domainValue);
        }
        
        [Test]
        public void SampleTest4()
        {
            // Arrange , Act , Assert
          
            // Arrange
            var database = new Database();
            //  public void SaveEntity(int entityId, int entity)
            database.SaveEntity(1,null);
            BusinessService service = new BusinessService(database);
           
            // Act
            int? domainValue =
                service.RetrieveEntityWithBusinessLogicApplied(1);
            
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
            //database = new Database();
          myDatabase = database;
        }
        
        public int? RetrieveEntityWithBusinessLogicApplied(int entityId)
        {
            int? entity = myDatabase.GetEntityById(entityId);
            
            if (entity % 3 == 0)
            {
                return (int)entity * 2;
            }
            else if (entity % 5 == 0)
            {
                return (int)entity * 3;
            }
            else if (entity == null)
                return -1;
            else
            {
                return entity;
            }
        }
    }

    public class Database : IDatabase
    {
        // Static "database" whose values remain in memory
        private static Dictionary<int,int?> database;
        
        public Database()
        {
            if (database == null)
            {
                // Create in memory database
                database = new Dictionary<int,int?>();
            }
        }
        
        public void SaveEntity(int entityId, int? entity)
        {
            database[entityId] = entity;
        }
        
        public int? GetEntityById(int entityId)
        {
            if (database.ContainsKey(entityId))
            {
                return database[entityId];
            }
            else
            {
                return null;
            }
        }
    }

}
}
