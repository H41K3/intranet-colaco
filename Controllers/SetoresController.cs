using IntranetColaco.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntranetColaco.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetoresController : ControllerBase
{
    [HttpGet("{setorKey}")]
    public IActionResult GetSetor(string setorKey)
    {
        if (!SetoresCatalogo.Todos.TryGetValue(setorKey.Trim(), out var setor))
            return NotFound(new { mensagem = "Setor não encontrado." });

        return Ok(setor);
    }
}