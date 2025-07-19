namespace BlazingPizza.Data;

public class PizzaService
{
    public Task<Pizza[]> GetPizzasAsync()
    {
        return Task<Pizza[]>.Factory.StartNew(() => new Pizza[]
        {
            // new Pizza { PizzaId = 0, Name = "The Baconatorizor", Price =  11.99M, Description = "It has EVERY kind of bacon", Vegetarian=false, Vegan=true},
            // new Pizza { PizzaId =1, Name = "Buffalo chicken", Price =  12.75M, Description = "Spicy chicken, hot sauce, and blue cheese, guaranteed to warm you up", Vegetarian=true, Vegan=true},
        });
    }
}
