namespace CoronaManager.Models
{
    public class Continent
    {
        public string Name { get; set; }
        public string [] Countries { get; set; }

        public Continent(string name, string [] countries)
        {
            Name = name;
            Countries = countries;
        }

        public static class Africa 
        {
            public const string Algeria = "Algeria";
            public const string Angola = "Angola";
            public const string Benin = "Benin";
            public const string Botswana = "Botswana";
            public const string Burkina_Faso = "Burkina Faso";
            public const string Burundi = "Burundi";
            public const string Cameroon = "Cameroon";
            public const string Cape_Verde = "Cape Verde";
            public const string Central_African_Republic = "Central African Republic";
            public const string Chad = "Chad";
            public const string Comoros = "Comoros";
            public const string Congo = "Congo";
            public const string Zaire = "Zaire";
            public const string Ivory_Coast = "Ivory Coast";
            public const string Djibouti = "Djibouti";
            public const string Egypt = "Egypt";
            public const string Equatorial_Guinea = "Equatorial Guinea";
            public const string Eritrea = "Eritrea";
            public const string Ethiopia = "Ethiopia";
            public const string Gabon = "Gabon";
            public const string Gambia = "Gambia";
            public const string Ghana = "Ghana";
            public const string Guinea = "Guinea";
            public const string Guinea_Bissau = "Guinea-Bissau";
            public const string Kenya = "Kenya";
            public const string Lesotho = "Lesotho";
            public const string Liberia = "Liberia";
            public const string Libya = "Libya";
            public const string Madagascar = "Madagascar";
            public const string Malawi = "Malawi";
            public const string Mali = "Mali";
            public const string Mauritania = "Mauritania";
            public const string Mauritius = "Mauritius";
            public const string Morocco = "Morocco";
            public const string Mozambique = "Mozambique";
            public const string Namibia = "Namibia";
            public const string Niger = "Niger";
            public const string Nigeria = "Nigeria";
            public const string Rwanda = "Rwanda";
            public const string São_Tomé_and_Príncipe = "São Tomé and Príncipe";
            public const string Senegal = "Senegal";
            public const string Seychelles = "Seychelles";
            public const string Sierra_Leone = "Sierra Leone";
            public const string Somalia = "Somalia";
            public const string South_Africa = "South Africa";
            public const string South_Sudan = "South Sudan";
            public const string Sudan = "Sudan";
            public const string Swaziland = "Swaziland";
            public const string Tanzania = "Tanzania";
            public const string Togo = "Togo";
            public const string Tunisia = "Tunisia";
            public const string Uganda = "Uganda";
            public const string Western_Sahara = "Western Sahara";
            public const string Zambia = "Zambia";
            public const string Zimbabwe = "Zimbabwe";

            public static string Name() => "Africa";

            public static string [] AllCountries() => Helpers.GetAllFields(typeof(Africa));
        }

        public static class Asia
        {
            public const string Afghanistan = "Afghanistan";
            public const string Armenia = "Armenia";
            public const string Azerbaijan = "Azerbaijan";
            public const string Bahrain = "Bahrain";
            public const string Bangladesh = "Bangladesh";
            public const string Bhutan = "Bhutan";
            public const string Brunei = "Brunei";
            public const string Cambodia = "Cambodia";
            public const string China = "China";
            public const string Cyprus = "Cyprus";
            public const string East_Timor = "East Timor";
            public const string Georgia = "Georgia";
            public const string Hong_Kong = "Hong Kong";
            public const string India = "India";
            public const string Indonesia = "Indonesia";
            public const string Iran = "Iran";
            public const string Iraq = "Iraq";
            public const string Israel = "Israel";
            public const string Japan = "Japan";
            public const string Jordan = "Jordan";
            public const string Kazakhstan = "Kazakhstan";
            public const string Kuwait = "Kuwait";
            public const string Kyrgyzstan = "Kyrgyzstan";
            public const string Laos = "Laos";
            public const string Lebanon = "Lebanon";
            public const string Malaysia = "Malaysia";
            public const string Maldives = "Maldives";
            public const string Mongolia = "Mongolia";
            public const string Myanmar = "Myanmar";
            public const string Nepal = "Nepal";
            public const string North_Korea = "North Korea";
            public const string Oman = "Oman";
            public const string Pakistan = "Pakistan";
            public const string Palestine = "Palestine";
            public const string Philippines = "Philippines";
            public const string Qatar = "Qatar";
            public const string Russia = "Russia";
            public const string Saudi_Arabia = "Saudi Arabia";
            public const string Singapore = "Singapore";
            public const string South_Korea = "S. Korea";
            public const string Sri_Lanka = "Sri Lanka";
            public const string Syria = "Syria";
            public const string Tajikistan = "Tajikistan";
            public const string Thailand = "Thailand";
            public const string Turkey = "Turkey";
            public const string Turkmenistan = "Turkmenistan";
            public const string Taiwan = "Taiwan";
            public const string UAE = "UAE";
            public const string Uzbekistan = "Uzbekistan";
            public const string Vietnam = "Vietnam";
            public const string Yemen = "Yemen";

