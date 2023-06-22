﻿using static MagicVilla_Utilites.DS;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;

        public string Url { get; set; }

        public Object Datos { get; set; }

        public string Token { get; set; }

    }
}
