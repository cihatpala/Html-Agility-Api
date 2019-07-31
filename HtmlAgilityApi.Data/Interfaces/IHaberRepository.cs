using HtmlAgilityApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAgilityApi.Data.Interfaces
{
    public interface IHaberRepository
    {
        List<Haber> GetTumHaberler();
        Haber GetHaber(int id);
    }
}
