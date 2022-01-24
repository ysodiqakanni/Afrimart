using System;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }  
        public DateTime DateCreated { get; set; }  
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
