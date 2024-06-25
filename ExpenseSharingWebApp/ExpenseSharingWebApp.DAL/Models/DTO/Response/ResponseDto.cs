using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO.Response
{
    public class ResponseDto
    {
        //To store result, whether it is a single object or a list
        public object? Result { get; set; }
        //Denote whether the request was succesfull or not
        public bool IsSuccess { get; set; } = true;
        //For Error/Success Message
        public string Message { get; set; } = "";
    }
}