            public static string Name() => "Asia";

            public static string[] AllCountries() => Helpers.GetAllFields(typeof(Asia));
        }

        public static class Europe
        {
            public const string Albania = "Albania";
            public const string Andorra = "Andorra";
            public const string Austria = "Austria";
            public const string Belarus = "Belarus";
            public const string Belgium = "Belgium";
            public const string Bosnia_and_Herzegovina = "Bosnia and Herzegovina";
            public const string Bulgaria = "Bulgaria";
            public const string Croatia = "Croatia";
            public const string Czechia = "Czechia";
            public const string Denmark = "Denmark";
            public const string Estonia = "Estonia";
            public const string Faeroe_Islands = "Faeroe Islands";
            public const string Finland = "Finland";
            public const string France = "France";
            public const string Germany = "Germany";
            public const string Greece = "Greece";
            public const string Gibraltar = "Gibraltar";
            public const string Hungary = "Hungary";
            public const string Iceland = "Iceland";
            public const string Ireland = "Ireland";
            public const string Italy = "Italy";
            public const string Kosovo = "Kosovo";
            public const string Latvia = "Latvia";
            public const string Liechtenstein = "Liechtenstein";
            public const string Lithuania = "Lithuania";
            public const string Luxembourg = "Luxembourg";
            public const string North_Macedonia = "North Macedonia";
            public const string Malta = "Malta";
            public const string Moldova = "Moldova";
            public const string Monaco = "Monaco";
            public const string Montenegro = "Montenegro";
            public const string Netherlands = "Netherlands";
            public const string Norway = "Norway";
            public const string Poland = "Poland";
            public const string Portugal = "Portugal";
            public const string Romania = "Romania";
            public const string Russia = "Russia";
            public const string San_Marino = "San Marino";
            public const string Serbia = "Serbia";
            public const string Slovakia = "Slovakia";
            public const string Slovenia = "Slovenia";
            public const string Spain = "Spain";
            public const string Sweden = "Sweden";
            public const string Switzerland = "Switzerland";
            public const string Ukraine = "Ukraine";
            public const string UK = "UK";
            public const string Vatican_City = "Vatican City";

            public static string Name() => "Europe";

            public static string [] AllCountries() => Helpers.GetAllFields(typeof(Europe));
        }

