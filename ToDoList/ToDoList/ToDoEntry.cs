using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class ToDoEntry
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueTime { get; set; }

        public ToDoEntry()
        {
            
        }
    }
}
