using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetController : ControllerBase
  {

    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet]
    public List<Pet> GetAllPets()
    {
      // query for all the menu items
      // sort them by name
      var pets = db.Pets.OrderBy(m => m.Name);
      // the sorted items  
      return pets.ToList();
    }

    [HttpGet("{id}")]
    public Pet GetSingle(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      return item;

    }

    [HttpPost]
    public Pet CreateNewPet(Pet pet)
    {
      db.Pets.Add(pet);
      db.SaveChanges();
      return pet;
    }

    [HttpPost("multiple")]
    public List<Pet> AddManyItems(List<Pet> pet)
    {
      db.Pets.AddRange(pet);
      db.SaveChanges();
      return pet;
    }

    [HttpPut("{id}/play")]
    public Pet Playtime(int id)
    {
      var pet = db.Pets.FirstOrDefault(i => i.Id == id);
      pet.HappinessLevel += 5;
      pet.HungarLevel += 3;
      db.SaveChanges();
      return pet;
    }
    [HttpPut("{id}/scold")]
    public Pet BadDog(int id)
    {
      // might not need new data
      var pet = db.Pets.FirstOrDefault(i => i.Id == id);
      pet.HappinessLevel -= 5;
      db.SaveChanges();
      return pet;
    }
    [HttpPut("{id}/feed")]
    public Pet Foods(int id)
    {
      // might not need new data
      var pet = db.Pets.FirstOrDefault(i => i.Id == id);
      pet.HungarLevel -= 5;
      pet.HungarLevel += 3;
      db.SaveChanges();
      return pet;
    }

    // for my mess
    // [HttpPatch]
    // public Pet Revive(DateTime DeathDate)
    // {
    //   var phoenixDown = db.Pets;
    //   DateTime reserection == null;
    //   phoenixDown.DeathDate = reserection;
    //   db.SaveChanges();
    //   return phoenixDown;
    // }


    [HttpDelete("{id}")]
    public ActionResult DeleteOne(int id)
    {
      var pet = db.Pets.FirstOrDefault(f => f.Id == id);
      if (pet == null)
      {
        return NotFound();
      }
      db.Pets.Remove(pet);
      db.SaveChanges();
      return Ok();
    }


  }
}



