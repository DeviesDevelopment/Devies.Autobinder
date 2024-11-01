using Consumer.Data;
using Microsoft.AspNetCore.Mvc;
using Devies.Autobinder;

namespace Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(
    ConsumerDbContext db,
    ILogger<ProductsController> logger) : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<Product>> List()
    {
        return db.Products.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById([Autobind(ParamName = "id")]int id)
    {
        return db.Products.Single(p => p.Id == id);
    }
}
