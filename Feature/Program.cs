using System;
using System.Collections.Generic;
using System.Linq;


namespace Feature
{
    internal class Program
    {
        private static void Main()
        {
            Func<int, int> squer = x => x * x;

            Func<int, int, int> add = (x, y) => x + y;

            Func<int, int, int> addwithMethod = (x, y) =>
            {
                var temp = x + y;
                return temp;
            };

            Console.WriteLine(squer(add(3, 5)));


            Action<int> write = x => Console.WriteLine(x);
            write(squer(add(3, 5)));


            //var developers = new[]
            //{
            //    new Employee {Id = 1, Name = "Mohamed"},
            //    new Employee {Id = 2, Name = "Amer"}
            //};

            //var sales = new List<Employee>
            //{
            //    new Employee {Id = 3, Name = "Ali"}
            //};   


            IEnumerable<Employee> developers = new[]
            {
                new Employee {Id = 1, Name = "Mohamed"},
                new Employee {Id = 2, Name = "Amer"}
            };

            IEnumerable<Employee> sales = new List<Employee>
            {
                new Employee {Id = 3, Name = "Ali"}
            };

            Console.WriteLine(developers.Count());

            IEnumerator<Employee> enumerable = sales.GetEnumerator();
            while (enumerable.MoveNext())
            {
                Console.WriteLine(enumerable.Current.Name);

            }

            // Deffrient between Named, Anoumnuse , Lambda Method
            //Named Method
            foreach (var employee in developers.Where(NameStartWithM))
            {
                Console.WriteLine(employee.Name);
            }


            ////Anoumenace Method
            //foreach (var employee in developers.Where(
            //    delegate (Employee employee)
            //     {
            //         return employee.Name.StartsWith("M");

            //    }))
            //{
            //    Console.WriteLine(employee.Name);
            //}

            //Lambda Expretion
            //foreach (var employee in developers.Where(e => e.Name.StartsWith("A")))
            //{
            //    Console.WriteLine(employee.Name);
            //}
            var query = developers.Where(e => e.Name.Length == 2)
                                  .OrderBy(e => e.Name);

            var query2 = from developer in developers
                where developer.Name.Length == 5
                orderby developer.Name
                select developer;

            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);
            }

        }
        private static bool NameStartWithM(Employee employee)
        {
            return employee.Name.StartsWith("M");
        }

    }
}
