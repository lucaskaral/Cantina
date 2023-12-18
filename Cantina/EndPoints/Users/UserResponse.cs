﻿using System.Xml.Linq;

namespace CantinaWebAPI.EndPoints.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identification { get; set; }//CPF
        public DateOnly DateOfBirth { get; set; }

    }
}
