using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfTehLab8.Models
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свою фамилию")]
        public string ClientSurname { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный email")]
        public string ClientEmail { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите телефон")]
        public string ClientPhone { get; set; }
        
    }
}