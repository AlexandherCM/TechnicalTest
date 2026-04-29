using App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Presenters
{
    public class Response<T>
    {
        public string status { get; set; } = "success";
        public string message { get; set; }
        public string messageDetail { get; set; }
        public T Data { get; set; }
    }
}   
