using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfTehLab8.Models
{
    public class OrderPositionDto
    {
        public int? ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Введите не отрицательное число товаров")]
        public int? ProductCount { get; set; }
    }
}