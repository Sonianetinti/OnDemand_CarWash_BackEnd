using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandCarWashSystem.Models;
using OnDemandCarWashSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;


namespace OnDemandCarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _order.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetOrderAsync")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var package = await _order.GetAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }


        [HttpPost]
        [Route("{id:int}")]
        // [ActionName("SendOrderEmailAsync")]
        public async Task<IActionResult> SendOrderEmail(int id, UserModel user) {
        // public async Task<IActionResult> SendOrderEmail(int id, string email) {
            var package = await _order.GetAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            double totalamount = 0;
            string textBody = "<p> Hello User, </p> <p>Thank you for ordering from Ondemand Car Wash system.</p> <p>Once the order is approved by admin, we will process it</p>";
            textBody += " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + "><tr bgcolor='#4da6ff'><td><b>washingInstructions</b></td> <td> <b> Date</b> </td> <td> <b>Status</b> </td> <td> <b>packageName</b> </td><td> <b>price</b> </td><td> <b>city</b> </td><td> <b>pincode</b> </td></tr>";
            // + " width = " + 400 + "
                 
            textBody += "<tr><td>" + package.WashingInstructions + "</td><td> " + package.Date + "</td><td> " + package.status+ "</td><td> " + package.packageName+"</td><td>" + Convert.ToInt32(package.price) + "</td> <td>" + package.city  + "</td> <td>" + package.pincode  +  "</td></tr>" ;
            totalamount += package.price;

            textBody += "</table> <br>";
            textBody += "<strong>Order Date :</strong>";
            textBody += package.Date.ToShortDateString();
            textBody += "<br><strong>Total Order Amount :</strong>";
            textBody += totalamount;
            textBody += "<br>";
            textBody += "<br><i>If you have any questions, contact us here on <b>netintisonia@gmail.com</b>! " +
            "We are here to help you! </i>";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", "neitintisonia@gmail.com"));
            message.To.Add(new MailboxAddress("Customer", user.Email));
            message.Subject = "Order Placed - Pharmacy Management System";
            message.Body = new TextPart("html") {
                Text = textBody
            };

            using (var client = new SmtpClient()) {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("netintisonia@gmail.com", "uswokbwfulqkyocz");
                client.Send(message);
                client.Disconnect(true);
            }
            return Ok("Email sent Successfully");
        }

        [HttpPost]
        public async Task<IActionResult> AddCarAsync(OrderModel addorder)
        {
            var order = new Models.OrderModel()
            {
                WashingInstructions = addorder.WashingInstructions,
                Date = addorder.Date,
                status = addorder.status,
                packageName = addorder.packageName,
                price = addorder.price,
                city = addorder.city,
                pincode = addorder.pincode,
            }; await _order.AddAsync(order);
            return Ok();
        }
        #region 
        //delete method  
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var order = await _order.DeleteAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
            }
            return Ok();
        }
        #endregion
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateOrderAsync([FromRoute] int id,[FromBody] OrderModel updateorder)
        {
            try
            {
                var order = new Models.OrderModel()
                {
                    WashingInstructions = updateorder.WashingInstructions,
                    Date = updateorder.Date,
                    status = updateorder.status,
                    packageName = updateorder.packageName,
                   
                    price = updateorder.price,
                    city = updateorder.city,
                    pincode = updateorder.pincode,
                };
                order = await _order.UpdateAsync(id, order);
                if (order == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }
    }
}

