using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Net.Http;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace EbayCrawler
{
    class Program
    {

        static void Main(string[] args)
        {
            Crawler_Login();
            //catch_product();
        }
        protected static void catch_product()
        {
            #region Main
            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                #region All Lists
                List<String> Name = new List<string>();
                List<String> Price = new List<string>();
                List<String> From = new List<string>();
                //List<String> Sold = new List<string>();
                #endregion
                #region Catcher
                for (int i = 1; i <= 10; i++)
                {
                    HtmlDocument document = htmlWeb.Load($"https://www.ebay.com/sch/i.html?_from=R40&_trksid=p2380057.m570.l1313&_nkw=laptop&_sacat=0&_pgn={i}");
                    HtmlNode[] product_Name = document.DocumentNode.SelectNodes("//h3[@class = 's-item__title']").ToArray(); // Product Name (String)
                    HtmlNode[] product_price = document.DocumentNode.SelectNodes("//span[@class = 's-item__price']").ToArray(); ; // Product Price (String)                                                                                                  //HtmlNode[] product_sold = document.DocumentNode.SelectNodes("//span[@class = 'BOLD NEGATIVE']").ToArray(); ; // Product Sold (String)
                    HtmlNode[] product_from = document.DocumentNode.SelectNodes("//span[@class = 's-item__location s-item__itemLocation'] ").ToArray(); // Product Location (String)

                    foreach (HtmlNode name in product_Name)
                    {
                        Name.Add(name.InnerText);
                    }
                    foreach (HtmlNode price in product_price)
                    {
                        Price.Add(price.InnerText);
                    }
                    foreach (HtmlNode from in product_from)
                    {
                        From.Add(from.InnerText);
                    }
                    //foreach (HtmlNode sold in product_sold)
                    //{
                    //    Sold.Add(sold.InnerText);
                    //}
                }
                #endregion

                try
                {
                    using (StreamWriter wrtier = new StreamWriter("Ebay Product.txt"))
                    {
                        var get_list = Name.Zip(Price, (n, p) => new { n, p }).Zip(From, (f, s) => new { Product =  f.n ,Price = f.p , Location =  s});
                       
                        //Console.WriteLine("Printing is complete", Console.ForegroundColor = ConsoleColor.Green);
                        foreach (var x  in get_list)
                        {
                            wrtier.WriteLine(string.Join(", ", new[] { (object)x.Product, x.Price, x.Location}));
                            Console.WriteLine(string.Join(", ", new[] { (object)x.Product, x.Price, x.Location }));
                        }
                        Console.WriteLine("! Ok ! ");
                    }
                 }
                 catch (Exception)
                 {
                    throw;
                 }
            }
            catch (Exception MAIN_Error)
            {
                MAIN_Error.ToString();
                Console.Write($"MAIN ERROR :     {MAIN_Error}");          
            }
            #endregion
        }
        protected static void Login() // Start program
        {
            string choice;
            string menu_desing = @"
            |=============================|
            |    EBAY PRODUCT CRAWLER     |
            |=============================|
            |     (1- Start Crawler)      |
            |=============================|
            |     (2- Exit Program)       |
            |=============================|
            ";
            Console.WriteLine(menu_desing);
            Console.Write("Select : ");
            choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Crawler_Login();
            }
            else if (choice == "2")
            {
                Environment.Exit(0);
            }
        }
        protected static void Crawler_Login() // Crawler Main Desing 
        {
            
            string page;
            string name;
            string crawler_menu = @"
              _______ .______        ___   ____    ____         _______.  ______ .______          ___      .______   .______    _______ .______      
             |   ____||   _  \      /   \  \   \  /   /        /       | /      ||   _  \        /   \     |   _  \  |   _  \  |   ____||   _  \     
             |  |__   |  |_)  |    /  ^  \  \   \/   /        |   (----`|  ,----'|  |_)  |      /  ^  \    |  |_)  | |  |_)  | |  |__   |  |_)  |    
             |   __|  |   _  <    /  /_\  \  \_    _/          \   \    |  |     |      /      /  /_\  \   |   ___/  |   ___/  |   __|  |      /     
             |  |____ |  |_)  |  /  _____  \   |  |        .----)   |   |  `----.|  |\  \----./  _____  \  |  |      |  |      |  |____ |  |\  \----.
             |_______||______/  /__/     \__\  |__|        |_______/     \______|| _| `._____/__/     \__\ | _|      | _|      |_______|| _| `._____|
                                                                                                                                                   
";
            Console.WriteLine(crawler_menu, Color.DeepSkyBlue, 20);
            Console.Write("Search Product : ", Color.YellowGreen);
            name = Console.ReadLine();
            Console.Write("How many pages ? : ");
            page = Console.ReadLine();
        }
    }
    
}

