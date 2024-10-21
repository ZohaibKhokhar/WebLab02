using System;
using System.IO;
namespace Lab02
{
    class Product
    {
        private int productId;
        private string productName;
        private double price;

        public Product()
        {
            productId = 0;
            productName= string.Empty;
            price = 0;
        }
        public Product(int id,string name,double p)
        {
            productId= id;
            productName= name;
            Price = p;
        }

        public int ProductId
        { get { return productId; } set { productId = value; } }

        public string ProductName
            { get { return productName; } set {productName = value; } }

        public double Price
            { get { return price; } set { price = value; } }

        public virtual void getProductInfo()
        {
            Console.WriteLine($"Product Name : {productName} Price : {Price}");
        }
        
    }

    class Electronics:Product
    {
        private int warrantyPeriod;

        public int WarrantyPeriod
        { 
            get { return warrantyPeriod; } 
            set { warrantyPeriod = value; } 
        }

        public override void getProductInfo()
        {
            Console.WriteLine($"Name : {ProductName} Price : {Price} Warranty Period : {warrantyPeriod} years");
        }
    }

    class Groceries:Product
    {
        private string expiryDate;

        public string ExpiryDate 
        { set { expiryDate = value; } get { return expiryDate; } }

        public override void getProductInfo()
        {
            Console.WriteLine("Name : "+ProductName+" Price : "+Price+" Expiry Date : "+ExpiryDate);
        }
    }

    class ProductService
    {
        public void savetoFile(Electronics product,string filepath)
        {
            using (FileStream fileStream = new FileStream(filepath, FileMode.Append))
            {
                using (StreamWriter sr=new StreamWriter(fileStream))
                {
                    sr.WriteLine(product.ProductId);
                    sr.WriteLine(10);
                    sr.WriteLine(product.ProductName);
                    sr.WriteLine(product.Price);
                    sr.WriteLine(product.WarrantyPeriod);
                }
            }
        }
        public void savetoFile(Groceries product, string filepath)
        {
            using (FileStream fileStream = new FileStream(filepath, FileMode.Append))
            {
                using (StreamWriter sr = new StreamWriter(fileStream))
                {
                    sr.WriteLine(product.ProductId);
                    sr.WriteLine(5);
                    sr.WriteLine(product.ProductName);
                    sr.WriteLine(product.Price);
                    sr.WriteLine(product.ExpiryDate);
                }
            }
        }
        public void getProductById(string filepath, int id)
        {
            using (FileStream filestream = new FileStream(filepath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(filestream))
                {
                    bool flag = true;
                    int pId = -1;
                    do
                    {
                        pId = int.Parse(sr.ReadLine());
                        int code = int.Parse(sr.ReadLine());
                        string name = sr.ReadLine();
                        double price = double.Parse(sr.ReadLine());
                        int months = 0;
                        string date = string.Empty;
                        if (code == 10)
                        {
                            months = int.Parse(sr.ReadLine());
                        }
                        else
                        {
                            date = sr.ReadLine();
                        }
                        if (pId == id && code == 5)
                        {
                            flag = false;
                            Console.WriteLine($"Name : {name} Price : {price} Expiry Date : {date}");
                        }
                        else if (pId == id)
                        {
                            flag = false;
                            Console.WriteLine($"Name : {name} Price : {price} Warranty : {months} months");
                        }

                    } while (sr.Peek() != -1 && flag); // Continue until end of file or flag is false
                    if (flag)
                        Console.WriteLine("Product not found \n");
                }
            }
        }


        public void ListAllProducts(string filepath)
        {
            using (FileStream filestream = new FileStream(filepath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(filestream))
                {
                    int pId = -1;
                    while (sr.Peek() != -1)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        pId = int.Parse(line);

                        line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        int code = int.Parse(line);

                        string name = sr.ReadLine();
                        if (string.IsNullOrEmpty(name))
                            continue;

                        line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        double price = double.Parse(line);

                        int months = 0;
                        string date = string.Empty;

                        if (code == 10)
                        {
                            line = sr.ReadLine();
                            if (string.IsNullOrEmpty(line))
                                continue;

                            months = int.Parse(line);
                        }
                        else
                        {
                            date = sr.ReadLine();
                            if (string.IsNullOrEmpty(date))
                                continue;
                        }

                        if (code == 10)
                            Console.WriteLine($"Id : {pId} Name : {name} Price : {price} Warranty : {months} months");
                        else if (code == 5)
                            Console.WriteLine($"Id : {pId} Name : {name} Price : {price} Expiry Date : {date}");
                    }
                }
            }
        }

    }

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Product Management System ");
            ProductService productService=new ProductService();
            
            int choice = 0;
            do
            {
                do
                {
                    Console.WriteLine("Choose a product type to add : ");
                    Console.WriteLine("1.Groceries");
                    Console.WriteLine("2.Electronics");
                    Console.WriteLine("3.Exit");
                    choice=int.Parse(Console.ReadLine());

                } while (choice != 1 && choice != 2 && choice != 3);
                if (choice == 1)
                {
                    Groceries grocery = new Groceries();
                    Console.WriteLine("Enter Product Id : ");
                    grocery.ProductId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Product Name : ");
                    grocery.ProductName = Console.ReadLine();
                    Console.WriteLine("Enter Product Price : ");
                    grocery.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Expiry date : ");
                    grocery.ExpiryDate = Console.ReadLine();
                    productService.savetoFile(grocery, "product.txt");
                }
                else if(choice==2)
                {
                    Electronics electronics = new Electronics();
                    Console.WriteLine("Enter Product Id : ");
                    electronics.ProductId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Product Name : ");
                    electronics.ProductName = Console.ReadLine();
                    Console.WriteLine("Enter Product Price : ");
                    electronics.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Warranty Period (months) : ");
                    electronics.WarrantyPeriod = int.Parse(Console.ReadLine());
                    productService.savetoFile(electronics, "product.txt");
                }
            } while (choice!=3);
          
       
            Console.WriteLine("List all Products\n");
            productService.ListAllProducts("product.txt");
            Console.WriteLine("Retrive by Product Id  ");
            Console.Write("Enter the id : ");
            int id = int.Parse(Console.ReadLine());
            productService.getProductById("product.txt", id);
        }
    }

}