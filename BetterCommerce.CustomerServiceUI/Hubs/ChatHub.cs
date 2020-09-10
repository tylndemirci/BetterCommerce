using System;
using System.IO;
using System.Threading.Tasks;
using BetterCommerce.CustomerServiceUI.Infrastructure;
using BetterCommerce.CustomerServiceUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BetterCommerce.CustomerServiceUI.Hubs
{
    public class ChatHub: Hub
    {
        
        public readonly IHttpContextAccessor _HttpContextAccessor;
        public readonly IHubContext<ChatHub> _HubContext;
       

        public ChatHub(IHubContext<ChatHub> hubContext, IHttpContextAccessor httpContextAccessor)
        {
            _HubContext = hubContext;
            _HttpContextAccessor = httpContextAccessor;
        }

        [HttpPost("[action]")]
        public async Task<int> SendMessage(string message, int roomId)
        {
            var newMessage = new Message
            {
                RoomId = roomId,
                Text = message,
                UserName = _HttpContextAccessor.HttpContext.User.Identity.Name,
                Timestamp = DateTime.Now
            };
            TxtWriter.WriteToTxt($"{newMessage.UserName}: {newMessage.Text}, {newMessage.Timestamp}, {newMessage.RoomId}");
            await _HubContext.Clients.Group(roomId.ToString())
                .SendAsync("ReceiveMessage", newMessage);
             return 1;
        }
        
        
        public Task JoinRoom(string roomId)
        {
            try
            {
                return Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }


        public Task LeaveRoom(string roomId)
        {
            try
            {
                return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
    }
}