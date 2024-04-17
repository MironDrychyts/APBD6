using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;
using Tutorial5.Repositories;

namespace Tutorial5.Controllers;

[ApiController]
// [Route("/api/animals")]
[Route("/api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet("{orderBy}")]
    public IActionResult GetAnimals(string orderBy = "Name")
    {
        var animals = _animalRepository.GetAnimals(orderBy);

        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        _animalRepository.AddAnimal(animal);
        
        // 201
        return Created("/api/animals", null);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, AddAnimal animal)
    {
        _animalRepository.UpdateAnimal(id,animal);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        _animalRepository.DeleteAnimal(id);
        return NoContent();
    }
    
}