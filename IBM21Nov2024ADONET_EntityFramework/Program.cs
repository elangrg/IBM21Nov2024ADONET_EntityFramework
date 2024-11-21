using IBM21Nov2024ADONET_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM21Nov2024ADONET_EntityFramework
{
    internal class Program
    {
        static void Main()
        {


            Models.IBM08Nov2024DbEntities _dbctx = new Models.IBM08Nov2024DbEntities();

            int choice = -1;

            do
            {

                Console.Clear();


                Console.WriteLine("1. List Product(Connection & Command with Reader)\n2. Add New Product\n3. Update Product By ID \n4. Delete Product By ID \n5. Invoke Stored Proc   \n0. Exit ");

                Console.Write("Enter Choice :");
                choice = int.Parse(Console.ReadLine());


                if (choice == 1)
                {
                    ListProductsUsingConComRdr(_dbctx);

                }

                if (choice == 2)
                {
                    AddNewProduct(_dbctx);

                }

                if (choice == 3)
                {
                    UpdateProduct(_dbctx);

                }


                if (choice == 4)
                {
                    DeleteProduct(_dbctx);

                }


                if (choice == 5)
                {
                    StoredProcEg(_dbctx);

                }




            }
            while (choice != 0);





        }

        private static void StoredProcEg(IBM08Nov2024DbEntities _db)
        {
            Console.Write("Enter Product ID :");
                int prdId = int.Parse(Console.ReadLine());

            ObjectParameter prm = new ObjectParameter("prdname", "");

            _db.GetProductNameByID(prdId, prm);

            if (prm.Value!=null)
            {

                Console.WriteLine($"Product Name: {prm.Value}");
            }

            Console.WriteLine("Press Any key to continue...");
            Console.ReadKey();

        }

        private static void ListProductsUsingConComRdr(IBM08Nov2024DbEntities db)
        {


            Console.WriteLine("Product ID".PadLeft(12, ' ') + "  Product Name".PadRight(32, ' ') + "Quantity".PadLeft(12, ' ') + "Rate".PadLeft(12, ' '));


            Console.WriteLine("  ".PadRight(12, '_') + "  ".PadRight(32, '_') + "  ".PadRight(12, '_')  + "  ".PadRight (12, '_') );

            foreach (var item in db.Products)
            {
                Console.WriteLine(
                        $"{item.ProductID.ToString().PadLeft(12, ' ')}  {item.ProductName.PadRight(30, ' ')}{item.Quantity.ToString().PadLeft(12, ' ')}{item.Rate.ToString().PadLeft(12, ' ')}");
            }


            Console.WriteLine("Press Any key to continue...");
            Console.ReadKey();

        }

        private static void AddNewProduct(IBM08Nov2024DbEntities _db)
        {
            Models.Product product = new Models.Product();
            Console.Clear();
            Console.WriteLine("Add New Product ");

            Console.Write("Enter Product Name:");
            product.ProductName = Console.ReadLine();

            Console.Write("Enter Quantity:");
            product.Quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Rate:");
            product.Rate = int.Parse(Console.ReadLine());


            _db.Products.Add(product);
            _db.SaveChanges();


            Console.WriteLine("Inserted Successfully....");


            Console.WriteLine("Press Any key to continue...");
            Console.ReadKey();
        }


        private static void UpdateProduct(IBM08Nov2024DbEntities _db)
        {
            Product product;
            SeekProduct(_db,out product);

            if (product==null)
            {
                return;
            }

            //Console.Clear();
            Console.WriteLine("Edit Product Details ");

            Console.Write("Enter Updated Product Name:");
            product.ProductName = Console.ReadLine();

            Console.Write("Enter Updated Quantity:");
            product.Quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Updated Rate:");
            product.Rate = int.Parse(Console.ReadLine());



            _db.SaveChanges();


            Console.WriteLine("Update Successfully....");


            Console.WriteLine("Press Any key to continue...");
            Console.ReadKey();
        }


        private static void DeleteProduct(IBM08Nov2024DbEntities _db)
        {
            Product product;
            SeekProduct(_db, out product);

            if (product == null)
            {
                return;
            }

           
            Console.Write("Are you Sure, Delete (Y/N)?");

           
            string rst= Console.ReadLine();

            if (rst.ToUpper()=="N")
            {
                return;


            }

            _db.Products.Remove(product);


            _db.SaveChanges();


            Console.WriteLine("Deleted Successfully....");


            Console.WriteLine("Press Any key to continue...");
            Console.ReadKey();
        }

        private static void SeekProduct(IBM08Nov2024DbEntities _db, out Product product)
        {
            Console.Clear();
            Console.Write("Enter  Product Name Containing :");
            string productName = Console.ReadLine();

            var qry = _db.Products.Where(p => p.ProductName.Contains(productName));

            // no rec found
            if (qry.Count() == 0) { Console.WriteLine("No Records Found...\n Press Any Key to Continue"); Console.ReadKey(); product = null; return ; }

            // List Product(s)
            Console.WriteLine("Product ID".PadLeft(12, ' ') + "  Product Name".PadRight(32, ' ') + "Quantity".PadLeft(12, ' ') + "Rate".PadLeft(12, ' '));
            Console.WriteLine("  ".PadRight(12, '_') + "  ".PadRight(32, '_') + "  ".PadRight(12, '_') + "  ".PadRight(12, '_'));

            qry.ToList().ForEach(item =>
            {
                Console.WriteLine(
                        $"{item.ProductID.ToString().PadLeft(12, ' ')}  {item.ProductName.PadRight(30, ' ')}{item.Quantity.ToString().PadLeft(12, ' ')}{item.Rate.ToString().PadLeft(12, ' ')}");
            });

            if (qry.Count() > 1)
            {

                Console.Write("Enter Product ID To Edit :");
                int prdId = int.Parse(Console.ReadLine());

                product = _db.Products.SingleOrDefault(p => p.ProductID == prdId);

            }
            else
            {
                product = qry.FirstOrDefault();
            }
        }
    }
}
