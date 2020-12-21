using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Inscripciones_Backend.Testing
{
    public class Utilities : IUtilities
    {
        #region snippet1
        public readonly string BaseAddress = "https://localhost";
        public readonly string LoginBaseAddress = "https://localhost";
        public readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = "yyyy-MM-ddThh:mm:ss.fffZ",
            Converters = new List<JsonConverter>() { new Newtonsoft.Json.Converters.StringEnumConverter() }
        };

        public readonly string _user = "admin@prueba.com";
        public readonly string _password = "Testing1*";

        public string Token = null;

        public string Login()
        {
            if (Token == null)
            {
                Token = "ABC123";

            }
            return Token;
        }

        public Utilities()
        { 
        }
        #endregion
    }
}
