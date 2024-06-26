﻿using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    void AddAnimal(AddAnimal animal);
    void UpdateAnimal(int id, AddAnimal animal);

    void DeleteAnimal(int id);
}