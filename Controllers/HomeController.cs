using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace CRUDelicious{
    public class HomeController : Controller
    {
        private MyContext dbContext;
 
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("/{DishId}")]
        public IActionResult Single_Dish(int DishId)
        {
            Dish Single_Dish = dbContext.dishes.FirstOrDefault(dish => dish.DishId == DishId);
            return View("Single_Dish",Single_Dish);
        }

        [HttpGet("delete/{DishId}")]
        public IActionResult Delete_Dish(int DishId)
        {
            Dish Delete_Dish = dbContext.dishes.FirstOrDefault(dish => dish.DishId == DishId);
            dbContext.dishes.Remove(Delete_Dish);
            dbContext.SaveChanges();
            return RedirectToAction("Show");
        }

        [HttpPost("edit/{DishId}")]
        public IActionResult Edit(int DishId)
        {
            Dish Edit_Dish = dbContext.dishes.FirstOrDefault(dish => dish.DishId == DishId);
            return View(Edit_Dish);
        }

        [HttpPost("update/{DishId}")]
        public IActionResult Update(Dish update_dish,int DishId)
        {
            if(ModelState.IsValid)
            {
                Dish Update_Dish = dbContext.dishes.FirstOrDefault(dishes => dishes.DishId == DishId);
                Update_Dish.Name=update_dish.Name;
                Update_Dish.Chef=update_dish.Chef;
                Update_Dish.Tastiness=update_dish.Tastiness;
                Update_Dish.Calories=update_dish.Calories;
                Update_Dish.Description=update_dish.Description;
                
                
                dbContext.SaveChanges();
                return View("Edit");   
            }
            Console.WriteLine("::::::::::");
            return RedirectToAction("Index");   

        }


        [HttpGet("")]
        public IActionResult Show()
        {
            List<Dish> All_Dishes = dbContext.dishes.OrderByDescending(dish => dish.Created_At).ToList();
            return View(All_Dishes);
        }
        
        [HttpGet("new")]
        public IActionResult New()
        {
            return View("Index");
        }

        [HttpPost("create")]
        public IActionResult Create(Dish dish)
        {
            if(ModelState.IsValid)
            {
                dbContext.dishes.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("New");
            }
            else
            {
                return View("Index");
            }
        }
    }
}