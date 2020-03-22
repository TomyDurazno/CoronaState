using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using static CoronaManager.Models.Continent;

namespace CoronaManager.Models
{
    public class ViewState
    {
        public List<Tab> Tabs { get; set; }
    }

    public class Tab
    {
        public string FriendlyName { get; set; }

        public string Name { get => Continent.ToString(); }

        public string Href { get => $"{Name}_href"; }

        public string Id { get => $"{Name}_id"; }
        
        public List<RowChart> Rows { get; set; }

        public bool IsActive { get; set; }

        public string Active => IsActive ? "active" : "";

        public string Visibility => IsActive ? "show" : "hidden";

        public Continents Continent { get; set; }

        public Tab(string name, Continents continent)
        {
            Rows = new List<RowChart>();            
            Continent = continent;
            FriendlyName = name;
        }
    }

    public class RowChart
    {
        public Chart One { get; set; }
        public Chart Two { get; set; }

        public RowChart(Chart one, Chart two)
        {
            One = one;
            Two = two;
        }
    }

    public class Chart
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }

        public string SetId(Continents continent) => $"{Id}_{continent}";
    }
}
