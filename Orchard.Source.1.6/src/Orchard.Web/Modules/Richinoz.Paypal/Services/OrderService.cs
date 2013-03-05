using System;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.Data;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrchardServices _orchardServices;

        public OrderService(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public OrderPart Create()
        {

            try
            {
                var order = _orchardServices.ContentManager.New("Order");
                order.As<OrderPart>().Details = "test";
                // order.i
                //order.As<TitlePart>().Title = movieInfo.Name;
                //order.As<BodyPart>().Text = movieInfo.Overview;
                //if (movieInfo.Released.Contains("-"))
                //{
                //    //TMDB Released format is YYYY-MM-DD
                //    order.As<MoviePart>().YearReleased = int.Parse(movieInfo.Released.Split('-')[0]);
                //}
                //order.As<MoviePart>().Rating = (MPAARating)Enum.Parse(typeof(MPAARating), movieInfo.Certification.Replace("-", ""));
                //order.As<MoviePart>().IMDB_Id = movieInfo.ImdbId;
                //order.As<MoviePart>().Tagline = movieInfo.Tagline;
                //order.As<MoviePart>().Keywords = String.Join(",", movieInfo.Keywords.Select(k => k.Trim()));

                //AssignGenres(order, movieInfo);
                //AssignActors(order.As<MoviePart>(), movieInfo);
                _orchardServices.ContentManager.Create(order, VersionOptions.Published);

                return order.As<OrderPart>();
            }
            catch (Exception)
            {
                _orchardServices.TransactionManager.Cancel();
                throw;
            }
        }
    }
}