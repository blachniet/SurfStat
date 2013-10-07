﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfStat.Sample.CLI.NuGet
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.Equals(args[0], "modem", StringComparison.OrdinalIgnoreCase) || string.Equals(args[0], "m", StringComparison.OrdinalIgnoreCase))
            {
                var stat = new SurfStat();
                var status = stat.GetModemStatusAsync().Result;
                WriteObject(status);
            }
            else if (string.Equals(args[0], "tria", StringComparison.OrdinalIgnoreCase) || string.Equals(args[0], "t", StringComparison.OrdinalIgnoreCase))
            {
                var stat = new SurfStat();
                var status = stat.GetTriaStatusAsync().Result;
                WriteObject(status);
            }
        }

        static void WriteObject<T>(T obj)
        {
            var stringPropertyNamesAndValues = obj.GetType()
                .GetProperties()
                .Where(pi => pi.GetGetMethod() != null)
                .Select(pi => new
                {
                    Name = pi.Name,
                    Value = pi.GetGetMethod().Invoke(obj, null).ToString()
                });

            foreach (var pair in stringPropertyNamesAndValues)
                Console.WriteLine("{0} : {1}", pair.Name.PadRight(30, ' '), pair.Value);
        }
    }
}
