﻿using GFData.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO.Orders
{
    public class OrdersDTO
    {
        public int OrderID { get; set; }
        public int GFOrderID { get; set; }
        public int CarID { get; set; }
        public int GarageID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public DateTime TimeAppointment { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
        public string PhoneNumber { get; set; }
        public List<FileOrdersDTO> FileOrders { get; set; }
        public List<ImageOrdersDTO> ImageOrders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
