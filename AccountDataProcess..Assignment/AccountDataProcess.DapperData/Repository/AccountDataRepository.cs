using AccountDataProcess.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AccountDataProcess.DapperData.Repository
{
    public class AccountDataRepository : IAccountDataRepository
    {
        private IDbConnection connection;

        public AccountDataRepository(string connectionstring)
        {
           connection = new SqlConnection(connectionstring);
        }

        public List<AccountData> GetAll()
        {
            using (IDbConnection cn = connection)
            {
                cn.Open();
                return cn.Query<AccountData>("SELECT * FROM AccountDatas").ToList();
            }
        }

        public AccountData Find(Guid Id)
        {
            using (IDbConnection cn = connection)
            {
                cn.Open();
                return cn.Query<AccountData>("SELECT * FROM AccountDatas WHERE Id = @Id", new { Id }).SingleOrDefault();
            }
        }

        public void Add(AccountData entity)
        {
            using (IDbConnection cn = connection)
            {
                var parameters = new
                {
                    Account = entity.Account,
                    Description = entity.Description,
                    CurrencyCode = entity.CurrencyCode,
                    Amount = entity.Amount,
                    Id = entity.Id
                };

                cn.Open();
                entity.Id = cn.Query<Guid>(
                    "INSERT INTO AccountDatas (Account, Description, CurrencyCode, Amount, Id) VALUES(@Account, @Description, @CurrencyCode,  @Amount, @Id)",
                    parameters).FirstOrDefault();
            }
        }

        public void Update(AccountData entity)
        {
            using (IDbConnection cn = connection)
            {
                var parameters = new
                {
                    Account = entity.Account,
                    Description = entity.Description,
                    CurrencyCode = entity.CurrencyCode,
                    Amount = entity.Amount,
                    Id = entity.Id
                };

                cn.Open();
                cn.Execute(
                    "UPDATE AccountDatas SET Account=@Account, Description=@Description, CurrencyCode=@CurrencyCode, Amount=@Amount WHERE Id=@Id",
                    parameters);
            }
        }

        public void Remove(Guid Id)
        {
            using (IDbConnection cn = connection)
            {
                cn.Open();
                cn.Execute(
                    "DELETE FROM AccountDatas WHERE Id=@Id",
                    new { Id = Id });
            }
        }

    }
}
