﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AirLineWeb.Dtos
{
    public class FlightDetailReadDto
    {
        public int Id { get; set; }

        public string FlightCode { get; set; }

        public decimal Price { get; set; }
    }
}
