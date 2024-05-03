using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Firma
{
    [Table(Name = "Kontrahenci")]

    internal class Kontrahent
    {
        [Column(Name = "Id", IsPrimaryKey = true, CanBeNull = false)] public int Id;
        [Column(CanBeNull = false)] public string Nazwa;
        [Column(CanBeNull = true)] public string NIP;
        [Column(CanBeNull = false)] public string Ulica;
        [Column(CanBeNull = false)] public string Miasto;
    }
}
