using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Abstract;
using ModeMan.Ecommerce.Services.Common;

namespace ModeMan.Ecommerce.Services.Concreete
{
    public class SliderService : Service<Slider>, ISliderService
    {
        public SliderService(ModeManDbContext context) : base(context)
        {
        }
    }
}
