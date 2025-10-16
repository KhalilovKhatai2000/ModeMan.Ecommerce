using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Abstract;
using ModeMan.Ecommerce.Services.Common;

namespace ModeMan.Ecommerce.Services.Concreete
{
    public class SocialMediaService : Service<SocialMedia>, ISocialMediaService
    {
        public SocialMediaService(ModeManDbContext context) : base(context)
        {
        }
    }
}
