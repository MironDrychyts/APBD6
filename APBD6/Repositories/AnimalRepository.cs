using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
       
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
      
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy} ASC ;";
        
        var reader = command.ExecuteReader();
        
        var animals = new List<Animal>();
        

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Category = reader["Category"].ToString(),
                Area = reader["Area"].ToString(),
                
            });
        }

        return animals;
    }

    public void AddAnimal(AddAnimal animal)
    {
     
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
   
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES(@animalName, @desctription, @category, @area);";
        command.Parameters.AddWithValue("animalName", animal.Name);
        command.Parameters.AddWithValue("desctription", animal.Description);
        command.Parameters.AddWithValue("category", animal.Category);
        command.Parameters.AddWithValue("area", animal.Area);
        command.ExecuteNonQuery();
    }

    public void UpdateAnimal(int id, AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
   
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Animal SET Name=@animalName, Description=@desctription, Category=@category, Area=@area WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("animalName", animal.Name);
        command.Parameters.AddWithValue("desctription", animal.Description);
        command.Parameters.AddWithValue("category", animal.Category);
        command.Parameters.AddWithValue("area", animal.Area);
        command.Parameters.AddWithValue("idAnimal", id);
        command.ExecuteNonQuery();
    }

    public void DeleteAnimal(int id)
    {
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
   
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", id);
        command.ExecuteNonQuery();

    }
}