        public static class North_America
        {
        public const string Antigua_and_Barbuda = "Antigua and Barbuda";
        public const string Anguilla = "Anguilla";
        public const string Aruba_ = "Aruba ";
        public const string The_Bahamas = "The Bahamas";
        public const string Barbados = "Barbados";
        public const string Belize = "Belize";
        public const string Bermuda = "Bermuda";
        public const string Bonaire = "Bonaire";
        public const string British_Virgin_Islands = "British Virgin Islands";
        public const string Canada = "Canada";
        public const string Cayman_Islands = "Cayman Islands";
        public const string Clipperton_Island = "Clipperton Island";
        public const string Costa_Rica = "Costa Rica";
        public const string Cuba = "Cuba";
        public const string Curaçao = "Curaçao";
        public const string Dominica = "Dominica";
        public const string Dominican_Republic = "Dominican Republic";
        public const string El_Salvador = "El Salvador";
        public const string Greenland = "Greenland";
        public const string Grenada = "Grenada";
        public const string Guadeloupe = "Guadeloupe";
        public const string Guatemala = "Guatemala";
        public const string Haiti = "Haiti";
        public const string Honduras = "Honduras";
        public const string Jamaica = "Jamaica";
        public const string Martinique = "Martinique";
        public const string Mexico = "Mexico";
        public const string Montserrat = "Montserrat";
        public const string Navassa_Island = "Navassa Island";
        public const string Nicaragua = "Nicaragua";
        public const string Panama = "Panama";
        public const string Puerto_Rico = "Puerto Rico";
        public const string Saba = "Saba";
        public const string Saint_Barthelemy = "Saint Barthelemy";
        public const string Saint_Kitts_and_Nevis = "Saint Kitts and Nevis";
        public const string Saint_Lucia = "Saint Lucia";
        public const string Saint_Martin = "Saint Martin";
        public const string Saint_Pierre_and_Miquelon = "Saint Pierre and Miquelon";
        public const string Saint_Vincent_and_the_Grenadines = "Saint Vincent and the Grenadines";
        public const string Sint_Eustatius = "Sint Eustatius";
        public const string Sint_Maarten = "Sint Maarten";
        public const string Trinidad_and_Tobago = "Trinidad and Tobago";
        public const string Turks_and_Caicos_ = "Turks and Caicos ";
        public const string USA = "USA";
        public const string Virgin_Islands = "U.S. Virgin Islands";

        public static string Name() => "North America";

        public static string [] AllCountries() => Helpers.GetAllFields(typeof(North_America));
        
        }

        public static class South_America
        {
        public const string Argentina = "Argentina";
        public const string Aruba = "Aruba";
        public const string Bolivia = "Bolivia";
        public const string Brazil = "Brazil";
        public const string Chile = "Chile";
        public const string Colombia = "Colombia";
        public const string Ecuador = "Ecuador";
        public const string Falkland_Islands = "Falkland Islands";
        public const string French_Guiana = "French Guiana";
        public const string Guyana = "Guyana";
        public const string Paraguay = "Paraguay";
        public const string Peru = "Peru";
        public const string South_Georgia_and_the_South_Sandwich_Islands = "South Georgia and the South Sandwich Islands";
        public const string Suriname = "Suriname";
        public const string St_Barth = "St. Barth";
        public const string Uruguay = "Uruguay";
        public const string Venezuela = "Venezuela";

        public static string Name() => "South America";

        public static string[] AllCountries() => Helpers.GetAllFields(typeof(South_America));
        }

        public static class Oceania
        {
            public const string Australia = "Australia";
            public const string Federated_States_of_Micronesia = "Federated States of Micronesia";
            public const string Fiji = "Fiji";
            public const string Guam = "Guam";
            public const string Kiribati = "Kiribati";
            public const string Marshall_Islands = "Marshall Islands";
            public const string Nauru = "Nauru";
            public const string New_Zealand = "New Zealand";
            public const string Palau = "Palau";
            public const string Papua_New_Guinea = "Papua New Guinea";
            public const string Samoa = "Samoa";
            public const string Solomon_Islands = "Solomon Islands";
            public const string Tonga = "Tonga";
            public const string Tuvalu = "Tuvalu";
            public const string Vanuatu = "Vanuatu";
            public const string Flores = "Flores";
            public const string Lombok = "Lombok";
            public const string Melanesia = "Melanesia";
            public const string New_Caledonia = "New Caledonia";
            public const string New_Guinea = "New Guinea";
            public const string Sulawesi = "Sulawesi";
            public const string Sumbawa = "Sumbawa";
            public const string Timor = "Timor";

            public static string Name() => "Oceania";
            public static string [] AllCountries() => Helpers.GetAllFields(typeof(Oceania));
        }
    }   
}
