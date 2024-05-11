using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using AutoPiter.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AutoPiter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BranchController(IBranchService branchService) : Controller
    {
        private readonly IBranchService _branchService = branchService;

        /// <summary>
        /// Получает список всех филиалов
        /// </summary>
        /// <returns>Список филиалов</returns>
        /// <response code="200">Успешно вернул список филиалов</response>
        [HttpGet]
        public async Task<ActionResult<List<BranchDTO>>> GetAllBranches()
        {
            var branches = await _branchService.GetAllBranchesAsync();
            return Ok(branches);
        }
    }
}
