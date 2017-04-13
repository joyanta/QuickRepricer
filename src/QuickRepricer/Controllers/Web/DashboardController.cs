using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickRepricer.Messages.Commands;
using QuickRepricer.Messages.ReqRes;
using QuickRepricer.Messaging;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Messaging.Spec;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.ViewModels.DashboardViewModels;
using System;
using System.Threading.Tasks;

namespace QuickRepricer.Controllers.Web
{
    [Authorize]
    [RequireHttps]
    //[ServiceFilter(typeof(AuthorizeSubscriber))]
    public class DashboardController : Controller
    {
        private MessagingConfiguration _messagingConfig;
        private UserManager<ApplicationUser> _userManager;
        

        public DashboardController(UserManager<ApplicationUser> userManager, 
            MessagingConfiguration messagingConfig)
        {
            _userManager = userManager;
            _messagingConfig = messagingConfig;
        }
        
        
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                if (string.IsNullOrWhiteSpace(user.StripeCustomerId))
                {
                    if (user.ActiveUntil > DateTime.Now)
                    {
                        // active trial session
                        ViewData["TrialEndDate"] = user.ActiveUntil.Date.ToString("d");
                        ViewData["TrailDaysLeft"] = (user.ActiveUntil.Date - DateTime.Today).TotalDays;
                    }
                    else
                    {
                        // not active trial session 
                        ViewData["TrialExpired"] = true;
                    }
                }
                else
                {
                    // was is client and subscription ended;
                    if (user.ActiveUntil.Date < DateTime.Now)
                    {
                        ViewData["SubscriptionEnded"] = user.ActiveUntil.Date.ToString("d");
                    }
                }



                // test
                var vm = new DashboardIndexViewModel()
                {
                    MerchantId = "A2VJ4SRVLLF6SA",
                    MarketPlaceId = "ATVPDKIKX0DER",
                    MwsAuthToken = "amzn.mws.faac2bdf-b67b-1a4b-f4cc-e474ab8e1f21"
                };
                
                return View(vm);
            }

            return RedirectToActionPermanent("About", "App");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(DashboardIndexViewModel vm)
        {
            var sendingQueue = MessageQueueFactory.CreateOutbound("subscribe",
                MessagePattern.RequestResponse, _messagingConfig);

            var responseQueue = sendingQueue.GetResponseQueue();
            

            var user = await GetCurrentUserAsync();
            sendingQueue.Send(new Message
            {
                Body = new SubscribeRequest(userName: user.UserName,
                                                merchantId: vm.MerchantId,
                                                marketPlaceId: vm.MarketPlaceId,
                                                mwsAuthToken: vm.MwsAuthToken),
                ResponseAddress = responseQueue.Address
            });

            string status = string.Empty;
            responseQueue.Receive(x =>
            {
                status = x.BodyAs<SubscribeStartResponse>().Status;  
            });

            // put a status stating a response has happened;
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(DashboardIndexViewModel vm)
        {
            var sendingQueue = MessageQueueFactory.CreateOutbound("unsubscribe",
                MessagePattern.FireAndForget, _messagingConfig);

            var user = await GetCurrentUserAsync();

            sendingQueue.Send(new Message
            {
                Body = new UnsubscribeCommand(user.UserName)
            });

            return RedirectToAction("Index");
        }
        
        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}