using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cars
{
    internal class Program
    {
        private static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());

            InsertData();
            QueryData();


            //var cars = ProcessCars("fuel.csv");
            //var manufacturers = ProcessManufacturers("manufacturers.csv");

            //CreatXml();

            //QueryXml();

            #region Grouping

            //var query = from car in cars
            //            group car by car.Manufacturer.ToUpper() into manufacture
            //            orderby manufacture.Key
            //            select manufacture;

            //var queryGroup =
            //    from manufacturer in manufacturers
            //    join car in cars on manufacturer.Name equals car.Manufacturer
            //        into carGroup
            //    select new
            //    {
            //        Manufactuere = manufacturer,
            //        Cars = carGroup
            //    }
            //    into result
            //    group result by result.Manufactuere.Headquarter;


            //var query2 =
            //    cars.GroupBy(c => c.Manufacturer.ToUpper())
            //        .OrderBy(c => c.Key);

            //var queryGroupJoin =
            //    manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
            //            (m, g) =>
            //                new
            //                {
            //                    Manufactuere = m,
            //                    Cars = g
            //                })
            //        .GroupBy(c => c.Manufactuere.Headquarter);


            //var queryAggregate =
            //    from car in cars
            //    group car by car.Manufacturer
            //    into carGroup
            //    select new
            //    {
            //        Name = carGroup.Key,
            //        Max = carGroup.Max(c => c.Combined),
            //        Min = carGroup.Min(c => c.Combined),
            //        Average = carGroup.Average(c => c.Combined)
            //    }
            //    into result
            //    orderby result.Max descending
            //    select result;



            //var ExpresionAggregate =
            //    cars.GroupBy(c => c.Manufacturer)
            //        .Select(g =>
            //        {
            //            var result = g.Aggregate(new CarStatistics(), (acc, c) => acc.Accumulate(c),
            //                acc => acc.Compute());

            //            return new
            //            {
            //                Name = g.Key,
            //                Average = result.Average,
            //                Max = result.Max,
            //                Min = result.Min

            //            };
            //        })
            //        .OrderByDescending(r => r.Max);

            //foreach (var result in ExpresionAggregate)
            //{
            //    Console.WriteLine($"{result.Name}");
            //    Console.WriteLine($"\t Max: {result.Max}");
            //    Console.WriteLine($"\t Min: {result.Min}");
            //    Console.WriteLine($"\t Avg: {result.Average}");
            //}

            //foreach (var group in queryGroupJoin)
            //{
            //    Console.WriteLine($"{group.Manufactuere.Name} : {group.Manufactuere.Headquarter}");
            //    foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(3))
            //    {
            //        Console.WriteLine($"\t {car.Name} : {car.Combined}");
            //    }
            //}


            //foreach (var group in queryGroup)
            //{
            //    Console.WriteLine($"{group.Key}");
            //    foreach (var car in group.SelectMany(c => c.Cars)
            //                             .OrderByDescending(c => c.Combined)
            //                             .Take(3))
            //    {
            //        Console.WriteLine($"\t {car.Name} : {car.Combined}");
            //    }
            //}

            #endregion

            #region Join

            //var orderCar = from car in cars
            //               join manufacturer in manufacturers
            //                   on
            //                   new { car.Manufacturer, car.Year }
            //                   equals
            //                   new { Manufacturer = manufacturer.Name, manufacturer.Year }
            //               orderby car.Combined descending, car.Name
            //               select new
            //               {
            //                   manufacturer.Headquarter,
            //                   car.Name,
            //                   car.Combined
            //               };



            //var orderExtMth =
            //    cars.Join(manufacturers,
            //            c => new { c.Manufacturer, c.Year }, m => new { Manufacturer = m.Name, m.Year }, (c, m) => new
            //            {
            //                m.Headquarter,
            //                c.Name,
            //                c.Combined
            //            })
            //        .OrderByDescending(c => c.Combined)
            //        .ThenBy(c => c.Name);

            //foreach (var car in orderExtMth.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarter} {car.Name} : {car.Combined}");
            //}

            #endregion

            #region Select Order

            //foreach (var car in cars)
            //{
            //    Console.WriteLine(car.Name);

            //}

            //var orderCar = cars.OrderByDescending(c => c.Combined)
            //                   .ThenBy(c => c.Name);

            //var orderCar = from car in cars
            //               where car.Manufacturer == "BMW" && car.Year == 2016
            //               orderby car.Combined descending, car.Name
            //               select new
            //               {
            //                   car.Name,
            //                   car.Manufacturer,
            //                   car.Year
            //               };

            //var query = cars
            //                .Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //                .OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name)
            //                .Select(c => c);

            //var top = cars
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name)
            //    .Select(c => c)
            //    .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016);

            //if (top != null) Console.WriteLine(top.Name);


            //var resultIfAny = cars.Any(c => c.Manufacturer == "BMW");
            //var resultIfAll = cars.Any(c => c.Manufacturer == "Ford");

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Name} : {car.Combined}");
            //}

            //var result = cars.Select(c => c.Name);
            //foreach (var name in result)
            //{
            //    foreach (var character in name)
            //    {
            //        Console.WriteLine(character);
            //    }
            //} 

            //var result = cars.SelectMany(c => c.Name)
            //    .OrderBy(c => c);

            //foreach (var character in result)
            //{
            //    Console.WriteLine(character);
            //}

            #endregion
        }

        private static void QueryData()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            #region Complex

            // query syntex
            var query2 =
                from car in db.Cars
                group car by car.Manufacturer
                into manufacturer
                select new
                {
                    Name = manufacturer.Key,
                    Cars = (from car in manufacturer
                            orderby car.Combined descending
                            select car).Take(2)
                };

            // Extention method syntex
            var query = db.Cars.GroupBy(c => c.Manufacturer)
                .Select(g => new
                {
                    Name = g.Key,
                    Cars = g.OrderByDescending(c => c.Combined).Take(2)
                });

            foreach (var group in query2)
            {
                Console.WriteLine(group.Name);
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t {car.Name} : {car.Combined}");
                }
            }

            #endregion

            #region Simple

            //var query = from car in db.Cars
            //    orderby car.Combined descending, car.Name
            //    select (car);

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Name} : {car.Combined}");
            //}
            //var query = db.Cars.Where(c => c.Manufacturer == "BMW")
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name)
            //    .Take(10) // without tolist it run on the sql  IEnumeramble
            //    .Select(c=> new {Name = c.Name.ToUpper()})
            //    .ToList(); // with tolist rum in the memory  IQueryable

            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.Name);
            //}

            //foreach (var car in query)
            //{
            //    Console.WriteLine($"{car.Name} : {car.Combined}");
            //}


            #endregion

        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryXml()
        {
            var documents = XDocument.Load("Fuel.xml");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";

            var query =
                //from element in documents.Descendants("Cars")
                from element in documents.Element(ns + "Cars")?.Elements(ex + "Car")
                                                              ?? Enumerable.Empty<XElement>()
                where element.Attribute("Manufucturer")?.Value == "BMW"
                select element.Attribute("Name")?.Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);

            }
        }

        private static void CreatXml()
        {
            var records = ProcessCars("fuel.csv");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var documents = new XDocument();
            var cars = new XElement(ns + "Cars",
                from record in records
                select new XElement(ex + "Car",
                    new XAttribute("Name", record.Name),
                    new XAttribute("combiend", record.Combined),
                    new XAttribute("Manufucturer", record.Manufacturer))
            );

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));
            //foreach (var record in records)
            //{
            //    var car = new XElement("Car");

            //    //var name = new XElement("Name", record.Name);
            //    //var combiend = new XElement("combiend", record.Combined);

            //    var name = new XAttribute("Name", record.Name);
            //    var combiend = new XAttribute("combiend", record.Combined);

            //    car.Add(name);
            //    car.Add(combiend);

            //    cars.Add(car);
            //}

            //foreach (var record in records)
            //{
            //    var car = new XElement("Car",
            //            new XAttribute("Name", record.Name),
            //            new XAttribute("combiend", record.Combined),
            //            new XAttribute("Manufucturer", record.Manufacturer)
            //        );

            //    cars.Add(car);
            //}

            documents.Add(cars);
            documents.Save("Fuel.xml");
        }

        private static List<Manufaturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(m => m.Length > 1)
                    .Select(m =>
                    {
                        var columns = m.Split(',');
                        return new Manufaturer
                        {
                            Name = columns[0],
                            Headquarter = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });

            return query.ToList();
        }

        private static List<Car> ProcessCars(string path)
        {
            var query = File.ReadAllLines(path)
                 .Skip(1)
                 .Where(line => line.Length > 1)
                 .ToCar();

            return query.ToList();

            //return
            //    File.ReadAllLines(path)
            //        .Skip(1)
            //        .Where(line => line.Length > 1)
            //        .Select(Car.ParseFromCsv)
            //        .ToList();

            //var cars =
            //    from line in File.ReadAllLines(path).Skip(1)
            //    where line.Length > 1
            //    select (Car.ParseFromCsv(line));

            //return cars.ToList();


        }
    }
}
