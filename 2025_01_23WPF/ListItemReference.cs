using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2025_01_23WPF
{
     class TodoItem
     {
         public string? Value { get; set; }
         public DateTime? Deadline { get; set; }
         public DateTime? CompletedAt { get; set; }
         public override string ToString()
         {
             return Value ?? "NA";
         }
         public string ToCsv()
         {
             return $"{Value}-{Deadline}-{CompletedAt}";
         }
         public static TodoItem FromCsv(string csv)
         {
             var split = csv.Split('-');
             var op = new TodoItem()
             {
                Value = split[0],
             };
             DateTime date;
             if (DateTime.TryParse(split[1], out date)) op.Deadline = date;
             if (DateTime.TryParse(split[2], out date)) op.CompletedAt = date;
             return op;
         }
     }
}
