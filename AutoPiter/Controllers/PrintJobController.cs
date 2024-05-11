using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using AutoPiter.Domain.Statuses;
using Microsoft.AspNetCore.Mvc;

namespace AutoPiter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PrintJobController(IJobPrintService jobPrintService) : Controller
    {
        private readonly IJobPrintService _jobPrintService = jobPrintService;

        /// <summary>
        /// Регистрация задания печати
        /// </summary>
        /// <param name="printCreateDTO">Данные для печати</param>
        /// <returns>Статус печати. 1 - успешно, 0 - не успешно</returns>
        /// <response code="200">Печать успешно создана</response>
        [HttpPost]
        public async Task<ActionResult<JobStatus>> Print(PrintJobCreateDTO printCreateDTO, CancellationToken cancellationToken)
        {
            var jobStatus = await _jobPrintService.PrintAsync(printCreateDTO, cancellationToken);
            return Ok(jobStatus);
        }

        /// <summary>
        /// Регистрация задания печати из csv файла, получаемого как бинарные данные
        /// </summary>
        /// <returns>Количество успешно созданных заданий печати</returns>
        /// <response code="200">Печати успешно создана</response>
        /// <response code="400">Неверная кодировка файла</response>
        [HttpPost("create_from_csv")]
        public async Task<ActionResult<int>> CreatePrintFromCSV()
        {
            var count = await _jobPrintService.CountInfoFromFile(Request.Body);
            return Ok(count);
        }
    }
}
