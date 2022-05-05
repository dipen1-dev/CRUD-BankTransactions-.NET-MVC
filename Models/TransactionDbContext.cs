using Microsoft.EntityFrameworkCore;

namespace CRUD_BankTransactions.Models
{
    public class TransactionDbContext : DbContext 
        //inherit the dbcontext to use the dbcontext to mapping the object to the relational database 

    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options
            ) : base(options)
            //only instance of dbcontextoptions is used for dbcontext which is options

        { }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
