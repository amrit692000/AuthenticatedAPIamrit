using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using APIauthent.Dbcontext;
using authentLib;

[Authorize] 
[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly ApplicationDbContext cont;

    public ShoppingCartController(ApplicationDbContext context)
    {
        cont = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var userEmail = User.Identity.Name;
        var userCartItems = cont.ShoppingCarts
      .Where(cart => cart.User == userEmail)
       .SelectMany(cart => cart.Products)
        .ToList();
        return Ok(userCartItems);
    }

    [HttpPost("RemoveItem")]
    public IActionResult RemoveItem(int productId)
    {
        var userEmail = User.Identity.Name;
        var userCart = cont.ShoppingCarts.FirstOrDefault(cart => cart.User == userEmail);
        var productToRemove = userCart?.Products.FirstOrDefault(p => p.Id == productId);
        if (productToRemove != null)
        {
            userCart.Products.Remove(productToRemove);
            cont.SaveChanges();
            return Ok();
        }
        return NotFound();
    }

    [HttpPost("AddItem")]
    public IActionResult AddItem(int productId)
    {
        var userEmail = User.Identity.Name;
        var userCart = cont.ShoppingCarts.FirstOrDefault(cart => cart.User == userEmail)
                       ?? new ShoppingCart { User = userEmail };

        var productToAdd = cont.Products.FirstOrDefault(p => p.Id == productId);
        if (productToAdd != null)
        {
            userCart.Products.Add(productToAdd);
            cont.SaveChanges();
            return Ok();
        }
        return NotFound();
    }
}
