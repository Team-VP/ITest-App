using ITestApp.DTO;

namespace ITestApp.Services.Contracts
{
    public interface IStatusesService
    {
        StatusDto GetStatusByName(string name);
    }
}
