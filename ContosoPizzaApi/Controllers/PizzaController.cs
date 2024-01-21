using ContosoPizzaApi.Models;
using ContosoPizzaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizzaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
        
    }

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();
    
    [HttpGet("{id:int}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza is null)
            return NotFound();

        return pizza;
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new {Id=pizza.Id}, pizza);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
        
        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();
        
        PizzaService.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        
        if(pizza is null)
            return NotFound();
        
        PizzaService.Delete(id);

        return NoContent();
    }
}
