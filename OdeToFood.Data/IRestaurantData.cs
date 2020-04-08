using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updateRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Crinul Alb", Location = "Oradea", Cuisine= CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Marty's", Location = "Cluj", Cuisine= CuisineType.Indian },
                new Restaurant { Id = 3, Name = "Eggcetera", Location = "Timisoara", Cuisine= CuisineType.Mexican },
                new Restaurant { Id = 4, Name = "Colloseum", Location = "Bucuresti", Cuisine= CuisineType.Indian },
                new Restaurant { Id = 5, Name = "KFC", Location = "Bucuresti", Cuisine= CuisineType.None },
                new Restaurant { Id = 6, Name = "Cocina", Location = "Timisoara", Cuisine= CuisineType.Italian },
                new Restaurant { Id = 7, Name = "MC", Location = "Bucuresti", Cuisine= CuisineType.Mexican },
                new Restaurant { Id = 8, Name = "Muie", Location = "Cluj", Cuisine= CuisineType.None },
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location ;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
    }


}
