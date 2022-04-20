using System;

namespace FridgeApp.Application.DTOs
{
    public class FridgeModelDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Version { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
    }
}