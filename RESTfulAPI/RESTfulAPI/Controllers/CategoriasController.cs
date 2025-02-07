﻿using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Repositories;

namespace RESTfulAPI.Controllers;


[Route("api/[Controller]")]
[ApiController]

//[Authorize]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository categoriaRepository;

    public CategoriasController(ICategoriaRepository categoriaRepository)
    {
        this.categoriaRepository = categoriaRepository;
    }
    [HttpGet]

    public async Task<IActionResult> Get()
    {
        var categorias = await categoriaRepository.GetCategorias();
        return Ok(categorias);
    }
}
