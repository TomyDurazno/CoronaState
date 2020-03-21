using System;
using System.Linq;

namespace CoronaManager
{
    public static class Helpers
    {
        public static string [] GetAllFields(Type t) => t.GetFields()
                                                         .Select(p => p.GetValue(null).ToString())
                                                         .ToArray();
    }
}
