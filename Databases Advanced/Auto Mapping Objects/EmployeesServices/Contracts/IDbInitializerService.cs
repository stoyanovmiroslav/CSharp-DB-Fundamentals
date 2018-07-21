namespace Employees.Services.Contracts
{
    public interface IDbInitializerService
    {
        void Initializer();
        void Seed();
    }
}