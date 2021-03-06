﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using Console = Colorful.Console;
using System.Drawing;
// CREATED AND DESING BY FLEAKER 131

namespace EbayCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Login();
        }
        protected static void catch_product(string product_searcher, int page)
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
                for (int i = 1; i <= page; i++)
                {
                    HtmlDocument document = htmlWeb.Load($"https://www.ebay.com/sch/i.html?_from=R40&_trksid=p2380057.m570.l1313&_nkw={product_searcher}&_sacat=0&_pgn={i}");
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
                       
                        
                        foreach (var x  in get_list)
                        {
                            wrtier.WriteLine(string.Join(", ", new[] { (object)x.Product, x.Price, x.Location}));
                            Console.WriteLine(string.Join(", ", new[] { (object)x.Product, x.Price, x.Location })); 
                        }
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
            int r = 75;
            int g = 190;
            int b = 242;
            string choice;
            string crawler_menu = @"
          |------------------------------------------------------------------------------------------|
          |     ______ ____   ___ __  __   _____  ______ ____   ___     ____   ____   ______ ____    |
          |    / ____// __ ) /   |\ \/ /  / ___/ / ____// __ \ /   |   / __ \ / __ \ / ____// __ \   |
          |   / __/  / __  |/ /| | \  /   \__ \ / /    / /_/ // /| |  / /_/ // /_/ // __/  / /_/ /   |
          |  / /___ / /_/ // ___ | / /   ___/ // /___ / _, _// ___ | / ____// ____// /___ / _, _/    |
          | /_____//_____//_/  |_|/_/   /____/ \____//_/ |_|/_/  |_|/_/    /_/    /_____//_/ |_|     |
          |                                                                                          |
          |------------------------------------------------------------------------------------------|
          |                                     (OPTIONS)                                            |
          |------------------------------------------------------------------------------------------|
          |                                 1(START SCRAPPER)1                                       |
          |------------------------------------------------------------------------------------------|
          |                                  2(QUIT PROGRAM)2                                        |
          |------------------------------------------------------------------------------------------|
          |                              (COMMAND EXAMPLE => SELECT : 1)                             |
          |                   (Do not scan more than 200 pages, the program may crash.)              |
          |                                    (FLEAKER 131)                                         |
          |------------------------------------------------------------------------------------------|


";

            Console.WriteLine(crawler_menu, Color.FromArgb(r,g,b));
            Console.Write("Select => ");
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
            int r = 75;
            int g = 190;
            int b = 242;
            string page;
            string name;
            string crawler_menu = @"
          |------------------------------------------------------------------------------------------|
          |     ______ ____   ___ __  __   _____  ______ ____   ___     ____   ____   ______ ____    |
          |    / ____// __ ) /   |\ \/ /  / ___/ / ____// __ \ /   |   / __ \ / __ \ / ____// __ \   |
          |   / __/  / __  |/ /| | \  /   \__ \ / /    / /_/ // /| |  / /_/ // /_/ // __/  / /_/ /   |
          |  / /___ / /_/ // ___ | / /   ___/ // /___ / _, _// ___ | / ____// ____// /___ / _, _/    |
          | /_____//_____//_/  |_|/_/   /____/ \____//_/ |_|/_/  |_|/_/    /_/    /_____//_/ |_|     |
          |                                                                                          |
          |------------------------------------------------------------------------------------------|";
            Console.WriteLine(crawler_menu, Color.FromArgb(r, g, b));
            Console.WriteLine("\n");
            Console.Write("Search Product : ", Color.YellowGreen);
            name = Console.ReadLine();
            Console.Write("How many pages ? : ", Color.YellowGreen);
            page = Console.ReadLine();
            Convert.ToInt16(page);
            catch_product(product_searcher: name, page: Convert.ToInt16(page));
        }
    }
    
}

