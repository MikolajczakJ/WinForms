using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace Firma
{
    [Table(Name ="FakturySprzedazy")]
    internal class FakturaSprzedazy
    {
        [Column(Name = "Id", IsPrimaryKey = true, CanBeNull = false)] public int Id;
        [Column(CanBeNull = false)] public string Numer;
        [Column(CanBeNull = false)] public Double Netto;
        [Column(CanBeNull = true)] public Double? Vat;
        [Column(CanBeNull = true)] public Double? Zaplacono;
        [Column(CanBeNull = false)] public DateTime Data;
        [Column(CanBeNull = false)] public int KontrahentId;
        [Column(CanBeNull = false)] public int PracownikId;

    }
}
