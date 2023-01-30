using PrywatnaPrzychodniaLekarska.Authorization;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Contracts
{
    public interface ILoginService
    {
        AuthResult TokenGenerator(Credentials dto);
    }
}