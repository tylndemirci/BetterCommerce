using BetterCommerce.CustomerServiceUI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BetterCommerce.CustomerServiceUI.Controllers
{
    public class SupportController:Controller
    {
        public readonly IHubContext<ChatHub> _HubContext;

        public SupportController(IHubContext<ChatHub> hubContext)
        {
            _HubContext = hubContext;
        }
        
        
    }
}