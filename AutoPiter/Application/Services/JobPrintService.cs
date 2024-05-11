using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using AutoPiter.Domain.Entities;
using AutoPiter.Domain.Exceptions;
using AutoPiter.Domain.Interfaces;
using AutoPiter.Domain.Statuses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Text;

namespace AutoPiter.Application.Services
{
    public class JobPrintService : BaseService, IJobPrintService
    {
        public JobPrintService(IUnitOfWork unitOfWork, ILogger<IBaseService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<int> CountInfoFromFile(Stream body)
        {
            int count = 0;

            var errorsDetailsSB = new StringBuilder();

            using (var reader = new StreamReader(body, Encoding.UTF8))
            {
                string? line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var jobParams = line.Split(';');
                    if (jobParams.Length == 4)
                    {
                        if (string.IsNullOrWhiteSpace(jobParams[0]) ||
                            string.IsNullOrWhiteSpace(jobParams[1]) ||
                            !int.TryParse(jobParams[2], out int deviceSerial) ||
                            !int.TryParse(jobParams[3], out int pagesCount))
                        {
                            continue;
                        }

                        try
                        {
                            var deviceId = await _unitOfWork.RepositoryEmployee.GetDeviceIdByEmployeeAndNumber(Guid.Parse(jobParams[1]), deviceSerial);

                            var printJob = new PrintJob
                            {
                                PrintJobName = jobParams[0],
                                EmployeeId = Guid.Parse(jobParams[1]),
                                DeviceId = deviceId,
                                PagesCount = pagesCount,
                                Status = JobStatus.Success
                            };

                            await _unitOfWork.RepositoryPrintJob.CreateAsync(printJob);
                            await _unitOfWork.SaveAsync();

                            count++;
                        }
                        catch (ApiException ex)
                        {
                            foreach (var kvp in jobParams)
                            {
                                errorsDetailsSB.Append($"{kvp} ");
                            }
                            errorsDetailsSB.Append(ex.ErrorMessage);
                        }
                        catch (Exception ex)
                        {
                            errorsDetailsSB.Append(ex.Message);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            var errors = errorsDetailsSB.ToString();

            if (!String.IsNullOrWhiteSpace(errors))
            {
                _logger.LogDebug(errors);
            }

            return count;
        }

        public async Task<JobStatus> PrintAsync(PrintJobCreateDTO printJobCreateDTO, CancellationToken cancellationToken)
        {
            if (printJobCreateDTO.PagesCount < 0)
            {
                throw new ApiException("Negative value PagesCount", "Не допускается отрицательное значение количества страниц");
            }

            var deviceId = await _unitOfWork.RepositoryEmployee.GetDeviceIdByEmployeeAndNumber(printJobCreateDTO.EmployeeId, printJobCreateDTO.DeviceSerialNumber);

            var rand = new Random();
            var secCount = rand.Next(1, 4);

            await Task.Delay(secCount * 1000, cancellationToken);
            var status = JobStatus.Success;

            var printJob = new PrintJob
            {
                PrintJobName = printJobCreateDTO.PrintJobName,
                EmployeeId = printJobCreateDTO.EmployeeId,
                DeviceId = deviceId,
                PagesCount = printJobCreateDTO.PagesCount,
                Status = status
            };

            await _unitOfWork.RepositoryPrintJob.CreateAsync(printJob);
            await _unitOfWork.SaveAsync();

            return status;
        }
    }
}
