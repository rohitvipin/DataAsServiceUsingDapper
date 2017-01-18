using DataAsService.Services.Interfaces;

namespace DataAsService.Services
{
    public class ApplicationContextService: IApplicationContextService
    {
        public string ConnectionString { get; set; }
    }
}
