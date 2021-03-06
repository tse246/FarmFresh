using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmFresh.Model
{
    public class ContextSeeder
    {
        public static void Seed(Context context)
        {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            List<Product> products = new List<Product>()
            {
                new Product(){
                    Name = "Product_1",
                    Detail = "Product_1_Detail",
                    Path = "assets/products/Untitled-1.png"
                },
                new Product(){
                    Name = "Product_2",
                    Detail = "Product_2_Detail",
                    Path = "assets/products/Untitled-2.png"
                },
                new Product(){
                    Name = "Product_3",
                    Detail = "Product_3_Detail",
                    Path = "assets/products/Untitled-3.png"
                },
                new Product(){
                    Name = "Product_4",
                    Detail = "Product_4_Detail",
                    Path = "assets/products/Untitled-4.jpg"
                },
                new Product(){
                    Name = "Product_5",
                    Detail = "Product_5_Detail",
                    Path = "assets/products/Untitled-5.png"
                },
                new Product(){
                    Name = "Product_6",
                    Detail = "Product_6_Detail",
                    Path = "assets/products/Untitled-6.jpg"
                },
                new Product(){
                    Name = "Product_7",
                    Detail = "Product_7_Detail",
                    Path = "assets/products/Untitled-7.png"
                },
                new Product(){
                    Name = "Product_8",
                    Detail = "Product_8_Detail",
                    Path = "assets/products/Untitled-8.png"
                },
                new Product(){
                    Name = "Product_9",
                    Detail = "Product_9_Detail",
                    Path = "assets/products/Untitled-9.png"
                }
            };

            List<Category> categories = new List<Category>()
            {
                new Category(){
                    Name = "On sale!",
                    Description = "On sale!",
                    Value = 1
                },
                new Category(){
                    Name = "New",
                    Description = "New",
                    Value = 2
                },
                new Category(){
                    Name = "Shop by Store",
                    Description = "Shop by Store",
                    Value = 3
                },
                new Category(){
                    Name = "Fruit & Veg",
                    Description = "Fruit & Veg",
                    Value = 4
                },
                new Category(){
                    Name = "Meat & Seafood",
                    Description = "Meat & Seafood",
                    Value = 5
                },
                new Category(){
                    Name = "Dairy and Chilled",
                    Description = "Dairy and Chilled",
                    Value = 6
                },
                new Category(){
                    Name = "Bakery",
                    Description = "Bakery",
                    Value = 7
                },
                new Category(){
                    Name = "Beverages",
                    Description = "Beverages",
                    Value = 8
                }
            };

            List<User> users = new List<User>()
            {
                new User()
                {
                    Username = "admin",
                    Password = "admin",
                    FirstName = "Admin",
                    LastName = "Admin"
                }
            };

            context.Products.AddRange(products);
            context.Category.AddRange(categories);
            context.Users.AddRange(users);
            context.SaveChanges();

            List<Product_Category> product_Categories = new List<Product_Category>()
            {
                new Product_Category()
                {
                    ProductID = 1,
                    CategoryID = 1
                },
                new Product_Category()
                {
                    ProductID = 1,
                    CategoryID = 2
                }
            };
            context.ProductCategories.AddRange(product_Categories);
            context.SaveChanges();
        }
    }
}
