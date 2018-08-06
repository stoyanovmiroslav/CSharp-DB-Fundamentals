using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stations.Data;
using Stations.DataProcessor.Dto.Import;
using Stations.Models;

namespace Stations.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            var stationDtos = JsonConvert.DeserializeObject<StationDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Station> stations = new List<Station>();

            foreach (var stationDto in stationDtos)
            {
                if (!IsValid(stationDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var isStationExist = context.Stations.Any(x => x.Name == stationDto.Name);

                if (isStationExist || stations.Any(x => x.Name == stationDto.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (stationDto.Town == null)
                {
                    stationDto.Town = stationDto.Name;
                }

                var station = new Station { Name = stationDto.Name, Town = stationDto.Town };

                stations.Add(station);
                sb.AppendLine(string.Format(SuccessMessage, station.Name));
            }

            context.Stations.AddRange(stations);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            var seatinClasses = JsonConvert.DeserializeObject<SeatingClass[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<SeatingClass> validSeatingClasses = new List<SeatingClass>();

            foreach (var seatingClass in seatinClasses)
            {
                if (!IsValid(seatingClass))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var iSeatingClassExist = context.SeatingClasses.Any(x => x.Name == seatingClass.Name || x.Abbreviation == seatingClass.Abbreviation);

                if (iSeatingClassExist || validSeatingClasses.Any(x => x.Name == seatingClass.Name || x.Abbreviation == seatingClass.Abbreviation))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                validSeatingClasses.Add(seatingClass);
                sb.AppendLine(string.Format(SuccessMessage, seatingClass.Name));
            }

            context.SeatingClasses.AddRange(validSeatingClasses);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
        {
            var trains = JsonConvert.DeserializeObject<TrainDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<Train> validTrains = new List<Train>();

            foreach (var train in trains)
            {
                if (!IsValid(train))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (train.Type == null)
                {
                    train.Type = TrainType.HighSpeed;
                }

                List<TrainSeat> trainSeats = new List<TrainSeat>();
                bool isSeatsValid = true;

                foreach (var seat in train.Seats)
                {
                    var seatinClass = context.SeatingClasses.FirstOrDefault(x => x.Name == seat.Name && x.Abbreviation == seat.Abbreviation);

                    if (seatinClass == null || !IsValid(seat))
                    {
                        isSeatsValid = false;
                        break;
                    }

                    var trainSeat = new TrainSeat { Quantity = seat.Quantity.Value, SeatingClass = seatinClass };
                    trainSeats.Add(trainSeat);
                }

                var isTrainNumberExist = context.Trains.Any(x => x.TrainNumber == train.TrainNumber);

                if (!isSeatsValid || isTrainNumberExist || validTrains.Any(x => x.TrainNumber == train.TrainNumber))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var newTrain = new Train
                {
                    TrainNumber = train.TrainNumber,
                    Type = train.Type,
                    TrainSeats = trainSeats
                };

                validTrains.Add(newTrain);
                sb.AppendLine(string.Format(SuccessMessage, newTrain.TrainNumber));
            }

            context.Trains.AddRange(validTrains);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            var trips = JsonConvert.DeserializeObject<TripDto[]>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });

            StringBuilder sb = new StringBuilder();

            List<Trip> validTrips = new List<Trip>(); 

            foreach (var trip in trips)
            {
                if (!IsValid(trip))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var isDepartureTimeBeforeArrival = trip.DepartureTime < trip.ArrivalTime;

                var train = context.Trains.FirstOrDefault(x => x.TrainNumber == trip.Train);

                var originStation = context.Stations.FirstOrDefault(x => x.Name == trip.OriginStation);

                var destinationStation = context.Stations.FirstOrDefault(x => x.Name == trip.DestinationStation);

                if (train == null || originStation == null || destinationStation == null || !isDepartureTimeBeforeArrival)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var newTrip = new Trip
                {
                    Train = train, OriginStation = originStation,
                    DestinationStation = destinationStation,
                    ArrivalTime = trip.ArrivalTime.Value,
                    DepartureTime = trip.DepartureTime.Value,
                    TimeDifference = trip.TimeDifference,
                    Status = trip.Status
                };

                validTrips.Add(newTrip);
                sb.AppendLine($"Trip from {newTrip.OriginStation.Name} to {newTrip.DestinationStation.Name} imported.");
            }

            context.Trips.AddRange(validTrips);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CustomerCardDto[]), new XmlRootAttribute("Cards"));

            StringBuilder sb = new StringBuilder();

            List<CustomerCard> customerCards = new List<CustomerCard>();

            var cards = (CustomerCardDto[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var card in cards)
            {
                if (!IsValid(card))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }


                var customerCard = new CustomerCard { Name = card.Name, Age = card.Age, Type = card.Type };

                customerCards.Add(customerCard);
                sb.AppendLine(string.Format(SuccessMessage, customerCard.Name));
            }

            context.Cards.AddRange(customerCards);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));

            StringBuilder sb = new StringBuilder();

            var tickets = (TicketDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Ticket> validTickets = new List<Ticket>();

            foreach (var ticket in tickets)
            {
                if (!IsValid(ticket))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var isvalidDateTime = DateTime.TryParseExact(ticket.Trip.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureTime);

                if (!isvalidDateTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var trip = context.Trips.FirstOrDefault(x => x.DepartureTime == departureTime && x.DestinationStation.Name == ticket.Trip.DestinationStation && x.OriginStation.Name == ticket.Trip.OriginStation);

                if (trip == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                CustomerCard customerCard = null;

                if (ticket.Card != null)
                {
                    customerCard = context.Cards.FirstOrDefault(x => x.Name == ticket.Card.Name);
                    if (customerCard == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                }

                bool isValidNumber = int.TryParse(ticket.Seat.Substring(2), out int numberOfSeat);
                string abbreviation = ticket.Seat.Substring(0, 2);

                var train = context.Trains.Include(x => x.TrainSeats).FirstOrDefault(x => x.TrainNumber == trip.Train.TrainNumber);
                var seats = train.TrainSeats.FirstOrDefault(x => x.SeatingClass.Abbreviation == abbreviation);

                if (seats == null || !isValidNumber || numberOfSeat <= 0 || numberOfSeat > seats.Quantity)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var newTicket = new Ticket
                {
                    Price = ticket.Price,
                    SeatingPlace = ticket.Seat,
                    Trip = trip,
                    CustomerCard = customerCard
                };

                validTickets.Add(newTicket);

                sb.AppendLine($"Ticket from {ticket.Trip.OriginStation} to {ticket.Trip.DestinationStation} departing at {departureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)} imported.");
            }

            context.Tickets.AddRange(validTickets);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            return System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}