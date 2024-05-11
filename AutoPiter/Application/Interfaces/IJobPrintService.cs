using AutoPiter.Application.DTO;
using AutoPiter.Domain.Statuses;

namespace AutoPiter.Application.Interfaces
{
    public interface IJobPrintService : IBaseService
    {
        Task<JobStatus> PrintAsync(PrintJobCreateDTO printJobCreateDTO, CancellationToken cancellationToken);
        Task<int> CountInfoFromFile(Stream body);
    }
}
