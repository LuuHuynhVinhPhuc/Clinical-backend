using ClinicalBackend.Domain.Interfaces;
﻿using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Domain.Entities
{
    public class Patient : BaseEntitty<Guid>, IAuditable
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        // day of birth
        public DateOnly DOB { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        // Get DateTime for signin 
        public DateTime CreatedAt { get; set; } // Ngày tạo 
        public DateTime ModifiedAt { get; set; }  // Thời gian sửa
    }
}