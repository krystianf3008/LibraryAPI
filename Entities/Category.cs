﻿namespace LibraryAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid? AddedBy { get; set; }
    }
}
