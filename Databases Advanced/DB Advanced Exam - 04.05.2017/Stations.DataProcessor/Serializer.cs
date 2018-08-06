using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stations.Data;
using Stations.DataProcessor.Dto.Export;
using Stations.Models;

namespace Stations.DataProcessor
{
    public class Serializer
    {
        public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
        {
            DateTime dateTime = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var trains = context.Trains
                                .Where(x => x.Trips.Any(a => a.Status == TripStatus.Delayed && a.DepartureTime <= dateTime))
                                .Select(x => new
                                {
                                    TrainNumber = x.TrainNumber,
                                    DelayedTrips = x.Trips.Where(a => a.Status == TripStatus.Delayed && a.DepartureTime <= dateTime).ToArray()
                                })
                                .Select(x => new
                                {
                                    TrainNumber = x.TrainNumber,
                                    DelayedTimes = x.DelayedTrips.Count(),
                                    MaxDelayedTime = x.DelayedTrips.Max(z => z.TimeDifference)
                                })
                              .OrderByDescending(x => x.DelayedTimes)
                              .ThenByDescending(x => x.MaxDelayedTime)
                              .ThenBy(x => x.TrainNumber)
                              .ToArray();

            var jsonString = JsonConvert.SerializeObject(trains);

            return jsonString;
        }

        public static string ExportCardsTicket(StationsDbContext context, string cardType)
        {
            Enum.TryParse<CardType>(cardType, out CardType convertedCardType);

            var tickets = context.Cards
                                 .Where(x => x.Type == convertedCardType && x.BoughtTickets.Count > 0)
                                 .Select(x => new CardDto
                                 {
                                     Name = x.Name,
                                     Type = x.Type.ToString(),
                                     Tickets = x.BoughtTickets.Select(a => new TicketDto
                                     {
                                         OriginStation = a.Trip.OriginStation.Name,
                                         DestinationStation = a.Trip.DestinationStation.Name,
                                         DepartureTime = a.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                     }).ToArray()
                                 })
                                 .OrderBy(x => x.Name)
                                 .ToArray();

            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));

            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            StringBuilder sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), tickets, xmlNamespaces);

            return sb.ToString().TrimEnd();
        }
    }
}