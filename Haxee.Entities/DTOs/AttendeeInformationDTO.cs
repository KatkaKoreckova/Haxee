﻿namespace Haxee.Entities.DTOs
{
    public class AttendeeInformationDTO
    {
        public required string CardId { get; set; }

        public required DateTime DateTime { get; set; }
        public required string Stand { get; set; }
    }
}
