﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts
{
    [Table("tbl_locations")]
    public class tbl_locations
    {
        [Key]
        public int id_location { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string address { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string city { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string state { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string country { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string zip { get; set; }

    }
}