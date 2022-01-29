using System;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }  
        public DateTime DateCreated { get; set; } = DateTime.UtcNow; 
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }
}
