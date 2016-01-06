using AccountDataProcess.DapperData.Repository;
using AccountDataProcess.Model.Entity;
using NUnit.Framework;
using System;

namespace KPMG.Test
{
   [TestFixture]
    public class DapperAccountDataRepositoryTest
    {
        // Initialize file reader
       
       private IAccountDataRepository accountDataRepository = new AccountDataRepository(@"Data Source=JAY-LAPTOP;Initial Catalog=AccountDataProcess;Integrated Security=True;MultipleActiveResultSets=True");

        //[SetUp]
        //public void SetUp()
        //{
        //    this.accountDataRepository = new AccountDataRepository(connectionString);
        //}

       [Test]
        public void AddAccountDataWithRepositoryTest()
        {
           // We use a static id, so we always can drop the project
            Guid id = new Guid("329BF626-9F06-483B-AD0F-F70E27BE30F7");

            CleanUp(id);

            AccountData data = new AccountData() { Id = id, Account = "Test Account", Amount = 200, CurrencyCode = "EUR", Description = "Unit test ADD" };

            accountDataRepository.Add(data);
           // return data with same Id

            //var result = new AccountDataRepository(connectionString).Find(id);
            //Assert.That(result.Id, Is.EqualTo(id));
            //Assert.That(result.Account, Is.EqualTo("Test Account"));
            //Assert.That(result.Amount, Is.EqualTo(200));
            //Assert.That(result.CurrencyCode, Is.EqualTo("EUR"));

        }

       private void CleanUp(Guid Id)
       {
           var result = accountDataRepository.Find(Id);

           if (result != null)
           {
               accountDataRepository.Remove(Id);
           }
       }
    }
}
