using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TechTalk.Services
{
    public class ReportService: IReportService
    {
        public async Task CreateReport(DateTime dt)
        {

        }
    }

    public interface IReportService
    {
        Task CreateReport(DateTime dt);
    }
}
