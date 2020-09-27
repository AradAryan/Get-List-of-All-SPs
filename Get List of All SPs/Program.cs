using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Get_List_of_All_SPs
{
    class Program
    {
        static void Main(string[] args)
        {
            var listDir = Directory.GetFiles(@"C:\Users\javad\Desktop", "*.Context.cs");
            List<string> list = new List<string>();
            string name = "", param = "";
            foreach (var item in listDir)
            {
                var lines = File.ReadAllLines(item);
                foreach (var line in lines)
                {
                    //  names.Add(name);
                    if (line.Contains("public virtual ObjectResult<"))
                    {
                        param = line.Split('(')[1].Split(')')[0];
                        param = param.Replace("Nullable<", "").Replace(">", "");
                    }
                    if (line.Contains("ExecuteFunction"))
                    {
                        name = line.Split('(')[3].Split('\"')[1];
                        if (!string.IsNullOrEmpty(param))
                            list.Add($"{name},{param}");
                        else
                            list.Add($"{name}");
                    }
                }
            }
            File.AppendAllLines(@"C:\Users\javad\Desktop\Sp.txt", list);
            Console.ReadKey();
        }
    }
}
