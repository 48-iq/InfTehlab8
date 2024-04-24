using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfTehLab8.Models
{
    public class Product
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название товара")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Введите не отрицательную цену")]
        [Required(ErrorMessage = "Пожалуйста, введите цену")]
        public decimal? Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Введите не отрицательное количество")]
        public int? CountInStore { get; set; }
    }
}