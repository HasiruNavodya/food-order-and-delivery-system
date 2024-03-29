﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace FODS_API.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet, Route("getneworderslist")]
        [Authorize]
        public JsonResult getNewOrders(int userId)
        {
            string query = @"SELECT * FROM [dbo].[ORDERS] WHERE UserId='" + userId + "' AND OrderStatus='pending' OR OrderStatus='new'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet, Route("getcompletedlist")]
        [Authorize]
        public JsonResult getCompletedOrders(int userId)
        {
            string query = @"SELECT * FROM [dbo].[ORDERS] WHERE UserId='" + userId + "' AND OrderStatus='completed'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpGet, Route("getOrderItems")]
        [Authorize]
        public JsonResult getOrderItems(int orderId)
        {
            string query = @"SELECT * FROM [dbo].[ORDER_ITEMS] WHERE OrderId='" + orderId + "'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpGet, Route("getproductdetails")]
        [Authorize]
        public JsonResult getProductDetails(int productId)
        {
            string query = @"SELECT * FROM [dbo].[PRODUCTS] WHERE ProductId='" + productId + "'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPut, Route("updateorderstatus")]
        [Authorize]
        public JsonResult updateOrderStatus(int orderId,String orderStatus)
        {
            string query = @"UPDATE dbo.ORDERS SET OrderStatus='" + orderStatus + "' WHERE OrderId='"+orderId+"' ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Updated Successfully!");
        }


        [HttpPut, Route("updateorderasdelivered")]
        [Authorize]
        public JsonResult updateOrderAsDelivered(int orderId)
        {
            string query = @"UPDATE dbo.ORDERS SET IsDelivered='1' WHERE OrderId='" + orderId + "' ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Updated Successfully!");
        }

        [HttpGet, Route("fetchneworders")]
        [Authorize]
        public JsonResult fetchNewOrders()
        {
            string query = @"SELECT * FROM [dbo].[ORDERS] WHERE IsProcessed = 0";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet, Route("getprocessedorders")]
        [Authorize]
        public JsonResult GetProcessedOrders()
        {
            string query = @"SELECT * FROM [dbo].[ORDERS] WHERE IsProcessed != 0 AND (IsReceived = 0 OR IsDelivered = 0)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet, Route("getcompletedorders")]
        [Authorize]
        public JsonResult GetCompletedOrders()
        {
            string query = @"SELECT * FROM [dbo].[ORDERS] WHERE IsReceived != 0 AND IsDelivered != 0";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPut, Route("putprocessed")]
        [Authorize]
        public JsonResult PutProcessed(int orderId,int empId)
        {
            string query = @"UPDATE [dbo].[ORDERS] SET IsProcessed =1, EmployeeId='"+empId+"',  OrderStatus='processed' WHERE OrderId =" + orderId;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpGet, Route("getorderlist")]
        [Authorize]
        public JsonResult getOrderlist(int EmployeeId)
        {
            string query = @"SELECT * FROM [dbo].[OrderDetails] WHERE EmployeeId='" + EmployeeId + "' AND IsDelivered='0'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet, Route("getcompleteddeliveryorders")]
        [Authorize]
        public JsonResult getCompletedDeliveryOrders(int EmployeeId)
        {
            string query = @"SELECT * FROM [dbo].[OrderDetails] WHERE EmployeeId='" + EmployeeId + "' AND IsDelivered='1'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost, Route("placeneworder")]
        [Authorize]
        public ActionResult placeOrder(int userId, double price, double lat, double lng, DateTime time)
        {
            string query = @$"INSERT INTO [dbo].[ORDERS] ([UserId],[EmployeeId],[OrderStatus],[IsDelivered],[IsProcessed],[IsReceived],[TotalPrice],[Longitude],[Latitude],[datetime]) 
                VALUES ({userId}, 1, 'pending', 0, 0, 0, {price}, {lng}, {lat}, '{time}') SELECT SCOPE_IDENTITY()";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok(table.Rows[0]["Column1"]);
        }

        [HttpPost, Route("neworder/additem")]
        [Authorize]
        public JsonResult addOrderItem(int orderId, int prodId, int qt)
        {
            string query = @$"INSERT INTO [dbo].[ORDER_ITEMS] ([OrderId],[ProductId],[Quantity]) VALUES ({orderId},{prodId},{qt})";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FODSDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Item Added!");
        }

    }
}
